using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Web;

namespace WebApplication1.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @" select EmployeeId,EmployeeName,Department,convert(varchar(10),DateofJoining,120) as DateofJoining,PhotoFileName from dbo.Employee";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post(Employee emp)
        {
            try
            {
                string query = @"insert into dbo.Employee values(
                               '" + emp.EmployeeName + @"',
                               '" + emp.Department + @"',
                               '" + emp.DateofJoining + @"',
                               '" + emp.PhotoFileName + @"')";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added successfully";
            }
            catch (Exception)
            {
                return "Failed to Add";
            }
        }
        public string Put(Employee emp)
        {
            try
            {
                string query = @"update dbo.Employee set
                                EmployeeName='" + emp.EmployeeName + @"',
                                Department='" + emp.Department + @"',
                                DateofJoining='" + emp.DateofJoining + @"',
                                PhotoFileName='" + emp.PhotoFileName + @"'
                                where EmployeeId=" + emp.EmployeeId + @"";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "updated successfully";
            }
            catch (Exception)
            {
                return "Failed to update";
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @"delete from dbo.Employee  where EmployeeId=" + id + @"";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted successfully";
            }
            catch (Exception)
            {
                return "Failed to delete";
            }
        }
        [Route ("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @" select DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalpath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);
                
                postedFile.SaveAs(physicalpath);
                return filename;
            }
            catch(Exception)
            {
                return "anonymous.png";
            }
        }
    }
}

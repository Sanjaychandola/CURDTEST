using CURDTEST.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace CURDTEST.NewFolder
{
    public class EmployeeDal
    {
        SqlConnection con = null;
        SqlCommand cmd = null;


        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
               AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("MVCConnection");
        }

        public List<Employee> GetAll()
        {
            List<Employee> Emplist = new List<Employee>();
            using(con=new SqlConnection(GetConnectionString()))
            {
                cmd=con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Get_Employees";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Employee emp = new Employee();
                    emp.Id = Convert.ToInt32(dr["Id"]);
                    emp.FirstName = Convert.ToString(dr["FirstName"]);
                    emp.LastName = Convert.ToString(dr["LastName"]);
                    emp.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                    emp.Salary = Convert.ToDouble(dr["Salary"]);
                    emp.Email = Convert.ToString(dr["Email"]);
                    Emplist.Add(emp);

                }
                con.Close();
            }
            return Emplist;
        }


        public bool Insert(Employee model)
        {
            int id = 0;
            using (con = new SqlConnection(GetConnectionString()))
            {
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Insert_Employee";
                cmd.Parameters.AddWithValue("@FirstName ", model.FirstName);
                cmd.Parameters.AddWithValue("@Lastname", model.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Salary", model.Salary);
                con.Open();
                id = cmd.ExecuteNonQuery();
                con.Close();
            }
            return id > 0 ? true : false;
        }

        public Employee GetById(int Id)
        {
            Employee employeebyId = new Employee();
            using (con = new SqlConnection(GetConnectionString()))
            {
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Get_EmployeeById";
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    // Employee employee = new Employee();
                    employeebyId.Id = Convert.ToInt32(dr["Id"]);
                    employeebyId.FirstName = dr["FirstName"].ToString();
                    employeebyId.LastName = dr["LastName"].ToString();
                    employeebyId.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date;
                    employeebyId.Email = dr["Email"].ToString();
                    employeebyId.Salary = Convert.ToDouble(dr["Salary"]);


                }
                con.Close();
            }
            return employeebyId;

        }

        public bool Update(Employee model)
        {
            int id = 0;
            using (con = new SqlConnection(GetConnectionString()))
            {
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Update_Employee";
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Salary", model.Salary);
                con.Open();
                id = cmd.ExecuteNonQuery();
                con.Close();
            } 
            return id > 0 ? true : false;
        }

        public bool Delete(int Id)
        {
            int deletedRowCount = 0;
            using (con = new SqlConnection(GetConnectionString()))
            {
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].usp_Delete_Employee";
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                Id = cmd.ExecuteNonQuery();
                con.Close();
            }
            return deletedRowCount > 0 ? true : false;
        }



    }
}

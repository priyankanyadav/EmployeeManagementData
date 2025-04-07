using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EmployeeData.EmployeeData
{
    public partial class Employee : System.Web.UI.Page
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployeeGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmployeeId.Text))
            {
                // Add new employee
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Employees (Name, Position, Department) VALUES (@Name, @Position, @Department)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                    cmd.Parameters.AddWithValue("@Department", txtDepartment.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                lblMessage.Text = "Employee added successfully!";
            }
            else
            {
                // Update existing employee
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Employees SET Name = @Name, Position = @Position, Department = @Department WHERE EmployeeId = @EmployeeId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@EmployeeId", int.Parse(txtEmployeeId.Text));
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                    cmd.Parameters.AddWithValue("@Department", txtDepartment.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                lblMessage.Text = "Employee updated successfully!";
            }

            clearForm();
            BindEmployeeGrid();
        }

        private void BindEmployeeGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                gvEmployees.DataSource = reader;
                gvEmployees.DataBind();

                con.Close();
            }
        }

        protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditEmployee")
            {
                int employeeId = Convert.ToInt32(e.CommandArgument);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtEmployeeId.Text = reader["EmployeeId"].ToString();
                        txtName.Text = reader["Name"].ToString();
                        txtPosition.Text = reader["Position"].ToString();
                        txtDepartment.Text = reader["Department"].ToString();

                        btnSave.Text = "Update Employee";
                    }
                    con.Close();
                }
            }
        }

        private void clearForm()
        {
            txtEmployeeId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtDepartment.Text = string.Empty;

            btnSave.Text = "Add Employee";
        }
    }
}

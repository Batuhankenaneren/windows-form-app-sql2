using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace visual2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'visualDataSet.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.visualDataSet.Employee);
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Visual;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT CityId, CityName FROM Cities", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();

                        table.Load(reader);

                        comboBox1.DataSource = table;

                        comboBox1.ValueMember = "CityName";

                        comboBox1.DisplayMember = "CityName";
                    }
                }

            }
            this.employeeTableAdapter.Fill(this.visualDataSet.Employee);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;

            int age;
            if (!int.TryParse(textBox2.Text, out age))
            {
                MessageBox.Show("Invalid input for age");
                return;
            }

            string address = textBox3.Text;

            string city = null;
            
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Please select a city");
                return;
            }

            else
            {
                city = comboBox1.SelectedValue.ToString();
            }

            int salary;

            if (!int.TryParse(textBox5.Text, out salary))
            {
                MessageBox.Show("Invalid input for department ID");
                return;
            }

            string status = "";
            if (radioButton1.Checked)
            {
                status = "Single";
            }

            else if (radioButton2.Checked)
            {
                status = "Married";
            }

            else if (radioButton3.Checked)
            {
                status = "Divorced";
            }
            else
            {
                MessageBox.Show("Please select a status");
                return;
            }

            string insurance = "";

            if (checkBox1.Checked)
            {
                insurance = "Insuranced";
            }

            else

            {
                insurance = "Not Insuranced";
            }

            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Visual;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Employee (EmployeeName, EmployeeAge, EmployeeAddress, EmployeeCity, EmployeeSalary, EmployeeStatus, EmployeeInsurance) VALUES (@name, @age, @address, @city, @salary, @status, @insurance)", connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@city", city);
                    command.Parameters.AddWithValue("@salary", salary);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@insurance", insurance);
                    command.ExecuteNonQuery();
                    this.employeeTableAdapter.Fill(this.visualDataSet.Employee);


                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.employeeTableAdapter.Fill(this.visualDataSet.Employee);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int employeeId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Visual;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Employee WHERE EmployeeId = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", employeeId);
                    command.ExecuteNonQuery();
                    this.employeeTableAdapter.Fill(this.visualDataSet.Employee);

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Validate the user input
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to edit");
                return;
            }
            string name = textBox1.Text;
            int age;
            if (!int.TryParse(textBox2.Text, out age))
            {
                MessageBox.Show("Invalid input for age");
                return;
            }
            string address = textBox3.Text;
            string city = null;
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Please select a city");
                return;
            }
            else
            {
                city = comboBox1.SelectedValue.ToString();
            }
            int salary;
            if (!int.TryParse(textBox5.Text, out salary))
            {
                MessageBox.Show("Invalid input for salary");
                return;
            }
            string status = "";
            if (radioButton1.Checked)
            {
                status = "Single";
            }
            else if (radioButton2.Checked)
            {
                status = "Married";
            }
            else if (radioButton3.Checked)
            {
                status = "Divorced";
            }
            else
            {
                MessageBox.Show("Please select a marital status");
                return;
            }
            string insurance;
            if (checkBox1.Checked)
            {
                insurance = "Insuranced";
            }
            else
            {
                insurance = "Not Insuranced";
            }

            // Get the ID of the selected employee from the data grid
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            // Update the employee in the Employee table
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Visual;Integrated Security=True"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Employee SET EmployeeName = @name, EmployeeAge = @age, EmployeeAddress = @address, EmployeeCity = @city, EmployeeSalary = @salary, EmployeeStatus = @status, EmployeeInsurance = @insurance WHERE EmployeeId = @id", connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@city", city);
                    command.Parameters.AddWithValue("@salary", salary);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@insurance", insurance);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }

            // Refresh the data in the visualDataSet.Employee table and update the data displayed on the form
            this.employeeTableAdapter.Fill(this.visualDataSet.Employee);

            // Clear the form fields
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox5.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            checkBox1.Checked = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox5.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            checkBox1.Checked = false;
            textBox1.Focus();
        }
    }
    
}

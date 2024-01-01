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

namespace CBTExam
{
    public partial class Login : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=CBTExam;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setup();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            setup();
            panel4.Visible = true;
        }
        public void setup()
        {
            panel3.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            btnLogOut.Visible = false;
            btnExam.Visible = false;
            txtExam.Clear();
            txtReg.Clear();
            txtReg.Focus();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnExam_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string query3 = "Select * From RegisterTable Where Registration_No = @Registration_No And Exam_ID = @Exam_ID";
                var command3 = new SqlCommand(query3, connection);
                command3.Parameters.Add("@Registration_No", SqlDbType.Int).Value = txtReg.Text;
                command3.Parameters.Add("@Exam_ID", SqlDbType.VarChar).Value = txtExam.Text;
                var adapter1 = new SqlDataAdapter(command3);
                var table2 = new DataTable();
                adapter1.Fill(table2);
                if (table2.Rows.Count == 1)
                {
                    Exam exam = new Exam();
                    exam.lblReg.Text = table2.Rows[0][1].ToString();
                    exam.lblFirstName.Text = table2.Rows[0][2].ToString();
                    exam.lblMiddleName.Text = table2.Rows[0][3].ToString();
                    exam.lblSurname.Text = table2.Rows[0][4].ToString();
                    exam.lblExam.Text = table2.Rows[0][5].ToString();
                    exam.Show();
                    this.Hide();
                    txtExam.Clear();
                    txtReg.Clear();
                }

            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtReg.Text == "" && txtExam.Text == "")
            {
                MessageBox.Show("Text field cannot be empty", "Empty Text Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if ( txtExam.Text == "")
            {
                MessageBox.Show("Exam number field cannot be empty", "Empty Text Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if ( txtReg.Text == "")
            {
                MessageBox.Show("Registration number field cannot be empty", "Empty Text Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // To check if the user has already wirtten the exam
            try
            {
                connection.Open();
                string query11 = "Select * From ExamTable Where Registration_No = @Registration_No And Exam_ID = @Exam_ID";
                var command11 = new SqlCommand(query11, connection);
                command11.Parameters.Add("@Registration_No", SqlDbType.Int).Value = txtReg.Text;
                command11.Parameters.Add("@Exam_ID", SqlDbType.VarChar).Value = txtExam.Text;
                var adapter11 = new SqlDataAdapter(command11);
                var table11 = new DataTable();
                adapter11.Fill(table11);
                if (table11.Rows.Count == 1)
                {
                    MessageBox.Show("You have already written the Examination ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception err1)
            {

                MessageBox.Show(err1.Message);
            }
            finally
            {
                connection.Close();
            }
            // To Login 
            try
            {
                connection.Open();
                string query2 = "Select * From RegisterTable Where Registration_No = @Registration_No And Exam_ID = @Exam_ID";
                var command2 = new SqlCommand(query2, connection);
                command2.Parameters.Add("@Registration_No", SqlDbType.Int).Value = txtReg.Text;
                command2.Parameters.Add("@Exam_ID", SqlDbType.VarChar).Value = txtExam.Text;
                var adapter = new SqlDataAdapter(command2);
                var table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count == 1)
                {
                    MessageBox.Show("Successfully Login ", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panel3.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;
                    label15.Visible = true;
                    btnLogOut.Visible = true;
                    btnExam.Visible = true;
                    panel4.Visible = false;
                    lblReg.Text = table.Rows[0][1].ToString();
                    lblFirstName.Text = table.Rows[0][2].ToString();
                    lblMiddleName.Text = table.Rows[0][3].ToString();
                    lblSurname.Text = table.Rows[0][4].ToString();
                    lblExam.Text = table.Rows[0][5].ToString();
                    //Exam exam = new Exam();
                    //exam.lblReg.Text = table.Rows[0][1].ToString();
                    //exam.lblFirstName.Text = table.Rows[0][2].ToString();
                    //exam.lblMiddleName.Text = table.Rows[0][3].ToString();
                    //exam.lblSurname.Text = table.Rows[0][4].ToString();
                    //exam.lblExam.Text = table.Rows[0][5].ToString();

                }
                else
                {
                    MessageBox.Show("Wrong Regitration No and Exam ID", "Invalid Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }
            finally
            {
                connection.Close();
            }
           
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

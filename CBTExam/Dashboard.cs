using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CBTExam
{
    public partial class Dashboard : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=CBTExam;Integrated Security=True");
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnStartExam_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure You want to exit this application", "Exit Applicstion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            panel3.Visible = false;
            txtExamStudents.Visible = false;
            txtRegisStu.Visible = false;
            textBox1.Visible = false;
            textBox3.Visible = false;
        }

        private void btnViewStudent_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView1.Location = new Point(322, 256);
            dataGridView1.Width = 815;
            dataGridView1.Height = 433;
            txtRegisStu.Visible = true;
            textBox1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            txtExamStudents.Visible = false;
            textBox3.Visible = false;
            var adapters = new SqlDataAdapter("Select ID, Registration_No, Firstname, Middlename, Surname, Exam_ID From RegisterTable", connection);
            var table = new DataTable();
            adapters.Fill(table);
            dataGridView1.DataSource = table;
            var adapter = new SqlDataAdapter("Select ID, Registration_No, Firstname, Middlename, Surname, Exam_ID From RegisterTable", connection);
            var table1 = new DataTable();
            adapter.Fill(table1);
            dataGridView1.DataSource = table1;
            txtRegisStu.Text = dataGridView1.RowCount.ToString();
            int total = Convert.ToInt32(txtRegisStu.Text);
            total = total - 1;
            txtRegisStu.Text = total.ToString();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel3.Location = new Point(528, 265);
            dataGridView1.Visible = false;
            textBox3.Visible = false;
            txtExamStudents.Visible = false;
            txtRegisStu.Visible = false;
            textBox1.Visible = false;

        }

        private void btnX_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = true;
            txtExamId.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtRegNo.Clear();
            txtSurname.Clear();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            txtExamStudents.Visible = true;
            textBox3.Location = new Point(30, 273);
            txtExamStudents.Location = new Point(30, 347);
            textBox3.Visible = true;
            dataGridView1.Location = new Point(322, 256);
            dataGridView1.Width = 1000;
            dataGridView1.Height = 433;
            panel2.Visible = false;
            panel3.Visible = false;
            txtRegisStu.Visible = false;
            textBox1.Visible = false;
            var adapter2 = new SqlDataAdapter("Select ID, Registration_No, Firstname, Middlename, Surname, Total, Total_Score, Subject, Time From ExamTable", connection);
            var table3 = new DataTable();
            adapter2.Fill(table3);
            dataGridView1.DataSource = table3;
            var adapter3 = new SqlDataAdapter("Select ID, Registration_No, Firstname, Middlename, Surname, Total, Total_Score, Subject, Time From ExamTable", connection);
            var table4 = new DataTable();
            adapter3.Fill(table4);
            dataGridView1.DataSource = table4;
            txtExamStudents.Text = dataGridView1.RowCount.ToString();
            int total = Convert.ToInt32(txtExamStudents.Text);
            total = total - 1;
            txtExamStudents.Text = total.ToString();
        }
        // To generate random for exam Id
        public void ramdomNumer()
        {
            Random rnum;
            int num;
            rnum = new System.Random();
            num = rnum.Next(1111, 9999);
            txtExamId.Text ="DCept" + num.ToString();
        }

        private void txtRegNo_TextChanged(object sender, EventArgs e)
        {
            ramdomNumer();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            //panel2.Visible = false;
            panel3.Visible = false;
            dataGridView1.Visible = false;
            textBox3.Visible = false;
            txtExamStudents.Visible = false;
            txtRegisStu.Visible = false;
            textBox1.Visible = false;
            MessageBox.Show("Not Available Now", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {   //To Save record into the database
            try
            {
                connection.Open();
                string query = "Insert into RegisterTable values (@Registration_No, @Firstname, @Middlename, @Surname, @Exam_ID)";
                var command = new SqlCommand(query, connection);
                command.Parameters.Add("@Registration_No", SqlDbType.Int).Value = txtRegNo.Text;
                command.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = txtFirstName.Text;
                command.Parameters.Add("@Middlename", SqlDbType.VarChar).Value = txtMiddleName.Text;
                command.Parameters.Add("@Surname", SqlDbType.VarChar).Value = txtSurname.Text;
                command.Parameters.Add("@Exam_ID", SqlDbType.VarChar).Value = txtExamId.Text;
                command.ExecuteNonQuery();
               
                MessageBox.Show("Data Successfully Saved....", "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFirstName.Clear();
                txtMiddleName.Clear();
                txtSurname.Clear();
                txtRegNo.Clear();
                txtExamId.Clear();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // To Update the Record in the database
            try
            {
                connection.Open();
                string qeury = "Update RegisterTable set Registration_No = @Registration_No, Firstname = @Firstname, Middlename = @Middlename, Surname = @Surname, Exam_ID = @Exam_ID";
                var cmd = new SqlCommand(qeury, connection);
                cmd.Parameters.Add("@Registration_No", SqlDbType.Int).Value = txtRegNo.Text;
                cmd.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = txtFirstName.Text;
                cmd.Parameters.Add("@Middlename", SqlDbType.VarChar).Value = txtMiddleName.Text;
                cmd.Parameters.Add("@Surname", SqlDbType.VarChar).Value = txtSurname.Text;
                cmd.Parameters.Add("@Exam_ID", SqlDbType.VarChar).Value = txtExamId.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Updated Successfully ....", "Data Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFirstName.Clear();
                txtMiddleName.Clear();
                txtSurname.Clear();
                txtRegNo.Clear();
                txtExamId.Clear();
               
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // To delete record from the database
            try
            {
                connection.Open();
                string quert = "Delete From RegisterTable Where Registration_No = @Registration_No";
                var cmd1 = new SqlCommand(quert, connection);
                cmd1.Parameters.Add("@Registration_No", SqlDbType.Int).Value = txtRegNo.Text;
                cmd1.ExecuteNonQuery();
          
                if (MessageBox.Show("Are You sure You want to delete this Record", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    MessageBox.Show("Data Deleted Successfully ....", "Data Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFirstName.Clear();
                    txtMiddleName.Clear();
                    txtSurname.Clear();
                    txtRegNo.Clear();
                    txtExamId.Clear();
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
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // To Search Record from the database
            try
            {
                connection.Open();
                string query2 = "Select * From RegisterTable Where Exam_ID = @Exam_ID";
                var command2 = new SqlCommand(query2, connection);
                command2.Parameters.Add("@Exam_ID", SqlDbType.VarChar).Value = txtExamId.Text;
                var adapter = new SqlDataAdapter(command2);
                var table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count == 1)
                {
                    txtRegNo.Text = table.Rows[0][1].ToString();
                    txtFirstName.Text = table.Rows[0][2].ToString();
                    txtMiddleName.Text = table.Rows[0][3].ToString();
                    txtSurname.Text = table.Rows[0][4].ToString();
                    txtExamId.Text = table.Rows[0][5].ToString();
                   
                }
            }
            catch (Exception erro2)
            {
                MessageBox.Show(erro2.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

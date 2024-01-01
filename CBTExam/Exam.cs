using System;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CBTExam
{
    public partial class Exam : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=CBTExam;Integrated Security=True");
        public Exam()
        {
            InitializeComponent();
        }

        private void Exam_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Timer Code
            DateTime date = DateTime.Now;
            lblDate.Text = date.ToString();
            int sec = Convert.ToInt16(lblSeconds.Text);
            int min = Convert.ToInt16(lblMinutes.Text);
            int milliSecond = Convert.ToInt16(txtMillsecond.Text);
            milliSecond += 7;
            txtMillsecond.Text = milliSecond.ToString();
            if (milliSecond >= 59)
            {
                milliSecond = 0;
                txtMillsecond.Text = milliSecond.ToString();
                sec -= 1;
                lblSeconds.Text = sec.ToString();
            }
            
            if ( sec == 0)
            {
                sec = 60;
                lblSeconds.Text = sec.ToString();
                min -= 1;
                lblMinutes.Text = min.ToString();
            }
            if ( min == 3)
            {
                txtMillsecond.BackColor = Color.Red;
                lblSeconds.BackColor = Color.Red;
                lblMinutes.BackColor = Color.Red;
            }
            if ( min == 0)
            {
                timer1.Stop();
                txtMillsecond.Text = "0";
                lblMinutes.Text = "20";
                lblSeconds.Text = "60";
                //To Submit Exam When the time is up
                try
                {  // To save data in the database
                    connection.Open();
                    string query = "Insert into ExamTable values(@Registration_No, @Firstname, @Middlename, @Surname, @Total, @Total_Score, @Subject, @Exam_ID, @Time)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.Add("@Registration_No", SqlDbType.Int).Value = lblReg.Text;
                    command.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = lblFirstName.Text;
                    command.Parameters.Add("@Middlename", SqlDbType.VarChar).Value = lblMiddleName.Text;
                    command.Parameters.Add("@Surname", SqlDbType.VarChar).Value = lblSurname.Text;
                    command.Parameters.Add("@Total", SqlDbType.Int).Value = lblCount.Text;
                    command.Parameters.Add("@Total_Score", SqlDbType.Int).Value = txtRight.Text;
                    command.Parameters.Add("@Subject", SqlDbType.VarChar).Value = lblSubject.Text;
                    command.Parameters.Add("@Exam_ID", SqlDbType.VarChar).Value = lblExam.Text;
                    command.Parameters.Add("@Time", SqlDbType.VarChar).Value = lblDate.Text;
                    command.ExecuteNonQuery();
                   // MessageBox.Show(" Examination Successfully Submitted....", "Examination Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRight.Text = "0";
                    txtWrong.Text = "0";
                    btnNext.Text = "NEXT";
                    lblCount.Text = "1";
                    txtCheck.Text = "0";
                    lblMinutes.Text = "19";
                    lblSeconds.Text = "59";
                    txtMillsecond.Text = "0";
                   // timer1.Stop();
                    Login login = new Login();
                    login.Show();
                    this.Hide();
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

        }

        private void Exam_Load(object sender, EventArgs e)
        {
            timer1.Start();
            question();
            option1.Checked = false;
            option2.Checked = false;
            option3.Checked = false;
            option4.Checked = false;
            txtRight.Visible = false;
            txtWrong.Visible = false;
            txtCheck.Visible = false;
            txtAnswer.Visible = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int correct = Convert.ToInt32(txtRight.Text);
            int wrong = Convert.ToInt32(txtWrong.Text);
            int check = Convert.ToInt32(txtCheck.Text);
            int total = correct + wrong;
            // If no option is selected before clicking on next
            check = 0;
            if (option1.Checked == false && option2.Checked == false && option3.Checked == false && option4.Checked == false)
            {
                MessageBox.Show("Please select option given to you", "No Option Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // If Option1 is the correct answer
            if (option1.Checked && option1.Text == txtAnswer.Text)
            {
                correct += 1;
                txtRight.Text = correct.ToString();
                check += 1;
                txtCheck.Text = check.ToString();
            }
            // If Option2 is the correct answer
            else if (option2.Checked && option2.Text == txtAnswer.Text)
            {
                correct += 1;
                txtRight.Text = correct.ToString();
                check += 1;
                txtCheck.Text = check.ToString();
            }
            // If Option3 is the correct answer
            else if (option3.Checked && option3.Text == txtAnswer.Text)
            {
                correct += 1;
                txtRight.Text = correct.ToString();
                check += 1;
                txtCheck.Text = check.ToString();
            }
            // If Option4 is the correct answer
            else if (option4.Checked && option4.Text == txtAnswer.Text)
            {
                correct += 1;
                txtRight.Text = correct.ToString();
                check += 1;
                txtCheck.Text = check.ToString();
            }
            // If selected Option is the wrong answer
            else
            {
                check = 0;
                txtCheck.Text = check.ToString();
                wrong += 1;
                txtWrong.Text = wrong.ToString();
            }
            // To go to the next question
            int next = Convert.ToInt32(lblCount.Text);
            if (lblCount.Text == "29")
            {
                btnNext.Text = "SUBMIT EXAM";
            }
            if (next == 30)
            {
                // To Submit
                btnNext.Text = "SUBMIT EXAM";
                if (MessageBox.Show("That is all for Exam. Do you wish to submit now", "Examination Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {  // To save data in the database
                        connection.Open();
                        string query = "Insert into ExamTable values(@Registration_No, @Firstname, @Middlename, @Surname, @Total, @Total_Score, @Subject, @Exam_ID, @Time)";
                        var command = new SqlCommand(query, connection);
                        command.Parameters.Add("@Registration_No", SqlDbType.Int).Value = lblReg.Text;
                        command.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = lblFirstName.Text;
                        command.Parameters.Add("@Middlename", SqlDbType.VarChar).Value = lblMiddleName.Text;
                        command.Parameters.Add("@Surname", SqlDbType.VarChar).Value = lblSurname.Text;
                        command.Parameters.Add("@Total", SqlDbType.Int).Value = lblCount.Text;
                        command.Parameters.Add("@Total_Score", SqlDbType.Int).Value = txtRight.Text;
                        command.Parameters.Add("@Subject", SqlDbType.VarChar).Value = lblSubject.Text;
                        command.Parameters.Add("@Exam_ID", SqlDbType.VarChar).Value = lblExam.Text;
                        command.Parameters.Add("@Time", SqlDbType.VarChar).Value = lblDate.Text;
                        command.ExecuteNonQuery();
                        MessageBox.Show(" Examination Successfully Submitted....", "Examination Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRight.Text = "0";
                        txtWrong.Text = "0";
                        btnNext.Text = "NEXT";
                        lblCount.Text = "1";
                        txtCheck.Text = "0";
                        lblMinutes.Text = "19";
                        lblSeconds.Text = "59";
                        txtMillsecond.Text = "0";
                        timer1.Stop();
                        Login login = new Login();
                        login.Show();
                        this.Hide();
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
                
            }
            else
            {
                next += 1;
                lblCount.Text = next.ToString();
                question();
                option1.Checked = false;
                option2.Checked = false;
                option3.Checked = false;
                option4.Checked = false;
            }
        }
        // To fetch the questions from the database where the question is stored
        public void question()
        {
            try
            {
                connection.Open();
                string query3 = "Select * from QuestionTable Where ID = @ID";
                var Command3 = new SqlCommand(query3, connection);
                Command3.Parameters.Add("@ID", SqlDbType.Int).Value = lblCount.Text;
                var adapter = new SqlDataAdapter(Command3);
                var table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count == 1)
                {
                    txtQuestion.Text = table.Rows[0][1].ToString();
                    option1.Text = table.Rows[0][2].ToString();
                    option2.Text = table.Rows[0][3].ToString();
                    option3.Text = table.Rows[0][4].ToString();
                    option4.Text = table.Rows[0][5].ToString();
                    txtAnswer.Text = table.Rows[0][6].ToString();
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

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            btnNext.Text = "NEXT";
            int correct = Convert.ToInt32(txtRight.Text);
            int wrong = Convert.ToInt32(txtWrong.Text);
            int check = Convert.ToInt32(txtCheck.Text);
            int previous = Convert.ToInt32(lblCount.Text);
            // If it is the first question
            if(previous <= 1)
            {
                MessageBox.Show("This is the first question");
                return;
            }
            else
            {
                previous -= 1;
                lblCount.Text = previous.ToString();
                question();
                option1.Checked = false;
                option2.Checked = false;
                option3.Checked = false;
                option4.Checked = false;
            }
            //if the last answer was correct       
            if (correct > 0 && check == 1)
            {
               // correct = 0;
                correct--;
                txtRight.Text = correct.ToString();
                //check = 0;
                //txtCheck.Text = check.ToString();
            }
           else if (wrong > 0 && check == 0)
           {
                wrong--;
                txtWrong.Text = wrong.ToString();
           }
           else if ( correct > 0 && check == 0)
           {
                correct--;
                txtRight.Text = correct.ToString();
           }
            else if (wrong > 0 && check == 1)
            {
                wrong--;
                txtWrong.Text = wrong.ToString();
            }
           if (wrong == 0 &&  correct == 0)
           {
                check = 0;
           }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are Sure you want to end this examination now", "End Examination", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {  // To save data in the database
                    connection.Open();
                    string query = "Insert into ExamTable values(@Registration_No, @Firstname, @Middlename, @Surname, @Total, @Total_Score, @Subject, @Exam_ID, @Time)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.Add("@Registration_No", SqlDbType.Int).Value = lblReg.Text;
                    command.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = lblFirstName.Text;
                    command.Parameters.Add("@Middlename", SqlDbType.VarChar).Value = lblMiddleName.Text;
                    command.Parameters.Add("@Surname", SqlDbType.VarChar).Value = lblSurname.Text;
                    command.Parameters.Add("@Total", SqlDbType.Int).Value = lblCount.Text;
                    command.Parameters.Add("@Total_Score", SqlDbType.Int).Value = txtRight.Text;
                    command.Parameters.Add("@Subject", SqlDbType.VarChar).Value = lblSubject.Text;
                    command.Parameters.Add("@Exam_ID", SqlDbType.VarChar).Value = lblExam.Text;
                    command.Parameters.Add("@Time", SqlDbType.VarChar).Value = lblDate.Text;
                    command.ExecuteNonQuery();
                    MessageBox.Show(" Examination Successfully Submitted....", "Examination Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRight.Text = "0";
                    txtWrong.Text = "0";
                    btnNext.Text = "NEXT";
                    lblCount.Text = "1";
                    txtCheck.Text = "0";
                    lblMinutes.Text = "19";
                    lblSeconds.Text = "59";
                    txtMillsecond.Text = "0";
                    timer1.Stop();
                    Login login = new Login();
                    login.Show();
                    this.Hide();
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
        }
    }
}

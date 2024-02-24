using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Side_Steel_Management_System
{
    public partial class forgetPasswordFr : Form{
        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        String seca;
        String secq;
        int id = 0;
        string st = "";
        string middle = "";
        public forgetPasswordFr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loginFr myform = new loginFr();
            this.Hide();
            myform.ShowDialog();
            this.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox2.Text != "")
            {
                CommandString = "select u.secretAns from users as u join Employees as e on  u.id=e.id where e.First_Name=@name";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                MyCommand.Parameters.AddWithValue("@name", usertxb.Text);
                try
                {
                    MyConnection.Open();

                    seca = MyCommand.ExecuteScalar().ToString();
               
                    if (seca == bunifuTextBox2.Text)
                    {
                        CommandString = "select u.id from users as u join Employees as e on  u.id=e.id where e.First_Name=@name";
                        MyCommand = new SqlCommand(CommandString, MyConnection);
                        MyCommand.Parameters.AddWithValue("@name", usertxb.Text);
                        try
                        {
                            if (MyCommand.ExecuteScalar() == null)
                            {
                                id = 0;
                            }
                            else
                            {


                                id = int.Parse(MyCommand.ExecuteScalar().ToString());
                                string pr = "";
                                CommandString = "select permission from Users where id=@id";
                                MyCommand = new SqlCommand(CommandString, MyConnection);
                                MyCommand.Parameters.AddWithValue("@id", id);
                                try
                                {
                                    pr = MyCommand.ExecuteScalar().ToString();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                                if (pr == "False")
                                {
                                    txterror.Text = "User Isn't Permitted to Access The System";
                                }
                                else
                                {
                                    CommandString = "select User_type from Employees where id=@id";
                                    MyCommand = new SqlCommand(CommandString, MyConnection);
                                    MyCommand.Parameters.AddWithValue("@id", id);
                                    try
                                    {
                                        st = MyCommand.ExecuteScalar().ToString();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }


                                    CommandString = "select Middle_Name from Employees where id=@id";
                                    MyCommand = new SqlCommand(CommandString, MyConnection);
                                    MyCommand.Parameters.AddWithValue("@id", id);
                                    try
                                    {
                                        middle = MyCommand.ExecuteScalar().ToString();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }


                                    this.Hide();
                                    DashboardFr myform = new DashboardFr();
                                    /// Program prog = new Program();

                                    Program.firstname = usertxb.Text;
                                    Program.middlname = middle;
                                    Program.usertp = st;
                                    Program.USRid = id;

                                    changestata();
                                    myform.ShowDialog();
                                    this.Close();
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }






                    }
                        else
                    {
                        MessageBox.Show("wrong answer");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MyConnection.Close();
            }
            else
                txterror.Text = "Enter You Secret Answer";
        }
        private void fill()
        {
            CommandString = "select u.secretQ from users as u join Employees as e on  u.id=e.id where e.First_Name=@name";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@name", usertxb.Text);
            try
            {
                MyConnection.Open();
                if (MyCommand.ExecuteScalar() == null)
                    secq = "";
                else
                {
                    secq = MyCommand.ExecuteScalar().ToString();
                }
                bunifuTextBox1.Text = secq;
                MyConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void forgetPasswordFr_Load(object sender, EventArgs e)
        {
            fill();
        }


        void changestata()
        {
            CommandString = "update users set status= '" + "Active" + "'where id=@id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", id);
            try
            {
                MyCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

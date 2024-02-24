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
    public partial class loginFr : Form
    {
        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        static int pass = 0;
        static int id = 0;
        static string st = "";
        static string middle = "";
        public loginFr()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == ""|| txtPass.Text == "")
                txterror.Text = "Missing Username Or Password";
            else { 
            CommandString = "select u.id from users as u join Employees as e on  u.id=e.id where e.First_Name=@name";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@name", txtUser.Text);
            try
            {
                MyConnection.Open();
                if (MyCommand.ExecuteScalar() == null)
                    id = 0;
                else
                {
                    id = int.Parse(MyCommand.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();
                if (id == 0)
                {
                    txterror.Text = "User Not Found";
                }
                

                
                else
                {
                    string pr = "";
                    CommandString = "select permission from Users where id=@id";
                    MyCommand = new SqlCommand(CommandString, MyConnection);
                    MyCommand.Parameters.AddWithValue("@id", id);
                    try
                    {
                        MyConnection.Open();
                        pr = MyCommand.ExecuteScalar().ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    MyConnection.Close();

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
                            MyConnection.Open();
                            st = MyCommand.ExecuteScalar().ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        MyConnection.Close();
                        CommandString = "select passwordd from users where id=@id";
                        MyCommand = new SqlCommand(CommandString, MyConnection);
                        MyCommand.Parameters.AddWithValue("@id", id);
                        try
                        {
                            MyConnection.Open();
                            pass = int.Parse(MyCommand.ExecuteScalar().ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        MyConnection.Close();
                        int pa = 0;
                        try
                        {
                            pa = int.Parse(txtPass.Text);
                        }
                        catch
                        {
                            txterror.Text = "Invalid Input";
                        }
                        if (st == "inactive")
                        {
                            MessageBox.Show("inactive account");
                        }


                        else if (pass == pa)
                        {
                            CommandString = "select Middle_Name from Employees where id=@id";
                            MyCommand = new SqlCommand(CommandString, MyConnection);
                            MyCommand.Parameters.AddWithValue("@id", id);
                            try
                            {
                                MyConnection.Open();
                                middle = MyCommand.ExecuteScalar().ToString();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            MyConnection.Close();

                            this.Hide();
                            DashboardFr myform = new DashboardFr();
                            /// Program prog = new Program();

                            Program.firstname = txtUser.Text;
                            Program.middlname = middle;
                            Program.usertp = st;
                            Program.USRid = id;

                            changestata();
                            myform.ShowDialog();
                            this.Close();
                        }

                        else
                        {
                            txterror.Text = "Incorrect Password";
                        }
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtUser.Text != "")
            {
                CommandString = "select u.id from users as u join Employees as e on  u.id=e.id where e.First_Name=@name";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                MyCommand.Parameters.AddWithValue("@name", txtUser.Text);
                try
                {
                    MyConnection.Open();
                    if (MyCommand.ExecuteScalar() == null)
                        txterror.Text = "This User Is Not Registerid";
                    else
                    {
                        forgetPasswordFr myform = new forgetPasswordFr();
                        myform.usertxb.Text = txtUser.Text;
                        this.Hide();
                        myform.ShowDialog();
                        this.Close();
                    }
                    MyConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //this.Close();
            }
            else
                txterror.Text = "Please Enter Username...";
        }

        private void loginFr_Load(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if(txtPass.Text != "")
            {
                txtPass.UseSystemPasswordChar = true;
            }
            else
                txtPass.UseSystemPasswordChar = false;
        }
    
        void changestata()
        {
            CommandString = "update users set status= '" +"Active"+ "'where id=@id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", id);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtPass.Text != "" && checkBox1.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else if (txtPass.Text != "" && checkBox1.Checked == false)
                txtPass.UseSystemPasswordChar = true;

            else if (txtPass.Text == "" && checkBox1.Checked == false)
                txtPass.UseSystemPasswordChar = false;

            else if(txtPass.Text == "" && checkBox1.Checked == true)
                txtPass.UseSystemPasswordChar = false;
        }
    }
}

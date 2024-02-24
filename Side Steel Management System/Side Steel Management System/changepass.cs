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
    public partial class changepass : Form
    {
        bool missing = false;
        int pas = 0;
        bool chechpassintegrity = true;

        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();

        public changepass()
        {
            InitializeComponent();
        }

        private void changepass_Load(object sender, EventArgs e)
        {
            txtPass.Focus();
            txtusername.Text = Program.firstname;
        }
        void checkEmpty()
        {

            doublecheck();
            if (txtPass.Text == "" || txtconfPass.Text == "")
            {
                missing = true;
                txterr.Text = "Missing Fields.. all field must filled";
            }
            else if (txtPass.Text.Length < 6)
            {
                missing = true;
                txterr.Text = "Password should be at least 6 numbers long";
            }
            else if (txtPass.Text.Length > 14)
            {
                missing = true;
                txterr.Text = "The password is too long";
            }
            //else if(!chechpassintegrity)
            //{
              //  missing = true;
               // txterr.Text = "The password should contain a symobl, one uppercase, and lowercases";
            //}

            else if (txtPass.Text != txtconfPass.Text)
            {
                missing = true;
                txterr.Text = "Passwords must match";
            }
            else
            {
                try
                {
                    pas = int.Parse(txtPass.Text);
                    missing = false;
                }
                catch (Exception ex)
                {
                    missing = true;
                    txterror.Text = "Invalid Input";
                }
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            int id = Program.USRid;
            checkEmpty();
            if (!missing )
            {
                //CommandString = "insert into MInventory(Item_Name, Quatity, Quality , Size ,Cost ,Providerr) values " +
                //     "( '" + ItemName.Text + "', '" + ItemQuantity.Text + "', '" + ItemQuality.Text + "', '" + ItemSize.Text + "', '" + ItemCost.Text + "' , '" + ItemProv.Text + "' );";


                CommandString = "update Users set passwordd = '" + pas+ "' where id = '"+id+"'";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    MyCommand.ExecuteNonQuery();
                    MessageBox.Show("Password has been Updated");
                    this.Close();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                MyConnection.Close();
         
            }
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

            else if (txtPass.Text == "" && checkBox1.Checked == true)
                txtPass.UseSystemPasswordChar = false;
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (txtPass.Text != "")
            {
                txtPass.UseSystemPasswordChar = true;
            }
            else
                txtPass.UseSystemPasswordChar = false;
        }

        private void txtconfPass_TextChanged(object sender, EventArgs e)
        {
            if (txtconfPass.Text != "")
            {
                txtconfPass.UseSystemPasswordChar = true;
            }
            else
                txtconfPass.UseSystemPasswordChar = false;
        }


        private void doublecheck()
        {
        //    int length =(txtPass.Text.Length);

        //     int upper_count = 0;
        //    int lower_count = 0;
        //    int symbol_count = 0;

        //    for (int i = 0; i < length; i++)
        //    {
        //        if (Char.IsUpper(txtPass.Text[i]))
        //        {
        //            upper_count++;
        //        }
        //        else if (Char.IsLower(txtPass.Text[i]))
        //        {
        //            lower_count++;
        //        }
        //        else if (Char.IsPunctuation(txtPass.Text[i]))
        //        {
        //            symbol_count++;
        //        }
        //    }

        //    if ((upper_count >= 1) && (lower_count >= 2) && (symbol_count >= 1))
        //    {
        //        chechpassintegrity = true;
        //    }
        //    else 
        //    {
        //        chechpassintegrity = false;
        //    }

        }
    }
}

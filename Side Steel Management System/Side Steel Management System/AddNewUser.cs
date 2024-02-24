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
    public partial class AddNewUser : Form
    {
        bool missing = false;
        int Employee_id = 0;
        bool employeExist = false;

        String name = "";
        String usertype = "";
        int pas = 0;


        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();
        DataTable table = new DataTable();

        public AddNewUser()
        {
            InitializeComponent();
        }

   

        void checkEmpty()
        {
            if (txtempname.Text == "" || txtpasswors.Text == "" || txtpassconform.Text == "" || txtusertype.SelectedIndex < 0 || txtsecretQ.SelectedIndex < 0 || txtAns.Text == "")
            {
                missing = true;
                empmss.Text = "Missing Fields.." + '\n' + '\t' + " all field must filled";
            }

            if (txtpasswors.Text.Length < 6)
            {
                missing = true;
                label7.Text = "Password should be at least 6 digit";
            }
             
            else if(txtpasswors.Text != txtpassconform.Text)
            {
                missing = true;
                label9.Text = "Passwords must match";
            }
            else
            {
                try
                {
                    pas = int.Parse(txtpasswors.Text);
                    missing = false;
                }
                catch (Exception ex)
                {
                    missing = true;
                    label7.Text = "Invalid Input";
                }
            }
        }

        void clear()
        {
            txtempname.Text = txtpasswors.Text = txtpassconform.Text = txtAns.Text = "";
            txtusertype.SelectedIndex = -1;
            txtsecretQ.SelectedIndex = -1;
            
        }
        private void AddNewUser_Load(object sender, EventArgs e)
        {
           // txtusertype.Text = "Select User Type";
            //txtsecretQ.Text = "Select Secret Question ";
            fill();
        }

       
        private void checkifexist()
        {
            CommandString = "select * from Employees where First_Name ='"+ txtempname.Text + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                MyDataReader = MyCommand.ExecuteReader();
               if(MyDataReader.Read())
                {
                    employeExist = true;
                    label1.Text = "valid Employee";
                    label1.ForeColor = Color.Green;
                    int quaInMagazineOrdinal = MyDataReader.GetOrdinal("id");
                    Employee_id = MyDataReader.GetInt32(quaInMagazineOrdinal);
                }
                else
                {
                    employeExist = false;
                    label1.Text = "Employee does not Exist";
                    label1.ForeColor = Color.Red;
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();
        }
        private void inserUserType()
        {
            CommandString = "update Employees set User_type ='" + txtusertype.SelectedItem + "' where id = ' " + Employee_id + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();
        }

        private void txtusertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (txtusertype.SelectedIndex < 0)
            //{
            //    txtusertype.ForeColor = Color.Red;
            //}
            //else
            //    txtusertype.ForeColor = Color.FromArgb(54, 69, 79);
        }

   
        private void fill()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                CommandString = "select top 2 * from Users order by id desc";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    Myadapter = new SqlDataAdapter(MyCommand);
                    MydataTable.Clear();
                    Myadapter.Fill(MydataTable);
                    int index = 0;
                    UserdataGrid.Rows.Clear();
                    foreach (DataRow inv in MydataTable.Rows)
                    {

                        getid(inv["id"].ToString());
                        UserdataGrid.Rows.Add();
                        UserdataGrid.Rows[index].Cells[0].Value = imageList1.Images[1];
                        UserdataGrid.Rows[index].Cells[1].Value = inv["id"].ToString();
                        UserdataGrid.Rows[index].Cells[2].Value = name.ToString();
                        UserdataGrid.Rows[index].Cells[3].Value = usertype.ToString();
                        UserdataGrid.Rows[index].Cells[4].Value = inv["status"].ToString();
                        if (inv["permission"].ToString() == "True")
                            UserdataGrid.Rows[index].Cells[5].Value = "Allowed".ToString();
                        else if (inv["permission"].ToString() == "False")
                            UserdataGrid.Rows[index].Cells[5].Value = "Denied".ToString();
                        UserdataGrid.Rows[index].Cells[6].Value = inv["register_date"].ToString();
                        index++;

                    }
                    UserdataGrid.Refresh();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }

                MyConnection.Close();

            }



        }

        private void getid(String id)
        {
            CommandString = "select First_Name,User_type from Employees where id = ' " + id + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                Myadapter = new SqlDataAdapter(MyCommand);
                table.Clear();
                Myadapter.Fill(table);
                name = table.Rows[0]["First_Name"].ToString();
                usertype = table.Rows[0]["User_type"].ToString();




            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

        }

    

      private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            checkEmpty();
            if (!missing && employeExist)
            {
                //CommandString = "insert into MInventory(Item_Name, Quatity, Quality , Size ,Cost ,Providerr) values " +
                //     "( '" + ItemName.Text + "', '" + ItemQuantity.Text + "', '" + ItemQuality.Text + "', '" + ItemSize.Text + "', '" + ItemCost.Text + "' , '" + ItemProv.Text + "' );";


                CommandString = "insert into Users(id, passwordd, secretQ, secretAns) values " +
                    "( '" + Employee_id + "', '" + pas + "', '" + txtsecretQ.SelectedItem + "', '" + txtAns.Text + "' );";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    MyCommand.ExecuteNonQuery();
                    MessageBox.Show("inserted");
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                MyConnection.Close();
                inserUserType();
                fill();
            }
        }

        private void txtempname_TextChanged_1(object sender, EventArgs e)
        {

            if (txtempname.Text == "")
            {

            }
            else
                checkifexist();
        }
    }

}

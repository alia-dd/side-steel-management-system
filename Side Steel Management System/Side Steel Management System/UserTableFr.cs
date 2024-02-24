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
using Microsoft.VisualBasic;

namespace Side_Steel_Management_System
{
    public partial class UserTableFr : Form
    {
        String name = "";
        String usertype = "";

        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();
        DataTable table = new DataTable();
        public UserTableFr()
        {
            InitializeComponent();
        }


        private void UserTableFr_Load(object sender, EventArgs e)
        {
            fill();
        }
        private void fill()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                CommandString = "select * from Users";
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
                        if(inv["permission"].ToString() == "True")
                            UserdataGrid.Rows[index].Cells[5].Value = "Allowed".ToString();
                        else if (inv["permission"].ToString() == "False")
                            UserdataGrid.Rows[index].Cells[5].Value = "Denied".ToString();
                        UserdataGrid.Rows[index].Cells[6].Value = inv["register_date"].ToString();
                        UserdataGrid.Rows[index].Cells[7].Value = imageList1.Images[3];
                        index ++;

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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fill();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                    fill();
                else
                {

                    CommandString = "select * from Users where id LIke '%" + txtsearch.Text + "%'";
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
                            //MessageBox.Show(inv["permission"].ToString());
                            getid(inv["id"].ToString());
                            UserdataGrid.Rows.Add();
                            UserdataGrid.Rows[index].Cells[0].Value = imageList1.Images[1];
                            UserdataGrid.Rows[index].Cells[1].Value = inv["id"].ToString();
                            UserdataGrid.Rows[index].Cells[2].Value = name.ToString();
                            UserdataGrid.Rows[index].Cells[3].Value = usertype.ToString();
                            UserdataGrid.Rows[index].Cells[4].Value = inv["status"].ToString();
                            if (inv["permission"].ToString() == "True")
                                UserdataGrid.Rows[index].Cells[5].Value = "Allowed".ToString();
                            else if (inv["permission"].ToString() == "Falsa")
                                UserdataGrid.Rows[index].Cells[5].Value = "Denied".ToString();
                            UserdataGrid.Rows[index].Cells[6].Value = inv["register_date"].ToString();
                            UserdataGrid.Rows[index].Cells[7].Value = imageList1.Images[3];
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

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       

        private void UserdataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (UserdataGrid.CurrentCell.ColumnIndex.Equals(7))
            {
                int id = Convert.ToInt32(UserdataGrid.SelectedRows[0].Cells[1].Value);
                try
                {

                    DialogResult dr = MessageBox.Show("Are sure you want to delete this User? ", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        deleteItem(id);
                    }
                    fill();

                }
                catch
                {
                    MessageBox.Show("Invalid Input");
                }

            }
        }
        private void deleteItem(int id)
        {
            CommandString = "delete from Users where id = @id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", id);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
                inserUserType(id);
                MessageBox.Show("User Was Deleted ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();

        }
        private void inserUserType(int id)
        {
            CommandString = "update Employees set User_type ='" + null + "' where id = ' " + id + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyCommand.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        private void btnallow_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(UserdataGrid.SelectedRows[0].Cells[1].Value);
            try
            {

                DialogResult dr = MessageBox.Show("Are sure you want to update permission? ", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    allowaccess(id);
                }
                fill();

            }
            catch
            {
                MessageBox.Show("Invalid Input");
            }
        }
        private void allowaccess(int id)
        {
            int pr = 1;
            CommandString = "update Users set permission ='"+pr+ "' where id = @id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", id);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
                MessageBox.Show("Permission Granted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();

        }
        private void denyaccess(int id)
        {
            int pr = 0;
            CommandString = "update Users set permission ='" + pr + "' where id = @id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", id);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
                MessageBox.Show("Permission Refoked");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();

        }

        private void btndeny_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(UserdataGrid.SelectedRows[0].Cells[1].Value);
            try
            {

                DialogResult dr = MessageBox.Show("Are sure you want to update permission? ", " Update Permission", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    denyaccess(id);
                }
                fill();

            }
            catch
            {
                MessageBox.Show("Invalid Input");
            }
        }
    }
}

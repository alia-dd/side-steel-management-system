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
    public partial class InventoryTableFr : Form
    {
        string USname = "";
        string usertp = "";
        int passw = 0;
        bool isUser = false;

        MInventory Minvent = new MInventory();
        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();



        public InventoryTableFr()
        {
            InitializeComponent();

        }


        private void InventoryTableFr_Load(object sender, EventArgs e)
        {

            USname = Program.firstname;
            usertp = Program.usertp;
            user();
            fill();
            //fillgrid();
        }
        void user()
        {
            if (usertp == "User")
            {
                isUser = true;
            }
        }
        private void fillgrid()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                ///InventorydataGrid.DataSource = mydata.MInventories.ToList<MInventory>();
                foreach (var inv in mydata.MInventories.ToList<MInventory>())
                {
                    InventorydataGrid.Rows.Add(new object[]
                    {
                        inv.id,
                        inv.Item_Name,
                        inv.Quatity,
                        inv.Quality,
                        inv.Size,
                        inv.Cost,
                        inv.Total,
                        inv.Providerr,
                        inv.Availabe,
                        inv.Item_location

                    });
                }

            }

        }
        private void clear()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                InventorydataGrid.DataSource = mydata.MInventories.ToList<MInventory>();


            }

        }

        private void fill()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                CommandString = "select * from MInventory";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    Myadapter = new SqlDataAdapter(MyCommand);
                    MydataTable.Clear();
                    Myadapter.Fill(MydataTable);
                    int index = 0;
                    InventorydataGrid.Rows.Clear();
                    foreach (DataRow inv in MydataTable.Rows)
                    {
                        // InventorydataGrid.Rows.Add(inv);

                        InventorydataGrid.Rows.Add();
                        InventorydataGrid.Rows[index].Cells[0].Value = inv["id"];
                        InventorydataGrid.Rows[index].Cells[1].Value = inv["Item_Name"];
                        InventorydataGrid.Rows[index].Cells[2].Value = inv["Quatity"];
                        InventorydataGrid.Rows[index].Cells[3].Value = inv["Quality"];
                        InventorydataGrid.Rows[index].Cells[4].Value = inv["Size"];
                        InventorydataGrid.Rows[index].Cells[5].Value = inv["Cost"];
                        InventorydataGrid.Rows[index].Cells[6].Value = inv["Total"];
                        InventorydataGrid.Rows[index].Cells[7].Value = inv["Providerr"];
                        if (Convert.ToInt32(inv["Quatity"]) <= 0)
                            InventorydataGrid.Rows[index].Cells[8].Value = "Not Availabe";
                        else
                           InventorydataGrid.Rows[index].Cells[8].Value = "Availabe";
                        InventorydataGrid.Rows[index].Cells[9].Value = inv["Item_location"];
                        InventorydataGrid.Rows[index].Cells[10].Value = imageList1.Images[2];
                        InventorydataGrid.Rows[index].Cells[11].Value = imageList1.Images[3];
                        index++;
                    }
                    InventorydataGrid.Refresh();

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }

                MyConnection.Close();

            }

        }

       
        private void InventorydataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (!isUser)
            {
                if (InventorydataGrid.CurrentCell.ColumnIndex.Equals(10))
                {

                    int id = Convert.ToInt32(InventorydataGrid.SelectedRows[0].Cells[0].Value);
                    UpdateInventory myform = new UpdateInventory();
                    myform.updateind = id;
                    myform.ShowDialog();

                }
                if (InventorydataGrid.CurrentCell.ColumnIndex.Equals(11))
                {

                    //MessageBox.Show(InventorydataGrid.SelectedRows[0].Cells[0].Value.ToString());
                    int Itemid = Convert.ToInt32(InventorydataGrid.SelectedRows[0].Cells[0].Value);
                    int pass = 0;
                    try
                    {
                        pass = int.Parse(Interaction.InputBox("Enter your password to delete this data", "Security check", "", -1, -1));

                        getpass();
                        if (pass == passw)
                            deleteItem(Itemid);
                        fill();
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Input");
                    }
                }
            }
        }

        private void getpass()
        {
            int id = 0;
            CommandString = "select id from Employees where First_Name = @name";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@name", USname);
            try
            {
                MyConnection.Open();

                id = int.Parse(MyCommand.ExecuteScalar().ToString());
                
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
                passw = int.Parse(MyCommand.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();
            
        }

        private void deleteItem(int id)
        {
            CommandString = "delete from MInventory where id = @id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", id);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
                MessageBox.Show("An Item Was Removed From The Inventory");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();
           
        }

  
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            fill();
        }

        ///search by item name
        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (bunifuTextBox2.Text == "")
                    fill();
                else
                {
                    CommandString = "select * from MInventory where Item_Name LIke '%"+bunifuTextBox2.Text+"%'";
                    MyCommand = new SqlCommand(CommandString, MyConnection);
                    try
                    {
                        MyConnection.Open();
                        Myadapter = new SqlDataAdapter(MyCommand);
                        MydataTable.Clear();
                        Myadapter.Fill(MydataTable);
                        int index = 0;
                        InventorydataGrid.Rows.Clear();
                        foreach (DataRow inv in MydataTable.Rows)
                        {
                            // InventorydataGrid.Rows.Add(inv);

                            InventorydataGrid.Rows.Add();
                            InventorydataGrid.Rows[index].Cells[0].Value = inv["id"].ToString();
                            InventorydataGrid.Rows[index].Cells[1].Value = inv["Item_Name"].ToString();
                            InventorydataGrid.Rows[index].Cells[2].Value = inv["Quatity"].ToString();
                            InventorydataGrid.Rows[index].Cells[3].Value = inv["Quality"].ToString();
                            InventorydataGrid.Rows[index].Cells[4].Value = inv["Size"].ToString();
                            InventorydataGrid.Rows[index].Cells[5].Value = inv["Cost"].ToString();
                            InventorydataGrid.Rows[index].Cells[6].Value = inv["Total"].ToString();
                            InventorydataGrid.Rows[index].Cells[7].Value = inv["Providerr"].ToString();
                            InventorydataGrid.Rows[index].Cells[8].Value = inv["Availabe"].ToString();
                            InventorydataGrid.Rows[index].Cells[9].Value = inv["Item_location"].ToString();
                            InventorydataGrid.Rows[index].Cells[10].Value = imageList1.Images[2];
                            InventorydataGrid.Rows[index].Cells[11].Value = imageList1.Images[3];
                            index++;
                        }
                        InventorydataGrid.Refresh();

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

        private void btnwerehouse_Click(object sender, EventArgs e)
        {
            CommandString = "select * from MInventory where Quatity <= 0 ";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);
                int index = 0;
                InventorydataGrid.Rows.Clear();
                foreach (DataRow inv in MydataTable.Rows)
                {
                    // InventorydataGrid.Rows.Add(inv);

                    InventorydataGrid.Rows.Add();
                    InventorydataGrid.Rows[index].Cells[0].Value = inv["id"].ToString();
                    InventorydataGrid.Rows[index].Cells[1].Value = inv["Item_Name"].ToString();
                    InventorydataGrid.Rows[index].Cells[2].Value = inv["Quatity"].ToString();
                    InventorydataGrid.Rows[index].Cells[3].Value = inv["Quality"].ToString();
                    InventorydataGrid.Rows[index].Cells[4].Value = inv["Size"].ToString();
                    InventorydataGrid.Rows[index].Cells[5].Value = inv["Cost"].ToString();
                    InventorydataGrid.Rows[index].Cells[6].Value = inv["Total"].ToString();
                    InventorydataGrid.Rows[index].Cells[7].Value = inv["Providerr"].ToString();
                    if (Convert.ToInt32(inv["Quatity"]) <= 0)
                        InventorydataGrid.Rows[index].Cells[8].Value = "Not Availabe";
                    else
                        InventorydataGrid.Rows[index].Cells[8].Value = "Availabe";
                    InventorydataGrid.Rows[index].Cells[9].Value = inv["Item_location"].ToString();
                    InventorydataGrid.Rows[index].Cells[10].Value = imageList1.Images[2];
                    InventorydataGrid.Rows[index].Cells[11].Value = imageList1.Images[3];

                    index++;
                }
                InventorydataGrid.Refresh();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            MyConnection.Close();
        }

        private void btnshop_Click(object sender, EventArgs e)
        {
            CommandString = "select * from MInventory where Quatity > 0 ";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);
                int index = 0;
                InventorydataGrid.Rows.Clear();
                foreach (DataRow inv in MydataTable.Rows)
                {
                    // InventorydataGrid.Rows.Add(inv);

                    InventorydataGrid.Rows.Add();
                    InventorydataGrid.Rows[index].Cells[0].Value = inv["id"].ToString();
                    InventorydataGrid.Rows[index].Cells[1].Value = inv["Item_Name"].ToString();
                    InventorydataGrid.Rows[index].Cells[2].Value = inv["Quatity"].ToString();
                    InventorydataGrid.Rows[index].Cells[3].Value = inv["Quality"].ToString();
                    InventorydataGrid.Rows[index].Cells[4].Value = inv["Size"].ToString();
                    InventorydataGrid.Rows[index].Cells[5].Value = inv["Cost"].ToString();
                    InventorydataGrid.Rows[index].Cells[6].Value = inv["Total"].ToString();
                    InventorydataGrid.Rows[index].Cells[7].Value = inv["Providerr"].ToString();
                    if (Convert.ToInt32(inv["Quatity"]) <= 0)
                        InventorydataGrid.Rows[index].Cells[8].Value = "Not Availabe";
                    else
                        InventorydataGrid.Rows[index].Cells[8].Value = "Availabe";
                    InventorydataGrid.Rows[index].Cells[9].Value = inv["Item_location"].ToString();
                    InventorydataGrid.Rows[index].Cells[10].Value = imageList1.Images[2];
                    InventorydataGrid.Rows[index].Cells[11].Value = imageList1.Images[3];
                    index++;
                }
                InventorydataGrid.Refresh();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            MyConnection.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(InventorydataGrid.SelectedRows[0].Cells[0].Value);

            CommandString = "indev_IVT_REP'" + id + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);

                RPIndvINV RPod = new RPIndvINV();
                RPod.SetDataSource(MydataTable);

                indvInventoryReport OrRP = new indvInventoryReport();
                OrRP.crystalReportViewer1.ReportSource = RPod;
                OrRP.ShowDialog();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();
        }
    
    
    }
    
}
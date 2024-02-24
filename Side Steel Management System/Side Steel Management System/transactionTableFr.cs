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
    public partial class transactionTableFr : Form
    {
        MInventory Minvent = new MInventory();
        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();


        public transactionTableFr()
        {
            InitializeComponent();
        }

        private void transactionTableFr_Load(object sender, EventArgs e)
        {
            
            fill();
        }
       

        private void fill()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                CommandString = "select * from transactions";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                 

                    MyConnection.Open();
                    Myadapter = new SqlDataAdapter(MyCommand);
                    MydataTable.Clear();
                    Myadapter.Fill(MydataTable);
                    int index = 0;
                    transactiondataGrid.Rows.Clear();
                    foreach (DataRow inv in MydataTable.Rows)
                    {
                        // InventorydataGrid.Rows.Add(inv);

                        transactiondataGrid.Rows.Add();
                        transactiondataGrid.Rows[index].Cells[0].Value = inv["id"];
                        transactiondataGrid.Rows[index].Cells[1].Value = inv["transactions"];
                        transactiondataGrid.Rows[index].Cells[2].Value = inv["Item_Name"];
                        transactiondataGrid.Rows[index].Cells[3].Value = inv["Quatity"];
                        transactiondataGrid.Rows[index].Cells[4].Value = inv["Quality"];
                        transactiondataGrid.Rows[index].Cells[5].Value = inv["Size"];
                        transactiondataGrid.Rows[index].Cells[6].Value = inv["Cost"];
                        transactiondataGrid.Rows[index].Cells[7].Value = inv["Total"];
                        transactiondataGrid.Rows[index].Cells[8].Value = inv["Providerr"];
                        index++;
                    }
                    transactiondataGrid.Refresh();

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }

                MyConnection.Close();

            }

        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            fill();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

            string tr = "Purchase";
            CommandString = "select * from transactions where transactions = '" +tr+ "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {


                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);
                int index = 0;
                transactiondataGrid.Rows.Clear();
                foreach (DataRow inv in MydataTable.Rows)
                {
                    // InventorydataGrid.Rows.Add(inv);

                    transactiondataGrid.Rows.Add();
                    transactiondataGrid.Rows[index].Cells[0].Value = inv["id"];
                    transactiondataGrid.Rows[index].Cells[1].Value = inv["transactions"];
                    transactiondataGrid.Rows[index].Cells[2].Value = inv["Item_Name"];
                    transactiondataGrid.Rows[index].Cells[3].Value = inv["Quatity"];
                    transactiondataGrid.Rows[index].Cells[4].Value = inv["Quality"];
                    transactiondataGrid.Rows[index].Cells[5].Value = inv["Size"];
                    transactiondataGrid.Rows[index].Cells[6].Value = inv["Cost"];
                    transactiondataGrid.Rows[index].Cells[7].Value = inv["Total"];
                    transactiondataGrid.Rows[index].Cells[8].Value = inv["Providerr"];
                    index++;
                }
                transactiondataGrid.Refresh();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            MyConnection.Close();
        }

        private void btnOUT_Click(object sender, EventArgs e)
        {
            string tr = "Sell";
            CommandString = "select * from transactions where transactions = '" + tr + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {


                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);
                int index = 0;
                transactiondataGrid.Rows.Clear();
                foreach (DataRow inv in MydataTable.Rows)
                {
                    // InventorydataGrid.Rows.Add(inv);

                    transactiondataGrid.Rows.Add();
                    transactiondataGrid.Rows[index].Cells[0].Value = inv["id"];
                    transactiondataGrid.Rows[index].Cells[1].Value = inv["transactions"];
                    transactiondataGrid.Rows[index].Cells[2].Value = inv["Item_Name"];
                    transactiondataGrid.Rows[index].Cells[3].Value = inv["Quatity"];
                    transactiondataGrid.Rows[index].Cells[4].Value = inv["Quality"];
                    transactiondataGrid.Rows[index].Cells[5].Value = inv["Size"];
                    transactiondataGrid.Rows[index].Cells[6].Value = inv["Cost"];
                    transactiondataGrid.Rows[index].Cells[7].Value = inv["Total"];
                    transactiondataGrid.Rows[index].Cells[8].Value = inv["Providerr"];
                    index++;
                }
                transactiondataGrid.Refresh();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            MyConnection.Close();
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (bunifuTextBox2.Text == "")
                    fill();
                else
                {
                    CommandString = "select * from transactions where Item_Name LIke '%" + bunifuTextBox2.Text + "%'";
                    MyCommand = new SqlCommand(CommandString, MyConnection);
                    try
                    {

                        MyConnection.Open();
                        Myadapter = new SqlDataAdapter(MyCommand);
                        MydataTable.Clear();
                        Myadapter.Fill(MydataTable);
                        int index = 0;
                        transactiondataGrid.Rows.Clear();
                        foreach (DataRow inv in MydataTable.Rows)
                        {
                            // InventorydataGrid.Rows.Add(inv);

                            transactiondataGrid.Rows.Add();
                            transactiondataGrid.Rows[index].Cells[0].Value = inv["id"];
                            transactiondataGrid.Rows[index].Cells[1].Value = inv["transactions"];
                            transactiondataGrid.Rows[index].Cells[2].Value = inv["Item_Name"];
                            transactiondataGrid.Rows[index].Cells[3].Value = inv["Quatity"];
                            transactiondataGrid.Rows[index].Cells[4].Value = inv["Quality"];
                            transactiondataGrid.Rows[index].Cells[5].Value = inv["Size"];
                            transactiondataGrid.Rows[index].Cells[6].Value = inv["Cost"];
                            transactiondataGrid.Rows[index].Cells[7].Value = inv["Total"];
                            transactiondataGrid.Rows[index].Cells[8].Value = inv["Providerr"];
                            index++;
                        }
                        transactiondataGrid.Refresh();


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
    }
}

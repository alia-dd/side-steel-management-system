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
    public partial class OrderDetailTableFr : Form
    {
        string usertp = "";
        bool isUser = false;

        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();

        public OrderDetailTableFr()
        {
            InitializeComponent();
        }

        private void OrderDetailTableFr_Load(object sender, EventArgs e)
        {
            usertp = Program.usertp;
            user();
            fillgrid();
        }
        void user()
        {
            if (usertp == "User")
            {
                isUser = true;
            }
        }
        public void fillgrid()
        {
            CommandString = "select * from orderr where customer_Name IS NOT NULL";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);
                int index = 0;
                OrderdetaildataGrid.Rows.Clear();

                foreach (DataRow inv in MydataTable.Rows)
                {

                    OrderdetaildataGrid.Rows.Add();
                    OrderdetaildataGrid.Rows[index].Cells[0].Value = inv["orde_id"];
                    //OrderdetaildataGrid.Rows[index].Cells[1].Value = inv["customer_Name"];
                    OrderdetaildataGrid.Rows[index].Cells[1].Value = inv["Item_Name"];
                    OrderdetaildataGrid.Rows[index].Cells[2].Value = inv["Quatity"];
                    OrderdetaildataGrid.Rows[index].Cells[3].Value = inv["Quality"];
                    OrderdetaildataGrid.Rows[index].Cells[4].Value = inv["Size"];
                    OrderdetaildataGrid.Rows[index].Cells[5].Value = inv["Providerr"];
                    OrderdetaildataGrid.Rows[index].Cells[6].Value = inv["wholesale_retail"];
                    OrderdetaildataGrid.Rows[index].Cells[7].Value = inv["Cost"];
                    OrderdetaildataGrid.Rows[index].Cells[8].Value = inv["prize"];
                    OrderdetaildataGrid.Rows[index].Cells[9].Value = inv["discount"];
                    OrderdetaildataGrid.Rows[index].Cells[10].Value = inv["Total"];
                    OrderdetaildataGrid.Rows[index].Cells[11].Value = inv["payment_type"];
                    OrderdetaildataGrid.Rows[index].Cells[12].Value = inv["id"];
                    OrderdetaildataGrid.Rows[index].Cells[13].Value = imageList1.Images[3];

                    index++;
                }
                OrderdetaildataGrid.Refresh();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            MyConnection.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillgrid();
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (bunifuTextBox2.Text == "")
                    fillgrid();
                else
                {
                    CommandString = "select * from orderr where Item_Name LIke '%" + bunifuTextBox2.Text + "%' or orde_id LIke '" + bunifuTextBox2.Text+ "%'";
                    MyCommand = new SqlCommand(CommandString, MyConnection);
                    try
                    {
                        MyConnection.Open();
                        Myadapter = new SqlDataAdapter(MyCommand);
                        MydataTable.Clear();
                        Myadapter.Fill(MydataTable);
                        int index = 0;
                        OrderdetaildataGrid.Rows.Clear();

                        foreach (DataRow inv in MydataTable.Rows)
                        {

                            OrderdetaildataGrid.Rows.Add();
                            OrderdetaildataGrid.Rows[index].Cells[0].Value = inv["orde_id"];
                            OrderdetaildataGrid.Rows[index].Cells[1].Value = inv["customer_Name"];
                            OrderdetaildataGrid.Rows[index].Cells[2].Value = inv["Item_Name"];
                            OrderdetaildataGrid.Rows[index].Cells[3].Value = inv["Quatity"];
                            OrderdetaildataGrid.Rows[index].Cells[4].Value = inv["Quality"];
                            OrderdetaildataGrid.Rows[index].Cells[5].Value = inv["Size"];
                            OrderdetaildataGrid.Rows[index].Cells[6].Value = inv["Providerr"];
                            OrderdetaildataGrid.Rows[index].Cells[7].Value = inv["wholesale_retail"];
                            OrderdetaildataGrid.Rows[index].Cells[8].Value = inv["Cost"];
                            OrderdetaildataGrid.Rows[index].Cells[9].Value = inv["prize"];
                            OrderdetaildataGrid.Rows[index].Cells[10].Value = inv["discount"];
                            OrderdetaildataGrid.Rows[index].Cells[11].Value = inv["Total"];
                            OrderdetaildataGrid.Rows[index].Cells[12].Value = inv["payment_type"];
                            OrderdetaildataGrid.Rows[index].Cells[13].Value = imageList1.Images[3];

                            index++;
                        }
                        OrderdetaildataGrid.Refresh();


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

        private void OrderdetaildataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isUser)
            {
                if (OrderdetaildataGrid.CurrentCell.ColumnIndex.Equals(12))
                {

                    int id = Convert.ToInt32(OrderdetaildataGrid.SelectedRows[0].Cells[0].Value);
                    UpdateEmployeeFr myform = new UpdateEmployeeFr();
                    myform.updateind = id;
                    myform.ShowDialog();

                }
                if (OrderdetaildataGrid.CurrentCell.ColumnIndex.Equals(12))
                {
                    int id = Convert.ToInt32(OrderdetaildataGrid.SelectedRows[0].Cells[11].Value);
                    try
                    {

                        DialogResult dr = MessageBox.Show("Are sure you want to Delete this Order? ", "Delete Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            deleteItem(id);
                        }
                        fillgrid();

                    }
                    catch
                    {
                        MessageBox.Show("Invalid Input");
                    }

                }
            }
        }
  
        private void deleteItem(int id)
        {
            MessageBox.Show(id.ToString());
            CommandString = "delete from orderr where id = @id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", id);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
                MessageBox.Show("Order Removed ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();

        }

        private void btnrepot_Click(object sender, EventArgs e)
        {
            int ordid = Convert.ToInt32(OrderdetaildataGrid.SelectedRows[0].Cells[0].Value);

            CommandString = "OrderReceipt'"+ordid+"'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);

                RPOrderReceipt RPod = new RPOrderReceipt();
                RPod.SetDataSource(MydataTable);

                OrderRecieiptReport OrRP = new OrderRecieiptReport();
                OrRP.crystalReportViewer1.ReportSource = RPod;
                OrRP.ShowDialog();
                

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();
         }
    }
}

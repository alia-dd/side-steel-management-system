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
    public partial class PlaceOrderFr : Form
    {
        int order_id = 1;
        public bool completeORD = false;

        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();
        public PlaceOrderFr()
        {
            InitializeComponent();
        }


        public void AddtoCart(string item_name, int quantity, string quality, string sale, int size, int cost, int discount, int total, string provider, string pay)
        {
            //try { 

            //            orderdataGrid.Rows.Add();
            //            orderdataGrid.Rows[0].Cells[0].Value = order_id.ToString();
            //            orderdataGrid.Rows[0].Cells[1].Value = item_name;
            //            orderdataGrid.Rows[0].Cells[2].Value = quantity.ToString();
            //            orderdataGrid.Rows[0].Cells[3].Value = quality;
            //            orderdataGrid.Rows[0].Cells[4].Value = size.ToString();
            //            orderdataGrid.Rows[0].Cells[5].Value = provider;
            //            orderdataGrid.Rows[0].Cells[6].Value = sale;
            //            orderdataGrid.Rows[0].Cells[7].Value = cost.ToString();
            //            orderdataGrid.Rows[0].Cells[8].Value = discount.ToString();
            //            orderdataGrid.Rows[0].Cells[9].Value = total.ToString();
            //            orderdataGrid.Rows[0].Cells[10].Value = pay;


            //        orderdataGrid.Refresh();

            //}
            //catch (Exception E)
            //{
            //        MessageBox.Show(E.Message);
            //}

        }

        private void getord_id()
        {
            CommandString = "select * from orderr where customer_Name IS NULL";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);

            try
            {
                MyConnection.Open();
                if (MyCommand.ExecuteScalar() == null)
                    ord_id();
                else if (MyCommand.ExecuteScalar() != null)
                    meh();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();

        }
        private void meh()
        {
            CommandString = "select max(orde_id) from orderr where customer_Name IS NULL";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);
            int id;
            try
            {
                order_id = Convert.ToInt32(MyCommand.ExecuteScalar());

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void ord_id()
        {
            CommandString = "select max(orde_id) from orderr";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);

            try
            {
                order_id = (Convert.ToInt32(MyCommand.ExecuteScalar())) + 1;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void btnaddTocart_Click(object sender, EventArgs e)
        {
            orderFr myform = new orderFr();
            myform.orderId = order_id;
            myform.ShowDialog();

        }

        private void btnplaceOrder_Click(object sender, EventArgs e)
        {
            RegisterCustomerFr myform = new RegisterCustomerFr();
            myform.order_id = order_id;
            myform.ShowDialog();
            fillgrid();

        }

        private void btncancelOrder_Click(object sender, EventArgs e)
        {
            orderdataGrid.Rows.Clear();
            DialogResult dr = MessageBox.Show("Are sure you want to cancel the order? ", "cancel order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                cancaleorder();
            }
        }

        public void fillgrid()
        {
            CommandString = "select * from orderr where orde_id = '" + order_id + "'order by Item_Name";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);
                int index = 0;
                orderdataGrid.Rows.Clear();

                foreach (DataRow inv in MydataTable.Rows)
                {

                    orderdataGrid.Rows.Add();
                    orderdataGrid.Rows[index].Cells[0].Value = inv["orde_id"];
                    orderdataGrid.Rows[index].Cells[1].Value = inv["Item_Name"];
                    orderdataGrid.Rows[index].Cells[2].Value = inv["Quatity"];
                    orderdataGrid.Rows[index].Cells[3].Value = inv["Quality"];
                    orderdataGrid.Rows[index].Cells[4].Value = inv["Size"];
                    orderdataGrid.Rows[index].Cells[5].Value = inv["Providerr"];
                    orderdataGrid.Rows[index].Cells[6].Value = inv["wholesale_retail"];
                    orderdataGrid.Rows[index].Cells[7].Value = inv["prize"];
                    orderdataGrid.Rows[index].Cells[8].Value = inv["discount"];
                    orderdataGrid.Rows[index].Cells[9].Value = inv["Total"];
                    orderdataGrid.Rows[index].Cells[10].Value = inv["payment_type"];
                    orderdataGrid.Rows[index].Cells[11].Value = inv["id"];

                    index++;
                }
                orderdataGrid.Refresh();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            MyConnection.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            getord_id();
            fillgrid();
        }

        private void PlaceOrderFr_Load(object sender, EventArgs e)
        {
            getord_id();
            MessageBox.Show(order_id.ToString());
            fillgrid();
        }

        
        private void cancaleorder()
        {
            CommandString = "delete from orderr where orde_id = @id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", order_id);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
                MessageBox.Show("order canceled......");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();
        }

        private void orderdataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (orderdataGrid.CurrentCell.ColumnIndex.Equals(12))
            {
                int id = Convert.ToInt32(orderdataGrid.SelectedRows[0].Cells[11].Value);
                MessageBox.Show(id.ToString());
                try
                {
                    string itemID = (orderdataGrid.SelectedRows[0].Cells[1].Value).ToString();
                    int quantity = Convert.ToInt32(orderdataGrid.SelectedRows[0].Cells[2].Value);
                    int size = Convert.ToInt32(orderdataGrid.SelectedRows[0].Cells[4].Value);
                    int cost = Convert.ToInt32(orderdataGrid.SelectedRows[0].Cells[7].Value) - 20;
                    string provider = (orderdataGrid.SelectedRows[0].Cells[5].Value).ToString();
                    MessageBox.Show(quantity.ToString());
                    
                    DialogResult dr = MessageBox.Show("Are sure you want to cancel the order? ", "cancel order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        UPDATEItemQuantity(itemID, quantity, size, cost, provider);
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

        private void UPDATEItemQuantity(string itemID,int quantity, int size, int cost, string provider )
        {
            
           

            int st = 0;
            CommandString = "select Quatity from MInventory where Item_Name = '" + itemID + "' and Size =  '" + size + "' and  Cost =  '" + cost + "' and  Providerr =  '" + provider + "'"; ;
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                st = Convert.ToInt32(MyCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();
            int plus = st + quantity;
            CommandString = "update MInventory set Quatity ='" + plus + "'where Item_Name = '" + itemID + "' and Size =  '" + size + "' and  Cost =  '" + cost + "' and  Providerr =  '" + provider + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);

            int countinOrder = 0;
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
                MessageBox.Show("Item Was Removed From The Order List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();

        }
    }
}

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
    public partial class DashBfr : Form
    {
        int countEmployee = 0;
        int countInventory = 0;
        int countOrder = 0;
        int countRef = 0;


        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();

        public DashBfr()
        {
            InitializeComponent();
        }

        private void DashBfr_Load(object sender, EventArgs e)
        {
            fillbtcount();
            fillShort();
            chechincompleteORD();

        }
        private void fillbtcount()
        {
            countEmployees();
            
            Cemployee.Text = countEmployee.ToString() + " Employees";
            countInventories();
            Cinventory.Text = countInventory.ToString() + " Items";
            countOrders();
            Corder.Text = countOrder.ToString() + " Total Orders";
            countRefs();
            CRef.Text = ($"{countRef:C}") + " Total Revenue";

        }
        private void countEmployees()
        {

            CommandString = "select count(id) from Employees";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);

            int ind = 1;
            try
            {
                MyConnection.Open();
                countEmployee = Convert.ToInt32(MyCommand.ExecuteScalar());
                

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();
        }
        private void countInventories()
        {

            CommandString = "select count(id) from MInventory";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);

            int ind = 1;
            try
            {
                MyConnection.Open();
                countInventory = Convert.ToInt32(MyCommand.ExecuteScalar());


            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();
        }
        private void countOrders()
        {
           
            CommandString = "select count(distinct orde_id) from orderr where customer_Name IS NOT NULL";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);

            int ind = 1;
            try
            {
                MyConnection.Open();
                countOrder = Convert.ToInt32(MyCommand.ExecuteScalar());

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();
        }
        private void countRefs()
        {

            CommandString = "select sum(Total) from orderr where customer_Name IS NOT NULL";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);

            int ind = 1;
            try
            {
                MyConnection.Open();
                countRef = Convert.ToInt32(MyCommand.ExecuteScalar());

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();
        }
        private void fillShort()
        {
            CommandString = "select * from MInventory where Quatity <= 10";
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
                    if (Convert.ToInt32(inv["Quatity"]) <= 0)
                        InventorydataGrid.Rows[index].Cells[3].Value = "Not Availabe";
                    else
                        InventorydataGrid.Rows[index].Cells[3].Value = "Availabe";
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
        private void chechincompleteORD()
        {
            CommandString = "select * from orderr where customer_Name IS NULL";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);

            try
            {
                MyConnection.Open();
                if (MyCommand.ExecuteScalar() == null)
                    waitingOrder.Text = "---";
                else if (MyCommand.ExecuteScalar() != null)
                    waitingOrder.Text = "1 Incomplete Order";

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();

        }

        private void waitingOrder_Click(object sender, EventArgs e)
        {

        }

        private void Cemployee_Click(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class RegisterCustomerFr : Form
    {
        
        bool missing = false;
        public int order_id;
        string Cname = "";

        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();

        public RegisterCustomerFr()
        {
            InitializeComponent();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            insertINTOcustomers();
            this.Close();

        }
      
        private void insertINTOcustomers()
        {
            checkEmpty();
            if (!missing)
            {

                CommandString = "insert into customer(First_Name, Middle_Name, Last_Name, Phone, Addresss, orde_id) values " +
                    "('" + txtfirstname.Text + "', '" + txtmiddlename.Text + "', '" + txtlastname.Text + "', '" + txtphone.Text + "', '" + txtaddress.Text + "', '" + order_id+ "');";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    MyCommand.ExecuteNonQuery();
                    Cname = txtfirstname.Text + " " + txtmiddlename.Text + " " + txtlastname.Text;
                    insertINTOOrder();
                    Receipt();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                MyConnection.Close();
                this.Close();
            }
        }
        private void insertINTOOrder()
        {
            checkEmpty();
            if (!missing)
            {

                CommandString = "update orderr set customer_Name ='" + Cname + "' where orde_id = ' " + order_id + "'";

                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyCommand.ExecuteNonQuery();
                    insertINTOTransaction();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
        }

        private void insertINTOTransaction()
        {
            string tr = "Sell";
            CommandString = "insert into transactions( Item_Name, Quatity, Quality, Size, Cost, Total, Providerr) select Item_Name, Quatity, Quality, Size, Cost, Total, Providerr from orderr where orde_id = '"+order_id+"'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyCommand.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            CommandString = "update transactions set transactions = '"+tr+ "' where transactions is null";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyCommand.ExecuteNonQuery();
                MessageBox.Show("Order Completed...");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            
        }


        private void Receipt()
        {
            CommandString = "OrderReceipt'" + order_id + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);

                RPOrderReceipt RPod = new RPOrderReceipt();
                RPod.SetDataSource(MydataTable);

                OrderRecieiptReport OrRP = new OrderRecieiptReport();
                OrRP.crystalReportViewer1.ReportSource = RPod;
                OrRP.ShowDialog();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void checkEmpty()
        {
            if (txtfirstname.Text == "" || txtmiddlename.Text == "" || txtlastname.Text == "" || txtphone.Text == "" || txtaddress.Text == "")
            {
                missing = true;
                empmss.Text = "Missing Fields.." + '\n' + '\t' + " all fields must filled";
            }
            else if (txtphone.Text.Length < 7)
            {
                missing = true;
                empmss.Text = "The phone number is short";
            }
            else if (txtphone.Text.Length > 10)
            {
                missing = true;
                empmss.Text = "The phone number is too long";
            }
            else
                missing = false;
        }

       
        private void RegisterCustomerFr_Load(object sender, EventArgs e)
        {
            txtfirstname.Focus();
        }
    }
}

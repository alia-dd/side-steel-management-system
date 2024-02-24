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
    public partial class orderFr : Form
    {
        int count = 0;

        bool nodisc = false;

        bool missing = false;

        public int orderId = 0;
        string itemID = "";
        int quantity = 0; 
        string quality = "";
        string sale = "";
        int size = 0;
        int cost = 0;
        float price = 0;
        double discount;
        int disct;
        string provider = "";
        string paymethod = "";


        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();

        public orderFr()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            checkempy();
            if (!missing)
            {
                calculateDiscount();
                addOrder();
                UPDATEItemQuantity();
                PlaceOrderFr myform = new PlaceOrderFr();
                myform.fillgrid();
            }
        }

        private void fill()
        {
            ItemDiscount.Enabled = true;
            Sale.Text = "Select Sale type";
            ItemDiscount.Text = "Select Discount";
            pay.Text = "Select Payment Method";
            //fillItemQuantity();
            fillItemQuality();
            fillItemSize();
            fillItemProv();
            fillCost();
        }
        private void fillItemName()
        {

            CommandString = "select id, Item_Name from MInventory order by Item_Name";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);
            DataSet MydataSet = new DataSet();

            try
            {
                MyConnection.Open();
                MydataSet.Clear();
                Myadapter.Fill(MydataSet);
                ItemName.DataSource = MydataSet.Tables[0].DefaultView.ToTable(true, "Item_Name");
                ItemName.DisplayMember = "Item_Name";
                ItemName.Text = "Select an ITEM";
               
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();

        }
        private void fillItemQuantity()
        {

            CommandString = "select SUM(Quatity) from MInventory where Item_Name =  '" + itemID + "'and Size =  '" + size + "' and  Cost =  '" + cost + "' and  Providerr =  '" + provider + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);


            int ind = 1;
            try
            {
                MyConnection.Open();
                if (MyCommand.ExecuteScalar().ToString() != "")
                {
                    count = Convert.ToInt32(MyCommand.ExecuteScalar());

                    ItemQuantity.SelectedIndex = -1;
                    ItemQuantity.Items.Clear();
                    while (ind <= count)
                    {
                        ItemQuantity.Items.Add(ind);
                        ind++;

                    }
                }
                else
                {
                    ItemQuantity.SelectedIndex = -1;
                    ItemQuantity.Items.Clear();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();

        }



        private void UPDATEItemQuantity()
        {
            int mins = count - quantity;

            CommandString = "update MInventory set Quatity ='" + mins + "'where Item_Name = '" + itemID + "' and Size =  '" + size + "' and  Cost =  '" + cost + "' and  Providerr =  '" + provider + "'";
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

        private void fillItemQuality()
        {
            CommandString = "select id, Quality from MInventory where Item_Name =  '" + itemID + "'order by Quality";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);
            DataSet MydataSet = new DataSet();
          
            try
            {
                MyConnection.Open();
                MydataSet.Clear();
                Myadapter.Fill(MydataSet);
                MyCommand.ExecuteNonQuery();
                ItemQuality.DataSource = MydataSet.Tables[0].DefaultView.ToTable(true, "Quality"); 
                ItemQuality.DisplayMember = "Quality";
                ItemQuality.Text = "Select ITEM Quality";

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();

        }
        private void fillItemSize()
        {
            CommandString = "select id, Size from MInventory where Item_Name =  '" + itemID + "' order by Size";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);
            DataSet MydataSet = new DataSet();
  
            try
            {
                MyConnection.Open();
                MydataSet.Clear();
                Myadapter.Fill(MydataSet);
                MyCommand.ExecuteNonQuery();
                ItemSize.DataSource = MydataSet.Tables[0].DefaultView.ToTable(true, "Size");
                ItemSize.DisplayMember = "Size";
                ItemSize.Text = "Select ITEM Size";

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();

        }
        private void fillItemProv()
        {
            CommandString = "select id, Providerr from MInventory  where Item_Name =  '" + itemID + "' order by Providerr";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);
            DataSet MydataSet = new DataSet();
         
            try
            {
                MyConnection.Open();
                MydataSet.Clear();
                Myadapter.Fill(MydataSet);
                MyCommand.ExecuteNonQuery();
                ItemProv.DataSource = MydataSet.Tables[0].DefaultView.ToTable(true, "Providerr");
                ItemProv.DisplayMember = "Providerr";
                ItemProv.Text = "Select an ITEM";

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();
        }
        private void fillCost()
        {
            CommandString = "select id, Cost from MInventory where Item_Name =  '" + itemID + "' order by Cost";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            Myadapter = new SqlDataAdapter(MyCommand);
            DataSet MydataSet = new DataSet();
          
            try
            {
                MyConnection.Open();
                MydataSet.Clear();
                Myadapter.Fill(MydataSet);
                MyCommand.ExecuteNonQuery();
                Item_Cost.DataSource = MydataSet.Tables[0].DefaultView.ToTable(true, "Cost");
                Item_Cost.DisplayMember = "Cost";
                Item_Cost.Text = "Select an ITEM";

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            MyConnection.Close();

        }

        private void Sale_SelectedIndexChanged(object sender, EventArgs e)
        {
            sale = Sale.SelectedItem.ToString();
            if (Sale.SelectedItem == "Wholesale")
            {
                ItemDiscount.Enabled = true;
                nodisc = false;
            }

            else if (Sale.SelectedItem == "Retail")
            {
                ItemDiscount.SelectedIndex = -1;
                ItemDiscount.Enabled = false;
                nodisc = true;
            }
        }

        private void orderFr_Load(object sender, EventArgs e)
        {
            txterror.Visible = false;
            fillItemName();
        }

        private void ItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dv = (DataRowView)ItemName.SelectedItem;
            itemID = (string)dv.Row["Item_Name"];
            if (MyConnection.State == ConnectionState.Closed)
                fill();
        }

        private void ItemQuantity_SelectedIndexChanged(object sender, EventArgs e)
        {
            quantity = Convert.ToInt32(ItemQuantity.SelectedItem);
        }

        private void ItemQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dv = (DataRowView)ItemQuality.SelectedItem;
            quality = (string)dv.Row["Quality"];
            if (MyConnection.State == ConnectionState.Closed)
                fillItemQuantity();
        }

        private void ItemSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dv = (DataRowView)ItemSize.SelectedItem;
            size = (int)dv.Row["Size"];
            if (MyConnection.State == ConnectionState.Closed)
                fillItemQuantity();
        }

        private void ItemDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nodisc)
                disct = 0;
            else if (!nodisc)
                disct = Convert.ToInt32(ItemDiscount.SelectedItem);
        }

        private void ItemProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dv = (DataRowView)ItemProv.SelectedItem;
            provider = (string)dv.Row["Providerr"];
            if (MyConnection.State == ConnectionState.Closed)
                fillItemQuantity();
        }

        private void Item_Cost_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataRowView dv = (DataRowView)Item_Cost.SelectedItem;
            cost = ((int)dv.Row["Cost"]);
            if (MyConnection.State == ConnectionState.Closed)
                fillItemQuantity();
            price = cost + 20;
        }

        private void pay_SelectedIndexChanged(object sender, EventArgs e)
        {
            paymethod = pay.SelectedItem.ToString();
        }

        private void checkempy()

        {
            if(nodisc)
            {
                if (ItemName.SelectedIndex < 0 || ItemQuantity.SelectedIndex < 0 || ItemQuality.SelectedIndex < 0 || Sale.SelectedIndex < 0 || ItemSize.SelectedIndex < 0 || ItemProv.SelectedIndex < 0 || Item_Cost.SelectedIndex < 0 || pay.SelectedIndex < 0)
                {
                    missing = true;
                    txterror.Visible = true;
                }
                else
                {
                    missing = false;
                    txterror.Visible = false;
                }

            }
            if (!nodisc)
            {
                if (ItemName.SelectedIndex < 0 || ItemQuantity.SelectedIndex < 0 || ItemQuality.SelectedIndex < 0 || Sale.SelectedIndex < 0 || ItemSize.SelectedIndex < 0 || ItemDiscount.SelectedIndex < 0 || ItemProv.SelectedIndex < 0 || Item_Cost.SelectedIndex < 0 || pay.SelectedIndex < 0)
                {
                    missing = true;
                    txterror.Visible = true;
                }
                else
                {
                    missing = false;
                    txterror.Visible = false;
                }
            }

        }       

        private void calculateDiscount()
        {
            if (disct > 0)
                discount = disct / 100.0;
            else if(disct == 0)
                discount = 1;

        }

        private void addOrder()
        {

            CommandString = "insert into orderr(Item_Name, Quatity, Quality , Size, wholesale_retail, Cost, prize, discount, Providerr, payment_type, orde_id) values " +
                    "( '" + itemID + "', '" + quantity + "', '" + quality + "', '" + size + "', '" + sale + "' , '" + cost + "', '" + price + "', '" + discount + "', '" + provider + "' , '" + paymethod + "', '" + orderId + "' );";

                MyCommand = new SqlCommand(CommandString, MyConnection);
                    try
                    {
                        MyConnection.Open();
                        MyCommand.ExecuteNonQuery();
                        MessageBox.Show(ItemName.Text + " was added to the cart........");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                    MyConnection.Close();

        }
    }
}

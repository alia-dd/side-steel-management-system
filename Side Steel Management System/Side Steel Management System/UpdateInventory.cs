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

    public partial class UpdateInventory : Form
    {
        bool missing = false;
        public int updateind = 0;
        int qnt = 0;
        int cost = 0;
        int size = 0;

        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();
        public UpdateInventory()
        {
            InitializeComponent();
        }

        private void btnCleare_Click(object sender, EventArgs e)
        {
            ItemName.Text = ItemQuantity.Text = ItemSize.Text = ItemCost.Text = ItemProv.Text = "";
            ItemQuality.SelectedIndex = -1;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            checkEmpty();
            try
            {
                qnt = Convert.ToInt32(ItemQuantity.Text);
                cost = Convert.ToInt32(ItemCost.Text);
                size = Convert.ToInt32(ItemSize.Text);
                if (qnt < 0 || cost <= 0 || size <= 0)
                {
                    missing = true;
                    empmss.Text = "Invalid Input";
                }
            }
            catch (Exception ex)
            {
                missing = true;
                empmss.Text = "Invalid Input";
            }

            updateitem();
        }



        void checkEmpty()
        {
             if (ItemName.Text == "" || ItemQuantity.Text == "" || ItemQuality.SelectedIndex < 0 || ItemSize.Text == "" || ItemCost.Text == "" || ItemProv.Text == "")
                {
                    missing = true;
                    empmss.Text = "Missing Fields.." + '\n' + '\t' + " all field must filled";
                }
                else
                    missing = false;
            
        }

        private void UpdateInventory_Load(object sender, EventArgs e)
        {
            fillgrid();
            filltxt();
        }
        private void fillgrid()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                CommandString = "select * from MInventory where id = '" + updateind + "'";
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
                        InventorydataGrid.Rows[index].Cells[8].Value = inv["Availabe"];
                        InventorydataGrid.Rows[index].Cells[9].Value = inv["Item_location"];
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

        private void filltxt()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                CommandString = "select Item_Name, Quatity, Quality , Size ,Cost ,Providerr from MInventory where id ='" + updateind + "'" ;
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    Myadapter = new SqlDataAdapter(MyCommand);
                    MydataTable.Clear();
                    Myadapter.Fill(MydataTable);
                    ItemName.Text = MydataTable.Rows[0]["Item_Name"].ToString();
                    ItemQuantity.Text = MydataTable.Rows[0]["Quatity"].ToString();
                    ItemQuality.Text = MydataTable.Rows[0]["Quality"].ToString();
                    ItemSize.Text = MydataTable.Rows[0]["Size"].ToString();
                    ItemCost.Text = MydataTable.Rows[0]["Cost"].ToString();
                    ItemProv.Text = MydataTable.Rows[0]["Providerr"].ToString();

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }

                MyConnection.Close();

            }

        }


        private void updateitem()
        {
           
            if (!missing)
            {
                CommandString = "update MInventory set Item_Name ='" + ItemName.Text + "', Quatity ='" + qnt +
                    "' ,Quality = '" + ItemQuality.SelectedIndex + "',Size ='" +size + "',Cost ='" + cost + "',Providerr ='" +
                    ItemProv.Text + "'where id = '" + updateind + "'";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    MyCommand.ExecuteNonQuery();
                    MessageBox.Show(ItemName.Text.ToString() + " " + "was updated.......");
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                MyConnection.Close();
            }
            fillgrid();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

        }

        private void ItemQuality_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class AddNewItemFr : Form
    {
        MInventory Minvent = new MInventory();

        bool exist = false;
        bool missing = false;
        int PitemID = 0;

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
        public AddNewItemFr()
        {
            InitializeComponent();
            //IsSubItem.Checked = false;
           
        }

      
        private void isSub()
        {
            //if(IsSubItem.Checked == false)
            //{
            //    selectPItem.BackColor = Color.FromArgb(192, 194, 201);
            //    selectPItem.Enabled = false;
                
            //}
            //else
            //{
            //    selectPItem.BackColor = Color.FromArgb(54, 69, 79);
            //    selectPItem.Enabled = true;
               
            //}

        }

        private void IsSubItem_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            isSub();
        }

        void checkEmpty()
        {
            //if(IsSubItem.Checked == true)
            //{
            //    if (ItemName.Text == "" || ItemQuantity.Text == "" || ItemQuality.Text == "" || ItemSize.Text == "" || ItemCost.Text == "" || ItemProv.Text == "" || selectPItem.SelectedIndex <= 0)
            //    {
            //        missing = true;
            //        // empmss.Text = "Missing Fields.." + '\n' + '\t' + " all field must filled";
            //    }
            //    else
            //        missing = false;
            //}
            //else
            //{
                if (ItemName.Text == "" || ItemQuantity.Text == "" || ItemQuality.SelectedIndex < 0 || ItemSize.Text == "" || ItemCost.Text == "" || ItemProv.Text == "")
                {
                    missing = true;
                    empmss.Text = "Missing Fields.." + '\n' + '\t' + " all field must filled";
                }
                else
                    missing = false;
            //}
        }
        
        private void AddNewItemFr_Load(object sender, EventArgs e)
        {
            //fillcombobox();
            fillgrid();
           
        }
        private void getid()
        {
            //CommandString = "select id from MInventory where Item_Name = ' "+ selectPItem.SelectedItem+"'";
            //MyCommand = new SqlCommand(CommandString, MyConnection);
            //try
            //{
            //    MyConnection.Open();
            //    MyDataReader = MyCommand.ExecuteReader();
            //    int quaInMagazineOrdinal = MyDataReader.GetOrdinal("id");
            //    PitemID = MyDataReader.GetInt32(quaInMagazineOrdinal);
            //}
            //catch (Exception E)
            //{
            //    MessageBox.Show(E.Message);
            //}
            //MyConnection.Close();
        }

        private void addPitem()
        {
            if (!missing)
            {
                int id = 0;
                CommandString = "select id from MInventory where Item_Name = '" + ItemName.Text + "' and Quality =  '" + ItemQuality.SelectedItem + "' and Size =  '" + ItemSize.Text + "' and  Cost =  '" + ItemCost.Text + "' and  Providerr =  '" + ItemProv.Text + "'";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    if (MyCommand.ExecuteScalar() == null)
                        exist = false;

                    else
                    {
                        exist = true;
                        id = Convert.ToInt32(MyCommand.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MyConnection.Close();

                if (exist)
                {
                    int st = 0;

                    CommandString = "select Quatity  from MInventory where id = @id";
                    MyCommand = new SqlCommand(CommandString, MyConnection);
                    MyCommand.Parameters.AddWithValue("@id", id);
                    try
                    {
                        MyConnection.Open();
                        st = int.Parse(MyCommand.ExecuteScalar().ToString());

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    MyConnection.Close();
                    int plus = st + qnt;
                    CommandString = "update MInventory set Quatity ='" + plus + "' where id = @id";
                    MyCommand = new SqlCommand(CommandString, MyConnection);
                    MyCommand.Parameters.AddWithValue("@id", id);
                    Myadapter = new SqlDataAdapter(MyCommand);

                    int countinOrder = 0;
                    try
                    {
                        MyConnection.Open();
                        MyCommand.ExecuteNonQuery();
                        insertINTOTransaction();
                        MessageBox.Show(ItemName.Text + "was added to the inventory >>>>>>>>>");

                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }
                    MyConnection.Close();
                }
                else if (!exist)
                {
                    CommandString = "insert into MInventory(Item_Name, Quatity, Quality , Size ,Cost ,Providerr) values " +
                            "( '" + ItemName.Text + "', '" + qnt + "', '" + ItemQuality.SelectedItem + "', '" + size + "', '" + cost + "' , '" + ItemProv.Text + "' );";
                    MyCommand = new SqlCommand(CommandString, MyConnection);
                    try
                    {
                        MyConnection.Open();
                        MyCommand.ExecuteNonQuery();
                        insertINTOTransaction();
                        MessageBox.Show(ItemName.Text + " was added to the inventory........");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }

                    MyConnection.Close();
                }
            }
            fillgrid();
        }
        private void addSubitem()
        {
            //    CommandString = "insert into SubItemInventory(Item_Name, Quatity, Quality , Size ,Cost ,Providerr,MInventory_id) values " +
            //            "( '" + ItemName.Text + "', '" + ItemQuantity.Text + "', '" + ItemQuality.Text + "', '" + ItemSize.Text + "', '" + ItemCost.Text + "' , '" + ItemProv.Text + "', '" + PitemID + "' );";
            //    MyCommand = new SqlCommand(CommandString, MyConnection);
            //    try
            //    {
            //        MyConnection.Open();
            //        MyCommand.ExecuteNonQuery();
            //        MessageBox.Show(ItemName.ToString() + " " + "was added to the Sub inventory........");
            //    }
            //    catch (Exception E)
            //    {
            //        MessageBox.Show(E.Message);
            //    }
            //    MyConnection.Close();
        }

        private void fillcombobox()
        {
            //    CommandString = "select id,Item_Name from MInventory; ";
            //    MyCommand = new SqlCommand(CommandString, MyConnection);
            //    Myadapter = new SqlDataAdapter(MyCommand);
            //    DataSet MydataSet = new DataSet();



            //    try
            //    {
            //        MyConnection.Open();
            //        MydataSet.Clear();
            //        Myadapter.Fill(MydataSet);
            //        MyCommand.ExecuteNonQuery();
            //        selectPItem.DataSource = MydataSet.Tables[0];
            //        selectPItem.DisplayMember = "Item_Name";
            //        selectPItem.ValueMember = "id";
            //        selectPItem.Text = "Select an ITEM";

            //    }
            //    catch (Exception E)
            //    {
            //        MessageBox.Show(E.Message);
            //    }
            //    MyConnection.Close();
        }

        private void fillgrid()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                CommandString = "select top 2 * from MInventory order by id desc";
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

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            checkEmpty();
            try
            {
                qnt = Convert.ToInt32(ItemQuantity.Text);
                cost = Convert.ToInt32(ItemCost.Text);
                size = Convert.ToInt32(ItemSize.Text);
                if(qnt < 0 || cost <= 0 || size <= 0)
                {
                    missing = true;
                    empmss.Text = "Invalid Input";
                }
            }
            catch(Exception ex)
            {
                missing = true;
                empmss.Text = "Invalid Input";
            }


            addPitem();
            clear();
            //if (IsSubItem.Checked == false)
            //{
            //    if (!missing)
            //    {
            //        addPitem();
            //    }
            //}
            //else if (IsSubItem.Checked == true)
            //{
            //    getid();
            //    if (selectPItem.SelectedIndex >= 0)
            //    {
            //        if (!missing)
            //        {
            //            addSubitem();
            //        }
            //    }
            }

        private void clear()
        {
            ItemName.Text = ItemQuantity.Text = ItemSize.Text = ItemCost.Text =  ItemProv.Text = "";
            ItemQuality.SelectedIndex = -1;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            clear();
        }


        private void insertINTOTransaction()
        {
            int total = cost * qnt;
            string tr = "Purchase";
            CommandString = "insert into transactions( Item_Name, Quatity, Quality, Size, Cost, Total, Providerr)  values " +
                            "( '" + ItemName.Text + "', '" + qnt + "', '" + ItemQuality.SelectedItem + "', '" + size + "', '" + cost + "' ,'" + total + "' , '" + ItemProv.Text + "' );";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyCommand.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            CommandString = "update transactions set transactions = '" + tr + "' where transactions is null";
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

        
    }
    
}

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
    public partial class CustomerTableFr : Form
    {
        bool isUser = false;
        string usertp = "";
        public CustomerTableFr()
        {
            InitializeComponent();
        }
        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();

        private void CustomerTableFr_Load(object sender, EventArgs e)
        {
            usertp = Program.usertp;
            user();
            fill();
        }
        private void fill()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                CommandString = "select * from customer order by id";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    Myadapter = new SqlDataAdapter(MyCommand);
                    MydataTable.Clear();
                    Myadapter.Fill(MydataTable);
                    int index = 0;
                    CustomerdataGrid.Rows.Clear();
                    foreach (DataRow inv in MydataTable.Rows)
                    {

                        CustomerdataGrid.Rows.Add();
                        CustomerdataGrid.Rows[index].Cells[0].Value = inv["id"];
                        CustomerdataGrid.Rows[index].Cells[1].Value = inv["First_Name"];
                        CustomerdataGrid.Rows[index].Cells[2].Value = inv["Middle_Name"];
                        CustomerdataGrid.Rows[index].Cells[3].Value = inv["Last_Name"];
                        CustomerdataGrid.Rows[index].Cells[4].Value = inv["Phone"];
                        CustomerdataGrid.Rows[index].Cells[5].Value = inv["Addresss"];
                        CustomerdataGrid.Rows[index].Cells[6].Value = inv["orde_id"];
                        CustomerdataGrid.Rows[index].Cells[7].Value = imageList1.Images[3];
                        index++;
                    }
                    CustomerdataGrid.Refresh();

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }

                MyConnection.Close();

            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fill();
        }

        void user()
        {
            if (usertp == "User")
            {
                isUser = true;
            }
        }
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                    fill();
                else
                {

                    CommandString = "select * from customer where First_Name LIke '" + txtsearch.Text + "%' or  orde_id LIke '%" + txtsearch.Text + "%'";
                    MyCommand = new SqlCommand(CommandString, MyConnection);
                    try
                    {
                        MyConnection.Open();
                        Myadapter = new SqlDataAdapter(MyCommand);
                        MydataTable.Clear();
                        Myadapter.Fill(MydataTable);
                        int index = 0;
                        CustomerdataGrid.Rows.Clear();
                        foreach (DataRow inv in MydataTable.Rows)
                        {

                            CustomerdataGrid.Rows.Add();
                            CustomerdataGrid.Rows[index].Cells[0].Value = inv["id"];
                            CustomerdataGrid.Rows[index].Cells[1].Value = inv["First_Name"];
                            CustomerdataGrid.Rows[index].Cells[2].Value = inv["Middle_Name"];
                            CustomerdataGrid.Rows[index].Cells[3].Value = inv["Last_Name"];
                            CustomerdataGrid.Rows[index].Cells[4].Value = inv["Phone"];
                            CustomerdataGrid.Rows[index].Cells[5].Value = inv["Addresss"];
                            CustomerdataGrid.Rows[index].Cells[6].Value = inv["orde_id"];
                            CustomerdataGrid.Rows[index].Cells[7].Value = imageList1.Images[3];
                            index++;
                        }
                        CustomerdataGrid.Refresh();

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

        private void CustomerdataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isUser)
            {
                if (CustomerdataGrid.CurrentCell.ColumnIndex.Equals(7))
                {
                    int id = Convert.ToInt32(CustomerdataGrid.SelectedRows[0].Cells[0].Value);
                    try
                    {

                        DialogResult dr = MessageBox.Show("Are sure you want to delete this Customer? ", "remove Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
        }

        private void deleteItem(int id)
        {
            MessageBox.Show(id.ToString());
            CommandString = "delete from customer where id = @id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", id);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
                MessageBox.Show("The Customer Was Removed ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(CustomerdataGrid.SelectedRows[0].Cells[0].Value);

            CommandString = "indev_CUSTM_REP'" + id + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);

                RPIndvCustomer RPod = new RPIndvCustomer();
                RPod.SetDataSource(MydataTable);

                CustomerReport OrRP = new CustomerReport();
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

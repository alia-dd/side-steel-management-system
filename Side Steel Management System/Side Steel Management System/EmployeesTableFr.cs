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
    public partial class EmployeesTableFr : Form
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
        public EmployeesTableFr()
        {
            InitializeComponent();
          
        }

        private void EmployeesTableFr_Load(object sender, EventArgs e)
        {
            usertp = Program.usertp;
            user();
            fill();
        }

        void user()
        {
            if (usertp == "User")
            {
                isUser = true;
            }
        }

        private void fill()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                CommandString = "select * from Employees";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    Myadapter = new SqlDataAdapter(MyCommand);
                    MydataTable.Clear();
                    Myadapter.Fill(MydataTable);
                    int index = 0;
                    EmployeedataGrid.Rows.Clear();
                    foreach (DataRow inv in MydataTable.Rows)
                    {

                        EmployeedataGrid.Rows.Add();
                        EmployeedataGrid.Rows[index].Cells[0].Value = inv["id"].ToString();
                        EmployeedataGrid.Rows[index].Cells[1].Value = inv["First_Name"].ToString();
                        EmployeedataGrid.Rows[index].Cells[2].Value = inv["Middle_Name"].ToString();
                        EmployeedataGrid.Rows[index].Cells[3].Value = inv["Last_Name"].ToString();
                        EmployeedataGrid.Rows[index].Cells[4].Value = inv["Age"].ToString();
                        EmployeedataGrid.Rows[index].Cells[5].Value = inv["Gender"].ToString();
                        EmployeedataGrid.Rows[index].Cells[6].Value = inv["Job_Desc"].ToString();
                        EmployeedataGrid.Rows[index].Cells[7].Value = inv["User_type"].ToString();
                        EmployeedataGrid.Rows[index].Cells[8].Value = inv["Phone"].ToString();
                        EmployeedataGrid.Rows[index].Cells[9].Value = inv["Addresss"].ToString();
                        EmployeedataGrid.Rows[index].Cells[10].Value = inv["register_date"].ToString();
                        EmployeedataGrid.Rows[index].Cells[11].Value = imageList1.Images[2];
                        EmployeedataGrid.Rows[index].Cells[12].Value = imageList1.Images[3];
                        index++;
                    }
                    EmployeedataGrid.Refresh();

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }

                MyConnection.Close();

            }

        }

        private void EmployeedataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isUser)
            {
                if (EmployeedataGrid.CurrentCell.ColumnIndex.Equals(11))
                {

                    int id = Convert.ToInt32(EmployeedataGrid.SelectedRows[0].Cells[0].Value);
                    UpdateEmployeeFr myform = new UpdateEmployeeFr();
                    myform.updateind = id;
                    myform.ShowDialog();

                }
                if (EmployeedataGrid.CurrentCell.ColumnIndex.Equals(12))
                {
                    int id = Convert.ToInt32(EmployeedataGrid.SelectedRows[0].Cells[0].Value);
                    try
                    {

                        DialogResult dr = MessageBox.Show("Are sure you want to remove this employee? ", "remove employee", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
            CommandString = "delete from Employees where id = @id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", id);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
                MessageBox.Show("Employee Was Removed ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fill();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                    fill();
                else
                {

                    CommandString = "select * from Employees where First_Name LIke '" + txtsearch.Text + "%' or  id LIke '%" + txtsearch.Text + "%'";
                    MyCommand = new SqlCommand(CommandString, MyConnection);
                    try
                    {
                        MyConnection.Open();
                        Myadapter = new SqlDataAdapter(MyCommand);
                        MydataTable.Clear();
                        Myadapter.Fill(MydataTable);
                        int index = 0;
                        EmployeedataGrid.Rows.Clear();
                        foreach (DataRow inv in MydataTable.Rows)
                        {

                            EmployeedataGrid.Rows.Add();
                            EmployeedataGrid.Rows[index].Cells[0].Value = inv["id"].ToString();
                            EmployeedataGrid.Rows[index].Cells[1].Value = inv["First_Name"].ToString();
                            EmployeedataGrid.Rows[index].Cells[2].Value = inv["Middle_Name"].ToString();
                            EmployeedataGrid.Rows[index].Cells[3].Value = inv["Last_Name"].ToString();
                            EmployeedataGrid.Rows[index].Cells[4].Value = inv["Age"].ToString();
                            EmployeedataGrid.Rows[index].Cells[5].Value = inv["Gender"].ToString();
                            EmployeedataGrid.Rows[index].Cells[6].Value = inv["Job_Desc"].ToString();
                            EmployeedataGrid.Rows[index].Cells[7].Value = inv["User_type"].ToString();
                            EmployeedataGrid.Rows[index].Cells[8].Value = inv["Phone"].ToString();
                            EmployeedataGrid.Rows[index].Cells[9].Value = inv["Addresss"].ToString();
                            EmployeedataGrid.Rows[index].Cells[10].Value = inv["register_date"].ToString();
                            EmployeedataGrid.Rows[index].Cells[11].Value = imageList1.Images[2];
                            EmployeedataGrid.Rows[index].Cells[12].Value = imageList1.Images[3];

                            index++;
                        }
                        EmployeedataGrid.Refresh();

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

        private void btnManagmet_Click(object sender, EventArgs e)
        {
            string ab = "Manager";
            CommandString = "select * from Employees where Job_Desc = '" + ab+ "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);
                int index = 0;

                EmployeedataGrid.Rows.Clear();
                foreach (DataRow inv in MydataTable.Rows)
                {

                    EmployeedataGrid.Rows.Add();
                    EmployeedataGrid.Rows[index].Cells[0].Value = inv["id"].ToString();
                    EmployeedataGrid.Rows[index].Cells[1].Value = inv["First_Name"].ToString();
                    EmployeedataGrid.Rows[index].Cells[2].Value = inv["Middle_Name"].ToString();
                    EmployeedataGrid.Rows[index].Cells[3].Value = inv["Last_Name"].ToString();
                    EmployeedataGrid.Rows[index].Cells[4].Value = inv["Age"].ToString();
                    EmployeedataGrid.Rows[index].Cells[5].Value = inv["Gender"].ToString();
                    EmployeedataGrid.Rows[index].Cells[6].Value = inv["Job_Desc"].ToString();
                    EmployeedataGrid.Rows[index].Cells[7].Value = inv["User_type"].ToString();
                    EmployeedataGrid.Rows[index].Cells[8].Value = inv["Phone"].ToString();
                    EmployeedataGrid.Rows[index].Cells[9].Value = inv["Addresss"].ToString();
                    EmployeedataGrid.Rows[index].Cells[10].Value = inv["register_date"].ToString();
                    EmployeedataGrid.Rows[index].Cells[11].Value = imageList1.Images[2];
                    EmployeedataGrid.Rows[index].Cells[12].Value = imageList1.Images[3];
                    index++;
                }
                EmployeedataGrid.Refresh();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            MyConnection.Close();

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(EmployeedataGrid.SelectedRows[0].Cells[0].Value);

            CommandString = "indev_EMP_REP'" + id + "'";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            try
            {
                MyConnection.Open();
                Myadapter = new SqlDataAdapter(MyCommand);
                MydataTable.Clear();
                Myadapter.Fill(MydataTable);

                RPindvEmp RPod = new RPindvEmp();
                RPod.SetDataSource(MydataTable);

                IndvEmployeeReport OrRP = new IndvEmployeeReport();
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

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
    public partial class UpdateEmployeeFr : Form
    {
        public int updateind = 0;
        bool missing = false;
        int phone = 0;

        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;
        DataTable MydataTable = new DataTable();

        public UpdateEmployeeFr()
        {
            InitializeComponent();
        }

        private void btnCleare_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            txtfirstname.Text = txtmiddlename.Text = txtlastname.Text = txtage.Text = txtphone.Text = txtaddress.Text = "";
            combGenrder.SelectedIndex = -1;
            txtjoddesc.SelectedIndex = -1;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            updateitem();
        }
        private void updateitem()
        {
            checkEmpty();
            try
            {
                phone = Convert.ToInt32(txtphone.Text);

            }
            catch (Exception ex)
            {
                missing = true;
                empmss.Text = "Invalid Input";
            }

            if (!missing)
            {
                

                 MessageBox.Show(updateind.ToString());
                 CommandString = "update Employees set First_Name ='" + txtfirstname.Text + "', Middle_Name ='" + txtmiddlename.Text +
                    "' ,Last_Name = '" + txtlastname.Text + "',Age ='" + txtage.Text + "',Gender ='" + combGenrder.SelectedItem + "',Job_Desc ='" +
                    txtjoddesc.SelectedItem + "',Phone ='" + txtphone.Text + "',Addresss ='" + txtaddress.Text +
                    "'where id = '" + updateind + "'";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    MyCommand.ExecuteNonQuery();
                    MessageBox.Show(txtfirstname.Text.ToString() + " " + "was updated.......");
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                MyConnection.Close();
            }

            fillgrid();
        }

        void checkEmpty()
        {
            if (txtfirstname.Text == "" || txtmiddlename.Text == "" || txtlastname.Text == "" || txtage.Text == "" || combGenrder.SelectedIndex < 0 || txtjoddesc.SelectedIndex < 0 || txtphone.Text == "" || txtaddress.Text == "")
            {
                missing = true;
                empmss.Text = "Missing Fields.." + '\n' + '\t' + " all fields must filled";
            }
            else if (Convert.ToInt32(txtage.Text) < 17 || Convert.ToInt32(txtage.Text) > 67)
            {
                missing = true;
                empmss.Text = "new employee should be older then 16 and younger then 68";
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

        private void fillgrid()
        {
                CommandString = "select * from Employees where id = '" + updateind + "'";
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
        private void filltxt()
        {
            
                CommandString = "select First_Name, Middle_Name, Last_Name , Age ,Phone ,Addresss from Employees where id ='" + updateind + "'";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    Myadapter = new SqlDataAdapter(MyCommand);
                    MydataTable.Clear();
                    Myadapter.Fill(MydataTable);
                    txtfirstname.Text = MydataTable.Rows[0]["First_Name"].ToString();
                    txtmiddlename.Text = MydataTable.Rows[0]["Middle_Name"].ToString();
                    txtlastname.Text = MydataTable.Rows[0]["Last_Name"].ToString();
                    txtage.Text = MydataTable.Rows[0]["Age"].ToString();
                    txtphone.Text = MydataTable.Rows[0]["Phone"].ToString();
                    txtaddress.Text = MydataTable.Rows[0]["Addresss"].ToString();

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }

                MyConnection.Close();
    

        }
        private void UpdateEmployeeFr_Load(object sender, EventArgs e)
        {
            fillgrid();
            filltxt();
        }
    }
}

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
    public partial class AddNewEmployeeFr : Form
    {
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

        public AddNewEmployeeFr()
        {
            InitializeComponent();
        }

        private void bunifuTextBox5_TextChanged(object sender, EventArgs e)
        {
        
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
            else if(txtphone.Text.Length < 7)
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
        private void clear()
        {
            txtfirstname.Text = txtmiddlename.Text = txtlastname.Text = txtage.Text= txtphone.Text = txtaddress.Text = "";
            combGenrder.SelectedIndex = -1;
            txtjoddesc.SelectedIndex = -1;
        }

        private void fill()
        {
            using (shop_Managment_SystemEntities mydata = new shop_Managment_SystemEntities())
            {
                CommandString = "select top 2 * from Employees order by id desc";
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

        }
        private void AddNewEmployeeFr_Load(object sender, EventArgs e)
        {
            //combGenrder.Text = "Select Gender";
            fill();
        }
        


        private void bunifuButton1_Click_1(object sender, EventArgs e)
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


                CommandString = "insert into Employees(First_Name, Middle_Name, Last_Name, Age,Gender, Job_Desc, Phone, Addresss) values " +
                    "( '" + txtfirstname.Text + "', '" + txtmiddlename.Text + "', '" + txtlastname.Text + "', '" + txtage.Text + "', '" + combGenrder.SelectedItem + "', '" + txtjoddesc.SelectedItem + "', '" + txtphone.Text + "', '" + txtaddress.Text + "');";
                MyCommand = new SqlCommand(CommandString, MyConnection);
                try
                {
                    MyConnection.Open();
                    MyCommand.ExecuteNonQuery();
                    MessageBox.Show("inserted");
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                MyConnection.Close();
                fill();
            }
        }
       

        private void combGenrder_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}

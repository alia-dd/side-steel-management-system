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
    public partial class DashboardFr : Form
    {

        static string ConnectionString = @"server=ENVY\SQLEXPRESS; database = shop_Managment_System; Integrated Security = True";
        SqlConnection MyConnection = new SqlConnection(ConnectionString);
        string CommandString;
        SqlCommand MyCommand = new SqlCommand();
        SqlCommandBuilder MycommandBuilder;
        SqlDataReader MyDataReader;
        SqlDataAdapter Myadapter;


        public String firstname = "";
         public String middlname = "";
         public String usertp = "";
        
        public DashboardFr()
        {
            InitializeComponent();
            hideAllPanal();
        }

        private void hideAllPanal()
        {
            Inventorypanel.Visible = false;
            Accountpanel2.Visible = false;
            Empoleepanel.Visible = false;
            Userpanel.Visible = false;
            Orderpanel.Visible = false;
        }
        private void hideSubPanal()
        {
            if(Inventorypanel.Visible == true)
                Inventorypanel.Visible = false;
            if (Accountpanel2.Visible == true)
                Accountpanel2.Visible = false;
            if (Empoleepanel.Visible == true)
                Empoleepanel.Visible = false;
            if (Userpanel.Visible == true)
                Userpanel.Visible = false;
            if (Orderpanel.Visible == true)
                Orderpanel.Visible = false;

        }
        private void showSubPanal(Panel subpanal)
        {
            if (subpanal.Visible == false)
            {
                hideSubPanal();
                subpanal.Visible = true;
            }
            else
                subpanal.Visible = false;
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            showSubPanal(Inventorypanel);
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            showSubPanal(Accountpanel2);
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            showSubPanal(Empoleepanel);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            showSubPanal(Userpanel);
        }

      

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            openChildForm(new DashBfr());
            hideSubPanal();
            shwlb.Image = btnDashboard.Image;
            shwlb.Text = btnDashboard.Text;
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            hideSubPanal();
        }    

        private void btnOrder_Click(object sender, EventArgs e)
        {
            showSubPanal(Orderpanel);
        }
       

        private void InventoryTable_Click(object sender, EventArgs e)
        {
            openChildForm(new InventoryTableFr());
            hideSubPanal();
            shwlb.Image = InventoryTable.Image;
            shwlb.Text = InventoryTable.Text;
        }

        private Form activeForm = null;
        public void openChildForm(Form ChForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = ChForm;
            ChForm.TopLevel = false;
            ChForm.FormBorderStyle = FormBorderStyle.None;
            ChForm.Dock = DockStyle.Fill;
            Frmpanel.Controls.Add(ChForm);
            Frmpanel.Tag = ChForm;
            ChForm.BringToFront();
            ChForm.Show();

        }

        private void EmployeeTable_Click(object sender, EventArgs e)
        {
            openChildForm(new EmployeesTableFr());
            hideSubPanal();
            shwlb.Image = EmployeeTable.Image;
            shwlb.Text = EmployeeTable.Text;
        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void UserTable_Click(object sender, EventArgs e)
        {
            openChildForm(new UserTableFr());
            hideSubPanal();
            shwlb.Image = UserTable.Image;
            shwlb.Text = UserTable.Text;
        }

        private void AddNewItem_Click(object sender, EventArgs e)
        {
            openChildForm(new AddNewItemFr());
            hideSubPanal();
            shwlb.Image = AddNewItem.Image;
            shwlb.Text = AddNewItem.Text;
        }

        private void newOrder_Click(object sender, EventArgs e)
        {
            openChildForm(new PlaceOrderFr());
            hideSubPanal();
            shwlb.Image = newOrder.Image;
            shwlb.Text = newOrder.Text;
        }

        private void orderDetails_Click(object sender, EventArgs e)
        {
            openChildForm(new OrderDetailTableFr());
            hideSubPanal();
            shwlb.Image = orderDetails.Image;
            shwlb.Text = orderDetails.Text;
        }

        private void addEmployee_Click(object sender, EventArgs e)
        {
            openChildForm(new AddNewEmployeeFr());
            hideSubPanal();
            shwlb.Image = addEmployee.Image;
            shwlb.Text = addEmployee.Text;
        }

        private void createUser_Click(object sender, EventArgs e)
        {
            openChildForm(new AddNewUser());
            hideSubPanal();
            shwlb.Image = createUser.Image;
            shwlb.Text = createUser.Text;
        }

        private void DashboardFr_Load(object sender, EventArgs e)
        {
            openChildForm(new DashBfr());
            hideSubPanal();
            shwlb.Image = btnDashboard.Image;
            shwlb.Text = btnDashboard.Text;

            firstname = Program.firstname;
             usertp = Program.usertp;
             middlname = Program.middlname;

            lbName.Text = char.ToUpper(firstname[0]) + firstname.Substring(1) +"."+ Char.ToUpper(middlname[0]);
            user();
           // MessageBox.Show(firstname);
        }
        void user()
        {
            if (usertp == "User")
            {
                btnUsers.Visible = false;
                
            }
        }

        private void OutstandingInvoice_Click(object sender, EventArgs e)
        {
            openChildForm(new CustomerTableFr());
            hideSubPanal();
            shwlb.Image = OutstandingInvoice.Image;
            shwlb.Text = OutstandingInvoice.Text;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            changestata();
            this.Hide();
            loginFr myform = new loginFr();
            changestata();
            myform.ShowDialog();
            this.Close();
        }
        private void changestata()
        {
            int id = 0;
            CommandString = "select u.id from users as u join Employees as e on  u.id=e.id where e.First_Name=@name";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@name", firstname);
            try
            {
                MyConnection.Open();
                if (MyCommand.ExecuteScalar() == null)
                    id = 0;
                else
                {
                    id = int.Parse(MyCommand.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();

            CommandString = "update users set status= '" + "not active" + "'where id=@id";
            MyCommand = new SqlCommand(CommandString, MyConnection);
            MyCommand.Parameters.AddWithValue("@id", id);
            try
            {
                MyConnection.Open();
                MyCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyConnection.Close();
        }

        private void changeps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            changepass myform = new changepass();
            myform.ShowDialog();
        }

        private void TransactionTable_Click(object sender, EventArgs e)
        {
            openChildForm(new transactionTableFr());
            hideSubPanal();
            shwlb.Image = TransactionTable.Image;
            shwlb.Text = TransactionTable.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InventoryReport inventRp = new InventoryReport();
            inventRp.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TransactionReport tranRP = new TransactionReport();
            tranRP.ShowDialog();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            EmployeeReport empRP = new EmployeeReport();
            empRP.ShowDialog();
        }

        private void orderrep_Click(object sender, EventArgs e)
        {
            OrderDetailReport empRP = new OrderDetailReport();
            empRP.ShowDialog();
        }
    }
}

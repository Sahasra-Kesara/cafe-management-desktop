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

namespace cafe_management
{
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\acer\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            Con.Open();
            string query = "select * from UsersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserOrder uorder = new UserOrder();
            uorder.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ItemsForm item = new ItemsForm();
            item.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "INSERT INTO UsersTbl (Uname, Uphone, Upassword) VALUES ('" + unameTb.Text + "','" + UphoneTb.Text + "','" + UpassTb.Text + "')";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("User Successfully Created");
            Con.Close();
            populate();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (UsersGV.SelectedRows.Count > 0)
            {
                unameTb.Text = UsersGV.SelectedRows[0].Cells[0].Value.ToString();
                UphoneTb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
                UpassTb.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
            }
            else
            {
                MessageBox.Show("No row selected");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(UphoneTb.Text == "")
            {
                MessageBox.Show("Select the User to be Deleted");
            }
            else
            {
                Con.Open();
                string query = "delete from UsersTbl where Uphone = '" + UphoneTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(UphoneTb.Text == "" || UpassTb.Text == "" || unameTb.Text == "")
            {
                MessageBox.Show("Fill All The Fields");
            }
            else
            {
                Con.Open();
                string query = "update UsersTbl set Uname = '" + unameTb.Text + "', Upassword='"+UpassTb.Text+"' where Uphone= '"+UphoneTb.Text+"'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Updated");
                Con.Close();
                populate();
            }
        }
    }
}

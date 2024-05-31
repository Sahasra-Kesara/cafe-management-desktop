using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace cafe_management
{
    public partial class ItemsForm : Form
    {
        public ItemsForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\acer\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            Con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserOrder order = new UserOrder();
            order.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersForm user = new UsersForm();
            user.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ItemNameTb.Text == "" || ItemNumTb.Text == "" || PriceCb.Text == "")
            {
                MessageBox.Show("Fill all the Data");
            }
            else
            {
                Con.Open();
                string query = "INSERT INTO ItemTbl VALUES (" + ItemNumTb.Text + ",'" + ItemNameTb.Text + "','" + CatCb.SelectedItem.ToString() + "', "+PriceCb.Text+")";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Created");
                Con.Close();
                populate();
            }

        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItemNumTb.Text = ItemsGV.SelectedRows[0].Cells[0].Value.ToString();
            ItemNameTb.Text = ItemsGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.SelectedItem = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
            PriceCb.Text = ItemsGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ItemNumTb.Text == "")
            {
                MessageBox.Show("Select the Item to be Deleted");
            }
            else
            {
                Con.Open();
                string query = "delete from ItemTbl where ItemNum = '" + ItemNumTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ItemNumTb.Text == "" || ItemNameTb.Text == "" || PriceCb.Text == "")
            {
                MessageBox.Show("Fill All The Fields");
            }
            else
            {
                Con.Open();
                string query = "update ItemTbl set ItemName = '" + ItemNameTb.Text + "', ItemCat='" + CatCb.SelectedItem.ToString() + "' where ItemPrice= " + PriceCb.Text + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Updated");
                Con.Close();
                populate();
            }
        }
    }
}

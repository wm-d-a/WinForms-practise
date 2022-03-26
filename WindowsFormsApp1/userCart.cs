using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class userCart : Form
    {
        public userCart()
        {
            InitializeComponent();
        }

        private void userCart_Load(object sender, EventArgs e)
        {
            listView1.CheckBoxes = true;
            DB db = new DB();
            DataTable table = new DataTable();
            listView1.Columns[0].Text = "";
            listView1.Columns[1].Text = "Цена";
            listView1.Columns[2].Text = "Название";
            MySqlCommand command = new MySqlCommand("SELECT * FROM `userCart`", db.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                ListViewItem item = new ListViewItem(new[] { row[0].ToString(), row[2].ToString(), row[1].ToString() });
                listView1.Items.Add(item);
            }
            listView1.View = View.Details;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string buf = Convert.ToString(textBox1.Text);
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Введите адрес!");
                textBox1.BackColor = Color.FromArgb(250, 128, 114);
            }

            else if (buf.Count(' '.Equals) == buf.Length)
            {
                MessageBox.Show("Введите адрес!");
                textBox1.BackColor = Color.FromArgb(250, 128, 114);
            }
            else
            {
                DB db = new DB();
                MySqlCommand command;
                db.openConnection();
                command = new MySqlCommand("DELETE FROM `usercart`", db.getConnection());
                command.ExecuteReader();
                db.closeConnection();
                mainForm mF = new mainForm();
                mF.Show();
                this.Close();
            }
        }
        Point lastPoint;
        private void userCart_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void userCart_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Black;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

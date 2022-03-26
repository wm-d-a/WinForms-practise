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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            
            listView1.CheckBoxes = true;
            DB db = new DB();
            DataTable table = new DataTable();
            listView1.Columns[0].Text = "";
            listView1.Columns[1].Text = "Цена";
            listView1.Columns[2].Text = "Название";
            MySqlCommand command = new MySqlCommand("SELECT * FROM `items`", db.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                ListViewItem item = new ListViewItem(new[] { row[0].ToString(), row[2].ToString(), row[1].ToString() });
                listView1.Items.Add(item);
            }
            listView1.View = View.Details;
            label4.Text = "0";
            label6.Text = "0";
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            

        }
        Point lastPoint;
        private void mainForm_MouseMove(object sender, MouseEventArgs e)
        {

            
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
            
        }

        private void mainForm_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Black;
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        List<int> ids = new List<int>();
        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int lastCheckBoxes = Convert.ToInt32(label4.Text);
            ListView.CheckedListViewItemCollection checkedItems = listView1.CheckedItems;
            label4.Text = Convert.ToString(checkedItems.Count);
            int currentCheckBoxes = Convert.ToInt32(label4.Text);
            int price = Convert.ToInt32(label6.Text);

            DB db = new DB();
            DataTable table = new DataTable();
            int id = Convert.ToInt32(e.Item.Text);
            if (lastCheckBoxes < currentCheckBoxes)
            {   
                
                MySqlCommand command = new MySqlCommand("SELECT `Цена (рубли)`, `Название` FROM `items` WHERE id = @ii", db.getConnection());
                command.Parameters.Add("@ii", MySqlDbType.Int32).Value = id;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                price += Convert.ToInt32(table.Rows[0][0].ToString());
                //MessageBox.Show(table.Rows[0][1].ToString());
                //MessageBox.Show(Convert.ToString(id));
                
                ids.Add(id);
            }
            else {
                ids.Remove(id);
                MySqlCommand command = new MySqlCommand("SELECT `Цена (рубли)`, `Название` FROM `items` WHERE id = @ii", db.getConnection());
                command.Parameters.Add("@ii", MySqlDbType.Int32).Value = id;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                price -= Convert.ToInt32(table.Rows[0][0].ToString());
            }
            label6.Text = Convert.ToString(price);
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {   

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        public string getName(int id) {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT `Название` FROM `items` WHERE id = @ii", db.getConnection());
            command.Parameters.Add("@ii", MySqlDbType.Int32).Value = id;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            //MessageBox.Show(Convert.ToString(id));
            //MessageBox.Show(table.Rows[0][0].ToString());
            return table.Rows[0][0].ToString();
        } 
        public string getPrice(int id) {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT `Цена (рубли)`, `Название` FROM `items` WHERE id = @ii", db.getConnection());
            command.Parameters.Add("@ii", MySqlDbType.Int32).Value = 7;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            //MessageBox.Show(Convert.ToString(id));
            //MessageBox.Show(table.Rows[0][0].ToString());
            return table.Rows[0][0].ToString();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            userCart uC = new userCart();
            DB db = new DB();
            MySqlCommand command;
            if (ids.Count == 0)
            {
                MessageBox.Show("Веберите товары!");
            }
            else
            {
                for (int ind = 0; ind < ids.Count; ind++)
                {
                    command = new MySqlCommand("INSERT INTO `usercart` (`id`, `Название`, `Цена`) VALUES (NULL, @in, @ip)", db.getConnection());
                    command.Parameters.Add("@in", MySqlDbType.VarChar).Value = getName(ids[ind]);
                    command.Parameters.Add("@ip", MySqlDbType.Int32).Value = getPrice(ids[ind]);
                    db.openConnection();
                    if (command.ExecuteNonQuery() == 1) { }
                    db.closeConnection();
                }
                uC.Show();
                this.Close();
            }
        }
    }
}

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
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            RegistrationForm regForm = new RegistrationForm();
            regForm.Show();
        }

        private void loginField_TextChanged(object sender, EventArgs e)
        {
            this.loginField.AutoSize = false;
        }

        private void passField_TextChanged(object sender, EventArgs e)
        {
            this.passField.AutoSize = false;
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Black;
        }

        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                this.Left += e.X - lastPoint.X;   
                this.Top += e.Y - lastPoint.Y;   
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            string loginUser = loginField.Text;
            string passUser = passField.Text;
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @ul AND `password` = @up", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = loginUser;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                loginField.BackColor = Color.FromArgb(143, 188, 143);
                passField.BackColor = Color.FromArgb(143, 188, 143);
                mainForm mF = new mainForm();
                mF.Show();
                this.Hide();

            }
            else {
                loginField.BackColor = Color.FromArgb(250, 128, 114);
                passField.BackColor = Color.FromArgb(250, 128, 114);
                MessageBox.Show("Пользователь не авторизован");
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

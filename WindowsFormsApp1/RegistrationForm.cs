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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            AuthorizationForm auth = new AuthorizationForm();

            string login = loginField.Text;
            string pass = passField.Text;
            string nick = nickField.Text;
            if (login.Length == 0 || pass.Length == 0 || nick.Length == 0) 
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            if (UserInDB())
            {
                return;
            }
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`id`, `login`, `password`, `nickname`) VALUES (NULL, @ul, @up, @unn)", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;
            command.Parameters.Add("@unn", MySqlDbType.VarChar).Value = nick;
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Пользователь зарегистрирован");
                auth.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не зарегистрирован");
            }
            db.closeConnection();
        }
        public bool UserInDB() {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @ul", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginField.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                loginField.BackColor = Color.FromArgb(250, 128, 114);
                MessageBox.Show("Такой логин уже существует!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

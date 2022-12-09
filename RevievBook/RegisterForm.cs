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

namespace RevievBook
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            UserNameField.Text = "Введите имя";
            UserSurnameField.Text = "Введите фамилию";
            
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }

        #region Перемещение левой кнопкой мыши
        Point LastPoint;
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - LastPoint.X;
                this.Top += e.Y - LastPoint.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            LastPoint = new Point(e.X, e.Y);
        }
        #endregion

        private void UserNameField_Enter(object sender, EventArgs e)
        {
            if (UserNameField.Text.Equals("Введите имя"))
            {
                UserNameField.Text = "";
                UserNameField.ForeColor = Color.Gray;
            }
            
        }

        private void UserNameField_Leave(object sender, EventArgs e)
        {
            if (UserNameField.Text.Equals(""))
            {
                UserNameField.Text = "Введите имя";
                UserNameField.ForeColor = Color.Gray;
            }
        }

        private void UserSurnameField_Enter(object sender, EventArgs e)
        {
            if (UserSurnameField.Text.Equals("Введите фамилию"))
            {
                UserSurnameField.Text = "";
                UserSurnameField.ForeColor = Color.Gray;
            }

        }

        private void UserSurnameField_Leave(object sender, EventArgs e)
        {
            if (UserSurnameField.Text.Equals(""))
            {
                UserSurnameField.Text = "Введите фамилию";
                UserSurnameField.ForeColor = Color.Gray;
            }

        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (UserNameField.Text == "Введите имя")
            {
                MessageBox.Show("Введите имя пользователя");
                return;
            }
           
            if (LoginField.Text == "")
            {
                MessageBox.Show("Введите логин пользователя");
                return;
            }
            if (PasswordField.Text == "")
            {
                MessageBox.Show("Введите пароль пользователя");
                return;
            }
            if (isUserExist())
                return;
            DataBase db = new DataBase();

            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `Name`, `Surname`) VALUES (@login,@pass,@name,@surname)", db.GetConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = LoginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = PasswordField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = UserNameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = UserSurnameField.Text;
           db.OpenConnection();
          

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт был создан");
                this.Hide();
                MainForms mainForms = new MainForms();
                mainForms.Show();
            }
            else
            {
                MessageBox.Show("Аккаунт не был создан");
            }

            db.CloseConnection();
        }
        public Boolean isUserExist()
        {
            DataBase db = new DataBase();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = LoginField.Text;
           
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void registerlabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}


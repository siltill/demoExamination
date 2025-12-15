using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demoNEW_EFCoreVersion
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            //var context = new ApplicationContext();
            //ExcelImportData importData = new ExcelImportData(context);
            //importData.ImportAllData();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните логин и пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new ApplicationContext())
            {
                var user = context.Users
                    .Include(u => u.UsersRole)
                    .FirstOrDefault(u => u.Login == login && u.Password == password); // В реальном проекте используйте хэширование пароля!

                if (user != null)
                {
                    UserSession.SetUser(user);
                    MessageBox.Show($"Добро пожаловать, {UserSession.FullName}!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            UserSession.SetGuest();
            MessageBox.Show("Вход выполнен как гость.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

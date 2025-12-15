using System;
using System.Windows.Forms;

namespace demoNEW_EFCoreVersion
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                using (var loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        using (var mainForm = new MainForm())
                        {
                            if (mainForm.ShowDialog() != DialogResult.OK)
                            {
                                // Если пользователь нажал "Вернуться", очищаем сессию и возвращаемся к форме авторизации
                                UserSession.Clear();
                                continue;
                            }
                        }
                    }
                    else
                    {
                        // Пользователь нажал "Выйти" в форме авторизации
                        break;
                    }
                }
            }
        }
    }
}
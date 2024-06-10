using ConferenceOrganization.AppData;
using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConferenceOrganization.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorizationHelper.CheckData(LoginTb.Text, PasswordPb.Password) == true)
            {
                MessageBox.Show("Пользователь успешно авторизован!");
                // успешная авторизация
                Close();
            }
            else
            {
                // неуспешная авторизация
                if (BlockSystemHelper.IncreaseIncorrectInput() == 3)
                {
                    BlockWindow blockWindow = new BlockWindow();
                    if (blockWindow.ShowDialog() == true)
                    {
                        BlockSystemHelper.incorrectInput = 0;
                    }
                }
                else
                {

                }
                MessageBox.Show("Пользователь не найден!");
            }
        }
        void SaveUserData()
        {
            if (RememberMeCb.IsChecked == true)
            {
                // Сохранение данных
                Properties.Settings.Default.LoginValue = LoginTb.Text;
                Properties.Settings.Default.PasswordValue = PasswordPb.Password;
            }
            else
            {
                // Очитска данных
                Properties.Settings.Default.LoginValue = string.Empty;
                Properties.Settings.Default.PasswordValue = string.Empty;
            }

            // Фиксируем изменения
            Properties.Settings.Default.Save();
        }

        void LoadUserData()
        {
            LoginTb.Text = Properties.Settings.Default.LoginValue = LoginTb.Text;
            PasswordPb.Password = Properties.Settings.Default.PasswordValue = PasswordPb.Password;
        }
    }
}

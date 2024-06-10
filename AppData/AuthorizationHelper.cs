using ConferenceOrganization.Model;
using ConferenceOrganization.View.Pages;
using ConferenceOrganization.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConferenceOrganization.AppData
{
    internal class AuthorizationHelper
    {
        public static User currentUser;
        public static string captcha;
        public static bool CheckData(string login, string password)
        {
            // Получаем одну запись по условию из таблиц БД
            currentUser = App.context.User.FirstOrDefault(u => u.Login == login && u.Password == password);

            // Продолжение
            if (currentUser != null)
            {
                //Генерируем captcha
                // Если true, то открываем страницы (капча введена правильно)
                if (GenerateCaptcha() == true)
                {
                    // Загружаем страницы
                    switch (currentUser.RoleId)
                    {
                        case 1:
                            // Загрузка страницы
                            FrameHelper.mainFrame.Navigate(new PartipicipantPage());
                            break;
                        case 2:
                            // Загрузка страницы
                            FrameHelper.mainFrame.Navigate(new ModeratorPage());
                            break;
                        case 3:
                            // Загрузка страницы
                            FrameHelper.mainFrame.Navigate(new JuryPage());
                            break;
                        case 4:
                            // Загрузка страницы
                            FrameHelper.mainFrame.Navigate(new OrganizatorPage());
                            break;  
                    }
                    return true;
                }
                // Иначе то...
                else
                {
                    MessageBox.Show("Капча введена неправильно!");

                    return false;
                }
            }

            return false;
        }

        public static bool GenerateCaptcha()
        {
            // Создаем гененратор случайных
            Random random = new Random();

            // Очищаем поле с капчей
            captcha = String.Empty; // captcha = ""
     
            for (int i = 1; i <= 4; i++)
            {
                // Генерируем число и конвертируем его в символ
                char character = Convert.ToChar(random.Next(65, 91));

                // Складываем символы
                captcha += character;
            }

            // Открываем окно
            CaptchaWindow captchaWindow = new CaptchaWindow();
            if (captchaWindow.ShowDialog() == true)
            {
                return true;
            }
            return false;
        }
    }
}

using ConferenceOrganization.AppData;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConferenceOrganization.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrganizatorPage.xaml
    /// </summary>
    public partial class OrganizatorPage : Page
    {
        public OrganizatorPage()
        {
            InitializeComponent();

            GenerateGreeting();
        }
        private void GenerateGreeting()
        {
            string greeting = string.Empty;

            // 1. Определение времени работы
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            if (new TimeSpan(5, 0, 0) < currentTime && currentTime <= new TimeSpan(11, 0, 0))
            {
                greeting = "Доброе утро!\n";
            }
            else if (new TimeSpan(11, 0, 1) < currentTime && currentTime <= new TimeSpan(18, 0, 0))
            {
                greeting = "Добрый день!\n";
            }
            else if (new TimeSpan(18, 0, 1) < currentTime && currentTime <= new TimeSpan(24, 0, 0))
            {
                greeting = "Добрый вечер!\n";
            }
            else
            {
                greeting = "Добро1 ночи!\n";
            }

            // 2. Определение пола пользователя
            greeting += AuthorizationHelper.currentUser.GenderId == 1 ? "Mrs" : "Ms";

            // 3. Определение имени и отчества из полного имени пользователя
            string[] name = AuthorizationHelper.currentUser.Fullname.Split(' ');
            greeting += $"{name[1]} {name[2]}";

            GreetingTbl.Text = greeting; // => Доброе утро! Mrs Иван Иванович
        }
        private void LoadImageProfile()
        {
            // 1. Создаём код ресурсов (ссылка)
            Uri uri = AuthorizationHelper.currentUser.Photo == null ? new Uri("https://images.thefacecdn.com/images/ED001_DABABY_WEB_1.jpg?auto=compress&q=25&fit=crop&crop=focalpoint&fp-x=0.5&fp-y=0.5&w=1180&h=1475") : new Uri(AuthorizationHelper.currentUser.Photo);

            // 2. Создаём мзображения из ссылки (конвертируем)
            BitmapImage image = new BitmapImage(uri);

            // 3. Передаём изображение в элемент Image
            PhotoImg.Source = image;
        }
    }
}

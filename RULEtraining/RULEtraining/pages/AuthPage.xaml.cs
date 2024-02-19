using RULEtraining.model;
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
using System.Windows.Threading;

namespace RULEtraining.pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private int ErrorCount = 0;
        private int time = 15;
        private DispatcherTimer Timer;

        public AuthPage()
        {
            InitializeComponent();
            textBlockCaptcha.Visibility = Visibility.Hidden;
            txtCaptcha.Visibility = Visibility.Hidden;
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
        }

        public void btnEnter_Click(object value1, object value2)
        {
            throw new NotImplementedException();
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientPage(null));
        }

        public void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = tb_login.Text;
            string password = tb_password.Password;

            var user = RuleeEntities1.GetContext().Authorization.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
            Auth(user);
            /*if (user != null)
            {
                MessageBox.Show("Авторизация выполнена успешно");

                switch (user.ID_Role)
                {
                    case 1:
                        NavigationService.Navigate(new ClientPage("пользователь"));
                        break;
                    case 2:
                        NavigationService.Navigate(new ClientPage("директор"));
                        break;
                    case 3:
                        NavigationService.Navigate(new ClientPage("админ"));
                        break;
                }
            }
            else
            {

                MessageBox.Show("Такого пользователя не существует");
                textBlockCaptcha.Visibility = Visibility;
                txtCaptcha.Visibility = Visibility;
                Captha();
                if (Convert.ToString(textBlockCaptcha.Text) != Convert.ToString(txtCaptcha.Text) && ErrorCount == 2)
                {
                    btnEnter.IsEnabled = false;
                    tb_login.IsEnabled = false;
                    tb_password.IsEnabled = false;
                    txtCaptcha.IsEnabled = false;
                    Timer.Tick += Timer_Tick;
                    Timer.Start();
                }
                else
                {
                    MessageBox.Show("Повторите ввод");
                    ErrorCount += 1;
                }
            }*/
        }


        private void Captha()
        {
            String allowchar = " ";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = " ";
            String temp = " ";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            textBlockCaptcha.IsEnabled = false;
            textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
            textBlockCaptcha.Text = pwd;

        }
        void Timer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                if (time <= 10)
                {
                    if (time % 2 == 0)
                    {
                        tb_error.Foreground = Brushes.Red;
                    }
                    else
                    {
                        tb_error.Foreground = Brushes.White;
                    }
                    time--;
                    tb_error.Text = string.Format("Вы заблокированы на 00:0{0}:0{1}", time / 60, time % 60);

                }
                else
                {
                    time--;
                    tb_error.Text = string.Format("Вы заблокированы на 00:0{0}:{1}", time / 60, time % 60);
                }
            }
            else
            {
                Timer.Stop();
                btnEnter.IsEnabled = true;
                tb_login.IsEnabled = true;
                tb_password.IsEnabled = true;
                txtCaptcha.IsEnabled = true;
                MessageBox.Show("Вы можете продолжить попытку авторизации");
            }
        }
        public bool Auth(Authorization user)
        {
            if (user != null)
            {
                MessageBox.Show("Авторизация выполнена успешно");

                switch (user.ID_Role)
                {
                    case 1:
                        NavigationService.Navigate(new ClientPage("пользователь"));
                        break;
                    case 2:
                        NavigationService.Navigate(new ClientPage("директор"));
                        break;
                    case 3:
                        NavigationService.Navigate(new ClientPage("админ"));
                        break;
                }
            return true;
            }
            else
            {

                MessageBox.Show("Такого пользователя не существует");
                textBlockCaptcha.Visibility = Visibility;
                txtCaptcha.Visibility = Visibility;
                Captha();
                if (Convert.ToString(textBlockCaptcha.Text) != Convert.ToString(txtCaptcha.Text) && ErrorCount == 2)
                {
                    btnEnter.IsEnabled = false;
                    tb_login.IsEnabled = false;
                    tb_password.IsEnabled = false;
                    txtCaptcha.IsEnabled = false;
                    Timer.Tick += Timer_Tick;
                    Timer.Start();
                }
                else
                {
                    MessageBox.Show("Повторите ввод");
                    ErrorCount += 1;
                }
            }
            return false;
        }
    }
}

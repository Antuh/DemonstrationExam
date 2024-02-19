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
using RULEtraining.pages;

namespace RULEtraining
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frmMain.Navigate(new AuthPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (frmMain.CanGoBack)
            {
                frmMain.GoBack();
            }
        }

        private void frmMain_ContentRendered(object sender, EventArgs e)
        {
            if (frmMain.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else btnBack.Visibility = Visibility.Hidden;
        }
    }
}

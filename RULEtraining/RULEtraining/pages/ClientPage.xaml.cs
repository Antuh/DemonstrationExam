using RULEtraining.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace RULEtraining.pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage(string role)
        {
            InitializeComponent();
            DataContext = this;
            Update();
            MessageBox.Show(role);
        }

        public string[] SortingList { get; set; } =
        {
            "Без сортировки",
            "По возрастанию фамилии",
            "По убыванию фамилии",
        };

        public string[] FilterList { get; set; } =
        {
            "Все",
            "Мужской",
            "Женский"
        };

        public string[] CountList { get; set; } =
        {
            "Все",
            "10",
            "25",
            "50"
        };

        private void Update()
        {
            var result = RuleeEntities1.GetContext().Client.ToList();

            if(cmbSort.SelectedIndex == 1) result = result.OrderBy(c => c.FirstName).ToList();
            if(cmbSort.SelectedIndex == 2) result = result.OrderByDescending(c => c.FirstName).ToList();

            if (cmbFilter.SelectedIndex != 0) result = result.Where(c => c.GenderCode == cmbFilter.SelectedIndex.ToString()).ToList();

            result = result.Where(
               c => c.FirstName.ToLower().Contains(txtSearch.Text.ToLower()) 
            || c.Phone.ToLower().Contains(txtSearch.Text.ToLower())
            || c.Email.ToLower().Contains(txtSearch.Text.ToLower())
            ).ToList();

            int currentCount = result.Count();
            int fullCount = RuleeEntities1.GetContext().Client.Count();
            txtCount.Text = $"{currentCount} из {fullCount}";

            if (cmbCount.SelectedIndex != 0 && cmbCount.SelectedItem != null) result = result.Take(Int32.Parse(cmbCount.SelectedValue.ToString())).ToList();

            lvClients.ItemsSource = result;
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void cmbCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditClientPage(null));
        }

        private void lvClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditClientPage(lvClients.SelectedItem as Client));
        }
    }
}

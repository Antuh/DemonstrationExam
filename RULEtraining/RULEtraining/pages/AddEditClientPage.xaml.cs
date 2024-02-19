using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using RULEtraining.model;

namespace RULEtraining.pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditClientPage.xaml
    /// </summary>
    public partial class AddEditClientPage : Page
    {
        Client _currentClient = new Client();
        public AddEditClientPage(Client client)
        {
            InitializeComponent();
            if(client != null) _currentClient = client;
            DataContext = _currentClient;
            cmbGender.ItemsSource = RuleeEntities1.GetContext().Gender.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var errors = new StringBuilder();

                if (cmbGender.SelectedItem == null) errors.AppendLine("Не выбран пол");

                if (dpBirthDate.SelectedDate == null) errors.AppendLine("Не выбрана дата");

                if (String.IsNullOrEmpty(txtSurname.Text)) errors.AppendLine("Пустое занчение фамилии");
                if (String.IsNullOrEmpty(txtName.Text)) errors.AppendLine("Пустое занчение имени");
                if (String.IsNullOrEmpty(txtPhone.Text)) errors.AppendLine("Пустое занчение телефона");

                if(!ValidateFIO(txtName.Text) ) { errors.AppendLine("Неправильно введена Фамилия"); }
                if(!ValidateFIO(txtSurname.Text) ) { errors.AppendLine("Неправильно введено Имя"); }

                if(errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                _currentClient.Birthday = (DateTime)dpBirthDate.SelectedDate;
                _currentClient.GenderCode = (cmbGender.SelectedIndex + 1).ToString();
                if (_currentClient.ID != 0)
                {
                    RuleeEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Изменено");
                }
                else
                {
                    _currentClient.RegistrationDate = DateTime.Now;
                    RuleeEntities1.GetContext().Client.Add(_currentClient);
                    RuleeEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Добавлено");
                }
                NavigationService.GoBack();
            }
            catch (Exception ) { return; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_currentClient.ClientService.Count > 0)
            {
                MessageBox.Show("Невозможно удалить, так как у клиента есть информация о посещении", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                RuleeEntities1.GetContext().Client.Remove(_currentClient);
                RuleeEntities1.GetContext().SaveChanges();
                MessageBox.Show("Удалено");
                NavigationService.GoBack();
            }
            
        }

        private bool ValidateFIO(string s)
        {
            Regex regex = new Regex("[^a-zа-я -]+");
            return regex.IsMatch(s);
        }
    }
    
}

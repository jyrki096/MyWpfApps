using DesktopContactsApp.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            ReadDataBase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();
            ReadDataBase();
        }

        private void ReadDataBase()
        {  
            using (var conn = new SQLite.SQLiteConnection(App.DataBasePath))
            {
                conn.CreateTable<Contact>();
                contacts = conn.Table<Contact>().OrderBy(x => x.Name).ToList();
            }

            if (contacts != null)
                contactsListView.ItemsSource = contacts;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            contactsListView.ItemsSource = filteredList;
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = (Contact)contactsListView.SelectedItem;

            if (selectedContact != null)
            {
                var contactDetailsWindow = new ContactDetailsWindow(selectedContact);
                contactDetailsWindow.ShowDialog();
                ReadDataBase();
            }
            
        }
    }
}

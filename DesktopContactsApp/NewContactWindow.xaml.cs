using DesktopContactsApp.Classes;
using SQLite;
using System.Windows;


namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {

        public NewContactWindow()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newContact = new Contact()
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                PhoneNumber = phoneTextBox.Text
            };

            

            using (var connection = new SQLiteConnection(App.DataBasePath))
            {
                connection.CreateTable<Contact>();
                connection.Insert(newContact);
            }
            
            Close();
        }
    }
}

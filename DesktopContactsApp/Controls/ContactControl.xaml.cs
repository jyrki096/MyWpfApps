using DesktopContactsApp.Classes;
using System.Windows;
using System.Windows.Controls;


namespace DesktopContactsApp.Controls
{
    /// <summary>
    /// Interaction logic for Control.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        

        public ContactControl()
        {
            InitializeComponent();
        }



        public Contact Contact
        {
            get { return (Contact)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl), new PropertyMetadata(new Contact() { Name="Name Lastname", Email="test@test.ru", PhoneNumber="7(999)999-99-99"}, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactControl control = d as ContactControl;

            if (control != null)
            {
                control.nameTextBlock.Text = (e.NewValue as Contact).Name;
                control.emailTextBlock.Text = (e.NewValue as Contact).Email;
                control.phoneTextBlock.Text = (e.NewValue as Contact).PhoneNumber;
            }
        }
    }
}

﻿using DesktopContactsApp.Classes;
using SQLite;
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
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for ContactDetailsWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        Contact contact;
        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();
            this.contact = contact;
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            nameTextBox.Text = contact.Name;
            emailTextBox.Text = contact.Email;
            phoneTextBox.Text = contact.PhoneNumber;
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            contact.Name = nameTextBox.Text;
            contact.Email = emailTextBox.Text;
            contact.PhoneNumber = phoneTextBox.Text;

            using (var connection = new SQLiteConnection(App.DataBasePath))
            {
                connection.CreateTable<Contact>();
                connection.Update(contact);
            }
            Close();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SQLiteConnection(App.DataBasePath))
            {
                connection.CreateTable<Contact>();
                connection.Delete(contact);
            }
            Close();
        }
    }
}
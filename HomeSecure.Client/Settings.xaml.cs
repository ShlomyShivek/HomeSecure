using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HomeSecure.Client.Configuration;
using HomeSecure.Data.Entities;

namespace HomeSecure.Client
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private HomeSecureConfig _config;
        private EmailSettingsNotificatonDataWrapper _emailSettingsFromDb;

        public Settings()
        {
            InitializeComponent();
        }

        public Settings(HomeSecureConfig config)
            : this()
        {
            _config = config;

            NotificationEntityData notificationData = _config.GetNotificationsData().FirstOrDefault(a => a.Name == "Notify My Gmail");
            _emailSettingsFromDb = new EmailSettingsNotificatonDataWrapper(notificationData);

            txtPassword.Password = _emailSettingsFromDb.Password;
            txtConfirmPassword.Password = _emailSettingsFromDb.Password;

            DataContext = _emailSettingsFromDb;
        }

        private void butSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == txtConfirmPassword.Password)
            {
                _emailSettingsFromDb.Password = txtPassword.Password;
                _config.Save();
                this.Close();
            }
        }
    }
}

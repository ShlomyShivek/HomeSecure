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
using System.Collections.ObjectModel;
using HomeSecure.Data;
using HomeSecure.Data.Entities;

namespace HomeSecure.Client
{
    /// <summary>
    /// Interaction logic for EventLog.xaml
    /// </summary>
    public partial class EventLog : Window
    {
        private ObservableCollection<SecurityEvent> _securityEvents;

        public EventLog()
        {
            InitializeComponent();
            _securityEvents = new ObservableCollection<SecurityEvent>();
            dgSecurityEvents.ItemsSource = _securityEvents;
            dgSecurityEvents.AutoGenerateColumns = false;
        }

        public void AddSecurityEvent(SecurityEvent securityEvent)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                _securityEvents.Insert(0,securityEvent);
            }));
        }
    }
}

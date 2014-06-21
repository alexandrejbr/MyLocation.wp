using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace MyLocation
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
            this.gps_status.Checked += new EventHandler<RoutedEventArgs>(gps_status_Checked);
            this.gps_status.Unchecked += new EventHandler<RoutedEventArgs>(gps_status_Unchecked);
            this.Loaded += new RoutedEventHandler(Settings_Loaded);
            /*Verificar se já existem configurações em ficheiro, se existem carrega caso contrário começa como Inactivo*/
        }

        void Settings_Loaded(object sender, RoutedEventArgs args)
        {
            if (App.Configurations.CanAccessLocationData)
            {
                this.gps_status.IsChecked = true;
                this.gps_status.Content =  "Activo";
            }
            else
            {
                this.gps_status.IsChecked = false;
                this.gps_status.Content = "Inactivo";
            }
        }

        void gps_status_Unchecked(object sender, RoutedEventArgs e)
        {
            this.gps_status.Content = "Inactivo";
            App.Configurations.CanAccessLocationData = false;
            App.GeoCoordinateWatcher.Stop();
            //this.gps_status.SwitchForeground = new SolidColorBrush(Colors.Red);
        }

        void gps_status_Checked(object sender, RoutedEventArgs e)
        {
            this.gps_status.Content = "Activo";
            App.Configurations.CanAccessLocationData = true;
            App.GeoCoordinateWatcher.Start();
            //this.gps_status.SwitchForeground = new SolidColorBrush(Colors.Green);
        }
    }
}
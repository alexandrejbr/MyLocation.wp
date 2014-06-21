using System;
using System.Collections.Generic;
using System.Device.Location;
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
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //GeoCoordinateWatcher geoCoordinateWatcher =
            //    new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            App.GeoCoordinateWatcher.PositionChanged += OnGeoWatcherPositionChanged;
            if (App.Configurations != null && App.Configurations.CanAccessLocationData)
            {
                App.GeoCoordinateWatcher.Start();
            }
        }

        void MainPage_Loaded(object sender, RoutedEventArgs args)
        {
            if (!App.Configurations.CanAccessLocationData)
            {
                this.Value_latitude.Text = "?";
                this.Value_longitude.Text = "?";
                this.Value_altitude.Text = "?";
            }

        }

        void OnGeoWatcherPositionChanged(object sender,
            GeoPositionChangedEventArgs<GeoCoordinate> args)
        {
            this.Value_latitude.Text = args.Position.Location.Latitude.ToString();
            this.Value_longitude.Text = args.Position.Location.Longitude.ToString();
            this.Value_altitude.Text = args.Position.Location.Altitude.ToString();
        }

        private void ApplicationBarMenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show("Esta aplicação para funcionar correctamente necessita aos serviços de localização do telefone, certifique-se que estes estão activos. Em situações em que não exista cobertura GPS, ter o WiFi ligado poderá ajudar a localização de forma mais precisa.");
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

        private void PrivacyPolicyApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PrivacyPolicyPage.xaml", UriKind.Relative));
        }
    }
}
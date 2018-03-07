using RestSharp;
using VFCApplication.FTTACloud;
using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VFCApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        const string PATH = "C:/Users/asantana/Desktop/VFCApplication/VFCApplication/Credentials.txt";
        CloudAPI Api;

        List<Device> myDevices;
        public MainWindow()
        {
            InitializeComponent();

            // Login to cloud
            string ApiKey = File.ReadAllText(PATH);
            Api = new CloudAPI(ApiKey);
            Api.Login("andres.santana@lascarelectronics.com", "Lascarsedi12");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            try
            {
                List<Device> myDevices = Api.GetDeviceList();

                foreach (Device d in myDevices)
                {
                    DeviceList.Items.Add(d.name);
                }
            }
            catch (CloudAPI.CloudException ce)
            {
                string message = "Caught exception: " + ce.Message;
                Output.Text = message;
                
            }
            Output.UpdateLayout();
        }

    }
}

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
using System.Diagnostics;

namespace VFCApplication
{
    public delegate void SendResponse();
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class DeviceWindow : Window
    {
        public event EventHandler FormSubmitted;

        private bool ClickedX = true;

        const string PATH = "C:/Users/asantana/Desktop/VFCApplication/VFCApplication/resources/Credentials.txt";
        
        // Cloud API related data
        CloudAPI Api;
        private LoginData Login;

        public DeviceWindow()
        {
            InitializeComponent();

            // Initialize cloud API
            string ApiKey = File.ReadAllText(PATH);
            Api = new CloudAPI(ApiKey);

            // Initialize login components
            LoginWindow lw = new LoginWindow(Api);
            Login = new LoginData();


            lw.ShowDialog();
         }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try {
                foreach (Device d in Api.GetDeviceList())
                {
                    DeviceList.Items.Add(d.name);
                }
            }
            catch (CloudAPI.CloudException ce)
            {
                Debug.WriteLine("Caught exception: " + ce.Message);
            }
            catch (Exception ee)
            {
                Debug.WriteLine("caught exception" + ee.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ClickedX)
            {
                if (MessageBox.Show("Are you sure want to exit?",
                       "Audit Checker",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Information) == MessageBoxResult.OK)
                    Environment.Exit(1);
                else
                    e.Cancel = true;
            }
        }
    }
}

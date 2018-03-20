using RestSharp;
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
using Jarloo.Calendar;

namespace VFCAuditChecker
{
    public delegate void SendResponse();
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class DeviceWindow : Window
    {

        private bool ClickedX = true;

        const string PATH = "C:/Users/asantana/Desktop/VFCAuditChecker/VFCAuditChecker/resources/Credentials.txt";
        
        // Cloud API related data
        CloudAPI Api;

        // Map list position to GUID
        List<string> GUIDKeys = new List<string>();

        public DeviceWindow()
        {
            InitializeComponent();

            // Initialize cloud API
            string ApiKey = File.ReadAllText(PATH);
            Api = new CloudAPI(ApiKey);

            // Initialize login components
            //LoginWindow lw = new LoginWindow(Api);

            //lw.ShowDialog();
            Api.Login("andres.santana@lascarelectronics.com", "Lascarsedi12");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            { 
                foreach (Models.Device d in Api.GetDeviceList())
                {
                    bool status = Api.GetAuditCheckStatus(d.GUID);
                    string full = d.name + " - Audit check enabled: " + Convert.ToString(status);
                    DeviceList.Items.Add(full);
                    GUIDKeys.Add(d.GUID);
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

            DateTime targetDate = new DateTime(2018, 3, 1);

            Calendar.BuildCalendar(targetDate);

            
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


        private void RefreshCalendar()
        {
            int year = (int)2018;
            int month = 3;

            DateTime targetDate = new DateTime(year, month, 1);

            Calendar.BuildCalendar(targetDate);
        }

        private void Calendar_DayChanged(object sender, DayChangedEventArgs e)
        {
            // do stuff with calendar here
        }

        private void DeviceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AuditList.Items.Clear();
            List<Models.Session> sessions = Api.GetSessions(GUIDKeys[DeviceList.SelectedIndex]);
            List<DateTime> dates = new List<DateTime>();
            sessions.ForEach(x => {                
                dates = Api.GetAuditChecks(GUIDKeys[DeviceList.SelectedIndex], x.GUID);
            });
            dates.ForEach(y => {
                AuditList.Items.Add(y.ToLongDateString() + " - " + y.ToLongTimeString());
                Calendar.Mark(y, Jarloo.Calendar.Backdrop.YELLOW); 
            });

        }

        private void Arrow_Click(object sender, EventArgs e)
        {
            // Turning calendar invisible
            if (Calendar.IsVisible == true)
            {
                AuditLabel.Text = "Audit Checks";
                Calendar.Visibility = Visibility.Hidden;
                AuditList.Visibility = Visibility.Visible;
            }
            // Turning calendar visible
            else
            {
                AuditLabel.Text = "Calendar";
                Calendar.Visibility = Visibility.Visible;
                AuditList.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String filename = "report " + DateTime.Today.ToString() + ".csv";

            File.Create("report.txt");
        }
    }
}

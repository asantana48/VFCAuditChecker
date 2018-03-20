using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VFCAuditChecker
{ 
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {    
        // The cloud object, which we receive from somewhere else
        private CloudAPI cloud;

        private bool ClickedX = true;

        public LoginWindow(CloudAPI cloud)
        {
            InitializeComponent();
            UserInput.Focus();
            this.cloud = cloud;
            ErrorText.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Send login data to cloud
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = UserInput.Text;
            string password = PasswordInput.Password.ToString();

            if (cloud.Login(email, password) == true)
            {
                ClickedX = false;
                Close();
            }
            else
                ErrorText.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handle the focus between elements when Enter is pressed
        /// </summary>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (UserInput.IsFocused && UserInput.Text.Length != 0 && PasswordInput.Password.Length == 0)
                    PasswordInput.Focus();
                else
                    LoginButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        /// <summary>
        /// Hide error text whenever user starts typing again
        /// </summary>
        private void Inputs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                ErrorText.Visibility = Visibility.Hidden;
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

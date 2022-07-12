using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ATM_Simulator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            wrongLabel.Visibility = Visibility.Collapsed;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            int account = Int32.Parse(numberBox.Text);
            int password = Int32.Parse(pinBox.Text);
            if (Login(account, password))
            {
                Current.currentUser.SetID(account);
                this.Frame.Navigate(typeof(FunctionPage));
            }
            else
            {
                wrongLabel.Visibility = Visibility.Visible;
            }


        }

        public static Boolean Login(int account, int password)
        {
            MySqlConnection connection = Current.EstablishConnection();
            string query = "select * from account limit 10000";
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (Int32.Parse(reader[0].ToString()) == account && Int32.Parse(reader[0].ToString()) == password)
                    {
                        connection.Close();
                        return true;
                    }
                }
                
            }
            connection.Close();
            return false;
        }
    }
}

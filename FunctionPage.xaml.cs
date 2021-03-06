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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ATM_Simulator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FunctionPage : Page
    {
        public FunctionPage()
        {
            this.InitializeComponent();
        }

        private void withdrawButton_Click(object sender, RoutedEventArgs e)
        {
            Current.GetBalanceData();
            this.Frame.Navigate(typeof(WithdrawPage));
        }

        private void historyButton_Click(object sender, RoutedEventArgs e)
        {
            Current.GetTransactionData();
            this.Frame.Navigate(typeof(HistoryPage));
        }
    }
}

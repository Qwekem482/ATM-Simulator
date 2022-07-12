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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ATM_Simulator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WithdrawPage : Page
    {
        Dictionary<int, int> withdrawBanknotes = new Dictionary<int, int>()
        {
            {10000, 0},
            {20000, 0},
            {50000, 0},
            {100000, 0},
            {200000, 0},
            {500000, 0},
        };

        public WithdrawPage()
        {
            this.InitializeComponent();
            Current.GetBanknotesData();
            multipleLabel.Text = "The money withdraw must be multiple of " + MustMultiple() + "VND.";
            string withdrawText = "Your balance: " + Current.currentUser.GetBalance().ToString() + "VND.";
            withdrawLabel.Text = withdrawText;
            wrongBox.Visibility = Visibility.Collapsed;
            denominations.Visibility = Visibility.Collapsed;
            exitButton.Visibility = Visibility.Collapsed;
        }

        private int MustMultiple()
        {
            foreach(KeyValuePair<int, int> banknotes in Current.banknotes)
            {
                if (banknotes.Value != 0)
                {
                    return banknotes.Key;
                }
            }
            return 0;
        }

        private Boolean CheckMultiple(int amount)
        {
            if (amount % MustMultiple() == 0)
            {
                return true;
            }
            return false;
        }
        
        private Boolean CheckBalance(int amount)
        {
            if ((long) amount < Current.currentUser.GetBalance())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Withdraw(int amount)
        {
            Current.banknotes = Current.banknotes.Reverse().ToDictionary(x => x.Key, x => x.Value);
            foreach (int i in Current.banknotes.Keys.ToList())
            {
                while (amount >= i)
                {
                    if (Current.banknotes[i] > 0)
                    {
                        amount -= i;
                        Current.banknotes[i]--;
                        withdrawBanknotes[i] += 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Current.currentUser.SetBalance(Current.currentUser.GetBalance() - (long) amount);
        }
        
        private string Denomination()
        {
            string forReturn = "Your money\n";
            foreach (int i in withdrawBanknotes.Keys)
            {
                forReturn += i + "VND:\t" + withdrawBanknotes[i] + "\n";
            }
            return forReturn;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            int amount = Int32.Parse(moneyBox.Text);
            if (!(CheckMultiple(amount) || CheckBalance(amount)))
            {
                wrongBox.Visibility = Visibility.Visible;
            }
            else
            {
                Withdraw(amount);
                denominations.Visibility = Visibility.Visible;
                confirmButton.Visibility = Visibility.Collapsed;
                wrongBox.Visibility = Visibility.Collapsed;
                withdrawLabel.Visibility = Visibility.Collapsed;
                multipleLabel.Visibility = Visibility.Collapsed;
                moneyBox.Visibility = Visibility.Collapsed;
                exitButton.Visibility = Visibility.Visible;
                denominations.Text = Denomination();
                Current.SaveWithdrawData(amount);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}

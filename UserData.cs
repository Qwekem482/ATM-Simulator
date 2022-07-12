using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Simulator
{
    class UserData
    {
        private int id;
        private long balance;
        private SortedDictionary<DateTime, long> transaction = new SortedDictionary<DateTime, long>();

        public void SetID(int id)
        {
            this.id = id;
        }

        public void SetBalance(long balance)
        {
            this.balance = balance;
        }

        public void AddTransaction (long amount)
        {
            DateTime dateTime = DateTime.Now;
            if (transaction.Count == 5)
            {
                transaction.Remove(transaction.Keys.First());
                transaction.Add(dateTime, amount);
            }
            else
            {
                transaction.Add(dateTime, amount);
            }
        }

        public void AddTransaction(DateTime dateTime, long amount)
        {
            if (transaction.Count == 5)
            {
                transaction.Remove(transaction.Keys.First());
                transaction.Add(dateTime, amount);
            }
            else
            {
                transaction.Add(dateTime, amount);
            }
        }

        public int GetID()
        {
            return id;
        }

        public long GetBalance()
        {
            return balance;
        }

        public string GetTransaction()
        {
            string forReturn  = "";
            foreach (KeyValuePair<DateTime, long> entry in transaction)
            {
                forReturn = forReturn + entry.Key.ToString() + "\t" + entry.Value + "\n";
            }
            return forReturn;
        }

        public UserData()
        {
        }
    }
}

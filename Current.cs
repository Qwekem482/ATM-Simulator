using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ATM_Simulator
{
    static class Current
    {
        public static UserData currentUser = new UserData();
        public static Dictionary<int, int> banknotes = new Dictionary<int, int>();

        public static MySqlConnection EstablishConnection()
        {
            string connectionQuery = "datasource=localhost;Database=atm_simulator;port=3307;User Id=root;password=Qwekem4824802";
            MySqlConnection forReturn = new MySqlConnection(connectionQuery);
            return forReturn;
        }

        public static void GetBalanceData()
        {
            MySqlConnection connection = EstablishConnection();
            string query = "select balance from balance where id = " + currentUser.GetID();
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            currentUser.SetBalance(Int64.Parse(reader[0].ToString()));
            connection.Close();
        }
        
        public static void GetTransactionData()
        {
            MySqlConnection connection = EstablishConnection();
            string query = "select * from transaction where idAccount = " + currentUser.GetID();
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = (MySqlDataReader)command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    currentUser.AddTransaction(DateTime.Parse(reader[2].ToString()), Int64.Parse(reader[3].ToString()));
                }
            }
            connection.Close();
        }

        public static void GetBanknotesData()
        {
            MySqlConnection connection = EstablishConnection();
            string query = "select * from banknotes";
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    banknotes.Add(Int32.Parse(reader[0].ToString()), Int32.Parse(reader[1].ToString()));
                }
            }
            connection.Close();
        }

        public static void SaveWithdrawData(int amount)
        {
            MySqlConnection connection = EstablishConnection();

            string query = "update `atm_simulator`.`balance` set `balance` = '" + currentUser.GetBalance().ToString() + "' where (`id` = '" + currentUser.GetID() + "');";
            //UPDATE `atm_simulator`.`balance` SET `balance` = '77535775' WHERE (`id` = '0000000000000001');
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            connection.Close();

            query = "insert into transaction(idAccount,time,amount) values ('" + currentUser.GetID() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + amount + "');";
            connection.Open();
            command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();
            connection.Close();
        }

    }
}

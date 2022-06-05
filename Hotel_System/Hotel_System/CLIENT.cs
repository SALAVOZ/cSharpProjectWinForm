using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace Hotel_System
{
    internal class CLIENT
    {
        CONNECT conn = new CONNECT();
        public bool insertClient(String firstName, String lastName, String Phone, String Country)
        {
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO clients(firstName, lastName, phone, country) VALUES (@fName,@lName,@phone,@country)", conn.getConnection());
            command.Parameters.Add("@fName", NpgsqlDbType.Varchar).Value = firstName;
            command.Parameters.Add("@lName", NpgsqlDbType.Varchar).Value = lastName;
            command.Parameters.Add("@phone", NpgsqlDbType.Varchar).Value = Phone;
            command.Parameters.Add("@country", NpgsqlDbType.Varchar).Value = Country;

            conn.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }
        }
        public DataTable getClients()
        {
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM clients", conn.getConnection());
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter();
            DataTable dataTable = new DataTable();

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);

            return dataTable;
        }
    }
}

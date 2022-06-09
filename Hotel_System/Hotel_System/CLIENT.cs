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

        public bool editClient(Int32 Id, String firstName, String lastName, String Phone, String Country)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE clients set firstName=@fName, lastName=@lName, phone=@phone, country=@country WHERE id=@cid", conn.getConnection());
            command.Parameters.Add("@cid", NpgsqlDbType.Integer).Value = Id;
            command.Parameters.Add("@fName", NpgsqlDbType.Varchar).Value = firstName;
            command.Parameters.Add("@lName", NpgsqlDbType.Varchar).Value = lastName;
            command.Parameters.Add("@phone", NpgsqlDbType.Varchar).Value = Phone;
            command.Parameters.Add("@country", NpgsqlDbType.Varchar).Value = Country;

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
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

        public bool removeClient(Int32 id)
        {
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM clients WHERE id=@cid", conn.getConnection());
            command.Parameters.Add("@cid", NpgsqlDbType.Bigint).Value = id;

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

        public bool isClientExists(Int32 clientId)
        {
            NpgsqlCommand command = new NpgsqlCommand("select exists(select id from clients where id=@clientId)", conn.getConnection());
            command.Parameters.Add("@clientId", NpgsqlDbType.Integer).Value = clientId;
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;

            conn.openConnection();
            adapter.Fill(table);
            return bool.Parse(table.Rows[0][0].ToString());// get command exists result
        }
    }
}

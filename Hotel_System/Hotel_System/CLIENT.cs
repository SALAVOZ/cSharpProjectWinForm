﻿using Npgsql;
using NpgsqlTypes;

namespace Hotel_System
{
    internal class CLIENT
    {
        CONNECT conn = new CONNECT();
        public bool insertClient(String firstName, String lastName, String Phone, String Country)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            String insertQuery = "INSERT INTO clients(firstName, lastName, phone, country) VALUES (@fName,@lName,@phone,@country)";
            command.CommandText = insertQuery;
            command.Connection = conn.getConnection();

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
    }
}

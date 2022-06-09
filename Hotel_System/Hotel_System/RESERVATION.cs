using Npgsql;
using System.Data;
using NpgsqlTypes;

namespace Hotel_System
{
    internal class RESERVATION
    {
        CONNECT conn = new CONNECT();
        CLIENT client = new CLIENT();
        public DataTable getAllReservation()
        {
            NpgsqlCommand command = new NpgsqlCommand("SELECT roomnumber,clientnumber,datein,dateout,room_type,label FROM reservations JOIN rooms ON rooms.room_number=reservations.roomnumber JOIN rooms_category ON rooms_category.category_id=rooms.room_type", conn.getConnection());
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public bool addReservation(Int32 ClientId, Int32 RoomId, DateTime dateIn, DateTime dateOut)
        {
            bool result = client.isClientExists(ClientId);
            if (result)
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO reservations(roomnumber,clientnumber,datein, dateout) VALUES(@rNumber,@cNumber,@dIn,@dOut); UPDATE rooms SET free='NO' WHERE room_number=@rNumber;", conn.getConnection());
                command.Parameters.Add("@rNumber", NpgsqlDbType.Integer).Value = RoomId;
                command.Parameters.Add("@cNumber", NpgsqlDbType.Integer).Value = ClientId;
                command.Parameters.Add("@dIn", NpgsqlDbType.Date).Value = dateIn;
                command.Parameters.Add("@dOut", NpgsqlDbType.Date).Value = dateOut;

                conn.openConnection();

                if (command.ExecuteNonQuery() >= 1)
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
            else
            {
                return false;
            }
        }

        public bool editReservation(Int32 ClientId, Int32 RoomId, DateTime dateIn, DateTime dateOut)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE reservations SET datein=@dIn,dateout=@dOut WHERE roomnumber=@rNumber AND clientnumber=@cNumber", conn.getConnection());
            command.Parameters.Add("@rNumber", NpgsqlDbType.Integer).Value = RoomId;
            command.Parameters.Add("@cNumber", NpgsqlDbType.Integer).Value = ClientId;
            command.Parameters.Add("@dIn", NpgsqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@dOut", NpgsqlDbType.Date).Value = dateOut;

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

        public bool removeReservation(Int32 ClientId, Int32 RoomId)
        {
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM reservations WHERE roomnumber=@rNumber AND clientnumber=@cNumber;UPDATE ROOMS SET free='YES' WHERE room_number=@rNumber", conn.getConnection());
            command.Parameters.Add("@rNumber", NpgsqlDbType.Integer).Value = RoomId;
            command.Parameters.Add("@cNumber", NpgsqlDbType.Integer).Value = ClientId;

            conn.openConnection();

            if (command.ExecuteNonQuery() >= 1)
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

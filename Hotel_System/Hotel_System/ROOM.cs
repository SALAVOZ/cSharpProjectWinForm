using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace Hotel_System
{
    internal class ROOM
    {
        CONNECT conn = new CONNECT();

        public DataTable getAllRooms()
        {
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM rooms", conn.getConnection());
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            return dataTable;
        }

        public DataTable getRoomTypes()
        {
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM rooms_category", conn.getConnection());
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            
            adapter.Fill(dataTable);

            return dataTable;
        }

        public DataTable getRoomsByType(Int32 typeId)
        {
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM rooms WHERE room_type=@rType", conn.getConnection());
            command.Parameters.Add("@rType", NpgsqlDbType.Integer).Value = typeId;
            DataTable dataTable = new DataTable();
            conn.openConnection();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);
            return dataTable;
        }
        public bool addNewRoom(Int32 roomNumber, Int32 roomTypeId, String phone, String free)
        {
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO rooms(room_number, room_type, free, phone) VALUES(@rNumber,@rType,@free, @phone)", conn.getConnection());
            command.Parameters.Add("@rNumber", NpgsqlDbType.Integer).Value = roomNumber;
            command.Parameters.Add("@rType", NpgsqlDbType.Integer).Value = roomTypeId;
            command.Parameters.Add("@phone", NpgsqlDbType.Varchar).Value = phone;
            command.Parameters.Add("@free", NpgsqlDbType.Varchar).Value = free;

            conn.openConnection();
            try
            {
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
            catch (PostgresException exp)
            {
                var alal = exp.TableName;
                return false;
            }
        }

        public bool editRoom(Int32 roomNumber, Int32 roomTypeId, String phone, String free)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE rooms SET room_type=@rType, free=@free, phone=@phone WHERE room_number=@rNumber", conn.getConnection());
            command.Parameters.Add("@rType", NpgsqlDbType.Integer).Value = roomTypeId;
            command.Parameters.Add("@free", NpgsqlDbType.Varchar).Value = free;
            command.Parameters.Add("@phone", NpgsqlDbType.Varchar).Value = phone;
            command.Parameters.Add("@rNumber", NpgsqlDbType.Integer).Value = roomNumber;

            conn.openConnection();

            try
            {
                if(command.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(NpgsqlException exp)
            {
                return false;
            }
        }

        public bool removeRoom(Int32 roomNumber)
        {
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM rooms WHERE room_number=@rNumber", conn.getConnection());
            command.Parameters.Add("@rNumber", NpgsqlDbType.Integer).Value = roomNumber;

            conn.openConnection();

            try
            {
                if(command.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(NpgsqlException exp)
            {
                return false;
            }
        }

        //public bool isRoomFree(Int32 roomNumber)
        //{
        //    NpgsqlCommand command = new NpgsqlCommand("SELECT EXISTS(SELECT * from reservations WHERE roomnumber=@rNumber)", conn.getConnection());
        //    command.Parameters.Add("@rNumber", NpgsqlDbType.Integer).Value = roomNumber;
        //    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        //    DataTable table = new DataTable();
        //    adapter.SelectCommand = command;

        //    conn.openConnection();
        //    adapter.Fill(table);
        //    return bool.Parse(table.Rows[0][0].ToString());// get command exists result
        //}
    }
}

using Npgsql;
using System.Data;
using NpgsqlTypes;

namespace Hotel_System
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            CONNECT conn = new CONNECT();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            NpgsqlCommand command = new NpgsqlCommand();
            String query = "SELECT * FROM users WHERE username=@usn AND password=@pass";

            command.CommandText = query;
            command.Connection = conn.getConnection();

            command.Parameters.Add("@usn", NpgsqlDbType.Varchar).Value = textBoxUsername.Text;
            command.Parameters.Add("@pass", NpgsqlDbType.Varchar).Value = textBoxPassword.Text;

            adapter.SelectCommand = command;
            adapter.Fill(dt);
        
            if(dt.Rows.Count > 0)
            {
                this.Hide();
                Main_From
                MessageBox.Show("YES");
            }
            else
            {
                MessageBox.Show("NO");
            }
        }
    }
}

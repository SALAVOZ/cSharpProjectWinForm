using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class ManageClientsForm : Form
    {
        CLIENT client = new CLIENT();
        public ManageClientsForm()
        {
            InitializeComponent();
        }

        private void buttonClearFields_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhone.Text = "";
            textBoxCountry.Text = "";
        }

        private void buttonAddNewClient_Click(object sender, EventArgs e)
        {
            String fName = textBoxFirstName.Text;
            String lName = textBoxLastName.Text;
            String Phone = textBoxPhone.Text;
            String Country = textBoxCountry.Text;

            if (fName.Trim().Equals("") || lName.Trim().Equals("") || Phone.Trim().Equals("") || Country.Trim().Equals(""))
            {
                MessageBox.Show("Error - Empty Fields", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Boolean result = client.insertClient(fName, lName, Phone, Country);
                if (result)
                {
                    dataGridView1.DataSource = client.getClients();
                    MessageBox.Show("The Client Is Added Successfully", "Client Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error - The Client Is Not Added", "Inserting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ManageClientsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = client.getClients();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var parsingResult = int.TryParse(textBoxID.Text, out int id);
            if (parsingResult)
            {
                String fName = textBoxFirstName.Text;
                String lName = textBoxLastName.Text;
                String Phone = textBoxPhone.Text;
                String Country = textBoxCountry.Text;


                if (textBoxID.Text.Trim().Equals("") || fName.Trim().Equals("") || lName.Trim().Equals("") || Phone.Trim().Equals("") || Country.Trim().Equals(""))
                {
                    MessageBox.Show("Error - Empty Fields", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean result = client.editClient(id, fName, lName, Phone, Country);
                    if (result)
                    {
                        dataGridView1.DataSource = client.getClients();
                        MessageBox.Show("The Client Is Edited Successfully", "Client Edited", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error - The Client Is Not Edited", "Editing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error - Wrong Id", "Id Error Parsing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxCountry.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var parsingResult = int.TryParse(textBoxID.Text, out int id);
            if(parsingResult)
            {
                Boolean result = client.removeClient(id);
                if (result)
                {
                    buttonClearFields.PerformClick();
                    dataGridView1.DataSource = client.getClients();
                    MessageBox.Show("The Client Is Deleted Successfully", "Client Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("The Client Is Not Deleted", "Client Is Not Deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error - Wrong Id", "Id Error Parsing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

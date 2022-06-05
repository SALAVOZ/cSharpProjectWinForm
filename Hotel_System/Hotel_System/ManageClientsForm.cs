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
                    MessageBox.Show("The Client Is Added Successfully", "Client Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error - The Client Is Not Added", "Inserting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

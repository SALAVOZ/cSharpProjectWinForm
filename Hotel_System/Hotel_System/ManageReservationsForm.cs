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
    public partial class ManageReservationsForm : Form
    {
        RESERVATION reserv = new RESERVATION();
        ROOM room = new ROOM();
        public ManageReservationsForm()
        {
            InitializeComponent();
        }

        private void ManageReservationsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = reserv.getAllReservation();
            comboBoxRoomType.DataSource = room.getRoomTypes();
            comboBoxRoomType.DisplayMember = "label";
            comboBoxRoomType.ValueMember = "category_id";
            int typeId = int.Parse(comboBoxRoomType.SelectedValue.ToString());
            comboBoxRoomNumber.DataSource = room.getRoomsByType(typeId);
            comboBoxRoomNumber.DisplayMember = "room_number";
            comboBoxRoomNumber.ValueMember = "room_number";

        }

        private void comboBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int typeId = int.Parse(comboBoxRoomType.SelectedValue.ToString());
                comboBoxRoomNumber.DataSource = room.getRoomsByType(typeId);
                comboBoxRoomNumber.DisplayMember = "roomnumber";
                comboBoxRoomNumber.ValueMember = "roomnumber";
                comboBoxRoomType.DisplayMember = "label";
                comboBoxRoomType.ValueMember = "room_type";

            }
            catch (Exception exp)
            {

            }
        }

        private void buttonAddNewClient_Click(object sender, EventArgs e)
        {
            Int32 clientId = int.Parse(textBoxClientID.Text);
            Int32 roomId = int.Parse(comboBoxRoomNumber.SelectedValue.ToString());
            DateTime dateIn = dateTimePickerIN.Value;
            DateTime dateOut = dateTimePickerOUT.Value;
            bool result = reserv.addReservation(clientId, roomId, dateIn, dateOut);
            if (result)
            {
                dataGridView1.DataSource = reserv.getAllReservation();
                MessageBox.Show("The Reservation Is Added Successfully", "Reservation Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The Reservation Is Not Added", "Reservation Is Not Added", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Int32 clientId = int.Parse(textBoxClientID.Text);
            Int32 roomId = int.Parse(comboBoxRoomNumber.SelectedValue.ToString());
            DateTime dateIn = dateTimePickerIN.Value;
            DateTime dateOut = dateTimePickerOUT.Value;
            bool result = reserv.editReservation(clientId, roomId, dateIn, dateOut);
            if (result)
            {
                dataGridView1.DataSource = reserv.getAllReservation();
                MessageBox.Show("The Reservation Is Edited Successfully", "Reservation Edited", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The Reservation Is Not Edited", "Reservation Is Not Edited", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            Int32 clientId = int.Parse(textBoxClientID.Text);
            Int32 roomId = int.Parse(comboBoxRoomNumber.SelectedValue.ToString());
            bool result = reserv.removeReservation(clientId, roomId);
            if (result)
            {
                dataGridView1.DataSource = reserv.getAllReservation();
                MessageBox.Show("The Reservation Is Removed Successfully", "Reservation Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The Reservation Is Not Removed", "Reservation Is Not Removed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxClientID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();// CLIENT ID
            try
            {
                comboBoxRoomType.SelectedValue = dataGridView1.CurrentRow.Cells[4].Value;//ROOM TYPE
                comboBoxRoomNumber.SelectedValue = dataGridView1.CurrentRow.Cells[0].Value;//ROOM NUMBER

            }
            catch(Exception exp)
            {

            }
        }
    }
}

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
    public partial class ManageRoomsForm : Form
    {
        ROOM room = new ROOM();
        public ManageRoomsForm()
        {
            InitializeComponent();
        }

        private void ManageRoomsForm_Load(object sender, EventArgs e)
        {
            comboBoxRoomType.DataSource = room.getRoomTypes();
            comboBoxRoomType.DisplayMember = "label";
            comboBoxRoomType.ValueMember = "category_id";
            dataGridView1.DataSource = room.getAllRooms();

        }

        private void buttonAddNewClient_Click(object sender, EventArgs e)
        {
            bool parsingResult = int.TryParse(textBoxRoomNumber.Text, out int roomNumber);
            String free = radioButtonYES.Checked ? radioButtonYES.Text : radioButtonNO.Text;
            if (parsingResult)
            {
                int typeId = int.Parse(comboBoxRoomType.SelectedValue.ToString());
                bool result = room.addNewRoom(roomNumber, typeId, textBoxPhone.Text, free);
                if(result)
                {
                    dataGridView1.DataSource = room.getAllRooms();
                    MessageBox.Show("Room Added Successfully", "Room Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Room Not Added", "Room Adding Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            bool parsingResult = int.TryParse(textBoxRoomNumber.Text, out int roomNumber);
            String free = radioButtonYES.Checked ? radioButtonYES.Text : radioButtonNO.Text;
            if(parsingResult)
            {
                int typeId = int.Parse(comboBoxRoomType.SelectedValue.ToString());
                bool result = room.editRoom(roomNumber, typeId, textBoxPhone.Text, free);
                if (result)
                {
                    dataGridView1.DataSource = room.getAllRooms();
                    MessageBox.Show("Room Edited Successfully", "Room Edited", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Room Not Edited", "Room Editing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxRoomNumber.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBoxRoomType.SelectedValue = dataGridView1.CurrentRow.Cells[1].Value;
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
            if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "YES")
            {
                radioButtonYES.Checked = true;
            }
            else
            {
                radioButtonNO.Checked = true;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            bool parsingResult = int.TryParse(textBoxRoomNumber.Text, out int roomNumber);
            if (parsingResult)
            {
                int typeId = int.Parse(comboBoxRoomType.SelectedValue.ToString());
                bool result = room.removeRoom(roomNumber);
                if (result)
                {
                    dataGridView1.DataSource = room.getAllRooms();
                    MessageBox.Show("Room Removed Successfully", "Room Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Room Not Removed", "Room Removing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}

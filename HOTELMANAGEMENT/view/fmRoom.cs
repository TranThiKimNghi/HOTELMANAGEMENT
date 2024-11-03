using HOTEL.MODEL;
using HOTELMANAGEMENT.BUS.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HOTELMANAGEMENT.view
{
    public partial class fmRoom : Form
    {
        private readonly RoomService roomService = new RoomService();
        HotelContextDB dB = new HotelContextDB();
        public fmRoom()
        {
            InitializeComponent();
        }

        private void fmRoom_Load(object sender, EventArgs e)
        {
            UpdateRoomList();
            List<Room> rooms = dB.Rooms.ToList();
         
            LoadRoomType(rooms);
            LoadRoomStatus(rooms);
        }
        private void LoadRoomType( List<Room> rooms)
        {
            this.cmbRoomType.DataSource = rooms;
            this.cmbRoomType.DisplayMember = "RoomType";
            this.cmbRoomType.ValueMember = "RoomID";
        }
        private void LoadRoomStatus(List<Room> rooms)
        {
            this.cmbStatus.DataSource = rooms;
            this.cmbStatus.DisplayMember = "RoomStatus";
            this.cmbStatus.ValueMember = "RoomID";
        }
      
        private void BindGrid(List<Room> listRoom)
        {
            dgvRoom.Rows.Clear();
            foreach (var item in listRoom)
            {
                int index = dgvRoom.Rows.Add();
                dgvRoom.Rows[index].Cells["RoomID"].Value = item.RoomID;
                dgvRoom.Rows[index].Cells["RoomType"].Value = item.RoomType;
                dgvRoom.Rows[index].Cells["RoomStatus"].Value = item.RoomStatus;
                dgvRoom.Rows[index].Cells["Capatity"].Value = item.Capacity;
                dgvRoom.Rows[index].Cells["BasePrice"].Value = item.BasePrice;

            }
        }
       
        private void UpdateRoomList()
        {
            try
            {
                var listRoom = roomService.GetAll();

                BindGrid(listRoom);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var room = new Room
                {
                    RoomID = txtRoomID.Text,
                    RoomType = cmbRoomType.Text,
                    RoomStatus = cmbStatus.Text,
                    BasePrice = decimal.Parse(txtPrice.Text),
                    Capacity = int.Parse(cpcNumber.Text),
                };

                dB.Rooms.Add(room);
                dB.SaveChanges();
                UpdateRoomList();

                MessageBox.Show("Thêm mới dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomID.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.");
                return;
            }
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var room = roomService.GetRoomById(txtRoomID.Text);
                    if (room != null)
                    {
                        roomService.DeleteRoom(txtRoomID.Text);

                        UpdateRoomList();
                        MessageBox.Show("Xóa dữ liệu thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Khách hàng không tồn tại.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRoom.Rows[e.RowIndex];
                txtRoomID.Text = row.Cells["RoomID"].Value?.ToString();
                cmbRoomType.Text = row.Cells["RoomType"].Value?.ToString();
                cmbStatus.Text = row.Cells["RoomStatus"].Value?.ToString();
                cpcNumber.Text = row.Cells["Capatity"].Value?.ToString();
                txtPrice.Text = row.Cells["BasePrice"].Value?.ToString();


            }
        }

        private void dgvRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

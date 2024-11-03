using HOTEL.MODEL;
using HOTELMANAGEMENT.BUS;
using HOTELMANAGEMENT.BUS.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTELMANAGEMENT.view
{
    public partial class fmBookingDetails : Form
    {
        private readonly BookingService bookingService = new BookingService();
        private readonly BookingDetailSevice detail = new BookingDetailSevice();
        private readonly RoomService roomService = new RoomService();
        private readonly ServiceDetailService serviceDetailService = new ServiceDetailService();

        public int RoomID { get; private set; }

        public fmBookingDetails()
        {
            InitializeComponent();
        }

        private void fmBookingDetails_Load(object sender, EventArgs e)
        {
            try
            {
                HotelContextDB db = new HotelContextDB();
                List<Service> listService = db.Services.ToList();
                List<Customer> listcustomer = db.Customers.ToList();
                List<ServiceDetail> details = db.ServiceDetails.ToList();
                List<Room> room = db.Rooms.ToList();
                FillServernamecombobox(listService);
                BindGrid(listService);
                LoadRoomComboBoxes(room);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
        }

        private void LoadRoomComboBoxes(List<Room> rooms)
        {
            try
            {
                cmbRoomID.DataSource = rooms;
                cmbRoomID.DisplayMember = "RoomID";
                cmbRoomID.ValueMember = "RoomID";

                cmbRoomType.DataSource = rooms;
                cmbRoomType.DisplayMember = "RoomType";
                cmbRoomType.ValueMember = "RoomID";

                cmbRoomStatus.DataSource = rooms;
                cmbRoomStatus.DisplayMember = "RoomStatus";
                cmbRoomStatus.ValueMember = "RoomID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FillServernamecombobox(List<Service> listService)
        {
            this.cmbServiceName.DataSource = listService;
            this.cmbServiceName.DisplayMember = "ServiceName";
            this.cmbServiceName.ValueMember = "ServiceID";
        }

        private void BindGrid(List<Service> listService)
        {
            using (var db = new HotelContextDB())
            {
     
                foreach (var item in listService)
                {
                    var serviceDetails = db.ServiceDetails.Where(d => d.ServiceID == item.ServiceID).ToList();
                    foreach (var detail in serviceDetails)
                    {
                        int index = dgvBookingService.Rows.Add();
                        decimal serviceTotal = item.Price * detail.Quantity;
                        dgvBookingService.Rows[index].Cells[0].Value = item.ServiceName;
                        dgvBookingService.Rows[index].Cells[1].Value = item.Price;
                        dgvBookingService.Rows[index].Cells[2].Value = detail.Quantity;
                        dgvBookingService.Rows[index].Cells[3].Value = serviceTotal;
                       
                    }
                }
            }
        }

        private void dgvBookingService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBookingService.Rows[e.RowIndex];
                cmbServiceName.Text = row.Cells[0].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgvBookingService.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvBookingService.SelectedRows)
                {
                    dgvBookingService.Rows.RemoveAt(row.Index);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cmbServiceName.SelectedValue != null && int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
            {
                var selectedService = cmbServiceName.SelectedItem as Service;
                int index = dgvBookingService.Rows.Add();
                dgvBookingService.Rows[index].Cells[0].Value = selectedService.ServiceName;
                dgvBookingService.Rows[index].Cells[1].Value = selectedService.Price;
                dgvBookingService.Rows[index].Cells[2].Value = quantity;
                dgvBookingService.Rows[index].Cells[3].Value = selectedService.Price * quantity;
               
            }
            else
            {
                MessageBox.Show("Vui lòng chọn số lượng.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fmPayment fmPayment = new fmPayment();
            fmPayment.ShowDialog(); 
        }

        private void dgvBookingService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
}





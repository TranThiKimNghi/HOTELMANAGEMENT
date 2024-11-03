using HOTEL.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTELMANAGEMENT.view
{
    public partial class fmPayment : Form
    {
        private readonly HotelContextDB context = new HotelContextDB();
        public fmPayment()
        {
            InitializeComponent();
            context = new HotelContextDB();
        }

        private void fmPayment_Load(object sender, EventArgs e)
        {
            var room = context.Rooms.ToList();
            LoadRoomComboBoxes(room);

            var booking = context.Bookings.ToList();
            LoadBookingComboBoxes(booking);

            List<Payment> dsPayment = context.Payments.ToList();
            LoadDataPayment(dsPayment);

            ResetControls();
        }

        private void LoadRoomComboBoxes(List<Room> rooms)
        {
            try
            {
                cmbRoomID.DataSource = rooms;
                cmbRoomID.DisplayMember = "RoomID";
                cmbRoomID.ValueMember = "RoomID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBookingComboBoxes(List<Booking> bookings)
        {
            try
            {
                cmbBookingID.DataSource = bookings;
                cmbBookingID.DisplayMember = "BookingID";
                cmbBookingID.ValueMember = "BookingID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataPayment(List<Payment> dsPayment)
        {
            dgvPayment.Rows.Clear();
            foreach (var item in dsPayment)
            {
                int index = dgvPayment.Rows.Add();
                dgvPayment.Rows[index].Cells[0].Value = item.PaymentID;
                dgvPayment.Rows[index].Cells[1].Value = item.BookingID;
                dgvPayment.Rows[index].Cells[2].Value = item.PaymentDate;
                var serviceUsage = item.ServiceUsages.FirstOrDefault();
                if (serviceUsage != null)
                {
                    dgvPayment.Rows[index].Cells[3].Value = serviceUsage.Room.RoomID;
                    dgvPayment.Rows[index].Cells[4].Value = serviceUsage.TotalAmount;
                }
                else
                {
                    dgvPayment.Rows[index].Cells[3].Value = DBNull.Value;
                    dgvPayment.Rows[index].Cells[4].Value = 0;
                }
            }
        }

        private void dgvPayment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPayment.Rows[e.RowIndex];
                txtPaymentID.Text = row.Cells["PaymentID"].Value.ToString();
                cmbBookingID.Text = row.Cells["BookingID"].Value.ToString();
                cmbRoomID.Text = row.Cells["RoomID"].Value.ToString();
                dtpPaymentDate.Value = Convert.ToDateTime(row.Cells["PaymentDate"].Value);
                txtTotalAmount.Text = row.Cells["TotalAmount"].Value.ToString();
            }
        }
        
        private void ResetControls()
        {
            txtPaymentID.Clear();
            cmbRoomID.SelectedIndex = -1;
            cmbBookingID.SelectedIndex = -1;
            txtTotalAmount.Clear();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            fmPrint fmPrint = new fmPrint();
            fmPrint.ShowDialog();   
        }

        private void cmbRoomID_TextChanged(object sender, EventArgs e)
        {

        }

        //private void cmbRoomID_SelectedIndexChanged(object sender, EventArgs e)
        //{
            
        //    var selecteBookingID = cmbBookingID.SelectedItem.ToString();
   
        //    cmbRoomID.Items.Clear();
   
        //    using (var context = new HotelContextDB())
        //    {
                
        //        var roomid = context.Rooms.Where(b => b.RoomID == selecteBookingID).Select(b => b.RoomID).ToList();

              
        //        foreach (var room in roomid)
        //        {
        //            cmbRoomID.Items.Add(roomid.ToString());
        //        }
        //    }

            
        //    if (cmbRoomID.Items.Count > 0)
        //    {
        //        cmbRoomID.SelectedIndex = 0;
        //    }
        //}
    }
    
    
}

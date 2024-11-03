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
    public partial class fmBooking : Form
    {
        public fmBooking()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            fmRoom fmRoom = new fmRoom();
            fmRoom.ShowDialog();    
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            fmService fmService = new fmService();  
            fmService.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            fmCustomer fmCustomer = new fmCustomer();   
            fmCustomer.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            fmPayment fmPayment = new fmPayment();  
            fmPayment.ShowDialog();
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            fmBookingDetails fmBookingDetails = new fmBookingDetails();
            fmBookingDetails.ShowDialog();
        }

        private void fmBooking_Load(object sender, EventArgs e)
        {

        }
    }
}

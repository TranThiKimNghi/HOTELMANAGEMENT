using HOTELMANAGEMENT.BUS.model;
using HOTELMANAGEMENT.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTELMANAGEMENT
{
    public partial class fmUser : Form
    {
        public fmUser()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtname.Text;
            string password = txtpassword.Text;
          
            UserService userService = new UserService();

         
            bool isLoginSuccess = userService.Login(username, password);

            if (isLoginSuccess)
            {
                MessageBox.Show("Login successful!");
                fmBooking fm = new fmBooking();
                fm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");

            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát ứng dụng
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

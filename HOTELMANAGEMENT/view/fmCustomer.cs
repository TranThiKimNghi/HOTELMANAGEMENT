using HOTEL.MODEL;
using HOTELMANAGEMENT.BUS.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HOTELMANAGEMENT.view
{
    public partial class fmCustomer : Form
    {
        private readonly CustomerService customerService = new CustomerService();
        HotelContextDB dB = new HotelContextDB();
   
        public fmCustomer()
        {
            InitializeComponent();
            LoadAllCustomers();

        }

        private void fmCustomer_Load(object sender, EventArgs e)
        {

            UpdateCustomerList();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];
                txtCustomerID.Text = row.Cells["CustomerID"].Value?.ToString();
                txtName.Text = row.Cells["FullName"].Value?.ToString();
                txtCitizenID.Text = row.Cells["CitizenID"].Value?.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();

                
            }
        }
        private void BindGrid(List<Customer> listCustomer)
        {
            dgvCustomer.Rows.Clear();
            foreach (var item in listCustomer)
            {
                int index = dgvCustomer.Rows.Add();
                dgvCustomer.Rows[index].Cells["CustomerID"].Value = item.CustomerID;
                dgvCustomer.Rows[index].Cells["FullName"].Value = item.FullName;
                dgvCustomer.Rows[index].Cells["CitizenID"].Value = item.CitizenID;
                dgvCustomer.Rows[index].Cells["Phone"].Value = item.Phone;
            }
        }
        private void UpdateCustomerList()
        {
            try
            {
                var listCustomer = customerService.GetAll();

                BindGrid(listCustomer);

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
                var customer = new Customer
                {
                    CustomerID = txtCustomerID.Text,
                    FullName = txtName.Text,
                    CitizenID = txtCitizenID.Text,
                    Phone = txtPhone.Text
                };

                dB.Customers.Add(customer);
                dB.SaveChanges();
                UpdateCustomerList();

                MessageBox.Show("Thêm mới dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerID.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.");
                return;
            }
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var customer = customerService.GetCustomerById(txtCustomerID.Text);
                    if (customer != null)
                    {
                        customerService.DeleteCustomer(txtCustomerID.Text);

                        UpdateCustomerList();
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

        private void btnFix_Click(object sender, EventArgs e)
        {
            try
            {
                var customer = customerService.GetCustomerById(txtCustomerID.Text);
                if (customer != null)
                {
                    customer.CustomerID = txtCustomerID.Text;
                    customer.FullName = txtName.Text;
                    customer.CitizenID = txtCitizenID.Text;
                    customer.Phone = txtPhone.Text;

                    customerService.UpdateCustomeer(customer);

                    UpdateCustomerList();

                    MessageBox.Show("Sửa dữ liệu thành công!");
                }
                else
                {
                    MessageBox.Show("khách hàng không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadAllCustomers()
        {
            // Lấy tất cả dữ liệu từ bảng Customers
            var allCustomers = dB.Customers.ToList();
            dgvCustomer.DataSource = null; // Xóa dữ liệu cũ
            dgvCustomer.DataSource = allCustomers;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();

            // Kiểm tra nếu textbox không có giá trị nào
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Vui lòng nhập tên hoặc ID để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Tìm kiếm theo CustomerID hoặc CustomerName
            var result = dB.Customers
                           .Where(c => c.CustomerID.ToString() == searchValue ||
                                       c.FullName.Contains(searchValue))
                           .ToList();

            // Kiểm tra nếu không có kết quả
            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả nào phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Đổ dữ liệu vào DataGridView
            dgvCustomer.DataSource = null;  // Xóa dữ liệu cũ
            dgvCustomer.DataSource = result;

            // Hiển thị thông tin hàng đầu tiên lên các TextBox
            var firstCustomer = result.First();
            txtCustomerID.Text = firstCustomer.CustomerID.ToString();
            txtName.Text = firstCustomer.FullName;
            txtCitizenID.Text = firstCustomer.CitizenID;
            txtPhone.Text = firstCustomer.Phone;
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();
            List<Customer> dscustomer = dB.Customers.ToList();

            // Lọc danh sách theo CustomerID hoặc FullName
            var filteredList = dscustomer
                .Where(s => s.CustomerID.ToString().ToLower().Contains(searchTerm) ||
                            s.FullName.ToLower().Contains(searchTerm))
                .ToList();

            // Cập nhật dữ liệu đã lọc vào DataGridView
            dgvCustomer.DataSource = null; // Xóa dữ liệu cũ
            dgvCustomer.DataSource = filteredList;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            // Gọi phương thức để tải lại toàn bộ dữ liệu
            LoadAllCustomers();

            // Xóa các ô nhập thông tin
            txtCustomerID.Clear();
            txtName.Clear();
            txtCitizenID.Clear();
            txtPhone.Clear();
            txtSearch.Clear();
        }

        private void dgvCustomer_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
             
        }
    }
}




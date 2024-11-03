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

        private void dgvCustomer_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim(); 

            if (!string.IsNullOrEmpty(searchValue))
            {
                var searchResults = customerService.GetAll()
                    .Where(c => c.CustomerID.Contains(searchValue) ||
                                c.FullName.Contains(searchValue) ||
                                c.CitizenID.Contains(searchValue) ||
                                c.Phone.Contains(searchValue))
                    .ToList();

                if (searchResults.Count > 0)
                {
                   
                    BindGrid(searchResults);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng phù hợp.");
                    UpdateCustomerList(); 
                }
            }
            else
            {
               
                UpdateCustomerList();
            }
        }
    }
}




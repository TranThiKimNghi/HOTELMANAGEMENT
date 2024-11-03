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
    public partial class fmService : Form
    {
        private readonly serviceServic serviceService = new serviceServic();
        private readonly ServiceDetailService serviceDetail = new ServiceDetailService();
        private readonly HotelContextDB context = new HotelContextDB();
        public fmService()
        {
            InitializeComponent();
        }

        private void BindGrid(List<Service> listService)
        {
            dgvService.Rows.Clear();
            foreach (var item in listService)
            {
                int index = dgvService.Rows.Add();
                dgvService.Rows[index].Cells[0].Value = item.ServiceID;
                dgvService.Rows[index].Cells[1].Value = item.ServiceName;

                var serviceDetail = item.ServiceDetails.FirstOrDefault();
                if (serviceDetail != null)
                    dgvService.Rows[index].Cells[2].Value = serviceDetail.Quantity;
                else
                    dgvService.Rows[index].Cells[2].Value = 0;

                dgvService.Rows[index].Cells[3].Value = item.Price;
            }
        }
        private void UpdateServiceList()
        {
            try
            {
                var listService = serviceService.GetAll();
                var listServiceDetail = serviceDetail.GetAll();
                BindGrid(listService);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillServernamecombobox(List<Service> listService)
        {
            this.cmbServiceName.DataSource = listService;
            this.cmbServiceName.DisplayMember = "ServiceName";
            this.cmbServiceName.ValueMember = "ServiceID";
        }
        private void fmService_Load(object sender, EventArgs e)
        {
            UpdateServiceList();
            var listService = context.Services.ToList();
            FillServernamecombobox(listService);
        }

        private void dgvService_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvService.Rows[e.RowIndex];
                txtServiceID.Text = row.Cells[0].Value?.ToString();
                cmbServiceName.Text = row.Cells[1].Value?.ToString();
                txtQuantity.Text = row.Cells[2].Value?.ToString();
                txtPrice.Text = row.Cells[3].Value?.ToString();

            }
        }
        HotelContextDB dB = new HotelContextDB();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new Service
                {
                    ServiceID = txtServiceID.Text,
                    ServiceName = cmbServiceName.Text,
                    Price = int.Parse(txtPrice.Text),
                };
                dB.Services.Add(service);

                var serviceDetail = new ServiceDetail
                {
                    ServiceID = service.ServiceID,
                    Quantity = int.Parse(txtQuantity.Text)
                };


                dB.ServiceDetails.Add(serviceDetail);
                dB.SaveChanges();
                UpdateServiceList();

                MessageBox.Show("Thêm mới dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServiceID.Text))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa.");
                return;
            }
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var service = serviceService.GetServiceById(txtServiceID.Text);
                    if (service != null)
                    {
                        serviceService.DeleteService(txtServiceID.Text);

                        UpdateServiceList();
                        MessageBox.Show("Xóa dữ liệu thành công!");
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
    }
}

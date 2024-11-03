using HOTEL;
using HOTEL.MODEL;
using HOTELMANAGEMENT.BUS.model;
using Microsoft.Reporting.WinForms;
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
    public partial class fmPrint : Form
    {
        public fmPrint()
        {
            InitializeComponent();
        }

        private void fmPrint_Load(object sender, EventArgs e)
        {
            HotelContextDB context = new HotelContextDB();
            List<Payment> listPayment = context.Payments.ToList();
            List<Service> listService = context.Services.ToList();
            List<ServiceUsage> listServiceUsage = context.ServiceUsages.ToList();
            // List<ServiceDetail> listServiceDetail = context.ServiceDetails.ToList();
            List<PaymentReport> listReport = new List<PaymentReport>();

            foreach (var payment in listPayment)
            {
                var serviceUsagesForPayment = listServiceUsage.Where(su => su.PaymentID == payment.PaymentID).ToList();

                foreach (var serviceUsage in serviceUsagesForPayment)
                {

                    var service = listService.FirstOrDefault(s => s.ServiceID == serviceUsage.RoomID);


                    PaymentReport report = new PaymentReport
                    {
                        ServiceName = service?.ServiceName,
                        TotalService = serviceUsage.TotalService.ToString(),
                        TotalAmount = serviceUsage.TotalAmount.ToString()
                    };


                    var serviceDetailsForUsage = listServiceUsage.Where(sd => sd.ServiceUsageID == serviceUsage.ServiceUsageID).ToList();
                    foreach (var serviceDetail in serviceDetailsForUsage)
                    {
                        // Tạo một bản sao của báo cáo để thêm vào danh sách cho mỗi ServiceDetail
                        PaymentReport detailReport = new PaymentReport
                        {
                            ServiceName = report.ServiceName,
                            TotalService = report.TotalService,
                            TotalAmount = report.TotalAmount,
                         //    Quantity = serviceDetail.Quantity.ToString()
                        };

                        // Thêm báo cáo chi tiết vào danh sách
                        listReport.Add(detailReport);
                    }
                }
            }

            // Thiết lập báo cáo
            this.reportViewer1.LocalReport.ReportPath = "PaymentReport.rdlc";
            var reportDataSource = new ReportDataSource("PaymentDataSet", listReport);
            this.reportViewer1.LocalReport.DataSources.Clear(); 
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}


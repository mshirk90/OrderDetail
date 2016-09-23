using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using BusinessObjects;

namespace Invoice_Report
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void RenderReport(ReportViewer reportViewer,
                                  string filename,
                                  string datasetName,
                                  string reportName,
                                  DataTable dt)
        {
            reportViewer.LocalReport.ReportPath = Server.MapPath(reportName);
            ReportDataSource reportDataSource = new ReportDataSource(datasetName, dt);
            reportViewer.LocalReport.DataSources.Add(reportDataSource);

            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //Render
            renderedBytes = reportViewer.LocalReport.Render(
                reportType, deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-attachment", "attachment; filename =" + filename + "." + fileNameExtension);
            Response.BinaryWrite(renderedBytes);
            Response.End();
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            try
            {
                OrderDetailList orderDetails = new OrderDetailList();
                int intOrderId = Convert.ToInt32(txtOrderId.Text);
                orderDetails = orderDetails.GetByOrderId(intOrderId);
                dsInvoice ds = new dsInvoice();
                foreach(OrderDetail od in orderDetails.List)
                {
                    DataRow drOrderDetail = ds.dtInvoice.NewRow();
                    drOrderDetail["OrderId"] = od.OrderID;
                    drOrderDetail["ProductName"] = od.ProductName;
                    drOrderDetail["UnitPrice"] = od.UnitPrice;
                    drOrderDetail["Quantity"] = od.Quantity;
                    drOrderDetail["SubTotal"] = od.SubTotal;
                    drOrderDetail["DiscountSubtotal"] = od.DiscountSubTotal;
                    drOrderDetail["Discount"] = od.Discount;
                    ds.dtInvoice.Rows.Add(drOrderDetail);

                }
                RenderReport(rvInvoice, "Invoice", "dsOrderDetails", "Invoice.rdlc", ds.dtInvoice);
            }

            catch (Exception ex)
            {
                //add a label

            }
        }
    }
}
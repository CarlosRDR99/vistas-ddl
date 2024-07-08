using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
namespace MVC.Controllers
{
    public class ProductoReporteController : Controller
    {
        // GET: ProductoReporte
        public ActionResult GetAll()
        {
            //ML.Result result = BL.Product.GetAll();
            //ML.Product product = new ML.Product();
            //ReportViewer reportViewer = new ReportViewer();
            //reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.SizeToReportContent = true;
            //reportViewer.Width = Unit.Percentage(900);
            //reportViewer.Height = Unit.Percentage(900);
            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\ProductoGetAllReporte.rdlc";
            //reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", result.Objects));
            //ViewBag.ReportViewer = reportViewer;
            //if (result.Correct)
            //{
            //    product.Products = result.Objects;
            //}
            ML.Result result = BL.Product.GetAllSP();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\ProductSPReporte.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", result.Objects));
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
    }
}
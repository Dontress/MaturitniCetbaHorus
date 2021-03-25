using MaturitniCetba;
using MaturitniCetba.Models;
using MaturitniCetba.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace MaturitniCetba.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            byte[] pdfBytes;
            using (var stream = new MemoryStream())
            using (var wri = new PdfWriter(stream))
            using (var pdf = new PdfDocument(wri))
            using (var doc = new Document(pdf))
            {
                doc.Add(new Paragraph("Hello World!"));
                doc.Add(new Paragraph("Hello World!"));
                doc.Add(new Paragraph("Hello World!"));
                doc.Add(new Paragraph("Hello World!"));
                doc.Add(new Paragraph("Hello World!"));
                doc.Add(new Paragraph("Hello World!"));
                doc.Add(new Paragraph("Hello World!"));
                doc.Add(new Paragraph("Hello World!"));
                doc.Add(new Paragraph("Hello World!"));
                doc.Close();
                pdfBytes = stream.ToArray();
            }
            return new FileContentResult(pdfBytes, "application/pdf");
        }
    }
}

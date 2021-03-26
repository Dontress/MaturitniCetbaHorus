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
using System.Collections.Generic;
using iText.Kernel.Font;
using System.Text;
using iText.IO.Font.Constants;
using iText.IO.Font;
using System.Drawing;

namespace MaturitniCetba.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index(int idZaka)
        {

            ChosenBooksDAO vybraneKnihy = new();
            List<KnihaModel> knihy = vybraneKnihy.GetChosenBooks(idZaka);

            if (!(knihy.Count == 0))
            {
                GetUserByIDDAO nacistZaka = new();

                UserModel user = nacistZaka.GetUserByID(idZaka);
                String jmeno = user.UserJmeno + " " + user.UserPrijmeni;

                string date = DateTime.UtcNow.ToString("dd.MM.yyyy");

                byte[] pdfBytes;
                int count = 0;

                PdfFont font = PdfFontFactory.CreateFont(@"Font\arial.ttf", "Cp1250", true);

                using (var stream = new MemoryStream())
                using (var writer = new PdfWriter(stream))
                using (var pdf = new PdfDocument(writer))
                using (var document = new Document(pdf))
                {
                    document.Add(new Paragraph(""));

                    document.Add(new Paragraph("Seznam litererárních děl pro ústní zkoušku z českého jazyka").SetFont(font).SetPaddingLeft(55).SetFontSize(15).SetBold());

                    document.Add(new Paragraph("Jmeno: " + jmeno).SetFont(font).SetPaddingLeft(55));

                    document.Add(new Paragraph("Třída: " + user.UserTrida).SetFont(font).SetPaddingLeft(55));

                    document.Add(new Paragraph("Školní rok: 2020/2021").SetFont(font).SetPaddingLeft(55));

                    document.Add(new Paragraph(""));

                    document.Add(new Paragraph("Vybrané knihy:").SetFont(font).SetFontSize(12).SetBold().SetPaddingLeft(55));

                    foreach (var item in knihy)
                    {
                        count++;
                        document.Add(new Paragraph(count.ToString() + ". " + item.Nazev + " (" + item.AutorJmeno + ")").SetFont(font).SetPaddingLeft(55));
                    }

                    document.Add(new Paragraph(""));
                    document.Add(new Paragraph(""));
                    document.Add(new Paragraph("")); 
                    document.Add(new Paragraph(""));
                    document.Add(new Paragraph(""));
                     
                    document.Add(new Paragraph("Datum: " + date + "                                                                        Podpis: ............ " ).SetFont(font).SetPaddingLeft(55));


                    document.Close();

                    pdfBytes = stream.ToArray();
                }
                return new FileContentResult(pdfBytes, "application/pdf");
            }
            else
                return RedirectToAction("Index","SpravaZaku");
            
        }

    
    }
}

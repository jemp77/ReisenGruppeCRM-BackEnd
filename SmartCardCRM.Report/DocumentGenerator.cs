using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Reporting.NETCore;
using SmartCardCRM.Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;

namespace SmartCardCRM.Report
{
    public class DocumentGenerator
    {
        public byte[] GenerateExcel(string reportName, Dictionary<string, dynamic> dataSets)
        {
            byte[] reportArray;
            string reportPath = string.Format("{0}{1}.rdlc", AppDomain.CurrentDomain.BaseDirectory, reportName);
            Stream reportDefinition;
            using (var fs = new FileStream(reportPath, FileMode.Open))
            {
                reportDefinition = fs;
                LocalReport report = new LocalReport();
                report.LoadReportDefinition(reportDefinition);
                foreach (var item in dataSets)
                {
                    report.DataSources.Add(new ReportDataSource(item.Key, item.Value));
                }
                //report.ListRenderingExtensions(); //Get valid format list
                reportArray = report.Render("EXCELOPENXML");
            }

            return reportArray;
        }

        //[Obsolete]
        //public byte[] GenerateWord(string reportName, Dictionary<string, dynamic> dataSets)
        //{
        //    byte[] reportArray = Array.Empty<byte>();
        //    string reportPath = string.Format("{0}{1}.rdlc", AppDomain.CurrentDomain.BaseDirectory, reportName);
        //    Stream reportDefinition;
        //    using (var fs = new FileStream(reportPath, FileMode.Open))
        //    {
        //        reportDefinition = fs;
        //        LocalReport report = new LocalReport();
        //        report.LoadReportDefinition(reportDefinition);
        //        foreach (var item in dataSets)
        //        {
        //            report.DataSources.Add(new ReportDataSource(item.Key, item.Value));
        //        }
        //        //report.ListRenderingExtensions(); //Get valid format list
        //        var reportData = report.Render("WORDOPENXML");
        //        using (MemoryStream mem = new MemoryStream())
        //        {
        //            mem.Write(reportData, 0, (int)reportData.Length);
        //            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(mem, true))
        //            {
        //                var paragraphs = wordDocument.MainDocumentPart.Document.Body.Descendants<Paragraph>().Where(x => !string.IsNullOrEmpty(x.InnerText) && x.InnerText.Trim().Length > 100);
        //                foreach (var item in paragraphs)
        //                {
        //                    item.ParagraphProperties.Justification.Val = JustificationValues.LowKashida;
        //                }
        //            }
                    
        //            reportArray = mem.ToArray();
        //        }
        //    }

        //    return reportArray;
        //}

        public byte[] GenerateWord(ClientDTO client, ContractDTO contract)
        {
            MemoryStream ms = new MemoryStream();
            byte[] clientSignature = Array.Empty<byte>();
            byte[] coOwnerSignature = Array.Empty<byte>();
            using (FileStream file = new FileStream("./Documents/Contract.docx", FileMode.Open, FileAccess.Read))
            {
                file.CopyTo(ms);
            }

            using (WordprocessingDocument doc = WordprocessingDocument.Open(ms, true))
            {
                var document = doc.MainDocumentPart.Document;

                foreach (var text in document.Descendants<Text>())
                {
                    switch (text.Text)
                    {
                        case var t when text.Text.Contains("ContractNumber"):
                            text.Text = text.Text.Replace("ContractNumber", contract.ContractNumber);
                            break;
                        case var t when text.Text.Contains("Buyers"):
                            var coOwner = string.IsNullOrEmpty(client.CoOwnerName) ? "" : $" - {client.CoOwnerName} {client.CoOwnerLastName}";
                            text.Text = text.Text.Replace("Buyers", $"{client.Name} {client.LastName} {coOwner}");
                            break;
                        case var t when text.Text.Contains("ContractMembershipInWords"):
                            text.Text = text.Text.Replace("ContractMembershipInWords", contract.ContractMembershipInWords);
                            break;
                        case var t when text.Text.Contains("MembershipPrice"):
                            text.Text = text.Text.Replace("MembershipPrice", String.Format("${0:#,0}", contract.MembershipPrice));
                            break;
                        case var t when text.Text.Contains("ValueInWords"):
                            text.Text = text.Text.Replace("ValueInWords", contract.EntryPriceInWords);
                            break;
                        case var t when text.Text.Contains("EntryPrice"):
                            text.Text = text.Text.Replace("EntryPrice", String.Format("${0:#,0}", contract.EntryPrice));
                            break;
                        case var t when text.Text.Contains("LendInstallmentPriceInWords"):
                            text.Text = text.Text.Replace("LendInstallmentPriceInWords", contract.LendInstallmentPriceInWords);
                            break;
                        case var t when text.Text.Contains("LendInstallmentPrice"):
                            text.Text = text.Text.Replace("LendInstallmentPrice", contract.LendInstallmentPrice == null ? "$0" : String.Format("${0:#,0}", contract.LendInstallmentPrice));
                            break;
                        case var t when text.Text.Contains("LendInstallmentDate"):
                            var installmentDate = contract.LendValue <= 0 ? "" : $" a partir del {contract.LendInstallmentDate.Value:dd/MM/yyyy}";
                            text.Text = text.Text.Replace("LendInstallmentDate", installmentDate);
                            break;
                        case var t when text.Text.Contains("DurationYears"):
                            text.Text = text.Text.Replace("DurationYears", contract.DurationYears.ToString());
                            break;
                        case var t when text.Text.Contains("ContractBeneficiariesCountInWords"):
                            text.Text = text.Text.Replace("ContractBeneficiariesCountInWords", contract.ContractBeneficiariesCountInWords);
                            break;
                        case var t when text.Text.Contains("ContractBeneficiariesCount"):
                            text.Text = text.Text.Replace("ContractBeneficiariesCount", contract.ContractBeneficiariesCount.ToString());
                            break;
                        case var t when text.Text.Contains("CustomDateTime"):
                            text.Text = text.Text.Replace("CustomDateTime", contract.CustomDateTime);
                            break;
                        case var t when text.Text.Contains("ClientFullName"):
                            text.Text = text.Text.Replace("ClientFullName", $"{client.Name} {client.LastName}");
                            break;
                        case var t when text.Text.Contains("ClientFullDocumentType"):
                            var documentType = client.DocumentType.Equals("CC") ? "Cédula de Ciudadanía No:" : client.DocumentType.Equals("CE") ? "Cédula de Extrangeria No:" : "Pasaporte No:";
                            text.Text = text.Text.Replace("ClientFullDocumentType", documentType);
                            break;
                        case var t when text.Text.Contains("ClientDocumentType"):
                            text.Text = text.Text.Replace("ClientDocumentType", client.DocumentType);
                            break;
                        case var t when text.Text.Contains("ClientDocument"):
                            text.Text = text.Text.Replace("ClientDocument", client.DocumentNumber);
                            break;
                        case var t when text.Text.Contains("CoOwnerFullName1"):
                            var coOwnerFullName1 = string.IsNullOrEmpty(client.CoOwnerName) ? "" : $"Nombre: {client.CoOwnerName} {client.CoOwnerLastName}";
                            text.Text = text.Text.Replace("CoOwnerFullName1", coOwnerFullName1);
                            break;
                        case var t when text.Text.Contains("CoOwnerFullName2"):
                            var coOwnerFullName2 = string.IsNullOrEmpty(client.CoOwnerName) ? "" : $"{client.CoOwnerName} {client.CoOwnerLastName},";
                            text.Text = text.Text.Replace("CoOwnerFullName2", coOwnerFullName2);
                            break;
                        case var t when text.Text.Contains("CoOwnerFullDocumentType"):
                            var coOwnerDocumentType = string.IsNullOrEmpty(client.CoOwnerDocumentType) ? "" : client.CoOwnerDocumentType.Equals("CC") ? "Cédula de Ciudadanía No:" : client.DocumentType.Equals("CE") ? "Cédula de Extrangeria No:" : "Pasaporte No:";
                            text.Text = text.Text.Replace("CoOwnerFullDocumentType", coOwnerDocumentType);
                            break;
                        case var t when text.Text.Contains("CoOwnerDocumentType"):
                            text.Text = text.Text.Replace("CoOwnerDocumentType", string.IsNullOrEmpty(client.CoOwnerDocumentType) ? "" : client.CoOwnerDocumentType);
                            break;
                        case var t when text.Text.Contains("CoOwnerDocument"):
                            text.Text = text.Text.Replace("CoOwnerDocument", client.CoOwnerDocumentNumber ?? "");
                            break;
                        case var t when text.Text.Contains("ContractorCity"):
                            text.Text = text.Text.Replace("ContractorCity", client.City);
                            break;
                        case var t when text.Text.Contains("LinerName"):
                            text.Text = text.Text.Replace("LinerName", client.Linner);
                            break;
                        case var t when text.Text.Contains("CloserName"):
                            text.Text = text.Text.Replace("CloserName", client.Closer);
                            break;
                        case var t when text.Text.Contains("Observations"):
                            text.Text = text.Text.Replace("Observations", contract.Observations);
                            break;
                        case var t when text.Text.Contains("CoOwnerSignTitle1"):
                            text.Text = text.Text.Replace("CoOwnerSignTitle1", string.IsNullOrEmpty(client.CoOwnerName) ? "" : "EL COMPRADOR");
                            break;
                        case var t when text.Text.Contains("CoOwnerSignTitle2"):
                            text.Text = text.Text.Replace("CoOwnerSignTitle2", string.IsNullOrEmpty(client.CoOwnerName) ? "" : "COMPRADOR 2:");
                            break;
                        case var t when text.Text.Contains("SignPlace"):
                            text.Text = text.Text.Replace("SignPlace", string.IsNullOrEmpty(client.CoOwnerName) ? "" : "____________________________________________");
                            break;
                        default:
                            break;
                    }
                }
                
                if (!string.IsNullOrEmpty(contract.ContractorSignature))
                    clientSignature = Convert.FromBase64String(contract.ContractorSignature.Split(",")[1]);

                if (!string.IsNullOrEmpty(contract.CoOwnerSignature))
                    coOwnerSignature = Convert.FromBase64String(contract.CoOwnerSignature.Split(",")[1]);

                List<Drawing> drawings = document.Descendants<Drawing>().ToList();
                List<OpenXmlElement> drwdDeleteParts = new List<OpenXmlElement>();

                foreach (Drawing drawing in drawings)
                {
                    DocProperties dpr = drawing.Descendants<DocProperties>().FirstOrDefault();
                    if (dpr != null && dpr.Title == "ContractorSignature" && !string.IsNullOrEmpty(contract.ContractorSignature))
                    {
                        foreach (DocumentFormat.OpenXml.Drawing.Blip b in drawing.Descendants<DocumentFormat.OpenXml.Drawing.Blip>().ToList())
                        {
                            OpenXmlPart imagePart = doc.MainDocumentPart.GetPartById(b.Embed);
                            using (var writer = new BinaryWriter(imagePart.GetStream()))
                            {
                                writer.Write(clientSignature);
                            }
                        }
                    }

                    if (dpr != null && dpr.Title == "CoOwnerSignature")
                    {
                        if (string.IsNullOrEmpty(contract.CoOwnerSignature))
                        {
                            drwdDeleteParts.Add(drawing);
                        }
                        else
                        {
                            foreach (DocumentFormat.OpenXml.Drawing.Blip b in drawing.Descendants<DocumentFormat.OpenXml.Drawing.Blip>().ToList())
                            {
                                OpenXmlPart imagePart = doc.MainDocumentPart.GetPartById(b.Embed);
                                using (var writer = new BinaryWriter(imagePart.GetStream()))
                                {
                                    writer.Write(coOwnerSignature);
                                }
                            }
                        }
                    }
                }

                foreach (var d in drwdDeleteParts)
                {
                    d.Remove();
                }
            }

            return ms.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using OfficeOpenXml;

namespace DataBase
{
    internal class Sheet
    {
        internal static string path = Path.Combine("C:/Users/filip/source/repos/DataBase/DataBase/", "thrlist.xlsx");
        internal static string link = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        internal static Dictionary<string, Threat> threatDict = new Dictionary<string, Threat>();

        internal static bool Exists() { return File.Exists(path); }

        internal static void Upload()
        {
            if (!Exists())
            {
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(link, path);
            }
        }

        internal static void Delite()
        {
            if (Exists())
            {
                File.Delete(path);
            }
        }

        internal static void Parse()
        {
            FileInfo existingFile = new FileInfo(path);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                worksheet.DeleteRow(1);
                worksheet.DeleteRow(1);
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 1; row <= rowCount; row++)
                {
                    Threat threat = new Threat();

                    threat.Id = int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim());

                    threat.Name = worksheet.Cells[row, 2].Value.ToString().Trim();

                    threat.Description = worksheet.Cells[row, 3].Value.ToString();

                    threat.Source = worksheet.Cells[row, 4].Value.ToString();

                    threat.Aim = worksheet.Cells[row, 5].Value.ToString();

                    threat.PrivacyBreak = worksheet.Cells[row, 6].Value.ToString();

                    threat.IntegrityBreak = worksheet.Cells[row, 7].Value.ToString();

                    threat.AvailabilityBreak = worksheet.Cells[row, 8].Value.ToString();

                    worksheet.Cells[row, colCount - 1].Style.Numberformat.Format = "dd-MM-yyyy";
                    DateTime appearDT = DateTime.FromOADate(double.Parse(worksheet.Cells[row, colCount - 1].Value.ToString()));
                    threat.AppearDate = appearDT;

                    worksheet.Cells[row, colCount].Style.Numberformat.Format = "dd-MM-yyyy";
                    DateTime updateDT = DateTime.FromOADate(double.Parse(worksheet.Cells[row, colCount].Value.ToString()));
                    threat.LastUpdateDate = updateDT;

                    threatDict[threat.Name] = threat;
                }
            }

        }

        internal static void Update()
        {
            Delite();
            Upload();
            Parse();
        }

    }
}

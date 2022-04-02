using Core.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace GitHubExplorerApi.Services
{
    public class ExcelService<T> : IExcelService<T> where T : class
    {
        public string GetExcelFromObject(IReadOnlyList<T> objects)
        {

            string path = Directory.GetCurrentDirectory()+"\\DataExport.xls";

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook))
            {

                SheetData partSheetData = GenerateSheetdataForDetails(objects);

                WorkbookPart workbookPart = document.AddWorkbookPart();
                GenerateWorkbookPartContent(workbookPart);

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId1");
                GenerateWorksheetPartContent(worksheetPart, partSheetData);
            }
            FileInfo file = new FileInfo(path);
            if (file.Exists)       
                return file.FullName;
                
                return string.Empty;
        }

        private SheetData GenerateSheetdataForDetails(IReadOnlyList<T> data)
        {
            SheetData sheetData1 = new SheetData();
            sheetData1.Append(CreateHeaderRowForExcel(data.First()));

            foreach (T element in data)
            {
                Row partsRows = GenerateRowForChildPartDetail(element);
                sheetData1.Append(partsRows);
            }
            return sheetData1;
        }

        private void GenerateWorksheetPartContent(WorksheetPart worksheetPart1, SheetData sheetData1)
        {
            Worksheet worksheet1 = new Worksheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            worksheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            worksheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");
            SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1" };

            SheetViews sheetViews1 = new SheetViews();

            SheetView sheetView1 = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            Selection selection1 = new Selection() { ActiveCell = "A1", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1" } };

            sheetView1.Append(selection1);

            sheetViews1.Append(sheetView1);
            SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties() { DefaultRowHeight = 15D, DyDescent = 0.25D };

            PageMargins pageMargins1 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };
            worksheet1.Append(sheetDimension1);
            worksheet1.Append(sheetViews1);
            worksheet1.Append(sheetFormatProperties1);
            worksheet1.Append(sheetData1);
            worksheet1.Append(pageMargins1);
            worksheetPart1.Worksheet = worksheet1;
        }
        private void GenerateWorkbookPartContent(WorkbookPart workbookPart1)
        {
            Workbook workbook1 = new Workbook();
            Sheets sheets1 = new Sheets();
            Sheet sheet1 = new Sheet() { Name = "Sheet1", SheetId = (UInt32Value)1U, Id = "rId1" };
            sheets1.Append(sheet1);
            workbook1.Append(sheets1);
            workbookPart1.Workbook = workbook1;
        }
        
        protected virtual Row CreateHeaderRowForExcel(T objectExample)
        {
            List<string> headers = new List<string>();

            PropertyInfo[] props = objectExample.GetType().GetProperties();
            foreach (PropertyInfo property in props)
            {
                headers.Add(property.Name);
            }

            Row workRow = new Row();
            foreach (string header in headers)
            {
                workRow.Append(CreateCell(header, 2U));

            }
            return workRow;
        }

        private Row GenerateRowForChildPartDetail(T objectToMap)
        {
            Row tRow = new Row();
            PropertyInfo[] props = objectToMap.GetType().GetProperties();
            foreach (PropertyInfo property in props)
            {
                string cellValue = property.GetValue(objectToMap, null)?.ToString();
                tRow.Append(CreateCell(cellValue ==null? "" : cellValue));
            }
            return tRow;
        }
        protected Cell CreateCell(string text)
        {
            Cell cell = new Cell();
            cell.StyleIndex = 1U;
            cell.DataType = ResolveCellDataTypeOnValue(text);
            cell.CellValue = new CellValue(text);
            return cell;
        }

        protected Cell CreateCell(string text, uint styleIndex)
        {
            Cell cell = new Cell();
            cell.StyleIndex = styleIndex;
            cell.DataType = ResolveCellDataTypeOnValue(text);
            cell.CellValue = new CellValue(text);
            return cell;
        }
        private EnumValue<CellValues> ResolveCellDataTypeOnValue(string text)
        {
            int intVal;
            double doubleVal;
            if (int.TryParse(text, out intVal) || double.TryParse(text, out doubleVal))
            {
                return CellValues.Number;
            }
            else
            {
                return CellValues.String;
            }
        }
        
    }
}
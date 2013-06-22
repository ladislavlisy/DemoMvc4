using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PayrollLibrary.Business.Core;
using PayrollLibrary.Business.CoreItems;
using System.IO;

namespace PayrollLibrary.Business.Export
{
    public class PayrollResultsPdfExporter : PayrollResultsExporter
    {
        class RoundedRectangleTableEvent : IPdfPTableEvent
        {
            public RoundedRectangleTableEvent()
            {
            }

            public void TableLayout(PdfPTable table, float[][] widths, float[] heights, int headerRows, int rowStart, PdfContentByte[] canvases)
            {
                PdfContentByte cb = canvases[PdfPTable.LINECANVAS];
                cb.SetColorStroke(new GrayColor(0.8f));
                float[] width0 = widths[0];
                float llx = width0[0];
                float lly = heights[0] + table.SpacingBefore;
                float urx = width0[width0.Length - 1];
                float ury = heights[heights.Length - 1];
                Rectangle rect = new Rectangle(llx, lly, urx, ury);
                cb.RoundRectangle(rect.Left, rect.Bottom, rect.Width, rect.Height, 4);
                cb.Stroke();
            }
        }
        public class ItemToAdd
        {
            public ItemToAdd(int page, int x, int y, int rotate, int charspace, int size, int height, int font, int align, string text)
            {
                this.text = text;
                this.pageNumber = page;
                this.x = x / 10.0f;
                this.y = y / 10.0f;
                this.size = size / 10.0f;
                this.height = height / 10.0f;
                this.fontSize = font;
                this.rotate = rotate / 10.0f;
                this.charSpace = charspace / 10.0f;
                this.textAlign = align;
            }

            # region Properties

            private int pageNumber;
            internal int PageNumber
            {
                get { return pageNumber; }
                private set { pageNumber = value; }
            }

            private float x;
            internal float X
            {
                get { return x; }
                private set { x = value; }
            }

            private float y;
            internal float Y
            {
                get { return y; }
                private set { y = value; }
            }

            private float rotate;
            internal float Rotate
            {
                get { return rotate; }
                private set { rotate = value; }
            }

            private float charSpace;
            internal float CharSpace
            {
                get { return charSpace; }
                private set { charSpace = value; }
            }

            private float size;
            internal float Size
            {
                get { return size; }
                private set { size = value; }
            }

            private float height;
            internal float Height
            {
                get { return height; }
                private set { height = value; }
            }

            private int fontSize;
            internal int FontSize
            {
                get { return fontSize; }
                private set { fontSize = value; }
            }

            private int textAlign;
            internal int TextAlign
            {
                get { return textAlign; }
                private set { textAlign = value; }
            }

            private string text;
            internal string Text
            {
                get { return text; }
                private set { text = value; }
            }

            # endregion

        }

        public PayrollResultsPdfExporter(string company, string department, string person, string personNumber, PayrollProcess payroll)
            : base(company, department, person, personNumber, payroll)
        {
        }

        float MillimetersToPoints(float millimeters)
        {
            return (millimeters / 25.4f * 72);
        }

        public bool ExportPdf(string fileNamePath)
        {
            const int PAGE_F1 = 1;

            const int FONT_TISK_HEAD = 12;
            const int FONT_TISK_TABS = 10;
            const int FONT_TISK_FINE =  8;

            const int FONT_HEGHT_HEAD = 50;
            const int FONT_HEGHT_TABS = 40;
            const int FONT_HEGHT_FINE = 30;

            try
            {

                // step 1: creation of a document-object			
                Document document = new Document(PageSize.A4, 0, 0, 150, 150);
                // step 2: we create a writer that listens to the document			
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileNamePath, FileMode.Create));
                // step 3: we open the document			
                document.Open();
                // Create a core font with a specified encoding
                BaseFont centralEuropeFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
                BaseFont centralEuropeFFix = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);

                document.NewPage();
                PdfContentByte cb = writer.DirectContent;

                Rectangle pageSizeWithRotation = PageSize.A4;

                BaseFont textFont = centralEuropeFont;
                BaseFont textFFix = centralEuropeFFix;
                string periodText = "Payroll period: " + PPeriod.Description();
                string detailsText = "Paycheck details for: " + EmployeeName;
                string titleHeader = "Happiness for every Payrollee";
                string titleFooter = "Notes";
                string paynoteText = "Paycheck details were calculated by Hrave Mzdy - simple online payroll agenda";
                string prepareText = "Prepared on " + DateTime.Now.ToString();

                ItemToAdd [] items = new ItemToAdd [] { 
                    new ItemToAdd(PAGE_F1, 100, 260, 0, 0, 800, FONT_HEGHT_FINE, FONT_TISK_FINE, Element.ALIGN_LEFT, periodText), 
                    new ItemToAdd(PAGE_F1, 150, 360, 0, 0, 800, FONT_HEGHT_HEAD, FONT_TISK_HEAD, Element.ALIGN_LEFT, detailsText),
                    new ItemToAdd(PAGE_F1, 150, 400, 0, 0, 800, FONT_HEGHT_FINE, FONT_TISK_FINE, Element.ALIGN_LEFT, titleHeader), 
                    new ItemToAdd(PAGE_F1, 150,2360, 0, 0, 800, FONT_HEGHT_HEAD, FONT_TISK_HEAD, Element.ALIGN_LEFT, titleFooter), 
                    new ItemToAdd(PAGE_F1, 150,2400, 0, 0, 800, FONT_HEGHT_FINE, FONT_TISK_FINE, Element.ALIGN_LEFT, paynoteText), 
                    new ItemToAdd(PAGE_F1, 150,2500, 0, 0, 800, FONT_HEGHT_FINE, FONT_TISK_FINE, Element.ALIGN_LEFT, prepareText)
                };

                DrawLabelItems(cb, pageSizeWithRotation, textFont, textFFix, items);

                Font fontTableNormal = new Font(centralEuropeFont, 10, Font.NORMAL);
                Font fontTableXSmall = new Font(centralEuropeFont,  8, Font.NORMAL);

                RoundedRectangleTableEvent tableRounded = new RoundedRectangleTableEvent();

                float[] tabWidths = new float[] { 1f, 1f };
                PdfPTable paycheckTable = new PdfPTable(tabWidths);
                paycheckTable.DefaultCell.Border = (PdfPCell.NO_BORDER);

                float[] hdrWidths = new float[] { 1f };
                PdfPTable hdrcheckTable = new PdfPTable(hdrWidths);
                hdrcheckTable.DefaultCell.Border = (PdfPCell.NO_BORDER);
                hdrcheckTable.SpacingAfter = (10f);

                PdfPTable paycheckHeader = CreateHeaderSection(fontTableNormal, fontTableXSmall, tableRounded);

                var column1Part1 = GetSourceEarningsExport();
                var column1Part2 = GetSourceTaxSourceExport();
                var column2Part1 = GetSourceTaxInsIncomeExport();
                var column2Part2 = GetSourceTaxInsResultExport();
                var columnIncome = GetSourceSummaryExport();

                var columnGross = columnIncome.ElementAt(0);
                var columnNetto = columnIncome.ElementAt(1);

                PdfPTable [] columns0 = CreateIncomeSummarySection(fontTableNormal, tableRounded, columnGross, columnNetto);

                PdfPTable[] columns1 = CreateDetailSourceSection(fontTableNormal, tableRounded, column1Part1, column1Part2);

                PdfPTable[] columns2 = CreateDetailSourceSection(fontTableNormal, tableRounded, column2Part1, column2Part2);

                hdrcheckTable.AddCell(paycheckHeader);
                //document.Add(hdrcheckTable);

                DrawTableToContent(cb, pageSizeWithRotation, hdrcheckTable, 150f, 600f, 1850f);

                paycheckTable.AddCell(columns0[0]);
                paycheckTable.AddCell(columns0[1]);
                paycheckTable.AddCell(columns1[0]);
                paycheckTable.AddCell(columns1[1]);
                paycheckTable.AddCell(columns2[0]);
                paycheckTable.AddCell(columns2[1]);

                //document.Add(paycheckTable);
                DrawTableToContent(cb, pageSizeWithRotation, paycheckTable, 150f, 850f, 1850f);

                // step 5: we close the document
                document.Close();
            }
            catch (InvalidOperationException ex)
            {
                string message = ex.Message;
                return false;
            }
            catch (SystemException ex)
            {
                string message = ex.Message;
                return false;
            }
            return true;
        }

        private void DrawTableToContent(PdfContentByte cb, Rectangle pageSizeWithRotation, PdfPTable table, float llx, float lly, float totalWidth)
        {
            float labelCoordX = pageSizeWithRotation.GetLeft(MillimetersToPoints(llx / 10.0f));
            float labelCoordY = pageSizeWithRotation.GetTop(MillimetersToPoints(lly / 10.0f));
            float labelCoordW = MillimetersToPoints(totalWidth / 10.0f);

            table.TotalWidth = labelCoordW;
            table.WriteSelectedRows(0, -1, labelCoordX, labelCoordY, cb);
        }

        private void DrawLabelItems(PdfContentByte cb, Rectangle pageSizeWithRotation, BaseFont textFont, BaseFont textFFix, ItemToAdd[] items)
        {
            foreach (var item in items)
            {
                DrawLabelToContent(cb, pageSizeWithRotation, textFont, textFFix, item);
            }
        }

        private void DrawLabelToContent(PdfContentByte cb, Rectangle pageSizeWithRotation, BaseFont textFont, BaseFont textFFix, ItemToAdd item)
        {
            float labelCoordX = pageSizeWithRotation.GetLeft(MillimetersToPoints(item.X));
            float labelCoordY = pageSizeWithRotation.GetTop(MillimetersToPoints(item.Y));
            float labelCoordS = MillimetersToPoints(item.Size);
            float labelCoordH = MillimetersToPoints(item.Height);

            int currTextSize = (item.FontSize % 200);
            BaseFont currTextFont = textFont;
            if (item.FontSize > 200)
            {
                currTextFont = textFFix;
            }
            AddLabelToContent(cb, item.Text, currTextFont, currTextSize, labelCoordX, labelCoordY, item.Rotate, item.CharSpace, item.TextAlign);
        }

        private PdfPTable[] CreateIncomeSummarySection(Font fontTableNormal, RoundedRectangleTableEvent tableRounded, IDictionary<string, string> columnGross, IDictionary<string, string> columnNetto)
        {
            float[] colWidths = new float[] { 3f, 1f };
            PdfPTable columnL = new PdfPTable(colWidths);
            PdfPTable columnR = new PdfPTable(colWidths);
            columnL.DefaultCell.Border = (PdfPCell.NO_BORDER);
            columnL.TableEvent = tableRounded;
            columnL.SpacingBefore = (10f);
            columnL.SpacingAfter = (10f);
            columnR.DefaultCell.Border = (PdfPCell.NO_BORDER);
            columnR.TableEvent = tableRounded;
            columnR.SpacingBefore = (10f);
            columnR.SpacingAfter = (10f);

            AddCellToTableTitle(columnL, columnGross["title"], fontTableNormal);
            AddCellToTableValue(columnL, columnGross["value"], fontTableNormal);

            AddCellToTableTitle(columnR, columnNetto["title"], fontTableNormal);
            AddCellToTableValue(columnR, columnNetto["value"], fontTableNormal);

            return new PdfPTable[] { columnL, columnR };
        }

        private PdfPTable[] CreateDetailSourceSection(Font fontTableNormal, RoundedRectangleTableEvent tableRounded, IDictionary<string, string>[] columnPart1, IDictionary<string, string>[] columnPart2)
        {
            float[] colWidths = new float[] { 3f, 1f };
            PdfPTable columnL = new PdfPTable(colWidths);
            PdfPTable columnR = new PdfPTable(colWidths);
            columnL.DefaultCell.Border = (PdfPCell.NO_BORDER);
            columnL.TableEvent = tableRounded;
            columnL.SpacingBefore = (10f);
            columnL.SpacingAfter = (10f);
            columnR.DefaultCell.Border = (PdfPCell.NO_BORDER);
            columnR.TableEvent = tableRounded;
            columnR.SpacingBefore = (10f);
            columnR.SpacingAfter = (10f);

            foreach (var columnItem in columnPart1)
            {
                AddCellToTableTitle(columnL, columnItem["title"], fontTableNormal);
                AddCellToTableValue(columnL, columnItem["value"], fontTableNormal);
            }
            foreach (var columnItem in columnPart2)
            {
                AddCellToTableTitle(columnR, columnItem["title"], fontTableNormal);
                AddCellToTableValue(columnR, columnItem["value"], fontTableNormal);
            }

            return new PdfPTable[] { columnL, columnR };
        }

        private PdfPTable CreateHeaderSection(Font fontTableNormal, Font fontTableXSmall, RoundedRectangleTableEvent tableRounded)
        {
            float[] headerWidths = new float[] { 1f, 3f, 1f };
            PdfPTable paycheckHeader = new PdfPTable(headerWidths);
            paycheckHeader.DefaultCell.Border = (PdfPCell.NO_BORDER);
            paycheckHeader.DefaultCell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            paycheckHeader.DefaultCell.NoWrap = true;
            paycheckHeader.TableEvent = tableRounded;

            AddTitleToHeader(paycheckHeader, "Personnel number", fontTableXSmall);
            AddTitleToHeader(paycheckHeader, "Person name", fontTableXSmall);
            AddTitleToHeader(paycheckHeader, "Period", fontTableXSmall);

            AddDataCellToHeader(paycheckHeader, EmployeeNumb, fontTableNormal);
            AddDataCellToHeader(paycheckHeader, EmployeeName, fontTableNormal);
            AddDataCellToHeader(paycheckHeader, PPeriod.Description(), fontTableNormal);

            AddTitleToHeader(paycheckHeader, "Department", fontTableXSmall);
            AddTitleToHeader(paycheckHeader, "Company", fontTableXSmall);
            AddTitleToHeader(paycheckHeader, "", fontTableXSmall);

            AddDataCellToHeader(paycheckHeader, EmployeeDept, fontTableNormal);
            AddDataCellToHeader(paycheckHeader, EmployerName, fontTableNormal);
            AddDataCellToHeader(paycheckHeader, "Paycheck", fontTableNormal);

            return paycheckHeader;
        }

        private void AddTitleToHeader(PdfPTable table, string phrase, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(phrase, font));
            cell.Border = (PdfPCell.NO_BORDER);
            cell.PaddingLeft = (5f);
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.NoWrap = true;
            table.AddCell(cell);
        }

        private void AddDataCellToHeader(PdfPTable table, string phrase, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(phrase, font));
            cell.Border = (PdfPCell.NO_BORDER);
            cell.PaddingLeft = (10f);
            cell.PaddingTop = (5f);
            cell.PaddingBottom = (5f);
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.NoWrap = true;
            table.AddCell(cell);
        }

        private void AddCellToTableTitle(PdfPTable table, string phrase, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(phrase, font));
            cell.Border = (PdfPCell.NO_BORDER);
            cell.PaddingLeft = (5f);
            cell.PaddingTop = (2f);
            cell.PaddingBottom = (2f);
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.NoWrap = true;
            table.AddCell(cell);
        }

        private void AddCellToTableValue(PdfPTable table, string phrase, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(phrase, font));
            cell.Border = (PdfPCell.NO_BORDER);
            cell.PaddingRight = (5f);
            cell.PaddingTop = (2f);
            cell.PaddingBottom = (2f);
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.NoWrap = true;
            table.AddCell(cell);
        }

        private void AddLabelToContent(PdfContentByte cb, string text, BaseFont bfFont, int nFontSize, float nTextBegX, float nTextBegY, float nRotate, float nCharSpace, int textAlign)
        {
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bfFont, nFontSize);
            cb.SetCharacterSpacing(nCharSpace);

            cb.BeginText();
            // put the alignment and coordinates here
            cb.ShowTextAligned(textAlign, text, nTextBegX, nTextBegY, nRotate);
            cb.EndText();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Aspose.Cells;
using System.IO;
using System.Web;
using System.Drawing;
using Helper.IO;
using log4net;

namespace Helper
{
    public class ExcelHelper
    {
        public static string ServerPath = "";
        public static string ServerHost = "";

        private Workbook book = null;
        private Worksheet sheet = null;
        public static readonly ILog Logger = LogManager.GetLogger(typeof(ExcelHelper));


        public string DataTableToExcel(DataTable dt, string outFileName, string[] hearArr, bool isOutLine)
        {

            int dvRowStart;

            if (dt.Rows.Count < 0)
            {
                return "";
            }

            int sheetRows = 65535;//设置Sheet的行数,此为最大上限,本来是65536,因表头要占去一行
            int sheetCount = (dt.Rows.Count - 1) / sheetRows + 1;//计算Sheet数

            try
            {
                book = new Workbook();
                book.Worksheets.Clear();
                //book.ConvertNumericData = false;
                for (int i = 0; i < sheetCount; i++)
                {
                    sheet = book.Worksheets.Add(i.ToString());
                    sheet = book.Worksheets[i];

                    //计算起始行
                    if (hearArr != null && hearArr.Length > 0)
                        AddHeader(hearArr);
                    else
                        AddHeader(dt);
                    dvRowStart = i * sheetRows;

                    for (int r = 1; r < (sheetRows - 1); r++)
                    {
                        if (dvRowStart > dt.Rows.Count - 1)
                        {
                            break;
                        }
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Columns[c].ColumnName.ToLower() == "ordercode" || dt.Columns[c].ColumnName.ToLower() == "订单号" || dt.Columns[c].ColumnName.ToLower() == "订单编码" || dt.Columns[c].ColumnName.ToLower() == "订单编号")
                            {
                                sheet.Cells[r, c].PutValue("\"" + dt.Rows[dvRowStart][c].ToString()+"\"");
                            }
                            if (dt.Columns[c].ColumnName.ToLower().IndexOf("数") > -1 || dt.Columns[c].ColumnName.ToLower().IndexOf("金币") > -1)
                            {
                                sheet.Cells[r, c].PutValue(dt.Rows[dvRowStart][c]);
                            }
                            else
                            {
                                sheet.Cells[r, c].PutValue(dt.Rows[dvRowStart][c].ToString());
                            }
                        }

                        dvRowStart++;
                    }

                }
                sheet.AutoFitColumns();

                #region 压缩
                string relativePath = "/UpLoadFile/excel/temp/";
                string path = ServerPath + "/UpLoadFile/excel/temp/";
                string fileName = outFileName + ".xls";
                string rarName = fileName.Replace(".xls", ".rar");
                string rarFileFullPath = path + rarName;
 
                relativePath = relativePath + rarName;
 
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                book.Save(path + fileName);
                RARHelper.WinRAR(path, rarName, fileName);
                File.Delete(path + fileName);


                return relativePath; //返回文件保存的路径

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public string GetDataTableToExcelByMemberReportPath(DataTable dt, string outFileName, string[] hearArr,
            int SyRegCount, int PhoneRegCount, int SiteRegCount)
        {
            string relativePath = "";
             

            int dvRowStart;

            if (dt.Rows.Count < 0)
            {
                return relativePath;
            }

            int sheetRows = 65535;//设置Sheet的行数,此为最大上限,本来是65536,因表头要占去一行
            int sheetCount = (dt.Rows.Count - 1) / sheetRows + 1;//计算Sheet数

            try
            {
                book = new Workbook();
                book.Worksheets.Clear();

                sheet = book.Worksheets.Add("统计信息");
                sheet = book.Worksheets["统计信息"];
                if (SiteRegCount != -1)
                {
                    sheet.Cells[0, 0].PutValue("网站注册");
                    sheet.Cells[0, 1].PutValue(SiteRegCount.ToString());
                }
                if (PhoneRegCount != -1)
                {
                    sheet.Cells[0, 2].PutValue("电话注册");
                    sheet.Cells[0, 3].PutValue(PhoneRegCount.ToString());
                }
                if (SyRegCount != -1)
                {
                    sheet.Cells[0, 4].PutValue("转入新系统会员");
                    sheet.Cells[0, 5].PutValue(SyRegCount.ToString());
                }
                int regSum = 0;
                if (SyRegCount != -1 || PhoneRegCount != -1 || SiteRegCount != -1)
                {
                    SyRegCount = SyRegCount == -1 ? 0 : SyRegCount;
                    PhoneRegCount = PhoneRegCount == -1 ? 0 : PhoneRegCount;
                    SiteRegCount = SiteRegCount == -1 ? 0 : SiteRegCount;

                    regSum = SiteRegCount + PhoneRegCount + SyRegCount;
                    sheet.Cells[0, 6].PutValue("共注册会员:" + regSum.ToString());
                }
                 

                for (int i = 1; i <= sheetCount; i++)
                {
                    sheet = book.Worksheets.Add(i.ToString());
                    sheet = book.Worksheets[i];
                    //计算起始行
                    if (hearArr != null && hearArr.Length > 0)
                        AddHeader(hearArr);
                    else
                        AddHeader(dt);
                    dvRowStart = (i - 1) * sheetRows;
                    
                    for (int r = 1; r < (sheetRows - 1); r++)
                    {
                        if (dvRowStart > dt.Rows.Count - 1)
                        {
                            break;
                        }
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            
                            if (dt.Columns[c].ColumnName.ToLower() == "ordercode" || dt.Columns[c].ColumnName.ToLower() == "订单号" || dt.Columns[c].ColumnName.ToLower() == "订单编码" || dt.Columns[c].ColumnName.ToLower() == "订单编号")
                            {
                                sheet.Cells[r, c].PutValue("\"" + dt.Rows[dvRowStart][c].ToString() + "\"");
                            }
                            if (dt.Columns[c].ColumnName.ToLower().IndexOf("数") > -1 || dt.Columns[c].ColumnName.ToLower().IndexOf("金币") > -1)
                            {
                                sheet.Cells[r, c].PutValue(dt.Rows[dvRowStart][c]);
                            }
                            else
                            {
                                sheet.Cells[r, c].PutValue(dt.Rows[dvRowStart][c].ToString());
                            }
                        }

                        dvRowStart++;
                    }

                }
                sheet.AutoFitColumns();
                
                Logger.Info("生成EXECL结束" + DateTime.Now.ToString());
                
                 relativePath = "/UpLoadFile/excel/temp/";
                string path = ServerPath + "/UpLoadFile/excel/temp/";
                string fileName = outFileName + ".xls";
                string rarName = fileName.Replace(".xls", ".rar");
                string rarFileFullPath = path + rarName;
                relativePath = relativePath + rarName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                book.Save(path + fileName);
                RARHelper.WinRAR(path, rarName, fileName);
                File.Delete(path + fileName);
                Logger.Info("压缩结束" + DateTime.Now.ToString());

             
            }
            catch (Exception ex)
            {
                throw ex;
             
            }
            finally
            {
                
            }

            return relativePath; //返回文件保存的路径
        }

        public string DataTableToExcelByMemberReport(DataTable dt, string outFileName, string[] hearArr,
            int SyRegCount, int PhoneRegCount, int SiteRegCount,bool isoutLine)
        {
            //定义循环中要使用的变量
            BinaryWriter bw = null;
            HttpResponse contextResponse = null;
            MemoryStream stream = null;

            //ICSharpCode.SharpZipLib.Zip.ZipOutputStream s = null;

            int dvRowStart;

            if (dt.Rows.Count < 0)
            {
                return "";
            }

            int sheetRows = 65535;//设置Sheet的行数,此为最大上限,本来是65536,因表头要占去一行
            int sheetCount = (dt.Rows.Count - 1) / sheetRows + 1;//计算Sheet数

            try
            {
                book = new Workbook();
                book.Worksheets.Clear();

                sheet = book.Worksheets.Add("统计信息");
                sheet = book.Worksheets["统计信息"];
                if (SiteRegCount != -1)
                {
                    sheet.Cells[0, 0].PutValue("网站注册");
                    sheet.Cells[0, 1].PutValue(SiteRegCount.ToString());
                }
                if (PhoneRegCount != -1)
                {
                    sheet.Cells[0, 2].PutValue("电话注册");
                    sheet.Cells[0, 3].PutValue(PhoneRegCount.ToString());
                }
                if (SyRegCount != -1)
                {
                    sheet.Cells[0, 4].PutValue("转入新系统会员");
                    sheet.Cells[0, 5].PutValue(SyRegCount.ToString());
                }
                int regSum = 0;
                if (SyRegCount != -1 || PhoneRegCount != -1 || SiteRegCount != -1)
                {
                    SyRegCount = SyRegCount == -1 ? 0 : SyRegCount;
                    PhoneRegCount = PhoneRegCount == -1 ? 0 : PhoneRegCount;
                    SiteRegCount = SiteRegCount == -1 ? 0 : SiteRegCount;

                    regSum = SiteRegCount + PhoneRegCount + SyRegCount;
                    sheet.Cells[0, 6].PutValue("共注册会员:" + regSum.ToString());
                }
                // sheet.Cells[0, 7].PutValue(regSum.ToString());
                for (int i = 1; i <= sheetCount; i++)
                {
                    sheet = book.Worksheets.Add(i.ToString());
                    sheet = book.Worksheets[i];
                    //计算起始行
                    if (hearArr != null && hearArr.Length > 0)
                        AddHeader(hearArr);
                    else
                        AddHeader(dt);
                    dvRowStart = (i - 1) * sheetRows;
                    //dvRowEnd = dvRowStart + sheetRows - 1;
                    //if (dvRowEnd > dt.Rows.Count - 1)
                    //{
                    //    dvRowEnd = dt.Rows.Count - 1;
                    //}

                    for (int r = 1; r < (sheetRows - 1); r++)
                    {
                        if (dvRowStart > dt.Rows.Count - 1)
                        {
                            break;
                        }
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            sheet.Cells[r, c].PutValue(dt.Rows[dvRowStart][c].ToString());
                        }

                        dvRowStart++;
                    }
                }
                sheet.AutoFitColumns();
                string relativePath = "/UpLoadFile/excel/temp/";
                string path = ServerPath + "/UpLoadFile/excel/temp/";
                string fileName = outFileName + ".xls";
                string rarName = fileName.Replace(".xls", ".rar");
                string rarFileFullPath = path + rarName;
                relativePath = relativePath + rarName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                book.Save(path + fileName);
                RARHelper.WinRAR(path, rarName, fileName);
                File.Delete(path + fileName);
                return relativePath;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return "";
            }
            finally
            {
                
            }
           
        }


        public bool DataTableToExcel(DataTable dt, string outFileName, string[] hearArr)
        {
            //定义循环中要使用的变量
            BinaryWriter bw = null;
            HttpResponse contextResponse = null;
            MemoryStream stream = null;

            //ICSharpCode.SharpZipLib.Zip.ZipOutputStream s = null;

            int dvRowStart;

            if (dt.Rows.Count < 0)
            {
                return false;
            }

            int sheetRows = 65535;//设置Sheet的行数,此为最大上限,本来是65536,因表头要占去一行
            int sheetCount = (dt.Rows.Count - 1) / sheetRows + 1;//计算Sheet数

            try
            {
                book = new Workbook();
                book.Worksheets.Clear();
                for (int i = 0; i < sheetCount; i++)
                {
                    sheet = book.Worksheets.Add(i.ToString());
                    sheet = book.Worksheets[i];
                    //计算起始行
                    if (hearArr != null && hearArr.Length > 0)
                        AddHeader(hearArr);
                    else
                        AddHeader(dt);
                    dvRowStart = i * sheetRows;
                    //dvRowEnd = dvRowStart + sheetRows - 1;
                    //if (dvRowEnd > dt.Rows.Count - 1)
                    //{
                    //    dvRowEnd = dt.Rows.Count - 1;
                    //}

                    for (int r = 1; r < (sheetRows - 1); r++)
                    {
                        if (dvRowStart > dt.Rows.Count - 1)
                        {
                            break;
                        }
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            //sheet.Cells[r, c].PutValue(dt.Rows[dvRowStart][c].ToString());
                            if (dt.Columns[c].ColumnName.ToLower() == "ordercode" || dt.Columns[c].ColumnName.ToLower() == "订单号" || dt.Columns[c].ColumnName.ToLower() == "订单编码" || dt.Columns[c].ColumnName.ToLower() == "订单编号")
                            {
                                sheet.Cells[r, c].PutValue("\"" + dt.Rows[dvRowStart][c].ToString() + "\"");
                            }
                            if (dt.Columns[c].ColumnName.ToLower().IndexOf("数") > -1 || dt.Columns[c].ColumnName.ToLower().IndexOf("金币") > -1)
                            {
                                sheet.Cells[r, c].PutValue(dt.Rows[dvRowStart][c]);
                            }
                            else
                            {
                                sheet.Cells[r, c].PutValue(dt.Rows[dvRowStart][c].ToString());
                            }
                        }

                        dvRowStart++;
                    }

                }
                sheet.AutoFitColumns();
                //stream = book.SaveToStream();

                #region 压缩
                string path = HttpContext.Current.Server.MapPath("/UpLoadFile/excel/temp/");
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xls";
                string rarName = fileName.Replace(".xls", ".rar");
                string rarFileFullPath = path + rarName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                book.Save(path + fileName);
                RARHelper.WinRAR(path, rarName, fileName);
                File.Delete(path + fileName);
                FileStream fileStream = new FileStream(rarFileFullPath, FileMode.Open);
                long fileSize = fileStream.Length;
                byte[] fileBuffer = new byte[fileSize];
                fileStream.Read(fileBuffer, 0, (int)fileSize);
                fileStream.Close();
                File.Delete(rarFileFullPath);
                #endregion


                contextResponse = HttpContext.Current.Response;
                contextResponse.Clear();
                contextResponse.Buffer = true;
                contextResponse.Charset = "GB2312"; //设置了类型为中文防止乱码的出现 
                contextResponse.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", "" + outFileName + ".rar")); //定义输出文件和文件名 

                contextResponse.ContentEncoding = Encoding.Default;
                //contextResponse.ContentType = "application/octet-stream"; //设置输出文件类型为excel文件。 

                //s = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(contextResponse.OutputStream);

                //ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry("" + outFileName + ".xls");
                //s.PutNextEntry(entry);


                bw = new BinaryWriter(contextResponse.OutputStream);
                bw.Write(fileBuffer);
            }
            catch (Exception ex)
            {
                throw ex;
                // return false;
            }
            finally
            {
                bw.Flush();
                bw.Close();
                //s.Flush();
                // s.Close();
                contextResponse.Flush();
            }
            return true;
        }
        
        public bool DataTableToExcelByMemberReport(DataTable dt, string outFileName, string[] hearArr,
            int SyRegCount, int PhoneRegCount, int SiteRegCount)
        {
            //定义循环中要使用的变量
            BinaryWriter bw = null;
            HttpResponse contextResponse = null;
            MemoryStream stream = null;

            //ICSharpCode.SharpZipLib.Zip.ZipOutputStream s = null;

            int dvRowStart;

            if (dt.Rows.Count < 0)
            {
                return false;
            }

            int sheetRows = 65535;//设置Sheet的行数,此为最大上限,本来是65536,因表头要占去一行
            int sheetCount = (dt.Rows.Count - 1) / sheetRows + 1;//计算Sheet数

            try
            {
                book = new Workbook();
                book.Worksheets.Clear();

                sheet = book.Worksheets.Add("统计信息");
                sheet = book.Worksheets["统计信息"];
                if (SiteRegCount != -1)
                {
                    sheet.Cells[0, 0].PutValue("网站注册");
                    sheet.Cells[0, 1].PutValue(SiteRegCount.ToString());
                }
                if (PhoneRegCount != -1)
                {
                    sheet.Cells[0, 2].PutValue("电话注册");
                    sheet.Cells[0, 3].PutValue(PhoneRegCount.ToString());
                }
                if (SyRegCount != -1)
                {
                    sheet.Cells[0, 4].PutValue("转入新系统会员");
                    sheet.Cells[0, 5].PutValue(SyRegCount.ToString());
                }
                int regSum = 0;
                if (SyRegCount != -1 || PhoneRegCount != -1 || SiteRegCount != -1)
                {
                    SyRegCount = SyRegCount == -1 ? 0 : SyRegCount;
                    PhoneRegCount = PhoneRegCount == -1 ? 0 : PhoneRegCount;
                    SiteRegCount = SiteRegCount == -1 ? 0 : SiteRegCount;

                    regSum = SiteRegCount + PhoneRegCount + SyRegCount;
                    sheet.Cells[0, 6].PutValue("共注册会员:" + regSum.ToString());
                }
                // sheet.Cells[0, 7].PutValue(regSum.ToString());
                for (int i = 1; i <= sheetCount; i++)
                {
                    sheet = book.Worksheets.Add(i.ToString());
                    sheet = book.Worksheets[i];
                    //计算起始行
                    if (hearArr != null && hearArr.Length > 0)
                        AddHeader(hearArr);
                    else
                        AddHeader(dt);
                    dvRowStart = (i - 1) * sheetRows;
                    //dvRowEnd = dvRowStart + sheetRows - 1;
                    //if (dvRowEnd > dt.Rows.Count - 1)
                    //{
                    //    dvRowEnd = dt.Rows.Count - 1;
                    //}

                    for (int r = 1; r < (sheetRows - 1); r++)
                    {
                        if (dvRowStart > dt.Rows.Count - 1)
                        {
                            break;
                        }
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            sheet.Cells[r, c].PutValue(dt.Rows[dvRowStart][c].ToString());
                        }

                        dvRowStart++;
                    }
                }
                sheet.AutoFitColumns();
                //stream = book.SaveToStream();
                Logger.Error("生成EXECL结束" + DateTime.Now.ToString());
                #region 压缩
                Logger.Error("压缩开始" + DateTime.Now.ToString());
                string path = HttpContext.Current.Server.MapPath("/UpLoadFile/excel/temp/");
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xls";
                string rarName = fileName.Replace(".xls", ".rar");
                string rarFileFullPath = path + rarName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                book.Save(path + fileName);
                RARHelper.WinRAR(path, rarName, fileName);
                File.Delete(path + fileName);
                FileStream fileStream = new FileStream(rarFileFullPath, FileMode.Open);
                long fileSize = fileStream.Length;
                byte[] fileBuffer = new byte[fileSize];
                fileStream.Read(fileBuffer, 0, (int)fileSize);
                fileStream.Close();
                File.Delete(rarFileFullPath);
                #endregion
                Logger.Error("压缩结束" + DateTime.Now.ToString());

                contextResponse = HttpContext.Current.Response;
                contextResponse.Clear();
                contextResponse.Buffer = true;
                contextResponse.Charset = "GB2312"; //设置了类型为中文防止乱码的出现 
                contextResponse.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", "" + outFileName + ".rar")); //定义输出文件和文件名 

                contextResponse.ContentEncoding = Encoding.Default;
                //contextResponse.ContentType = "application/octet-stream"; //设置输出文件类型为excel文件。 

                //s = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(contextResponse.OutputStream);

                //ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry("" + outFileName + ".xls");
                //s.PutNextEntry(entry);


                bw = new BinaryWriter(contextResponse.OutputStream);
                bw.Write(fileBuffer);
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
            finally
            {
                bw.Flush();
                bw.Close();
                //s.Flush();
                // s.Close();
                contextResponse.Flush();
            }
            return true;
        }

        private void AddHeader(DataTable dt)
        {
            Cell cell = null;
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                cell = sheet.Cells[0, col];
                cell.PutValue(dt.Columns[col].ColumnName);
                cell.Style.ForegroundColor = Color.Yellow;
                cell.Style.Pattern = BackgroundType.Solid;
                cell.Style.Font.Size = 10;
                cell.Style.Font.Color = Color.Black;

                cell.Style.Font.IsBold = true;
            }
        }

        private void AddHeader(string[] headerArr)
        {
            Cell cell = null;
            for (int col = 0; col < headerArr.Length; col++)
            {
                cell = sheet.Cells[0, col];
                cell.Style.ForegroundColor = System.Drawing.Color.Yellow;
                cell.PutValue(headerArr[col]);
                cell.Style.ForegroundColor = Color.Yellow;
                cell.Style.Pattern = BackgroundType.Solid;
                cell.Style.Font.Size = 10;
                cell.Style.Font.Color = Color.Black;

                cell.Style.Font.IsBold = true;
            }
        }

        private void AddBody(DataTable dt)
        {
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    sheet.Cells[r + 1, c].PutValue(dt.Rows[r][c].ToString());
                }
            }
        }


        #region Excel转换为DataSet
        /// <summary>
        /// 兼容64位系统
        /// </summary>
        /// <param name="excelFile"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string excelFile, string sheetName)
        {
            DataSet ds = new DataSet();
            Workbook workbook = new Workbook();
            workbook.Open(excelFile);
            Worksheets wsts = workbook.Worksheets;
            for (int i = 0; i < wsts.Count; i++)
            {
                Worksheet wst = wsts[i];
                int MaxR = wst.Cells.MaxRow;
                int MaxC = wst.Cells.MaxColumn;
                if (MaxR > 0 && MaxC > 0)
                {
                    DataTable dt = wst.Cells.ExportDataTableAsString(0, 0, MaxR + 1, MaxC + 1, true);
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }
        #endregion
    }
}

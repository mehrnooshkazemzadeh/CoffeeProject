using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Framework.Core.Tools
{
    public class Filecontroller : IFileController
    {
        private readonly IHostingEnvironment env;

        public Filecontroller(IHostingEnvironment env)
        {
            this.env = env;
        }
        public string GetFileName(string img)
        {
            var temp = img.Split("\\");
            var imageName = temp[temp.Length - 1];
            return imageName;
        }
        public IWorksheet GetExcellSheet(IFormFile fileName, int sheetNumber)
        {
            var excelEngine = new ExcelEngine();
            var filePath = UploadFile(fileName, "ExcelFiles");
            if (string.IsNullOrWhiteSpace(filePath))
                return null;
            IWorksheet sheet;
            using (var stream = File.OpenRead(filePath))
            {
                stream.Position = 0;
                var workbook = excelEngine.Excel.Workbooks.Open(stream);
                sheet = workbook.Worksheets[sheetNumber];
            }
            return sheet;
        }
        public IEnumerable<string> GetExcellColumns(IFormFile fileName, int sheetNumber)
        {
            var sheet = GetExcellSheet(fileName, sheetNumber);
            return sheet.Rows.Select(x => x.DisplayText).ToList();
        }

        public IEnumerable<string> GetRowData(IWorksheet worksheet, int columnIndex, string columnHeader = null, bool includeHeader = false)
        {
            if (worksheet == null)
            {
                return null;
            }
            return worksheet.Columns[columnIndex].Where(x => includeHeader || x.DisplayText != columnHeader).Select(x => x.DisplayText.TrimEnd().TrimStart()).ToList();
        }
        public string[] ProcessZipFile(string extractFolderName, IFormFile item)
        {
            var destinationFile = Path.Combine(env.WebRootPath, @$"Images\unzip\{extractFolderName}");
            if (!File.Exists(destinationFile))
                Directory.CreateDirectory(destinationFile);
            try
            {
                ZipFile.ExtractToDirectory(Path.Combine(env.WebRootPath, $@"Images\{item.FileName}"), destinationFile);
            }
            catch { }
            string[] imgFile;
            try
            {
                imgFile = Directory.GetFiles($@"{destinationFile}\\{extractFolderName}");
            }
            catch
            {
                imgFile = Directory.GetFiles($@"{destinationFile}");

            }

            return imgFile;
        }


        public string UploadFile(IFormFile file, string targetPath)
        {
            if (file == null) return null;
            if (string.IsNullOrWhiteSpace(file.FileName))
                return null;
            var uploadFolder = Path.Combine(env.WebRootPath, targetPath);
            if (!System.IO.File.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);
            var filePath = Path.Combine(uploadFolder, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return filePath;
        }
        public void Remove(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return;
            var path = env.WebRootPath + fileName.Replace("/", "\\");
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch { }
        }
    }
}

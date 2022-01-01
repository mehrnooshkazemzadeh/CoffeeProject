using Microsoft.AspNetCore.Http;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.Tools
{
    public interface IFileController
    {
        string UploadFile(IFormFile fileName, string targetPath);
        void Remove(string fileName);
        string GetFileName(string img);
        string[] ProcessZipFile(string extractFolderName, IFormFile item);
        IWorksheet GetExcellSheet(IFormFile fileName, int sheetNumber);
        IEnumerable<string> GetRowData(IWorksheet worksheet, int columnIndex, string columnHeader = null, bool includeHeader = false);
        IEnumerable<string> GetExcellColumns(IFormFile fileName, int sheetNumber);

    }
}

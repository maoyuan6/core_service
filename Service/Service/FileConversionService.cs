using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Aspose.Words;
using Aspose.Cells;
using SaveFormat = Aspose.Words.SaveFormat;

namespace Service.Service
{
    public class FileConversionService : IFileConversionService
    {
        public async Task<byte[]> ConvertToPdfAsync(IFormFile file)
        {
            (file == null || file.Length == 0).TrueThrowException("未获取到文件");

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            (fileExtension != ".doc" && fileExtension != ".docx" &&
                 fileExtension != ".xls" && fileExtension != ".xlsx").TrueThrowException("请上传word或者excel文件");
            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                byte[] pdfBytes = fileExtension switch
                {
                    ".doc" or ".docx" => ConvertWordToPdf(memoryStream),
                    ".xls" or ".xlsx" => ConvertExcelToPdf(memoryStream),
                    _ => throw new NotSupportedException("File format is not supported.")
                };
                return pdfBytes; 

            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        private byte[] ConvertWordToPdf(Stream wordStream)
        {
            var doc = new Aspose.Words.Document(wordStream);
            using var pdfStream = new MemoryStream();
            doc.Save(pdfStream, Aspose.Words.SaveFormat.Pdf);
            return pdfStream.ToArray();
        }

        private byte[] ConvertExcelToPdf(Stream excelStream)
        {
            var workbook = new Workbook(excelStream);
            using var pdfStream = new MemoryStream();
            workbook.Save(pdfStream, Aspose.Cells.SaveFormat.Pdf);
            return pdfStream.ToArray();
        }
    }
}

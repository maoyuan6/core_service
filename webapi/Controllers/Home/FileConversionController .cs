using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Webapi.Controllers.Base;

namespace Webapi.Controllers.Home
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Basic")]
    public class FileConversionController : BaseApiController
    {
        private readonly IFileConversionService _fileConversionService;
        public FileConversionController(IFileConversionService fileConversionService)
        {
            _fileConversionService = fileConversionService;
        }
        /// <summary>
        /// 文件转化成PDF
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> ConvertToPdf(IFormFile file)
        {
            var pdfBytes = await _fileConversionService.ConvertToPdfAsync(file); 
            return File(pdfBytes, "application/pdf", Path.GetFileNameWithoutExtension(file.FileName) + ".pdf");
        }
    }
}

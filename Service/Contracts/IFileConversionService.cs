using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Microsoft.AspNetCore.Http;

namespace Service.Contracts
{
    public interface IFileConversionService : IBaseService
    {
        Task<byte[]> ConvertToPdfAsync(IFormFile file);
    }
}

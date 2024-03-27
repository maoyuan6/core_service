using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository.JWT
{
    public interface ITokenHelper
    {
        string GetToken<T>(T user);
    }
}

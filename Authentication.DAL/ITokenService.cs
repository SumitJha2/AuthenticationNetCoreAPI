using Authentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.DAL
{
   public interface ITokenService
    {
        string GenerateToken(TokenParametersModel model,string userName);
        
    }
}

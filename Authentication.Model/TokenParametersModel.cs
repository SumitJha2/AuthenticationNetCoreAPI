using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Model
{
   public class TokenParametersModel
    {
        public string SecretKey { get; set; }
        public string IssuerKey { get; set; }
        public string AudenceKey { get; set; }
        public string ExperyTime { get; set; }

    }
}

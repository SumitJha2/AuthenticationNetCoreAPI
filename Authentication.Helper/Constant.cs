using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Helper
{
   public static class Constant
    {
        #region Encryption Key
        public static readonly string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        #endregion

        #region Message Strings
        public static readonly string UserNameEmpty = "UserName is Empty";
        public static readonly string PasswordEmpty = "Password is Empty";
        public static readonly string UnAuthorizedAccess = "UnAuthorized Access";
        public static readonly string UserNameString = "UserName";
        public static readonly string UserIDString = "UserID";
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExam.Common
{
    [Serializable]
    public class AdminLogin
    {
        public int AdminID { set; get; }
        public string username { set; get; }
    }
}
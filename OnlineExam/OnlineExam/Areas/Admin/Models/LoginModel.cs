using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExam.Areas.Admin.Models
{
    public class LoginModel
    {
        public string username { set; get; }
        public string password { set; get; }
        public bool RememberMe { set; get; }
    }
}
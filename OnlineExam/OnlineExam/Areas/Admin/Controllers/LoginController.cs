using Model.Dao;
using OnlineExam.Areas.Admin.Models;
using OnlineExam.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineExam.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login (LoginModel model)
        {
            var dao = new AdminDao();
            var result = dao.Login(model.username, Encryptor.MD5Hash(model.password));
            if(result)
            {
                var user = dao.GetByID(model.username);
                var user_session = new AdminLogin();
                user_session.AdminID = user.AdminID;
                Session.Add(CommonContanst.USER_SESSION,user_session);
                return RedirectToAction("Index", "Home");
            }else
            {
                ModelState.AddModelError("", "Đăng nhập không đúng");
            }
            return View("Index");
        }
    }
}
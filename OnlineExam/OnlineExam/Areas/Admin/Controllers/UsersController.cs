using Model.Dao;
using OnlineExam.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineExam.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            var imp = new UserDao();
            var model = imp.ListAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Model.EF.User user)
        {
            var mk = user.Password;
            var fullname = user.FullName;
            var role = user.Role;
            var status = user.Status;
            if (mk != null && fullname != null && role != null && status != null)
            {
                var dao = new UserDao();

                var encryptMD5 = Encryptor.MD5Hash(user.Password);
                user.Password = encryptMD5;

                var id = dao.Update(user);
                if (id)
                {
                    SetAlert("Update thành công!", "success");
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ModelState.AddModelError("", "Update không thành công!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ các trường!");
            }
            return View("Edit");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                new UserDao().Delete(id);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    message = e.Message,
                });
            }

        }
    }
}
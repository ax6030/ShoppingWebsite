using ShoppingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShoppingWebsite.Controllers
{
    public class HomeController : Controller
    {
        dbShoppingCarEntities db = new dbShoppingCarEntities();
        public ActionResult Index()
        {
            var products = db.table_Product.OrderByDescending(x => x.Id).ToList();
            return View(products);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(table_Member member)
        {
            //如果資料驗證未通過則回傳原本的View
            if (!ModelState.IsValid)
            {
                return View(member);
            }

            // 如果註冊的帳號不存在，才能執行新增與儲存
            var hadMember = db.table_Member.Where(m=>m.UserId == member.UserId).FirstOrDefault();

            if (hadMember == null)
            {
                db.table_Member.Add(member);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            ViewBag.Message = "帳號已被使用，請重新輸入";
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(table_Member member)
        {
            //找出符合登入帳號與密碼的 Member資料
            var hasMember = db.table_Member.Where(m=>m.UserId == member.UserId &&  m.Password == member.Password).FirstOrDefault();
            if (hasMember == null)
            {
                ViewBag.Message = "帳號或密碼錯誤，請確認後重新輸入";
                return View(member);
            }

            Session["Welcome"] =  $"{hasMember.Name},您好";

            FormsAuthentication.RedirectFromLoginPage(hasMember.UserId, true);

            return RedirectToAction("Index", "Member");
        }
    }
}
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWebsite.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult ShowContent(int id)
        {
            return Content($"測試路由顯示接收值 : id = {id}");
        }

        
    }
}
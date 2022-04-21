using CRUD5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD5.Controllers
{
    public class HomeController : Controller
    { 
        //建立資料庫連結
        dbcustomerEntities cdb = new dbcustomerEntities(); //客戶
        dbemployeeEntities edb = new dbemployeeEntities(); //廠商
        dbproductEntities pdb = new dbproductEntities();   //產品

        public ActionResult Index()  //進入首頁
        {
            return View();  //顯示頁面
        }
        /// <summary>
        /// 客戶資料
        /// </summary>
        public ActionResult customercreate()  //新增
        {
            return View();
        }
        [HttpPost]
        public ActionResult customercreate(tcustomer vCustomer)  //收客戶資料
        {
            cdb.tcustomer.Add(vCustomer);  //增加一筆
            cdb.SaveChanges();  //存取到資料庫
            return RedirectToAction("customersearch");
        }

        public ActionResult customeredit(int id)  //修改
        {  
            var customer = cdb.tcustomer.Where(m => m.CId == id).FirstOrDefault();  //篩選資料
            return View(customer);
        }
        [HttpPost]
        public ActionResult customeredit(tcustomer vCustomer)  //接收新的值
        {
            int id = vCustomer.CId;  //取得編號
            var customer = cdb.tcustomer.Where(m => m.CId == id).FirstOrDefault();  //抓取資料
            customer.CName = vCustomer.CName;  //取代舊的資料
            customer.CPhone = vCustomer.CPhone;
            customer.CAddress = vCustomer.CAddress;
            cdb.SaveChanges();
            return RedirectToAction("customersearch");
        }

        public ActionResult customerdelete(int id)  //刪除
        {
            var customer = cdb.tcustomer.Where(b => b.CId == id).FirstOrDefault();  //抓取id的那筆資料
            cdb.tcustomer.Remove(customer);  //刪除
            cdb.SaveChanges();  //存檔
            return RedirectToAction("customersearch");
        }

        public ActionResult customersearch(string searching)  //查詢
        {
            return View(cdb.tcustomer.Where(x => x.CName.Contains(searching) || searching == null).ToList());  //抓取GET到的值
        }

        /// <summary>
        /// 廠商資料
        /// </summary>
        public ActionResult employeecreate()  //新增
        {
            return View();
        }
        [HttpPost]
        public ActionResult employeecreate(temployee vEmployee)
        {
            edb.temployee.Add(vEmployee);  //增加一筆
            edb.SaveChanges();  //存取到資料庫
            return RedirectToAction("employeesearch");
        }

        public ActionResult employeeedit(int id)  //修改
        {
            var employee = edb.temployee.Where(m => m.EId == id).FirstOrDefault();
            return View(employee);
        }
        [HttpPost]       
        public ActionResult employeeedit(temployee vEmployee) //接收新的值
        {
            int id = vEmployee.EId;  //取得編號
            var Employee = edb.temployee.Where(m => m.EId == id).FirstOrDefault();  //抓取資料
            Employee.EName = vEmployee.EName;  //取代舊的資料
            Employee.EPhone = vEmployee.EPhone;
            Employee.EAddress = vEmployee.EAddress;
            edb.SaveChanges();
            return RedirectToAction("employeesearch");
        }

        public ActionResult employeedelete(int id)  //刪除
        {
            var employee = edb.temployee.Where(a => a.EId == id).FirstOrDefault(); //抓取id的那筆資料
            edb.temployee.Remove(employee);
            edb.SaveChanges();
            return RedirectToAction("employeesearch");
        }

        public ActionResult employeesearch(string searching)  //查詢
        {
            return View(edb.temployee.Where(x => x.EName.Contains(searching) || searching == null).ToList()); //抓取GET到的值
        }

        /// <summary>
        /// 產品資料
        /// </summary>
        public ActionResult productcreate()  //新增
        {
            return View();
        }
        [HttpPost]
        public ActionResult productcreate(tproduct vProduct)
        {
            pdb.tproduct.Add(vProduct);  //增加一筆
            pdb.SaveChanges();  //存取到資料庫
            return RedirectToAction("productsearch");
        }

        public ActionResult productedit(int id)  //修改
        {
            var product = pdb.tproduct.Where(m => m.PId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult productedit(tproduct vProduct) //接收新的值
        {
            int id = vProduct.PId;  //取得編號
            var Employee = pdb.tproduct.Where(m => m.PId == id).FirstOrDefault();  //抓取資料
            Employee.PName = vProduct.PName;  //取代舊的資料
            Employee.PQuantity = vProduct.PQuantity;
            Employee.PPurchaseprice = vProduct.PPurchaseprice;
            Employee.PSellingprice = vProduct.PSellingprice;
            Employee.PInventory = vProduct.PInventory;
            pdb.SaveChanges();
            return RedirectToAction("productsearch");
        }

        public ActionResult productdelete(int id)  //刪除
        {
            var product = pdb.tproduct.Where(a => a.PId == id).FirstOrDefault();  //抓取id的那筆資料
            pdb.tproduct.Remove(product);
            pdb.SaveChanges();
            return RedirectToAction("productsearch");
        }

        public ActionResult productsearch(string searching)  //查詢
        {
            return View(pdb.tproduct.Where(x => x.PName.Contains(searching) || searching == null).ToList()); //抓取GET到的值
        }
     
    }
}
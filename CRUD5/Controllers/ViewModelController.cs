using CRUD5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD5.Controllers
{
    public class ViewModelController : Controller
    {
        //建立資料庫連結
        dbcustomerEntities cdb = new dbcustomerEntities();  //客戶
        dbemployeeEntities edb = new dbemployeeEntities();  //廠商
        dbproductEntities pdb = new dbproductEntities();    //產品
        dbpurchaseEntities2 purdb = new dbpurchaseEntities2(); //進貨
        dbsalesEntities sdb = new dbsalesEntities();           //銷貨
        // GET: AllModels
       
        /// <summary>
        /// 進貨資料
        /// </summary>
        public ActionResult purchase()  //首頁
        {
            AllViewModel all = new AllViewModel();
            return View(all);          
        }

        public ActionResult purchasecreate(int id)  //新增
        {
            AllViewModel all = new AllViewModel();
            var product = pdb.tproduct.Where(m => m.PId == id).FirstOrDefault();  //篩選產品資料        
            if (product != null)
            {
                ViewBag.product_Name = product.PName;            //取得產品名稱
                ViewBag.product_Quantity = product.PQuantity;            //單位
                ViewBag.product_Purchaseprice = product.PPurchaseprice;  //進價
            }
            else
            {
                ViewBag.product_Name = "無此產品";
                ViewBag.product_Quantity = "";
                ViewBag.product_Purchaseprice = null;
            }
            var employee = edb.temployee.ToList();   //DB資料轉為SelectListItem給下拉清單用
            ViewBag.employeeName = (from item in edb.temployee select new SelectListItem
            {
                Text = item.EName,  //顯示的文字
                Value = item.EName  //傳的值的文字
            });
            ViewBag.employeePhone = (from item in edb.temployee select new SelectListItem
            {
                Text = item.EPhone,
                Value = item.EPhone
            });
            ViewBag.employeeAddress = (from item in edb.temployee select new SelectListItem
            {
                Text = item.EAddress,
                Value = item.EAddress
            });
            return View(all);
        }
        [HttpPost]
        public ActionResult purchasecreate(AllViewModel vPurchase) //接收值
        {
            string name = vPurchase.purchase.PName;  //取得產品名稱
            var product = pdb.tproduct.Where(m => m.PName == name).FirstOrDefault();  //抓取資料
            product.PInventory = product.PInventory + vPurchase.purchase.PAdd;  //庫存增加
            vPurchase.purchase.date = DateTime.Now;  //帶入日期
            purdb.tpurchase.Add(vPurchase.purchase); //增加一筆進貨紀錄
            pdb.SaveChanges();  //存檔
            purdb.SaveChanges();
            return RedirectToAction("purchase");
        }

        public ActionResult purchasedelete(int id)  //刪除
        {
            var purchase = purdb.tpurchase.Where(b => b.PId == id).FirstOrDefault();  //抓取id的那筆資料
            purdb.tpurchase.Remove(purchase);
            purdb.SaveChanges();
            return RedirectToAction("purchase");
        }

        /// <summary>
        /// 銷貨資料
        /// </summary>
        public ActionResult sales()  //首頁
        {
            AllViewModel all = new AllViewModel();
            return View(all);
        }
        public ActionResult salescreate(int id)  //新增
        {
            AllViewModel all = new AllViewModel();
            var product = pdb.tproduct.Where(m => m.PId == id).FirstOrDefault();  //篩選產品資料        
            if (product != null)
            {
                ViewBag.product_Name = product.PName;            //取得產品名稱
                ViewBag.product_Quantity = product.PQuantity;           //單位
                ViewBag.product_Purchaseprice = product.PSellingprice;  //售價
            }
            else
            {
                ViewBag.product_Name = "無此產品";
                ViewBag.product_Quantity = "";
                ViewBag.product_Purchaseprice = null;
            }
            var customer = cdb.tcustomer.ToList();   //DB資料轉為SelectListItem給下拉清單用
            ViewBag.customerName = (from item in cdb.tcustomer select new SelectListItem
            {
                Text = item.CName,
                Value = item.CName
            });
            ViewBag.customerPhone = (from item in cdb.tcustomer select new SelectListItem
            {
                Text = item.CPhone,
                Value = item.CPhone
            });
            ViewBag.customerAddress = (from item in cdb.tcustomer select new SelectListItem
            {
                Text = item.CAddress,
                Value = item.CAddress
            });
            return View(all);
        }
        [HttpPost]
        public ActionResult salescreate(AllViewModel vSales) //接收值
        {
            string name = vSales.sales.SName;  //取得產品名稱          
            var product = pdb.tproduct.Where(m => m.PName == name).FirstOrDefault();  //抓取資料
            product.PInventory = product.PInventory - vSales.sales.PReduce;  //庫存減少
            if (product.PInventory < 0)  //如果存量為負
            { product.PInventory = 0; }
            vSales.sales.date = DateTime.Now;  //帶入日期
            sdb.tsales.Add(vSales.sales);      //增加一筆銷貨紀錄
            pdb.SaveChanges();  //存檔
            sdb.SaveChanges();
            return RedirectToAction("sales");
        }
        public ActionResult salesdelete(int id)  //刪除
        {
            var sales = sdb.tsales.Where(b => b.SId == id).FirstOrDefault();  //抓取id的那筆資料
            sdb.tsales.Remove(sales);
            sdb.SaveChanges();
            return RedirectToAction("sales");
        }
    }
}

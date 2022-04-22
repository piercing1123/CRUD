using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD5.Models
{
    public class AllViewModel
    {
     /* public tcustomer customer { get; set; }
        public temployee employee { get; set; }
        public tproduct proruct { get; set; }  */
        public tpurchase purchase { get; set; } //取值
        public tsales sales { get; set; }

        public IEnumerable<tcustomer> vcustomer { get; set; }
        public IEnumerable<temployee> vemployee { get; set; }
        public IEnumerable<tproduct> vproduct { get; set; }
        public IEnumerable<tpurchase> vpurchase { get; set; }
        public IEnumerable<tsales> vsales { get; set; }
        public AllViewModel()  //讓View使用多個Model
        {
            dbcustomerEntities cdb = new dbcustomerEntities();
            this.vcustomer = cdb.tcustomer.ToList();

            dbemployeeEntities edb = new dbemployeeEntities();
            this.vemployee = edb.temployee.ToList();

            dbproductEntities pdb = new dbproductEntities();
            this.vproduct = pdb.tproduct.ToList();

            dbpurchaseEntities2 purdb = new dbpurchaseEntities2();
            this.vpurchase = purdb.tpurchase.ToList();

            dbsalesEntities sdb = new dbsalesEntities();
            this.vsales = sdb.tsales.ToList();
        }
    }
}
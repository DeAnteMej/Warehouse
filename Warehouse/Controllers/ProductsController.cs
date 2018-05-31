using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class ProductsController : Controller
    {
        private WarehouseContext db = new WarehouseContext();

        // GET: Products
        public ActionResult Index()
        {
            var list = db.Products.ToList();
            if (ControllerContext.IsChildAction)
            {
                return PartialView("_ProductTable", list);
            }
            else
            {
                return View(list);
            }
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Quantity,Category,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                //return RedirectToAction("Index");
                //return RedirectToAction("Details", new { product.Id });
                return RedirectToAction("Index", new { product.Id });
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            //B 2018-05-31 - Ej kunna ändra på Quantity
            //Installera NUGET "valueinjecter"
            var productEdit = Mapper.Map<ProductEditViewModel>(product);

            //var productEdit = new ProductEditViewModel
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Price = product.Price,
            //    Category = product.Category,
            //    Description = product.Description,
            //};

            return View(productEdit);
            //E 2018-05-31 - Ej kunna ändra på Quantity
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEditViewModel productEdit)
        {
            //2018-05-31 - Ej kunna ändra på Quantity
            if (!ModelState.IsValid) return View(productEdit);

            //Installera NUGET "valueinjecter"
            var product = Mapper.Map<Product>(productEdit);

            //var product = new Product
            //{
            //    Id = productEdit.Id,
            //    Name = productEdit.Name,
            //    Price = productEdit.Price,
            //    Category = productEdit.Category,
            //    Description = productEdit.Description,
            //};

            db.Entry(product).State = EntityState.Modified;
            db.Entry(product).Property(p => p.Quantity).IsModified = false;
            //E 2018-05-31 - Ej kunna ändra på Quantity

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

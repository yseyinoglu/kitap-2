using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Admin.Helpers;
using System.Drawing.Imaging;

namespace Admin.Controllers
{
    public class KitapİlanController : Controller
    {
        private kitapContainer db = new kitapContainer();

        // GET: Kitapİlan
        public ActionResult Index()
        {
            var kitapİlanSet = db.KitapİlanSet.Include(k => k.User).Include(k => k.Kategori).Include(k => k.KitapDetay);
            return View(kitapİlanSet.ToList());
        }

        // GET: Kitapİlan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitapİlan kitapİlan = db.KitapİlanSet.Find(id);
            if (kitapİlan == null)
            {
                return HttpNotFound();
            }
            return View(kitapİlan);
        }

        // GET: Kitapİlan/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserSet, "Id", "Name");
            ViewBag.KategoriId = new SelectList(db.KategoriSet, "Id", "KatagoriAdi");
            ViewBag.KitapDetayId = new SelectList(db.KitapDetaySet, "Id", "KitapSayfaNo");
            return View();
        }

        // POST: Kitapİlan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Baslik,KitapSayfaNo,KitapFiyat,KitapTarih,UserId,KategoriId,KitapDetayId")] Kitapİlan kitapİlan, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                if (Resim != null && Resim.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(Resim.InputStream))
                    {
                        kitapİlan.Resim = reader.ReadBytes(Resim.ContentLength);
                    }
                }

                db.KitapİlanSet.Add(kitapİlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserSet, "Id", "Name", kitapİlan.UserId);
            ViewBag.KategoriId = new SelectList(db.KategoriSet, "Id", "KatagoriAdi", kitapİlan.KategoriId);
            ViewBag.KitapDetayId = new SelectList(db.KitapDetaySet, "Id", "KitapSayfaNo", kitapİlan.KitapDetayId);
            return View(kitapİlan);
        }

        // GET: Kitapİlan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitapİlan kitapİlan = db.KitapİlanSet.Find(id);
            if (kitapİlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserSet, "Id", "Name", kitapİlan.UserId);
            ViewBag.KategoriId = new SelectList(db.KategoriSet, "Id", "KatagoriAdi", kitapİlan.KategoriId);
            ViewBag.KitapDetayId = new SelectList(db.KitapDetaySet, "Id", "KitapSayfaNo", kitapİlan.KitapDetayId);
            return View(kitapİlan);
        }

        // POST: Kitapİlan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Baslik,KitapSayfaNo,KitapFiyat,KitapTarih,UserId,KategoriId,KitapDetayId")] Kitapİlan kitapİlan, HttpPostedFileBase Resim)
        {
            if (ModelState.IsValid)
            {
                if (Resim != null && Resim.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(Resim.InputStream))
                    {
                        kitapİlan.Resim = reader.ReadBytes(Resim.ContentLength);
                    }
                }
                else
                {
                    db.Entry(kitapİlan).Property("Resim").IsModified = false;
                }

                db.Entry(kitapİlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserSet, "Id", "Name", kitapİlan.UserId);
            ViewBag.KategoriId = new SelectList(db.KategoriSet, "Id", "KatagoriAdi", kitapİlan.KategoriId);
            ViewBag.KitapDetayId = new SelectList(db.KitapDetaySet, "Id", "KitapSayfaNo", kitapİlan.KitapDetayId);
            return View(kitapİlan);
        }

        // GET: Kitapİlan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitapİlan kitapİlan = db.KitapİlanSet.Find(id);
            if (kitapİlan == null)
            {
                return HttpNotFound();
            }
            return View(kitapİlan);
        }

        // POST: Kitapİlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitapİlan kitapİlan = db.KitapİlanSet.Find(id);
            db.KitapİlanSet.Remove(kitapİlan);
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
        public ActionResult Resim(int id)
        {
            byte[] file = db.KitapİlanSet.Find(id).Resim;
            if (file == null)
            {
                return Content("Resim bulunamadı");
            }
            return File(file, ImageHelper.GetContentType(file).ToString());
        }
    }
}

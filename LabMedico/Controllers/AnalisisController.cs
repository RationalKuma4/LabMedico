using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LabMedico.Models;

namespace LabMedico.Controllers
{
    public class AnalisisController : Controller
    {
        private LaboratorioDbContext db = new LaboratorioDbContext();

        // GET: Analisis
        public ActionResult Index()
        {
            return View(db.Analisis.ToList());
        }

        // GET: Analisis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Analisis analisis = db.Analisis.Find(id);
            if (analisis == null)
            {
                return HttpNotFound();
            }
            return View(analisis);
        }

        // GET: Analisis/Create
        public ActionResult Create()
        {
            ViewBag.EstudioId = new SelectList(db.Estudios, "EstudioId", "Nombre");
            ViewBag.Estatus = Constantes.estatus;
            return View();
        }

        // POST: Analisis/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServicioId,Nombre,Descripcion,Requisitos,Estatus,CategoriaId")] Analisis analisis)
        {
            if (ModelState.IsValid)
            {
                db.Analisis.Add(analisis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(analisis);
        }

        // GET: Analisis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Analisis analisis = db.Analisis.Find(id);
            ViewBag.Estatus = Constantes.estatus;
            ViewBag.EstudioId = new SelectList(db.Estudios, "EstudioId", "Nombre", analisis.CategoriaId);
            if (analisis == null)
            {
                return HttpNotFound();
            }
            return View(analisis);
        }

        // POST: Analisis/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServicioId,Nombre,Descripcion,Requisitos,Estatus,CategoriaId")] Analisis analisis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(analisis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(analisis);
        }

        // GET: Analisis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Analisis analisis = db.Analisis.Find(id);
            if (analisis == null)
            {
                return HttpNotFound();
            }
            return View(analisis);
        }

        // POST: Analisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Analisis analisis = db.Analisis.Find(id);
            db.Analisis.Remove(analisis);
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

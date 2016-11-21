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
    public class TecnicoCitasController : Controller
    {
        private LaboratorioDbContext db = new LaboratorioDbContext();

        // GET: TecnicoCitas
        public ActionResult Index()
        {
            var tecnicoCitas = db.TecnicoCitas.Include(t => t.Tecnico);
            return View(tecnicoCitas.ToList());
        }

        // GET: TecnicoCitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TecnicoCitas tecnicoCitas = db.TecnicoCitas.Find(id);
            if (tecnicoCitas == null)
            {
                return HttpNotFound();
            }
            return View(tecnicoCitas);
        }

        // GET: TecnicoCitas/Create
        public ActionResult Create()
        {
            ViewBag.TecnicoId = new SelectList(db.Tecnicoes, "TecnicoId", "Nombre");
            return View();
        }

        // POST: TecnicoCitas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TecnicoCitasId,TecnicoId,CitaId")] TecnicoCitas tecnicoCitas)
        {
            if (ModelState.IsValid)
            {
                db.TecnicoCitas.Add(tecnicoCitas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TecnicoId = new SelectList(db.Tecnicoes, "TecnicoId", "Nombre", tecnicoCitas.TecnicoId);
            return View(tecnicoCitas);
        }

        // GET: TecnicoCitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TecnicoCitas tecnicoCitas = db.TecnicoCitas.Find(id);
            if (tecnicoCitas == null)
            {
                return HttpNotFound();
            }
            ViewBag.TecnicoId = new SelectList(db.Tecnicoes, "TecnicoId", "Nombre", tecnicoCitas.TecnicoId);
            return View(tecnicoCitas);
        }

        // POST: TecnicoCitas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TecnicoCitasId,TecnicoId,CitaId")] TecnicoCitas tecnicoCitas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tecnicoCitas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TecnicoId = new SelectList(db.Tecnicoes, "TecnicoId", "Nombre", tecnicoCitas.TecnicoId);
            return View(tecnicoCitas);
        }

        // GET: TecnicoCitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TecnicoCitas tecnicoCitas = db.TecnicoCitas.Find(id);
            if (tecnicoCitas == null)
            {
                return HttpNotFound();
            }
            return View(tecnicoCitas);
        }

        // POST: TecnicoCitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TecnicoCitas tecnicoCitas = db.TecnicoCitas.Find(id);
            db.TecnicoCitas.Remove(tecnicoCitas);
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

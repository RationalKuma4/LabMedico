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
    public class HistoricosController : Controller
    {
        private LaboratorioDbContext db = new LaboratorioDbContext();

        // GET: Historicos
        public ActionResult Index()
        {
            var historicoes = db.Historicoes.Include(h => h.Citas);
            return View(historicoes.ToList());
        }

        // GET: Historicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historico historico = db.Historicoes.Find(id);
            if (historico == null)
            {
                return HttpNotFound();
            }
            return View(historico);
        }

        // GET: Historicos/Create
        public ActionResult Create()
        {
            ViewBag.CitaId = new SelectList(db.Citas, "CitaId", "HoraAplicacion");
            return View();
        }

        // POST: Historicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoricoId,CitaId,FechaRegistro,Monto")] Historico historico)
        {
            if (ModelState.IsValid)
            {
                db.Historicoes.Add(historico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CitaId = new SelectList(db.Citas, "CitaId", "HoraAplicacion", historico.CitaId);
            return View(historico);
        }

        // GET: Historicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historico historico = db.Historicoes.Find(id);
            if (historico == null)
            {
                return HttpNotFound();
            }
            ViewBag.CitaId = new SelectList(db.Citas, "CitaId", "HoraAplicacion", historico.CitaId);
            return View(historico);
        }

        // POST: Historicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoricoId,CitaId,FechaRegistro,Monto")] Historico historico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CitaId = new SelectList(db.Citas, "CitaId", "HoraAplicacion", historico.CitaId);
            return View(historico);
        }

        // GET: Historicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historico historico = db.Historicoes.Find(id);
            if (historico == null)
            {
                return HttpNotFound();
            }
            return View(historico);
        }

        // POST: Historicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Historico historico = db.Historicoes.Find(id);
            db.Historicoes.Remove(historico);
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

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
    public class CitasController : Controller
    {
        private LaboratorioDbContext db = new LaboratorioDbContext();

        // GET: Citas
        public ActionResult Index()
        {
            var citas = db.Citas.Include(c => c.Clientes).Include(c => c.Tecnicos);
            return View(citas.ToList());
        }

        // GET: Citas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Citas/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nombre");
            ViewBag.TecnicoId = new SelectList(db.Tecnicoes, "TecnicoId", "Nombre");
            return View();
        }

        // POST: Citas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CitaId,FechaRegistro,FechaEntrega,FechaAplicacion,HoraAplicacion,UsuarioId,ClienteId,AnalisisId,TecnicoId,Estatus,Monto")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Citas.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nombre", cita.ClienteId);
            ViewBag.TecnicoId = new SelectList(db.Tecnicoes, "TecnicoId", "Nombre", cita.TecnicoId);
            return View(cita);
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nombre", cita.ClienteId);
            ViewBag.TecnicoId = new SelectList(db.Tecnicoes, "TecnicoId", "Nombre", cita.TecnicoId);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CitaId,FechaRegistro,FechaEntrega,FechaAplicacion,HoraAplicacion,UsuarioId,ClienteId,AnalisisId,TecnicoId,Estatus,Monto")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nombre", cita.ClienteId);
            ViewBag.TecnicoId = new SelectList(db.Tecnicoes, "TecnicoId", "Nombre", cita.TecnicoId);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita cita = db.Citas.Find(id);
            db.Citas.Remove(cita);
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

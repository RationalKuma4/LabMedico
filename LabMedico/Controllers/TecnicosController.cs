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
    public class TecnicosController : Controller
    {
        private LaboratorioDbContext db = new LaboratorioDbContext();

        // GET: Tecnicos
        public ActionResult Index()
        {
            var tecnicoes = db.Tecnicoes.Include(t => t.Estudios).Include(t => t.Sucursales);
            return View(tecnicoes.ToList());
        }

        // GET: Tecnicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico tecnico = db.Tecnicoes.Find(id);
            if (tecnico == null)
            {
                return HttpNotFound();
            }
            return View(tecnico);
        }

        // GET: Tecnicos/Create
        public ActionResult Create()
        {
            ViewBag.EstudioId = new SelectList(db.Estudios, "EstudioId", "Nombre");
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Nombre");
            ViewBag.Estatus = Constantes.estatus;
            return View();
        }

        // POST: Tecnicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TecnicoId,Nombre,ApellidoPaterno,ApellidoMaterno,Calle,NumeroInterior,NumeroExterior,Colonia,DelegacionMunicipio,CodigoPostal,Estado,Edad,Sexo,Notas,Estatus,SucursalId,EstudioId")] Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                db.Tecnicoes.Add(tecnico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstudioId = new SelectList(db.Estudios, "EstudioId", "Nombre", tecnico.EstudioId);
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Nombre", tecnico.SucursalId);
            return View(tecnico);
        }

        // GET: Tecnicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico tecnico = db.Tecnicoes.Find(id);
            if (tecnico == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstudioId = new SelectList(db.Estudios, "EstudioId", "Nombre", tecnico.EstudioId);
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Nombre", tecnico.SucursalId);
            ViewBag.Estatus = Constantes.estatus;
            return View(tecnico);
        }

        // POST: Tecnicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TecnicoId,Nombre,ApellidoPaterno,ApellidoMaterno,Calle,NumeroInterior,NumeroExterior,Colonia,DelegacionMunicipio,CodigoPostal,Estado,Edad,Sexo,Notas,Estatus,SucursalId,EstudioId")] Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tecnico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstudioId = new SelectList(db.Estudios, "EstudioId", "Nombre", tecnico.EstudioId);
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Nombre", tecnico.SucursalId);
            return View(tecnico);
        }

        // GET: Tecnicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico tecnico = db.Tecnicoes.Find(id);
            if (tecnico == null)
            {
                return HttpNotFound();
            }
            return View(tecnico);
        }

        // POST: Tecnicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tecnico tecnico = db.Tecnicoes.Find(id);
            db.Tecnicoes.Remove(tecnico);
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

using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LabMedico.Models;
using Microsoft.Reporting.WebForms;

namespace LabMedico.Controllers
{
    public class CitasController : Controller
    {
        private LaboratorioDbContext _db = new LaboratorioDbContext();
        //private readonly LaboratorioDbContext _db;
        /*public CitasController(LaboratorioDbContext db)
        {
            _db = db;
        }*/
        // GET: Citas
        public ActionResult Index()
        {
            var citas = _db.Citas.Include(c => c.Analisis).Include(c => c.Clientes).Include(c => c.Usuarios);
            return View(citas.ToList());
        }

        // GET: Citas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = _db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Citas/Create
        public ActionResult Create()
        {
            ViewBag.AnalisisId = new SelectList(_db.Analisis, "AnalisisId", "Nombre");
            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "Nombre");
            //ViewBag.Id = new SelectList(_db.LaboratorioUsers, "Id", "Usuario");
            return View();
        }

        // POST: Citas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CitaId,FechaRegistro,FechaEntrega,FechaAplicacion,HoraAplicacion,Id,ClienteId,AnalisisId,Estatus,Monto")] Cita cita)
        {
            cita.FechaRegistro = DateTime.Today;
            cita.FechaEntrega = DateTime.Today.AddDays(5);
            cita.Id = _db.Users.Where(u => u.UserName.Equals(User.Identity.Name)).ToList().FirstOrDefault().Id;
            cita.Monto = _db.AnalisisSucursals.Where(m => m.AnalisisId == 1 && m.SucursalId == 1)
                .FirstOrDefault()
                .Costo;

            if (!ModelState.IsValid)
            {
                _db.Citas.Add(cita);
                _db.SaveChanges();
                InsertTecnicoCita(cita);
                return RedirectToAction("Index");
            }

            ViewBag.AnalisisId = new SelectList(_db.Analisis, "AnalisisId", "Nombre", cita.AnalisisId);
            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "Nombre", cita.ClienteId);
            //ViewBag.Id = new SelectList(_db.LaboratorioUsers, "Id", "Usuario", cita.Id);
            return View(cita);
        }

        private void InsertTecnicoCita(Cita cita)
        {
            var userSucursal = _db.Users.Where(u => u.Usuario.Equals(User.Identity.Name))
                .FirstOrDefault();
            var estudioId = _db.Analisis.Where(a => a.AnalisisId == cita.AnalisisId).FirstOrDefault().EstudioId;
            var tecnicoId = _db.Tecnicoes.Where(t => t.EstudioId == estudioId).FirstOrDefault().TecnicoId;

            var tecnicoCitas = new TecnicoCitas
            {
                CitaId = cita.Id,
                TecnicoId = tecnicoId
            };

            _db.TecnicoCitas.Add(tecnicoCitas);
            _db.SaveChanges();
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = _db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnalisisId = new SelectList(_db.Analisis, "AnalisisId", "Nombre", cita.AnalisisId);
            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "Nombre", cita.ClienteId);
            //ViewBag.Id = new SelectList(_db.LaboratorioUsers, "Id", "Usuario", cita.Id);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CitaId,FechaRegistro,FechaEntrega,FechaAplicacion,HoraAplicacion,Id,ClienteId,AnalisisId,Estatus,Monto")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cita).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnalisisId = new SelectList(_db.Analisis, "AnalisisId", "Nombre", cita.AnalisisId);
            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "Nombre", cita.ClienteId);
            //ViewBag.Id = new SelectList(_db.LaboratorioUsers, "Id", "Usuario", cita.Id);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = _db.Citas.Find(id);
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
            Cita cita = _db.Citas.Find(id);
            _db.Citas.Remove(cita);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        public FileResult PrintCita()
        {
            using (var local = new LocalReport())
            {
                var citaInfo = _db.Citas
                    .Where(c => c.CitaId == 1)
                    .ToList();
                local.ReportPath = "Reports\\NotaEmision.rdlc";
                local.EnableExternalImages = false;
                local.DisplayName = "Cita";
                local.DataSources.Add(new ReportDataSource("Appoiment", citaInfo));
                local.Refresh();
                var reporte = local.Render("pdf");
                return File(reporte, System.Net.Mime.MediaTypeNames.Application.Octet, "hola.pdf");
            }
        }
    }
}

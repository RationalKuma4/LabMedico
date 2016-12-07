using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LabMedico.Models;
using Microsoft.Reporting.WebForms;
using LabMedico.ReportRepository;

namespace LabMedico.Controllers
{
    [Authorize(Roles = "Recepcionista, Administrador")]
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
            var sucursalId = _db.Users
                .Where(u => u.UserName.Equals(User.Identity.Name))
                .FirstOrDefault()
                .SucursalId;

            var citas = _db.Citas
                .Include(c => c.Analisis)
                .Include(c => c.Clientes)
                .Include(c => c.Usuarios);

            var result =
                from c in citas
                join u in _db.Users on c.Id equals u.Id
                where u.SucursalId == sucursalId
                && c.Estatus.Equals("Act")
                select c;

            return View(result.ToList());
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
        /*public ActionResult Create()
        {
            ViewBag.AnalisisId = new SelectList(_db.Analisis, "AnalisisId", "Nombre");
            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "NombreCompleto");
            //ViewBag.Id = new SelectList(_db.LaboratorioUsers, "Id", "Usuario");
            return View();
        }*/

        public ActionResult Create(int id)
        {
            var sucursalId = _db.Users
                .Where(s => s.Usuario.Equals(User.Identity.Name))
                .FirstOrDefault().SucursalId;

            /*var result =
               from a in _db.Analisis
               join aa in _db.AnalisisSucursals
                   on a.AnalisisId equals aa.AnalisisId
               where aa.SucursalId == sucursalId
               select a;*/

            var result2 =
                from a in _db.Analisis
                join aa in _db.AnalisisSucursals
                    on a.AnalisisId equals aa.AnalisisId
                into resultSet
                from r in resultSet.DefaultIfEmpty()
                where r.SucursalId == sucursalId
                select new
                {
                    AnalisisId = a.AnalisisId,
                    Nombre = a.Nombre
                };

            ViewBag.AnalisisId = new SelectList(result2, "AnalisisId", "Nombre");
            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "NombreCompleto", id);
            ViewBag.Estatus = Constantes.estatus;
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
            cita.Estatus = "Act";

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
            var tecnicoId = _db.Tecnicoes.Where(t => t.EstudioId == estudioId && t.SucursalId == userSucursal.SucursalId)
                .FirstOrDefault()
                .TecnicoId;

            var tecnicoCitas = new TecnicoCitas
            {
                CitaId = cita.CitaId,
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
            cita.FechaAplicacion.ToString();
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnalisisId = new SelectList(_db.Analisis, "AnalisisId", "Nombre", cita.AnalisisId);
            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "Nombre", cita.ClienteId);
            ViewBag.Estatus = Constantes.estatus;
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

        [HttpGet]
        public FileResult PrintCita(int id = 0)
        {
            using (var local = new LocalReport())
            {
                var citaInfo = (new CItasRepository()).RegresaNota(id);
                local.ReportPath = "Reports\\NotaEmision.rdlc";
                local.DisplayName = "Cita";
                local.DataSources.Add(new ReportDataSource("CItaVM", citaInfo));
                local.Refresh();
                var reporte = local.Render("pdf");
                return File(reporte, System.Net.Mime.MediaTypeNames.Application.Octet, "NotaCliente" + id.ToString() + ".pdf");
            }
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult ProcesaCitas()
        {
            return View(_db.Citas.Where(c => c.Estatus.Equals("Act")).ToList());
        }

        public ActionResult RealizaProceso()
        {
            var citasProcesadas = _db.Citas
                .Where(c => c.Estatus.Equals("Act"))
                .ToList();

            foreach (var item in citasProcesadas)
            {
                item.Estatus = "Pro";
                _db.Entry(item).State = EntityState.Modified;

                var historico = new Historico
                {
                    CitaId = item.CitaId,
                    FechaRegistro = DateTime.Now,
                    Monto = item.Monto
                };
                _db.Historicoes.Add(historico);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}

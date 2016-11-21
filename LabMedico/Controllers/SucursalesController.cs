using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LabMedico.Models;
using LabMedico.ViewModels;

namespace LabMedico.Controllers
{
    public class SucursalesController : Controller
    {
        private LaboratorioDbContext db = new LaboratorioDbContext();

        // GET: Sucursales
        public ActionResult Index()
        {
            //return View(db.Sucursals.ToList());
            return View(SucursalesResulset());
        }

        // GET: Sucursales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = db.Sucursals.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // GET: Sucursales/Create
        public ActionResult Create()
        {
            ViewBag.Estatus = Constantes.estatus;
            ViewBag.Zonas = new SelectList(db.Zonas, "ZonaId", "ZonaNombre");
            return View();
        }

        // POST: Sucursales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SucursalId,Nombre,Calle,NumeroInterior,NumeroExterior,Colonia,DelegacionMunicipio,CodigoPostal,Telefono,Horario,Estatus,ZonaId")] Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                db.Sucursals.Add(sucursal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sucursal);
        }

        // GET: Sucursales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = db.Sucursals.Find(id);
            ViewBag.Zonas = new SelectList(db.Zonas, "ZonaId", "ZonaNombre", sucursal.ZonaId);
            ViewBag.Estatus = Constantes.estatus;
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // POST: Sucursales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SucursalId,Nombre,Calle,NumeroInterior,NumeroExterior,Colonia,DelegacionMunicipio,CodigoPostal,Telefono,Horario,Estatus,ZonaId")] Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sucursal);
        }

        // GET: Sucursales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = db.Sucursals.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // POST: Sucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sucursal sucursal = db.Sucursals.Find(id);
            db.Sucursals.Remove(sucursal);
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

        private List<SucursalViewModel> SucursalesResulset()
        {
            var result =
                from s in db.Sucursals
                join z in db.Zonas on s.ZonaId equals z.ZonaId
                into resulSet
                from r in resulSet.DefaultIfEmpty()
                select new
                {
                    SucursalId = s.SucursalId,
                    Nombre = s.Nombre,
                    DelegacionMunicipio = s.DelegacionMunicipio,
                    CodigoPostal = s.CodigoPostal,
                    Telefono = s.Telefono,
                    Horario = s.Horario,
                    ZonaNombre = r.ZonaNombre
                };

            List<SucursalViewModel> sucursales = new List<SucursalViewModel>();

            foreach (var item in result.ToList())
            {
                var sucursal = new SucursalViewModel
                {
                    SucursalId = item.SucursalId,
                    Nombre = item.Nombre,
                    DelegacionMunicipio = item.DelegacionMunicipio,
                    CodigoPostal = item.CodigoPostal,
                    Telefono = item.Telefono,
                    Horario = item.Horario,
                    ZonaNombre = item.ZonaNombre
                };

                sucursales.Add(sucursal);
            }

            return sucursales;
        }
    }
}

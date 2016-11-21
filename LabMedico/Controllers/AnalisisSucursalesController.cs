using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LabMedico.Models;

namespace LabMedico.Controllers
{
    public class AnalisisSucursalesController : Controller
    {
        private LaboratorioDbContext db = new LaboratorioDbContext();

        // GET: AnalisisSucursales
        public ActionResult Index()
        {
            var analisisSucursals = db.AnalisisSucursals.Include(a => a.Analisis).Include(a => a.Sucursales);
            return View(analisisSucursals.ToList());
        }

        // GET: AnalisisSucursales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalisisSucursal analisisSucursal = db.AnalisisSucursals.Find(id);
            if (analisisSucursal == null)
            {
                return HttpNotFound();
            }
            return View(analisisSucursal);
        }

        // GET: AnalisisSucursales/Create
        public ActionResult Create()
        {
            ViewBag.AnalisisId = new SelectList(db.Analisis, "AnalisisId", "Nombre");
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Nombre");
            ViewBag.Estatus = Constantes.estatus;
            return View();
        }

        // POST: AnalisisSucursales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnalisisSucursalId,SucursalId,AnalisisId,Costo,Estatus")] AnalisisSucursal analisisSucursal)
        {
            if (ModelState.IsValid)
            {
                db.AnalisisSucursals.Add(analisisSucursal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnalisisId = new SelectList(db.Analisis, "AnalisisId", "Nombre", analisisSucursal.AnalisisId);
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Nombre", analisisSucursal.SucursalId);
            return View(analisisSucursal);
        }

        // GET: AnalisisSucursales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalisisSucursal analisisSucursal = db.AnalisisSucursals.Find(id);
            if (analisisSucursal == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnalisisId = new SelectList(db.Analisis, "AnalisisId", "Nombre", analisisSucursal.AnalisisId);
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Nombre", analisisSucursal.SucursalId);
            ViewBag.Estatus = Constantes.estatus;
            return View(analisisSucursal);
        }

        // POST: AnalisisSucursales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnalisisSucursalId,SucursalId,AnalisisId,Costo,Estatus")] AnalisisSucursal analisisSucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(analisisSucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnalisisId = new SelectList(db.Analisis, "AnalisisId", "Nombre", analisisSucursal.AnalisisId);
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Nombre", analisisSucursal.SucursalId);
            return View(analisisSucursal);
        }

        // GET: AnalisisSucursales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalisisSucursal analisisSucursal = db.AnalisisSucursals.Find(id);
            if (analisisSucursal == null)
            {
                return HttpNotFound();
            }
            return View(analisisSucursal);
        }

        // POST: AnalisisSucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnalisisSucursal analisisSucursal = db.AnalisisSucursals.Find(id);
            db.AnalisisSucursals.Remove(analisisSucursal);
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

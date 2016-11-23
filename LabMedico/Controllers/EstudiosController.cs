using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LabMedico.Models;

namespace LabMedico.Controllers
{
    public class EstudiosController : Controller
    {
        private LaboratorioDbContext _db = new LaboratorioDbContext();
        //private readonly LaboratorioDbContext _db;
        /*public EstudiosController(LaboratorioDbContext db)
        {
            _db = db;
        }*/

        // GET: Estudios
        public ActionResult Index()
        {
            return View(_db.Estudios.ToList());
        }

        // GET: Estudios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudio estudio = _db.Estudios.Find(id);

            if (estudio == null)
            {
                return HttpNotFound();
            }
            return View(estudio);
        }

        // GET: Estudios/Create
        public ActionResult Create()
        {
            ViewBag.Estatus = Constantes.estatus;
            return View();
        }

        // POST: Estudios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstudioId,Nombre,Descripcion,Estatus")] Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                _db.Estudios.Add(estudio);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estudio);
        }

        // GET: Estudios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudio estudio = _db.Estudios.Find(id);
            ViewBag.Estatus = Constantes.estatus;
            if (estudio == null)
            {
                return HttpNotFound();
            }
            return View(estudio);
        }

        // POST: Estudios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstudioId,Nombre,Descripcion,Estatus")] Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(estudio).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estudio);
        }

        // GET: Estudios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudio estudio = _db.Estudios.Find(id);
            if (estudio == null)
            {
                return HttpNotFound();
            }
            return View(estudio);
        }

        // POST: Estudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudio estudio = _db.Estudios.Find(id);
            _db.Estudios.Remove(estudio);
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
    }
}

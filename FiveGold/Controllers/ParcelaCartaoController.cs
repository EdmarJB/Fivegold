using FiveGold.Models.Mapeamento;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class ParcelaCartaoController : Controller
    {
        private FiveGoldContext db = new FiveGoldContext();

        // GET: ParcelaCartao
        public ActionResult Index()
        {
            return View(db.ParcelaCartaos.ToList());
        }

        // GET: ParcelaCartao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParcelaCartao parcelaCartao = db.ParcelaCartaos.Find(id);
            if (parcelaCartao == null)
            {
                return HttpNotFound();
            }
            return View(parcelaCartao);
        }

        // GET: ParcelaCartao/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParcelaCartao parcelaCartao)
        {
            if (ModelState.IsValid)
            {
                db.ParcelaCartaos.Add(parcelaCartao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parcelaCartao);
        }

        // GET: ParcelaCartao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParcelaCartao parcelaCartao = db.ParcelaCartaos.Find(id);
            if (parcelaCartao == null)
            {
                return HttpNotFound();
            }
            return View(parcelaCartao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ParcelaCartao parcelaCartao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parcelaCartao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parcelaCartao);
        }

        // GET: ParcelaCartao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParcelaCartao parcelaCartao = db.ParcelaCartaos.Find(id);
            if (parcelaCartao == null)
            {
                return HttpNotFound();
            }
            return View(parcelaCartao);
        }

        // POST: ParcelaCartao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParcelaCartao parcelaCartao = db.ParcelaCartaos.Find(id);
            db.ParcelaCartaos.Remove(parcelaCartao);
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

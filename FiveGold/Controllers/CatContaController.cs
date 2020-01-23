using FiveGold.Models.Business;
using FiveGold.Models.Mapeamento;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class CatContaController : Controller
    {
        private FiveGoldContext db = new FiveGoldContext();

        private CatContaBusiness catContaBusiness = new CatContaBusiness();

        // GET: CatConta
        public ActionResult Index()
        {
            return View(catContaBusiness.Listar());
        }

        // GET: CatConta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catConta = catContaBusiness.Buscar(id);
            if (catConta == null)
            {
                return HttpNotFound();
            }
            return View(catConta);
        }

        // GET: CatConta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CatConta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatConta catConta)
        {
            if (ModelState.IsValid)
            {
                catContaBusiness.Inserir(catConta);
                return RedirectToAction("Index");
            }

            return View(catConta);
        }

        // GET: CatConta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catConta = catContaBusiness.Alterar(id);
            if (catConta == null)
            {
                return HttpNotFound();
            }
            return View(catConta);
        }

        // POST: CatConta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CatConta catConta)
        {
            if (ModelState.IsValid)
            {
                catContaBusiness.AlterarConfirmacao(catConta);
                return RedirectToAction("Index");
            }
            return View(catConta);
        }

        // GET: CatConta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catConta = catContaBusiness.Apagar(id);
            if (catConta == null)
            {
                return HttpNotFound();
            }
            return View(catConta);
        }

        // POST: CatConta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            catContaBusiness.ApagarConfirmacao(id);
            return RedirectToAction("Index");
        }
    }
}
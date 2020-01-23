using FiveGold.Mapeamento.Business;
using FiveGold.Models.Mapeamento;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class CatReceitaController : Controller
    {
        private CatReceitaBusiness catReceitaBusiness = new CatReceitaBusiness();

        // GET: CatReceita
        public ActionResult Index()
        {
            return View(catReceitaBusiness.Listar());
        }

        // GET: CatReceita/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catReceita = catReceitaBusiness.Buscar(id);
            if (catReceita == null)
            {
                return HttpNotFound();
            }
            return View(catReceita);
        }

        // GET: CatReceita/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CatReceita/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatReceita catReceita)
        {
            if (ModelState.IsValid)
            {
                catReceitaBusiness.Inserir(catReceita);
                return RedirectToAction("Index");
            }

            return View(catReceita);
        }

        // GET: CatReceita/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catReceita = catReceitaBusiness.Alterar(id);
            if (catReceita == null)
            {
                return HttpNotFound();
            }
            return View(catReceita);
        }

        // POST: CatReceita/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CatReceita catReceita)
        {
            if (ModelState.IsValid)
            {
                catReceitaBusiness.AlterarConfirmacao(catReceita);
                return RedirectToAction("Index");
            }
            return View(catReceita);
        }

        // GET: CatReceita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catReceita = catReceitaBusiness.Apagar(id);
            if (catReceita == null)
            {
                return HttpNotFound();
            }
            return View(catReceita);
        }

        // POST: CatReceita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            catReceitaBusiness.ApagarConfirmacao(id);
            return RedirectToAction("Index");
        }
    }
}

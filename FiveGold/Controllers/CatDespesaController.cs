using FiveGold.Mapeamento.Business;
using FiveGold.Models.Mapeamento;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class CatDespesaController : Controller
    {
        private CatDespesaBusiness catDespesaBusiness = new CatDespesaBusiness();

        // GET: CatDespesa
        public ActionResult Index()
        {
            return View(catDespesaBusiness.Listar());
        }

        // GET: CatDespesa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catDespesa = catDespesaBusiness.Buscar(id);
            if (catDespesa == null)
            {
                return HttpNotFound();
            }
            return View(catDespesa);
        }

        // GET: CatDespesa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CatDespesa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatDespesa catDespesa)
        {
            if (ModelState.IsValid)
            {
                catDespesaBusiness.Inserir(catDespesa);
                return RedirectToAction("Index");
            }

            return View(catDespesa);
        }

        // GET: CatDespesa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catDespesa = catDespesaBusiness.Alterar(id);
            if (catDespesa == null)
            {
                return HttpNotFound();
            }
            return View(catDespesa);
        }

        // POST: CatDespesa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CatDespesa catDespesa)
        {
            if (ModelState.IsValid)
            {
                catDespesaBusiness.AlterarConfirmacao(catDespesa);
                return RedirectToAction("Index");
            }
            return View(catDespesa);
        }

        // GET: CatDespesa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catDespesa = catDespesaBusiness.Apagar(id);
            if (catDespesa == null)
            {
                return HttpNotFound();
            }
            return View(catDespesa);
        }

        // POST: CatDespesa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            catDespesaBusiness.ApagarConfirmacao(id);
            return RedirectToAction("Index");
        }
    }
}

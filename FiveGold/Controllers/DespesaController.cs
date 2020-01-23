using FiveGold.Mapeamento.Business;
using FiveGold.Models;
using FiveGold.Models.Mapeamento;
using System;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class DespesaController : Controller
    {
        private DespesaBusiness despesaBusiness = new DespesaBusiness();

        // GET: Despesa
        public ActionResult Index()
        {
            return View(despesaBusiness.Listar(User.Identity.Name));
        }

        // GET: Despesa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var despesa = despesaBusiness.Buscar(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return PartialView(despesa);
        }

        // GET: Despesa/Create
        public ActionResult Create()
        {
            Despesa d = new Despesa();
            d.Data = DateTime.Now;

            ViewBag.IDCatDespesa = new SelectList(despesaBusiness.ListarCatDespesa(), "IDCatDespesa", "Nome");
            ViewBag.IDConta = new SelectList(despesaBusiness.ListarConta(User.Identity.Name), "IDConta", "Nome");
            return PartialView(d);
        }

        // POST: Despesa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Despesa despesa)
        {
            despesa.UserId = User.Identity.Name;

            if (ModelState.IsValid)
            {
                despesaBusiness.Inserir(despesa);
                return RedirectToAction("../Dashboard/Index");
            }

            ViewBag.IDCatDespesa = new SelectList(despesaBusiness.ListarCatDespesa(), "IDCatDespesa", "Nome", despesa.IDCatDespesa);
            ViewBag.IDConta = new SelectList(despesaBusiness.ListarConta(User.Identity.Name), "IDConta", "Nome", despesa.IDConta);
            return View(despesa);
        }

        // GET: Despesa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var despesa = despesaBusiness.Alterar(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCatDespesa = new SelectList(despesaBusiness.ListarCatDespesa(), "IDCatDespesa", "Nome", despesa.IDCatDespesa);
            ViewBag.IDConta = new SelectList(despesaBusiness.ListarConta(User.Identity.Name), "IDConta", "Nome", despesa.IDConta);
            return PartialView(despesa);
        }

        // POST: Despesa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                despesaBusiness.AlterarConfirmacao(despesa);
                return RedirectToAction("../Dashboard/Despesas");
            }
            ViewBag.IDCatDespesa = new SelectList(despesaBusiness.ListarCatDespesa(), "IDCatDespesa", "Nome", despesa.IDCatDespesa);
            ViewBag.IDConta = new SelectList(despesaBusiness.ListarConta(User.Identity.Name), "IDConta", "Nome", despesa.IDConta);
            return View(despesa);
        }

        // GET: Despesa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var despesa = despesaBusiness.Apagar(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return PartialView(despesa);
        }

        // POST: Despesa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            despesaBusiness.ApagarConfirmar(id);
            return RedirectToAction("../Dashboard/Despesas");
        }
    }
}

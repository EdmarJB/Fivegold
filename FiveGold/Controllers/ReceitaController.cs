using FiveGold.Mapeamento.Business;
using FiveGold.Models;
using System;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FiveGold.Models.Mapeamento;

namespace FiveGold.Controllers
{
    public class ReceitaController : Controller
    {
        private ReceitaBusiness receitaBusiness = new ReceitaBusiness();

        // GET: Receita
        public ActionResult Index()
        {
            return View(receitaBusiness.Listar(User.Identity.Name));
        }

        // GET: Receita/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var receita = receitaBusiness.Buscar(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return PartialView(receita);
        }

        // GET: Receita/Create
        public ActionResult Create()
        {
            Receita r = new Receita();
            r.Data = DateTime.Now;

            ViewBag.IDCatReceita = new SelectList(receitaBusiness.ListarCatReceita(), "IDCatReceita", "Nome");
            ViewBag.IDConta = new SelectList(receitaBusiness.ListarConta(User.Identity.Name), "IDConta", "Nome");
            return PartialView(r);
        }

        // POST: Receita/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Receita receita)
        {

            //receita.UserId = User.Identity.GetUserId();
            //receita.UserId = Usuario.Get.UserId;
            //receita.UserId = Usuario.Get.IdUsuarioLogado;
            //receita.UserId = Usuario.Get.Nome;
            //receita.Data = DateTime.Now;
            receita.UserId = User.Identity.Name;

            if (ModelState.IsValid)
            {
                receitaBusiness.Inserir(receita);
                return RedirectToAction("../Dashboard/Index");
            }

            ViewBag.IDCatReceita = new SelectList(receitaBusiness.ListarCatReceita(), "IDCatReceita", "Nome", receita.IDCatReceita);
            ViewBag.IDConta = new SelectList(receitaBusiness.ListarConta(User.Identity.Name), "IDConta", "Nome", receita.IDConta);
            return View(receita);
        }

        // GET: Receita/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var receita = receitaBusiness.Alterar(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCatReceita = new SelectList(receitaBusiness.ListarCatReceita(), "IDCatReceita", "Nome", receita.IDCatReceita);
            ViewBag.IDConta = new SelectList(receitaBusiness.ListarConta(User.Identity.Name), "IDConta", "Nome", receita.IDConta);
            return PartialView(receita);
        }

        // POST: Receita/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Receita receita)
        {
            if (ModelState.IsValid)
            {
                receitaBusiness.Alterar(receita);
                return RedirectToAction("../Dashboard/Receitas");
            }
            ViewBag.IDCatReceita = new SelectList(receitaBusiness.ListarCatReceita(), "IDCatReceita", "Nome", receita.IDCatReceita);
            ViewBag.IDConta = new SelectList(receitaBusiness.ListarConta(User.Identity.Name), "IDConta", "Nome", receita.IDConta);
            return View(receita);
        }

        // GET: Receita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var receita = receitaBusiness.Apagar(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return PartialView(receita);
        }

        // POST: Receita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            receitaBusiness.ApagarConfirmar(id);
            return RedirectToAction("../Dashboard/Receitas");
        }
    }
}

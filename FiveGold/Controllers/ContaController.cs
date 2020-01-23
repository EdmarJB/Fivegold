using FiveGold.Mapeamento.Business;
using FiveGold.Models;
using FiveGold.Models.Mapeamento;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class ContaController : Controller
    {
        public ContaBusiness contaBusiness = new ContaBusiness();

        // GET: Conta
        public ActionResult Index()
        {
            return View(contaBusiness.Listar());
        }

        // GET: Conta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var conta = contaBusiness.Buscar(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return PartialView(conta);
        }

        // GET: Conta/Create
        public ActionResult Create()
        {
            ViewBag.IDCatConta = new SelectList(contaBusiness.ListarCatContas(), "IDCatConta", "Nome");
            return PartialView();
        }

        // POST: Conta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Conta conta)
        {
            conta.UserId = User.Identity.Name;

            if (ModelState.IsValid)
            {
                contaBusiness.Inserir(conta);
                return RedirectToAction("../Dashboard/Contas");
            }

            ViewBag.IDCatConta = new SelectList(contaBusiness.ListarCatContas(), "IDCatConta", "Nome", conta.IDCatConta);
            return PartialView(conta);
        }

        // GET: Conta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var conta = contaBusiness.Alterar(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCatConta = new SelectList(contaBusiness.ListarCatContas(), "IDCatConta", "Nome", conta.IDCatConta);
            return PartialView(conta);
        }

        // POST: Conta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Conta conta)
        {
            if (ModelState.IsValid)
            {
                contaBusiness.AlterarConfirmacao(conta);
                return RedirectToAction("../Dashboard/Contas");
            }
            ViewBag.IDCatConta = new SelectList(contaBusiness.ListarCatContas(), "IDCatConta", "Nome", conta.IDCatConta);
            return View(conta);
        }

        // GET: Conta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var conta = contaBusiness.Apagar(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return PartialView(conta);
        }

        // POST: Conta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contaBusiness.ApagarConfirmacao(id);
            return RedirectToAction("../Dashboard/Contas");
        }
    }
}

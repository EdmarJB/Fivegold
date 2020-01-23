using FiveGold.Models.Business;
using FiveGold.Models.Mapeamento;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class TransferenciaController : Controller
    {
        private FiveGoldContext db = new FiveGoldContext();

        TransferenciaBusiness transferenciaBusiness = new TransferenciaBusiness();

        // GET: Transferencia
        public ActionResult Index()
        {
            ViewBag.ListaNomeContas = transferenciaBusiness.ListarContas(User.Identity.Name);
            return View(transferenciaBusiness.ListarTransferencias(User.Identity.Name));
        }

        // GET: Transferencia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = transferenciaBusiness.Buscar(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return PartialView(transferencia);
        }

        // GET: Transferencia/Create
        public ActionResult Create()
        {
            Transferencia t = new Transferencia();
            t.DataTransferencia = DateTime.Now;

            ViewBag.IDContaOrigem = new SelectList(transferenciaBusiness.ListarContas(User.Identity.Name), "IDConta", "Nome");
            ViewBag.IDContaDestino = new SelectList(transferenciaBusiness.ListarContas(User.Identity.Name), "IDConta", "Nome");

            return PartialView(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transferencia transferencia, int IDContaOrigem, int IDContaDestino)
        {
            if (ModelState.IsValid)
            {
                if (transferenciaBusiness.VerificaSaldo(IDContaOrigem) >= transferencia.Valor)
                {
                    transferenciaBusiness.Debitar(IDContaOrigem, transferencia.Valor);
                    transferenciaBusiness.Creditar(IDContaDestino, transferencia.Valor);

                    transferencia.UserId = User.Identity.Name;
                    transferencia.IDOrigem = IDContaOrigem;
                    transferencia.IDDestino = IDContaDestino;
                    transferenciaBusiness.Salvar(transferencia);
                }
                else
                {
                    ViewBag.SaldoInsuficiente = "Saldo Insuficiente";
                    ViewBag.IDContaOrigem = new SelectList(transferenciaBusiness.ListarContas(User.Identity.Name), "IDConta", "Nome", IDContaOrigem);
                    ViewBag.IDContaDestino = new SelectList(transferenciaBusiness.ListarContas(User.Identity.Name), "IDConta", "Nome", IDContaDestino);
                    //return View(transferencia);
                    return RedirectToAction("../Dashboard/Transferencias");
                }

                return RedirectToAction("../Dashboard/Transferencias");
            }

            ViewBag.IDContaOrigem = new SelectList(transferenciaBusiness.ListarContas(User.Identity.Name), "IDConta", "Nome", IDContaOrigem);
            ViewBag.IDContaDestino = new SelectList(transferenciaBusiness.ListarContas(User.Identity.Name), "IDConta", "Nome", IDContaDestino);
            return View(transferencia);
        }

        // GET: Transferencia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = transferenciaBusiness.Buscar(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return PartialView(transferencia);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                transferenciaBusiness.Editar(transferencia);
                return RedirectToAction("../Dashboard/Transferencias");
            }
            return View(transferencia);
        }

        // GET: Transferencia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = transferenciaBusiness.Buscar(id);
            ViewBag.IDConta = transferenciaBusiness.ListarContas(User.Identity.Name);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return PartialView(transferencia);
        }

        // POST: Transferencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transferencia transferencia = transferenciaBusiness.Buscar(id);
            transferenciaBusiness.Apagar(transferencia);
            return RedirectToAction("../Dashboard/Transferencias");
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

using FiveGold.Mapeamento.Business;
using FiveGold.Models.Business;
using FiveGold.Models.Mapeamento;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class PagarFaturaController : Controller
    {
        private FiveGoldContext db = new FiveGoldContext();
        DashboardBusiness dashboardBusiness = new DashboardBusiness();

        // GET: PagarFatura
        public ActionResult Index()
        {
            var pagarFaturas = db.PagarFaturas.Include(p => p.Conta);
            return View(pagarFaturas.ToList());
        }

        // GET: PagarFatura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagarFatura pagarFatura = db.PagarFaturas.Find(id);
            if (pagarFatura == null)
            {
                return HttpNotFound();
            }
            return View(pagarFatura);
        }

        // GET: PagarFatura/Create
        public ActionResult Create(int IDCartao, Decimal ValorFatura, DateTime DataInicial)
        {
            PagarFatura pagarFatura = new PagarFatura();
            pagarFatura.Valor = ValorFatura;
            pagarFatura.DataInicial = DataInicial;
            pagarFatura.IDCartao = IDCartao;
            pagarFatura.IDCatDespesa = 1;

            DespesaBusiness despesaBusiness = new DespesaBusiness();
            //ViewBag.IDCatDespesa = new SelectList(despesaBusiness.ListarCatDespesa(), "IDCatDespesa", "Nome", 1);
            ViewBag.IDConta = new SelectList(dashboardBusiness.listarContasPagarFatura(User.Identity.Name), "IDConta", "Nome");
            return PartialView(pagarFatura);
        }

        [HttpPost]
        public JsonResult Create(PagarFatura pagarFatura)
        {
            // Setar a fatura do Mês informado como paga
            if (dashboardBusiness.SetarFaturaPaga(pagarFatura.IDCartao, pagarFatura.DataInicial))
            {
                // Criar a despesa do pagamento da fatura
                DespesaBusiness despesaBusiness = new DespesaBusiness();
                Despesa despesa = new Despesa();

                despesa.IDCatDespesa = pagarFatura.IDCatDespesa;
                despesa.UserId = User.Identity.Name;
                despesa.Valor = pagarFatura.Valor;
                despesa.IDConta = pagarFatura.IDConta;
                despesa.Data = DateTime.Now;
                despesa.Descricao = "Pagamento de fatura do cartao - " + dashboardBusiness.Verificacartao(pagarFatura.IDCartao).ToString();

                if (despesaBusiness.Inserir(despesa))
                    return Json("pagou");
                else
                    dashboardBusiness.SetarFaturaNaoPaga(pagarFatura.IDCartao, pagarFatura.DataInicial);
                    return Json("naoPagou");
            }
            else
                return Json("problema");
        }

        // GET: PagarFatura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagarFatura pagarFatura = db.PagarFaturas.Find(id);
            if (pagarFatura == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDConta = new SelectList(dashboardBusiness.listarContas(User.Identity.Name), "IDConta", "Nome", pagarFatura.IDConta);
            return View(pagarFatura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Valor,IDConta")] PagarFatura pagarFatura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagarFatura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDConta = new SelectList(dashboardBusiness.listarContas(User.Identity.Name), "IDConta", "Nome", pagarFatura.IDConta);
            return View(pagarFatura);
        }

        // GET: PagarFatura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagarFatura pagarFatura = db.PagarFaturas.Find(id);
            if (pagarFatura == null)
            {
                return HttpNotFound();
            }
            return View(pagarFatura);
        }

        // POST: PagarFatura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PagarFatura pagarFatura = db.PagarFaturas.Find(id);
            db.PagarFaturas.Remove(pagarFatura);
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

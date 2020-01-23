using FiveGold.Models.Business;
using FiveGold.Models.Mapeamento;
using System;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class DespesaCartaoController : Controller
    {
        private DespesaCartaoBusiness despesaCartaoBusiness = new DespesaCartaoBusiness();

        // GET: DespesaCartao
        public ActionResult Index(int id)
        {
            return View(despesaCartaoBusiness.Listar(id));
        }

        // GET: DespesaCartao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var despesaCartao = despesaCartaoBusiness.Buscar(id);
            if (despesaCartao == null)
            {
                return HttpNotFound();
            }
            return PartialView(despesaCartao);
        }

        // GET: DespesaCartao/Create
        public ActionResult Create()
        {
            DespesaCartao dc = new DespesaCartao();
            dc.DataCompra = DateTime.Now;
            dc.QtdParcela = 1;

            ViewBag.IDCartao = new SelectList(despesaCartaoBusiness.ListarCartao(User.Identity.Name), "IDCartao", "Nome");
            ViewBag.IDCatDespesa = new SelectList(despesaCartaoBusiness.ListarCatDespesa(), "IDCatDespesa", "Nome");
            return PartialView(dc);
        }

        // POST: DespesaCartao/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(DespesaCartao despesaCartao)
        {
            try
            {
                var diaFechamento = despesaCartaoBusiness.DiaFechamentoCartao(despesaCartao.IDCartao);
                DateTime DataFechamento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, diaFechamento);
                despesaCartao.ValorParcela = Decimal.ToDouble(despesaCartao.Valor) / despesaCartao.QtdParcela;
                despesaCartao.Pago = false;

                DateTime DataFechamentoVirtual = new DateTime(despesaCartao.DataCompra.Year, despesaCartao.DataCompra.Month, diaFechamento);

                if (despesaCartao.DataCompra >= DataFechamentoVirtual)
                {
                    DateTime DataPrimeiraParcela = despesaCartao.DataCompra.AddMonths(1);
                    despesaCartao.DataPrimeiraParcela = new DateTime(DataPrimeiraParcela.Year, DataPrimeiraParcela.Month, diaFechamento);

                    DateTime DataUltimaParcela = despesaCartao.DataCompra.AddMonths(despesaCartao.QtdParcela);
                    despesaCartao.DataUltimaParcela = new DateTime(DataUltimaParcela.Year, DataUltimaParcela.Month, diaFechamento);
                }
                else if (despesaCartao.DataCompra < DataFechamentoVirtual)
                {
                    despesaCartao.DataPrimeiraParcela = new DateTime(despesaCartao.DataCompra.Year, despesaCartao.DataCompra.Month, diaFechamento);

                    DateTime DataUltimaParcela = despesaCartao.DataCompra.AddMonths(despesaCartao.QtdParcela - 1);
                    despesaCartao.DataUltimaParcela = new DateTime(DataUltimaParcela.Year, DataUltimaParcela.Month, diaFechamento);
                }

                int Cont = despesaCartao.QtdParcela;
                DateTime Parcela = despesaCartao.DataPrimeiraParcela.AddMonths(-1);
                while (Cont >= 1)
                {
                    ParcelaCartao parcelaCartao = new ParcelaCartao();
                    parcelaCartao.IDCartao = despesaCartao.IDCartao;
                    parcelaCartao.Pago = despesaCartao.Pago;
                    parcelaCartao.DataParcela = Parcela;
                    Cont--;
                    Parcela = Parcela.AddMonths(1);

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            despesaCartaoBusiness.CriaParcelaCartao(parcelaCartao);
                        }
                        catch (Exception)
                        {

                            return Json("naoCadastrou");
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    despesaCartaoBusiness.Inserir(despesaCartao);
                    return Json("cadastrou");
                }
                else
                {
                    return Json("naoCadastrou");
                }
            }
            catch (Exception)
            {

                return Json("naoCadastrou");
            }

        }

        [HttpPost]
        public JsonResult VerificaLimite(int IDCartao, Decimal ValorDespesa)
        {
            var limite = despesaCartaoBusiness.limite(IDCartao);
            var gasto = despesaCartaoBusiness.gasto(IDCartao);

            if ((gasto + ValorDespesa) > limite)
            {
                return Json("ok");
            }
            else
            {
                return Json("false");
            }
        }

        // GET: DespesaCartao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var despesaCartao = despesaCartaoBusiness.Alterar(id);

            ViewBag.IDCartao = new SelectList(despesaCartaoBusiness.ListarCartao(User.Identity.Name), "IDCartao", "Nome", despesaCartao.IDCartao);
            ViewBag.IDCatDespesa = new SelectList(despesaCartaoBusiness.ListarCatDespesa(), "IDCatDespesa", "Nome", despesaCartao.IDCatDespesa);

            if (despesaCartao == null)
            {
                return HttpNotFound();
            }

            return PartialView(despesaCartao);
        }

        // POST: DespesaCartao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DespesaCartao despesaCartao)
        {

            despesaCartao.ValorParcela = Decimal.ToDouble(despesaCartao.Valor) / despesaCartao.QtdParcela;

            var diaFechamento = despesaCartaoBusiness.DiaFechamentoCartao(despesaCartao.IDCartao);

            DateTime DataFechamentoVirtual = new DateTime(despesaCartao.DataCompra.Year, despesaCartao.DataCompra.Month, diaFechamento);

            if (despesaCartao.DataCompra >= DataFechamentoVirtual)
            {
                DateTime DataPrimeiraParcela = despesaCartao.DataCompra.AddMonths(1);
                despesaCartao.DataPrimeiraParcela = new DateTime(DataPrimeiraParcela.Year, DataPrimeiraParcela.Month, diaFechamento);

                DateTime DataUltimaParcela = despesaCartao.DataCompra.AddMonths(despesaCartao.QtdParcela);
                despesaCartao.DataUltimaParcela = new DateTime(DataUltimaParcela.Year, DataUltimaParcela.Month, diaFechamento);
            }
            else if (despesaCartao.DataCompra < DataFechamentoVirtual)
            {
                despesaCartao.DataPrimeiraParcela = new DateTime(despesaCartao.DataCompra.Year, despesaCartao.DataCompra.Month, diaFechamento);

                DateTime DataUltimaParcela = despesaCartao.DataCompra.AddMonths(despesaCartao.QtdParcela - 1);
                despesaCartao.DataUltimaParcela = new DateTime(DataUltimaParcela.Year, DataUltimaParcela.Month, diaFechamento);
            }

            ViewBag.IDCartao = new SelectList(despesaCartaoBusiness.ListarCartao(User.Identity.Name), "IDCartao", "Nome", despesaCartao.IDCartao);
            ViewBag.IDCatDespesa = new SelectList(despesaCartaoBusiness.ListarCatDespesa(), "IDCatDespesa", "Nome", despesaCartao.IDCatDespesa);

            if (ModelState.IsValid)
            {
                despesaCartaoBusiness.AlterarConfirmacao(despesaCartao);
                return RedirectToAction("../Dashboard/DespCartao/" + despesaCartao.IDCartao);
            }

            return View(despesaCartao);
        }

        // GET: DespesaCartao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var despesaCartao = despesaCartaoBusiness.Apagar(id);
            if (despesaCartao == null)
            {
                return HttpNotFound();
            }
            return PartialView(despesaCartao);
        }

        // POST: DespesaCartao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            despesaCartaoBusiness.ApagarConfrmacao(id);
            return RedirectToAction("../Dashboard/Cartao");
        }
    }
}

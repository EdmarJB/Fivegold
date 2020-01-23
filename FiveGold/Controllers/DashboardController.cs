using FiveGold.Mapeamento.Business;
using FiveGold.Models.Business;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private DashboardBusiness dashboardBusiness = new DashboardBusiness();
        String[] meses = { "", "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };

        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.saldo = dashboardBusiness.Saldo(User.Identity.Name);
            ViewBag.receita = dashboardBusiness.Receita(User.Identity.Name);
            ViewBag.despesa = dashboardBusiness.Despesa(User.Identity.Name);
            ViewBag.despesaCartao = dashboardBusiness.DespesaCartao(User.Identity.Name);
            ViewBag.mesatual = meses[DateTime.Now.Month];

            if (ResultReceita().Count == 0)
            {
                ViewBag.msgReceita = "Nenhuma Receita";
            }

            if (ResultDespesa().Count == 0)
            {
                ViewBag.msgDespesa = "Nenhuma Despesa";
            }

            return View();
        }

        public ActionResult ListarContas()
        {
            var Contas = dashboardBusiness.listarContas(User.Identity.Name);
            return View(Contas);
        }

        // SALDO
        public ActionResult Contas()
        {
            return View();
        }

        public ActionResult ListarCartoes()
        {
            BandeiraBusiness bandeiraBusiness = new BandeiraBusiness();
            ViewBag.ListarBandeiras = bandeiraBusiness.Listar();

            var cartoes = dashboardBusiness.listarCartoes(User.Identity.Name);
            return View(cartoes);
        }

        public ActionResult Cartao()
        {
            return View();
        }

        public ActionResult DespCartao(int id, int? direcao, DateTime? DataInicio, DateTime? DataFim)
        {
            DespesaCartaoBusiness despesaCartaoBusiness = new DespesaCartaoBusiness();
            
            if (DataInicio == null && DataFim == null && direcao == null)
            {
                DateTime Diafechamento = DateTime.Parse(despesaCartaoBusiness.DiaFechamentoCartao(id) + DateTime.Now.ToString("/MM/yyyy"));
                DateTime Add_1_Mes = Diafechamento.AddMonths(1);
                DateTime UltimoDia = Add_1_Mes.AddDays(-1);

                var DespCartao = dashboardBusiness.ListarDespCartao(id, Diafechamento);
                ViewBag.fatura = dashboardBusiness.BuscarFatura(id, Diafechamento);
                
                if (dashboardBusiness.verificaFaturaPaga(id, Diafechamento))
                    ViewBag.FaturaPaga = 1;

                ViewBag.DataInicial = Diafechamento.ToString("yyyy-MM-dd");
                ViewBag.DataFinal = UltimoDia.ToString("yyyy-MM-dd");
                ViewBag.IDCartao = id;

                if (meses[Diafechamento.Month] == meses[UltimoDia.Month])
                {
                    ViewBag.mes = meses[Diafechamento.Month] + " de " + Diafechamento.Year; ;
                }
                else
                {
                    ViewBag.mes = Diafechamento.Day + " de " + meses[Diafechamento.Month] + " à " + UltimoDia.Day + " de " + meses[UltimoDia.Month];
                }

                ViewBag.dia = UltimoDia.Day;

                return View(DespCartao);

            }
            if (DataInicio != null && DataFim != null && direcao == null)
            {
                DateTime dataInicial = Convert.ToDateTime(DataInicio);
                DateTime datafinal = Convert.ToDateTime(DataFim);

                var DespCartao = dashboardBusiness.ListarDespCartao(id, DataInicio);
                ViewBag.fatura = 0;

                if (dashboardBusiness.verificaFaturaPaga(id, dataInicial))
                    ViewBag.FaturaPaga = 1;

                ViewBag.DataInicial = Convert.ToDateTime(DataInicio).ToString("yyyy-MM-dd");
                ViewBag.DataFinal = Convert.ToDateTime(DataFim).ToString("yyyy-MM-dd");
                ViewBag.IDCartao = id;

                if (meses[dataInicial.Month] == meses[datafinal.Month])
                {
                    ViewBag.mes = dataInicial.Day + " à " + datafinal.Day + " de " + meses[dataInicial.Month];
                }
                else
                {
                    ViewBag.mes = dataInicial.Day + " de " + meses[dataInicial.Month] + " à " + datafinal.Day + " de " + meses[datafinal.Month];
                }

                    return PartialView("_ListaDespCartao", DespCartao);
            }
            if (DataInicio != null && DataFim == null && direcao != null)
            {
                if (direcao == 2)
                {
                    DateTime dataInicial = Convert.ToDateTime(DataInicio);
                    DateTime dataInicialVirtual = new DateTime(dataInicial.Year, dataInicial.Month, despesaCartaoBusiness.DiaFechamentoCartao(id));
                    DateTime Diafechamento = dataInicialVirtual.AddMonths(1);
                    DateTime UltimoDiaproxMes = Diafechamento.AddMonths(1);
                    DateTime UltimoDiadoMes = UltimoDiaproxMes.AddDays(-1);

                    var DespCartao = dashboardBusiness.ListarDespCartao(id, Diafechamento);
                    ViewBag.fatura = dashboardBusiness.BuscarFatura(id, Diafechamento);

                    if (dashboardBusiness.verificaFaturaPaga(id, Diafechamento))
                        ViewBag.FaturaPaga = 1;

                    ViewBag.DataInicial = Diafechamento.ToString("yyyy-MM-dd");
                    ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                    ViewBag.IDCartao = id;
                    ViewBag.dia = UltimoDiadoMes.Day;

                    if (meses[Diafechamento.Month] == meses[UltimoDiadoMes.Month])
                    {
                        ViewBag.mes = meses[Diafechamento.Month] + " de " + Diafechamento.Year;
                    }
                    else
                    {
                        ViewBag.mes = Diafechamento.Day + " de " + meses[Diafechamento.Month] + " à " + UltimoDiadoMes.Day + " de " + meses[UltimoDiadoMes.Month];
                    }

                    return PartialView("_ListaDespCartao", DespCartao);
                }
                else
                {
                    DateTime dataInicial = Convert.ToDateTime(DataInicio);
                    DateTime dataInicialVirtual = new DateTime(dataInicial.Year, dataInicial.Month, despesaCartaoBusiness.DiaFechamentoCartao(id));
                    DateTime Diafechamento = dataInicialVirtual.AddMonths(-1);
                    DateTime UltimoDiaproxMes = Diafechamento.AddMonths(1);
                    DateTime UltimoDiadoMes = UltimoDiaproxMes.AddDays(-1);

                    var DespCartao = dashboardBusiness.ListarDespCartao(id, Diafechamento);
                    ViewBag.fatura = dashboardBusiness.BuscarFatura(id, Diafechamento);

                    if (dashboardBusiness.verificaFaturaPaga(id, Diafechamento))
                        ViewBag.FaturaPaga = 1;

                    ViewBag.DataInicial = Diafechamento.ToString("yyyy-MM-dd");
                    ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                    ViewBag.IDCartao = id;
                    ViewBag.dia = UltimoDiadoMes.Day;

                    if (meses[Diafechamento.Month] == meses[UltimoDiadoMes.Month])
                    {
                        ViewBag.mes = meses[Diafechamento.Month] + " de " + Diafechamento.Year;
                    }
                    else
                    {
                        ViewBag.mes = Diafechamento.Day + " de " + meses[Diafechamento.Month] + " à " + UltimoDiadoMes.Day + " de " + meses[UltimoDiadoMes.Month];
                    }

                    return PartialView("_ListaDespCartao", DespCartao);
                }
               
            }
            if (DataInicio != null && DataFim == null && direcao == null)
            {
                DateTime Diafechamento = Convert.ToDateTime(DataInicio);
                DateTime Add_1_Mes = Diafechamento.AddMonths(1);
                DateTime UltimoDia = Add_1_Mes.AddDays(-1);

                var DespCartao = dashboardBusiness.ListarDespCartao(id, Diafechamento);
                ViewBag.fatura = dashboardBusiness.BuscarFatura(id, Diafechamento);

                if (dashboardBusiness.verificaFaturaPaga(id, Diafechamento))
                    ViewBag.FaturaPaga = 1;

                ViewBag.DataInicial = Diafechamento.ToString("yyyy-MM-dd");
                ViewBag.DataFinal = UltimoDia.ToString("yyyy-MM-dd");
                ViewBag.IDCartao = id;

                if (meses[Diafechamento.Month] == meses[UltimoDia.Month])
                {
                    ViewBag.mes = meses[Diafechamento.Month] + " de " + Diafechamento.Year;
                }
                else
                {
                    ViewBag.mes = Diafechamento.Day + " de " + meses[Diafechamento.Month] + " à " + UltimoDia.Day + " de " + meses[UltimoDia.Month];
                }

                ViewBag.dia = UltimoDia.Day;

                return View(DespCartao);

            }

            return PartialView();
        }

        public ActionResult Receitas(int? direcao, DateTime? DataInicio, DateTime? DataFim)
        {
            CatReceitaBusiness catReceitaBusiness = new CatReceitaBusiness();

            if (DataInicio == null || DataFim == null && direcao == null)
            {
                DateTime PrimeiroDiadoMes = DateTime.Parse("01" + DateTime.Now.ToString("/MM/yyyy"));
                DateTime PrimeiroDiadoProximoMes = PrimeiroDiadoMes.AddMonths(1);
                DateTime UltimoDiadoMes = PrimeiroDiadoProximoMes.AddDays(-1);

                var receitas = dashboardBusiness.listarReceitas(User.Identity.Name, PrimeiroDiadoMes, UltimoDiadoMes);
                ViewBag.CatReceitas = catReceitaBusiness.Listar();

                ViewBag.DataInicial = PrimeiroDiadoMes.ToString("yyyy-MM-dd");
                ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                ViewBag.mes = meses[PrimeiroDiadoMes.Month] + " de " + PrimeiroDiadoMes.Year;

                return View(receitas);
            }

            if (DataInicio != null && DataFim != null && direcao == null)
            {
                DateTime dataInicial = Convert.ToDateTime(DataInicio);
                DateTime datafinal = Convert.ToDateTime(DataFim);

                var receitas = dashboardBusiness.listarReceitas(User.Identity.Name, DataInicio, DataFim);
                ViewBag.CatReceitas = catReceitaBusiness.Listar();

                ViewBag.DataInicial = dataInicial.ToString("yyyy-MM-dd");
                ViewBag.DataFinal = datafinal.ToString("yyyy-MM-dd");
                

                if (meses[dataInicial.Month] == meses[datafinal.Month])
                {
                    ViewBag.mes = dataInicial.Day + " à " + datafinal.Day + " de " + meses[dataInicial.Month];
                }
                else
                {
                    ViewBag.mes = dataInicial.Day + " de " + meses[dataInicial.Month] + " à " + datafinal.Day + " de " + meses[datafinal.Month];
                }

                return PartialView("_ListaReceita", receitas);
            }

            if (DataInicio != null && DataFim == null && direcao != null)
            {
                if (direcao == 2)
                {
                    DateTime dataInicial = Convert.ToDateTime(DataInicio);
                    DateTime datat = dataInicial.AddMonths(1);
                    DateTime dataInicialVirtual = new DateTime(datat.Year, datat.Month, 01);
                    DateTime PrimeiroDiadoProximoMes = dataInicialVirtual.AddMonths(1);
                    DateTime UltimoDiadoMes = PrimeiroDiadoProximoMes.AddDays(-1);

                    var receitas = dashboardBusiness.listarReceitas(User.Identity.Name, dataInicialVirtual, UltimoDiadoMes);
                    ViewBag.CatReceitas = catReceitaBusiness.Listar();

                    ViewBag.DataInicial = dataInicialVirtual.ToString("yyyy-MM-dd");
                    ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                    ViewBag.mes = meses[dataInicialVirtual.Month] + " de " + dataInicialVirtual.Year;

                    return PartialView("_ListaReceita", receitas);
                }
                else
                {
                    DateTime dataInicial = Convert.ToDateTime(DataInicio);
                    DateTime datat = dataInicial.AddMonths(-1);
                    DateTime dataInicialVirtual = new DateTime(datat.Year, datat.Month, 01);
                    DateTime PrimeiroDiadoProximoMes = dataInicialVirtual.AddMonths(1);
                    DateTime UltimoDiadoMes = PrimeiroDiadoProximoMes.AddDays(-1);

                    var receitas = dashboardBusiness.listarReceitas(User.Identity.Name, dataInicialVirtual, UltimoDiadoMes);
                    ViewBag.CatReceitas = catReceitaBusiness.Listar();

                    ViewBag.DataInicial = dataInicialVirtual.ToString("yyyy-MM-dd");
                    ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                    ViewBag.mes = meses[dataInicialVirtual.Month] + " de " + dataInicialVirtual.Year;

                    return PartialView("_ListaReceita", receitas);
                }
            }

            return PartialView();
        }

        public ActionResult Despesas(int? direcao, DateTime? DataInicio, DateTime? DataFim)
        {
            CatDespesaBusiness catDespesaBusiness = new CatDespesaBusiness();

            if (DataInicio == null || DataFim == null && direcao == null)
            {
                DateTime PrimeiroDiadoMes = DateTime.Parse("01" + DateTime.Now.ToString("/MM/yyyy"));
                DateTime PrimeiroDiadoProximoMes = PrimeiroDiadoMes.AddMonths(1);
                DateTime UltimoDiadoMes = PrimeiroDiadoProximoMes.AddDays(-1);

                var despesas = dashboardBusiness.listarDespesas(User.Identity.Name, PrimeiroDiadoMes, UltimoDiadoMes);
                ViewBag.CatDespesas = catDespesaBusiness.Listar();

                ViewBag.DataInicial = PrimeiroDiadoMes.ToString("yyyy-MM-dd");
                ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                ViewBag.mes = meses[PrimeiroDiadoMes.Month] + " de " + PrimeiroDiadoMes.Year;

                return View(despesas);
            }

            if (DataInicio != null && DataFim != null && direcao == null)
            {
                DateTime dataInicial = Convert.ToDateTime(DataInicio);
                DateTime datafinal = Convert.ToDateTime(DataFim);

                var despesas = dashboardBusiness.listarDespesas(User.Identity.Name, DataInicio, DataFim);
                ViewBag.CatDespesas = catDespesaBusiness.Listar();

                ViewBag.DataInicial = dataInicial.ToString("yyyy-MM-dd");
                ViewBag.DataFinal = datafinal.ToString("yyyy-MM-dd");
                

                if (meses[dataInicial.Month] == meses[datafinal.Month])
                {
                    ViewBag.mes = dataInicial.Day + " à " + datafinal.Day + " de " + meses[dataInicial.Month];
                }
                else
                {
                    ViewBag.mes = dataInicial.Day + " de " + meses[dataInicial.Month] + " à " + datafinal.Day + " de " + meses[datafinal.Month];
                }
                return PartialView("_ListaDespesa", despesas);
            }

            if (DataInicio != null && DataFim == null && direcao != null)
            {
                if (direcao == 2)
                {
                    DateTime dataInicial = Convert.ToDateTime(DataInicio);
                    DateTime datat = dataInicial.AddMonths(1);
                    DateTime dataInicialVirtual = new DateTime(datat.Year, datat.Month, 01);
                    DateTime PrimeiroDiadoProximoMes = dataInicialVirtual.AddMonths(1);
                    DateTime UltimoDiadoMes = PrimeiroDiadoProximoMes.AddDays(-1);

                    var despesas = dashboardBusiness.listarDespesas(User.Identity.Name, dataInicialVirtual, UltimoDiadoMes);
                    ViewBag.CatDespesas = catDespesaBusiness.Listar();

                    ViewBag.DataInicial = dataInicialVirtual.ToString("yyyy-MM-dd");
                    ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                    ViewBag.mes = meses[dataInicialVirtual.Month] + " de " + dataInicialVirtual.Year;

                    return PartialView("_ListaDespesa", despesas);
                }
                else
                {
                    DateTime dataInicial = Convert.ToDateTime(DataInicio);
                    DateTime datat = dataInicial.AddMonths(-1);
                    DateTime dataInicialVirtual = new DateTime(datat.Year, datat.Month, 01);
                    DateTime PrimeiroDiadoProximoMes = dataInicialVirtual.AddMonths(1);
                    DateTime UltimoDiadoMes = PrimeiroDiadoProximoMes.AddDays(-1);

                    var despesas = dashboardBusiness.listarDespesas(User.Identity.Name, dataInicialVirtual, UltimoDiadoMes);
                    ViewBag.CatDespesas = catDespesaBusiness.Listar();

                    ViewBag.DataInicial = dataInicialVirtual.ToString("yyyy-MM-dd");
                    ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                    ViewBag.mes = meses[dataInicialVirtual.Month] + " de " + dataInicialVirtual.Year;

                    return PartialView("_ListaDespesa", despesas);
                }
            }

            return PartialView();
        }

        public ActionResult Transferencias(int? direcao, DateTime? DataInicio, DateTime? DataFim)
        {

            if (DataInicio == null || DataFim == null && direcao == null)
            {
                DateTime PrimeiroDiadoMes = DateTime.Parse("01" + DateTime.Now.ToString("/MM/yyyy"));
                DateTime PrimeiroDiadoProximoMes = PrimeiroDiadoMes.AddMonths(1);
                DateTime UltimoDiadoMes = PrimeiroDiadoProximoMes.AddDays(-1);

                var transferencias = dashboardBusiness.listarTransferencias(User.Identity.Name, PrimeiroDiadoMes, UltimoDiadoMes);

                ViewBag.ListaNomeContas = dashboardBusiness.listarContas(User.Identity.Name);
                ViewBag.DataInicial = PrimeiroDiadoMes.ToString("yyyy-MM-dd");
                ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                ViewBag.mes = meses[PrimeiroDiadoMes.Month] + " de " + PrimeiroDiadoMes.Year;

                return View(transferencias);
            }
            if (DataInicio != null && DataFim != null && direcao == null)
            {
                DateTime dataInicial = Convert.ToDateTime(DataInicio);
                DateTime datafinal = Convert.ToDateTime(DataFim);

                var transferencias = dashboardBusiness.listarTransferencias(User.Identity.Name, DataInicio, DataFim);

                ViewBag.ListaNomeContas = dashboardBusiness.listarContas(User.Identity.Name);
                ViewBag.DataInicial = dataInicial.ToString("yyyy-MM-dd");
                ViewBag.DataFinal = datafinal.ToString("yyyy-MM-dd");
                

                if (meses[dataInicial.Month] == meses[datafinal.Month])
                {
                    ViewBag.mes = dataInicial.Day + " à " + datafinal.Day + " de " + meses[dataInicial.Month];
                }
                else
                {
                    ViewBag.mes = dataInicial.Day + " de " + meses[dataInicial.Month] + " à " + datafinal.Day + " de " + meses[datafinal.Month];
                }

                return PartialView("_ListaTransferencia", transferencias);
            }

            if (DataInicio != null && DataFim == null && direcao != null)
            {
                if (direcao == 2)
                {
                    DateTime dataInicial = Convert.ToDateTime(DataInicio);
                    DateTime datat = dataInicial.AddMonths(1);
                    DateTime dataInicialVirtual = new DateTime(datat.Year, datat.Month, 01);
                    DateTime PrimeiroDiadoProximoMes = dataInicialVirtual.AddMonths(1);
                    DateTime UltimoDiadoMes = PrimeiroDiadoProximoMes.AddDays(-1);

                    var transferencias = dashboardBusiness.listarTransferencias(User.Identity.Name, dataInicialVirtual, UltimoDiadoMes);

                    ViewBag.ListaNomeContas = dashboardBusiness.listarContas(User.Identity.Name);
                    ViewBag.DataInicial = dataInicialVirtual.ToString("yyyy-MM-dd");
                    ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                    ViewBag.mes = meses[dataInicialVirtual.Month] + " de " + dataInicialVirtual.Year;

                    return PartialView("_ListaTransferencia", transferencias);
                }
                else
                {
                    DateTime dataInicial = Convert.ToDateTime(DataInicio);
                    DateTime datat = dataInicial.AddMonths(-1);
                    DateTime dataInicialVirtual = new DateTime(datat.Year, datat.Month, 01);
                    DateTime PrimeiroDiadoProximoMes = dataInicialVirtual.AddMonths(1);
                    DateTime UltimoDiadoMes = PrimeiroDiadoProximoMes.AddDays(-1);

                    var transferencias = dashboardBusiness.listarTransferencias(User.Identity.Name, dataInicialVirtual, UltimoDiadoMes);

                    ViewBag.ListaNomeContas = dashboardBusiness.listarContas(User.Identity.Name);
                    ViewBag.DataInicial = dataInicialVirtual.ToString("yyyy-MM-dd");
                    ViewBag.DataFinal = UltimoDiadoMes.ToString("yyyy-MM-dd");
                    ViewBag.mes = meses[dataInicialVirtual.Month] + " de " + dataInicialVirtual.Year;

                    return PartialView("_ListaTransferencia", transferencias);
                }
            }

            return PartialView();
        }

        public ActionResult ReceitaMensal()
        {
            return Json(ResultReceita(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DespesaMensal()
        {
            return Json(ResultDespesa(), JsonRequestBehavior.AllowGet);
        }

        public List<ReceitaMensal> ResultDespesa()
        {
            return dashboardBusiness.Despesamensal(User.Identity.Name);
        }

        public List<ReceitaMensal> ResultReceita()
        {
            return dashboardBusiness.Receitamensal(User.Identity.Name);
        }

        public ActionResult GraficoReDe()
        {
            return Json(Grafico(), JsonRequestBehavior.AllowGet);
        }

        public List<GraficoReDe> Grafico()
        {
            int r = 0;
            int d = 0;

            DateTime DataAtual = DateTime.Now.AddMonths(-5);
            DateTime Data = DateTime.Parse("01" + DataAtual.ToString("/MM/yyyy"));

            var receita = dashboardBusiness.ColunaReceita(User.Identity.Name, Data);
            var despesa = dashboardBusiness.ColunaDespesa(User.Identity.Name, Data);

            List<GraficoReDe> lista = new List<GraficoReDe>();

            foreach (var re in receita)
            {
                foreach (var de in despesa)
                {
                    if (re.Mes == de.Mes)
                    {
                        lista.Add(new GraficoReDe()
                        {
                            ValorReceita = re.Valor,
                            ValorDespesa = de.Valor,
                            Mes = re.Mes,
                            Ano = re.Ano
                        });

                        r++;
                    }
                }
                if (r == 0)
                {
                    lista.Add(new GraficoReDe()
                    {
                        ValorReceita = re.Valor,
                        ValorDespesa = 0,
                        Mes = re.Mes,
                        Ano = re.Ano
                    });
                }
                r = 0;
            }

            foreach (var de in despesa)
            {
                foreach (var re in receita)
                {
                    if (de.Mes == re.Mes)
                    {
                        d++;
                    }
                }
                if (d == 0)
                {
                    lista.Add(new GraficoReDe()
                    {
                        ValorReceita = 0,
                        ValorDespesa = de.Valor,
                        Mes = de.Mes,
                        Ano = de.Ano
                    });
                }
                d = 0;
            }

            return lista = lista.OrderBy(l => l.Ano).OrderBy(l => l.Mes).ToList();
        }

        [Authorize(Users = "fivegoldfinance@hotmail.com")]
        public ActionResult PainelADM()
        {
            return View();
        }

        [HttpPost]
        public JsonResult VerificaDespesas(int IDCartao)
        {
            List<DespesaCartao> lista = new List<DespesaCartao>();

            try
            {
                lista = dashboardBusiness.ListarQtdDespesas(IDCartao);

                if (lista.Count() > 0)
                {
                    return Json("ok");
                }
                else
                {
                    return Json("false");
                }
            }
            catch (Exception)
            {
                return Json("erro");
            }

        }

        [HttpPost]
        public JsonResult ApagarDespesas(int IDCartao)
        {
            try
            {
                dashboardBusiness.ApagarDespesasCartao(IDCartao);
                return Json("ok");
            }
            catch (Exception)
            {
                return Json("erro");
            }

        }
    }
}
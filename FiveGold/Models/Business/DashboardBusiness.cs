using Dapper;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiveGold.Models.Business
{
    public class DashboardBusiness : Conexao, IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public Decimal Saldo(String UserId)
        {
            try
            {
                var Saldo = conn.Query<Decimal>(@"
                select sum(Saldo) 
                from Contas where UserId = @User", new { User = UserId }).SingleOrDefault();

                return Saldo;
            }
            catch (Exception ex)
            {
                var e = ex;
                return 0;
            }
        }

        public Double Receita(String UserId)
        {
            try
            {
                var receita = conn.Query<Double>(@"
                select sum(Valor) 
                from Receitas where UserId = @User AND 
                MONTH(Data) = MONTH(GETDATE()) and year(Data) = YEAR(GETDATE())", new { User = UserId }).SingleOrDefault();

                return receita;
            }
            catch (Exception ex)
            {
                var e = ex;
                return 0;
            }
        }

        public Double Despesa(String UserId)
        {
            try
            {
                var despesa = conn.Query<Double>(@"
                select sum(Valor) 
                from Despesas where UserId = @User AND 
                MONTH(Data) = MONTH(GETDATE()) and year(Data) = YEAR(GETDATE())", new { User = UserId }).SingleOrDefault();

                return despesa;
            }
            catch (Exception ex)
            {
                var e = ex;
                return 0;
            }
        }

        public Decimal DespesaCartao(String UserId)
        {
            try
            {
                var cartao = conn.Query<Decimal>(@"
                select sum(ValorParcela * (
		        CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) < 0 THEN 0 ELSE
		        (CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) > QtdParcela THEN 0 ELSE 
		        (CASE WHEN ((((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= 0) and (((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) <= QtdParcela)) and (cc.DiaFechamento > DAY(getdate())) THEN 1 ELSE 
		        (CASE WHEN ((((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= 1) and (((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) <= QtdParcela)) THEN 1 ELSE
		        (CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) = 0 ) THEN 0
		        END) END)END) END) END)) as Fatura
		        from CartaoCreditoes cc
		        left join DespesaCartaos dc on dc.IDCartao = cc.IDCartao
		        where cc.UserId = @User", new { User = UserId }).SingleOrDefault();

                return cartao;
            }
            catch (Exception ex)
            {
                var e = ex;
                return 0;
            }
        }

        public List<Conta> listarContas(String UserId)
        {
            return db.Conta.Where(u => u.UserId == UserId).ToList();


            //List<Conta> Lista = new List<Conta>();
            //Lista = db.Conta.Where(u => u.UserId == UserId).ToList();
            ////Lista = conn.Query<Conta>(@"select * from Contas where 
            ////                           UserId = @user", new { user = UserId}).ToList();

            //Lista.Add(new Conta() {
            //IDConta = 0,
            //Nome = "[ Selecione ]"});

            //var List = Lista.OrderBy(o => o.Nome);

            //return List.ToList();
        }

        public List<Conta> listarContasPagarFatura(String UserId)
        {
            List<Conta> Lista = new List<Conta>();
            Lista = db.Conta.Where(u => u.UserId == UserId).ToList();
            //Lista = conn.Query<Conta>(@"select * from Contas where 
            //                           UserId = @user", new { user = UserId}).ToList();

            Lista.Add(new Conta()
            {
                IDConta = 0,
                Nome = "[ Selecione ]"
            });

            var List = Lista.OrderBy(o => o.Nome);

            return List.ToList();
        }

        public List<CartaoCredito> listarCartoes(String UserId)
        {
            //return db.CartaoCredito.Where(u => u.UserId == UserId).ToList();

            try
            {
                var cartao = conn.Query<CartaoCredito>(@"
            select sum(ValorParcela * 
			(CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) < 0 THEN 0 ELSE
			(CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= QtdParcela THEN QtdParcela ELSE 
			(CASE WHEN ((((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= 0) and (((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) <= QtdParcela)) and (cc.DiaFechamento > DAY(getdate())) THEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) +1) ELSE 
			(CASE WHEN ((((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= 1) and (((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) <= QtdParcela))  THEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) )
			 END) END) END) END)) as Gasto,
             sum(ValorParcela * (
			 CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) < 0 THEN 0 ELSE
			(CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) > QtdParcela THEN 0 ELSE 
			(CASE WHEN ((((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= 0) and (((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) <= QtdParcela)) and (cc.DiaFechamento > DAY(getdate())) THEN 1 ELSE 
			(CASE WHEN ((((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= 1) and (((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) <= QtdParcela)) THEN 1 ELSE
			(CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) = 0 ) THEN 0
			 END) END)END) END) END)) as Fatura,
			cc.Nome, cc.Limite, cc.DiaFechamento, cc.DiaPagamento, cc.IDCartao, cc.IDBandeira,
			((sum(ValorParcela * 
			(CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) < 0 THEN 0 ELSE
			(CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= QtdParcela THEN QtdParcela ELSE 
			(CASE WHEN ((((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= 0) and (((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) <= QtdParcela)) and (cc.DiaFechamento > DAY(getdate())) THEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) +1) ELSE 
			(CASE WHEN ((((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= 1) and (((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) <= QtdParcela))  THEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) )
			 END) END) END) END)) * 100) / cc.Limite) as Porcentagem
			from CartaoCreditoes cc
            left join DespesaCartaos dc on dc.IDCartao = cc.IDCartao
			where cc.UserId = @User
			group By cc.Nome, cc.Limite, cc.DiaFechamento, cc.DiaPagamento, cc.IDCartao, cc.IDBandeira", new { User = UserId }).ToList();

                return cartao;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Receita> listarReceitas(String UserId, DateTime? PrimeiroDiadoMes, DateTime? UltimoDiadoMes)
        {
            try
            {
                return conn.Query<Receita>(@"
                select * from Receitas 
                where  UserId =  @User and 
                Data between @Pdia and @Udia
                order by Data desc", new { User = UserId, Pdia = PrimeiroDiadoMes, Udia = UltimoDiadoMes }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Despesa> listarDespesas(String UserId, DateTime? PrimeiroDiadoMes, DateTime? UltimoDiadoMes)
        {
            try
            {
                return conn.Query<Despesa>(@"
                select * from Despesas 
                where  UserId =  @User and 
                Data between @Pdia and @Udia
                order by Data desc", new { User = UserId, Pdia = PrimeiroDiadoMes, Udia = UltimoDiadoMes }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Transferencia> listarTransferencias(String UserId, DateTime? PrimeiroDiadoMes, DateTime? UltimoDiadoMes)
        {
            try
            {
                return conn.Query<Transferencia>(@"
                select * from Transferencias 
                where  UserId =  @User and 
                DataTransferencia between @Pdia and @Udia
                order by DataTransferencia desc", new { User = UserId, Pdia = PrimeiroDiadoMes, Udia = UltimoDiadoMes }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<DespesaCartao> ListarDespCartao(int Id, DateTime? DataInicial/*, DateTime? UltimoDia*/)
        {
            try
            {
                return conn.Query<DespesaCartao>(@"
                select dc.Descricao, dc.DataCompra, dc.QtdParcela, dc.ValorParcela, dc.IDDespCartao, (DATEDIFF(month, @DataIni, DataUltimaParcela)) as Parcela 
                from CartaoCreditoes cc left join DespesaCartaos dc on dc.IDCartao = cc.IDCartao where 
                (CASE WHEN ((DATEDIFF(month, @DataIni, DataUltimaParcela)) ) < 0 THEN 0 ELSE
                (CASE WHEN ((DATEDIFF(month, @DataIni, DataUltimaParcela)) ) > QtdParcela THEN 0 ELSE 
                (CASE WHEN ((((DATEDIFF(month, @DataIni, DataUltimaParcela)) ) >= 0) and (((DATEDIFF(month, @DataIni, DataUltimaParcela)) ) <= QtdParcela)) and (cc.DiaFechamento > DAY(@DataIni)) THEN 1 ELSE 
                (CASE WHEN ((((DATEDIFF(month, @DataIni, DataUltimaParcela)) ) >= 1) and (((DATEDIFF(month, @DataIni, DataUltimaParcela)) ) <= QtdParcela)) THEN 1 ELSE
                (CASE WHEN ((DATEDIFF(month, @DataIni, DataUltimaParcela)) = 0 ) THEN 0
	            END) END)END) END) END) between 1 and QtdParcela and cc.IDCartao = @cartao
                ", new { cartao = Id, DataIni = DataInicial }).ToList();

                //return conn.Query<DespesaCartao>(@"
                //select * from DespesaCartaos 
                //where  IDCartao =  @IDCartao and 
                //DataCompra between @Pdia and @Udia
                //order by DataCompra desc
                //", new { IDCartao = Id, Pdia = PrimeiroDia, Udia = UltimoDia }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ReceitaMensal> Receitamensal(String UserId)
        {
            try
            {
                return conn.Query<ReceitaMensal>(@"
                select cr.Nome, sum(r.Valor) as Valor from Receitas r
                inner join CatReceitas cr on r.IDCatReceita = cr.IDCatReceita
                where r.UserId = @User and MONTH(Data) = MONTH(getdate()) and YEAR(Data) = YEAR(getdate())
                group by cr.Nome order by Valor desc", new { User = UserId }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ReceitaMensal> Despesamensal(String UserId)
        {
            try
            {
                return conn.Query<ReceitaMensal>(@"
                select cd.Nome, sum(d.Valor) as Valor from Despesas d
                inner join CatDespesas cd on d.IDCatDespesa = cd.IDCatDespesa
                where d.UserId = @User and MONTH(Data) = MONTH(getdate()) and YEAR(Data) = YEAR(getdate())
                group by cd.Nome order by Valor desc", new { User = UserId }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<GraficoColuna> ColunaReceita(String UserId, DateTime Data)
        {
            try
            {
                return conn.Query<GraficoColuna>(@"
                select sum(Valor) as Valor, MONTH(Data) as Mes, YEAR(Data) as Ano from Receitas
				where UserId = @User AND Data between @data and GETDATE()
				group by MONTH(Data), YEAR(Data)", new { User = UserId, data = Data }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<GraficoColuna> ColunaDespesa(String UserId, DateTime Data)
        {
            try
            {
                return conn.Query<GraficoColuna>(@"
                select sum(Valor) as Valor, MONTH(Data) as Mes, YEAR(Data) as Ano from Despesas
				where UserId = @User AND Data between @data and GETDATE()
				group by MONTH(Data), YEAR(Data)", new { User = UserId, data = Data }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<DespesaCartao> ListarQtdDespesas(int IDCartao)
        {
            try
            {
                return conn.Query<DespesaCartao>(@"
                    select * from DespesaCartaos where IDCartao = @cartao
                    ", new { cartao = IDCartao }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ApagarDespesasCartao(int IDCartao)
        {
            try
            {
                conn.Execute(@"delete from DespesaCartaos where IDCartao = @cartao
            ", new { cartao = IDCartao });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Decimal BuscarFatura(int IDCartao, DateTime dataa)
        {
            try
            {
                return conn.Query<Decimal>(@"
             select sum(ValorParcela * (
			 CASE WHEN ((DATEDIFF(month, @data, DataUltimaParcela)) ) < 0 THEN 0 ELSE
			(CASE WHEN ((DATEDIFF(month, @data, DataUltimaParcela)) ) > QtdParcela THEN 0 ELSE 
			(CASE WHEN ((((DATEDIFF(month, @data, DataUltimaParcela)) ) >= 0) and (((DATEDIFF(month, @data, DataUltimaParcela)) ) <= QtdParcela)) and (cc.DiaFechamento > DAY(@data)) THEN 1 ELSE 
			(CASE WHEN ((((DATEDIFF(month, @data, DataUltimaParcela)) ) >= 1) and (((DATEDIFF(month, @data, DataUltimaParcela)) ) <= QtdParcela)) THEN 1 ELSE
			(CASE WHEN ((DATEDIFF(month, @data, DataUltimaParcela)) = 0 ) THEN 0
			 END) END)END) END) END)) as Fatura
			from CartaoCreditoes cc
            left join DespesaCartaos dc on dc.IDCartao = cc.IDCartao
			where cc.IDCartao = @id", new { id = IDCartao, data = dataa }).SingleOrDefault();
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public Boolean SetarFaturaPaga(int IDCartao, DateTime DataInicial)
        {
            try
            {
                conn.Execute(@"update ParcelaCartaos set Pago = 'true'
                                where DataParcela = @Data and 
                                IDCartao = @cartao", new { cartao = IDCartao, Data = DataInicial });

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void SetarFaturaNaoPaga(int IDCartao, DateTime DataInicial)
        {
            conn.Execute(@"update ParcelaCartaos set Pago = 'false'
                            where DataParcela = @Data and 
                            IDCartao = @cartao", new { cartao = IDCartao, Data = DataInicial });

        }

        public Boolean verificaFaturaPaga(int IDCartao, DateTime DataParcela)
        {
            try
            {
                //List<DespesaCartao> lista = new List<DespesaCartao>();
                var lista = conn.Query<int>(@"select COUNT(Pago) from ParcelaCartaos where Pago = 'true' 
                and DataParcela = @data and IDCartao = @cartao", new { cartao = IDCartao, data = DataParcela }).SingleOrDefault();

                if (lista > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public String Verificacartao(int id)
        {
            return conn.Query<String>(@"select Nome from CartaoCreditoes where IDCartao = @cartao", new {cartao = id }).SingleOrDefault();
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
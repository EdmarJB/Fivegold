using Dapper;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FiveGold.Models.Business
{
    public class DespesaCartaoBusiness : Conexao, IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<DespesaCartao> Listar(int id)
        {
            var despesaCartaos = db.DespesaCartao.Include(d => d.CartaoCredito).Include(d => d.CatDespesa).Where(i => i.IDCartao == id);
            return despesaCartaos.ToList();
        }

        public Decimal limite(int IdCartao)
        {
            return conn.Query<Decimal>(@"
                select Limite 
                from CartaoCreditoes where IDCartao = @idCartao", new { idCartao = IdCartao }).SingleOrDefault();
        }

        public Decimal gasto(int IdCartao)
        {
            try
            {
                return conn.Query<Decimal>(@"
                select sum(ValorParcela * 
			    (CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) < 0 THEN 0 ELSE
			    (CASE WHEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= QtdParcela THEN QtdParcela ELSE 
			    (CASE WHEN ((((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= 0) and (((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) <= QtdParcela)) and (cc.DiaFechamento > DAY(getdate())) THEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) +1) ELSE 
			    (CASE WHEN ((((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) >= 1) and (((DATEDIFF(month, GETDATE(), DataUltimaParcela)) ) <= QtdParcela))  THEN ((DATEDIFF(month, GETDATE(), DataUltimaParcela)) )
			     END) END) END) END)) as Gasto
			    from CartaoCreditoes cc
                left join DespesaCartaos dc on dc.IDCartao = cc.IDCartao
			    where cc.IDCartao = @idCartao", new { idCartao = IdCartao }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                var exe = ex;
                return 0;
            }
        }

        public DespesaCartao Buscar(int? id)
        {
            DespesaCartao despesaCartao = db.DespesaCartao.Find(id);
            return despesaCartao;
        }

        //---------------INSERIR------------------

        public List<CartaoCredito> ListarCartao(String User)
        {
            var cc = db.CartaoCredito.Where(c => c.UserId == User).ToList();
            cc.Add(new CartaoCredito
            {
                IDCartao = 0,
                Nome = "[ Selecione ]"
            });

            return cc = cc.OrderBy(d => d.Nome).ToList();
        }

        public List<CatDespesa> ListarCatDespesa()
        {
            var cd = db.CatDespesa.ToList();
            cd.Add(new CatDespesa
            {
                IDCatDespesa = 0,
                Nome = "[ Selecione ]"
            });

            return cd = cd.OrderBy(d => d.Nome).ToList();
        }

        public void Inserir(DespesaCartao despesaCartao)
        {
            db.DespesaCartao.Add(despesaCartao);
            db.SaveChanges();
        }

        //---------------ALTERAR------------------

        public DespesaCartao Alterar(int? id)
        {
            DespesaCartao despesaCartao = db.DespesaCartao.Find(id);
            return despesaCartao;
        }

        public void AlterarConfirmacao(DespesaCartao despesaCartao)
        {
            db.Entry(despesaCartao).State = EntityState.Modified;
            db.SaveChanges();
        }

        //---------------APAGAR-------------------

        public DespesaCartao Apagar(int? id)
        {
            DespesaCartao despesaCartao = db.DespesaCartao.Find(id);
            return despesaCartao;
        }

        public void ApagarConfrmacao(int id)
        {
            DespesaCartao despesaCartao = db.DespesaCartao.Find(id);
            db.DespesaCartao.Remove(despesaCartao);
            db.SaveChanges();
        }

        public int DiaFechamentoCartao(int IdCartao)
        {
            return conn.Query<int>(@"
                select DiaFechamento 
                from CartaoCreditoes where IDCartao = @idCartao", new { idCartao = IdCartao }).SingleOrDefault();
        }

        public void CriaParcelaCartao(ParcelaCartao parcelaCartao)
        {
            try
            {
                db.ParcelaCartaos.Add(parcelaCartao);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //---------------DISPOSABLE---------------

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
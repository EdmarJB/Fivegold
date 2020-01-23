using Dapper;
using FiveGold.Models;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FiveGold.Mapeamento.Business
{
    public class DespesaBusiness : Conexao, IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<Despesa> Listar(String UserId)
        {
            var despesa = db.Despesa.Include(d => d.CatDespesa).Include(d => d.Conta).Where(u => u.UserId == UserId).OrderByDescending(o => o.Data);
            return despesa.ToList();
        }

        public Despesa Buscar(int? id)
        {
            Despesa despesa = db.Despesa.Find(id);
            return despesa;
        }

        //--------------INSERIR-------------------

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

        public List<Conta> ListarConta(String User)
        {
            var c = db.Conta.Where(u => u.UserId == User).ToList();
            c.Add(new Conta
            {
                IDConta = 0,
                Nome = "[ Selecione ]"
            });

            return c = c.OrderBy(d => d.Nome).ToList();
        }

        public Boolean Inserir(Despesa despesa)
        {
            try
            {
                db.Despesa.Add(despesa);
                db.SaveChanges();

                var valor = despesa.Valor;
                var IdConta = despesa.IDConta;

                var Update = conn.Query<int>(@"Update Contas SET Saldo = Saldo - @valor 
                 WHERE IDConta = @IdConta", new { valor = valor, IdConta = IdConta }).SingleOrDefault();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //--------------ALTERAR-------------------

        public Despesa Alterar(int? id)
        {
            Despesa despesa = db.Despesa.Find(id);
            return despesa;
        }

        public void AlterarConfirmacao(Despesa despesa)
        {
            db.Entry(despesa).State = EntityState.Modified;
            db.SaveChanges();
        }

        //--------------APAGAR--------------------

        public Despesa Apagar(int? id)
        {
            Despesa despesa = db.Despesa.Find(id);
            return despesa;
        }

        public void ApagarConfirmar(int id)
        {
            Despesa despesa = db.Despesa.Find(id);
            db.Despesa.Remove(despesa);
            db.SaveChanges();
        }

        //---------------DISPOSABLE---------------

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
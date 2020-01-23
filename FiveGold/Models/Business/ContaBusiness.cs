using FiveGold.Models;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FiveGold.Mapeamento.Business
{
    public class ContaBusiness : IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<Conta> Listar()
        {
            var conta = db.Conta.Include(c => c.CatConta);
            return conta.ToList();
        }

        public Conta Buscar(int? id)
        {
            Conta conta = db.Conta.Find(id);
            return conta;
        }

        //---------------INSERIR------------------

        public List<CatConta> ListarCatContas()
        {
            return db.CatContas.ToList();
        }

        public void Inserir(Conta conta)
        {
            db.Conta.Add(conta);
            db.SaveChanges();
        }

        //--------------ALTERAR-------------------

        public Conta Alterar(int? id)
        {
            Conta conta = db.Conta.Find(id);
            return conta;
        }

        public void AlterarConfirmacao(Conta conta)
        {
            db.Entry(conta).State = EntityState.Modified;
            db.SaveChanges();
        }

        //---------------APAGAR-------------------
        public Conta Apagar(int? id)
        {
            Conta conta = db.Conta.Find(id);
            return conta;
        }

        public void ApagarConfirmacao(int id)
        {
            Conta conta = db.Conta.Find(id);
            db.Conta.Remove(conta);
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
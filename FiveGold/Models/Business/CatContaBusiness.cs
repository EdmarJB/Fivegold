using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FiveGold.Models.Business
{
    public class CatContaBusiness : IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<CatConta> Listar()
        {
            return db.CatContas.ToList();
        }

        public CatConta Buscar(int? id)
        {
            CatConta catConta = db.CatContas.Find(id);
            return catConta;
        }

        //---------------INSERIR------------------

        public void Inserir(CatConta catConta)
        {
            db.CatContas.Add(catConta);
            db.SaveChanges();
        }

        //---------------ALTERAR-----------------

        public CatConta Alterar(int? id)
        {
            CatConta catConta = db.CatContas.Find(id);
            return catConta;
        }

        public void AlterarConfirmacao(CatConta catConta)
        {
            db.Entry(catConta).State = EntityState.Modified;
            db.SaveChanges();
        }

        //----------------APAGAR------------------

        public CatConta Apagar(int? id)
        {
            CatConta catConta = db.CatContas.Find(id);
            return catConta;
        }

        public void ApagarConfirmacao(int? id)
        {
            CatConta catConta = db.CatContas.Find(id);
            db.CatContas.Remove(catConta);
            db.SaveChanges();
        }

        //----------------DISPOSABLE--------------

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
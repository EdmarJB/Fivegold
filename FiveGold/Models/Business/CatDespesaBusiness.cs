using FiveGold.Models;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FiveGold.Mapeamento.Business
{
    public class CatDespesaBusiness :IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<CatDespesa> Listar()
        {
            return db.CatDespesa.ToList();
        }

        public CatDespesa Buscar(int? id)
        {
            CatDespesa catDespesa = db.CatDespesa.Find(id);
            return catDespesa;
        }

        //--------------INSERIR-------------------

        public void Inserir(CatDespesa catDespesa)
        {
            db.CatDespesa.Add(catDespesa);
            db.SaveChanges();
        }

        //---------------ALTERAR--------------------

        public CatDespesa Alterar(int? id)
        {
            CatDespesa catDespesa = db.CatDespesa.Find(id);
            return catDespesa;
        }

        public void AlterarConfirmacao(CatDespesa catDespesa)
        {
            db.Entry(catDespesa).State = EntityState.Modified;
            db.SaveChanges();
        }

        //--------------Apagar--------------------

        public CatDespesa Apagar(int? id)
        {
            CatDespesa catDespesa = db.CatDespesa.Find(id);
            return catDespesa;
        }

        public void ApagarConfirmacao(int id)
        {
            CatDespesa catDespesa = db.CatDespesa.Find(id);
            db.CatDespesa.Remove(catDespesa);
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
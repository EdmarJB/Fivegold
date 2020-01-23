using FiveGold.Models;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FiveGold.Mapeamento.Business
{
    public class CatReceitaBusiness : IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<CatReceita> Listar()
        {
            return db.CategoriaReceita.ToList();
        }

        public CatReceita Buscar(int? id)
        {
            CatReceita catReceita = db.CategoriaReceita.Find(id);
            return catReceita;
        }

        //--------------INSERIR-------------------

        public void Inserir(CatReceita catReceita)
        {
            db.CategoriaReceita.Add(catReceita);
            db.SaveChanges();
        }

        //--------------ALTERAR-------------------

        public CatReceita Alterar(int? id)
        {
            CatReceita catReceita = db.CategoriaReceita.Find(id);
            return catReceita;
        }

        public void AlterarConfirmacao(CatReceita catReceita)
        {
            db.Entry(catReceita).State = EntityState.Modified;
            db.SaveChanges();
        }

        //----------------APAGAR----------------

        public CatReceita Apagar(int? id)
        {
            CatReceita catReceita = db.CategoriaReceita.Find(id);
            return catReceita;
        }

        public void ApagarConfirmacao(int id)
        {
            CatReceita catReceita = db.CategoriaReceita.Find(id);
            db.CategoriaReceita.Remove(catReceita);
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
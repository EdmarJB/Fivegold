using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FiveGold.Models.Business
{
    public class TemaConhecimentoBusiness : IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<TemaConhecimento> Listar()
        {
            return db.TemaConhecimentoes.ToList();
        }

        public TemaConhecimento Buscar(int? id)
        {
            return db.TemaConhecimentoes.Find(id);
        }

        public TemaConhecimento Inserir(TemaConhecimento temaConhecimento)
        {
            db.TemaConhecimentoes.Add(temaConhecimento);
            db.SaveChanges();
            return temaConhecimento;
        }

        public TemaConhecimento Alterar(TemaConhecimento temaConhecimento)
        {
            db.Entry(temaConhecimento).State = EntityState.Modified;
            db.SaveChanges();

            return temaConhecimento;
        }

        public void Remove(TemaConhecimento temaConhecimento)
        {
            db.TemaConhecimentoes.Remove(temaConhecimento);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
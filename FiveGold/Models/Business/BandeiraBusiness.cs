using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FiveGold.Models.Business
{
    public class BandeiraBusiness : IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<Bandeira> Listar()
        {
            return db.Bandeira.ToList();
        }
        public Bandeira Buscar(int? id)
        {
            return db.Bandeira.Find(id);
        }

        public Bandeira Inserir(Bandeira bandeira)
        {

            db.Bandeira.Add(bandeira);
            db.SaveChanges();

            return bandeira;
        }

        public Bandeira Alterar(Bandeira bandeira)
        {
            db.Entry(bandeira).State = EntityState.Modified;
            db.SaveChanges();

            return bandeira;
        }

        internal void Apagar(int id)
        {
            Bandeira bandeira = db.Bandeira.Find(id);
            db.Bandeira.Remove(bandeira);
            db.SaveChanges();
        }

        public void Dispose()
        {
                db.Dispose();
                GC.SuppressFinalize(this);
        }

    }
}
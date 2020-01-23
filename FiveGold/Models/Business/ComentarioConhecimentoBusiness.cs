using FiveGold.Models;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FiveGold.Mapeamento.Business
{
    public class ComentarioConhecimentoBusiness : IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<ComentarioConhecimento> Listar()
        {
            return db.ComentarioConhecimento.ToList();
        }

        public ComentarioConhecimento Buscar(int? id)
        {
            ComentarioConhecimento comentarioConhecimento = db.ComentarioConhecimento.Find(id);
            return comentarioConhecimento;
        }

        //--------------INSERIR------------------

        public void Inserir(ComentarioConhecimento comentarioConhecimento)
        {
            db.ComentarioConhecimento.Add(comentarioConhecimento);
            db.SaveChanges();
        }

        //---------------ALTERAR-----------------

        public ComentarioConhecimento Alterar(int? id)
        {
            ComentarioConhecimento comentarioConhecimento = db.ComentarioConhecimento.Find(id);
            return comentarioConhecimento;
        }

        public void AlterarConfirmacao(ComentarioConhecimento comentarioConhecimento)
        {
            db.Entry(comentarioConhecimento).State = EntityState.Modified;
            db.SaveChanges();
        }

        //----------------APAGAR-------------------

        public ComentarioConhecimento Apagar(int? id)
        {
            ComentarioConhecimento comentarioConhecimento = db.ComentarioConhecimento.Find(id);
            return comentarioConhecimento;
        }

        public void ApagarConfirmacao(int id)
        {
            ComentarioConhecimento comentarioConhecimento = db.ComentarioConhecimento.Find(id);
            db.ComentarioConhecimento.Remove(comentarioConhecimento);
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
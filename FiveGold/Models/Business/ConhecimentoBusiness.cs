using Dapper;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FiveGold.Mapeamento.Business
{
    public class ConhecimentoBusiness : Conexao, IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<Conhecimento> Listar()
        {
            var conhecimento = db.Conhecimento.Where(v => v.validacao == true).Include(c => c.TemaConhecimento);
            return conhecimento.ToList();
        }

        public List<Conhecimento> ListarValidacoesPendente()
        {
            return db.Conhecimento.Where(v => v.validacao == false).ToList();
        }

        public void ValidarPost(int IDPost)
        {
            try
            {
                conn.Execute(@"update Conhecimentoes 
                              set validacao = 'true' 
                              where IDConhecimento = @id
                              ", new { id = IDPost });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //-----------------BUSCAR----------------------

        public List<ComentarioConhecimento> BuscarComentarios(int? id)
        {
            var comentarios = db.ComentarioConhecimento.Where(x => x.IDConhecimento == id);
            return comentarios.ToList();
        }

        public Conhecimento Buscar(int? id)
        {
            Conhecimento conhecimento = db.Conhecimento.Find(id);
            return conhecimento;
        }

        //--------------INSERIR-------------------

        public void Inserir(Conhecimento conhecimento)
        {
            db.Conhecimento.Add(conhecimento);
            db.SaveChanges();
        }

        public void InserirFoto(Conhecimento conhecimento)
        {
            db.Entry(conhecimento).State = EntityState.Modified;
            db.SaveChanges();
        }

        //--------------ALTERAR-------------------

        public void AlterarConfirmacao(Conhecimento conhecimento)
        {
            db.Entry(conhecimento).State = EntityState.Modified;
            db.SaveChanges();
        }

        //---------------APAGAR------------------

        public Conhecimento Apagar(int? id)
        {
            Conhecimento conhecimento = db.Conhecimento.Find(id);
            return conhecimento;
        }

        public void ApagarConfirmacao(int id)
        {
            Conhecimento conhecimento = db.Conhecimento.Find(id);
            db.Conhecimento.Remove(conhecimento);
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
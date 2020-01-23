using Dapper;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FiveGold.Mapeamento.Business
{
    public class ReceitaBusiness : Conexao, IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<Receita> Listar(String UserId)
        {
            var receita = db.Receita.Include(r => r.CatReceita).Include(r => r.Conta).Where(u => u.UserId == UserId).OrderByDescending(o => o.Data);
            return receita.ToList();
        }

        public Receita Buscar(int? id)
        {
            Receita receita = db.Receita.Find(id);
            return receita;
        }

        //--------------INSERIR-------------------

        public List<CatReceita> ListarCatReceita()
        {
            var cr = db.CategoriaReceita.ToList();
            cr.Add(new CatReceita
            {
                IDCatReceita = 0,
                Nome = "[ Selecione ]"
            });

            return cr = cr.OrderBy(d => d.Nome).ToList();
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

        public void Inserir(Receita receita)
        {
            db.Receita.Add(receita);
            db.SaveChanges();

            var valor = receita.Valor;
            var IdConta = receita.IDConta;

            var Update = conn.Query<int>(@"Update Contas SET Saldo = Saldo + @valor 
            WHERE IDConta = @IdConta", new { valor = valor, IdConta = IdConta }).SingleOrDefault();
        }

        //---------------ALTERAR------------------

        public Receita Alterar(int? id)
        {
            Receita receita = db.Receita.Find(id);
            return receita;
        }

        public void Alterar(Receita receita)
        {
            db.Entry(receita).State = EntityState.Modified;
            db.SaveChanges();
        }

        //--------------APAGAR--------------------

        public Receita Apagar(int? id)
        {
            Receita receita = db.Receita.Find(id);
            return receita;
        }

        public void ApagarConfirmar(int id)
        {
            Receita receita = db.Receita.Find(id);
            db.Receita.Remove(receita);
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
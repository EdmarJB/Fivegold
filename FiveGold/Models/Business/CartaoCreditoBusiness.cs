using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FiveGold.Models
{
    public class CartaoCreditoBusiness : IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<CartaoCredito> Listar()
        {
            var cartaoCredito = db.CartaoCredito.Include(c => c.Bandeira);
            return cartaoCredito.ToList();
        }

        public CartaoCredito Buscar(int? id)
        {
            CartaoCredito cartaoCredito = db.CartaoCredito.Find(id);
            return cartaoCredito;
        }

        //--------------INSERIR----------------

        public SelectList ListarBandeira()
        {
            var IDBandeira = new SelectList(db.Bandeira, "IDBandeira", "Nome");
            return IDBandeira;
        }

        public SelectList ListarConta(String User)
        {
            var IDConta = new SelectList(db.Conta.Where(u => u.UserId == User), "IDConta", "Nome");
            return IDConta;
        }

        public void Inserir(CartaoCredito cartaoCredito)
        {
            db.CartaoCredito.Add(cartaoCredito);
            db.SaveChanges();
        }

        public SelectList ListarBandeiraPost(CartaoCredito cartaoCredito)
        {
            var IDBandeiraPost = new SelectList(db.Bandeira, "IDBandeira", "Nome", cartaoCredito.IDBandeira);
            return IDBandeiraPost;
        }

        //-------------ALTERAR------------------

        public CartaoCredito Alterar(int? id)
        {
            CartaoCredito cartaoCredito = db.CartaoCredito.Find(id);
            return cartaoCredito;
        }

        public void AlterarConfirmacao(CartaoCredito cartaoCredito)
        {
            db.Entry(cartaoCredito).State = EntityState.Modified;
            db.SaveChanges();
        }

        //-------------APAGAR--------------------

            public CartaoCredito Apagar(int? id)
        {
            CartaoCredito cartaoCredito = db.CartaoCredito.Find(id);
            return cartaoCredito;
        }

        public void ApagarConfirmacao(int id)
        {
            CartaoCredito cartaoCredito = db.CartaoCredito.Find(id);
            db.CartaoCredito.Remove(cartaoCredito);
            db.SaveChanges();
        }

        //-------------DISPOSE--------------------

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
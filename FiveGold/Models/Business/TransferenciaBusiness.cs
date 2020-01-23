using Dapper;
using FiveGold.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FiveGold.Models.Business
{
    public class TransferenciaBusiness : Conexao, IDisposable
    {
        private FiveGoldContext db = new FiveGoldContext();

        public List<Conta> ListarContas(String User)
        {
            return db.Conta.Where(u => u.UserId == User).ToList();
        }

        public List<Transferencia> ListarTransferencias(String UserId)
        {
            return db.Transferencias.Where(u => u.UserId == UserId).OrderByDescending(o => o.DataTransferencia).ToList();
        }

        public Transferencia Buscar(int? id)
        {
            return db.Transferencias.Find(id);
        }

        public void Salvar(Transferencia transferencia)
        {
            db.Transferencias.Add(transferencia);
            db.SaveChanges();
        }

        public void Editar(Transferencia transferencia)
        {
            db.Entry(transferencia).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Apagar(Transferencia transferencia)
        {
            db.Transferencias.Remove(transferencia);
            db.SaveChanges();
        }

        public Decimal VerificaSaldo(int IDConta)
        {
            return conn.Query<Decimal>(@"
        select Saldo from Contas where  IDConta = @id", new { id = IDConta}).SingleOrDefault();
        }

        public void Debitar(int IDContaOrigem, Decimal Valor)
        {
            conn.Execute(@"
        update Contas set Saldo = Saldo - @val where IDConta = @id", new { id = IDContaOrigem, val = Valor});
        }

        public void Creditar(int IDContaDestino, Decimal Valor)
        {
            conn.Execute(@"
        update Contas set Saldo = Saldo + @val where IDConta = @id", new { id = IDContaDestino, val = Valor });
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
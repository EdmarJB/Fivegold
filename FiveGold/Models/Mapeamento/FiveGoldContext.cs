using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace FiveGold.Models.Mapeamento
{
    public class FiveGoldContext : DbContext
    {
        public FiveGoldContext() : base("name=FiveGoldContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.CatReceita> CategoriaReceita { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.Conta> Conta { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.Bandeira> Bandeira { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.Receita> Receita { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.Despesa> Despesa { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.CartaoCredito> CartaoCredito { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.ComentarioConhecimento> ComentarioConhecimento { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.Conhecimento> Conhecimento { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.CatConta> CatContas { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.CatDespesa> CatDespesa { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.DespesaCartao> DespesaCartao { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.Transferencia> Transferencias { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.TemaConhecimento> TemaConhecimentoes { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.PagarFatura> PagarFaturas { get; set; }

        public System.Data.Entity.DbSet<FiveGold.Models.Mapeamento.ParcelaCartao> ParcelaCartaos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiveGold.Models.Mapeamento
{
    public class CartaoCredito
    {
        [Key]
        [Display(Name = "Código Cartão")]
        public int IDCartao { get; set; }

        [Display(Name = "Código Usuário")]
        public String UserId { get; set; }

        [Display(Name = "Nome Cartão")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String Nome { get; set; }

        public Decimal Limite { get; set; }

        [Display(Name = "Dia Fechamento")]
        public int DiaFechamento { get; set; }

        [Display(Name = "Dia Pagamento")]
        public int DiaPagamento { get; set; }

        [NotMapped]
        public Decimal Fatura { get; set; }

        [NotMapped]
        public Decimal Gasto { get; set; }

        [NotMapped]
        public Decimal Porcentagem { get; set; }

        [NotMapped]
        [Display(Name = "Limite Disponivel")]
        public Decimal LimiteDisponivel { get; set; }

        [Display(Name = "Bandeira")]
        public int IDBandeira  { get; set; }

        public virtual Bandeira Bandeira { get; set; }

        public virtual ICollection<DespesaCartao> DespesaCartao { get; set; }

        
    }
}
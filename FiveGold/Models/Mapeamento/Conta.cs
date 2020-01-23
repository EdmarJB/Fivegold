using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiveGold.Models.Mapeamento
{
    public class Conta
    {
        [Key]
        [Display(Name = "Código Conta")]
        public int IDConta { get; set; }

        [Display(Name = "Código Usuário")]
        public String UserId { get; set; }

        [Required]
        [Display(Name = "Nome Conta")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String Nome { get; set; }

        public Decimal Saldo { get; set; }

        [Required]
        [Display(Name = "Tipo Conta")]
        public int IDCatConta { get; set; }

        public virtual CatConta CatConta { get; set; }
        public virtual ICollection<Despesa> Despesa { get; set; }
        public virtual ICollection<Receita> Receita { get; set; }
        public virtual ICollection<PagarFatura> PagarFatura { get; set; }
    }
}
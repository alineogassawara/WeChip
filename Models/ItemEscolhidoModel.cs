using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeChip.Models
{
    [Table("ItemEscolhido")]
    public class ItemEscolhidoModel
    {   
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdOferta { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdProduto { get; set; }

        public int Quantidade { get; set; }

        public int ValorUnitario { get; set; }

        [ForeignKey("IdOferta")]
        public OfertaModel Oferta { get; set; }

        [ForeignKey("IdProduto")]
        public ProdutoModel Produto { get; set; }

        [NotMapped]
        public double ValorItem 
        {
            get => this.Quantidade * this.ValorUnitario;
        }

        public ICollection<ItemEscolhidoModel> ItemEscolhido { get; set; }
    }
}
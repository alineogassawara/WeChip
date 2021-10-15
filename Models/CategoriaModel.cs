using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;   

namespace WeChip.Models
{
    public class CategoriaModel
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required, MaxLength(128)]
        public string Nome { get; set; }

        public ICollection<ProdutoModel> Produtos { get; set;}
    }
}
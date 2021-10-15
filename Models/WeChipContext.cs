using Microsoft.EntityFrameworkCore;

namespace WeChip.Models
{
    public class WeChipContext : DbContext
    {
        public DbSet<CategoriaModel> Categorias { get; set; }
        
        public DbSet<ProdutoModel> Produtos { get; set; }

        public DbSet<ClienteModel> Clientes { get; set; }

        public DbSet<OfertaModel> Ofertas { get; set; }

        public DbSet<ItemEscolhidoModel> ItensEscolhidos { get; set; }

        public WeChipContext(DbContextOptions<WeChipContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaModel>().ToTable("Categoria");
            modelBuilder.Entity<ProdutoModel>().ToTable("Produto");
            modelBuilder.Entity<ClienteModel>().ToTable("Cliente");
            modelBuilder.Entity<ClienteModel>().OwnsMany(c => c.Endereco, e => 
            {
                e.WithOwner().HasForeignKey("IdCliente");
                e.HasKey("IdCliente", "Id");
            });
            modelBuilder.Entity<OfertaModel>().OwnsOne(o => o.EnderecoEntrega, e =>
            {
                e.Ignore(e => e.Id);
                e.Ignore(e => e.Selecionado);
                e.ToTable("Oferta");
            });   
             modelBuilder.Entity<ItemEscolhidoModel>().HasKey(ie => new {ie.IdOferta, ie.IdProduto});
        }
    }
}
using APICatalogo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context
{
    public class APICatalogoContext : IdentityDbContext 
    {
        public APICatalogoContext(DbContextOptions<APICatalogoContext> options) : base(options) { }

        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Categoria
            modelBuilder.Entity<Categoria>().HasKey(a => a.CategoriaId);
            modelBuilder.Entity<Categoria>().Property(a => a.Nome).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<Categoria>().Property(a => a.ImagemUrl).HasMaxLength(300).IsRequired();

            //Produtos
            modelBuilder.Entity<Produto>().HasKey(a => a.ProdutoId);
            modelBuilder.Entity<Produto>().Property(a => a.Nome).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<Produto>().Property(a => a.Descricao).HasMaxLength(300).IsRequired();
            modelBuilder.Entity<Produto>().Property(a => a.Preco).HasColumnType("decimal(10,2)").IsRequired();
            modelBuilder.Entity<Produto>().Property(a => a.ImagemUrl).HasMaxLength(300).IsRequired();

            //Relacionamento um para muitos
            modelBuilder.Entity<Produto>().HasOne(a => a.Categoria).WithMany(a => a.Produtos).HasForeignKey(a => a.CategoriaId);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}

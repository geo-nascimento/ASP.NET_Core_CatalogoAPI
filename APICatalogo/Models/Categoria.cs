using System.Collections.ObjectModel;

namespace APICatalogo.Models
{
    public class Categoria
    {
        /*É boa prática inicializar as propriedades que são coleções no construtor*/

        public Categoria()
        {
           Produtos = new Collection<Produto>();
        }

        public int CategoriaId { get; set; }
        public string? Nome { get; set; }
        public string? ImagemUrl { get; set; }

        //Relacionamento de um para muitos: produto possui uma categoria e a categoria pode estar em vários produtos
        public ICollection<Produto>? Produtos { get; set; }
    }
}

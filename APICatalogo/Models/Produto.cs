using System.Text.Json.Serialization;

namespace APICatalogo.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public int CategoriaId { get; set; }//Chave estrangeira
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        //Relacionamento de um para muitos: produto possui uma categoria e a categoria pode estar em vários produtos
        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}

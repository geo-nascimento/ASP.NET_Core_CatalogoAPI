using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly APICatalogoContext _context;

        public ProdutosController(APICatalogoContext context)
        {
            _context = context;
        }

        [HttpGet]// /produtos
        public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
        {
            var produtos = await _context.Produtos!.AsNoTracking().ToListAsync();

            if (produtos is null)
            {
                return NotFound();
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")] // /produtos/id
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {

            var produto = await _context.Produtos!.AsNoTracking().FirstOrDefaultAsync(a => a.ProdutoId == id);

            if (produto is null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();

            _context.Produtos?.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { Id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id}:int")]
        public ActionResult Put(int id, Produto produto)
        {//Abordagem alternativa a que eu sempre uso
            if (id != produto.ProdutoId)
                return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos?.FirstOrDefault(a => a.ProdutoId == id);

            if (produto is null)
                return NotFound();
            

            _context.Produtos?.Remove(produto);
            _context.SaveChanges(true);

            return Ok();

        }
    }
}

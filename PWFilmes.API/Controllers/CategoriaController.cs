using Microsoft.AspNetCore.Mvc;
using PWFilmes.API.Context;
using PWFilmes.Domain;
using System.Collections.Generic;
using System.Linq;

namespace PWFilmes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private PWFilmesContext _context;

        public CategoriaController(PWFilmesContext context)
        {
            _context = context;
        }

        [HttpGet("listar")]
        public IActionResult Listar()
        {
              return Ok(_context.CategoriaSet.AsEnumerable());
            //return Ok(_context.CategoriaSet.ToList());
        }

        [HttpGet("obter/{codigo}")]
        public IActionResult Obter(int codigo)
        {
            return Ok(_context.CategoriaSet.Find(codigo));
        }

        [HttpPost("adicionar")]
        public IActionResult Adicionar(Categoria categoria)
        {
            _context.CategoriaSet.Add(categoria);
            _context.SaveChanges();

            return Created("Created",
               $"Categoria {categoria.Codigo} Adicionada com Sucesso");

        }
        [HttpPut("Atualizar")]
        public IActionResult Atualizar(Categoria categoria)
        {
            if (_context.CategoriaSet.Any(p =>p.Codigo == categoria.Codigo))
            {
                _context.Attach(categoria);
                _context.CategoriaSet.Update(categoria);
                _context.SaveChanges();

                return Ok($"Categoria {categoria.Codigo} não Localizada");
            }
                
            return BadRequest($"Categoria {categoria.Codigo} não Localizada");
        }
        [HttpDelete("excluir/{codigo}")]
        public IActionResult Excluir(int codigo)
        {
            var categoria = _context.CategoriaSet.Find(codigo);
            if (categoria == null)
                return BadRequest($"Categoria {codigo} não encontrada");

            _context.CategoriaSet.Remove(categoria);
            _context.SaveChanges();

            return Ok($"Categoria {codigo} Removida com Sucesso.");
        }
    }
}

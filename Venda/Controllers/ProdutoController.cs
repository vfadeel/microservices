using Venda.Models;
using Venda.Repositories;
using Microsoft.AspNetCore.Mvc;
using Venda.Publishers;

namespace Venda.Controllers;

[ApiController]
[Route("produtos")]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoRepository _produtoRepository;

    public ProdutoController(ProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet("{IdProduto}")]
    public Produto Selecionar(int IdProduto)
    {
        return _produtoRepository.Selecionar(IdProduto);
    }

    [HttpGet]
    public IEnumerable<Produto> SelecionarTodos()
    {
        return _produtoRepository.SelecionarTodos();
    }

}

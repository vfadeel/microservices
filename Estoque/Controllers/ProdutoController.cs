using Estoque.Models;
using Estoque.Publishers;
using Estoque.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers;

[ApiController]
[Route("produtos")]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoRepository _produtoRepository;
    private readonly ProdutoInclusaoPublisher _produtoInclusaoPublisher;
    private readonly ProdutoAlteracaoPublisher _produtoAlteracaoPublisher;
    private readonly ProdutoExclusaoPublisher _produtoExclusaoPublisher;

    public ProdutoController(ProdutoRepository produtoRepository, 
                             ProdutoInclusaoPublisher produtoInclusaoPublisher,
                             ProdutoExclusaoPublisher produtoExclusaoPublisher,
                             ProdutoAlteracaoPublisher produtoAlteracaoPublisher)
    {
        _produtoRepository = produtoRepository;
        _produtoInclusaoPublisher = produtoInclusaoPublisher;
        _produtoAlteracaoPublisher = produtoAlteracaoPublisher;
        _produtoExclusaoPublisher = produtoExclusaoPublisher;
    }

    [HttpPost]
    public int Incluir(Produto _produto)
    {
        _produto.IdProduto = _produtoRepository.Incluir(_produto);

        _produtoInclusaoPublisher.Publicar(_produto);
        
        return _produto.IdProduto;
    }

    [HttpPut]
    public void Alterar(Produto _produto)
    {
        _produtoRepository.Alterar(_produto);
        
        _produtoAlteracaoPublisher.Publicar(_produto);
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

    [HttpDelete("{IdProduto}")]
    public void Excluir(int IdProduto)
    {
        _produtoRepository.Excluir(IdProduto);
        
        _produtoExclusaoPublisher.Publicar(IdProduto);
    }
}

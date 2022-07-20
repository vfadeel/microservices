using Estoque.Models;
using Estoque.Publishers;
using Estoque.Repositories;
using Microsoft.AspNetCore.Mvc;
using Infraestrutura.Database;

namespace Estoque.Controllers;

[ApiController]
[Route("produtos")]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoRepository _produtoRepository;
    private readonly ProdutoInclusaoPublisher _produtoInclusaoPublisher;
    private readonly ProdutoAlteracaoPublisher _produtoAlteracaoPublisher;
    private readonly UnitOfWork _unitOfWork;
    private readonly ProdutoExclusaoPublisher _produtoExclusaoPublisher;

    public ProdutoController(ProdutoRepository produtoRepository,
                             ProdutoInclusaoPublisher produtoInclusaoPublisher,
                             ProdutoExclusaoPublisher produtoExclusaoPublisher,
                             ProdutoAlteracaoPublisher produtoAlteracaoPublisher,
                             UnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository;
        _produtoInclusaoPublisher = produtoInclusaoPublisher;
        _produtoAlteracaoPublisher = produtoAlteracaoPublisher;
        _unitOfWork = unitOfWork;
        _produtoExclusaoPublisher = produtoExclusaoPublisher;
    }

    [HttpPost]
    public int Incluir(Produto _produto)
    {
        try
        {
            _unitOfWork.Start();

            _produto.IdProduto = _produtoRepository.Incluir(_produto);

            _produtoInclusaoPublisher.Publicar(new ProdutoInclusaoEvento()
            {
                Produto = _produto
            });

            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }

        return _produto.IdProduto;
    }

    [HttpPut]
    public void Alterar(Produto _produto)
    {
        try
        {

            _unitOfWork.Start();

            _produtoRepository.Alterar(_produto);

            _produtoAlteracaoPublisher.Publicar(new ProdutoAlteracaoEvento()
            {
                Produto = _produto
            });

            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
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
        try
        {
            _unitOfWork.Start();

            _produtoRepository.Excluir(IdProduto);

            _produtoExclusaoPublisher.Publicar(new ProdutoExclusaoEvento()
            {
                IdProduto = IdProduto
            });

            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }

    }
}

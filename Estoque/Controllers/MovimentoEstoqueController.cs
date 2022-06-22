using Estoque.Models;
using Estoque.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers;

[ApiController]
[Route("movimentos-estoque")]
public class MovimentoEstoqueController : ControllerBase
{
    private readonly MovimentoEstoqueRepository _movimentoEstoqueRepository;

    public MovimentoEstoqueController(MovimentoEstoqueRepository movimentoEstoqueRepository)
    {
        _movimentoEstoqueRepository = movimentoEstoqueRepository;
    }

    [HttpPost]
    public int Incluir(MovimentoEstoque _movimentoEstoque)
    {
        return _movimentoEstoqueRepository.Incluir(_movimentoEstoque);
    }

    [HttpPut]
    public void Alterar(MovimentoEstoque _movimentoEstoque)
    {
        _movimentoEstoqueRepository.Alterar(_movimentoEstoque);
    }

    [HttpGet("{IdMovimentoEstoque}")]
    public MovimentoEstoque Selecionar(int IdMovimentoEstoque)
    {
        return _movimentoEstoqueRepository.Selecionar(IdMovimentoEstoque);
    }

    [HttpGet]
    public IEnumerable<MovimentoEstoque> SelecionarTodos()
    {
        return _movimentoEstoqueRepository.SelecionarTodos();
    }

    [HttpDelete("{IdMovimentoEstoque}")]
    public void Excluir(int IdMovimentoEstoque)
    {
        _movimentoEstoqueRepository.Excluir(IdMovimentoEstoque);
    }
}

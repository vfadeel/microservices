using Compra.Models;
using Compra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Compra.Controllers;

[ApiController]
[Route("fornecedores")]
public class FornecedorController : ControllerBase
{
    private readonly FornecedorRepository _fornecedorRepository;

    public FornecedorController(FornecedorRepository fornecedorRepository)
    {
        _fornecedorRepository = fornecedorRepository;
    }

    [HttpPost]
    public int Incluir(Fornecedor _fornecedor)
    {
        return _fornecedorRepository.Incluir(_fornecedor);
    }

    [HttpPut]
    public void Alterar(Fornecedor _fornecedor)
    {
        _fornecedorRepository.Alterar(_fornecedor);
    }

    [HttpGet("{IdFornecedor}")]
    public Fornecedor Selecionar(int IdFornecedor)
    {
        return _fornecedorRepository.Selecionar(IdFornecedor);
    }

    [HttpGet]
    public IEnumerable<Fornecedor> SelecionarTodos()
    {
        return _fornecedorRepository.SelecionarTodos();
    }

    [HttpDelete("{IdFornecedor}")]
    public void Excluir(int IdFornecedor)
    {
        _fornecedorRepository.Excluir(IdFornecedor);
    }
}

using Compra.Models;
using Compra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Compra.Controllers;

[ApiController]
[Route("compras")]
public class CompraController : ControllerBase
{
    private readonly CompraRepository _compraRepository;

    public CompraController(CompraRepository compraRepository)
    {
        _compraRepository = compraRepository;
    }

    [HttpPost]
    public int Incluir(Compra.Models.Compra _compra)
    {
        return _compraRepository.Incluir(_compra);
    }

    [HttpPut]
    public void Alterar(Compra.Models.Compra _compra)
    {
        _compraRepository.Alterar(_compra);
    }

    [HttpGet("{IdCompra}")]
    public Compra.Models.Compra Selecionar(int IdCompra)
    {
        return _compraRepository.Selecionar(IdCompra);
    }

    [HttpGet]
    public IEnumerable<Compra.Models.Compra> SelecionarTodos()
    {
        return _compraRepository.SelecionarTodos();
    }

    [HttpDelete("{IdCompra}")]
    public void Excluir(int IdCompra)
    {
        _compraRepository.Excluir(IdCompra);
    }
}

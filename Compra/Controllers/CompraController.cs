using Compra.Models;
using Compra.Publishers;
using Compra.Repositories;
using Infraestrutura.Publishers;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace Compra.Controllers;

[ApiController]
[Route("compras")]
public class CompraController : ControllerBase
{
    private readonly CompraRepository _compraRepository;
    private readonly MovimentoEstoqueInclusaoPublisher _movimentoEstoqueInclusaoPublisher;

    public CompraController(CompraRepository compraRepository,
                            MovimentoEstoqueInclusaoPublisher movimentoEstoqueInclusaoPublisher)
    {
        _compraRepository = compraRepository;
        _movimentoEstoqueInclusaoPublisher = movimentoEstoqueInclusaoPublisher;
    }

    [HttpPost]
    public int Incluir(Compra.Models.Compra _compra)
    {
        return _compraRepository.Incluir(_compra);

        _movimentoEstoqueInclusaoPublisher.Publicar(new MovimentoEstoqueInclusaoEvento()
        {
            IdProduto = _compra.IdProduto,
            Quantidade = _compra.Quantidade,
            Tipo = "Entrada"
        });
    }

    [HttpPut]
    public void Alterar(Compra.Models.Compra _compra)
    {
        throw new NotImplementedException();
        // _compraRepository.Alterar(_compra);
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
        Compra.Models.Compra _compra = _compraRepository.Selecionar(IdCompra);

        _compraRepository.Excluir(_compra.IdCompra);

        _movimentoEstoqueInclusaoPublisher.Publicar(new MovimentoEstoqueInclusaoEvento()
        {
            IdProduto = _compra.IdProduto,
            Quantidade = _compra.Quantidade,
            Tipo = "Saida"
        });
    }
}

using Venda.Models;
using Venda.Repositories;
using Microsoft.AspNetCore.Mvc;
using Venda.Publishers;

namespace Venda.Controllers;

[ApiController]
[Route("pedidos")]
public class PedidoController : ControllerBase
{
    private readonly PedidoRepository _pedidoRepository;
    private readonly MovimentoEstoqueInclusaoPublisher _movimentoEstoqueInclusaoPublisher;

    public PedidoController(PedidoRepository pedidoRepository,
                            MovimentoEstoqueInclusaoPublisher movimentoEstoqueInclusaoPublisher)
    {
        _pedidoRepository = pedidoRepository;
        _movimentoEstoqueInclusaoPublisher = movimentoEstoqueInclusaoPublisher;
    }

    [HttpPost]
    public int Incluir(Pedido _pedido)
    {
        int IdPedido = _pedidoRepository.Incluir(_pedido);

        _movimentoEstoqueInclusaoPublisher.Publicar(new MovimentoEstoqueInclusaoEvento()
        {
            IdProduto = _pedido.IdProduto,
            Quantidade = _pedido.Quantidade,
            Tipo = "Saida"
        });

        return IdPedido;
    }

    [HttpPut]
    public void Alterar(Pedido _pedido)
    {
        _pedidoRepository.Alterar(_pedido);
    }

    [HttpGet("{IdPedido}")]
    public Pedido Selecionar(int IdPedido)
    {
        return _pedidoRepository.Selecionar(IdPedido);
    }

    [HttpGet]
    public IEnumerable<Pedido> SelecionarTodos()
    {
        return _pedidoRepository.SelecionarTodos();
    }

    [HttpDelete("{IdPedido}")]
    public void Excluir(int IdPedido)
    {
        Pedido _pedido = _pedidoRepository.Selecionar(IdPedido);

        _pedidoRepository.Excluir(_pedido.IdPedido);

        _movimentoEstoqueInclusaoPublisher.Publicar(new MovimentoEstoqueInclusaoEvento()
        {
            IdProduto = _pedido.IdProduto,
            Quantidade = _pedido.Quantidade,
            Tipo = "Entrada"
        });
    }
}

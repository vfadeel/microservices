using Venda.Models;
using Venda.Repositories;
using Microsoft.AspNetCore.Mvc;
using Venda.Publishers;
using Infraestrutura.Database;

namespace Venda.Controllers;

[ApiController]
[Route("pedidos")]
public class PedidoController : ControllerBase
{
    private readonly PedidoRepository _pedidoRepository;
    private readonly MovimentoEstoqueInclusaoPublisher _movimentoEstoqueInclusaoPublisher;
    private readonly UnitOfWork _unitOfWork;

    public PedidoController(PedidoRepository pedidoRepository,
                            MovimentoEstoqueInclusaoPublisher movimentoEstoqueInclusaoPublisher,
                            UnitOfWork unitOfWork)
    {
        _pedidoRepository = pedidoRepository;
        _movimentoEstoqueInclusaoPublisher = movimentoEstoqueInclusaoPublisher;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public int Incluir(Pedido _pedido)
    {
        _unitOfWork.Start();

        try
        {
            int IdPedido = _pedidoRepository.Incluir(_pedido);

            _movimentoEstoqueInclusaoPublisher.Publicar(new MovimentoEstoqueInclusaoEvento()
            {
                IdProduto = _pedido.IdProduto,
                Quantidade = _pedido.Quantidade,
                Tipo = "Saida"
            });

            _unitOfWork.Commit();

            return IdPedido;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }

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

        _unitOfWork.Start();

        try
        {
            Pedido _pedido = _pedidoRepository.Selecionar(IdPedido);

            _pedidoRepository.Excluir(_pedido.IdPedido);

            _movimentoEstoqueInclusaoPublisher.Publicar(new MovimentoEstoqueInclusaoEvento()
            {
                IdProduto = _pedido.IdProduto,
                Quantidade = _pedido.Quantidade,
                Tipo = "Entrada"
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

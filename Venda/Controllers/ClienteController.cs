using Venda.Models;
using Venda.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Venda.Controllers;

[ApiController]
[Route("clientes")]
public class ClienteController : ControllerBase
{
    private readonly ClienteRepository _clienteRepository;

    public ClienteController(ClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpPost]
    public int Incluir(Cliente _cliente)
    {
        return _clienteRepository.Incluir(_cliente);
    }

    [HttpPut]
    public void Alterar(Cliente _cliente)
    {
        _clienteRepository.Alterar(_cliente);
    }

    [HttpGet("{IdCliente}")]
    public Cliente Selecionar(int IdCliente)
    {
        return _clienteRepository.Selecionar(IdCliente);
    }

    [HttpGet]
    public IEnumerable<Cliente> SelecionarTodos()
    {
        return _clienteRepository.SelecionarTodos();
    }

    [HttpDelete("{IdCliente}")]
    public void Excluir(int IdCliente)
    {
        _clienteRepository.Excluir(IdCliente);
    }
}

using Venda.Models;
using Venda.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Venda.Controllers;

[ApiController]
[Route("eventos")]
public class EventoController : ControllerBase
{
    public EventoRepository _eventoRepository;

    public EventoController(EventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    [HttpGet]
    public IEnumerable<Evento> SelecionarTodos()
    {
        return _eventoRepository.SelecionarTodos();
    }

}
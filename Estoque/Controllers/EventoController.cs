using Estoque.Models;
using Estoque.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers;

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
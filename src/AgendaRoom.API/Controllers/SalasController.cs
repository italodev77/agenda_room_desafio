using AgendaRoom.DALs;
using AgendaRoom.DTOs;
using AgendaRoom.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgendaRoom.Controllers;

[Route("api/salas")]
[ApiController]
public class SalasController: ControllerBase
{
    
    private readonly SalasDAL _salasDAL;

    public SalasController(SalasDAL salasDal)
    {
        _salasDAL = salasDal;
    }

    [HttpGet("listar-salas")]
    public async Task<IActionResult> GetAllSalas()
    {
        var salas = await _salasDAL.GetAllSalas();
        return Ok(salas);
    }

    [HttpGet("listar-salas/{id}")]
    public async Task<IActionResult> GetSalaById(int id)
    {
        var salas = await _salasDAL.GetSalasById(id);
        if (salas == null)
            return NotFound("Sala não encontrada");
        return Ok(salas);
    }

    [HttpPost("criar-sala")]
    public async Task<IActionResult> CreateSala([FromBody] CriarSalaDTO model)
    {
        var sala = new Salas
        {
            nome = model.Nome,
            capacidade = model.Capacidade,
        };
        
        var novaSala = await _salasDAL.CreateSalas(sala);
        return Ok(novaSala);
    }

    [HttpPut("editar-sala/{id}")]
    public async Task<IActionResult> UpdateSala(int id,[FromBody] UpdateSalaDTO model)
    {
        var salas = await _salasDAL.GetSalasById(id);
        if(salas == null)
            return NotFound("Sala não encontrada");
        salas.nome = model.Nome ?? salas.nome;
        salas.capacidade = model.Capacidade;
        
        await _salasDAL.UpdateSala(salas);
        return Ok("Sala atualizada com sucesso");
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSala(int id)
    {
        var sala = await _salasDAL.GetSalasById(id);
        if (sala == null)
            return NotFound("Sala não encontrado.");

        await _salasDAL.DeleteSala(id);
        return Ok("Sala excluída com sucesso.");
    }

}
using AgendaRoom.API.DTOs;
using AgendaRoom.DALs;
using AgendaRoom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgendaRoom.API.Controllers;

[Route("api/rooms")]
[ApiController]
public class RoomsController: ControllerBase
{
    
    private readonly RoomDal _roomDal;

    public RoomsController(RoomDal roomDal)
    {
        _roomDal = roomDal;
    }

    [HttpGet("list-rooms")]
    public async Task<IActionResult> GetAllRooms()
    {
        var rooms = await _roomDal.GetAllRooms();
        return Ok(rooms);
    }

    [HttpGet("list-room/{id}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        var rooms = await _roomDal.GetRoomById(id);
        if (rooms == null)
            return NotFound("Sala não encontrada");
        return Ok(rooms);
    }

    [HttpPost("create-room")]
    public async Task<IActionResult> CreateSala([FromBody] CreateRoomDTO model)
    {
        var room = new Room()
        {
            name = model.Name,
            capacity = model.Capacity,
        };
        
        var newRoom = await _roomDal.CreateRooms(room);
        return Ok(newRoom);
    }

    [HttpPut("update-room/{id}")]
    public async Task<IActionResult> UpdateRoom(int id,[FromBody] UpdateRoomDTO model)
    {
        var room = await _roomDal.GetRoomById(id);
        if(room == null)
            return NotFound("Sala não encontrada");
        room.name = model.Name ?? room.name;
        room.capacity = model.Capacity;
        
        await _roomDal.UpdateRoom(room);
        return Ok("Sala atualizada com sucesso");
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSala(int id)
    {
        var sala = await _roomDal.GetSalasById(id);
        if (sala == null)
            return NotFound("Sala não encontrado.");

        await _roomDal.DeleteSala(id);
        return Ok("Sala excluída com sucesso.");
    }

}
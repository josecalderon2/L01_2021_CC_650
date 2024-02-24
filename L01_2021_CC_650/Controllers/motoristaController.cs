using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_CC_650.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace L01_2021_CC_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristasController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContexto;

        public MotoristasController(RestauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        // Método para obtener el listado de todos los motoristas
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllMotoristas()
        {
            List<Motoristas> listadoMotoristas = _restauranteContexto.Motoristas.ToList();

            if (listadoMotoristas.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoMotoristas);
        }

        // Método para obtener un motorista por su ID
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetMotoristaById(int id)
        {
            Motoristas motorista = _restauranteContexto.Motoristas.Find(id);

            if (motorista == null)
            {
                return NotFound();
            }

            return Ok(motorista);
        }

        // Método para guardar un nuevo motorista
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarMotorista([FromBody] Motoristas motorista)
        {
            try
            {
                _restauranteContexto.Motoristas.Add(motorista);
                _restauranteContexto.SaveChanges();
                return Ok(motorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar los datos de un motorista
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] Motoristas motoristaModificar)
        {
            Motoristas motoristaActual = _restauranteContexto.Motoristas.Find(id);

            if (motoristaActual == null)
            {
                return NotFound();
            }

            motoristaActual.nombreMotorista = motoristaModificar.nombreMotorista;

            _restauranteContexto.Entry(motoristaActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(motoristaModificar);
        }

        // Método para eliminar un motorista
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarMotorista(int id)
        {
            Motoristas motorista = _restauranteContexto.Motoristas.Find(id);

            if (motorista == null)
                return NotFound();

            _restauranteContexto.Motoristas.Remove(motorista);
            _restauranteContexto.SaveChanges();

            return Ok(motorista);
        }

        // Método para obtener el listado de motoristas filtrados por nombre
        [HttpGet]
        [Route("GetByNombre/{nombre}")]
        public IActionResult GetMotoristasByNombre(string nombre)
        {
            List<Motoristas> motoristasFiltrados = _restauranteContexto.Motoristas.Where(m => m.nombreMotorista.Contains(nombre)).ToList();

            if (motoristasFiltrados.Count == 0)
            {
                return NotFound();
            }

            return Ok(motoristasFiltrados);
        }
    }
}

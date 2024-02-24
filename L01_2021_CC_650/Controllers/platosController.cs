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
    public class PlatosController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContexto;

        public PlatosController(RestauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        // Método para obtener el listado de todos los platos
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllPlatos()
        {
            List<Platos> listadoPlatos = _restauranteContexto.Platos.ToList();

            if (listadoPlatos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPlatos);
        }

        // Método para obtener un plato por su ID
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetPlatoById(int id)
        {
            Platos plato = _restauranteContexto.Platos.Find(id);

            if (plato == null)
            {
                return NotFound();
            }

            return Ok(plato);
        }

        // Método para guardar un nuevo plato
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPlato([FromBody] Platos plato)
        {
            try
            {
                _restauranteContexto.Platos.Add(plato);
                _restauranteContexto.SaveChanges();
                return Ok(plato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar los datos de un plato
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarPlato(int id, [FromBody] Platos platoModificar)
        {
            Platos platoActual = _restauranteContexto.Platos.Find(id);

            if (platoActual == null)
            {
                return NotFound();
            }

            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;

            _restauranteContexto.Entry(platoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(platoModificar);
        }

        // Método para eliminar un plato
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarPlato(int id)
        {
            Platos plato = _restauranteContexto.Platos.Find(id);

            if (plato == null)
                return NotFound();

            _restauranteContexto.Platos.Remove(plato);
            _restauranteContexto.SaveChanges();

            return Ok(plato);
        }

        // Método para obtener el listado de platos filtrados por precio menor que un valor dado
        [HttpGet]
        [Route("GetByPriceLessThan/{precio}")]
        public IActionResult GetPlatosByPriceLessThan(decimal precio)
        {
            List<Platos> platosFiltrados = _restauranteContexto.Platos.Where(p => p.precio < precio).ToList();

            if (platosFiltrados.Count == 0)
            {
                return NotFound();
            }

            return Ok(platosFiltrados);
        }
    }
}

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
    public class PedidosController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContexto;

        public PedidosController(RestauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Pedidos> listadoPedidos = _restauranteContexto.Pedidos.ToList();

            if (listadoPedidos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPedidos);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            Pedidos pedido = _restauranteContexto.Pedidos.Find(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPedido([FromBody] Pedidos pedido)
        {
            try
            {
                _restauranteContexto.Pedidos.Add(pedido);
                _restauranteContexto.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarPedido(int id, [FromBody] Pedidos pedidoModificar)
        {
            Pedidos pedidoActual = _restauranteContexto.Pedidos.Find(id);

            if (pedidoActual == null)
            {
                return NotFound();
            }

            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            _restauranteContexto.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(pedidoModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarPedido(int id)
        {
            Pedidos pedido = _restauranteContexto.Pedidos.Find(id);

            if (pedido == null)
                return NotFound();

            _restauranteContexto.Pedidos.Remove(pedido);
            _restauranteContexto.SaveChanges();

            return Ok(pedido);
        }

        [HttpGet]
        [Route("ByCliente/{clienteId}")]
        public IActionResult GetPedidosByCliente(int clienteId)
        {
            List<Pedidos> pedidosCliente = _restauranteContexto.Pedidos.Where(p => p.clienteId == clienteId).ToList();

            if (pedidosCliente.Count == 0)
            {
                return NotFound();
            }

            return Ok(pedidosCliente);
        }

        [HttpGet]
        [Route("ByMotorista/{motoristaId}")]
        public IActionResult GetPedidosByMotorista(int motoristaId)
        {
            List<Pedidos> pedidosMotorista = _restauranteContexto.Pedidos.Where(p => p.motoristaId == motoristaId).ToList();

            if (pedidosMotorista.Count == 0)
            {
                return NotFound();
            }

            return Ok(pedidosMotorista);
        }
    }
}
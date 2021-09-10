using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using senai.inlock.webApi_.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Controllers
{

    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class EstudiosController : ControllerBase
    {
        private IEstudioRepository _estudioRepository { get; set; }

        public EstudiosController()
        {
            _estudioRepository = new EstudioRepository();
        }


        [HttpPost]
        public IActionResult Post(EstudioDomain novoEstudio)
        {
            _estudioRepository.Cadastrar(novoEstudio);

            return StatusCode(201);
        }


        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EstudioDomain estudioBuscado = _estudioRepository.BuscarPorId(id);

            if (estudioBuscado == null)
            {
                return NotFound("Nenhum estudio encontrado");
            }

            return Ok(estudioBuscado);
        }


        [HttpGet]
        public IActionResult Get()
        {
            List<EstudioDomain> listaEstudio = _estudioRepository.ListarTodos();

            return Ok(listaEstudio);

        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, EstudioDomain estudioAtualizado)
        {
            EstudioDomain estudioBuscado = _estudioRepository.BuscarPorId(id);

            if (estudioBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Estudio não encontrado!",
                            erro = true
                        }
                    );
            }

            try
            {
                _estudioRepository.AtualizarIdUrl(id, estudioAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _estudioRepository.Deletar(id);

            return NoContent();
        }
    }

}

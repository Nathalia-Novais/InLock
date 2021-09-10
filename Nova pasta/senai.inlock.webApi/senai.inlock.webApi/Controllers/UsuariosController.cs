using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using senai.inlock.webApi_.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            List<UsuarioDomain> listaUsuario = _usuarioRepository.ListarTodos();

            return Ok(listaUsuario);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorId(id);

            if (usuarioBuscado == null)
            {
                return NotFound("Nenhum usuario encontrado");
            }

            return Ok(usuarioBuscado);
        }

        [HttpPost]
        public IActionResult Post(UsuarioDomain novoUsuario)
        {

            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, UsuarioDomain usuarioAtualizado)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorId(id);

            if (usuarioBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Usuario não encontrado!",
                            erro = true
                        }
                    );
            }

            try
            {
                _usuarioRepository.AtualizarIdUrl(id, usuarioAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UsuarioDomain login)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorEmailSenha(login.email, login.senha);

            if (usuarioBuscado == null)
            {
              return NotFound("E-mail ou senha inválidos!");
            }
           

            //return Ok(usuarioBuscado);

            var minhasClaims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.idUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.idTipoUsuario.ToString()),
                new Claim("Claim personalizada", "Valor Teste")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Usuarios-chave-autenticacao"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var meuToken = new JwtSecurityToken(
                    issuer: "inlock.webAPI",               
                    audience: "inlock.webAPI",               
                    claims: minhasClaims,                
                    expires: DateTime.Now.AddDays(1),  
                    signingCredentials: creds        
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(meuToken)
            });



        }

            [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _usuarioRepository.Deletar(id);

            return NoContent();
        }

    }
}


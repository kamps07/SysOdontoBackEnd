using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : Controller
    {

        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public IActionResult CadastrarUsuario([FromBody] UsuarioDTO usuario)
        {
            var dao = new UsuarioDAO();

            bool usuarioExiste = dao.VerificarUsuario(usuario);
            if (usuarioExiste)
            {
                var mensagem = "Usuário já existe na base de dados";
                return Conflict(mensagem);
            }

            dao.Cadastrar(usuario);
            return Ok();
        }


        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult LoginUsuario([FromBody] UsuarioDTO usuario)
        {
            var dao = new UsuarioDAO();
            var usuarioLogado = dao.Login(usuario);

            if (usuarioLogado == null || usuarioLogado.Email is null)
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(usuarioLogado);

            return Ok(new { token });
        }

        [HttpGet]
        [Route("Dentistas")]
        public IActionResult ListarDentistas()
        {
            var clinicaID = int.Parse(HttpContext.User.FindFirst("clinica")?.Value);
            var dao = new UsuarioDAO();
            var dentistas = dao.ListarDentistas(clinicaID);

            return Ok(dentistas);
        }

        private string GenerateJwtToken(UsuarioDTO usuario)
        {
            var secretKey = "PU8a9W4sv2opkqlOwmgsn3w3Innlc4D5";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim("ID", usuario.ID.ToString()),
                    new Claim("Email", usuario.Email),
                    new Claim("Funcao", usuario.Funcao),
                    new Claim("ImgUrl", usuario.Clinica.ImgURL),
                    new Claim("Clinica", usuario.Clinica.ID.ToString()),
                };

            var token = new JwtSecurityToken(
                "APISysOdonto", //Nome da sua api
                "APISysOdonto", //Nome da sua api
                claims, //Lista de claims
                expires: DateTime.UtcNow.AddDays(1), //Tempo de expiração do Token, nesse caso o Token expira em um dia
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    
}


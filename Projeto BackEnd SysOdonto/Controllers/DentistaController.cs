﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistaController : Controller
    {

        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public IActionResult Cadastrar([FromBody] DentistaDTO dentista)
        {
            var dao = new DentistaDAO();

            bool dentistaExiste = dao.VerificarDentista(dentista);
            if (dentistaExiste)
            {
                var mensagem = "E-mail já existe na base de dados";
                return Conflict(mensagem);
            }

            dao.Cadastrar(dentista);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromForm] DentistaDTO dentista)
        {
            var dao = new DentistaDAO();
            var usuarioLogado = dao.Login(dentista);

            if (usuarioLogado.ID == 0)
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(usuarioLogado);

            return Ok(new { token });
        }

        private string GenerateJwtToken(DentistaDTO dentista)
        {
            var secretKey = "PU8a9W4sv2opkqlOwmgsn3w3Innlc4D5";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim("ID", dentista.ID.ToString()),
                    new Claim("Email", dentista.Email),
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
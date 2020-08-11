using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {

        public List<Aluno> alunos = new List<Aluno>()
        {
            new Aluno()
            {
                Id = 1,
                Nome = "Marta",
                Sobrenome = "Maria",
                Telefone = "45678936514"
            },
            new Aluno()
            {
                Id = 2,
                Nome = "Paula",
                Sobrenome = "Henrique",
                Telefone = "13123534454"
            },
            new Aluno()
            {
                Id = 3,
                Nome = "Joao",
                Sobrenome = "Paulo",
                Telefone = "989768567856"
            },
        };

        public AlunoController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(alunos);
        }


        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = alunos.FirstOrDefault(x => x.Id == id);
            if (aluno == null) return BadRequest("Aluno nao encontrado");
            return Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = alunos.FirstOrDefault(x => x.Nome.Contains(nome) && x.Sobrenome.Contains(sobrenome));
            if (aluno == null) return BadRequest("Aluno nao encontrado");
            return Ok(aluno);
        }

        [HttpPost()]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}

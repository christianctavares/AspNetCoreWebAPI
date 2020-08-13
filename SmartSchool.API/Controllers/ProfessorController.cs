using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllProfessores(true));
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetByID(int id)
        {

            var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor nao encontrado");

            return Ok(prof);
        }


        [HttpGet("byName")]
        public IActionResult GetByName(string nome)
        {

            var prof = _repo.GetAllProfessores(true).FirstOrDefault(x => x.Nome.Contains(nome));
            if (prof == null) return BadRequest("Professor nao encontrado");

            return Ok(prof);
        }

        [HttpPost()]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor nao cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,Professor professor)
        {

            var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor nao encontrado");

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor nao atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {

            var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor nao encontrado");

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor nao atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor nao encontrado");

            _repo.Delete(prof);
            if (_repo.SaveChanges())
            {
                return Ok("professor deletado");
            }
            return BadRequest("Professor nao cadastrado");
        }

    }
}


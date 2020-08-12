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
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetByID(int id)
        {

            var prof = _context.Professores.FirstOrDefault(x => x.Id == id);
            if (prof == null) return BadRequest("Professor nao encontrado");

            return Ok(prof);
        }


        [HttpGet("byName")]
        public IActionResult GetByName(string nome)
        {

            var prof = _context.Professores.FirstOrDefault(x => x.Nome.Contains(nome));
            if (prof == null) return BadRequest("Professor nao encontrado");

            return Ok(prof);
        }

        [HttpPost()]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,Professor professor)
        {

            var prof = _context.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (prof == null) return BadRequest("Professor nao encontrado");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {

            var prof = _context.Professores.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (prof == null) return BadRequest("Professor nao encontrado");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var prof = _context.Professores.FirstOrDefault(x => x.Id == id);
            if (prof == null) return BadRequest("Professor nao encontrado");

            _context.Remove(prof);
            _context.SaveChanges();
            return Ok();
        }

    }
}


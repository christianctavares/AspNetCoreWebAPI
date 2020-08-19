using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<Professor>>(professores));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetByID(int id)
        {

            var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor nao encontrado");

            var profDto = _mapper.Map<Professor>(prof);

            return Ok(profDto);
        }


        [HttpGet("byName")]
        public IActionResult GetByName(string nome)
        {

            var prof = _repo.GetAllProfessores(true).FirstOrDefault(x => x.Nome.Contains(nome));
            if (prof == null) return BadRequest("Professor nao encontrado");

            var profDto = _mapper.Map<Professor>(prof);

            return Ok(profDto);
        }

        [HttpPost()]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);
            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor nao cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,ProfessorDto model)
        {

            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor nao encontrado");

            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor nao atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorDto model)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor nao encontrado");

            _mapper.Map(model, professor);
            _repo.Update(professor);

            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
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


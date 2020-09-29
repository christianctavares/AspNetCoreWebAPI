using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.V1.Dtos;
using SmartSchool.API.Models;
using SmartSchool.API.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartSchool.API.V1.Controllers
{
    /// <summary>
    /// Versao 1 do meu controlador de alunos
    /// </summary>
    /// 
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AlunoController : ControllerBase
    {

        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Metodo que retorna todos os meus alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);
            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }

        /// <summary>
        /// metodo que retorna um unico alunoDTO
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistarDto());
        }


        /// <summary>
        /// metodo que retorna um unico aluno por  meido do id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno nao encontrado");

            var alunoDto = _mapper.Map<Aluno>(aluno);
            return Ok(alunoDto);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _repo.GetAllAlunos().FirstOrDefault(x => x.Nome.Contains(nome) && x.Sobrenome.Contains(sobrenome));
            if (aluno == null) return BadRequest("Aluno nao encontrado");
            var alunoDto = _mapper.Map<Aluno>(aluno);
            return Ok(alunoDto);
        }

        [HttpPost()]
        public IActionResult Post(AlunoRegistarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno nao cadastrado");

        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoDto model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno nao encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<Aluno>(aluno));
            }
            return BadRequest("Aluno nao atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoDto model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno nao encontrado");

            _mapper.Map(model, aluno);
            _repo.Update(aluno);

            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<Aluno>(aluno));
            }
            return BadRequest("Aluno nao atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno nao encontrado");

            _repo.Update(alu);

            if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado");
            }
            return BadRequest("Aluno nao cadastrado");
        }
    }
}

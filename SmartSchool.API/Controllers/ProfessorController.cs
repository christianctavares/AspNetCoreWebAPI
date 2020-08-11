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
    public class ProfessorController : ControllerBase
    {

        public List<Professor> professores = new List<Professor>()
        {
            new Professor()
            {
                Id = 1,
                Nome = "Luciano",
            }
        };

        public ProfessorController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Profesores: Martha, Paula, Lucas, Rafa");
        }

    }
}


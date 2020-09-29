using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V1.Dtos
{
    public class AlunoDto
    {
     

        /// <summary>
        /// Id do aluno e chave do banco
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Matricula do aluno para utilizar em outras acoes
        /// </summary>
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public DateTime DataInic { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
        
    }
}

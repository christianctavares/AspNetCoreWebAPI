using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {

        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        //ALUNOS

        Aluno[] GetAllAlunos(bool includesProfessor = false);
        Aluno[] GetAllAlunosByDisciplinaID(int disciplinaId, bool includesProfessor = false);
        Aluno GetAlunoById(int alunoId, bool includesProfessor = false);

        //PROFESSORES

        Professor[] GetAllProfessores(bool includesAluno = false);
        Professor[] GetAllProfessoresByDisciplinaID(int disciplinaId, bool includesAluno = false);
        Professor GetProfessorById(int professorId, bool includesDisciplina = false);
    }
}

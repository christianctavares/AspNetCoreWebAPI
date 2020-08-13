using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;

        public Repository(SmartContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }


        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public Aluno[] GetAllAlunos(bool includesProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includesProfessor)
            {
                query = query.Include(a => a.AlunosDisciplina)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaID(int disciplinaId, bool includesProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includesProfessor)
            {
                query = query.Include(a => a.AlunosDisciplina)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(x => x.Id)
                         .Where(aluno => aluno.AlunosDisciplina.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includesProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includesProfessor)
            {
                query = query.Include(a => a.AlunosDisciplina)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(x => x.Id)
                         .Where(x => x.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includesAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includesAluno)
            {
                query = query.Include(a => a.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplina)
                             .ThenInclude(d => d.Aluno);
            }

            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaID(int disciplinaId, bool includesAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includesAluno)
            {
                query = query.Include(a => a.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplina)
                             .ThenInclude(d => d.Aluno);
            }

            query = query.AsNoTracking()
                .OrderBy(aluno => aluno.Id)
                .Where(aluno => aluno.Disciplinas.Any(
                    d => d.AlunosDisciplina.Any(ad => ad.DisciplinaId == disciplinaId)
                ));

            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includesDisciplina = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includesDisciplina)
            {
                query = query.Include(a => a.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplina)
                             .ThenInclude(d => d.Aluno);
            }

            query = query.AsNoTracking().OrderBy(x => x.Id).Where(x => x.Id == professorId);
            return query.FirstOrDefault();
        }
    }
}

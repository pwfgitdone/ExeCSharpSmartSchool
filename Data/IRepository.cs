using System.Threading.Tasks;
using SmartSchool_WebAPI.Models;

namespace SmartSchool_WebAPI.Data
{
    public interface IRepository
    {
        //GERAL
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //ALUNO
        Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor);        
        Task<Aluno[]> GetAlunosAsyncByDisciplinaId(int disciplinaId, bool includeDisciplina);
        Task<Aluno> GetAlunoAsyncById(int alunoId, bool includeProfessor);
        
        //PROFESSOR
        Task<Professor[]> GetAllProfessoresAsync(bool includeAluno);
        Task<Professor> GetProfessorAsyncById(int professorId, bool includeAluno);
        Task<Professor[]> GetProfessoresAsyncByAlunoId(int alunoId, bool includeDisciplina);
        Task<Aluno[]> GetAllAlunosByProfessorAsync(int professorId, bool includeDisciplina);
        

        //DISCIPLINA
        Task<Disciplina[]> GetAllDisciplinasAsync(bool includeProfessor);
        Task<Disciplina> GetDisciplinaAsyncById(int disciplinaId, bool includeProfessor);

        //MATRÍCULA
        Task<AlunoDisciplina[]> GetAllMatriculasAsync();
        Task<AlunoDisciplina> GetMatriculaAsyncByAlunoIdDisciplinaId(int alunoId, int disciplinaId);
    }
}
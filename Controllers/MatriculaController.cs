using Microsoft.AspNetCore.Mvc;
using SmartSchool_WebAPI.Data;
using SmartSchool_WebAPI.Models;

namespace SmartSchool_WebAPI.Controllers
{
    [ApiController]
    [Route("api/matricula")]
    public class MatriculaController : ControllerBase
    {
        private readonly IRepository _repo;
        public MatriculaController(IRepository repo)
        {
            _repo = repo;
        }

        public IRepository Repo { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try {
                var result = await _repo.GetAllMatriculasAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{AlunoId}/{DisciplinaId}")]
        public async Task<IActionResult> GetByAlunoIdDisciplinaId(int AlunoId, int DisciplinaId)
        {
            
            try {
                var result = await _repo.GetMatriculaAsyncByAlunoIdDisciplinaId(AlunoId, DisciplinaId);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(AlunoDisciplina model) {
            
            try {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync()) {
                    return Ok(model);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPut("{AlunoId}/{DisciplinaId}")]
        public async Task<IActionResult> put(int AlunoId, int DisciplinaId, AlunoDisciplina model) {
            
            try {
                var matricula = await _repo.GetMatriculaAsyncByAlunoIdDisciplinaId(AlunoId, DisciplinaId);
                if(matricula == null) return NotFound(new { message = "Matrícula não encontrada" });

                _repo.Update(model);

                if(await _repo.SaveChangesAsync()) {
                    return Ok(model);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpDelete("{AlunoId}/{DisciplinaId}")]
        public async Task<IActionResult> delete(int AlunoId, int DisciplinaId) {
            
            try {
                var matricula = await _repo.GetMatriculaAsyncByAlunoIdDisciplinaId(AlunoId, DisciplinaId);
                if(matricula == null) return NotFound();

                _repo.Delete(matricula);

                if(await _repo.SaveChangesAsync()) {
                    return Ok(new { message = "Matrícula deletada" });
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest();
        }

    }
}

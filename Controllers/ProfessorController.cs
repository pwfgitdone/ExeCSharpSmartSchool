using Microsoft.AspNetCore.Mvc;
using SmartSchool_WebAPI.Data;
using SmartSchool_WebAPI.Models;

namespace SmartSchool_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        public IRepository Repo { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try {
                var result = await _repo.GetAllProfessoresAsync(true);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{ProfessorId}")]
        public async Task<IActionResult> GetByProfessorId(int ProfessorId)
        {
            
            try {
                var result = await _repo.GetProfessorAsyncById(ProfessorId, true);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("AlunosDe/{ProfessorId}")]
        public async Task<IActionResult> GetAlunosByProfessorId(int ProfessorId)
        {
            
            try {
                var result = await _repo.GetAllAlunosByProfessorAsync(ProfessorId, true);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("ByAluno/{alunoId}")]
        public async Task<IActionResult> GetByAlunoId(int alunoId) {
            
            try {
                var result = await _repo.GetProfessoresAsyncByAlunoId(alunoId, false);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Professor model) {
            
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

        [HttpPut("{professorId}")]
        public async Task<IActionResult> put(int professorId, Professor model) {
            
            try {
                var professor = await _repo.GetProfessorAsyncById(professorId, false);
                if(professor == null) return NotFound(new { message = "Professor não encontrado" });

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

        [HttpDelete("{professorId}")]
        public async Task<IActionResult> delete(int professorId) {
            
            try {
                var professor = await _repo.GetProfessorAsyncById(professorId, false);
                if(professor == null) return NotFound();

                _repo.Delete(professor);

                if(await _repo.SaveChangesAsync()) {
                    return Ok(new { message = "Professor deletado" });
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
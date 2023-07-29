using Microsoft.AspNetCore.Mvc;
using SmartSchool_WebAPI.Data;
using SmartSchool_WebAPI.Models;

namespace SmartSchool_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly IRepository _repo;
        public DisciplinaController(IRepository repo)
        {
            _repo = repo;
        }

        public IRepository Repo { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try {
                var result = await _repo.GetAllDisciplinasAsync(true);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{DisciplinaId}")]
        public async Task<IActionResult> GetByDisciplinaId(int DisciplinaId)
        {
            
            try {
                var result = await _repo.GetDisciplinaAsyncById(DisciplinaId, true);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Disciplina model) {
            
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

        [HttpPut("{disciplinaId}")]
        public async Task<IActionResult> put(int disciplinaId, Disciplina model) {
            
            try {
                var disciplina = await _repo.GetDisciplinaAsyncById(disciplinaId, false);
                if(disciplina == null) return NotFound(new { message = "Disciplina não encontrada" });

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

        [HttpDelete("{disciplinaId}")]
        public async Task<IActionResult> delete(int disciplinaId) {
            
            try {
                var disciplina = await _repo.GetDisciplinaAsyncById(disciplinaId, false);
                if(disciplina == null) return NotFound();

                _repo.Delete(disciplina);

                if(await _repo.SaveChangesAsync()) {
                    return Ok(new { message = "Disciplina deletada" });
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

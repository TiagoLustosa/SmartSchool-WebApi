using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool_WebApi.Data;
using SmartSchool_WebApi.Models;

namespace SmartSchool_WebApi.Controllers
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
       [HttpGet]
        public async Task<IActionResult> Get()
        {
            try{
                var result = await _repo.GetAllProfessoresAsync(false);
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
        }
        [HttpGet("{ProfessorId}")]
        public async Task<IActionResult> GetByAlunoId(int ProfessorId)
        {
            
            try
            {
                var result = await _repo.GetProfessorAsyncById(ProfessorId, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
        [HttpGet("ByAluno/{AlunoId}")]
        public async Task<IActionResult> GetProfessorByAlunoId (int AlunoId){
            try{
                var result = await _repo.GetProfessoresAsyncByAlunoId(AlunoId, false);
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Professor model){
            try{
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()){
                    return Ok(model);
                }
            }
            catch(Exception ex){
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest();
        }

         [HttpPut("{professorId}")]
        public async Task<IActionResult> put(int professorId, Professor model)
        {
            try
            {
                var Professor = await _repo.GetProfessorAsyncById(professorId, false);
                if(Professor == null) return NotFound();

                _repo.Update(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok(model);
                }                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            return BadRequest();
        }
        [HttpDelete("{ProfessorId}")]
        public async Task<IActionResult> Delete(int professorId)
        {
            try
            {
                var professor = await _repo.GetProfessorAsyncById(professorId, false);
                if(professor == null)
                {
                    return NotFound();
                }
                _repo.Delete(professor);
                if(await _repo.SaveChangesAsync())
                {
                    return Ok("Professor Deletado.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest();
        }
    }

}
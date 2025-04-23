using Microsoft.AspNetCore.Mvc;
using UniChat_BLL;
using UniChat_BLL.Dto;

namespace UniChat_BackEnd.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SemesterController : ControllerBase
  {
    private readonly SemesterService _semesterService;

    public SemesterController(SemesterService semesterService)
    {
      _semesterService = semesterService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSemesterById(int id)
    {
      var semester = await _semesterService.GetSemesterByIdAsync(id);
      if (semester == null) return NotFound();
      return Ok(semester);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSemesters()
    {
      var semesters = await _semesterService.GetAllSemestersAsync();
      return Ok(semesters);
    }

    [HttpPost]
    public async Task<IActionResult> AddSemester([FromBody] SemesterDto semesterDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      await _semesterService.AddSemesterAsync(semesterDto);
      return CreatedAtAction(nameof(GetSemesterById), new { id = semesterDto.Id }, semesterDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSemester(int id, [FromBody] SemesterDto semesterDto)
    {
      if (id != semesterDto.Id) return BadRequest("ID mismatch");
      await _semesterService.UpdateSemesterAsync(semesterDto);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSemester(int id)
    {
      await _semesterService.DeleteSemesterAsync(id);
      return NoContent();
    }
  }
}
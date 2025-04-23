using Microsoft.AspNetCore.Mvc;
using UniChat_BLL;
using UniChat_BLL.Dto;

namespace UniChat_BackEnd.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StudyController : ControllerBase
  {
    private readonly StudyService _studyService;

    public StudyController(StudyService studyService)
    {
      _studyService = studyService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudyById(int id)
    {
      var study = await _studyService.GetStudyByIdAsync(id);
      if (study == null) return NotFound();
      return Ok(study);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudies()
    {
      var studies = await _studyService.GetAllStudiesAsync();
      return Ok(studies);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudy([FromBody] StudyDto studyDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      await _studyService.AddStudyAsync(studyDto);
      return CreatedAtAction(nameof(GetStudyById), new { id = studyDto.Id }, studyDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudy(int id, [FromBody] StudyDto studyDto)
    {
      if (id != studyDto.Id) return BadRequest("ID mismatch");
      await _studyService.UpdateStudyAsync(studyDto);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudy(int id)
    {
      await _studyService.DeleteStudyAsync(id);
      return NoContent();
    }
  }
}
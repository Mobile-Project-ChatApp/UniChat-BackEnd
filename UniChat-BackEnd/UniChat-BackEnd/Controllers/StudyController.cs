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

    [HttpGet]
    public IActionResult GetAllStudies()
    {
      List<StudyDto> studies = _studyService.GetAllStudies();
      return Ok(studies);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudyById(int id)
    {
      StudyDto study = _studyService.GetStudyById(id);
      if (study == null)
      {
        return NotFound();
      }
      return Ok(study);
    }

    [HttpPost]
    public IActionResult AddStudy([FromBody] CreateEditStudyDto study)
    {
      if (study == null)
      {
        return BadRequest("Invalid study data.");
      }

      bool result = _studyService.AddStudy(study);
      if (!result)
      {
        return BadRequest("Study already exists.");
      }

      return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudy(int id, [FromBody] CreateEditStudyDto study)
    {
      if (study == null)
      {
        return BadRequest("Invalid study data.");
      }

      bool result = _studyService.UpdateStudy(id, study);
      if (!result)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudy(int id)
    {
      bool result = _studyService.DeleteStudy(id);
      if (!result)
      {
        return NotFound();
      }

      return Ok(result);
    }
  }
}
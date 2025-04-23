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

    [HttpGet]
    public IActionResult GetAllSemesters()
    {
      List<SemesterDto> semesters = _semesterService.GetAllSemesters();
      return Ok(semesters);
    }

    [HttpGet("{id}")]
    public IActionResult GetSemesterById(int id)
    {
      SemesterDto semester = _semesterService.GetSemesterById(id);
      if (semester == null)
      {
        return NotFound();
      }
      return Ok(semester);
    }

    [HttpPost]
    public IActionResult AddSemester([FromBody] CreateEditSemesterDto semester)
    {
      if (semester == null)
      {
        return BadRequest("Invalid semester data.");
      }

      bool result = _semesterService.AddSemester(semester);
      if (!result)
      {
        return BadRequest("Semester already exists.");
      }

      return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSemester(int id, [FromBody] CreateEditSemesterDto semester)
    {
      if (semester == null)
      {
        return BadRequest("Invalid semester data.");
      }

      bool result = _semesterService.UpdateSemester(id, semester);
      if (!result)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSemester(int id)
    {
      bool result = _semesterService.DeleteSemester(id);
      if (!result)
      {
        return NotFound();
      }

      return Ok(result);
    }
  }
}
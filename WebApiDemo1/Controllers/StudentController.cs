using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo1.Dtos;
using WebApiDemo1.Entities;
using WebApiDemo1.Services.Abstract;

namespace WebApiDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet]
        public async Task<IEnumerable<StudentDto>> Get()
        {
            var items = await _studentService.GetAllAsync();
            var dataToReturn = items.Select(s =>
            {
                return new StudentDto
                {
                    Id = s.Id,
                    Age = s.Age,
                    Fullname = s.Fullname,
                    Score = s.Score,
                    SerioNo = s.SerioNo
                };
            });
            return dataToReturn;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _studentService.GetAsync(s => s.Id == id);
            if (item == null) return NotFound();
            var dto = new StudentDto
            {
                Id = item.Id,
                Age = item.Age,
                Fullname = item.Fullname,
                Score = item.Score,
                SerioNo = item.SerioNo
            };
            return Ok(dto);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentAddDto dto)
        {
            var entity = new Student
            {
                Age = dto.Age,
                Fullname = dto.Fullname,
                Score = dto.Score,
                SerioNo = dto.SerioNo
            };
            await _studentService.AddAsync(entity);
            return Ok(entity);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentAddDto dto)
        {
            var entity = _studentService.GetAsync(s => s.Id == id).Result;

            if (entity == null) return NotFound();

            entity.Fullname = dto.Fullname;
            entity.SerioNo = dto.SerioNo;
            entity.Score = dto.Score;
            entity.Age = dto.Age;

            await _studentService.UpdateAsync(entity);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _studentService.GetAsync(s => s.Id == id).Result;

            if (entity == null) return NotFound();

            await _studentService.DeleteAsync(entity);

            return Ok();
        }


    }
}

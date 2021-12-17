using DevelopmentFast.Infra.ProjectTest.DomainTest.Entities;
using DevelopmentFast.Infra.ProjectTest.DomainTest.Repositories;
using DevelopmentFast.Repository.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevelopmentFast.Infra.ProjectTest.APITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Student>>> GetAllAsync([FromServices] IStudentRepository studentRepository, [FromServices] IBaseRedisRepositoryDF baseRedisRepositoryDF)
        {
            var teste = await baseRedisRepositoryDF.GetAsync<List<Student>>("TESTE");
            if(teste == null)
            {
                var objs = await studentRepository.GetAll().Include(x => x.Courses).AsNoTrackingWithIdentityResolution().ToListAsync();
                await baseRedisRepositoryDF.CreateOrUpdateAsync<List<Student>>("TESTE", objs, TimeSpan.FromMinutes(1));
                return Ok(objs);

            }
            return Ok(teste);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<Student>> GetByIdAsync(string Id, [FromServices] IStudentRepository studentRepository)
        {
            var result = await studentRepository.GetById(Id).Include(x=> x.Courses).SingleOrDefaultAsync();


            return result == null? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IAsyncEnumerable<Student>>> SaveAsync([FromBody] Student student, [FromServices] IStudentRepository studentRepository)
        {
            return Ok(await studentRepository.CreateAsync(student));
        }

        [HttpPut]
        public async Task<ActionResult<IAsyncEnumerable<Student>>> PutAsync([FromBody] Student student,[FromServices] IStudentRepository studentRepository)
        {
            return Ok(await studentRepository.UpdateAsync(student));
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult<Student>> DeleteAsync(string Id, [FromServices] IStudentRepository studentRepository)
        {
            var entity = await studentRepository.GetById(Id).SingleOrDefaultAsync();
            return entity is null ? NotFound() : Ok(await studentRepository.DeleteAsync(entity));
            
        }


    }
}

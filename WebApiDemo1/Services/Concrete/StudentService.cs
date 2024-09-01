using System.Linq.Expressions;
using WebApiDemo1.Entities;
using WebApiDemo1.Repository.Abstract;
using WebApiDemo1.Services.Abstract;

namespace WebApiDemo1.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Student entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(Student entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Student> GetAsync(Expression<Func<Student, bool>> predicate)
        {
            return await _repository.GetAsync(predicate);
        }

        public async Task UpdateAsync(Student entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}

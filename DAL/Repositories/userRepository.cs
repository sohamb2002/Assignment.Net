using MyApp.DAL.Entity;
using MyApp.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.DAL.DBContext;
using MyApp.DAL.Repository;

namespace MyApp.DAL.Repositories
{
    public class UserRepository : Repository<User, AssignmentNetContext>, IUserRepository
    {
        private readonly AssignmentNetContext _context;

        public UserRepository(AssignmentNetContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
//             public async Task<User> GetByIdAsync(int id)
// {
    
//     return await EMDBContext.Set<User>().FirstOrDefaultAsync(u => u.Id == id);
// }
    }
}

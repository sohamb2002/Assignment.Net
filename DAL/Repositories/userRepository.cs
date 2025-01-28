
using MyApp.DAL.DBContext;
using MyApp.DAL.Entity;
using MyApp.DAL.IRepositories;
using MyApp.DAL.Repository;

namespace MyApp.DAL.Repositories
{
    public class UserRepository :Repository<User, AssignmentNetContext>, IUserRepository
    {
        public UserRepository(AssignmentNetContext context) : base(context)
        {
        }

        
    }
}
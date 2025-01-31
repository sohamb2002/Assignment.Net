using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApp.DAL.Entity;
using MyApp.DAL.IRepositories;
using MyApp.DAL.DBContext;
using MyApp.DAL.Repository;

public class CategoryRepository : Repository<Category, AssignmentNetContext>, ICategoryRepository
{
    private readonly AssignmentNetContext _context;

    public CategoryRepository(AssignmentNetContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}

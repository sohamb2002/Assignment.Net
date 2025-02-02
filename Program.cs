using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.BAL.IServices;
using MyApp.BAL.Services;
using MyApp.DAL.DBContext;
using MyApp.DAL.IRepositories;
using MyApp.DAL.Repositories;
using MyApp.DAL.Entity;
using MyApp.DAL.Entity.DTO;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DBContext
            builder.Services.AddDbContext<AssignmentNetContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Assignment.Net")));

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            // Repository

            // Services 

            // Register Services
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<IPostRepository, PostRepository>();
            // builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

            // Add Controllers
            builder.Services.AddControllers();

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            

            var app = builder.Build();

           

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }

    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();
        }
    }
}

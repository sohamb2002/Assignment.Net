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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()); // ADD THIS
});

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
app.UseCors("AllowAngular");
           

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
              CreateMap<User, UserDTO>().ReverseMap(); // Maps from User to UserDTO and vice versa
        
        // Mapping for Update
        CreateMap<UpdateUserDTO, User>(); // Maps from UpdateUserDTO to User (no ReverseMap since you're not mapping back)
        
        // Similarly, if you have Post and UpdatePostDTO
        CreateMap<Post, PostDTO>().ReverseMap(); // Maps from Post to PostDTO and vice versa
        CreateMap<EditPostDTO, Post>(); // Maps from UpdatePostDTO to Post
        }
    }
}

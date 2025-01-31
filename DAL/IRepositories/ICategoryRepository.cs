using System.Collections.Generic;
using System.ComponentModel;
using MyApp.DAL.Entity;
using MyApp.DAL.Interfaces;

namespace MyApp.DAL.IRepositories
{
public interface ICategoryRepository : IRepository<Category>{
 
  Task<ICollection<Category>> GetAllAsync();

}
}
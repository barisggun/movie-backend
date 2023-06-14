using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface IUserService
    {
        AppUser GetById(int id);
        List<AppUser> GetAll();
        void Create(AppUser appUser);
        void Update(AppUser appUser);
        void Delete(AppUser appUser);
    }
}

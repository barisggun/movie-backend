using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        Context c = new Context();

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Create(AppUser appUser)
        {
            _userDal.Create(appUser);
        }

        public void Delete(AppUser appUser)
        {
            _userDal.Delete(appUser);
        }

        public List<AppUser> GetAll()
        {
            return _userDal.GetAll();
        }

        public AppUser GetById(int id)
        {
            return _userDal.GetById(id);
        }

        public void Update(AppUser appUser)
        {
            _userDal.Update(appUser);
        }
    }
}

using MovieApp.EntityLayer.Entities;
using MovieApp.EntityLayer.Entities.ConnectionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Abstract
{
    public interface IHomepageCoverService
    {
        HomepageCover GetById(int id);
        List<HomepageCover> GetAll();
        void Create(HomepageCover homepageCover);
        void Update(HomepageCover homepageCover);
        void Delete(HomepageCover homepageCover);
    }
}

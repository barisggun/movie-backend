using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Concrete
{
    public class HomepageCoverManager : IHomepageCoverService
    {
        IHomepageCoverDal _homepageCoverDal;

        public HomepageCoverManager(IHomepageCoverDal homepageCoverDal)
        {
            _homepageCoverDal = homepageCoverDal;
        }

        public void Create(HomepageCover homepageCover)
        {
            _homepageCoverDal.Create(homepageCover);
        }

        public void Delete(HomepageCover homepageCover)
        {
            _homepageCoverDal.Delete(homepageCover);
        }

        public List<HomepageCover> GetAll()
        {
            return _homepageCoverDal.GetAll();
        }

        public HomepageCover GetById(int id)
        {
            return _homepageCoverDal.GetById(id);
        }

        public void Update(HomepageCover homepageCover)
        {
            _homepageCoverDal.Update(homepageCover);
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using MovieApp.BusinessLayer.Abstract;
using MovieApp.DataAccess.Abstract;
using MovieApp.DataAccess.Concrete;
using MovieApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.BusinessLayer.Concrete
{
    public class BlogManager:IBlogService
    {
        IBlogDal _blogDal;
        Context c = new Context();

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public void Create(Blog blog)
        {
            blog.UpdateSlug();
           _blogDal.Create(blog);
        }

        public void Delete(Blog blog)
        {
            _blogDal.Delete(blog);
        }

        public List<Blog> GetAll()
        {
            return _blogDal.GetAll();
        }

        public Blog GetById(int id)
        {
            return _blogDal.GetById(id);
        }

        public Blog GetBySlug(string slug)
        {
            var blog = c.Blogs
            .Include(m => m.Movies)
            .FirstOrDefault(m => m.Slug == slug);

            return blog;
        }

        public void Update(Blog blog)
        {
            _blogDal.Update(blog);
        }

        public List<Blog> GetLast3Blog()
        {
            return _blogDal.GetAll().Take(3).ToList();
        }

        public List<Blog> GetBlogListWithMovie()
        {
            return _blogDal.GetListWithMovie();
        }
        public List<Blog> GetBlogListWithMovieAndWriter()
        {
            var blogs = c.Blogs.Include(b => b.Movies)
                .Include(b => b.AppUsers)
                .ToList();
            return blogs;
        }

        public List<Blog> GetBlogListByWriter(int id)
        {
            return _blogDal.GetAll(x => x.AppUserId == id);
        }

        public List<Blog> GetListWithMovieByWriterBm(int id)
        {
            return _blogDal.GetListWithMovieByWriter(id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Models;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class NewsService : INewsService
    {
        private readonly RealEstateContext _context;

        public NewsService(RealEstateContext context)
        {
            _context = context;
        }

        public bool AddNewArticle(NewsModel newsModel)
        {
            try
            {
                var person = _context.Persons.FirstOrDefault(p => p.Email == newsModel.AuthorEmail);
                if(person != null) 
                {
                    var newsEntity = new News
                    {
                        Title = newsModel.Title,
                        Description = newsModel.Description,
                        AuthorID = person.PersonID
                    };
                    _context.News.Add(newsEntity);
                    _context.SaveChanges();

                    return true;
                }
                return false;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }       

        }

        public IEnumerable<NewsModel> GetAllNews()
        {
            try
            {
                var allNews = (from article in _context.News
                               join person in _context.Persons on article.AuthorID equals person.PersonID
                               where article.IsDeleted == false
                               select new NewsModel
                               {
                                   Title = article.Title,
                                   Description = article.Description,
                                   AuthorEmail = person.Email,
                                   AuthorName = person.Name,
                                   AuthorSurname = person.Surname,
                                   PublishedDate = article.CreateDate,
                                   NewsID = article.NewsID
                               }).ToList();

                return allNews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public NewsModel GetAnArticle(int newsID)
        {
            var article = (from news in _context.News
                          join person in _context.Persons on news.AuthorID equals person.PersonID
                          where news.NewsID == newsID
                          where news.IsDeleted == false
                          select new NewsModel
                          {
                              Title = news.Title,
                              Description = news.Description,
                              AuthorEmail = person.Email,
                              AuthorName = person.Name,
                              AuthorSurname = person.Surname,
                              PublishedDate = news.CreateDate,
                              NewsID = news.NewsID
                          }).SingleOrDefault();
            return article;
        }
    }
}

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
    public class AddNewArticleService : IAddNewArticleService
    {
        private readonly RealEstateContext _context;

        public AddNewArticleService(RealEstateContext context)
        {
            _context = context;
        }

        public bool AddNewArticle(ArticleModel articleModel)
        {
            try
            {
                var person = _context.Persons.FirstOrDefault(p => p.Email == articleModel.AuthorEmail);
                if(person != null) 
                {
                    var newsEntity = new News
                    {
                        Title = articleModel.Title,
                        Description = articleModel.Description,
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
    }
}

using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class PublisherService: IPublisherService
    {
        private BookStoreDbContext dbContext;

        public PublisherService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IList<Publisher> GetPublishers()
        {
            var publishers = dbContext.Publisher.AsNoTracking().ToList();
            return publishers;
        }
    }
}

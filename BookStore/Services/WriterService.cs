using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class WriterService : IWriterService
    {
        private BookStoreDbContext dbContext;

        public WriterService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<Writer> GetWriters()
        {
            var writers = dbContext.Writers.AsNoTracking().ToList();
            return writers;
           
        }
    }
}

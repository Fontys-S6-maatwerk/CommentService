using CommentService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Repositories
{
    public class CommentContext : DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> options) :base(options)
        {

        }

        public DbSet<CommentDataModel> Comments { get; set; }
        public DbSet<UserDBO> Users { get; set; }
    }
}

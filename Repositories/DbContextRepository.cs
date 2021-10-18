using CommentService.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Repositories
{
    public class DbContextRepository
    {
        private CommentContext commentContext;
        public DbContextRepository(CommentContext context)
        {
            commentContext = context;
        }

        public CommentDataModel CreateComment(CommentDataModel comment)
        {
            EntityEntry<CommentDataModel> addedComment = commentContext.Comments.Add(comment);
            commentContext.SaveChanges();
            return addedComment.Entity;
        }
    }
}

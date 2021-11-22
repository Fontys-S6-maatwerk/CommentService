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

        internal CommentPage GetComments(Guid id, int pageNumber, int pageSize)
        {
            int totalElements = commentContext.Comments
                .Where(exercise => exercise.SolutionId.Equals(id))
                .Count();
            int totalPages = totalElements / pageSize;

            return new CommentPage()
            {
                Items = commentContext.Comments
                    .Where(exercise => exercise.SolutionId.Equals(id))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                TotalPages = totalElements % pageSize == 0 ? totalPages : totalPages + 1,
                TotalElements = totalElements
            };
        }
    }
}

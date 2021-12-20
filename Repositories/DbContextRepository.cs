using CommentService.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Repositories
{
    public class DbContextRepository : IRepository
    {
        private CommentContext commentContext;
        public DbContextRepository(CommentContext context)
        {
            commentContext = context;
        }

        public CommentDataModel Create(CommentDataModel comment)
        {
            if (commentContext.Users.Any(x => x.Id.Equals(comment.UserId))) comment.User = null;

            EntityEntry<CommentDataModel> addedComment = commentContext.Comments.Add(comment);
            commentContext.SaveChanges();

            if (addedComment.Entity.User == null) addedComment.Entity.User = GetUser(addedComment.Entity.UserId);

            return addedComment.Entity;
        }

        private UserDBO GetUser(Guid userId)
        {
            return commentContext.Users.Find(userId);
        }

        public void UpdateUser(UserDBO user)
        {
            commentContext.Users.Update(user);
            commentContext.SaveChanges();
        }

        public void AddUser(UserDBO user)
        {
            commentContext.Users.Add(user);
            commentContext.SaveChanges();
        }

        public CommentPage Get(Guid id, int pageNumber, int pageSize)
        {
            int totalElements = commentContext.Comments
                .Where(exercise => exercise.SolutionId.Equals(id))
                .Count();
            int totalPages = totalElements / pageSize;

            return new CommentPage()
            {
                Items = commentContext.Comments
                    .Include(comment => comment.User)
                    .Where(comment => comment.SolutionId.Equals(id))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
                    .Select(comment => { comment.User = GetUser(comment.UserId); return comment; })
                    .ToList(),
                TotalPages = totalElements % pageSize == 0 ? totalPages : totalPages + 1,
                TotalElements = totalElements
            };
        }
    }
}

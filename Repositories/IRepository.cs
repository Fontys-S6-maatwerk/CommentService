using CommentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Repositories
{
    public interface IRepository
    {
        public CommentPage Get(Guid solutionId, int pageNumber, int pageSize);
        public CommentDataModel Create(CommentDataModel comment);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Models
{
    public class CommentQueryModel
    {
        public Guid Id { get; set; }
        public Guid SolutionId { get; set; }
        public Guid UserId { get; set; }
        public UserQM User { get; set; }
        public string Content { get; set; }
        public long CreationTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Models
{
    public class CommentDataModel
    {
        public Guid Id { get; set; }
        public Guid SolutionId { get; set; }
        public Guid UserId { get; set; }
        public string text { get; set; }
    }
}

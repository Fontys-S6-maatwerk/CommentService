using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Models
{
    public class CommentPageResponse
    {
        public List<CommentQueryModel> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalElements { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Models
{
    public class CommentDataModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid SolutionId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public long CreationTime { get; set; }
    }
}

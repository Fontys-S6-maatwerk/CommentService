using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommentService.Models
{
    public class UserDBO
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [ForeignKey("UserId")]
        public ICollection<CommentDataModel> Comments { get; set; }
    }
}

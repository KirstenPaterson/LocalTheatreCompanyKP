using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalTheatreCompany.Models
{
    public class Comment
    {
        [Key]
        public int commentId { get; set; }

       
        public int articleId { get; set; }

       
        [DataType(DataType.MultilineText)]
        public String commentText { get; set; }

        
        public String commentAuthor { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
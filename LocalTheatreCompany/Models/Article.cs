using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalTheatreCompany.Models
{
    public class Article
    {
        /// <summary>
        /// 
        /// </summary>
        /// 

        [Key]
        public int articleId { get; set; }
        
        public String title { get; set; }

        public String UserName{ get; set; }

        [DataType(DataType.MultilineText)]
        public String postText { get; set; }
        
        public DateTime postDate { get; set; }

        public List<Comment> Comments { get; set; }


    }
}
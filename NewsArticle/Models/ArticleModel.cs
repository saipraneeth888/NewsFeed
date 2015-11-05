using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsArticle.Models
{
    public class ArticleModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsArticle.Models
{
    interface IArticleRepository
    {
        List<ArticleModel> GetAllArticles();

        List<ArticleModel> GetMyArticles(string userName);

        Boolean DeleteArticle(string id);

        Boolean EditArticle(string id, string title, string description);

        Boolean AddNewArticle(ArticleModel a);
    }
}

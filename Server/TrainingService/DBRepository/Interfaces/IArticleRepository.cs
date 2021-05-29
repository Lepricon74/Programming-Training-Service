using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingService.Models.ResponsesModels;
using TrainingService.Models;

namespace TrainingService.DBRepository.Interfaces
{
    public interface IArticleRepository
    {
        public List<ArticleResponse> GetArticlesList();
        public Article GetArticle(int articleId);

        public void AddArticle(Article newArticle);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Exceptions;
using BLL.AddModels;
using BLL.VievModels;

namespace BLL.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private async Task<string> GetThemeNameAsync(int id)
        {
            var theme = await _unitOfWork.ThemeRepo
                    .GetByIdAsync(id);
            return theme.Name;
        }
        private async Task<double> GetArticleAvgRate(int id)
        {
            var res = await _unitOfWork.ArticleRateRepo.GetAllWithDetails();
            res.ToList();
            if(res.Count()==0)
            {
                return 0;
            }
            return res.Select(x => x.Rate).Average(); ;
        }




        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ArticelVievModel> AddAsync(ArticleAddmodel item)
        {
            var theme = _unitOfWork.ThemeRepo
              .GetAll()
              .Where(t => t.Name == item.ThemeName)
              .FirstOrDefault();

            if (!Char.IsUpper(item.Body[0]) 
                || !Char.IsUpper(item.Title[0])
                || !item.AuthorFullName.Contains(" ")
                || !Char.IsUpper(item.AuthorFullName[0])
                || theme == null)
            {
                throw new InvalidArgumentException();
            }

            if (_unitOfWork.ArticleRepo
                .GetAll()
                .Select(x => x.Title)
                .FirstOrDefault() == item.Title
                )
            {
                throw new AlreadyExistException();
            }

            Article article = _mapper.Map<Article>(item);
            article.ThemeId = theme.Id;
            article.Created = DateTime.Now;

            await _unitOfWork.ArticleRepo.AddAsync(article);
            _unitOfWork.SaveChanges();

            ArticelVievModel articelVievModel = _mapper.Map<ArticelVievModel>(article);
            articelVievModel.ThemeName = theme.Name;
            articelVievModel.AvgRate = 0;

            return articelVievModel;
        }

        public async Task Delete(int id)
        {
            if (await _unitOfWork.ArticleRepo.GetByIdAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.ArticleRepo.DeleteById(id);
            _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<ArticelVievModel>> GetAllWithDetails()
        {
            var dbArticles = await _unitOfWork.ArticleRepo.GetAllWithDetails();
            dbArticles.ToList();
            List<ArticelVievModel>  result = new List<ArticelVievModel>();
            foreach (var item in dbArticles)
            {
                ArticelVievModel articelVievModel = _mapper.Map<ArticelVievModel>(item);
                articelVievModel.ThemeName = await GetThemeNameAsync(item.ThemeId);
                articelVievModel.AvgRate = await GetArticleAvgRate(item.Id);
                result.Add(articelVievModel);
            }
            return result;
        }

        public async Task<ArticelVievModel> GetByIdWithDetailsAsync(int id)
        {
            var dbArticle = await _unitOfWork.ArticleRepo.GetByIdWithDetaileAsync(id);
            if (dbArticle == null)
            {
                throw new NotFoundException();
            }
            ArticelVievModel articelVievModel = _mapper.Map<ArticelVievModel>(dbArticle);
            articelVievModel.ThemeName = await GetThemeNameAsync(dbArticle.ThemeId);
            articelVievModel.AvgRate = await GetArticleAvgRate(dbArticle.Id);

            return articelVievModel;
        }

        public async Task<IEnumerable<ArticelVievModel>> GetByLength(int length)
        {
            if(length<50)
            {
                length = 50;
            }
            var dbArticles = await _unitOfWork.ArticleRepo
                .GetAllWithDetails();
            dbArticles.ToList();
            List<ArticelVievModel> result = new List<ArticelVievModel>();
            foreach (var item in dbArticles)
            {
                if(item.Body.Length <= length)
                {
                    ArticelVievModel articelVievModel = _mapper.Map<ArticelVievModel>(item);
                    articelVievModel.ThemeName = await GetThemeNameAsync(item.ThemeId);
                    articelVievModel.AvgRate = await GetArticleAvgRate(item.Id);
                    result.Add(articelVievModel);
                }
            }
            return result;
        }

        public async Task<ArticelVievModel> GetByName(string name)
        {
            var dbArticles = await _unitOfWork.ArticleRepo
                .GetAllWithDetails();
            dbArticles.ToList();
            Article dbArticle = null;
            foreach (var item in dbArticles)
            {
                if(item.Title == name)
                {
                    dbArticle = item;
                    break;
                }
            }
 

            if (dbArticle == null)
            {
                throw new NotFoundException();
            }
            
            ArticelVievModel articelVievModel = _mapper.Map<ArticelVievModel>(dbArticle);
            articelVievModel.ThemeName = await GetThemeNameAsync(dbArticle.ThemeId);
            articelVievModel.AvgRate = await GetArticleAvgRate(dbArticle.Id);
            return articelVievModel;
        }

        public async Task<IEnumerable<ArticelVievModel>> GetByRangeOfRate(double max, double min)
        {
            if(max<min)
            {
                var buff = max;
                max = min;
                min = buff;
            }
            var res = await GetAllWithDetails();
            return res.Where(x => x.AvgRate <= max && x.AvgRate >= min);

        }

        public async Task<IEnumerable<ArticelVievModel>> GetByTheme(string themeName)
        {
            var theme = _unitOfWork.ThemeRepo
                .GetAll()
                .Where(x => x.Name == themeName)
                .FirstOrDefault();
            if (theme == null)
            {
                throw new InvalidArgumentException();
            }
            var dbArticles = await _unitOfWork.ArticleRepo
                .GetAllWithDetails();
            dbArticles.ToList();
            if (dbArticles == null)
            {
                throw new NotFoundException();
            }
            List<ArticelVievModel> result = new List<ArticelVievModel>();
            foreach (var item in dbArticles)
            {
                if(item.ThemeId == theme.Id)
                {
                    ArticelVievModel articelVievModel = _mapper.Map<ArticelVievModel>(item);
                    articelVievModel.ThemeName = theme.Name;
                    articelVievModel.AvgRate = await GetArticleAvgRate(item.Id);
                    result.Add(articelVievModel);
                }
            }
            return result;
        }

        public async Task<ArticelVievModel> Update(ArticleAddmodel item)
        {
            var theme = _unitOfWork.ThemeRepo
              .GetAll()
              .Where(t => t.Name == item.ThemeName)
              .FirstOrDefault();

            if (!Char.IsUpper(item.Body[0])
                || !Char.IsUpper(item.Title[0])
                || !item.AuthorFullName.Contains(" ")
                || !Char.IsUpper(item.AuthorFullName[0])
                || theme == null)
            {
                throw new InvalidArgumentException();
            }

            if (_unitOfWork.ArticleRepo
                .GetAll()
                .Select(x => x.Title)
                .FirstOrDefault() != item.Title
                )
            {
                throw new NotFoundException();
            }

            Article article = _mapper.Map<Article>(item);
            article.ThemeId = theme.Id;

            var updatedArticle = _unitOfWork.ArticleRepo.Update(article);
            _unitOfWork.SaveChanges();

            ArticelVievModel articelVievModel = _mapper.Map<ArticelVievModel>(article);
            articelVievModel.ThemeName = theme.Name;
            articelVievModel.AvgRate = await GetArticleAvgRate(updatedArticle.Id);

            return articelVievModel;
        }
    }
}

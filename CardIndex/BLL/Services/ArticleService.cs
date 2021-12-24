using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Exceptions;

namespace BLL.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ArticleModel> AddAsync(ArticleModel item)
        {
            if (!Char.IsUpper(item.Body[0]) || !Char.IsUpper(item.Title[0]))
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

            item.Created = DateTime.Now;
            var addingItem = _mapper.Map<Article>(item);
            await _unitOfWork.ArticleRepo.AddAsync(addingItem);

            return item;
        }

        public async void Delete(int id)
        {
            if (await _unitOfWork.ArticleRepo.GetByIdAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.ArticleRepo.DeleteById(id);
        }

        public IEnumerable<ArticleModel> GetAll()
        {
            var articles = _unitOfWork.ArticleRepo.GetAll().Select(x => _mapper.Map<ArticleModel>(x));
            return articles;
        }

        public IEnumerable<ArticleModel> GetAllWithDetails()
        {
            var articles = _unitOfWork.ArticleRepo.GetAllWithDetails().Select(x => _mapper.Map<ArticleModel>(x));
            return articles;
        }

        public double GetAvgRate(string name)
        {
            var articleId = _unitOfWork.ArticleRepo
                .GetAll()
                .Where(x => x.Title == name)
                .Select(x => x.Id)
                .FirstOrDefault();
            if (articleId == 0)
            {
                throw new NotFoundException();
            }
            var rates = _unitOfWork.ArticleRateRepo
                .GetAll()
                .Where(x => x.ArticleId == articleId)
                .Select(x => x.Rate)
                .AsEnumerable();
            double Avgrate = rates.Sum() / rates.Count();

            return Avgrate;
        }

        public async Task<ArticleModel> GetByIdAsync(int id)
        {
            var result = _mapper.Map<ArticleModel>(await _unitOfWork.ArticleRepo.GetByIdAsync(id));
            if (result == null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public async Task<ArticleModel> GetByIdWithDetailsAsync(int id)
        {
            var result = _mapper.Map<ArticleModel>(await _unitOfWork.ArticleRepo.GetByIdWithDetaileAsync(id));
            if (result == null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public IEnumerable<ArticleModel> GetByName(string name)
        {
            var result = _unitOfWork.ArticleRepo
                .GetAllWithDetails()
                .Where(x => x.Title == name)
                .Select(x => _mapper.Map<ArticleModel>(x));
            if (result == null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public IEnumerable<ArticleModel> GetByTheme(string theme)
        {
            var themeId = _unitOfWork.ThemeRepo
                .GetAll()
                .Where(x => x.Name == theme)
                .Select(x => x.Id)
                .FirstOrDefault();
            if (themeId == 0)
            {
                throw new InvalidArgumentException();
            }
            var articleModel = _unitOfWork.ArticleRepo
                .GetAllWithDetails()
                .Where(x => x.ThemeId == themeId)
                .Select(x => _mapper.Map<ArticleModel>(x));
            if (articleModel == null)
            {
                throw new NotFoundException();
            }

            return articleModel;
        }

        public async Task<ArticleModel> Update(ArticleModel item)
        {
            if (!Char.IsUpper(item.Body[0]) || !Char.IsUpper(item.Title[0]))
            {
                throw new InvalidArgumentException();
            }
            if (await _unitOfWork.ArticleRepo.GetByIdAsync(item.Id) == null)
            {
                throw new NotFoundException();
            }

            var addingItem = _mapper.Map<Article>(item);
            _unitOfWork.ArticleRepo.Update(addingItem);

            return item;
        }
    }
}

using Administration.Exceptions;
using AutoMapper;
using BLL.AddModels;
using BLL.Interfaces;
using BLL.VievModels;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private async Task<string> GetThemeNameAsync(int id)
        {
            var theme = await _unitOfWork.ThemeRepository
                    .GetByIdAsync(id);
            return theme.Name;
        }
        private async Task<double> GetArticleAvgRateAsync(int id)
        {
            var res = await _unitOfWork.CardAssessmentRepository.GetAllWithDetailsAsync();
            var rates = res.Where(x => x.CardId == id).Select(x => x.Rate).ToList();
            if (rates.Count()==0)
            {
                return 0;
            }
            return rates.Average(); 
        }

        public CardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CardVievModel> AddAsync(CardAddmodel item)
        {
            item.AuthorFullName.Trim();
            item.Title.Trim();
            item.Body.Trim();
            item.ThemeName.Trim();
            Char.ToUpper(item.ThemeName[0]);
            Char.ToUpper(item.Body[0]);
            Char.ToUpper(item.Title[0]);
            Char.ToUpper(item.AuthorFullName[0]);

            var theme = _unitOfWork.ThemeRepository
              .GetAll()
              .FirstOrDefault(t => t.Name == item.ThemeName);


            if (
                !item.AuthorFullName.Contains(" ")
                || theme == null)
            {
                throw new InvalidArgumentException();
            }

            if (_unitOfWork.CardRepository
                .GetAll()
                .Select(x => x.Title)
                .FirstOrDefault() == item.Title
                )
            {
                throw new AlreadyExistException();
            }

            item.theme = theme;

            Card article = _mapper.Map<Card>(item);
            article.ThemeId = theme.Id;
            article.Created = DateTime.Now;

            await _unitOfWork.CardRepository.AddAsync(article);
            _unitOfWork.SaveChanges();

            CardVievModel articelVievModel = _mapper.Map<CardVievModel>(article);
            articelVievModel.ThemeName = theme.Name;
            articelVievModel.AvgRate = 0;

            return articelVievModel;
        }

        public async Task DeleteAsync(int id)
        {
            if (await _unitOfWork.CardRepository.GetByIdAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.CardRepository.DeleteById(id);
            _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<CardVievModel>> GetAllWithDetailsAsync()
        {
            var dbArticles = await _unitOfWork.CardRepository.GetAllWithDetailsAsync();
            dbArticles.ToList();
            List<CardVievModel> result = new List<CardVievModel>();
            foreach (var item in dbArticles)
            {
                CardVievModel articelVievModel = _mapper.Map<CardVievModel>(item);
                articelVievModel.ThemeName = await GetThemeNameAsync(item.ThemeId);
                articelVievModel.AvgRate = await GetArticleAvgRateAsync(item.Id);
                result.Add(articelVievModel);
            }
            return result;
        }

        public async Task<IEnumerable<CardVievModel>> GetByLengthAsync(int length)
        {
            if (length < 50)
            {
                length = 50;
            }
            var dbArticles = await _unitOfWork.CardRepository.GetAllWithDetailsAsync();
            dbArticles.ToList();
            List<CardVievModel> result = new List<CardVievModel>();
            foreach (var item in dbArticles)
            {
                if (item.Body.Length <= length)
                {
                    CardVievModel articelVievModel = _mapper.Map<CardVievModel>(item);
                    articelVievModel.ThemeName = await GetThemeNameAsync(item.ThemeId);
                    articelVievModel.AvgRate = await GetArticleAvgRateAsync(item.Id);
                    result.Add(articelVievModel);
                }
            }
            return result;
        }

        public async Task<CardVievModel> GetByNameAsync(string name)
        {
            name.Trim();
            Char.ToUpper(name[0]);
            var dbArticles = await _unitOfWork.CardRepository.GetAllWithDetailsAsync();
            dbArticles.ToList();
            Card dbArticle = null;
            foreach (var item in dbArticles)
            {
                if (item.Title == name)
                {
                    dbArticle = item;
                    break;
                }
            }


            if (dbArticle == null)
            {
                throw new NotFoundException();
            }

            CardVievModel articelVievModel = _mapper.Map<CardVievModel>(dbArticle);
            articelVievModel.ThemeName = await GetThemeNameAsync(dbArticle.ThemeId);
            articelVievModel.AvgRate = await GetArticleAvgRateAsync(dbArticle.Id);
            return articelVievModel;
        }

        public async Task<IEnumerable<CardVievModel>> GetByRangeOfRateAsync(double max, double min)
        {
            if (max < min)
            {
                var buff = max;
                max = min;
                min = buff;
            }
            var res = await GetAllWithDetailsAsync();
            var result = res.Where(x => x.AvgRate <= max && x.AvgRate >= min);
            return result;

        }

        public async Task<IEnumerable<CardVievModel>> GetByThemeAsync(string themeName)
        {
            themeName.Trim();
            Char.ToUpper(themeName[0]);
            var theme = _unitOfWork.ThemeRepository
                .GetAll()
                .Where(x => x.Name == themeName)
                .FirstOrDefault();
            if (theme == null)
            {
                throw new InvalidArgumentException();
            }
            var dbArticles = await _unitOfWork.CardRepository.GetAllWithDetailsAsync();
            dbArticles.ToList();
            if (dbArticles == null)
            {
                throw new NotFoundException();
            }
            List<CardVievModel> result = new List<CardVievModel>();
            foreach (var item in dbArticles)
            {
                if (item.ThemeId == theme.Id)
                {
                    CardVievModel articelVievModel = _mapper.Map<CardVievModel>(item);
                    articelVievModel.ThemeName = theme.Name;
                    articelVievModel.AvgRate = await GetArticleAvgRateAsync(item.Id);
                    result.Add(articelVievModel);
                }
            }
            return result;
        }

        public async Task<CardVievModel> UpdateAsync(CardAddmodel item)
        {
            item.AuthorFullName.Trim();
            item.Title.Trim();
            item.Body.Trim();

            //Char.ToUpper(item.Body[0]);
            //Char.ToUpper(item.Title[0]);
            //Char.ToUpper(item.AuthorFullName[0]);

            var theme = _unitOfWork.ThemeRepository
              .GetAll()
              .FirstOrDefault(t => t.Name == item.ThemeName);


            if (
                !item.AuthorFullName.Contains(" ")
                || theme == null)
            {
                throw new InvalidArgumentException();
            }

            if (_unitOfWork.CardRepository
                .GetAll()
                .Where(x => x.Id==item.Id)
                .Select(x => x.Id)
                .FirstOrDefault() != item.Id
                )
            {
                throw new NotFoundException();
            }

            Card article = _mapper.Map<Card>(item);
            article.ThemeId = theme.Id;

            var updatedArticle = _unitOfWork.CardRepository.Update(article);
            _unitOfWork.SaveChanges();

            CardVievModel articelVievModel = _mapper.Map<CardVievModel>(article);
            articelVievModel.ThemeName = theme.Name;
            articelVievModel.AvgRate = await GetArticleAvgRateAsync(updatedArticle.Id);

            return articelVievModel;
        }
    }
}

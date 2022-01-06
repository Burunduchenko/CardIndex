
using Administration.Exceptions;
using Administration.Interfaces;
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
    public class ArticleRateService : IBaseService<ArticleRateAddModel, ArticleRateVievModel>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleRateService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ArticleRateVievModel> AddAsync(ArticleRateAddModel item)
        {
            if (item.Rate < 0)
            {
                item.Rate = 0;
            }
            else if (item.Rate > 10)
            {
                item.Rate = 10;
            }
            var articlebuff = await _unitOfWork.ArticleRepo
                .GetAllWithDetailsAsync();

            var article = articlebuff.Where(a => a.Title == item.ArticleTitle)
                .FirstOrDefault();

            var dbuser = await _userService
                .GetUsersAsync();

            var user = dbuser.Where(u => u.UserName == item.UserLogin)
                .FirstOrDefault()
;
            if (article == null || user == null)
            {
                throw new InvalidArgumentException();
            }


            ArticleRate articleRateAdded = _mapper.Map<ArticleRate>(item);
            articleRateAdded.ArticleId = article.Id;
            articleRateAdded.UserId = user.Id;
            articleRateAdded.Date = DateTime.Now;
            articleRateAdded.Article = article;

            await _unitOfWork.ArticleRateRepo.AddAsync(articleRateAdded);

            ArticleRateVievModel articleRateViev = _mapper.Map<ArticleRateVievModel>(articleRateAdded);
            articleRateViev.ArticleName = article.Title;
            articleRateViev.UserLogin = user.UserName;
            return articleRateViev;
        }

        public async Task DeleteAsync(int id)
        {
            if (await _unitOfWork.ArticleRateRepo.GetByIdWithDetaileAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.ArticleRateRepo.DeleteById(id);
        }

        public async Task<IEnumerable<ArticleRateVievModel>> GetAllWithDetailsAsync()
        {
            var dbArticleRates = await _unitOfWork.ArticleRateRepo.GetAllWithDetailsAsync();
            List<ArticleRateVievModel> articelVievs = new List<ArticleRateVievModel>();
            foreach (var item in dbArticleRates)
            {
                ArticleRateVievModel articelViev = _mapper.Map<ArticleRateVievModel>(item);
                articelViev.ArticleName = _unitOfWork.ArticleRepo
                    .GetAll()
                    .Where(a => a.Id == item.ArticleId)
                    .Select(a => a.Title)
                    .FirstOrDefault();
                var dbuser = await _userService
                    .GetUsersAsync();
                articelViev.UserLogin = dbuser
                    .Where(u => u.Id == item.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefault();
                articelVievs.Add(articelViev);
            }
            return articelVievs;
        }

    }
}

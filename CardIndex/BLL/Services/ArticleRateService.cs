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
using Administration.Account;
using BLL.AddModels;
using BLL.VievModels;

namespace BLL.Services
{
    public class ArticleRateService : IBaseService<ArticleRateAddModel, ArticleRateVievModel>
    {
        private readonly IUserService _userServiceAdm;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleRateService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userServiceAdm)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userServiceAdm = userServiceAdm;
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
                .GetAllWithDetails();

            var article = articlebuff.Where(a => a.Title == item.ArticleName)
                .FirstOrDefault();

            var dbuser = await _userServiceAdm
                .GetAllUsers();

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

            await _unitOfWork.ArticleRateRepo.AddAsync(articleRateAdded);
            _unitOfWork.SaveChanges();

            ArticleRateVievModel articleRateViev = _mapper.Map<ArticleRateVievModel>(articleRateAdded);
            articleRateViev.ArticleName = article.Title;
            articleRateViev.UserLogin = user.UserName;
            return articleRateViev;
        }

        public async Task Delete(int id)
        {
            if (await _unitOfWork.ArticleRateRepo.GetByIdWithDetaileAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.ArticleRateRepo.DeleteById(id);
        }

        public async Task<IEnumerable<ArticleRateVievModel>> GetAllWithDetails()
        {
            var dbArticleRates = await _unitOfWork.ArticleRateRepo.GetAllWithDetails();
            List<ArticleRateVievModel> articelVievs = new List<ArticleRateVievModel>();
            foreach (var item in dbArticleRates)
            {
                ArticleRateVievModel articelViev = _mapper.Map<ArticleRateVievModel>(item);
                articelViev.ArticleName = _unitOfWork.ArticleRepo
                    .GetAll()
                    .Where(a => a.Id == item.ArticleId)
                    .Select(a => a.Title)
                    .FirstOrDefault();
                var dbuser = await _userServiceAdm
                    .GetAllUsers();
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

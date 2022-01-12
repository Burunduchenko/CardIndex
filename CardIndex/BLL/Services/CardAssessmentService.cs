
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
    public class CardAssessmentService : IBaseService<CardAssessmentAddModel, CardAssementVievModel>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CardAssessmentService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<CardAssementVievModel> AddAsync(CardAssessmentAddModel item)
        {
            char.ToUpper(item.CardTitle[0]);

            if (item.Rate < 0)
            {
                item.Rate = 0;
            }
            else if (item.Rate > 10)
            {
                item.Rate = 10;
            }
            var articlebuff = await _unitOfWork.CardRepository
                .GetAllWithDetailsAsync();

            var article = articlebuff.Where(a => a.Title == item.CardTitle)
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


            CardAssessment articleRateAdded = _mapper.Map<CardAssessment>(item);
            articleRateAdded.CardId = article.Id;
            articleRateAdded.UserId = user.Id;
            articleRateAdded.Date = DateTime.Now;
            articleRateAdded.Card = article;

            await _unitOfWork.CardAssessmentRepository.AddAsync(articleRateAdded);

            CardAssementVievModel articleRateViev = _mapper.Map<CardAssementVievModel>(articleRateAdded);
            articleRateViev.CardTitle = article.Title;
            articleRateViev.UserLogin = user.UserName;
            return articleRateViev;
        }

        public async Task DeleteAsync(int id)
        {
            if (await _unitOfWork.CardAssessmentRepository.GetByIdWithDetaileAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.CardAssessmentRepository.DeleteById(id);
        }

        public async Task<IEnumerable<CardAssementVievModel>> GetAllWithDetailsAsync()
        {
            var dbArticleRates = await _unitOfWork.CardAssessmentRepository.GetAllWithDetailsAsync();
            List<CardAssementVievModel> articelVievs = new List<CardAssementVievModel>();
            foreach (var item in dbArticleRates)
            {
                CardAssementVievModel articelViev = _mapper.Map<CardAssementVievModel>(item);
                articelViev.CardTitle = _unitOfWork.CardRepository
                    .GetAll()
                    .Where(a => a.Id == item.CardId)
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

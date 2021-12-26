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
    public class ArticleRateService : IBaseService<ArticleRateModel>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleRateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ArticleRateModel> AddAsync(ArticleRateModel item)
        {
            if (item.Rate < 0)
            {
                item.Rate = 0;
            }
            else if (item.Rate > 10)
            {
                item.Rate = 10;
            }

            if (
                await _unitOfWork.ArticleRepo.GetByIdAsync(item.ArticleId) == null ||
                await _unitOfWork.UserRepo.GetByIdAsync(item.UserId) == null)
            {
                throw new InvalidArgumentException();
            }


            item.Date = DateTime.Now;

            await _unitOfWork.ArticleRateRepo.AddAsync(_mapper.Map<ArticleRate>(item));
            _unitOfWork.SaveChanges();
            return item;
        }

        public async Task Delete(int id)
        {
            if (await _unitOfWork.ArticleRateRepo.GetByIdWithDetaileAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.ArticleRateRepo.DeleteById(id);
        }

        public IEnumerable<ArticleRateModel> GetAllWithDetails()
        {
            return _unitOfWork.ArticleRateRepo.GetAllWithDetails().Select(x => _mapper.Map<ArticleRateModel>(x));
        }


    }
}

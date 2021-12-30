using AutoMapper;
using BLL.AddModels;
using BLL.Exceptions;
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
    public class ThemeService : IBaseService<ThemeAddModel, ThemeVievModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ThemeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ThemeVievModel> AddAsync(ThemeAddModel item)
        {
            if (_unitOfWork.ThemeRepo.GetAll().Select(x => x.Name == item.Name).FirstOrDefault())
            {
                throw new AlreadyExistException();
            }

            await _unitOfWork.ThemeRepo.AddAsync(_mapper.Map<Theme>(item));
            _unitOfWork.SaveChanges();

            return new ThemeVievModel() { Name = item.Name};
        }

        public async Task Delete(int id)
        {
            if (await _unitOfWork.ThemeRepo.GetByIdAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.ThemeRepo.DeleteById(id);
        }

        public async Task<IEnumerable<ThemeVievModel>> GetAllWithDetails()
        {
            var dbthemes = await _unitOfWork.ThemeRepo.GetAllWithDetails();
            List<ThemeVievModel> result = new List<ThemeVievModel>();
            foreach (var item in dbthemes)
            {
                result.Add(_mapper.Map<ThemeVievModel>(item));
            }
            return result;
        }

    }
}

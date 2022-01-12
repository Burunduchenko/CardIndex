using Administration.Exceptions;
using AutoMapper;
using BLL.AddModels;
using BLL.Interfaces;
using BLL.VievModels;
using DAL.Entities;
using DAL.Interfaces;
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
            char.ToUpper(item.Name[0]);
            if (_unitOfWork.ThemeRepository.GetAll().Select(x => x.Name == item.Name).FirstOrDefault())
            {
                throw new AlreadyExistException();
            }

            await _unitOfWork.ThemeRepository.AddAsync(_mapper.Map<Theme>(item));
            _unitOfWork.SaveChanges();

            return new ThemeVievModel() { Name = item.Name };
        }

        public async Task DeleteAsync(int id)
        {
            if (await _unitOfWork.ThemeRepository.GetByIdAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.ThemeRepository.DeleteById(id);
        }

        public async Task<IEnumerable<ThemeVievModel>> GetAllWithDetailsAsync()
        {
            var dbthemes = await _unitOfWork.ThemeRepository.GetAllWithDetailsAsync();
            List<ThemeVievModel> result = new List<ThemeVievModel>();
            foreach (var item in dbthemes)
            {
                result.Add(_mapper.Map<ThemeVievModel>(item));
            }
            return result;
        }

    }
}

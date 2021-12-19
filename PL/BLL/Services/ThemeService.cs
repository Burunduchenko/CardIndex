using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ThemeService : IService<ThemeModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ThemeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ThemeModel> AddAsync(ThemeModel item)
        {
            if(_unitOfWork.ThemeRepo.GetAll().Select(x => x.Name == item.Name) != null)
            {
                throw new AlreadyExistException();
            }

            await _unitOfWork.ThemeRepo.AddAsync(_mapper.Map<Theme>(item));
            _unitOfWork.SaveChanges();
            return item;
        }

        public async void Delete(int id)
        {
            if (await _unitOfWork.ThemeRepo.GetByIdAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.ThemeRepo.DeleteById(id);
        }

        public IEnumerable<ThemeModel> GetAll()
        {
            return _unitOfWork.ThemeRepo.GetAll().Select(x => _mapper.Map<ThemeModel>(x));
        }

        public IEnumerable<ThemeModel> GetAllWithDetails()
        {
            return _unitOfWork.ThemeRepo.GetAllWithDetails().Select(x => _mapper.Map<ThemeModel>(x));
        }

        public async Task<ThemeModel> GetByIdAsync(int id)
        {
            var result = _mapper.Map<ThemeModel>(await _unitOfWork.ThemeRepo.GetByIdAsync(id));
            if (result == null)
            {
                throw new Exception();
            }
            return result;
        }

        public async Task<ThemeModel> GetByIdWithDetailsAsync(int id)
        {
            var result = _mapper.Map<ThemeModel>(await _unitOfWork.ThemeRepo.GetByIdWithDetaileAsync(id));
            if (result == null)
            {
                throw new Exception();
            }
            return result;
        }

        public async Task<ThemeModel> Update(ThemeModel item)
        {
            if (_unitOfWork.ThemeRepo.GetAll().Select(x => x.Name == item.Name) == null)
            {
                throw new NotFoundException();
            }

            var mappedItem = _mapper.Map<Theme>(item);
            var resultesItem = _mapper.Map<ThemeModel>(_unitOfWork.ThemeRepo.Update(mappedItem));
            return resultesItem;

        }
    }
}

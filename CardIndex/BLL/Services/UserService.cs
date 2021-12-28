using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IService<UserModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserModel> AddAsync(UserModel item)
        {
            if (
                   !int.TryParse(item.PhoneNumber, out _)
                || !Char.IsUpper(item.FirstName[0])
                || !Char.IsUpper(item.LastName[0])
                || item.Login.Length < 5
                )
            {
                throw new InvalidArgumentException();
            }

            if (_unitOfWork.UserRepo.GetAll().Where(x => x.Login == item.Login).FirstOrDefault() != null)
            {
                throw new AlreadyExistException();
            }

            await _unitOfWork.UserRepo.AddAsync(_mapper.Map<User>(item));
            _unitOfWork.SaveChanges();
            return item;
        }

        public async Task Delete(int id)
        {
            if (await _unitOfWork.UserRepo.GetByIdWithDetaileAsync(id) == null)
            {
                throw new NotFoundException();
            }
            _unitOfWork.UserRepo.DeleteById(id);
        }

        public IEnumerable<UserModel> GetAllWithDetails()
        {
            return _unitOfWork.UserRepo.GetAllWithDetails().Select(x => _mapper.Map<UserModel>(x));
        }


        public async Task<UserModel> GetByIdWithDetailsAsync(int id)
        {
            var result = _mapper.Map<UserModel>(await _unitOfWork.UserRepo.GetByIdWithDetaileAsync(id));
            if (result == null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public async Task<UserModel> Update(UserModel item)
        {
            if (
                   !int.TryParse(item.PhoneNumber, out _)
                || !Char.IsUpper(item.FirstName[0])
                || !Char.IsUpper(item.LastName[0])
                || item.Login.Length < 5
                )
            {
                throw new InvalidArgumentException();
            }

            if (_unitOfWork.UserRepo.GetAll().Where(x => x.Id == item.Id).FirstOrDefault() == null)
            {
                throw new NotFoundException();
            }

            var mappedItem = _mapper.Map<User>(item);
            var resultesItem = _mapper.Map<UserModel>(_unitOfWork.UserRepo.Update(mappedItem));
            return resultesItem;

        }
    }
}

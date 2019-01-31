using Isbak.KullaniciYonetimi.Dal;
using Isbak.KullaniciYonetimi.Dal.Contexts;
using Isbak.KullaniciYonetimi.Dal.Repositories;
using Isbak.KullaniciYonetimi.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isbak.KullaniciYonetimi.Services
{
    public class UserService
    {
        private EFContext _dbContext;
        private IUnitOfWork _uow;
        private IRepository<User> _userRepository;

        public UserService()
        {
            _dbContext = new EFContext();
            // EFBlogContext'i kullanıyor olduğumuz için EFUnitOfWork'den türeterek constructor'ına
            // ilgili context'i constructor injection yöntemi ile inject ediyoruz.
            _uow = new EFUnitOfWork(_dbContext);
            _userRepository = new EFRepository<User>(_dbContext);
        }

        public int UserCreate(User model)
        {
            _userRepository.Add(model);
            return _uow.SaveChanges();
        }
    }
}

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
    public class LoginService
    {
        private EFContext _dbContext;
        private IRepository<User> _userRepository;

        public LoginService()
        {
            _dbContext = new EFContext();
            // EFBlogContext'i kullanıyor olduğumuz için EFUnitOfWork'den türeterek constructor'ına
            // ilgili context'i constructor injection yöntemi ile inject ediyoruz.
            _userRepository = new EFRepository<User>(_dbContext);
        }
        public User LoginUser(User model)
        {
            return _userRepository.Get(x => x.Username == model.Username && x.Password == model.Password);
        }
    }
}

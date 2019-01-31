using Isbak.KullaniciYonetimi.Dal;
using Isbak.KullaniciYonetimi.Dal.Contexts;
using Isbak.KullaniciYonetimi.Dal.Repositories;
using Isbak.KullaniciYonetimi.Entities.Roles;
using Isbak.KullaniciYonetimi.Entities.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isbak.KullaniciYonetimi.Services
{
    public class RoleService
    {
        private EFContext _dbContext;
        private IRepository<Role> _roleRepository;
        private IRepository<User> _userRepository;
        private IUnitOfWork _uow;

        public RoleService()
        {
            _dbContext = new EFContext();
            // EFBlogContext'i kullanıyor olduğumuz için EFUnitOfWork'den türeterek constructor'ına
            // ilgili context'i constructor injection yöntemi ile inject ediyoruz.
            _roleRepository = new EFRepository<Role>(_dbContext);
            _userRepository = new EFRepository<User>(_dbContext);
            _uow = new EFUnitOfWork(_dbContext);
        }

        public string[] GetRoles(string[] roleName)
        {
            var roles = _roleRepository.GetAll();

            roleName = roles.Select(x => x.RoleName).ToArray();
            _uow.SaveChanges();
            return roleName;
        }
    }
}

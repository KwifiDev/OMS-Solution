using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class UserService : GenericService<User, UserDto>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IGenericRepository<User> genericRepo,
                           IMapperService mapper,
                           IUserRepository repository) : base(genericRepo, mapper)
        {
            _userRepository = repository;
        }

        public async Task<UserDto?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            User? user = await _userRepository.GetByUsernameAndPasswordAsync(username, password);

            return user == null ? null : _mapperService.Map<User, UserDto>(user);
        }
    }
}

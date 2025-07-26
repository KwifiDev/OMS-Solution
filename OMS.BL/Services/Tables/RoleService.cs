using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Tables;
using OMS.Common.Enums;
using OMS.DA.Entities;

namespace OMS.BL.Services.Tables
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapperService _mapper;

        public RoleService(RoleManager<Role> roleManager, IMapperService mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(await _roleManager.Roles.ToListAsync());
        }

        public async Task<RoleModel?> FindByIdAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null) return null;

            return _mapper.Map<Role, RoleModel>(role);
        }

        public async Task<RoleModel?> FindByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null) return null;

            return _mapper.Map<Role, RoleModel>(role);
        }

        public async Task<EnRoleResult> AddAsync(RoleModel model)
        {
            var role = _mapper.Map<RoleModel, Role>(model);

            var isExists = await _roleManager.RoleExistsAsync(role.Name!);

            if (!isExists) return EnRoleResult.RoleConflict;

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded) return EnRoleResult.Failed;

            model.Id = Convert.ToInt32(await _roleManager.GetRoleIdAsync(role));

            return EnRoleResult.Success;
        }

        public async Task<EnRoleResult> UpdateAsync(RoleModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id.ToString());

            if (role is null) return EnRoleResult.NotFound;

            var isExists = await _roleManager.RoleExistsAsync(model.Name!);

            if (!isExists) return EnRoleResult.RoleConflict;

            role.Name = model.Name;
            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded ? EnRoleResult.Success : EnRoleResult.Failed;
        }

        public async Task<EnRoleResult> DeleteAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role is null) return EnRoleResult.NotFound;

            var result = await _roleManager.DeleteAsync(role);

            return result.Succeeded ? EnRoleResult.Success : EnRoleResult.Failed;
        }

        public async Task<bool> IsExists(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            return (role is not null);
        }


    }
}

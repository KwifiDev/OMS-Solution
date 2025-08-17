using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using OMS.API.CustomAttributes;

namespace OMS.API.Extensions
{
    public class AuthorizeCrudConvetion : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            var crudAttr = action.Attributes.OfType<AuthorizeCrudAttribute>().FirstOrDefault();

            if (crudAttr != null)
            {
                var controllerName = action.Controller.ControllerName;
                var policy = $"{controllerName}.{crudAttr.CrudAction}";

                action.Filters.Add(new AuthorizeFilter(policy));
            }
        }
    }
}

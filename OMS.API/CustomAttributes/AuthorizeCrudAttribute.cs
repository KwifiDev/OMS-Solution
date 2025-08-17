using Microsoft.AspNetCore.Authorization;
using OMS.Common.Enums;

namespace OMS.API.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeCrudAttribute : AuthorizeAttribute
    {
        public EnCrudAction CrudAction {  get; }

        public AuthorizeCrudAttribute(EnCrudAction crudAction)
        {
            CrudAction = crudAction;
        }

    }
}

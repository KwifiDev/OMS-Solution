using System.Reflection;

namespace OMS.Common.Data
{
    public static class PermissionsData
    {

        public static IEnumerable<string> GetAll()
        {
            var permissions = new List<string>();
            var nestedTypes = typeof(PermissionsData).GetNestedTypes(BindingFlags.Public | BindingFlags.Static);

            foreach (var type in nestedTypes)
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                                .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string));

                foreach (var field in fields)
                {
                    var value = field.GetValue(null) as string;
                    if (!string.IsNullOrEmpty(value))
                    {
                        permissions.Add(value);
                    }
                }
            }

            return permissions.OrderBy(p => p).ToList();
        }

        public static class Dashboard
        {
            public const string View = "Dashboard.View";
        }

        public static class Revenues
        {
            public const string View = "Revenues.View";
            public const string Add = "Revenues.Add";
            public const string Edit = "Revenues.Edit";
            public const string Delete = "Revenues.Delete";
        }

        public static class People
        {
            public const string View = "People.View";
            public const string Add = "People.Add";
            public const string Edit = "People.Edit";
            public const string Delete = "People.Delete";
        }

        public static class PeopleDetail
        {
            public const string View = "PeopleDetail.View";
        }

        public static class Branches
        {
            public const string View = "Branches.View";
            public const string Add = "Branches.Add";
            public const string Edit = "Branches.Edit";
            public const string Delete = "Branches.Delete";
        }

        public static class BranchesOperationalMetric
        {
            public const string View = "BranchesOperationalMetric.View";
        }

        public static class Users
        {
            public const string View = "Users.View";
            public const string Add = "Users.Add";
            public const string Edit = "Users.Edit";
            public const string Delete = "Users.Delete";
            public const string ChangePassword = "Users.ChangePassword";
            public const string ManageRoles = "Users.ManageRoles";
            public const string Activation = "Users.Activation";
        }

        public static class Roles
        {
            public const string View = "Roles.View";
            public const string Add = "Roles.Add";
            public const string Edit = "Roles.Edit";
            public const string Delete = "Roles.Delete";
        }

        public static class RoleClaims
        {
            public const string View = "RoleClaims.View";
            public const string Add = "RoleClaims.Add";
            public const string Delete = "RoleClaims.Delete";
        }


        public static class Clients
        {
            public const string View = "Clients.View";
            public const string Add = "Clients.Add";
            public const string Edit = "Clients.Edit";
            public const string Delete = "Clients.Delete";
            public const string PayDebts = "Clients.PayDebts";
        }

        public static class ClientsSummary
        {
            public const string View = "ClientsSummary.View";
        }

        public static class Services
        {
            public const string View = "Services.View";
            public const string Add = "Services.Add";
            public const string Edit = "Services.Edit";
            public const string Delete = "Services.Delete";
        }

        public static class Accounts
        {
            public const string View = "Accounts.View";
            public const string Add = "Accounts.Add";
            public const string Edit = "Accounts.Edit";
            public const string Delete = "Accounts.Delete";
            public const string Transaction = "Accounts.Transaction";
        }

        public static class Debts
        {
            public const string View = "Debts.View";
            public const string Add = "Debts.Add";
            public const string Edit = "Debts.Edit";
            public const string Delete = "Debts.Delete";
            public const string Pay = "Debts.Pay";
            public const string Cancel = "Debts.Cancel";

        }

        public static class DebtsSummary
        {
            public const string View = "DebtsSummary.View";
        }


        public static class RolesSummary
        {
            public const string View = "RolesSummary.View";
        }

        public static class Discounts
        {
            public const string View = "Discounts.View";
            public const string Add = "Discounts.Add";
            public const string Edit = "Discounts.Edit";
            public const string Delete = "Discounts.Delete";
        }

        public static class DiscountsApplied
        {
            public const string View = "DiscountsApplied.View";
        }

        public static class PaymentsSummary
        {
            public const string View = "PaymentsSummary.View";
        }

        public static class Sales
        {
            public const string View = "Sales.View";
            public const string Add = "Sales.Add";
            public const string Edit = "Sales.Edit";
            public const string Delete = "Sales.Delete";
            public const string Cancel = "Sales.Cancel";
        }

        public static class SalesSummary
        {
            public const string View = "SalesSummary.View";
        }

        public static class Transactions
        {
            public const string View = "Transactions.View";
        }

        public static class TransactionsSummary
        {
            public const string View = "TransactionsSummary.View";
        }

        public static class ServicesSummary
        {
            public const string View = "ServicesSummary.View";
        }

        public static class UsersAccount
        {
            public const string View = "UsersAccount.View";
        }

        public static class UsersDetail
        {
            public const string View = "UsersDetail.View";
        }

        public static class Permissions
        {
            public const string View = "Permissions.View";
            public const string Add = "Permissions.Add";
            public const string Edit = "Permissions.Edit";
            public const string Delete = "Permissions.Delete";
        }

    }
}

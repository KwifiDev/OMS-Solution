[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class TenantHeaderAttribute : Attribute
{
    public bool Required { get; set; } = true;
    public string Description { get; set; } = "Tenant Identifier Required";
}
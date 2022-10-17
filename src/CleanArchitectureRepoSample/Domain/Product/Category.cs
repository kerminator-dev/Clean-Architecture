using Domain.Common;

namespace Domain.Product
{
    public class Category : AuditableWithBaseEntity<long>
    {
        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public string DisplayOrder { get; set; } = String.Empty;
    }
}

using System.Xml;

namespace MyApi.Entity
{
    public class ProductEntity : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}

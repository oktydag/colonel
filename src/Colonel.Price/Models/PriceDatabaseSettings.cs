namespace Colonel.Price.Models
{
    public class PriceDatabaseSettings : IPriceDatabaseSettings
    {
        public string PriceCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

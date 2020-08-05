namespace Colonel.Price.Models
{
    public interface IPriceDatabaseSettings
    {
        string PriceCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

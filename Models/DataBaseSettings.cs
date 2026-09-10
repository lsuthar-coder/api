namespace Trial.Server.Models
{
    public class DataBaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string TBCollectionName { get; set; } = null!;
        public string COACollectionName { get; set; } = null!;
    }
}

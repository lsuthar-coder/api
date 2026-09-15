using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Trial.Server.Models;

namespace Trial.Server.Services;

public class COAService(IOptions<DataBaseSettings> DatabaseSetting)
{
    private readonly IMongoCollection<ChartOfAccount> _chartsOfAccountsCollection = new MongoClient(DatabaseSetting.Value.ConnectionString)
        .GetDatabase(DatabaseSetting.Value.DatabaseName)
        .GetCollection<ChartOfAccount>(DatabaseSetting.Value.COACollectionName);

    public async Task<List<ChartOfAccount>> GetAsync() =>
        await _chartsOfAccountsCollection.Find(_ => true).ToListAsync();

    public async Task<ChartOfAccount?> GetAsync(string id) =>
        await _chartsOfAccountsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(ChartOfAccount newCOA) =>
        await _chartsOfAccountsCollection.InsertOneAsync(newCOA);

}
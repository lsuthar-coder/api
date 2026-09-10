using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Trial.Server.Models;

namespace Trial.Server.Services;

public class TBService(IOptions<DataBaseSettings> DatabaseSetting)
{
    private readonly IMongoCollection<TrialBalance> _trialBalanceCollection = new MongoClient(DatabaseSetting.Value.ConnectionString)
        .GetDatabase(DatabaseSetting.Value.DatabaseName)
        .GetCollection<TrialBalance>(DatabaseSetting.Value.TBCollectionName);

    public async Task<List<TrialBalance>> GetAsync() =>
        await _trialBalanceCollection.Find(_ => true).ToListAsync();

    public async Task<TrialBalance?> GetAsync(string id) =>
        await _trialBalanceCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TrialBalance newTB) =>
        await _trialBalanceCollection.InsertOneAsync(newTB);

}
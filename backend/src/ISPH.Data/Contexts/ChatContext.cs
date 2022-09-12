using System.Threading.Tasks;
using StackExchange.Redis;

namespace ISPH.Data.Contexts;

public class ChatContext
{
    private readonly IDatabase _database;
    public ChatContext(IConnectionMultiplexer multiplexer) => _database = multiplexer.GetDatabase();
    public async Task AddAsync(int responseId, string connectionId) => await _database.StringSetAsync(connectionId, responseId);
    public async Task RemoveAsync(string connectionId) => await _database.KeyDeleteAsync(connectionId);
    public async Task<string?> GetAsync(string connectionId) => await _database.StringGetAsync(connectionId);
}
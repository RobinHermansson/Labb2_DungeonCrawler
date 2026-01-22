using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using MongoDB.Driver;

namespace DungeonCrawler.Infrastructure.Repositories.Mongo;

public class MongoEnemyRepository : MongoRepository<Enemy, Guid>, IEnemyRepository
{
    public MongoEnemyRepository(IMongoDatabase database) : base(database, "enemies")
    {

    }
}
using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DungeonCrawler.Infrastructure.Repositories.Mongo;

internal class MongoCharacterRepository : MongoRepository<Character, Guid>, ICharacterRepository
{
    public MongoCharacterRepository(IMongoDatabase database) : base(database, "characters")
    {

    }
}

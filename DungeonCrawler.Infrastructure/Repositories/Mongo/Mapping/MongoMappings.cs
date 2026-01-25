using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace DungeonCrawler.Infrastructure.Repositories.Mongo.Mapping;

public class MongoMappings
{
    private static bool _registered = false;

    public static void Register()
    {
        if (_registered) return;

        var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true),
            new CamelCaseElementNameConvention()
        };

        ConventionRegistry.Register("appConventions", pack, _ => true);
        BsonClassMap.RegisterClassMap<PlayerClass>(pc =>
        {
            pc.AutoMap();
            pc.MapIdMember(x => x.Id)
                .SetIdGenerator(null)
                .SetSerializer(new MongoDB.Bson.Serialization.Serializers.GuidSerializer(BsonType.String));
            pc.MapMember(x => x.Name).SetElementName("name");
            pc.MapMember(x => x.BaseHitPoints).SetElementName("baseHitpoints");
            pc.MapMember(x => x.BaseAttackDiceCount).SetElementName("baseAttackDiceCount");
            pc.MapMember(x => x.BaseDefenceDiceCount).SetElementName("baseDefenceDiceCount");
            pc.MapMember(x => x.BaseAttackModifier).SetElementName("baseAttackModifier");
            pc.MapMember(x => x.BaseDefenceModifier).SetElementName("baseDefenceModifier");
            pc.MapMember(x => x.BaseVisionRange).SetElementName("baseVisionRange");
        });

        // Message
        BsonClassMap.RegisterClassMap<Message>(m =>
        {
            m.AutoMap();
        });
        BsonClassMap.RegisterClassMap<MessageLog>(ml =>
        {
            ml.AutoMap();
        });
        //SaveGame
        BsonClassMap.RegisterClassMap<SaveGame>(lt =>
        {
            lt.AutoMap();
            lt.MapIdMember(lt => lt.Id)
                .SetIdGenerator(null)
                .SetSerializer(new MongoDB.Bson.Serialization.Serializers.GuidSerializer(BsonType.String));
            
            lt.MapMember(x => x.PlayerClassId)
                .SetSerializer(new MongoDB.Bson.Serialization.Serializers.NullableSerializer<Guid>(
                    new MongoDB.Bson.Serialization.Serializers.GuidSerializer(BsonType.String)));
        }); 
        //LevelTemplate
        BsonClassMap.RegisterClassMap<LevelTemplate>(lt =>
        {
            lt.AutoMap();
            lt.MapIdMember(lt => lt.Id)
            .SetIdGenerator(null)
            .SetSerializer(new MongoDB.Bson.Serialization.Serializers.GuidSerializer(BsonType.String));
        });

        // Base LevelElement mapping
        BsonClassMap.RegisterClassMap<LevelElement>(le =>
        {
            le.AutoMap();
            le.SetIsRootClass(true);
            le.MapIdMember(x => x.Id)
                .SetIdGenerator(null) // Don't auto-generate, use our Guid
                .SetSerializer(new MongoDB.Bson.Serialization.Serializers.GuidSerializer(BsonType.String)); // Store as string for readability
            le.UnmapMember(x => x.PositionChanged);
            le.UnmapMember(x => x.HasBeenSeenChanged);
            le.UnmapMember(x => x.VisibilityChanged);
        });

        BsonClassMap.RegisterClassMap<Position>(p =>
        {
            p.AutoMap();
            p.MapMember(x => x.XPos).SetElementName("xPos");
            p.MapMember(x => x.YPos).SetElementName("yPos");
        });

        // Character mapping
        BsonClassMap.RegisterClassMap<Character>(c =>
        {
            c.AutoMap();
            c.SetDiscriminator("Character");
            c.MapMember(x => x.Name).SetElementName("Name");
            c.UnmapMember(x => x.GameState); // Ignore the GameState, to not have circular references

        });
        // Specific character types
        BsonClassMap.RegisterClassMap<Player>(c =>
        {
            c.AutoMap();
            c.SetDiscriminator("Player");
            c.UnmapMember(x => x.Class);
        });

        BsonClassMap.RegisterClassMap<Enemy>(c =>
        {
            c.AutoMap();
            c.SetDiscriminator("Enemy");
        });

        // Other entities
        BsonClassMap.RegisterClassMap<Snake>(c =>
        {
            c.AutoMap();
            c.SetDiscriminator("Snake");
        });
        BsonClassMap.RegisterClassMap<Rat>(c =>
        {
            c.AutoMap();
            c.SetDiscriminator("Rat");
        });
        BsonClassMap.RegisterClassMap<Wall>(c =>
        {
            c.AutoMap();
            c.SetDiscriminator("Wall");
        });
        BsonClassMap.RegisterClassMap<EmptySpace>(c =>
        {
            c.AutoMap();
            c.SetDiscriminator("EmptySpace");
        });

        _registered = true;

    }
}

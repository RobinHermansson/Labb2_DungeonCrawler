// Services/SetupService.cs
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Infrastructure.Utilities;

namespace DungeonCrawler.App.Services;

public class InitialSetupService
{
    private readonly IPlayerClassRepository _playerClassRepository;
    private readonly ILevelTemplateRepository _levelTemplateRepository;
    
    public InitialSetupService(
        IPlayerClassRepository playerClassRepository,
        ILevelTemplateRepository levelTemplateRepository)
    {
        _playerClassRepository = playerClassRepository;
        _levelTemplateRepository = levelTemplateRepository;
    }
    
    public async Task InitializeDatabaseAsync()
    {
        var classSeeder = new PlayerClassSeeder(_playerClassRepository);
        await classSeeder.SeedDefaultClassesAsync();
        
        var levelTemplate = await _levelTemplateRepository.GetByLevelNumberAsync(1);
        if (levelTemplate == null)
        {
            var levelImporter = new LevelImporter(_levelTemplateRepository);
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, "Levels", "Level1.txt");
            
            if (File.Exists(path))
            {
                await levelImporter.ImportFromFileAsync(path, 1, "Level 1");
            }
            else
            {
                throw new FileNotFoundException(
                    $"Level file not found: {path}. Please ensure Level1.txt exists in the Levels folder.");
            }
        }
    }
}
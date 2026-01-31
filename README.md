# Labb2 - Console Dungeon Crawler!

For my second assignment in my .NET studies at we were tasked to create a Dungeon Crawler in the Console. At this point we had studied for about 2 months.

## The assignment/task

Create a roguelike dungeon crawler game built in C# that runs entirely in the console. Navigate through dark dungeons, battle enemies, and explore the dungeon rendered through ASCII characters.
<img src="GitImages/StartGame.jpg" alt="Start-game screen." >

### Key Features:

* **Level Generation**: Level(s) loaded from text files for easy creation and modification. Based on the text in the file, the walls `#`, player `@`, and enemies -- which currently are rats `r` and snakes `s` -- are created .
* **Class Hierarchy**: Clean inheritance structure from `LevelElement` → `Character` → `Player/Enemy`
* **Game Loop**: Continuously process player and enemy actions based on user input, hande movement, attacks, and combat mechanics accordingly.
* **Field of Vision**: Implements a visibility system where previously seen areas remain visible. Further improved by dynamically dimming Walls not within vision.
  
<img src="GitImages/VisionRange.jpg" alt="Showcasing the vision range in-game." >

*   **Enemy AI Behaviors**:
    -   Rats with random movement patterns
    -   Snakes that flee when the player is detected within the snakes own vision range
* **Turn-Based Combat**: -   Strategic dice-based combat system with attack and defense modifiers and the ability to flee combat.

<img src="GitImages/Combat.jpg" alt="Combat happening in the game." >

* **Movement Patterns**: The player moves based on input, rats move randomly, and snakes move away from the player if within a certain range.
* **Game State Management**: Full game loop with title screen, gameplay, and game over states
* **Event-based rendering**: Instead of redrawing the whole level each turn, the renderer subscribes to each level element’s `PositionChanged` and `VisibilityChanged` events and only redraws that element (and clears its previous cell) when those events fire.
* **Player classes**: Composition-based classes (e.g. Warrior, Mage) stored in MongoDB and applied to the player
* **Persistent storage & save system**: Game state is stored in MongoDB. You can save and load the game, choose from multiple save slots, and delete saves. Level templates and player classes are also persisted in MongoDB.

## Architecture Decisions

### Character Base Class

The `Character` class serves as a foundation for both the player and enemies, providing shared functionality:

-   Health points and combat stats
-   Position tracking
-   Movement and collision detection
-   Vision range implementation


### Efficient Distance Calculations

The game uses distance calculations to determine enemy behavior and vision range:
<img src="GitImages/DistanceCalc.jpg" alt="Pythagoras theorem for distance calculation." >

And to not do the distance calculation against all elements in the world, an optimization is done by only checking distance against *actually* nearby elements in the world.
<img src="GitImages/CheckSurrounding.jpg" alt="Apply the distance calculation." >

### Separate Renderers
The general game is rendered by a dedicated Renderer that subscribes to each level element’s `PositionChanged` and `VisibilityChanged` events and only redraws the affected element when something changes, instead of redrawing the whole level every turn. Combat has its own renderer and logic.

## Technical highlights 

- **Clean Architecture**: Solution is split into Domain (entities, interfaces, no dependencies), Infrastructure (MongoDB repositories, file import), and App (UI, game loop, services). Domain has no reference to Infrastructure or App.
- **Repository pattern**: Persistence is abstracted behind interfaces (`ILevelTemplateRepository`, `ISaveGameRepository`, `IPlayerClassRepository`, etc.) in the Domain layer; concrete MongoDB implementations live in Infrastructure.
- **Persistence**: MongoDB is used for level templates, player classes, and save games (multiple save slots). Async/await is used for all database operations.
- **Event-driven design**: Level elements raise `PositionChanged` and `VisibilityChanged` events; the renderer subscribes and redraws only what changed instead of redrawing the whole level each turn.
- **Composition**: Player classes (e.g. Warrior, Mage) are applied via composition and stored in MongoDB rather than hard-coded inheritance.
- **Tech stack**: C#, .NET 8, MongoDB Driver. Console-based UI with a structured game loop, menus, and pause/save flows.

## Future Improvements

-   Additional enemy types with more complex behaviors
-   Procedural level generation
-   Inventory system and collectible items
-   Dictionary storage for all elements for O(1) lookup of object positions

*Already implemented: event-based rendering (per-element redraw on `PositionChanged` / `VisibilityChanged`); composition-based player class system (e.g. Warrior, Mage) with classes stored in MongoDB.*

## Getting Started

### Prerequisites

-   .NET 6.0 or higher (or .NET 8.0 to match the project)
-   Windows, macOS, or Linux terminal with Unicode support
-   **MongoDB** — a running MongoDB instance (local or remote). The game uses MongoDB for level templates, player classes, and save games.

### How to run (after cloning the repo)

1. **Start MongoDB** on your system (e.g. start the MongoDB service or run `mongod`).
2. **Run the game** — open `Labb2_DungeonCrawler.sln` in Visual Studio and run (F5 or the Run button).

   If you prefer the terminal instead:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project Labb2_DungeonCrawler
   ```

The level file `Level1.txt` lives in `DungeonCrawler.Infrastructure\Data\Levels\Level1.txt` and is copied to the application output when you build, so no manual copy is needed. If Level 1 is not yet in the database, the game imports it from that file on first run.

## Controls

-   Arrow keys or WASD: Move character
-   Enter: Select/Confirm
-   Esc: Attempt to escape combat (On your turn)

## Acknowledgments

-   Built as part of `IT-Högskolan .NET-utvecklare` program
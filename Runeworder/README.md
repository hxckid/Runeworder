# Runeworder

A Unity mobile application for Diablo 2 players to track runes, discover runewords, and manage their collection. Available for Android devices.

## Features

### Rune Management
- **Rune Collection Tracking**: Mark which runes you have collected
- **Rune Details**: Long-press any rune to view its properties, bonuses, and crafting recipes
- **Visual Indicators**: Color-coded runes in runewords show which ones you have (green) and which you're missing

### Runeword Discovery
- **Comprehensive Database**: Access to all runewords from multiple Diablo 2 versions
- **Smart Filtering**: Filter runewords by:
  - Type (Weapons, Armors, Helms, Shields)
  - Weapon category (Swords, Axes, Spears, etc.)
  - Number of sockets (2-6)
  - Game version (Resurrected 2.6, 2.4, LoD 1.11, 1.10, Original)
  - Ladder/Non-Ladder status
  - Completion status (show only completed runewords)
- **Search Functionality**: Search runewords by name
- **Custom Search**: Advanced search with multiple filter combinations
- **Sorting Options**: Sort by name, runes, or required level

### Multi-Language Support
- **English** and **Russian** language support
- Automatic language detection based on system settings
- Full localization of all UI elements and content

### Game Version Support
- Diablo 2: Resurrected (Patch 2.6, 2.4)
- Diablo 2: Lord of Destruction (LoD 1.11, 1.10)
- Original Diablo 2 runewords

### User Experience
- **Progress Tracking**: See how many runes you have for each runeword
- **Persistent Data**: Your rune collection is saved automatically
- **Intuitive UI**: Clean, easy-to-navigate interface
- **Tooltips**: Detailed information about runes and runewords

## Requirements

- **Unity Version**: 6000.2.2f1 (Unity 6)
- **Platform**: Android
- **Minimum Android Version**: See Unity project settings

## Project Structure

```
Runeworder/
├── Assets/
│   ├── Scripts/          # Core application scripts
│   │   ├── AppManager.cs          # Main application manager
│   │   ├── RuneController.cs      # Rune interaction handling
│   │   ├── RunewordsController.cs # Runeword filtering and display
│   │   ├── UIController.cs        # UI management
│   │   └── SO/                    # ScriptableObject definitions
│   ├── ScriptableObjects/ # Game data (runes, runewords, gems)
│   ├── Scenes/            # Unity scenes
│   ├── Prefabs/           # UI prefabs
│   ├── Gfx/               # Graphics and sprites
│   └── Fonts/              # Custom fonts
├── ProjectSettings/        # Unity project configuration
└── Packages/              # Unity package dependencies
```

## Key Scripts

- **AppManager**: Manages application state, language switching, and user data persistence
- **RuneController**: Handles rune toggle interactions and long-press gestures for tooltips
- **RunewordsController**: Implements filtering, searching, and sorting logic for runewords
- **ScriptableObjects**: Data structures for runes, runewords, and gems

## Installation

1. Clone this repository
2. Open the project in Unity 6000.2.2f1 or compatible version
3. Ensure all packages are installed (Unity will download them automatically)
4. Open the main scene from `Assets/Scenes/`
5. Build for Android platform

## Building for Android

1. In Unity, go to **File > Build Settings**
2. Select **Android** as the platform
3. Configure Android build settings as needed
4. Click **Build** to create an APK

## Usage

1. **Mark Your Runes**: Tap runes in the runes tab to mark which ones you have collected
2. **Find Runewords**: Navigate to the runewords tab to see available runewords
3. **Filter & Search**: Use the filter buttons or search bar to find specific runewords
4. **View Details**: Long-press any rune to see its properties and crafting recipe
5. **Track Progress**: Green runes indicate you have them; default color means you're missing them

## Data Persistence

Your rune collection is automatically saved using Unity's `PlayerPrefs` system. The data persists between app sessions.

## Dependencies

- Unity UI (uGUI) 2.0.0
- TextMesh Pro
- Unity AI Assistant (for development)
- Unity AI Generators (for development)

## Development

### Adding New Runewords

Runewords are stored as ScriptableObjects in `Assets/ScriptableObjects/`. To add new runewords:

1. Create a new `Runeword_SO` asset
2. Fill in the runeword details (name, runes, type, level, etc.)
3. Add it to the appropriate `RunewordsDB_SO` database

### Adding New Runes

Runes are also ScriptableObjects. To add new runes:

1. Create a new `Rune_SO` asset
2. Configure the rune properties and bonuses
3. Add it to the `RunesDB_SO` database

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

For support, email: **sisyfeanlabor@gmail.com**

## License

This project is developed by **Sisyphean Labor Team**.

## Acknowledgments

- Built for the Diablo 2 community
- Special thanks to all beta testers and contributors

---

**Note**: This application is a fan-made tool and is not affiliated with or endorsed by Blizzard Entertainment or Activision Blizzard.


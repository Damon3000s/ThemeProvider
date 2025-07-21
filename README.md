# ThemeProvider

**A semantic color theming library for .NET applications**

ThemeProvider is a comprehensive theming system that uses semantic color specifications rather than arbitrary color names. It provides a unified approach to theming across different UI frameworks through intelligent color mapping and includes 44+ carefully crafted themes from popular color schemes.

## ✨ Key Features

- **🎨 44+ Beautiful Themes**: Carefully crafted themes including Catppuccin, Tokyo Night, Gruvbox, Nord, Dracula, and many more
- **🧠 Semantic Color System**: Define colors by purpose (primary, accent, warning) rather than appearance (blue, red, green)
- **🎯 Centralized Theme Registry**: Easy discovery and management of all available themes
- **🔌 Framework Integration**: Built-in support for Dear ImGui with extensible architecture for other frameworks
- **⚡ Intelligent Color Mapping**: Automatic priority-based color interpolation and extrapolation
- **♿ Accessibility-First**: WCAG contrast ratio calculations and accessibility level checking
- **🎛️ Advanced Color Science**: Perceptually uniform color space (OKLCh) for natural color operations

## 🚀 Quick Start

### Installation

```bash
dotnet add package ktsu.ThemeProvider
# For ImGui integration:
dotnet add package ktsu.ThemeProvider.ImGui
```

### Basic Usage

```csharp
using ktsu.ThemeProvider;
using static ktsu.ThemeProvider.ThemeRegistry;

// Discover available themes
var allThemes = AllThemes;
var darkThemes = DarkThemes;
var catppuccinThemes = GetThemesInFamily("Catppuccin");

// Find and create a specific theme
var themeInfo = FindTheme("Catppuccin Mocha");
var theme = themeInfo?.CreateInstance();

// Request semantic colors
var primaryColor = theme.GetColor(new SemanticColorRequest(SemanticMeaning.Primary, Priority.Medium));
var warningColor = theme.GetColor(new SemanticColorRequest(SemanticMeaning.Warning, Priority.High));
```

### Framework Integration (Dear ImGui)

```csharp
using ktsu.ThemeProvider.ImGui;

// Create theme and mapper
var theme = new Themes.Catppuccin.Mocha();
var imguiMapper = new ImGuiPaletteMapper();

// Get complete ImGui color palette
var imguiColors = imguiMapper.MapTheme(theme);

// Apply to ImGui (in your render loop)
var style = ImGui.GetStyle();
foreach ((ImGuiCol colorKey, Vector4 colorValue) in imguiColors)
{
    style.Colors[(int)colorKey] = colorValue;
}
```

## 🎨 Available Themes

### Theme Registry

The `ThemeRegistry` provides centralized access to all available themes with rich metadata:

```csharp
// Browse themes by family
foreach (string family in Families)
{
    var themesInFamily = GetThemesInFamily(family);
    Console.WriteLine($"{family}: {themesInFamily.Length} variants");
}

// Filter themes
var lightThemes = LightThemes;
var darkThemes = DarkThemes;

// Create theme instances
var allThemeInstances = CreateAllThemeInstances();
var gruvboxInstances = CreateThemeInstancesInFamily("Gruvbox");
```

### Supported Themes (44 total)

| **Family** | **Variants** | **Description** |
|------------|-------------|------------------|
| **Catppuccin** | 4 variants (Latte, Frappe, Macchiato, Mocha) | Warm pastel themes with excellent readability |
| **Tokyo Night** | 3 variants (Night, Storm, Day) | Clean themes inspired by Tokyo's neon nights |
| **Gruvbox** | 6 variants (Dark, Light × Hard, Medium, Soft) | Retro groove colors with warm backgrounds |
| **Everforest** | 6 variants (Dark, Light × Hard, Medium, Soft) | Green forest colors for comfortable viewing |
| **Nightfox** | 7 variants (Nightfox, Dayfox, Duskfox, etc.) | Fox-inspired vibrant themes |
| **Kanagawa** | 3 variants (Wave, Dragon, Lotus) | Japanese-inspired themes |
| **PaperColor** | 2 variants (Light, Dark) | Material Design inspired themes |
| **Single Variants** | | Nord, Dracula, VSCode, One Dark, Monokai, Nightfly |

## 🧠 Semantic Color System

### Core Concepts

Instead of hardcoding colors like "blue" or "red", ThemeProvider uses semantic specifications:

```csharp
// ❌ Traditional approach
var buttonColor = Color.Blue;
var errorColor = Color.Red;

// ✅ Semantic approach
var buttonColor = theme.GetColor(new SemanticColorRequest(SemanticMeaning.Primary, Priority.Medium));
var errorColor = theme.GetColor(new SemanticColorRequest(SemanticMeaning.Error, Priority.High));
```

### Semantic Meanings

- **Primary**: Main brand/accent colors
- **Alternate**: Secondary accent colors  
- **Neutral**: Background, borders, inactive elements
- **CallToAction**: Important buttons and highlights
- **Success/Warning/Error**: Status and feedback colors
- **Information/Caution**: Informational messaging

### Priority System

Priorities control color intensity and importance:

- **VeryLow → VeryHigh**: Automatically mapped to appropriate lightness values
- **Intelligent Interpolation**: Colors between defined values are interpolated
- **Theme-Aware Ordering**: Light themes use high-to-low priority mapping, dark themes use low-to-high

### Advanced Color Operations

```csharp
// Accessibility checking
float contrastRatio = ColorMath.GetContrastRatio(foreground, background);
var accessibilityLevel = ColorMath.GetAccessibilityLevel(foreground, background, isLargeText);

// Color space conversions
var oklchColor = rgbColor.ToOklch();
var adjustedColor = oklchColor.WithLightness(0.7f).ToRgb();
```

## 🔌 Framework Integration

### Built-in ImGui Support

```csharp
public class ImGuiPaletteMapper : IPaletteMapper<ImGuiCol, Vector4>
{
    public string FrameworkName => "Dear ImGui";
    
    public ImmutableDictionary<ImGuiCol, Vector4> MapTheme(ISemanticTheme theme)
    {
        // Systematic mapping of all ImGui colors using semantic specifications
    }
}
```

### Creating Custom Framework Mappers

```csharp
public class MyFrameworkMapper : IPaletteMapper<MyColorEnum, MyColorType>
{
    public string FrameworkName => "My UI Framework";
    
    public ImmutableDictionary<MyColorEnum, MyColorType> MapTheme(ISemanticTheme theme)
    {
        var requests = new Dictionary<MyColorEnum, SemanticColorRequest>
        {
            { MyColorEnum.Button, new(SemanticMeaning.Primary, Priority.Medium) },
            { MyColorEnum.Background, new(SemanticMeaning.Neutral, Priority.VeryLow) },
            // ... other mappings
        };

        var mappedColors = SemanticColorMapper.MapColors(requests.Values, theme);
        
        return requests.ToDictionary(
            kvp => kvp.Key,
            kvp => ConvertToMyColorType(mappedColors[kvp.Value])
        ).ToImmutableDictionary();
    }
}
```

## 📖 Examples

### Complete Demo Application

The [ThemeProviderDemo](ThemeProviderDemo/) project showcases all features:

- **Theme Browser**: Explore all 44 themes with metadata
- **Theme Overview**: Visual semantic color grid for any theme
- **Semantic Colors**: Interactive color request builder
- **ImGui Mapping**: Live demonstration of framework integration  
- **Accessibility**: WCAG contrast ratio testing
- **UI Preview**: Live preview of themed UI elements

### Theme Usage Patterns

```csharp
// Pattern 1: Direct theme usage
var theme = new Themes.Catppuccin.Mocha();
var primaryColor = theme.GetColor(new(SemanticMeaning.Primary, Priority.Medium));

// Pattern 2: Theme registry approach  
var themeInfo = FindTheme("Tokyo Night Storm");
var theme = themeInfo?.CreateInstance();

// Pattern 3: Bulk color mapping
var colorRequests = new[]
{
    new SemanticColorRequest(SemanticMeaning.Primary, Priority.Medium),
    new SemanticColorRequest(SemanticMeaning.Error, Priority.High),
    new SemanticColorRequest(SemanticMeaning.Success, Priority.High),
};
var mappedColors = SemanticColorMapper.MapColors(colorRequests, theme);
```

## 🏗️ Development

### Building

```bash
dotnet build
```

### Running the Demo

```bash
dotnet run --project ThemeProviderDemo
```

### Project Structure

```
ThemeProvider/
├── ThemeProvider/              # Core semantic color system
│   ├── Themes/                # All 44 theme implementations  
│   ├── SemanticColorMapper.cs # Color interpolation engine
│   ├── ThemeRegistry.cs       # Centralized theme discovery
│   └── ColorMath.cs          # Accessibility and color operations
├── ThemeProvider.ImGui/       # Dear ImGui integration
└── ThemeProviderDemo/         # Comprehensive demo application
```

### Design Principles [[memory:2677368]]

- **SOLID Architecture**: Single responsibility, dependency inversion
- **DRY**: Shared semantic specifications across frameworks
- **Semantic-First**: Colors defined by purpose, not appearance  
- **Accessibility**: WCAG compliance built-in
- **Extensibility**: Framework-agnostic core with pluggable mappers

## 📋 Requirements

- .NET 8.0 or later
- Dear ImGui integration requires [Hexa.NET.ImGui](https://github.com/HexaEngine/Hexa.NET.ImGui)

## 📄 License

Licensed under the MIT License. See [LICENSE.md](LICENSE.md) for details.

## 🤝 Contributing

Contributions are welcome! Please ensure:

1. **Semantic Consistency**: Follow semantic color principles
2. **Theme Quality**: New themes should be well-balanced and accessible
3. **Documentation**: Update documentation for new features
4. **Testing**: Include accessibility and contrast validation

---

**Made with ❤️ by [ktsu.dev](https://ktsu.dev)**

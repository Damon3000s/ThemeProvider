# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```bash
# Build the entire solution
dotnet build

# Build specific project
dotnet build ThemeProvider/ThemeProvider.csproj

# Run the demo application (requires .NET 9)
dotnet run --project ThemeProviderDemo
```

## Architecture Overview

ThemeProvider is a semantic color theming library that uses meaning-based color specifications (Primary, Error, Warning, etc.) combined with priority levels to generate consistent, accessible color palettes across UI frameworks.

### Core Projects

- **ThemeProvider** - Core library with semantic color system, 44 theme implementations, and color math
- **ThemeProvider.ImGui** - Dear ImGui framework integration using Hexa.NET.ImGui
- **ThemeProviderDemo** - Interactive demo using ktsu.ImGuiApp

### Key Abstractions

**ISemanticTheme** (`ThemeProvider/ISemanticTheme.cs`)
- Themes implement this interface, providing `SemanticMapping` (dictionary of SemanticMeaning -> colors) and `IsDarkTheme` flag
- Each theme defines static colors from its color palette and maps them to semantic meanings

**SemanticColorMapper** (`ThemeProvider/SemanticColorMapper.cs`)
- Maps `SemanticColorRequest` (meaning + priority) to actual colors
- Uses global lightness range calculation across all theme colors
- Interpolates/extrapolates colors in Oklab perceptual color space to achieve target lightness for each priority level
- Dark themes: higher priority = higher lightness; Light themes: higher priority = lower lightness

**IPaletteMapper<TColorKey, TColorValue>** (`ThemeProvider/IPaletteMapper.cs`)
- Framework-specific mappers convert semantic themes to UI framework color dictionaries
- `ImGuiPaletteMapper` maps all ImGuiCol enum values to semantic requests

**ThemeRegistry** (`ThemeProvider/ThemeRegistry.cs`)
- Central registration of all 44 themes with metadata (name, family, variant, dark/light)
- Factory functions for theme instantiation

### Adding New Themes

1. Create a new class in `ThemeProvider/Themes/{Family}/{ThemeName}.cs`
2. Implement `ISemanticTheme` with static colors and `SemanticMapping` dictionary
3. Register in `ThemeRegistry.AllThemes` with metadata

Theme pattern (see `ThemeProvider/Themes/Catppuccin/Mocha.cs`):
- Define static `PerceptualColor` fields from hex values using `PerceptualColor.FromRgb("#hex")`
- Map semantic meanings (Neutral, Primary, Alternate, Success, Warning, Error, etc.) to color collections
- Neutral typically gets multiple colors for lightness gradient; other meanings usually get single accent colors

### Color System

- **SemanticMeaning**: Purpose-based categories (Neutral, Primary, Alternate, Success, CallToAction, Information, Caution, Warning, Error, Failure, Debug)
- **Priority**: 7 levels from VeryLow to VeryHigh controlling lightness/intensity
- **PerceptualColor**: Wraps RGB with Oklab conversions for perceptually uniform operations

### Project SDK

Projects use `ktsu.Sdk` (custom SDK) which handles common configuration. Target framework is .NET 9 for the demo app.

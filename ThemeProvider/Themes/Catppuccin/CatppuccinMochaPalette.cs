// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using ktsu.ThemeProvider.Core;

/// <summary>
/// Provides the official Catppuccin Mocha color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public static class CatppuccinMochaPalette
{
	/// <summary>
	/// Represents a single color in the Catppuccin palette with its properties.
	/// </summary>
	public record struct PaletteColor(
		string Name,
		string HexValue,
		ColorProperties Properties)
	{
		/// <summary>
		/// Gets the hue component of this color in degrees (0-360).
		/// </summary>
		public readonly float Hue => Properties.OklabValue.ToPolar().H;
	}

	/// <summary>Base color - Catppuccin Mocha background</summary>
	public static readonly PaletteColor Base = new(
		"Base",
		"#1e1e2e",
		ColorProperties.FromRgb(RgbColor.FromHex("#1e1e2e"), default, weight: 0.3f, temperature: -0.1f, energy: 0.2f, formality: 0.8f, accessibilityPriority: 0.5f));

	/// <summary>Mantle color - Catppuccin Mocha secondary background</summary>
	public static readonly PaletteColor Mantle = new(
		"Mantle",
		"#181825",
		ColorProperties.FromRgb(RgbColor.FromHex("#181825"), default, weight: 0.2f, temperature: -0.1f, energy: 0.1f, formality: 0.8f, accessibilityPriority: 0.4f));

	/// <summary>Crust color - Catppuccin Mocha darkest background</summary>
	public static readonly PaletteColor Crust = new(
		"Crust",
		"#11111b",
		ColorProperties.FromRgb(RgbColor.FromHex("#11111b"), default, weight: 0.1f, temperature: -0.1f, energy: 0.0f, formality: 0.8f, accessibilityPriority: 0.3f));

	/// <summary>Surface0 color - Catppuccin Mocha primary surface</summary>
	public static readonly PaletteColor Surface0 = new(
		"Surface0",
		"#313244",
		ColorProperties.FromRgb(RgbColor.FromHex("#313244"), default, weight: 0.4f, temperature: -0.05f, energy: 0.3f, formality: 0.7f, accessibilityPriority: 0.6f));

	/// <summary>Surface1 color - Catppuccin Mocha elevated surface</summary>
	public static readonly PaletteColor Surface1 = new(
		"Surface1",
		"#45475a",
		ColorProperties.FromRgb(RgbColor.FromHex("#45475a"), default, weight: 0.5f, temperature: 0.0f, energy: 0.4f, formality: 0.6f, accessibilityPriority: 0.7f));

	/// <summary>Surface2 color - Catppuccin Mocha highest surface</summary>
	public static readonly PaletteColor Surface2 = new(
		"Surface2",
		"#585b70",
		ColorProperties.FromRgb(RgbColor.FromHex("#585b70"), default, weight: 0.6f, temperature: 0.05f, energy: 0.5f, formality: 0.5f, accessibilityPriority: 0.8f));

	/// <summary>Overlay0 color - Catppuccin Mocha primary overlay</summary>
	public static readonly PaletteColor Overlay0 = new(
		"Overlay0",
		"#6c7086",
		ColorProperties.FromRgb(RgbColor.FromHex("#6c7086"), default, weight: 0.6f, temperature: 0.1f, energy: 0.5f, formality: 0.4f, accessibilityPriority: 0.7f));

	/// <summary>Overlay1 color - Catppuccin Mocha medium overlay</summary>
	public static readonly PaletteColor Overlay1 = new(
		"Overlay1",
		"#7f849c",
		ColorProperties.FromRgb(RgbColor.FromHex("#7f849c"), default, weight: 0.7f, temperature: 0.1f, energy: 0.6f, formality: 0.3f, accessibilityPriority: 0.8f));

	/// <summary>Overlay2 color - Catppuccin Mocha highest overlay</summary>
	public static readonly PaletteColor Overlay2 = new(
		"Overlay2",
		"#9399b2",
		ColorProperties.FromRgb(RgbColor.FromHex("#9399b2"), default, weight: 0.8f, temperature: 0.1f, energy: 0.6f, formality: 0.2f, accessibilityPriority: 0.9f));

	/// <summary>Text color - Catppuccin Mocha primary text</summary>
	public static readonly PaletteColor Text = new(
		"Text",
		"#cdd6f4",
		ColorProperties.FromRgb(RgbColor.FromHex("#cdd6f4"), default, weight: 1.0f, temperature: 0.0f, energy: 0.7f, formality: 0.4f, accessibilityPriority: 1.0f));

	/// <summary>Subtext1 color - Catppuccin Mocha secondary text</summary>
	public static readonly PaletteColor Subtext1 = new(
		"Subtext1",
		"#bac2de",
		ColorProperties.FromRgb(RgbColor.FromHex("#bac2de"), default, weight: 0.8f, temperature: 0.0f, energy: 0.6f, formality: 0.5f, accessibilityPriority: 0.95f));

	/// <summary>Subtext0 color - Catppuccin Mocha tertiary text</summary>
	public static readonly PaletteColor Subtext0 = new(
		"Subtext0",
		"#a6adc8",
		ColorProperties.FromRgb(RgbColor.FromHex("#a6adc8"), default, weight: 0.7f, temperature: 0.0f, energy: 0.5f, formality: 0.6f, accessibilityPriority: 0.9f));

	/// <summary>Lavender color - Catppuccin Mocha purple accent</summary>
	public static readonly PaletteColor Lavender = new(
		"Lavender",
		"#b4befe",
		ColorProperties.FromRgb(RgbColor.FromHex("#b4befe"), default, weight: 0.8f, temperature: -0.3f, energy: 0.8f, formality: 0.3f, accessibilityPriority: 0.8f));

	/// <summary>Blue color - Catppuccin Mocha blue accent</summary>
	public static readonly PaletteColor Blue = new(
		"Blue",
		"#89b4fa",
		ColorProperties.FromRgb(RgbColor.FromHex("#89b4fa"), default, weight: 1.0f, temperature: -0.5f, energy: 0.7f, formality: 0.6f, accessibilityPriority: 1.0f));

	/// <summary>Sapphire color - Catppuccin Mocha sapphire blue accent</summary>
	public static readonly PaletteColor Sapphire = new(
		"Sapphire",
		"#74c7ec",
		ColorProperties.FromRgb(RgbColor.FromHex("#74c7ec"), default, weight: 0.9f, temperature: -0.4f, energy: 0.8f, formality: 0.4f, accessibilityPriority: 0.9f));

	/// <summary>Sky color - Catppuccin Mocha sky blue accent</summary>
	public static readonly PaletteColor Sky = new(
		"Sky",
		"#89dceb",
		ColorProperties.FromRgb(RgbColor.FromHex("#89dceb"), default, weight: 0.7f, temperature: -0.3f, energy: 0.9f, formality: 0.2f, accessibilityPriority: 0.9f));

	/// <summary>Teal color - Catppuccin Mocha teal accent</summary>
	public static readonly PaletteColor Teal = new(
		"Teal",
		"#94e2d5",
		ColorProperties.FromRgb(RgbColor.FromHex("#94e2d5"), default, weight: 0.7f, temperature: -0.2f, energy: 0.8f, formality: 0.3f, accessibilityPriority: 0.7f));

	/// <summary>Green color - Catppuccin Mocha green accent</summary>
	public static readonly PaletteColor Green = new(
		"Green",
		"#a6e3a1",
		ColorProperties.FromRgb(RgbColor.FromHex("#a6e3a1"), default, weight: 0.8f, temperature: 0.0f, energy: 0.7f, formality: 0.4f, accessibilityPriority: 1.0f));

	/// <summary>Yellow color - Catppuccin Mocha yellow accent</summary>
	public static readonly PaletteColor Yellow = new(
		"Yellow",
		"#f9e2af",
		ColorProperties.FromRgb(RgbColor.FromHex("#f9e2af"), default, weight: 0.9f, temperature: 0.8f, energy: 0.9f, formality: 0.2f, accessibilityPriority: 1.0f));

	/// <summary>Peach color - Catppuccin Mocha peach accent</summary>
	public static readonly PaletteColor Peach = new(
		"Peach",
		"#fab387",
		ColorProperties.FromRgb(RgbColor.FromHex("#fab387"), default, weight: 0.8f, temperature: 0.6f, energy: 0.8f, formality: 0.3f, accessibilityPriority: 0.8f));

	/// <summary>Maroon color - Catppuccin Mocha maroon accent</summary>
	public static readonly PaletteColor Maroon = new(
		"Maroon",
		"#eba0ac",
		ColorProperties.FromRgb(RgbColor.FromHex("#eba0ac"), default, weight: 0.9f, temperature: 0.8f, energy: 0.8f, formality: 0.4f, accessibilityPriority: 0.9f));

	/// <summary>Red color - Catppuccin Mocha red accent</summary>
	public static readonly PaletteColor Red = new(
		"Red",
		"#f38ba8",
		ColorProperties.FromRgb(RgbColor.FromHex("#f38ba8"), default, weight: 1.0f, temperature: 0.9f, energy: 0.9f, formality: 0.4f, accessibilityPriority: 1.0f));

	/// <summary>Mauve color - Catppuccin Mocha mauve accent</summary>
	public static readonly PaletteColor Mauve = new(
		"Mauve",
		"#cba6f7",
		ColorProperties.FromRgb(RgbColor.FromHex("#cba6f7"), default, weight: 0.8f, temperature: 0.0f, energy: 0.7f, formality: 0.5f, accessibilityPriority: 0.8f));

	/// <summary>Pink color - Catppuccin Mocha pink accent</summary>
	public static readonly PaletteColor Pink = new(
		"Pink",
		"#f5c2e7",
		ColorProperties.FromRgb(RgbColor.FromHex("#f5c2e7"), default, weight: 0.7f, temperature: 0.5f, energy: 0.8f, formality: 0.2f, accessibilityPriority: 0.7f));

	/// <summary>
	/// Gets all colors in the palette as an enumerable collection.
	/// </summary>
	public static IEnumerable<PaletteColor> AllColors =>
	[
		Base, Mantle, Crust,
		Surface0, Surface1, Surface2,
		Overlay0, Overlay1, Overlay2,
		Text, Subtext1, Subtext0,
		Lavender, Blue, Sapphire, Sky, Teal,
		Green, Yellow, Peach, Maroon, Red,
		Mauve, Pink
	];

	/// <summary>
	/// Gets a color by its name.
	/// </summary>
	/// <param name="name">The name of the color to retrieve.</param>
	/// <returns>The color if found, null otherwise.</returns>
	public static PaletteColor? GetColorByName(string name) =>
		AllColors.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}

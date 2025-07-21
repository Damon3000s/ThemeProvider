// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;
using ktsu.ThemeProvider.Core;

/// <summary>
/// Provides semantic mapping utilities for Catppuccin Mocha colors.
/// Maps official Catppuccin colors to semantic meanings used by the theme system.
/// </summary>
public static class CatppuccinMochaSemanticMapping
{
	/// <summary>
	/// Gets the semantic hue mappings for Catppuccin Mocha theme.
	/// Maps semantic meanings to appropriate Catppuccin colors based on design intent.
	/// </summary>
	public static Dictionary<SemanticMeaning, float> SemanticHues => new()
	{
		// Blue family for normal interactions (Sapphire/Blue)
		[SemanticMeaning.Normal] = CatppuccinMochaPalette.Sapphire.Hue,

		// Peach for emphasis and highlighting
		[SemanticMeaning.Emphasis] = CatppuccinMochaPalette.Peach.Hue,

		// Green for success states
		[SemanticMeaning.Success] = CatppuccinMochaPalette.Green.Hue,

		// Mauve/Purple for call-to-action (primary buttons)
		[SemanticMeaning.CallToAction] = CatppuccinMochaPalette.Mauve.Hue,

		// Sky blue for informational content
		[SemanticMeaning.Information] = CatppuccinMochaPalette.Sky.Hue,

		// Peach trending toward yellow for caution
		[SemanticMeaning.Caution] = CatppuccinMochaPalette.Peach.Hue,

		// Yellow for warnings
		[SemanticMeaning.Warning] = CatppuccinMochaPalette.Yellow.Hue,

		// Red for errors
		[SemanticMeaning.Error] = CatppuccinMochaPalette.Red.Hue,

		// Maroon (red-orange) for failures
		[SemanticMeaning.Failure] = CatppuccinMochaPalette.Maroon.Hue,

		// Lavender for debug/development content
		[SemanticMeaning.Debug] = CatppuccinMochaPalette.Lavender.Hue
	};

	/// <summary>
	/// Gets the primary color for a semantic meaning.
	/// </summary>
	/// <param name="meaning">The semantic meaning to get the color for.</param>
	/// <returns>The primary palette color for the semantic meaning.</returns>
	public static CatppuccinMochaPalette.PaletteColor GetPrimaryColorFor(SemanticMeaning meaning)
	{
		return meaning switch
		{
			SemanticMeaning.Normal => CatppuccinMochaPalette.Blue,
			SemanticMeaning.Emphasis => CatppuccinMochaPalette.Peach,
			SemanticMeaning.Success => CatppuccinMochaPalette.Green,
			SemanticMeaning.CallToAction => CatppuccinMochaPalette.Mauve,
			SemanticMeaning.Information => CatppuccinMochaPalette.Sky,
			SemanticMeaning.Caution => CatppuccinMochaPalette.Peach,
			SemanticMeaning.Warning => CatppuccinMochaPalette.Yellow,
			SemanticMeaning.Error => CatppuccinMochaPalette.Red,
			SemanticMeaning.Failure => CatppuccinMochaPalette.Maroon,
			SemanticMeaning.Debug => CatppuccinMochaPalette.Lavender,
			_ => CatppuccinMochaPalette.Blue // Default fallback
		};
	}

	/// <summary>
	/// Gets the background color for a specific semantic specification.
	/// </summary>
	/// <param name="spec">The semantic color specification.</param>
	/// <returns>The appropriate background color from the palette.</returns>
	public static CatppuccinMochaPalette.PaletteColor GetBackgroundColor(SemanticColorSpec spec)
	{
		return (spec.Importance, spec.IsPrimary) switch
		{
			(ImportanceLevel.Low, true) => CatppuccinMochaPalette.Base,
			(ImportanceLevel.Medium, false) => CatppuccinMochaPalette.Mantle,
			(ImportanceLevel.Low, false) => CatppuccinMochaPalette.Crust,
			_ => CatppuccinMochaPalette.Base
		};
	}

	/// <summary>
	/// Gets the surface color for a specific semantic specification.
	/// </summary>
	/// <param name="spec">The semantic color specification.</param>
	/// <returns>The appropriate surface color from the palette.</returns>
	public static CatppuccinMochaPalette.PaletteColor GetSurfaceColor(SemanticColorSpec spec)
	{
		return (spec.Importance, spec.IsPrimary) switch
		{
			(ImportanceLevel.Low, true) => CatppuccinMochaPalette.Surface0,
			(ImportanceLevel.Medium, true) => CatppuccinMochaPalette.Surface1,
			(ImportanceLevel.High, true) => CatppuccinMochaPalette.Surface2,
			(ImportanceLevel.Low, false) => CatppuccinMochaPalette.Overlay0,
			(ImportanceLevel.Medium, false) => CatppuccinMochaPalette.Overlay1,
			(ImportanceLevel.High, false) => CatppuccinMochaPalette.Overlay2,
			_ => CatppuccinMochaPalette.Surface0
		};
	}

	/// <summary>
	/// Gets the text color for a specific semantic specification.
	/// </summary>
	/// <param name="spec">The semantic color specification.</param>
	/// <returns>The appropriate text color from the palette.</returns>
	public static CatppuccinMochaPalette.PaletteColor GetTextColor(SemanticColorSpec spec)
	{
		// For semantic text colors, use the semantic meaning
		if (spec.Meaning != SemanticMeaning.Normal)
		{
			return GetPrimaryColorFor(spec.Meaning);
		}

		// For normal text, use hierarchy
		return spec.Importance switch
		{
			ImportanceLevel.Critical => CatppuccinMochaPalette.Text,
			ImportanceLevel.High => CatppuccinMochaPalette.Subtext1,
			ImportanceLevel.Medium => CatppuccinMochaPalette.Subtext0,
			_ => CatppuccinMochaPalette.Text
		};
	}

	/// <summary>
	/// Gets the widget/accent color for a specific semantic specification.
	/// </summary>
	/// <param name="spec">The semantic color specification.</param>
	/// <returns>The appropriate widget color from the palette.</returns>
	public static CatppuccinMochaPalette.PaletteColor GetWidgetColor(SemanticColorSpec spec) =>
		GetPrimaryColorFor(spec.Meaning);
}

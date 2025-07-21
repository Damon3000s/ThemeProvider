// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

/// <summary>
/// Provides the authentic Catppuccin Mocha theme specification.
/// Based on the official Catppuccin color palette: https://catppuccin.com/palette
///
/// Catppuccin is a community-driven pastel theme that aims to be the middle ground
/// between low and high contrast themes. Mocha is the darkest variant offering
/// a cozy feeling with color-rich accents.
/// </summary>
public static class CatppuccinMocha
{
	/// <summary>
	/// Creates the authentic Catppuccin Mocha theme definition with all 26 colors
	/// mapped to semantic roles with appropriate temperature, energy, and accessibility metadata.
	/// </summary>
	public static ThemeDefinition CreateTheme()
	{
		return ThemeDefinition.CreateBuilder("Catppuccin Mocha")
			.WithDescription("A soothing pastel theme for the high-spirited! The darkest Catppuccin flavor offering a cozy feeling with color-rich accents.")
			.WithAuthor("Catppuccin Organization")
			.SetDarkTheme(true)
			.WithMetadata("source", "https://catppuccin.com/palette")
			.WithMetadata("flavor", "Mocha")
			.WithMetadata("version", "1.0.0")
			.WithMetadata("license", "MIT")

			// Base colors - fundamental background hierarchy
			.WithColor(ColorRole.Crust, "#11111b",
				weight: 0.1f, temperature: -0.1f, energy: 0.0f, formality: 0.8f, accessibilityPriority: 0.3f)
			.WithColor(ColorRole.Mantle, "#181825",
				weight: 0.2f, temperature: -0.1f, energy: 0.1f, formality: 0.8f, accessibilityPriority: 0.4f)
			.WithColor(ColorRole.Base, "#1e1e2e",
				weight: 0.3f, temperature: -0.1f, energy: 0.2f, formality: 0.8f, accessibilityPriority: 0.5f)

			// Surface colors - UI element backgrounds with hierarchy
			.WithColor(ColorRole.Surface0, "#313244",
				weight: 0.4f, temperature: -0.05f, energy: 0.3f, formality: 0.7f, accessibilityPriority: 0.6f)
			.WithColor(ColorRole.Surface1, "#45475a",
				weight: 0.5f, temperature: 0.0f, energy: 0.4f, formality: 0.6f, accessibilityPriority: 0.7f)
			.WithColor(ColorRole.Surface2, "#585b70",
				weight: 0.6f, temperature: 0.05f, energy: 0.5f, formality: 0.5f, accessibilityPriority: 0.8f)

			// Overlay colors - subtle transparent/overlay elements
			.WithColor(ColorRole.Overlay0, "#6c7086",
				weight: 0.6f, temperature: 0.1f, energy: 0.5f, formality: 0.4f, accessibilityPriority: 0.7f)
			.WithColor(ColorRole.Overlay1, "#7f849c",
				weight: 0.7f, temperature: 0.1f, energy: 0.6f, formality: 0.3f, accessibilityPriority: 0.8f)
			.WithColor(ColorRole.Overlay2, "#9399b2",
				weight: 0.8f, temperature: 0.1f, energy: 0.6f, formality: 0.2f, accessibilityPriority: 0.9f)

			// Text colors - typography hierarchy
			.WithColor(ColorRole.Subtext0, "#a6adc8",
				weight: 0.7f, temperature: 0.0f, energy: 0.5f, formality: 0.6f, accessibilityPriority: 0.9f)
			.WithColor(ColorRole.Subtext1, "#bac2de",
				weight: 0.8f, temperature: 0.0f, energy: 0.6f, formality: 0.5f, accessibilityPriority: 0.95f)
			.WithColor(ColorRole.Text, "#cdd6f4",
				weight: 1.0f, temperature: 0.0f, energy: 0.7f, formality: 0.4f, accessibilityPriority: 1.0f)

			// Accent colors - vibrant semantic colors
			.WithColor(ColorRole.Violet, "#b4befe", // Lavender -> Violet
				weight: 0.8f, temperature: -0.3f, energy: 0.8f, formality: 0.3f, accessibilityPriority: 0.8f)
			.WithColor(ColorRole.Blue, "#89b4fa",
				weight: 0.9f, temperature: -0.5f, energy: 0.7f, formality: 0.6f, accessibilityPriority: 0.9f)
			.WithColor(ColorRole.Cyan, "#74c7ec", // Sapphire -> Cyan
				weight: 0.8f, temperature: -0.4f, energy: 0.8f, formality: 0.4f, accessibilityPriority: 0.8f)
			.WithColor(ColorRole.Sky, "#89dceb",
				weight: 0.7f, temperature: -0.3f, energy: 0.9f, formality: 0.2f, accessibilityPriority: 0.7f)
			.WithColor(ColorRole.Teal, "#94e2d5",
				weight: 0.7f, temperature: -0.2f, energy: 0.8f, formality: 0.3f, accessibilityPriority: 0.7f)
			.WithColor(ColorRole.Green, "#a6e3a1",
				weight: 0.8f, temperature: 0.0f, energy: 0.7f, formality: 0.4f, accessibilityPriority: 0.8f)
			.WithColor(ColorRole.Yellow, "#f9e2af",
				weight: 0.9f, temperature: 0.8f, energy: 0.9f, formality: 0.2f, accessibilityPriority: 0.9f)
			.WithColor(ColorRole.Amber, "#f9e2af", // Using yellow for amber in Catppuccin
				weight: 0.9f, temperature: 0.7f, energy: 0.9f, formality: 0.3f, accessibilityPriority: 0.9f)
			.WithColor(ColorRole.Orange, "#fab387", // Using peach for orange
				weight: 0.8f, temperature: 0.6f, energy: 0.8f, formality: 0.3f, accessibilityPriority: 0.8f)
			.WithColor(ColorRole.Red, "#f38ba8",
				weight: 1.0f, temperature: 0.9f, energy: 0.9f, formality: 0.4f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.Pink, "#f5c2e7",
				weight: 0.7f, temperature: 0.5f, energy: 0.8f, formality: 0.2f, accessibilityPriority: 0.7f)
			.WithColor(ColorRole.Purple, "#cba6f7", // Using mauve for purple
				weight: 0.8f, temperature: 0.0f, energy: 0.7f, formality: 0.5f, accessibilityPriority: 0.8f)

			// Semantic role mappings
			.WithColor(ColorRole.Primary, "#89b4fa", // Blue
				weight: 1.0f, temperature: -0.5f, energy: 0.7f, formality: 0.6f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.Secondary, "#cba6f7", // Mauve/Purple
				weight: 0.8f, temperature: 0.0f, energy: 0.7f, formality: 0.5f, accessibilityPriority: 0.8f)
			.WithColor(ColorRole.Success, "#a6e3a1", // Green
				weight: 0.8f, temperature: 0.0f, energy: 0.7f, formality: 0.4f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.Warning, "#f9e2af", // Yellow
				weight: 0.9f, temperature: 0.8f, energy: 0.9f, formality: 0.2f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.Error, "#f38ba8", // Red
				weight: 1.0f, temperature: 0.9f, energy: 0.9f, formality: 0.4f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.Info, "#89dceb", // Sky
				weight: 0.7f, temperature: -0.3f, energy: 0.9f, formality: 0.2f, accessibilityPriority: 0.9f)

			// Interactive elements
			.WithColor(ColorRole.Link, "#89b4fa", // Blue
				weight: 0.9f, temperature: -0.5f, energy: 0.7f, formality: 0.6f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.LinkHover, "#74c7ec", // Sapphire
				weight: 0.9f, temperature: -0.4f, energy: 0.8f, formality: 0.4f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.LinkActive, "#b4befe", // Lavender
				weight: 0.8f, temperature: -0.3f, energy: 0.8f, formality: 0.3f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.LinkVisited, "#cba6f7", // Mauve
				weight: 0.7f, temperature: 0.0f, energy: 0.7f, formality: 0.5f, accessibilityPriority: 0.8f)

			// Form and UI elements
			.WithColor(ColorRole.Button, "#89b4fa", // Blue
				weight: 0.9f, temperature: -0.5f, energy: 0.7f, formality: 0.6f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.ButtonHover, "#74c7ec", // Sapphire
				weight: 0.9f, temperature: -0.4f, energy: 0.8f, formality: 0.4f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.ButtonActive, "#b4befe", // Lavender
				weight: 0.8f, temperature: -0.3f, energy: 0.8f, formality: 0.3f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.ButtonDisabled, "#6c7086", // Overlay0
				weight: 0.3f, temperature: 0.1f, energy: 0.2f, formality: 0.8f, accessibilityPriority: 0.3f)

			.WithColor(ColorRole.Input, "#313244", // Surface0
				weight: 0.4f, temperature: -0.05f, energy: 0.3f, formality: 0.7f, accessibilityPriority: 0.8f)
			.WithColor(ColorRole.InputBorder, "#585b70", // Surface2
				weight: 0.6f, temperature: 0.05f, energy: 0.5f, formality: 0.5f, accessibilityPriority: 0.7f)
			.WithColor(ColorRole.InputFocus, "#89b4fa", // Blue
				weight: 0.9f, temperature: -0.5f, energy: 0.7f, formality: 0.6f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.InputError, "#f38ba8", // Red
				weight: 1.0f, temperature: 0.9f, energy: 0.9f, formality: 0.4f, accessibilityPriority: 1.0f)

			// Utility colors
			.WithColor(ColorRole.Border, "#45475a", // Surface1
				weight: 0.4f, temperature: 0.0f, energy: 0.3f, formality: 0.7f, accessibilityPriority: 0.6f)
			.WithColor(ColorRole.Divider, "#585b70", // Surface2
				weight: 0.3f, temperature: 0.05f, energy: 0.3f, formality: 0.8f, accessibilityPriority: 0.5f)
			.WithColor(ColorRole.Selection, "#89b4fa", // Blue with lower opacity conceptually
				weight: 0.7f, temperature: -0.5f, energy: 0.6f, formality: 0.5f, accessibilityPriority: 0.8f)
			.WithColor(ColorRole.Highlight, "#f9e2af", // Yellow
				weight: 0.8f, temperature: 0.8f, energy: 0.9f, formality: 0.2f, accessibilityPriority: 0.9f)
			.WithColor(ColorRole.Shadow, "#11111b", // Crust (darkest)
				weight: 0.1f, temperature: -0.1f, energy: 0.0f, formality: 0.9f, accessibilityPriority: 0.2f)
			.WithColor(ColorRole.Focus, "#89b4fa", // Blue
				weight: 1.0f, temperature: -0.5f, energy: 0.7f, formality: 0.6f, accessibilityPriority: 1.0f)
			.WithColor(ColorRole.Disabled, "#6c7086", // Overlay0
				weight: 0.2f, temperature: 0.1f, energy: 0.2f, formality: 0.8f, accessibilityPriority: 0.3f)

			.Build();
	}

	/// <summary>
	/// Gets a quick reference to commonly used colors from Catppuccin Mocha.
	/// </summary>
	public static class Colors
	{
		// Base colors
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public static readonly RgbColor Rosewater = RgbColor.FromHex("#f5e0dc");
		public static readonly RgbColor Flamingo = RgbColor.FromHex("#f2cdcd");
		public static readonly RgbColor Pink = RgbColor.FromHex("#f5c2e7");
		public static readonly RgbColor Mauve = RgbColor.FromHex("#cba6f7");
		public static readonly RgbColor Red = RgbColor.FromHex("#f38ba8");
		public static readonly RgbColor Maroon = RgbColor.FromHex("#eba0ac");
		public static readonly RgbColor Peach = RgbColor.FromHex("#fab387");
		public static readonly RgbColor Yellow = RgbColor.FromHex("#f9e2af");
		public static readonly RgbColor Green = RgbColor.FromHex("#a6e3a1");
		public static readonly RgbColor Teal = RgbColor.FromHex("#94e2d5");
		public static readonly RgbColor Sky = RgbColor.FromHex("#89dceb");
		public static readonly RgbColor Sapphire = RgbColor.FromHex("#74c7ec");
		public static readonly RgbColor Blue = RgbColor.FromHex("#89b4fa");
		public static readonly RgbColor Lavender = RgbColor.FromHex("#b4befe");
		public static readonly RgbColor Text = RgbColor.FromHex("#cdd6f4");
		public static readonly RgbColor Subtext1 = RgbColor.FromHex("#bac2de");
		public static readonly RgbColor Subtext0 = RgbColor.FromHex("#a6adc8");
		public static readonly RgbColor Overlay2 = RgbColor.FromHex("#9399b2");
		public static readonly RgbColor Overlay1 = RgbColor.FromHex("#7f849c");
		public static readonly RgbColor Overlay0 = RgbColor.FromHex("#6c7086");
		public static readonly RgbColor Surface2 = RgbColor.FromHex("#585b70");
		public static readonly RgbColor Surface1 = RgbColor.FromHex("#45475a");
		public static readonly RgbColor Surface0 = RgbColor.FromHex("#313244");
		public static readonly RgbColor Base = RgbColor.FromHex("#1e1e2e");
		public static readonly RgbColor Mantle = RgbColor.FromHex("#181825");
		public static readonly RgbColor Crust = RgbColor.FromHex("#11111b");
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}

	/// <summary>
	/// Validates that the current implementation matches the official Catppuccin Mocha specification.
	/// This can be used in tests to ensure authenticity is maintained.
	/// </summary>
	public static bool ValidateAuthenticity()
	{
		ThemeDefinition theme = CreateTheme();

		// Verify key colors match official specification
		Dictionary<ColorRole, string> expectedColors = new()
		{
			{ ColorRole.Text, "#cdd6f4" },
			{ ColorRole.Base, "#1e1e2e" },
			{ ColorRole.Blue, "#89b4fa" },
			{ ColorRole.Red, "#f38ba8" },
			{ ColorRole.Green, "#a6e3a1" },
			{ ColorRole.Yellow, "#f9e2af" },
			{ ColorRole.Purple, "#cba6f7" },
			{ ColorRole.Pink, "#f5c2e7" }
		};

		foreach ((ColorRole role, string expectedHex) in expectedColors)
		{
			if (!theme.TryGetColor(role, out ColorProperties color))
			{
				return false;
			}

			if (color.RgbValue.ToHex().Equals(expectedHex, StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}

			return false;
		}

		return true;
	}
}

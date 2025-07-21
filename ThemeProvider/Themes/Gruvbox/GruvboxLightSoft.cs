// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Gruvbox;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Gruvbox Light Soft color palette with official hex values.
/// Soft variant uses lower contrast backgrounds for reduced eye strain.
/// Based on the Gruvbox theme by morhetz.
/// </summary>
public class GruvboxLightSoft : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Light colors (backgrounds in light theme) - using soft for reduced contrast
	public static readonly PerceptualColor LightHard = PerceptualColor.FromRgb("#f9f5d7");
	public static readonly PerceptualColor Light0 = PerceptualColor.FromRgb("#fbf1c7");
	public static readonly PerceptualColor Light0Soft = PerceptualColor.FromRgb("#f2e5bc");
	public static readonly PerceptualColor Light1 = PerceptualColor.FromRgb("#ebdbb2");
	public static readonly PerceptualColor Light2 = PerceptualColor.FromRgb("#d5c4a1");
	public static readonly PerceptualColor Light3 = PerceptualColor.FromRgb("#bdae93");
	public static readonly PerceptualColor Light4 = PerceptualColor.FromRgb("#a89984");

	// Dark colors (foregrounds in light theme)
	public static readonly PerceptualColor Dark0Hard = PerceptualColor.FromRgb("#1d2021");
	public static readonly PerceptualColor Dark0 = PerceptualColor.FromRgb("#282828");
	public static readonly PerceptualColor Dark0Soft = PerceptualColor.FromRgb("#32302f");
	public static readonly PerceptualColor Dark1 = PerceptualColor.FromRgb("#3c3836");
	public static readonly PerceptualColor Dark2 = PerceptualColor.FromRgb("#504945");
	public static readonly PerceptualColor Dark3 = PerceptualColor.FromRgb("#665c54");
	public static readonly PerceptualColor Dark4 = PerceptualColor.FromRgb("#7c6f64");

	// Faded colors for light theme
	public static readonly PerceptualColor FadedRed = PerceptualColor.FromRgb("#cc241d");
	public static readonly PerceptualColor FadedGreen = PerceptualColor.FromRgb("#98971a");
	public static readonly PerceptualColor FadedYellow = PerceptualColor.FromRgb("#d79921");
	public static readonly PerceptualColor FadedBlue = PerceptualColor.FromRgb("#458588");
	public static readonly PerceptualColor FadedPurple = PerceptualColor.FromRgb("#b16286");
	public static readonly PerceptualColor FadedAqua = PerceptualColor.FromRgb("#689d6a");
	public static readonly PerceptualColor FadedOrange = PerceptualColor.FromRgb("#d65d0e");

	// Neutral colors
	public static readonly PerceptualColor Gray = PerceptualColor.FromRgb("#928374");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Dark0Hard,    // Darkest (for text in light theme)
		Light0Soft,   // Lightest (for backgrounds in light theme - reduced contrast)
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [FadedOrange],
		[SemanticMeaning.Alternate] = [FadedPurple],
		[SemanticMeaning.Success] = [FadedGreen],
		[SemanticMeaning.CallToAction] = [FadedGreen],
		[SemanticMeaning.Information] = [FadedAqua],
		[SemanticMeaning.Caution] = [FadedBlue],
		[SemanticMeaning.Warning] = [FadedYellow],
		[SemanticMeaning.Error] = [FadedRed],
		[SemanticMeaning.Failure] = [FadedRed],
		[SemanticMeaning.Debug] = [FadedPurple]
	};

	/// <summary>
	/// Gruvbox Light Soft is a light theme with reduced contrast
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Dayfox color palette with official hex values.
/// A soft light theme with warm accents.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Dayfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Light background colors
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#f6f2ee");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#efeae6");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#f6f2ee");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#f0ebe7");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#e9e4e0");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#e1dcd8");
	public static readonly PerceptualColor Sel0 = PerceptualColor.FromRgb("#e7ddd9");
	public static readonly PerceptualColor Sel1 = PerceptualColor.FromRgb("#d6ccc7");

	// Dark foreground colors
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#1d344f");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#24394f");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#3c5d6f");
	public static readonly PerceptualColor Fg3 = PerceptualColor.FromRgb("#6b7d86");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#7d7a68");

	// Accent colors for light theme
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#a5222f");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#955f20");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#986936");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#396847");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#2848a9");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#287980");
	public static readonly PerceptualColor Magenta = PerceptualColor.FromRgb("#7847bd");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#b9477c");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Darkest (for text in light theme)
		BgAlt,       // Lightest (for backgrounds)
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Orange],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Cyan],
		[SemanticMeaning.Information] = [Blue],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Orange],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Magenta]
	};

	/// <summary>
	/// Dayfox is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

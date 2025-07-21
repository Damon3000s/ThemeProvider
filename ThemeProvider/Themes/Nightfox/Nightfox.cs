// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Nightfox color palette with official hex values.
/// A soft dark theme with blue and orange accents.
/// Based on the Nightfox theme by EdenEast.
/// </summary>
public class Nightfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Background colors
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#192330");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#131a24");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#192330");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#212e3f");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#29394f");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#39506d");
	public static readonly PerceptualColor Sel0 = PerceptualColor.FromRgb("#2b3b51");
	public static readonly PerceptualColor Sel1 = PerceptualColor.FromRgb("#3c5372");

	// Foreground colors
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#d6d6d7");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#cdcecf");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#aeafb0");
	public static readonly PerceptualColor Fg3 = PerceptualColor.FromRgb("#71839b");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#738091");

	// Accent colors
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#c94f6d");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#f4a261");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#dbc074");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#81b29a");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#719cd6");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#63cdcf");
	public static readonly PerceptualColor Magenta = PerceptualColor.FromRgb("#9d79d6");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#d67ad2");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg1,         // Lightest
		BgAlt,       // Darkest
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
	/// Nightfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

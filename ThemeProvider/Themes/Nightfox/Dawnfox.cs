// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Dawnfox color palette with official hex values.
/// A dawn-inspired light variant with soft, warm morning tones.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Dawnfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Dawn-inspired light backgrounds
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#faf4ed");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#f4ede4");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#faf4ed");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#f2e9de");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#eae0d5");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#d7c9bd");
	public static readonly PerceptualColor Sel0 = PerceptualColor.FromRgb("#e9dfdb");
	public static readonly PerceptualColor Sel1 = PerceptualColor.FromRgb("#ddd1c7");

	// Morning foreground colors
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#575279");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#6e6a86");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#797593");
	public static readonly PerceptualColor Fg3 = PerceptualColor.FromRgb("#9893a5");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#a8a5b8");

	// Dawn accent colors - soft pastels
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#b4637a");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#ea9d34");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#d7827e");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#286983");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#56949f");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#d7827e");
	public static readonly PerceptualColor Magenta = PerceptualColor.FromRgb("#907aa9");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#d685af");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Darkest (for text in light theme)
		BgAlt,       // Lightest (for backgrounds)
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Pink],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Blue],
		[SemanticMeaning.Information] = [Blue],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Orange],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Magenta]
	};

	/// <summary>
	/// Dawnfox is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

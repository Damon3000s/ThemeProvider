// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Duskfox color palette with official hex values.
/// A muted dark theme with desaturated warm tones.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Duskfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Background colors
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#232136");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#1a1826");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#232136");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#2d2a45");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#373354");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#47407d");
	public static readonly PerceptualColor Sel0 = PerceptualColor.FromRgb("#2a2d3a");
	public static readonly PerceptualColor Sel1 = PerceptualColor.FromRgb("#3c3a52");

	// Foreground colors
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#e0def4");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#cdcbe0");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#aeafc7");
	public static readonly PerceptualColor Fg3 = PerceptualColor.FromRgb("#6e6a86");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#6e6a86");

	// Muted accent colors
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#eb6f92");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#ea9a97");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#f6c177");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#a3be8c");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#9ccfd8");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#9ccfd8");
	public static readonly PerceptualColor Magenta = PerceptualColor.FromRgb("#c4a7e7");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#f5c2e7");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Lightest
		BgAlt,       // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Magenta],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Cyan],
		[SemanticMeaning.Information] = [Blue],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Orange],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Pink]
	};

	/// <summary>
	/// Duskfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

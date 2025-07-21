// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.TokyoNight;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Tokyo Night Storm color palette with official hex values.
/// Storm variant uses slightly lighter backgrounds for reduced contrast.
/// Based on the Tokyo Night theme by enkia.
/// </summary>
public class TokyoNightStorm : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Background colors (Storm variant uses #24283b instead of #1a1b26)
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#24283b");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#1f2335");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#24283b");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#2d3348");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#414868");
	public static readonly PerceptualColor Sel0 = PerceptualColor.FromRgb("#2d3f76");
	public static readonly PerceptualColor Sel1 = PerceptualColor.FromRgb("#364a82");

	// Foreground colors
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#c0caf5");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#a9b1d6");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#9aa5ce");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#565f89");

	// Tokyo Night core colors
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#7aa2f7");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#bb9af7");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#7dcfff");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#9ece6a");
	public static readonly PerceptualColor Teal = PerceptualColor.FromRgb("#1abc9c");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#e0af68");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#ff9e64");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#f7768e");
	public static readonly PerceptualColor Magenta = PerceptualColor.FromRgb("#bb9af7");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Lightest
		BgAlt,       // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
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
	/// Tokyo Night Storm is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

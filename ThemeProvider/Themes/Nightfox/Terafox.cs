// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Terafox color palette with official hex values.
/// An earthy, terra-inspired variant with warm brown and green tones.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Terafox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Earth-toned background colors
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#152528");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#0f1c1e");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#152528");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#1d3337");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#254147");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#2f4f56");
	public static readonly PerceptualColor Sel0 = PerceptualColor.FromRgb("#293e40");
	public static readonly PerceptualColor Sel1 = PerceptualColor.FromRgb("#3a5458");

	// Natural foreground colors
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#fbebd3");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#f6e2c7");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#adbcbc");
	public static readonly PerceptualColor Fg3 = PerceptualColor.FromRgb("#7d8c8c");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#868d8d");

	// Terra accent colors
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#e85c51");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#ffa500");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#fdb292");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#7aa4a1");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#5a93aa");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#a1cdd8");
	public static readonly PerceptualColor Magenta = PerceptualColor.FromRgb("#ad5c7c");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#ff8080");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Lightest
		BgAlt,       // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Green],
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
	/// Terafox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

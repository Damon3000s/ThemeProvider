// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Carbonfox color palette with official hex values.
/// A minimalist dark variant inspired by carbon fiber and industrial design.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Carbonfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Carbon-inspired dark backgrounds
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#161616");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#0c0c0c");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#161616");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#21272a");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#2a3439");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#4b5563");
	public static readonly PerceptualColor Sel0 = PerceptualColor.FromRgb("#2a2a2a");
	public static readonly PerceptualColor Sel1 = PerceptualColor.FromRgb("#3d3d3d");

	// Industrial foreground colors
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#f2f4f8");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#b6b8bb");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#8b8d8f");
	public static readonly PerceptualColor Fg3 = PerceptualColor.FromRgb("#6f7d8c");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#6f7d8c");

	// Carbon accent colors - minimal and functional
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#ee5396");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#3ddbd9");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#08bdba");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#25be6a");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#78a9ff");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#33b1ff");
	public static readonly PerceptualColor Magenta = PerceptualColor.FromRgb("#be95ff");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#ff7eb6");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Lightest
		BgAlt,       // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Cyan],
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
	/// Carbonfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

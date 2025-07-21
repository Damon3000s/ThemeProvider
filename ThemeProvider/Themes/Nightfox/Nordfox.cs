// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Nordfox color palette with official hex values.
/// A Nord-inspired variant with cool blue tones and arctic aesthetics.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Nordfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Nord-inspired background colors
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#2e3440");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#232831");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#2e3440");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#3b4252");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#434c5e");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#4c566a");
	public static readonly PerceptualColor Sel0 = PerceptualColor.FromRgb("#3e4a5b");
	public static readonly PerceptualColor Sel1 = PerceptualColor.FromRgb("#4f6074");

	// Nord foreground colors
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#cdcecf");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#b6b8bb");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#81848a");
	public static readonly PerceptualColor Fg3 = PerceptualColor.FromRgb("#60728a");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#60728a");

	// Nord accent colors
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#bf616a");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#d08770");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#ebcb8b");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#a3be8c");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#81a1c1");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#88c0d0");
	public static readonly PerceptualColor Magenta = PerceptualColor.FromRgb("#b48ead");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#b48ead");

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
	/// Nordfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

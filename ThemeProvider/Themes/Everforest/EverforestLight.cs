// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Everforest Light color palette with official hex values.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestLight : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#fdf6e3");
	public static readonly PerceptualColor BgDim = PerceptualColor.FromRgb("#f3efda");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#fdf6e3");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#f4f0d9");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#efebd4");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#e6e2cc");
	public static readonly PerceptualColor Bg4 = PerceptualColor.FromRgb("#e0dcc7");
	public static readonly PerceptualColor Bg5 = PerceptualColor.FromRgb("#ddd8c0");
	public static readonly PerceptualColor Grey0 = PerceptualColor.FromRgb("#a6b0a0");
	public static readonly PerceptualColor Grey1 = PerceptualColor.FromRgb("#939f91");
	public static readonly PerceptualColor Grey2 = PerceptualColor.FromRgb("#829181");
	public static readonly PerceptualColor Fg = PerceptualColor.FromRgb("#5c6a72");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#f85552");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#f57d26");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#dfa000");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#8da101");
	public static readonly PerceptualColor Aqua = PerceptualColor.FromRgb("#35a77c");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#3a94c5");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#df69ba");
	public static readonly PerceptualColor StatuslineA = PerceptualColor.FromRgb("#8da101");
	public static readonly PerceptualColor StatuslineB = PerceptualColor.FromRgb("#5c6a72");
	public static readonly PerceptualColor StatuslineC = PerceptualColor.FromRgb("#fdf6e3");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg,          // Darkest (for text in light theme)
		BgDim,       // Lightest (for backgrounds in light theme)
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Green],
		[SemanticMeaning.Alternate] = [Orange],
		[SemanticMeaning.Success] = [Blue],
		[SemanticMeaning.CallToAction] = [Aqua],
		[SemanticMeaning.Information] = [Purple],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Red],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// Everforest Light is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

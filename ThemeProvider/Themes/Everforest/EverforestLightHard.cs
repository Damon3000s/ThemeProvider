// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Everforest Light Hard color palette with official hex values.
/// Hard variant uses higher contrast backgrounds (#fffbef) for better readability.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestLightHard : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#fffbef");
	public static readonly PerceptualColor BgDim = PerceptualColor.FromRgb("#f8f4e6");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#fffbef");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#f7f4e0");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#f0ecce");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#e9e5c5");
	public static readonly PerceptualColor Bg4 = PerceptualColor.FromRgb("#e1ddbb");
	public static readonly PerceptualColor Bg5 = PerceptualColor.FromRgb("#dad5b1");
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
	public static readonly PerceptualColor StatuslineC = PerceptualColor.FromRgb("#fffbef");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg,          // Darkest (for text in light theme)
		BgDim,       // Lightest (for backgrounds in light theme - high contrast)
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
	/// Everforest Light Hard is a light theme with high contrast
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

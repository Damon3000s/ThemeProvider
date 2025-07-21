// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Everforest Light Soft color palette with official hex values.
/// Soft variant uses lower contrast backgrounds (#fffae8) for reduced eye strain.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestLightSoft : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#fffae8");
	public static readonly PerceptualColor BgDim = PerceptualColor.FromRgb("#f0f0e2");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#fffae8");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#f5f2dc");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#efead4");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#e8e3ca");
	public static readonly PerceptualColor Bg4 = PerceptualColor.FromRgb("#e2dcc3");
	public static readonly PerceptualColor Bg5 = PerceptualColor.FromRgb("#dcd6bb");
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
	public static readonly PerceptualColor StatuslineC = PerceptualColor.FromRgb("#fffae8");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg,          // Darkest (for text in light theme)
		BgDim,       // Lightest (for backgrounds in light theme - reduced contrast)
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
	/// Everforest Light Soft is a light theme with reduced contrast for eye comfort
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

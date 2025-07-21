// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Everforest Dark Soft color palette with official hex values.
/// Soft variant uses lower contrast backgrounds (#333c43) for reduced eye strain.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestDarkSoft : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#333c43");
	public static readonly PerceptualColor BgDim = PerceptualColor.FromRgb("#293136");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#333c43");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#3a464c");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#434f55");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#4d5a60");
	public static readonly PerceptualColor Bg4 = PerceptualColor.FromRgb("#555f66");
	public static readonly PerceptualColor Bg5 = PerceptualColor.FromRgb("#5d6b66");
	public static readonly PerceptualColor Grey0 = PerceptualColor.FromRgb("#7a8478");
	public static readonly PerceptualColor Grey1 = PerceptualColor.FromRgb("#859289");
	public static readonly PerceptualColor Grey2 = PerceptualColor.FromRgb("#9da9a0");
	public static readonly PerceptualColor Fg = PerceptualColor.FromRgb("#d3c6aa");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#e67e80");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#e69875");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#dbbc7f");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#a7c080");
	public static readonly PerceptualColor Aqua = PerceptualColor.FromRgb("#83c092");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#7fbbb3");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#d699b6");
	public static readonly PerceptualColor StatuslineA = PerceptualColor.FromRgb("#a7c080");
	public static readonly PerceptualColor StatuslineB = PerceptualColor.FromRgb("#d3c6aa");
	public static readonly PerceptualColor StatuslineC = PerceptualColor.FromRgb("#333c43");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg,          // Lightest
		BgDim,       // Darkest
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
	/// Everforest Dark Soft is a dark theme with reduced contrast for eye comfort
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Everforest Dark color palette with official hex values.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestDark : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#2d353b");
	public static readonly PerceptualColor BgDim = PerceptualColor.FromRgb("#232a2e");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#2d353b");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#343f44");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#3d484d");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#475258");
	public static readonly PerceptualColor Bg4 = PerceptualColor.FromRgb("#4f585e");
	public static readonly PerceptualColor Bg5 = PerceptualColor.FromRgb("#56635f");
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
	public static readonly PerceptualColor StatuslineC = PerceptualColor.FromRgb("#2d353b");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg,          // Lightest
		StatuslineB,
		Grey2,
		Grey1,
		Grey0,
		Bg5,
		Bg4,
		Bg3,
		Bg2,
		Bg1,
		Bg0,
		BgDim,       // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Aqua],
		[SemanticMeaning.Caution] = [Orange],
		[SemanticMeaning.Warning] = [Yellow],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// Everforest Dark is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

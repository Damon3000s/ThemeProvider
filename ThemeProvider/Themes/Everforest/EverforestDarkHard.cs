// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Everforest Dark Hard color palette with official hex values.
/// Hard variant uses higher contrast backgrounds (#272e33).
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestDarkHard : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#272e33");
	public static readonly PerceptualColor BgDim = PerceptualColor.FromRgb("#1e2326");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#272e33");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#2e383c");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#374145");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#414b50");
	public static readonly PerceptualColor Bg4 = PerceptualColor.FromRgb("#495156");
	public static readonly PerceptualColor Bg5 = PerceptualColor.FromRgb("#4f5b58");
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
	public static readonly PerceptualColor StatuslineC = PerceptualColor.FromRgb("#272e33");

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
	/// Everforest Dark Hard is a dark theme with high contrast
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

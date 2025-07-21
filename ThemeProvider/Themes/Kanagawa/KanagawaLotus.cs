// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Kanagawa;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Kanagawa Lotus color palette with official hex values.
/// A light variant inspired by lotus flowers and zen gardens.
/// Based on the Kanagawa theme by rebelot.
/// </summary>
public class KanagawaLotus : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Lotus-inspired light backgrounds
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#f2ecbc");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#f7f4dd");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#f2ecbc");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#e7dba0");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#e4d794");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#d8ce9b");
	public static readonly PerceptualColor WaveBlue1 = PerceptualColor.FromRgb("#d7d3c0");
	public static readonly PerceptualColor WaveBlue2 = PerceptualColor.FromRgb("#d5cea3");

	// Lotus foreground colors - soft and natural
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#545464");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#43436c");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#6f6f9a");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#a6a69c");

	// Soft lotus palette - muted and serene
	public static readonly PerceptualColor SakuraPink = PerceptualColor.FromRgb("#b35b79");
	public static readonly PerceptualColor WaveRed = PerceptualColor.FromRgb("#cc5d73");
	public static readonly PerceptualColor SummerGreen = PerceptualColor.FromRgb("#6f894e");
	public static readonly PerceptualColor AutumnYellow = PerceptualColor.FromRgb("#77713f");
	public static readonly PerceptualColor CrystalBlue = PerceptualColor.FromRgb("#4d699b");
	public static readonly PerceptualColor SpringBlue = PerceptualColor.FromRgb("#5e857a");
	public static readonly PerceptualColor KatanaGray = PerceptualColor.FromRgb("#8a8a7a");
	public static readonly PerceptualColor IceBlue = PerceptualColor.FromRgb("#7e9fb8");
	public static readonly PerceptualColor BoatYellow1 = PerceptualColor.FromRgb("#836f4a");
	public static readonly PerceptualColor BoatYellow2 = PerceptualColor.FromRgb("#b98f56");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Darkest (for text in light theme)
		BgAlt,       // Lightest (for backgrounds)
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [CrystalBlue],
		[SemanticMeaning.Alternate] = [SakuraPink],
		[SemanticMeaning.Success] = [SummerGreen],
		[SemanticMeaning.CallToAction] = [SpringBlue],
		[SemanticMeaning.Information] = [IceBlue],
		[SemanticMeaning.Caution] = [AutumnYellow],
		[SemanticMeaning.Warning] = [BoatYellow2],
		[SemanticMeaning.Error] = [WaveRed],
		[SemanticMeaning.Failure] = [WaveRed],
		[SemanticMeaning.Debug] = [SakuraPink]
	};

	/// <summary>
	/// Kanagawa Lotus is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

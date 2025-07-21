// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Kanagawa;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Kanagawa Wave color palette with official hex values.
/// A dark theme inspired by Japanese paintings with warm, muted tones.
/// Based on the Kanagawa theme by rebelot.
/// </summary>
public class KanagawaWave : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Background colors - traditional Japanese palette
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#1f1f28");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#16161d");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#1f1f28");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#2a2a37");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#363646");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#54546d");
	public static readonly PerceptualColor WaveBlue1 = PerceptualColor.FromRgb("#223249");
	public static readonly PerceptualColor WaveBlue2 = PerceptualColor.FromRgb("#2d4f67");

	// Foreground colors
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#dcd7ba");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#c8c093");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#9caca8");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#727169");

	// Traditional color palette
	public static readonly PerceptualColor SakuraPink = PerceptualColor.FromRgb("#d27e99");
	public static readonly PerceptualColor WaveRed = PerceptualColor.FromRgb("#e82424");
	public static readonly PerceptualColor SummerGreen = PerceptualColor.FromRgb("#98bb6c");
	public static readonly PerceptualColor AutumnYellow = PerceptualColor.FromRgb("#e6c384");
	public static readonly PerceptualColor CrystalBlue = PerceptualColor.FromRgb("#7e9cd8");
	public static readonly PerceptualColor SpringBlue = PerceptualColor.FromRgb("#7fb4ca");
	public static readonly PerceptualColor KatanaGray = PerceptualColor.FromRgb("#717c7c");
	public static readonly PerceptualColor IceBlue = PerceptualColor.FromRgb("#a3d4d5");
	public static readonly PerceptualColor BoatYellow1 = PerceptualColor.FromRgb("#938056");
	public static readonly PerceptualColor BoatYellow2 = PerceptualColor.FromRgb("#c0a36e");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Lightest
		BgAlt,       // Darkest
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
	/// Kanagawa Wave is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Kanagawa;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the Kanagawa Dragon color palette with official hex values.
/// A darker, more intense variant inspired by Japanese dragons and ink paintings.
/// Based on the Kanagawa theme by rebelot.
/// </summary>
public class KanagawaDragon : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Dragon-inspired darker backgrounds
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#0d0c0c");
	public static readonly PerceptualColor BgAlt = PerceptualColor.FromRgb("#181616");
	public static readonly PerceptualColor Bg0 = PerceptualColor.FromRgb("#0d0c0c");
	public static readonly PerceptualColor Bg1 = PerceptualColor.FromRgb("#181616");
	public static readonly PerceptualColor Bg2 = PerceptualColor.FromRgb("#201d23");
	public static readonly PerceptualColor Bg3 = PerceptualColor.FromRgb("#282727");
	public static readonly PerceptualColor WaveBlue1 = PerceptualColor.FromRgb("#0a0a0a");
	public static readonly PerceptualColor WaveBlue2 = PerceptualColor.FromRgb("#12120f");

	// Dragon foreground colors - intense and sharp
	public static readonly PerceptualColor Fg0 = PerceptualColor.FromRgb("#c5c9c5");
	public static readonly PerceptualColor Fg1 = PerceptualColor.FromRgb("#b5b4b1");
	public static readonly PerceptualColor Fg2 = PerceptualColor.FromRgb("#9e9b93");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#625e5a");

	// Intense dragon palette
	public static readonly PerceptualColor SakuraPink = PerceptualColor.FromRgb("#c4746e");
	public static readonly PerceptualColor WaveRed = PerceptualColor.FromRgb("#c4746e");
	public static readonly PerceptualColor SummerGreen = PerceptualColor.FromRgb("#8a9a7b");
	public static readonly PerceptualColor AutumnYellow = PerceptualColor.FromRgb("#c4b28a");
	public static readonly PerceptualColor CrystalBlue = PerceptualColor.FromRgb("#8ba4b0");
	public static readonly PerceptualColor SpringBlue = PerceptualColor.FromRgb("#7fb4ca");
	public static readonly PerceptualColor KatanaGray = PerceptualColor.FromRgb("#8a8980");
	public static readonly PerceptualColor IceBlue = PerceptualColor.FromRgb("#9cabca");
	public static readonly PerceptualColor BoatYellow1 = PerceptualColor.FromRgb("#a69764");
	public static readonly PerceptualColor BoatYellow2 = PerceptualColor.FromRgb("#b6927b");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Fg0,         // Lightest
		Background,  // Darkest
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
	/// Kanagawa Dragon is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

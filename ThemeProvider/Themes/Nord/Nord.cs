// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nord;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the official Nord color palette with exact hex values and properties.
/// Based on the official specification: https://www.nordtheme.com/
/// </summary>
public class Nord : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Polar Night (Dark colors)
	public static readonly PerceptualColor Nord0 = PerceptualColor.FromRgb("#2e3440");
	public static readonly PerceptualColor Nord1 = PerceptualColor.FromRgb("#3b4252");
	public static readonly PerceptualColor Nord2 = PerceptualColor.FromRgb("#434c5e");
	public static readonly PerceptualColor Nord3 = PerceptualColor.FromRgb("#4c566a");

	// Snow Storm (Light colors)
	public static readonly PerceptualColor Nord4 = PerceptualColor.FromRgb("#d8dee9");
	public static readonly PerceptualColor Nord5 = PerceptualColor.FromRgb("#e5e9f0");
	public static readonly PerceptualColor Nord6 = PerceptualColor.FromRgb("#eceff4");

	// Frost (Blue colors)
	public static readonly PerceptualColor Nord7 = PerceptualColor.FromRgb("#8fbcbb");
	public static readonly PerceptualColor Nord8 = PerceptualColor.FromRgb("#88c0d0");
	public static readonly PerceptualColor Nord9 = PerceptualColor.FromRgb("#81a1c1");
	public static readonly PerceptualColor Nord10 = PerceptualColor.FromRgb("#5e81ac");

	// Aurora (Accent colors)
	public static readonly PerceptualColor Nord11 = PerceptualColor.FromRgb("#bf616a"); // Red
	public static readonly PerceptualColor Nord12 = PerceptualColor.FromRgb("#d08770"); // Orange
	public static readonly PerceptualColor Nord13 = PerceptualColor.FromRgb("#ebcb8b"); // Yellow
	public static readonly PerceptualColor Nord14 = PerceptualColor.FromRgb("#a3be8c"); // Green
	public static readonly PerceptualColor Nord15 = PerceptualColor.FromRgb("#b48ead"); // Purple

	public static Collection<PerceptualColor> Neutrals =>
	[
		Nord6, // Lightest
		Nord5,
		Nord4,
		Nord3,
		Nord2,
		Nord1,
		Nord0, // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Nord10], // Main blue
		[SemanticMeaning.Alternate] = [Nord15], // Purple
		[SemanticMeaning.Success] = [Nord14], // Green
		[SemanticMeaning.CallToAction] = [Nord14], // Green
		[SemanticMeaning.Information] = [Nord8], // Cyan
		[SemanticMeaning.Caution] = [Nord12], // Orange
		[SemanticMeaning.Warning] = [Nord13], // Yellow
		[SemanticMeaning.Error] = [Nord11], // Red
		[SemanticMeaning.Failure] = [Nord11], // Red
		[SemanticMeaning.Debug] = [Nord15] // Purple
	};

	/// <summary>
	/// Nord is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

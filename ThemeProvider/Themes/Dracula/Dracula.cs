// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Dracula;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the official Dracula color palette with exact hex values.
/// Based on the official Dracula theme specification.
/// </summary>
public class Dracula : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#282a36");
	public static readonly PerceptualColor CurrentLine = PerceptualColor.FromRgb("#44475a");
	public static readonly PerceptualColor Selection = PerceptualColor.FromRgb("#44475a");
	public static readonly PerceptualColor Foreground = PerceptualColor.FromRgb("#f8f8f2");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#6272a4");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#8be9fd");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#50fa7b");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#ffb86c");
	public static readonly PerceptualColor Pink = PerceptualColor.FromRgb("#ff79c6");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#bd93f9");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#ff5555");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#f1fa8c");

	public static Collection<PerceptualColor> Neutrals =>
	[
		Foreground,   // Lightest
		Background,   // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Purple],
		[SemanticMeaning.Alternate] = [Pink],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Cyan],
		[SemanticMeaning.Caution] = [Orange],
		[SemanticMeaning.Warning] = [Yellow],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// Dracula is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

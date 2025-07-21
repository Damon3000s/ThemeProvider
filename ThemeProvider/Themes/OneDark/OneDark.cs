// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.OneDark;

using System.Collections.ObjectModel;

/// <summary>
/// Provides the One Dark color palette with official hex values.
/// Based on the Atom One Dark theme.
/// </summary>
public class OneDark : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly PerceptualColor Background = PerceptualColor.FromRgb("#282c34");
	public static readonly PerceptualColor CursorLine = PerceptualColor.FromRgb("#2c323c");
	public static readonly PerceptualColor Selection = PerceptualColor.FromRgb("#3e4451");
	public static readonly PerceptualColor Foreground = PerceptualColor.FromRgb("#abb2bf");
	public static readonly PerceptualColor Comment = PerceptualColor.FromRgb("#5c6370");
	public static readonly PerceptualColor Red = PerceptualColor.FromRgb("#e06c75");
	public static readonly PerceptualColor Orange = PerceptualColor.FromRgb("#d19a66");
	public static readonly PerceptualColor Yellow = PerceptualColor.FromRgb("#e5c07b");
	public static readonly PerceptualColor Green = PerceptualColor.FromRgb("#98c379");
	public static readonly PerceptualColor Cyan = PerceptualColor.FromRgb("#56b6c2");
	public static readonly PerceptualColor Blue = PerceptualColor.FromRgb("#61afef");
	public static readonly PerceptualColor Purple = PerceptualColor.FromRgb("#c678dd");
	public static readonly PerceptualColor White = PerceptualColor.FromRgb("#ffffff");
	public static readonly PerceptualColor Black = PerceptualColor.FromRgb("#181a1f");
	public static readonly PerceptualColor VisualGray = PerceptualColor.FromRgb("#3e4451");
	public static readonly PerceptualColor SpecialGray = PerceptualColor.FromRgb("#3b4048");
	public static readonly PerceptualColor VertSplit = PerceptualColor.FromRgb("#181a1f");

	public static Collection<PerceptualColor> Neutrals =>
	[
		White,         // Lightest
		Black,         // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
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
	/// One Dark is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

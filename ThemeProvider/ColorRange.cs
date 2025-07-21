// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

/// <summary>
/// Represents a range of colors defined by start and end points in perceptual color space.
/// Used for interpolating colors between two extremes.
/// </summary>
/// <param name="Start">The starting color of the range</param>
/// <param name="End">The ending color of the range</param>
public readonly record struct ColorRange(PerceptualColor Start, PerceptualColor End)
{
	/// <summary>
	/// Gets the perceptual distance between the start and end colors.
	/// </summary>
	public float Distance => Start.SemanticDistanceTo(End);

	/// <summary>
	/// Indicates whether this range represents a single color (start equals end).
	/// </summary>
	public bool IsSingleColor => Distance < float.Epsilon;

	/// <summary>
	/// Creates a color range from two colors, automatically ordering them
	/// based on the theme type for appropriate interpolation.
	/// </summary>
	/// <param name="color1">First color</param>
	/// <param name="color2">Second color</param>
	/// <param name="isDarkTheme">Whether this is for a dark theme</param>
	/// <returns>A color range with appropriate start and end points</returns>
	public static ColorRange FromColors(PerceptualColor color1, PerceptualColor color2, bool isDarkTheme)
	{
		// For dark themes: low priority (start) should be darker, high priority (end) should be lighter
		// For light themes: low priority (start) should be lighter, high priority (end) should be darker
		if (isDarkTheme)
		{
			// Dark theme: darker to lighter (lower lightness to higher lightness)
			if (color1.Lightness <= color2.Lightness)
			{
				return new ColorRange(color1, color2);
			}
			else
			{
				return new ColorRange(color2, color1);
			}
		}
		else
		{
			// Light theme: lighter to darker (higher lightness to lower lightness)
			if (color1.Lightness >= color2.Lightness)
			{
				return new ColorRange(color1, color2);
			}
			else
			{
				return new ColorRange(color2, color1);
			}
		}
	}

	/// <summary>
	/// Creates a color range from two colors, automatically ordering them
	/// to create a meaningful interpolation range (default: dark theme behavior).
	/// </summary>
	/// <param name="color1">First color</param>
	/// <param name="color2">Second color</param>
	/// <returns>A color range with appropriate start and end points</returns>
	public static ColorRange FromColors(PerceptualColor color1, PerceptualColor color2) =>
		FromColors(color1, color2, isDarkTheme: true);
}

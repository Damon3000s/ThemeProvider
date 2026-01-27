// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

using System.Numerics;

/// <summary>
/// Represents the perceptual properties of a color.
/// This enables color space operations for semantic color mapping.
/// </summary>
public readonly record struct PerceptualColor //TODO: rename to PerceptualColor
{
	/// <summary>The color in Oklab perceptual space</summary>
	public OklabColor OklabValue { get; init; }

	/// <summary>The RGB representation of the color</summary>
	public RgbColor RgbValue { get; init; }

	/// <summary>
	/// Gets the hue component of the color in Oklab polar coordinates (LCh).
	/// </summary>
	public float Hue { get; init; }

	/// <summary>
	/// Gets the chroma (colorfulness) component of the color in Oklab polar coordinates (LCh).
	/// </summary>
	public float Chroma { get; init; }

	/// <summary>
	/// Gets the lightness component of the color in Oklab space.
	/// </summary>
	public float Lightness { get; init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="PerceptualColor"/> struct with default values.
	/// </summary>
	public PerceptualColor(RgbColor rgb)
	{
		OklabColor oklab = ColorMath.RgbToOklab(rgb);
		OklabValue = oklab;
		RgbValue = rgb;
		Hue = oklab.ToPolar().H;
		Chroma = oklab.ToPolar().C;
		Lightness = oklab.L;
	}

	/// <summary>
	/// Creates color properties from an RGB color.
	/// </summary>
	public static PerceptualColor FromRgb(RgbColor rgb) => new(rgb);

	/// <summary>
	/// Creates color properties from an RGB color.
	/// </summary>
	public static PerceptualColor FromRgb(string hex) => FromRgb(RgbColor.FromHex(hex));

	/// <summary>
	/// Converts the perceptual properties to a vector for color space operations.
	/// </summary>
	public Vector3 ToPerceptualVector() => OklabValue.ToVector3();

	/// <summary>
	/// Calculates perceptual distance to another color in Oklab space.
	/// </summary>
	public float SemanticDistanceTo(PerceptualColor other) =>
		// Perceptual distance
		OklabValue.DistanceTo(other.OklabValue);
}

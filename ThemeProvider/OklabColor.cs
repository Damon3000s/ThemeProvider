// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;
#pragma warning disable IDE0090

using System.Numerics;

/// <summary>
/// Represents a color in the Oklab perceptual color space designed for image processing
/// and color manipulation with better uniformity than CIELAB.
/// </summary>
public readonly record struct OklabColor(float L, float A, float B)
{
	/// <summary>
	/// Creates an Oklab color from individual components.
	/// </summary>
	/// <param name="l">Perceived lightness (0-1, typically)</param>
	/// <param name="a">Green-red axis (negative = green, positive = red/magenta)</param>
	/// <param name="b">Blue-yellow axis (negative = blue, positive = yellow)</param>
	public OklabColor(double l, double a, double b) : this((float)l, (float)a, (float)b) { }

	/// <summary>
	/// Converts this Oklab color to a 3D vector for mathematical operations.
	/// </summary>
	public Vector3 ToVector3() => new(L, A, B);

	/// <summary>
	/// Calculates the perceptual distance between two colors in Oklab space.
	/// This gives a good approximation of how different two colors appear to human vision.
	/// </summary>
	public float DistanceTo(OklabColor other)
	{
		float dl = L - other.L;
		float da = A - other.A;
		float db = B - other.B;
		return (float)Math.Sqrt((dl * dl) + (da * da) + (db * db));
	}

	/// <summary>
	/// Linearly interpolates between two colors in Oklab space.
	/// This produces more perceptually uniform gradients than RGB interpolation.
	/// </summary>
	public static OklabColor Lerp(OklabColor from, OklabColor to, float t)
	{
		float invT = 1.0f - t;
		return new OklabColor(
			(from.L * invT) + (to.L * t),
			(from.A * invT) + (to.A * t),
			(from.B * invT) + (to.B * t)
		);
	}

	/// <summary>
	/// Converts this Oklab color to polar coordinates (LCh - Lightness, Chroma, Hue).
	/// </summary>
	public (float L, float C, float H) ToPolar()
	{
		float c = (float)Math.Sqrt((A * A) + (B * B));
		float h = (float)(Math.Atan2(B, A) * 180f / Math.PI);
		if (h < 0)
		{
			h += 360f;
		}

		return (L, c, h);
	}

	/// <summary>
	/// Creates an Oklab color from polar coordinates (LCh).
	/// </summary>
	public static OklabColor FromPolar(float l, float c, float h)
	{
		float hRad = (float)(h * Math.PI / 180f);
		return new OklabColor(l, c * Math.Cos(hRad), c * Math.Sin(hRad));
	}
}

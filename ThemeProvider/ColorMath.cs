// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;
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
		return MathF.Sqrt((dl * dl) + (da * da) + (db * db));
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
		float c = MathF.Sqrt((A * A) + (B * B));
		float h = MathF.Atan2(B, A) * 180f / MathF.PI;
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
		float hRad = h * MathF.PI / 180f;
		return new OklabColor(l, c * MathF.Cos(hRad), c * MathF.Sin(hRad));
	}
}

/// <summary>
/// Represents an RGB color with floating-point precision.
/// </summary>
public readonly record struct RgbColor(float R, float G, float B)
{
	/// <summary>
	/// Creates an RGB color from 8-bit values (0-255).
	/// </summary>
	public static RgbColor FromBytes(byte r, byte g, byte b) =>
		new(r / 255f, g / 255f, b / 255f);

	/// <summary>
	/// Creates an RGB color from a hex string (e.g., "#FF0000" or "FF0000").
	/// </summary>
	public static RgbColor FromHex(string hex)
	{
		ArgumentNullException.ThrowIfNull(hex);
		if (hex.StartsWith('#'))
		{
			hex = hex[1..];
		}

		if (hex.Length != 6)
		{
			throw new ArgumentException("Invalid hex color format", nameof(hex));
		}

		return FromBytes(
			Convert.ToByte(hex[0..2], 16),
			Convert.ToByte(hex[2..4], 16),
			Convert.ToByte(hex[4..6], 16)
		);
	}

	/// <summary>
	/// Converts this RGB color to a hex string.
	/// </summary>
	public string ToHex()
	{
		byte r = (byte)Math.Round(R * 255);
		byte g = (byte)Math.Round(G * 255);
		byte b = (byte)Math.Round(B * 255);
		return $"#{r:X2}{g:X2}{b:X2}";
	}

	/// <summary>
	/// Converts this RGB color to 8-bit values.
	/// </summary>
	public (byte R, byte G, byte B) ToBytes() =>
		((byte)Math.Round(R * 255), (byte)Math.Round(G * 255), (byte)Math.Round(B * 255));

	/// <summary>
	/// Converts this linear RGB color to sRGB gamma-corrected values.
	/// </summary>
	public RgbColor ToSRgb()
	{
		return new RgbColor(
			LinearToSRgb(R),
			LinearToSRgb(G),
			LinearToSRgb(B)
		);
	}

	/// <summary>
	/// Converts this sRGB gamma-corrected color to linear RGB values.
	/// </summary>
	public RgbColor ToLinear()
	{
		return new RgbColor(
			SRgbToLinear(R),
			SRgbToLinear(G),
			SRgbToLinear(B)
		);
	}

	private static float LinearToSRgb(float linear)
	{
		return linear <= 0.0031308f
			? 12.92f * linear
			: (1.055f * MathF.Pow(linear, 1f / 2.4f)) - 0.055f;
	}

	private static float SRgbToLinear(float sRgb)
	{
		return sRgb <= 0.04045f
			? sRgb / 12.92f
			: MathF.Pow((sRgb + 0.055f) / 1.055f, 2.4f);
	}
}

/// <summary>
/// Provides color space conversion utilities and color mathematics operations.
/// </summary>
public static class ColorMath
{
	/// <summary>
	/// Converts linear RGB to Oklab color space.
	/// Based on Björn Ottosson's Oklab specification (2020).
	/// </summary>
	public static OklabColor RgbToOklab(RgbColor rgb)
	{
		// Convert to LMS space using the specified matrix
		float l = (0.4122214708f * rgb.R) + (0.5363325363f * rgb.G) + (0.0514459929f * rgb.B);
		float m = (0.2119034982f * rgb.R) + (0.6806995451f * rgb.G) + (0.1073969566f * rgb.B);
		float s = (0.0883024619f * rgb.R) + (0.2817188376f * rgb.G) + (0.6299787005f * rgb.B);

		// Apply cube root
		float l_ = MathF.Sign(l) * MathF.Pow(MathF.Abs(l), 1f / 3f);
		float m_ = MathF.Sign(m) * MathF.Pow(MathF.Abs(m), 1f / 3f);
		float s_ = MathF.Sign(s) * MathF.Pow(MathF.Abs(s), 1f / 3f);

		// Convert to Lab
		return new OklabColor(
			(0.2104542553f * l_) + (0.7936177850f * m_) - (0.0040720468f * s_),
			(1.9779984951f * l_) - (2.4285922050f * m_) + (0.4505937099f * s_),
			(0.0259040371f * l_) + (0.7827717662f * m_) - (0.8086757660f * s_)
		);
	}

	/// <summary>
	/// Converts Oklab to linear RGB color space.
	/// </summary>
	public static RgbColor OklabToRgb(OklabColor oklab)
	{
		// Convert to LMS'
		float l_ = oklab.L + (0.3963377774f * oklab.A) + (0.2158037573f * oklab.B);
		float m_ = oklab.L - (0.1055613458f * oklab.A) - (0.0638541728f * oklab.B);
		float s_ = oklab.L - (0.0894841775f * oklab.A) - (1.2914855480f * oklab.B);

		// Apply cube
		float l = l_ * l_ * l_;
		float m = m_ * m_ * m_;
		float s = s_ * s_ * s_;

		// Convert to RGB
		return new RgbColor(
			(+4.0767416621f * l) - (3.3077115913f * m) + (0.2309699292f * s),
			(-1.2684380046f * l) + (2.6097574011f * m) - (0.3413193965f * s),
			(-0.0041960863f * l) - (0.7034186147f * m) + (1.7076147010f * s)
		);
	}

	/// <summary>
	/// Calculates the relative luminance of a color according to WCAG standards.
	/// Used for contrast ratio calculations.
	/// </summary>
	public static float GetRelativeLuminance(RgbColor rgb)
	{
		RgbColor linear = rgb.ToLinear();
		return (0.2126f * linear.R) + (0.7152f * linear.G) + (0.0722f * linear.B);
	}

	/// <summary>
	/// Calculates the WCAG contrast ratio between two colors.
	/// Returns a value from 1:1 (no contrast) to 21:1 (maximum contrast).
	/// </summary>
	public static float GetContrastRatio(RgbColor color1, RgbColor color2)
	{
		float lum1 = GetRelativeLuminance(color1);
		float lum2 = GetRelativeLuminance(color2);

		float lighter = Math.Max(lum1, lum2);
		float darker = Math.Min(lum1, lum2);

		return (lighter + 0.05f) / (darker + 0.05f);
	}

	/// <summary>
	/// Checks if a color combination meets WCAG accessibility standards.
	/// </summary>
	public static AccessibilityLevel GetAccessibilityLevel(RgbColor foreground, RgbColor background, bool isLargeText = false)
	{
		float contrast = GetContrastRatio(foreground, background);

		float aaThreshold = isLargeText ? 3.0f : 4.5f;
		float aaaThreshold = isLargeText ? 4.5f : 7.0f;

		if (contrast >= aaaThreshold)
		{
			return AccessibilityLevel.AAA;
		}

		if (contrast >= aaThreshold)
		{
			return AccessibilityLevel.AA;
		}

		return AccessibilityLevel.Fail;
	}

	/// <summary>
	/// Adjusts a color's lightness to meet minimum contrast requirements while preserving hue and chroma as much as possible.
	/// </summary>
	public static RgbColor AdjustForAccessibility(RgbColor foreground, RgbColor background, AccessibilityLevel targetLevel, bool isLargeText = false)
	{
		float requiredContrast = targetLevel switch
		{
			AccessibilityLevel.AAA => isLargeText ? 4.5f : 7.0f,
			AccessibilityLevel.AA => isLargeText ? 3.0f : 4.5f,
			_ => 1.0f
		};

		OklabColor oklabFg = RgbToOklab(foreground.ToLinear());
		float backgroundLum = GetRelativeLuminance(background);

		// Binary search for the right lightness adjustment
		float minL = 0f, maxL = 1f;
		const int maxIterations = 20;

		for (int i = 0; i < maxIterations; i++)
		{
			OklabColor adjusted = new((minL + maxL) / 2f, oklabFg.A, oklabFg.B);
			RgbColor adjustedRgb = OklabToRgb(adjusted).ToSRgb();

			// Clamp to valid RGB range
			adjustedRgb = new RgbColor(
				Math.Clamp(adjustedRgb.R, 0f, 1f),
				Math.Clamp(adjustedRgb.G, 0f, 1f),
				Math.Clamp(adjustedRgb.B, 0f, 1f)
			);

			float contrast = GetContrastRatio(adjustedRgb, background);

			if (Math.Abs(contrast - requiredContrast) < 0.1f)
			{
				return adjustedRgb;
			}

			if (contrast < requiredContrast)
			{
				float adjustedLum = GetRelativeLuminance(adjustedRgb);
				if (adjustedLum > backgroundLum)
				{
					minL = adjusted.L;
				}
				else
				{
					maxL = adjusted.L;
				}
			}
			else
			{
				float adjustedLum = GetRelativeLuminance(adjustedRgb);
				if (adjustedLum > backgroundLum)
				{
					maxL = adjusted.L;
				}
				else
				{
					minL = adjusted.L;
				}
			}
		}

		// Fallback: return the original color if adjustment fails
		return foreground;
	}

	/// <summary>
	/// Creates a perceptually uniform gradient between two colors in Oklab space.
	/// </summary>
	public static RgbColor[] CreateGradient(RgbColor from, RgbColor to, int steps)
	{
		if (steps < 2)
		{
			throw new ArgumentException("Gradient must have at least 2 steps", nameof(steps));
		}

		OklabColor fromOklab = RgbToOklab(from.ToLinear());
		OklabColor toOklab = RgbToOklab(to.ToLinear());

		RgbColor[] gradient = new RgbColor[steps];

		for (int i = 0; i < steps; i++)
		{
			float t = i / (float)(steps - 1);
			OklabColor interpolated = OklabColor.Lerp(fromOklab, toOklab, t);
			gradient[i] = OklabToRgb(interpolated).ToSRgb();
		}

		return gradient;
	}
}

/// <summary>
/// Represents accessibility compliance levels according to WCAG standards.
/// </summary>
public enum AccessibilityLevel
{
	/// <summary>Does not meet minimum accessibility standards</summary>
	Fail,
	/// <summary>Meets WCAG AA standards (minimum compliance)</summary>
	AA,
	/// <summary>Meets WCAG AAA standards (enhanced compliance)</summary>
	AAA
}

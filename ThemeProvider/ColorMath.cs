// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

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
	public static float GetRelativeLuminance(RgbColor rgb) =>
		// Input RGB values are already linear, no gamma correction needed
		(0.2126f * rgb.R) + (0.7152f * rgb.G) + (0.0722f * rgb.B);

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

		OklabColor oklabFg = RgbToOklab(foreground);
		float backgroundLum = GetRelativeLuminance(background);

		// Binary search for the right lightness adjustment
		float minL = 0f, maxL = 1f;
		const int maxIterations = 20;

		for (int i = 0; i < maxIterations; i++)
		{
			OklabColor adjusted = new((minL + maxL) / 2f, oklabFg.A, oklabFg.B);
			RgbColor adjustedRgb = OklabToRgb(adjusted);

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

		OklabColor fromOklab = RgbToOklab(from);
		OklabColor toOklab = RgbToOklab(to);

		RgbColor[] gradient = new RgbColor[steps];

		for (int i = 0; i < steps; i++)
		{
			float t = i / (float)(steps - 1);
			OklabColor interpolated = OklabColor.Lerp(fromOklab, toOklab, t);
			gradient[i] = OklabToRgb(interpolated);
		}

		return gradient;
	}
}

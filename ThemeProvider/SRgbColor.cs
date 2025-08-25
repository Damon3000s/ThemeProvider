// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

/// <summary>
/// Represents an RGB color with floating-point precision.
/// </summary>
public readonly record struct SRgbColor(float R, float G, float B)
{
	/// <summary>
	/// Creates an sRGB color from 8-bit values (0-255).
	/// </summary>
	public static SRgbColor FromBytes(byte r, byte g, byte b) =>
		new(r / 255f, g / 255f, b / 255f);

	/// <summary>
	/// Creates an sRGB color from a hex string (e.g., "#FF0000" or "FF0000").
	/// </summary>
	public static SRgbColor FromHex(string hex)
	{
#if !NET6_0_OR_GREATER
		ArgumentNullExceptionPolyfill.ThrowIfNull(hex);
#else
		ArgumentNullException.ThrowIfNull(hex);
#endif

#if NET5_0_OR_GREATER || NETSTANDARD2_1
		if (hex.StartsWith('#'))
#else
		if (hex.StartsWith("#"))
#endif
		{
#if NET5_0_OR_GREATER || NETSTANDARD2_1
			hex = hex[1..];
#else
			hex = hex.Substring(1);
#endif
		}

		if (hex.Length != 6)
		{
			throw new ArgumentException("Invalid hex color format", nameof(hex));
		}

#if NET5_0_OR_GREATER || NETSTANDARD2_1
		return FromBytes(
			Convert.ToByte(hex[0..2], 16),
			Convert.ToByte(hex[2..4], 16),
			Convert.ToByte(hex[4..6], 16)
		);
#else
		return FromBytes(
			Convert.ToByte(hex.Substring(0, 2), 16),
			Convert.ToByte(hex.Substring(2, 2), 16),
			Convert.ToByte(hex.Substring(4, 2), 16)
		);
#endif
	}

	/// <summary>
	/// Converts this sRGB color to a hex string.
	/// </summary>
	public string ToHex()
	{
		byte r = (byte)Math.Round(R * 255);
		byte g = (byte)Math.Round(G * 255);
		byte b = (byte)Math.Round(B * 255);
		return $"#{r:X2}{g:X2}{b:X2}";
	}

	/// <summary>
	/// Converts this sRGB color to 8-bit values.
	/// </summary>
	public (byte R, byte G, byte B) ToBytes() =>
		((byte)Math.Round(R * 255), (byte)Math.Round(G * 255), (byte)Math.Round(B * 255));

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

	private static float SRgbToLinear(float sRgb)
	{
		return sRgb <= 0.04045f
			? sRgb / 12.92f
			: MathF.Pow((sRgb + 0.055f) / 1.055f, 2.4f);
	}
}

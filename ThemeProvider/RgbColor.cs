// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

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
	public SRgbColor ToSRgb()
	{
		return new SRgbColor(
			LinearToSRgb(R),
			LinearToSRgb(G),
			LinearToSRgb(B)
		);
	}

	private static float LinearToSRgb(float linear)
	{
		return linear <= 0.0031308f
			? 12.92f * linear
			: (1.055f * MathF.Pow(linear, 1f / 2.4f)) - 0.055f;
	}
}

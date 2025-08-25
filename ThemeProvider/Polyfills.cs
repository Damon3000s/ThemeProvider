// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

#pragma warning disable IDE0161 //IDE0161: Convert to file-scoped namespace
#pragma warning disable CA1815
#pragma warning disable IDE0290
#pragma warning disable IDE0005
#pragma warning disable IDE0055
#pragma warning disable CS1591

#if NETSTANDARD2_0 || NETSTANDARD2_1
namespace System.Runtime.CompilerServices
{
	/// <summary>
	/// Reserved to be used by the compiler for tracking metadata.
	/// This class should not be used by developers in source code.
	/// </summary>
	internal static class IsExternalInit
	{
	}
}
#endif

namespace ktsu.ThemeProvider
{

#if NETSTANDARD2_0 || NETSTANDARD2_1
}

// Provide minimal System.Numerics.Vector3 for netstandard2.0 targets that don't have it
namespace System.Numerics
{
	#if NETSTANDARD2_0
	// Minimal polyfill for Vector3 used only as a data container.
	public readonly struct Vector3
	{
		public float X { get; }
		public float Y { get; }
		public float Z { get; }

		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}
	#endif
}

namespace ktsu.ThemeProvider
{
#endif
#if !NET6_0_OR_GREATER
	/// <summary>
	/// Polyfill for ArgumentNullException.ThrowIfNull for older .NET versions
	/// </summary>
	internal static class ArgumentNullExceptionPolyfill
	{
		/// <summary>
		/// Throws an <see cref="System.ArgumentNullException"/> if <paramref name="argument"/> is null.
		/// </summary>
		/// <param name="argument">The reference type argument to validate as non-null.</param>
		/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
		public static void ThrowIfNull(object? argument, string? paramName = null)
		{
			if (argument is null)
			{
				throw new System.ArgumentNullException(paramName);
			}
		}
	}
#endif

	/// <summary>
	/// Minimal math helpers used where newer APIs aren't available.
	/// </summary>
	internal static class CompatMath
	{
		public static float Clamp(float value, float min, float max)
		{
#if NET6_0_OR_GREATER
			return Math.Clamp(value, min, max);
#else
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
#endif
		}
	}
}
#pragma warning restore IDE0161

#if NETSTANDARD2_0
namespace System
{
	/// <summary>
	/// Minimal MathF polyfill backed by double-precision Math operations.
	/// </summary>
	public static class MathF
	{
		public const float PI = (float)Math.PI;
		public static float Abs(float x) => (float)Math.Abs(x);
		public static float Sign(float x) => Math.Sign(x);
		public static float Sqrt(float x) => (float)Math.Sqrt(x);
		public static float Pow(float x, float y) => (float)Math.Pow(x, y);
		public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);
		public static float Cos(float x) => (float)Math.Cos(x);
		public static float Sin(float x) => (float)Math.Sin(x);
	}
}
#endif

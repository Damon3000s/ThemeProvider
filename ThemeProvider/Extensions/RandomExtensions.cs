// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="Random"/> class.
/// </summary>
public static class RandomExtensions
{
	/// <returns>A double representing a normally distributed random value (mean=0, stddev=1).</returns>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "<Pending>")]
	public static double NextGaussian(this Random random)
	{
		ArgumentNullException.ThrowIfNull(random);

		// Box-Muller transform
		static double NextGaussianInternal(Random rnd)
		{
			double u1 = 1.0 - rnd.NextDouble();
			double u2 = 1.0 - rnd.NextDouble();
			return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
		}

		return NextGaussianInternal(random);
	}
}

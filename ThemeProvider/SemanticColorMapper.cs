// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

/// <summary>
/// Maps semantic color requests to actual colors using a global lightness-based priority system.
/// All colors with the same priority across different semantic meanings will have similar lightness levels.
/// </summary>
public sealed class SemanticColorMapper
{
	/// <summary>
	/// Maps a collection of semantic color requests to actual colors using the provided theme.
	/// Returns a complete mapping for all priority levels of the semantic meanings that were requested.
	/// Uses a global lightness range divided by priority levels to ensure consistent visual hierarchy.
	/// </summary>
	/// <param name="requests">The collection of semantic color requests to map</param>
	/// <param name="theme">The semantic theme providing available colors for each meaning</param>
	/// <returns>A dictionary mapping each request to its assigned color, including all priority levels for the requested semantic meanings</returns>
	public static IReadOnlyDictionary<SemanticColorRequest, PerceptualColor> MapColors(
		IEnumerable<SemanticColorRequest> requests,
		ISemanticTheme theme)
	{
#if !NET6_0_OR_GREATER
		ArgumentNullExceptionPolyfill.ThrowIfNull(requests);
		ArgumentNullExceptionPolyfill.ThrowIfNull(theme);
#else
		ArgumentNullException.ThrowIfNull(requests);
		ArgumentNullException.ThrowIfNull(theme);
#endif

		List<SemanticColorRequest> requestsList = [.. requests];
		if (requestsList.Count == 0)
		{
			return new Dictionary<SemanticColorRequest, PerceptualColor>();
		}

		// Calculate the global lightness range for use by all semantics
		(float globalMinLightness, float globalMaxLightness) = CalculateGlobalLightnessRange(theme);

		// Always use ALL possible priority levels for consistent mapping
#if NET5_0_OR_GREATER
		Priority[] allPriorities = Enum.GetValues<Priority>();
#else
		Priority[] allPriorities = (Priority[])Enum.GetValues(typeof(Priority));
#endif
		List<Priority> priorityLevels = [.. allPriorities.OrderBy(p => p)];

		Dictionary<SemanticColorRequest, PerceptualColor> result = [];

		// Get all unique semantic meanings from the requests
		HashSet<SemanticMeaning> requestedMeanings = [.. requestsList.Select(r => r.Meaning)];

		// Generate complete mappings for all priority levels of each requested semantic meaning
		foreach (SemanticMeaning meaning in requestedMeanings)
		{
			// Get available colors for this semantic meaning
			if (!theme.SemanticMapping.TryGetValue(meaning, out Collection<PerceptualColor>? availableColors) ||
				availableColors.Count == 0)
			{
				continue; // Skip if no colors available for this meaning
			}

			// Generate colors for ALL priority levels for this semantic meaning
			foreach (Priority priority in allPriorities)
			{
				SemanticColorRequest fullRequest = new(meaning, priority);

				// Calculate target lightness based on whether this is neutral or non-neutral
				float targetLightness = CalculateTargetLightnessForSemantic(
					priority, meaning, globalMinLightness, globalMaxLightness, theme.IsDarkTheme);

				// Generate the color using interpolation/extrapolation
				PerceptualColor color = InterpolateToTargetLightness(availableColors, targetLightness);

				result[fullRequest] = color;
			}
		}

		return result;
	}

	/// <summary>
	/// Generates a complete palette containing all possible combinations of semantic meanings and priorities from the theme.
	/// This provides every color that can be requested from the theme, useful for theme exploration, previews, and pre-generation.
	/// </summary>
	/// <param name="theme">The semantic theme to generate the complete palette from</param>
	/// <returns>A dictionary mapping every possible semantic color request to its assigned color</returns>
	public static IReadOnlyDictionary<SemanticColorRequest, PerceptualColor> MakeCompletePalette(ISemanticTheme theme)
	{
#if !NET6_0_OR_GREATER
		ArgumentNullExceptionPolyfill.ThrowIfNull(theme);
#else
		ArgumentNullException.ThrowIfNull(theme);
#endif

		// Get all available semantic meanings from the theme
		HashSet<SemanticMeaning> availableMeanings = [.. theme.SemanticMapping.Keys];

		if (availableMeanings.Count == 0)
		{
			return new Dictionary<SemanticColorRequest, PerceptualColor>();
		}

		// Get all possible priorities
#if NET5_0_OR_GREATER
		Priority[] allPriorities = Enum.GetValues<Priority>();
#else
		Priority[] allPriorities = (Priority[])Enum.GetValues(typeof(Priority));
#endif

		// Generate requests for all combinations of meanings and priorities
		List<SemanticColorRequest> allPossibleRequests = [];
		foreach (SemanticMeaning meaning in availableMeanings)
		{
			foreach (Priority priority in allPriorities)
			{
				allPossibleRequests.Add(new SemanticColorRequest(meaning, priority));
			}
		}

		// Use the existing MapColors method to generate the complete palette
		// This ensures consistency with individual color requests
		return MapColors(allPossibleRequests, theme);
	}

	/// <summary>
	/// Calculates the lightness range across all available semantics in the theme.
	/// This range will be used as the basis for all semantic color mappings.
	/// </summary>
	private static (float min, float max) CalculateGlobalLightnessRange(ISemanticTheme theme)
	{
		float globalMin = float.MaxValue;
		float globalMax = float.MinValue;

		foreach (KeyValuePair<SemanticMeaning, Collection<PerceptualColor>> kvp in theme.SemanticMapping)
		{
			Collection<PerceptualColor> colors = kvp.Value;
			for (int i = 0; i < colors.Count; i++)
			{
				PerceptualColor color = colors[i];
				if (color.Lightness < globalMin)
				{
					globalMin = color.Lightness;
				}
				if (color.Lightness > globalMax)
				{
					globalMax = color.Lightness;
				}
			}
		}

		// Ensure we have a valid range
		if (globalMin == float.MaxValue || globalMax == float.MinValue)
		{
			return (0.0f, 1.0f); // Fallback range
		}

		return (globalMin, globalMax);
	}

	/// <summary>
	/// Calculates the target lightness for a specific semantic meaning and priority.
	/// Neutral semantics use the full global range, while non-neutral semantics use 50-90% of it.
	/// </summary>
	private static float CalculateTargetLightnessForSemantic(
		Priority priority,
		SemanticMeaning meaning,
		float globalMinLightness,
		float globalMaxLightness,
		bool isDarkTheme)
	{
		// Get all priorities and find the position of the current priority
#if NET5_0_OR_GREATER
		Priority[] allPriorities = Enum.GetValues<Priority>();
#else
		Priority[] allPriorities = (Priority[])Enum.GetValues(typeof(Priority));
#endif
		int priorityIndex = Array.IndexOf(allPriorities, priority);

		if (allPriorities.Length == 1)
		{
			float globalCenter = (globalMinLightness + globalMaxLightness) / 2.0f;
			return meaning == SemanticMeaning.Neutral ? globalCenter : globalCenter;
		}

		// Calculate position in range (0.0 to 1.0)
		float position = priorityIndex / (float)(allPriorities.Length - 1);

		// Determine the lightness range to use
		float minLightness, maxLightness;
		if (meaning == SemanticMeaning.Neutral)
		{
			// Neutral uses the full global range
			minLightness = globalMinLightness;
			maxLightness = globalMaxLightness;
		}
		else
		{
			// Non-neutral uses 50-90% of the global range
			float globalRange = globalMaxLightness - globalMinLightness;
			float rangeStart = globalMinLightness + (globalRange * 0.5f); // 50%
			float rangeEnd = globalMinLightness + (globalRange * 0.9f);   // 90%

			minLightness = rangeStart;
			maxLightness = rangeEnd;
		}

		float lightnessRange = maxLightness - minLightness;

		// Calculate target lightness based on theme type
		float targetLightness;
		if (isDarkTheme)
		{
			// In dark themes, higher priority (later in enum) gets higher lightness (more visible)
			targetLightness = minLightness + (position * lightnessRange);
		}
		else
		{
			// In light themes, higher priority (later in enum) gets lower lightness (more visible)
			targetLightness = maxLightness - (position * lightnessRange);
		}

#if NET6_0_OR_GREATER
		return Math.Clamp(targetLightness, 0.0f, 1.0f);
#else
		return CompatMath.Clamp(targetLightness, 0.0f, 1.0f);
#endif
	}

	/// <summary>
	/// Creates a color with the target lightness by interpolating or extrapolating from available colors.
	/// Preserves hue and chroma characteristics while achieving the exact lightness needed for priority hierarchy.
	/// </summary>
	private static PerceptualColor InterpolateToTargetLightness(
		Collection<PerceptualColor> availableColors,
		float targetLightness)
	{
		if (availableColors.Count == 0)
		{
			throw new ArgumentException("No colors available", nameof(availableColors));
		}

		if (availableColors.Count == 1)
		{
			// Single color - extrapolate by adjusting lightness while preserving hue and chroma
			return ExtrapolateColorToLightness(availableColors[0], targetLightness);
		}

		// Multiple colors - find the best interpolation or extrapolation
		List<PerceptualColor> colorsList = [.. availableColors.OrderBy(c => c.Lightness)];

		float minLightness = colorsList[0].Lightness;
#if NET5_0_OR_GREATER || NETSTANDARD2_1
		float maxLightness = colorsList[^1].Lightness;
#else
		float maxLightness = colorsList[colorsList.Count - 1].Lightness;
#endif

		// If target is within the range, interpolate between the closest colors
		if (targetLightness >= minLightness && targetLightness <= maxLightness)
		{
			return InterpolateBetweenColors(colorsList, targetLightness);
		}

		// If target is outside the range, extrapolate from the closest end
		if (targetLightness < minLightness)
		{
			// Extrapolate from the darkest color
			return ExtrapolateColorToLightness(colorsList[0], targetLightness);
		}
		else
		{
			// Extrapolate from the lightest color
#if NET5_0_OR_GREATER || NETSTANDARD2_1
			return ExtrapolateColorToLightness(colorsList[^1], targetLightness);
#else
			return ExtrapolateColorToLightness(colorsList[colorsList.Count - 1], targetLightness);
#endif
		}
	}

	/// <summary>
	/// Extrapolates a single color to a target lightness while preserving its hue and chroma.
	/// </summary>
	private static PerceptualColor ExtrapolateColorToLightness(PerceptualColor baseColor, float targetLightness)
	{
		// Work in Oklab space for perceptually uniform lightness adjustment
		OklabColor baseOklab = baseColor.OklabValue;

		// Start with the target lightness and original chroma
		OklabColor targetOklab = new(
			L: CompatMath.Clamp(targetLightness, 0.0f, 1.0f),
			A: baseOklab.A,
			B: baseOklab.B
		);

		// Convert to RGB to check if it's in gamut
		RgbColor targetRgb = ColorMath.OklabToRgb(targetOklab);

		// Check if RGB values are within valid range
		bool inGamut = targetRgb.R >= 0f && targetRgb.R <= 1f &&
				   targetRgb.G >= 0f && targetRgb.G <= 1f &&
				   targetRgb.B >= 0f && targetRgb.B <= 1f;

		if (inGamut)
		{
			// Color is in gamut, use it directly
			return new PerceptualColor(targetRgb);
		}

		// Color is out of gamut, we need to find the best in-gamut color
		// by reducing chroma while maintaining the target lightness
		float originalChroma = MathF.Sqrt((baseOklab.A * baseOklab.A) + (baseOklab.B * baseOklab.B));
		float hue = MathF.Atan2(baseOklab.B, baseOklab.A);

		// Binary search for the maximum chroma that keeps us in gamut
		float minChroma = 0f;
		float maxChroma = originalChroma;
		const float tolerance = 0.001f;
		const int maxIterations = 20;

		OklabColor bestOklab = targetOklab;

		for (int i = 0; i < maxIterations && (maxChroma - minChroma) > tolerance; i++)
		{
			float testChroma = (minChroma + maxChroma) / 2f;

			// Create color with reduced chroma
			OklabColor testOklab = new(
				L: targetLightness,
				A: testChroma * MathF.Cos(hue),
				B: testChroma * MathF.Sin(hue)
			);

			RgbColor testRgb = ColorMath.OklabToRgb(testOklab);

			bool testInGamut = testRgb.R >= 0f && testRgb.R <= 1f &&
						   testRgb.G >= 0f && testRgb.G <= 1f &&
						   testRgb.B >= 0f && testRgb.B <= 1f;

			if (testInGamut)
			{
				// This chroma works, try higher
				minChroma = testChroma;
				bestOklab = testOklab;
			}
			else
			{
				// This chroma is too high, try lower
				maxChroma = testChroma;
			}
		}

		// Convert the best in-gamut color to RGB
		RgbColor bestRgb = ColorMath.OklabToRgb(bestOklab);

		// Final safety clamp (should not be needed if binary search worked correctly)
#if NET6_0_OR_GREATER
		bestRgb = new RgbColor(
			Math.Clamp(bestRgb.R, 0f, 1f),
			Math.Clamp(bestRgb.G, 0f, 1f),
			Math.Clamp(bestRgb.B, 0f, 1f)
		);
#else
		bestRgb = new RgbColor(
			CompatMath.Clamp(bestRgb.R, 0f, 1f),
			CompatMath.Clamp(bestRgb.G, 0f, 1f),
			CompatMath.Clamp(bestRgb.B, 0f, 1f)
		);
#endif

		return new PerceptualColor(bestRgb);
	}

	/// <summary>
	/// Interpolates between colors in a sorted list to achieve a target lightness.
	/// </summary>
	private static PerceptualColor InterpolateBetweenColors(List<PerceptualColor> sortedColors, float targetLightness)
	{
		// Find the two colors that bracket the target lightness
		PerceptualColor lowerColor = sortedColors[0];
#if NET5_0_OR_GREATER || NETSTANDARD2_1
		PerceptualColor upperColor = sortedColors[^1];
#else
		PerceptualColor upperColor = sortedColors[sortedColors.Count - 1];
#endif

		for (int i = 0; i < sortedColors.Count - 1; i++)
		{
			if (targetLightness >= sortedColors[i].Lightness && targetLightness <= sortedColors[i + 1].Lightness)
			{
				lowerColor = sortedColors[i];
				upperColor = sortedColors[i + 1];
				break;
			}
		}

		// Calculate interpolation factor
		float lightnessRange = upperColor.Lightness - lowerColor.Lightness;
		float t = lightnessRange == 0 ? 0.5f : (targetLightness - lowerColor.Lightness) / lightnessRange;
#if NET6_0_OR_GREATER
		t = Math.Clamp(t, 0f, 1f);
#else
		t = CompatMath.Clamp(t, 0f, 1f);
#endif

		// Interpolate in Oklab space
		OklabColor interpolatedOklab = OklabColor.Lerp(
			lowerColor.OklabValue,
			upperColor.OklabValue,
			t);

		// Convert back to RGB
		RgbColor interpolatedRgb = ColorMath.OklabToRgb(interpolatedOklab);

		// Clamp RGB values to valid range
#if NET6_0_OR_GREATER
		interpolatedRgb = new RgbColor(
			Math.Clamp(interpolatedRgb.R, 0f, 1f),
			Math.Clamp(interpolatedRgb.G, 0f, 1f),
			Math.Clamp(interpolatedRgb.B, 0f, 1f)
		);
#else
		interpolatedRgb = new RgbColor(
			CompatMath.Clamp(interpolatedRgb.R, 0f, 1f),
			CompatMath.Clamp(interpolatedRgb.G, 0f, 1f),
			CompatMath.Clamp(interpolatedRgb.B, 0f, 1f)
		);
#endif

		return new PerceptualColor(interpolatedRgb);
	}
}

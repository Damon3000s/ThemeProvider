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
		Ensure.NotNull(requests);
		Ensure.NotNull(theme);

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
		Ensure.NotNull(theme);

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

		return Math.Max(0.0f, Math.Min(targetLightness, 1.0f));
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
		float maxLightness = colorsList[^1].Lightness;

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
			return ExtrapolateColorToLightness(colorsList[^1], targetLightness);
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
			L: Math.Max(0.0f, Math.Min(targetLightness, 1.0f)),
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
		float originalChroma = (float)Math.Sqrt((baseOklab.A * baseOklab.A) + (baseOklab.B * baseOklab.B));
		float hue = (float)Math.Atan2(baseOklab.B, baseOklab.A);

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
				A: testChroma * (float)Math.Cos(hue),
				B: testChroma * (float)Math.Sin(hue)
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
		bestRgb = new RgbColor(
			Math.Max(0f, Math.Min(bestRgb.R, 1f)),
			Math.Max(0f, Math.Min(bestRgb.G, 1f)),
			Math.Max(0f, Math.Min(bestRgb.B, 1f))
		);

		return new PerceptualColor(bestRgb);
	}

	/// <summary>
	/// Interpolates between colors in a sorted list to achieve a target lightness.
	/// </summary>
	private static PerceptualColor InterpolateBetweenColors(List<PerceptualColor> sortedColors, float targetLightness)
	{
		// Find the two colors that bracket the target lightness
		PerceptualColor lowerColor = sortedColors[0];
		PerceptualColor upperColor = sortedColors[^1];

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
		t = Math.Max(0f, Math.Min(t, 1f));

		// Interpolate in Oklab space
		OklabColor interpolatedOklab = OklabColor.Lerp(
			lowerColor.OklabValue,
			upperColor.OklabValue,
			t);

		// Convert back to RGB
		RgbColor interpolatedRgb = ColorMath.OklabToRgb(interpolatedOklab);

		// Clamp RGB values to valid range
		interpolatedRgb = new RgbColor(
			Math.Max(0f, Math.Min(interpolatedRgb.R, 1f)),
			Math.Max(0f, Math.Min(interpolatedRgb.G, 1f)),
			Math.Max(0f, Math.Min(interpolatedRgb.B, 1f))
		);

		return new PerceptualColor(interpolatedRgb);
	}
}

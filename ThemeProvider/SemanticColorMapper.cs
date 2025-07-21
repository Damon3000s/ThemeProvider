// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

using System.Collections.Generic;
using System.Collections.Immutable;
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
	/// Uses a global lightness range divided by priority levels to ensure consistent visual hierarchy.
	/// </summary>
	/// <param name="requests">The collection of semantic color requests to map</param>
	/// <param name="theme">The semantic theme providing available colors for each meaning</param>
	/// <returns>A dictionary mapping each request to its assigned color</returns>
	public static ImmutableDictionary<SemanticColorRequest, PerceptualColor> MapColors(
		IEnumerable<SemanticColorRequest> requests,
		ISemanticTheme theme)
	{
		ArgumentNullException.ThrowIfNull(requests);
		ArgumentNullException.ThrowIfNull(theme);

		List<SemanticColorRequest> requestsList = [.. requests];
		if (requestsList.Count == 0)
		{
			return ImmutableDictionary<SemanticColorRequest, PerceptualColor>.Empty;
		}

		// Calculate the global lightness range across all semantic meanings
		(float minLightness, float maxLightness) = CalculateGlobalLightnessRange(theme);

		// Get all unique priority levels from the requests
		List<Priority> uniquePriorities = [.. requestsList
			.Select(r => r.Priority)
			.Distinct()
			.OrderBy(p => p)];

		// Create a mapping of priority to target lightness
		Dictionary<Priority, float> priorityToLightness = CalculatePriorityLightnessMapping(
			uniquePriorities, minLightness, maxLightness, theme.IsDarkTheme);

		Dictionary<SemanticColorRequest, PerceptualColor> result = [];

		// Process each request
		foreach (SemanticColorRequest request in requestsList)
		{
			// Get available colors for this semantic meaning
			if (!theme.SemanticMapping.TryGetValue(request.Meaning, out Collection<PerceptualColor>? availableColors) ||
				availableColors.Count == 0)
			{
				continue; // Skip if no colors available for this meaning
			}

			// Get the target lightness for this priority
			float targetLightness = priorityToLightness[request.Priority];

			// Find the closest color by lightness within this semantic meaning
			PerceptualColor bestColor = InterpolateToTargetLightness(availableColors, targetLightness);

			result[request] = bestColor;
		}

		return result.ToImmutableDictionary();
	}

	/// <summary>
	/// Calculates the global lightness range across all colors in all semantic meanings.
	/// </summary>
	private static (float min, float max) CalculateGlobalLightnessRange(ISemanticTheme theme)
	{
		float minLightness = float.MaxValue;
		float maxLightness = float.MinValue;

		foreach ((SemanticMeaning meaning, Collection<PerceptualColor> colors) in theme.SemanticMapping)
		{
			foreach (PerceptualColor color in colors)
			{
				if (color.Lightness < minLightness)
				{
					minLightness = color.Lightness;
				}
				if (color.Lightness > maxLightness)
				{
					maxLightness = color.Lightness;
				}
			}
		}

		// Ensure we have a valid range
		if (minLightness == float.MaxValue || maxLightness == float.MinValue)
		{
			return (0.0f, 1.0f); // Fallback range
		}

		return (minLightness, maxLightness);
	}

	/// <summary>
	/// Creates a mapping from priority levels to target lightness values by dividing
	/// the global lightness range according to the priority levels.
	/// </summary>
	private static Dictionary<Priority, float> CalculatePriorityLightnessMapping(
		List<Priority> priorities,
		float minLightness,
		float maxLightness,
		bool isDarkTheme)
	{
		Dictionary<Priority, float> result = [];

		if (priorities.Count == 0)
		{
			return result;
		}

		if (priorities.Count == 1)
		{
			// Single priority gets the middle of the range
			result[priorities[0]] = (minLightness + maxLightness) / 2.0f;
			return result;
		}

		float lightnessRange = maxLightness - minLightness;

		// For dark themes, higher priority should be lighter (more visible)
		// For light themes, higher priority should be darker (more visible)
		for (int i = 0; i < priorities.Count; i++)
		{
			Priority priority = priorities[i];

			// Calculate position in range (0.0 to 1.0)
			float position = priorities.Count == 1 ? 0.5f : i / (float)(priorities.Count - 1);

			float targetLightness;
			if (isDarkTheme)
			{
				// In dark themes, higher priority (later in enum) gets higher lightness
				targetLightness = minLightness + (position * lightnessRange);
			}
			else
			{
				// In light themes, higher priority (later in enum) gets lower lightness
				targetLightness = maxLightness - (position * lightnessRange);
			}

			result[priority] = Math.Clamp(targetLightness, 0.0f, 1.0f);
		}

		return result;
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
			return ExtrapolateColorToLightness(availableColors.First(), targetLightness);
		}

		// Multiple colors - find the best interpolation or extrapolation
		List<PerceptualColor> colorsList = [.. availableColors.OrderBy(c => c.Lightness)];

		float minLightness = colorsList.First().Lightness;
		float maxLightness = colorsList.Last().Lightness;

		// If target is within the range, interpolate between the closest colors
		if (targetLightness >= minLightness && targetLightness <= maxLightness)
		{
			return InterpolateBetweenColors(colorsList, targetLightness);
		}

		// If target is outside the range, extrapolate from the closest end
		if (targetLightness < minLightness)
		{
			// Extrapolate from the darkest color
			return ExtrapolateColorToLightness(colorsList.First(), targetLightness);
		}
		else
		{
			// Extrapolate from the lightest color
			return ExtrapolateColorToLightness(colorsList.Last(), targetLightness);
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
			L: Math.Clamp(targetLightness, 0.0f, 1.0f),
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
		bestRgb = new RgbColor(
			Math.Clamp(bestRgb.R, 0f, 1f),
			Math.Clamp(bestRgb.G, 0f, 1f),
			Math.Clamp(bestRgb.B, 0f, 1f)
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
		t = Math.Clamp(t, 0f, 1f);

		// Interpolate in Oklab space
		OklabColor interpolatedOklab = OklabColor.Lerp(
			lowerColor.OklabValue,
			upperColor.OklabValue,
			t);

		// Convert back to RGB
		RgbColor interpolatedRgb = ColorMath.OklabToRgb(interpolatedOklab);

		// Clamp RGB values to valid range
		interpolatedRgb = new RgbColor(
			Math.Clamp(interpolatedRgb.R, 0f, 1f),
			Math.Clamp(interpolatedRgb.G, 0f, 1f),
			Math.Clamp(interpolatedRgb.B, 0f, 1f)
		);

		return new PerceptualColor(interpolatedRgb);
	}
}

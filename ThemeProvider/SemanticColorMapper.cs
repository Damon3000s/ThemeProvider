// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

/// <summary>
/// Maps semantic color requests to actual colors by interpolating within available color ranges
/// based on priority levels for each semantic meaning.
/// </summary>
public sealed class SemanticColorMapper
{
	/// <summary>
	/// Maps a collection of semantic color requests to actual colors using the provided theme.
	/// Colors are interpolated within each semantic's available range based on priority levels.
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

		Dictionary<SemanticColorRequest, PerceptualColor> result = [];

		// Group requests by semantic meaning
		List<IGrouping<SemanticMeaning, SemanticColorRequest>> requestsByMeaning = [.. requestsList
			.GroupBy(request => request.Meaning)];

		foreach (IGrouping<SemanticMeaning, SemanticColorRequest> meaningGroup in requestsByMeaning)
		{
			SemanticMeaning meaning = meaningGroup.Key;
			List<SemanticColorRequest> requestsForMeaning = [.. meaningGroup.OrderBy(r => r.Priority)];

			// Get available colors for this semantic meaning
			if (!theme.SemanticMapping.TryGetValue(meaning, out Collection<PerceptualColor>? availableColors) ||
				availableColors.Count == 0)
			{
				continue; // Skip if no colors available for this meaning
			}

			// Map colors for this semantic meaning
			Dictionary<SemanticColorRequest, PerceptualColor> mappedColors = MapColorsForSemantic(requestsForMeaning, availableColors, theme.IsDarkTheme);

			foreach ((SemanticColorRequest request, PerceptualColor color) in mappedColors)
			{
				result[request] = color;
			}
		}

		return result.ToImmutableDictionary();
	}

	/// <summary>
	/// Maps colors for a specific semantic meaning by interpolating across the available color range
	/// based on priority distribution.
	/// </summary>
	private static Dictionary<SemanticColorRequest, PerceptualColor> MapColorsForSemantic(
		List<SemanticColorRequest> requests,
		Collection<PerceptualColor> availableColors,
		bool isDarkTheme)
	{
		Dictionary<SemanticColorRequest, PerceptualColor> result = [];

		if (requests.Count == 0 || availableColors.Count == 0)
		{
			return result;
		}

		// If only one color available, assign it to all requests
		if (availableColors.Count == 1)
		{
			PerceptualColor singleColor = availableColors.First();
			foreach (SemanticColorRequest request in requests)
			{
				result[request] = singleColor;
			}
			return result;
		}

		// Calculate color range bounds
		ColorRange colorRange = CalculateColorRange(availableColors, isDarkTheme);

		// Get unique priority levels in order
		List<Priority> uniquePriorities = [.. requests
			.Select(r => r.Priority)
			.Distinct()
			.OrderBy(p => p)];

		// If only one priority level, use the middle of the range
		if (uniquePriorities.Count == 1)
		{
			PerceptualColor middleColor = InterpolateInRange(colorRange, t: 0.5f);
			foreach (SemanticColorRequest request in requests)
			{
				result[request] = middleColor;
			}
			return result;
		}

		// Map each priority to a position along the color range
		for (int i = 0; i < uniquePriorities.Count; i++)
		{
			Priority priority = uniquePriorities[i];
			float t = uniquePriorities.Count == 1 ? 0.5f : i / (float)(uniquePriorities.Count - 1);
			PerceptualColor interpolatedColor = InterpolateInRange(colorRange, t);

			// Assign this color to all requests with this priority
			foreach (SemanticColorRequest request in requests.Where(r => r.Priority == priority))
			{
				result[request] = interpolatedColor;
			}
		}

		return result;
	}

	/// <summary>
	/// Calculates the color range from available colors by finding the extremes
	/// in perceptual color space, ordered appropriately for the theme type.
	/// </summary>
	private static ColorRange CalculateColorRange(Collection<PerceptualColor> colors, bool isDarkTheme)
	{
		List<PerceptualColor> colorsList = [.. colors];

		if (colorsList.Count == 1)
		{
			return new ColorRange(colorsList[0], colorsList[0]);
		}

		// Find the two colors with maximum perceptual distance
		float maxDistance = 0f;
		PerceptualColor rangeStart = colorsList[0];
		PerceptualColor rangeEnd = colorsList[0];

		for (int i = 0; i < colorsList.Count; i++)
		{
			for (int j = i + 1; j < colorsList.Count; j++)
			{
				float distance = colorsList[i].SemanticDistanceTo(colorsList[j]);
				if (distance > maxDistance)
				{
					maxDistance = distance;
					rangeStart = colorsList[i];
					rangeEnd = colorsList[j];
				}
			}
		}

		// Use the theme-aware ordering
		return ColorRange.FromColors(rangeStart, rangeEnd, isDarkTheme);
	}

	/// <summary>
	/// Interpolates between the start and end colors of a range using the given parameter.
	/// </summary>
	private static PerceptualColor InterpolateInRange(ColorRange range, float t)
	{
		t = Math.Clamp(t, 0f, 1f);

		OklabColor interpolatedOklab = OklabColor.Lerp(
			range.Start.OklabValue,
			range.End.OklabValue,
			t);

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

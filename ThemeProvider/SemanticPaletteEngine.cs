// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;
using System.Collections.Immutable;
using System.Numerics;

/// <summary>
/// Represents a semantic request for colors, describing the desired characteristics
/// rather than specific colors. This allows the system to generate appropriate
/// colors that match the theme while meeting accessibility requirements.
/// </summary>
public readonly record struct SemanticColorRequest
{
	/// <summary>The primary semantic role being requested</summary>
	public ColorRole PrimaryRole { get; init; }

	/// <summary>Related roles that should be considered for harmony</summary>
	public ImmutableArray<ColorRole> RelatedRoles { get; init; } = [];

	/// <summary>Desired temperature bias (-1.0 = cool, 1.0 = warm)</summary>
	public float DesiredTemperature { get; init; } = 0.0f;

	/// <summary>Desired energy level (0.0 = calm, 1.0 = energetic)</summary>
	public float DesiredEnergy { get; init; } = 0.5f;

	/// <summary>Desired formality level (0.0 = casual, 1.0 = formal)</summary>
	public float DesiredFormality { get; init; } = 0.5f;

	/// <summary>Required accessibility level</summary>
	public AccessibilityLevel AccessibilityRequirement { get; init; } = AccessibilityLevel.AA;

	/// <summary>Background color this will be used against (for contrast calculation)</summary>
	public RgbColor? BackgroundColor { get; init; }

	/// <summary>Whether this is for large text (affects contrast requirements)</summary>
	public bool IsLargeText { get; init; } = false;

	/// <summary>Importance weight (0.0 = low priority, 1.0 = critical)</summary>
	public float ImportanceWeight { get; init; } = 0.5f;

	/// <summary>Optional constraints on the final color</summary>
	public ColorConstraints? Constraints { get; init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="SemanticColorRequest"/> record struct
	/// with default values for all properties.
	/// </summary>
	public SemanticColorRequest()
	{
	}
}

/// <summary>
/// Defines constraints that can be applied to color generation.
/// </summary>
public readonly record struct ColorConstraints
{
	/// <summary>Minimum lightness (0.0 = black, 1.0 = white)</summary>
	public float? MinLightness { get; init; }

	/// <summary>Maximum lightness (0.0 = black, 1.0 = white)</summary>
	public float? MaxLightness { get; init; }

	/// <summary>Minimum chroma/saturation</summary>
	public float? MinChroma { get; init; }

	/// <summary>Maximum chroma/saturation</summary>
	public float? MaxChroma { get; init; }

	/// <summary>Preferred hue range (in degrees, 0-360)</summary>
	public (float Min, float Max)? HueRange { get; init; }

	/// <summary>Colors to avoid (maintain distance from)</summary>
	public ImmutableArray<RgbColor> AvoidColors { get; init; } = [];

	/// <summary>Minimum distance to maintain from avoid colors</summary>
	public float AvoidanceDistance { get; init; } = 0.1f;

	/// <summary>
	/// Initializes a new instance of the <see cref="ColorConstraints"/> record struct
	/// with default values for all properties.
	/// </summary>
	public ColorConstraints()
	{
	}
}

/// <summary>
/// Represents a semantic graph of color relationships for an application.
/// This describes how colors should work together without specifying exact colors.
/// </summary>
public sealed record SemanticColorGraph
{
	/// <summary>All color requests in this graph</summary>
	public required ImmutableArray<SemanticColorRequest> Requests { get; init; }

	/// <summary>Relationships between color requests that should be harmonious</summary>
	public ImmutableArray<(int FromIndex, int ToIndex, float HarmonyWeight)> Relationships { get; init; } = [];

	/// <summary>Global constraints that apply to the entire palette</summary>
	public GlobalConstraints GlobalConstraints { get; init; } = new();

	/// <summary>
	/// Creates a builder for constructing semantic color graphs.
	/// </summary>
	public static SemanticColorGraphBuilder CreateBuilder() => new();
}

/// <summary>
/// Global constraints that apply to an entire color palette.
/// </summary>
public readonly record struct GlobalConstraints
{
	/// <summary>Overall color harmony preference (0.0 = diverse, 1.0 = harmonious)</summary>
	public float HarmonyPreference { get; init; } = 0.7f;

	/// <summary>Overall accessibility priority (0.0 = aesthetics first, 1.0 = accessibility first)</summary>
	public float AccessibilityPriority { get; init; } = 0.8f;

	/// <summary>Color distribution preference (0.0 = clustered, 1.0 = distributed)</summary>
	public float DistributionPreference { get; init; } = 0.6f;

	/// <summary>Whether to prefer theme authenticity over exact semantic matching</summary>
	public bool PreferThemeAuthenticity { get; init; } = true;

	/// <summary>
	/// Initializes a new instance of the <see cref="GlobalConstraints"/> record struct
	/// with default values for all properties.
	/// </summary>
	public GlobalConstraints()
	{
	}
}

/// <summary>
/// Builder for creating semantic color graphs with fluent API.
/// </summary>
public sealed class SemanticColorGraphBuilder
{
	private readonly List<SemanticColorRequest> requests = [];
	private readonly List<(int, int, float)> relationships = [];
	private GlobalConstraints globalConstraints = new();

	/// <summary>Adds a color request to the graph.</summary>
	public SemanticColorGraphBuilder AddRequest(SemanticColorRequest request)
	{
		requests.Add(request);
		return this;
	}

	/// <summary>Adds a simple color request by role.</summary>
	public SemanticColorGraphBuilder AddRequest(ColorRole role,
		AccessibilityLevel accessibility = AccessibilityLevel.AA,
		RgbColor? background = null,
		bool isLargeText = false)
	{
		return AddRequest(new SemanticColorRequest
		{
			PrimaryRole = role,
			AccessibilityRequirement = accessibility,
			BackgroundColor = background,
			IsLargeText = isLargeText,
			ImportanceWeight = role == ColorRole.Text ? 1.0f : 0.5f
		});
	}

	/// <summary>Adds a harmony relationship between two requests.</summary>
	public SemanticColorGraphBuilder AddHarmony(int fromIndex, int toIndex, float weight = 1.0f)
	{
		relationships.Add((fromIndex, toIndex, weight));
		return this;
	}

	/// <summary>Sets global constraints for the entire palette.</summary>
	public SemanticColorGraphBuilder WithGlobalConstraints(GlobalConstraints constraints)
	{
		globalConstraints = constraints;
		return this;
	}

	/// <summary>Builds the semantic color graph.</summary>
	public SemanticColorGraph Build()
	{
		return new SemanticColorGraph
		{
			Requests = [.. requests],
			Relationships = [.. relationships],
			GlobalConstraints = globalConstraints
		};
	}
}

/// <summary>
/// Result of semantic palette generation, containing the generated colors
/// and metadata about how well the requirements were satisfied.
/// </summary>
public sealed record SemanticPaletteResult
{
	/// <summary>The generated colors, indexed to match the input requests</summary>
	public required ImmutableArray<RgbColor> GeneratedColors { get; init; }

	/// <summary>How well each color satisfies its accessibility requirements (0.0 = fail, 1.0 = perfect)</summary>
	public required ImmutableArray<float> AccessibilityScores { get; init; }

	/// <summary>How well each color matches its semantic requirements (0.0 = poor, 1.0 = perfect)</summary>
	public required ImmutableArray<float> SemanticMatchScores { get; init; }

	/// <summary>Overall harmony score for the entire palette</summary>
	public required float OverallHarmonyScore { get; init; }

	/// <summary>Whether all critical accessibility requirements were met</summary>
	public bool MeetsAccessibilityRequirements => AccessibilityScores.All(score => score >= 0.8f);

	/// <summary>Warnings or notes about the generation process</summary>
	public ImmutableArray<string> Warnings { get; init; } = [];

	/// <summary>Additional metadata about the generation process</summary>
	public ImmutableDictionary<string, object> Metadata { get; init; } = ImmutableDictionary<string, object>.Empty;
}

/// <summary>
/// Main engine for generating semantic color palettes from themes.
/// This processes semantic color graphs and produces accessible, harmonious color palettes
/// that maintain theme authenticity while meeting application requirements.
/// </summary>
public sealed class SemanticPaletteEngine(ThemeDefinition theme, int? seed = null)
{
	private readonly ThemeDefinition theme = theme;
	private readonly Random random = seed.HasValue ? new Random(seed.Value) : new Random();

	/// <summary>
	/// Generates a semantic color palette from the provided graph of requirements.
	/// Uses vector mathematics to find optimal color combinations that balance
	/// theme authenticity, semantic appropriateness, and accessibility.
	/// </summary>
	public SemanticPaletteResult GeneratePalette(SemanticColorGraph graph)
	{
		ArgumentNullException.ThrowIfNull(graph);
		ImmutableArray<SemanticColorRequest> requests = graph.Requests;
		RgbColor[] generatedColors = new RgbColor[requests.Length];
		float[] accessibilityScores = new float[requests.Length];
		float[] semanticScores = new float[requests.Length];
		List<string> warnings = [];

		// Phase 1: Generate initial color candidates for each request
		List<List<ColorCandidate>> candidates = GenerateInitialCandidates(requests, warnings);

		// Phase 2: Optimize for harmony and relationships
		OptimizeForHarmony(candidates, graph, generatedColors, warnings);

		// Phase 3: Ensure accessibility requirements are met
		EnsureAccessibilityCompliance(requests, generatedColors, accessibilityScores, warnings);

		// Phase 4: Calculate semantic match scores
		CalculateSemanticScores(requests, generatedColors, semanticScores);

		// Phase 5: Calculate overall harmony
		float harmonyScore = CalculateOverallHarmony(generatedColors, graph);

		return new SemanticPaletteResult
		{
			GeneratedColors = [.. generatedColors],
			AccessibilityScores = [.. accessibilityScores],
			SemanticMatchScores = [.. semanticScores],
			OverallHarmonyScore = harmonyScore,
			Warnings = [.. warnings],
			Metadata = ImmutableDictionary<string, object>.Empty
				.Add("theme_name", theme.Name)
				.Add("generation_time", DateTime.UtcNow)
				.Add("request_count", requests.Length)
		};
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
	private List<List<ColorCandidate>> GenerateInitialCandidates(
		ImmutableArray<SemanticColorRequest> requests,
		List<string> warnings)
	{
		List<List<ColorCandidate>> candidates = [];

		foreach (SemanticColorRequest request in requests)
		{
			List<ColorCandidate> requestCandidates = [];

			// Start with exact theme matches
			if (theme.TryGetColor(request.PrimaryRole, out ColorProperties themeColor))
			{
				requestCandidates.Add(new ColorCandidate
				{
					Color = themeColor.RgbValue,
					Source = ColorSource.ThemeExact,
					SemanticDistance = 0.0f,
					ThemeAuthenticity = 1.0f
				});
			}

			// Add semantic matches from theme
			List<ColorCandidate> semanticMatches = FindSemanticMatches(request);
			requestCandidates.AddRange(semanticMatches);

			// Generate synthetic candidates if needed
			if (requestCandidates.Count < 5)
			{
				List<ColorCandidate> syntheticCandidates = GenerateSyntheticCandidates(request);
				requestCandidates.AddRange(syntheticCandidates);
			}

			// Sort by quality score
			requestCandidates.Sort((a, b) => b.QualityScore.CompareTo(a.QualityScore));

			// Keep top candidates
			candidates.Add([.. requestCandidates.Take(10)]);
		}

		return candidates;
	}

	private List<ColorCandidate> FindSemanticMatches(SemanticColorRequest request)
	{
		List<ColorCandidate> candidates = [];
		Vector4 targetSemantic = new(
			request.DesiredTemperature,
			request.DesiredEnergy,
			request.DesiredFormality,
			request.ImportanceWeight
		);

		foreach ((ColorRole role, ColorProperties color) in theme.Colors)
		{
			if (role == request.PrimaryRole)
			{
				continue; // Already handled exact match
			}

			Vector4 colorSemantic = color.ToSemanticVector();
			float distance = Vector4.Distance(targetSemantic, colorSemantic);

			candidates.Add(new ColorCandidate
			{
				Color = color.RgbValue,
				Source = ColorSource.ThemeSemantic,
				SemanticDistance = distance,
				ThemeAuthenticity = 0.8f - (distance * 0.2f),
				SourceRole = role
			});
		}

		return candidates;
	}

	private List<ColorCandidate> GenerateSyntheticCandidates(SemanticColorRequest request)
	{
		List<ColorCandidate> candidates = [];

		// Find the best theme color as a starting point
		KeyValuePair<ColorRole, ColorProperties> bestMatch = theme.Colors.MinBy(kvp =>
		{
			Vector4 targetSemantic = new(
				request.DesiredTemperature,
				request.DesiredEnergy,
				request.DesiredFormality,
				request.ImportanceWeight
			);
			return Vector4.Distance(targetSemantic, kvp.Value.ToSemanticVector());
		});

		OklabColor baseOklab = ColorMath.RgbToOklab(bestMatch.Value.RgbValue.ToLinear());

		// Generate variations
		for (int i = 0; i < 5; i++)
		{
			OklabColor variation = CreateSemanticVariation(baseOklab, request, i);
			RgbColor rgb = ColorMath.OklabToRgb(variation).ToSRgb();

			// Clamp to valid RGB range
			rgb = new RgbColor(
				Math.Clamp(rgb.R, 0f, 1f),
				Math.Clamp(rgb.G, 0f, 1f),
				Math.Clamp(rgb.B, 0f, 1f)
			);

			candidates.Add(new ColorCandidate
			{
				Color = rgb,
				Source = ColorSource.Synthetic,
				SemanticDistance = i * 0.1f,
				ThemeAuthenticity = Math.Max(0.3f, 0.7f - (i * 0.1f))
			});
		}

		return candidates;
	}

	private OklabColor CreateSemanticVariation(OklabColor baseColor, SemanticColorRequest request, int variationIndex)
	{
		float variationFactor = variationIndex * 0.1f;

		// Adjust lightness based on energy and formality
		float lightnessAdjustment = ((request.DesiredEnergy - 0.5f) * 0.2f) -
								   ((request.DesiredFormality - 0.5f) * 0.1f);

		// Adjust chroma based on energy
		(float l, float c, float h) = baseColor.ToPolar();
		float chromaAdjustment = (request.DesiredEnergy - 0.5f) * 0.3f;

		// Adjust hue based on temperature
		float hueAdjustment = request.DesiredTemperature * 30f; // ±30 degrees

		// Apply variations with some randomness
		float newL = Math.Clamp(l + lightnessAdjustment + (float)(random.NextGaussian() * 0.05), 0f, 1f);
		float newC = Math.Max(0f, c + chromaAdjustment + (float)(random.NextGaussian() * 0.02));
		float newH = (h + hueAdjustment + (float)(random.NextGaussian() * 10)) % 360f;
		if (newH < 0)
		{
			newH += 360f;
		}

		return OklabColor.FromPolar(newL, newC, newH);
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
	private void OptimizeForHarmony(
		List<List<ColorCandidate>> candidates,
		SemanticColorGraph graph,
		RgbColor[] result,
		List<string> warnings)
	{
		// Use a simple greedy optimization approach
		// In a production system, this might use more sophisticated optimization

		for (int i = 0; i < candidates.Count; i++)
		{
			ColorCandidate best = candidates[i][0]; // Start with the best candidate

			// Check if any relationships require adjustment
			foreach ((int fromIndex, int toIndex, float weight) in graph.Relationships)
			{
				if (fromIndex == i && toIndex < i) // Relationship to already selected color
				{
					RgbColor targetColor = result[toIndex];
					ColorCandidate harmonious = FindMostHarmoniousCandidate(candidates[i], targetColor, weight);
					if (harmonious.QualityScore * weight > best.QualityScore)
					{
						best = harmonious;
					}
				}
			}

			result[i] = best.Color;
		}
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
	private ColorCandidate FindMostHarmoniousCandidate(List<ColorCandidate> candidates, RgbColor targetColor, float weight)
	{
		OklabColor targetOklab = ColorMath.RgbToOklab(targetColor.ToLinear());

		return candidates
			.Select(candidate =>
			{
				OklabColor candidateOklab = ColorMath.RgbToOklab(candidate.Color.ToLinear());
				float harmonyScore = CalculateHarmonyScore(candidateOklab, targetOklab);

				return new { Candidate = candidate, HarmonyScore = (harmonyScore * weight) + (candidate.QualityScore * (1 - weight)) };
			})
			.MaxBy(x => x.HarmonyScore)!
			.Candidate;
	}

	private static float CalculateHarmonyScore(OklabColor color1, OklabColor color2)
	{
		(float l1, float c1, float h1) = color1.ToPolar();
		(float l2, float c2, float h2) = color2.ToPolar();

		// Harmonious hue relationships (complementary, triadic, analogous)
		float hueDiff = Math.Abs(h1 - h2);
		if (hueDiff > 180)
		{
			hueDiff = 360 - hueDiff;
		}

		// Prefer complementary (180°), triadic (120°, 240°), or analogous (30°) relationships
		float hueScore = 1.0f;
		if (Math.Abs(hueDiff - 180) < 15)
		{
			hueScore = 1.0f; // Complementary
		}
		else if (Math.Abs(hueDiff - 120) < 15 || Math.Abs(hueDiff - 240) < 15)
		{
			hueScore = 0.9f; // Triadic
		}
		else if (hueDiff < 30)
		{
			hueScore = 0.8f; // Analogous
		}
		else
		{
			hueScore = 0.5f - (hueDiff / 360f); // Penalty for dissonant relationships
		}

		// Similar chroma and lightness can also be harmonious
		float chromaDiff = Math.Abs(c1 - c2);
		float lightnessDiff = Math.Abs(l1 - l2);

		float chromaScore = 1.0f - (Math.Min(chromaDiff, 0.5f) * 2.0f);
		float lightnessScore = 1.0f - (Math.Min(lightnessDiff, 0.5f) * 2.0f);

		return (hueScore * 0.5f) + (chromaScore * 0.3f) + (lightnessScore * 0.2f);
	}

	private static void EnsureAccessibilityCompliance(
		ImmutableArray<SemanticColorRequest> requests,
		RgbColor[] colors,
		float[] scores,
		List<string> warnings)
	{
		for (int i = 0; i < requests.Length; i++)
		{
			SemanticColorRequest request = requests[i];
			if (request.BackgroundColor.HasValue)
			{
				AccessibilityLevel level = ColorMath.GetAccessibilityLevel(
					colors[i],
					request.BackgroundColor.Value,
					request.IsLargeText
				);

				scores[i] = level switch
				{
					AccessibilityLevel.AAA => 1.0f,
					AccessibilityLevel.AA => request.AccessibilityRequirement == AccessibilityLevel.AAA ? 0.7f : 1.0f,
					_ => 0.0f
				};

				if (level < request.AccessibilityRequirement)
				{
					// Try to adjust the color for accessibility
					RgbColor adjusted = ColorMath.AdjustForAccessibility(
						colors[i],
						request.BackgroundColor.Value,
						request.AccessibilityRequirement,
						request.IsLargeText
					);

					AccessibilityLevel adjustedLevel = ColorMath.GetAccessibilityLevel(
						adjusted,
						request.BackgroundColor.Value,
						request.IsLargeText
					);

					if (adjustedLevel >= request.AccessibilityRequirement)
					{
						colors[i] = adjusted;
						scores[i] = adjustedLevel switch
						{
							AccessibilityLevel.AAA => 1.0f,
							AccessibilityLevel.AA => 0.9f,
							_ => 0.7f
						};
					}
					else
					{
						warnings.Add($"Could not meet accessibility requirement {request.AccessibilityRequirement} for role {request.PrimaryRole}");
					}
				}
			}
			else
			{
				scores[i] = 1.0f; // No background specified, assume it's fine
			}
		}
	}

	private void CalculateSemanticScores(
		ImmutableArray<SemanticColorRequest> requests,
		RgbColor[] colors,
		float[] scores)
	{
		for (int i = 0; i < requests.Length; i++)
		{
			SemanticColorRequest request = requests[i];
			OklabColor color = ColorMath.RgbToOklab(colors[i].ToLinear());

			// Create target semantic vector
			ColorProperties targetProperties = ColorProperties.FromRgb(
				colors[i],
				request.ImportanceWeight,
				request.DesiredTemperature,
				request.DesiredEnergy,
				request.DesiredFormality
			);

			// Find the closest theme color for comparison
			if (theme.TryGetColor(request.PrimaryRole, out ColorProperties themeColor))
			{
				scores[i] = 1.0f - (targetProperties.SemanticDistanceTo(themeColor) * 0.5f);
			}
			else
			{
				(ColorRole _, ColorProperties closestColor) = theme.FindClosestColor(targetProperties);
				scores[i] = 1.0f - (targetProperties.SemanticDistanceTo(closestColor) * 0.3f);
			}

			scores[i] = Math.Clamp(scores[i], 0f, 1f);
		}
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
	private float CalculateOverallHarmony(RgbColor[] colors, SemanticColorGraph graph)
	{
		if (colors.Length < 2)
		{
			return 1.0f;
		}

		float totalHarmony = 0f;
		int comparisons = 0;

		// Calculate harmony between all color pairs
		for (int i = 0; i < colors.Length; i++)
		{
			for (int j = i + 1; j < colors.Length; j++)
			{
				OklabColor oklab1 = ColorMath.RgbToOklab(colors[i].ToLinear());
				OklabColor oklab2 = ColorMath.RgbToOklab(colors[j].ToLinear());

				float harmony = CalculateHarmonyScore(oklab1, oklab2);

				// Weight by relationship strength if it exists
				float weight = 1.0f;
				foreach ((int fromIndex, int toIndex, float relationshipWeight) in graph.Relationships)
				{
					if ((fromIndex == i && toIndex == j) || (fromIndex == j && toIndex == i))
					{
						weight = relationshipWeight;
						break;
					}
				}

				totalHarmony += harmony * weight;
				comparisons++;
			}
		}

		return comparisons > 0 ? totalHarmony / comparisons : 1.0f;
	}

	private readonly record struct ColorCandidate
	{
		public required RgbColor Color { get; init; }
		public required ColorSource Source { get; init; }
		public required float SemanticDistance { get; init; }
		public required float ThemeAuthenticity { get; init; }
		public ColorRole? SourceRole { get; init; }

		public float QualityScore => (ThemeAuthenticity * 0.6f) + ((1.0f - SemanticDistance) * 0.4f);
	}

	private enum ColorSource
	{
		ThemeExact,
		ThemeSemantic,
		Synthetic
	}
}

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

// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Semantic;
using ktsu.ThemeProvider.Core;

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

	/// <summary>Adds a simple color request by spec.</summary>
	public SemanticColorGraphBuilder AddRequest(SemanticColorSpec spec,
		AccessibilityLevel accessibility = AccessibilityLevel.AA,
		RgbColor? background = null,
		bool isLargeText = false)
	{
		return AddRequest(new SemanticColorRequest
		{
			PrimarySpec = spec,
			AccessibilityRequirement = accessibility,
			BackgroundColor = background,
			IsLargeText = isLargeText,
			ImportanceWeight = spec.Role == VisualRole.Text ? 1.0f : 0.5f
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

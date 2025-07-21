// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Semantic;
using System.Collections.Immutable;

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

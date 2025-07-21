// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.ImGui;
using System.Collections.Immutable;
using System.Numerics;
using Hexa.NET.ImGui;
using ktsu.ThemeProvider.Core;
using ktsu.ThemeProvider.Output;
using ktsu.ThemeProvider.Themes;

/// <summary>
/// Maps semantic themes to ImGui color palettes, providing comprehensive styling
/// for ImGui applications using semantic color specifications.
/// </summary>
public sealed class ImGuiPaletteMapper : IPaletteMapper<ImGuiCol, Vector4>
{
	/// <summary>
	/// Gets the framework name this mapper supports.
	/// </summary>
	public string FrameworkName => "Dear ImGui";

	/// <summary>
	/// Maps a semantic theme to a complete ImGui color palette.
	/// </summary>
	public ImmutableDictionary<ImGuiCol, Vector4> MapTheme(ThemeDefinition theme)
	{
		ArgumentNullException.ThrowIfNull(theme);

		Dictionary<ImGuiCol, Vector4> colors = [];

		// Background colors (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.WindowBg, new(SemanticMeaning.Primary, VisualRole.Background, ImportanceLevel.Low)); // Base
		AddColorIfAvailable(colors, theme, ImGuiCol.ChildBg, new(SemanticMeaning.Secondary, VisualRole.Background, ImportanceLevel.Low)); // Crust
		AddColorIfAvailable(colors, theme, ImGuiCol.PopupBg, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Low)); // Surface0
		AddColorIfAvailable(colors, theme, ImGuiCol.MenuBarBg, new(SemanticMeaning.Secondary, VisualRole.Surface, ImportanceLevel.Low)); // Overlay0

		// Text colors (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.Text, new(SemanticMeaning.Primary, VisualRole.Text, ImportanceLevel.Critical));
		AddColorIfAvailable(colors, theme, ImGuiCol.TextDisabled, new(SemanticMeaning.Secondary, VisualRole.Text, ImportanceLevel.Medium));

		// Interactive elements (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.Button, new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.Critical));
		AddColorIfAvailable(colors, theme, ImGuiCol.ButtonHovered, new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.Critical), 1.1f);
		AddColorIfAvailable(colors, theme, ImGuiCol.ButtonActive, new(SemanticMeaning.Secondary, VisualRole.Widget, ImportanceLevel.Critical), 0.9f);

		// Frame/input elements (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.FrameBg, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Medium)); // Surface1
		AddColorIfAvailable(colors, theme, ImGuiCol.FrameBgHovered, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.High)); // Surface2
		AddColorIfAvailable(colors, theme, ImGuiCol.FrameBgActive, new(SemanticMeaning.Secondary, VisualRole.Surface, ImportanceLevel.High)); // Peach

		// Headers (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.Header, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Low)); // Surface0
		AddColorIfAvailable(colors, theme, ImGuiCol.HeaderHovered, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Medium)); // Surface1
		AddColorIfAvailable(colors, theme, ImGuiCol.HeaderActive, new(SemanticMeaning.Secondary, VisualRole.Surface, ImportanceLevel.High)); // Peach

		// Scrollbars (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.ScrollbarBg, new(SemanticMeaning.Secondary, VisualRole.Surface, ImportanceLevel.Low)); // Crust
		AddColorIfAvailable(colors, theme, ImGuiCol.ScrollbarGrab, new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.Medium)); // Surface1
		AddColorIfAvailable(colors, theme, ImGuiCol.ScrollbarGrabHovered, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.High)); // Surface2
		AddColorIfAvailable(colors, theme, ImGuiCol.ScrollbarGrabActive, new(SemanticMeaning.Secondary, VisualRole.Surface, ImportanceLevel.High)); // Peach

		// Checkmarks and selections (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.CheckMark, new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.High));
		AddColorIfAvailable(colors, theme, ImGuiCol.SliderGrab, new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.High));
		AddColorIfAvailable(colors, theme, ImGuiCol.SliderGrabActive, new(SemanticMeaning.Secondary, VisualRole.Widget, ImportanceLevel.High), 1.2f);

		// Separators (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.Separator, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Medium)); // Surface1
		AddColorIfAvailable(colors, theme, ImGuiCol.SeparatorHovered, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.High)); // Surface2
		AddColorIfAvailable(colors, theme, ImGuiCol.SeparatorActive, new(SemanticMeaning.Secondary, VisualRole.Surface, ImportanceLevel.High)); // Peach

		// Tabs (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.Tab, new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.Low)); // Surface0
		AddColorIfAvailable(colors, theme, ImGuiCol.TabHovered, new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.Medium)); // Surface1
		AddColorIfAvailable(colors, theme, ImGuiCol.TabSelected, new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.High)); // Surface1

		// Plot colors (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.PlotLines, new(SemanticMeaning.Secondary, VisualRole.Widget, ImportanceLevel.Medium));
		AddColorIfAvailable(colors, theme, ImGuiCol.PlotLinesHovered, new(SemanticMeaning.Secondary, VisualRole.Widget, ImportanceLevel.Medium), 1.2f);
		AddColorIfAvailable(colors, theme, ImGuiCol.PlotHistogram, new(SemanticMeaning.Secondary, VisualRole.Widget, ImportanceLevel.High));
		AddColorIfAvailable(colors, theme, ImGuiCol.PlotHistogramHovered, new(SemanticMeaning.Secondary, VisualRole.Widget, ImportanceLevel.High), 1.2f);

		// Table colors (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.TableHeaderBg, new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.Medium)); // Surface1
		AddColorIfAvailable(colors, theme, ImGuiCol.TableBorderStrong, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.High)); // Surface2
		AddColorIfAvailable(colors, theme, ImGuiCol.TableBorderLight, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Low)); // Surface0
		AddColorIfAvailable(colors, theme, ImGuiCol.TableRowBg, new(SemanticMeaning.Primary, VisualRole.Background, ImportanceLevel.Low), alpha: 0.0f); // Base (transparent)
		AddColorIfAvailable(colors, theme, ImGuiCol.TableRowBgAlt, new(SemanticMeaning.Secondary, VisualRole.Background, ImportanceLevel.Low), alpha: 0.2f); // Crust

		// Text selection (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.TextSelectedBg, new(SemanticMeaning.Secondary, VisualRole.Surface, ImportanceLevel.Medium), alpha: 0.4f);

		// Title bars (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.TitleBg, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Medium)); // Surface0
		AddColorIfAvailable(colors, theme, ImGuiCol.TitleBgActive, new(SemanticMeaning.Primary, VisualRole.Widget, ImportanceLevel.Medium)); // Surface1
		AddColorIfAvailable(colors, theme, ImGuiCol.TitleBgCollapsed, new(SemanticMeaning.Secondary, VisualRole.Surface, ImportanceLevel.Low)); // Crust

		// Borders (using structural colors only)
		AddColorIfAvailable(colors, theme, ImGuiCol.Border, new(SemanticMeaning.Primary, VisualRole.Surface, ImportanceLevel.Medium), alpha: 0.5f); // Surface1
		AddColorIfAvailable(colors, theme, ImGuiCol.BorderShadow, new(SemanticMeaning.Secondary, VisualRole.Background, ImportanceLevel.Low), alpha: 0.0f); // Crust (invisible)

		// Fill in any missing colors with defaults
		FillMissingColors(colors);

		return colors.ToImmutableDictionary();
	}

	/// <summary>
	/// Gets the default color for a specific ImGui color when no semantic mapping is available.
	/// </summary>
	public Vector4 GetDefaultColor(ImGuiCol colorKey) => colorKey switch
	{
		// Basic grayscale defaults for dark theme
		ImGuiCol.Text => new Vector4(1.00f, 1.00f, 1.00f, 1.00f),
		ImGuiCol.TextDisabled => new Vector4(0.50f, 0.50f, 0.50f, 1.00f),
		ImGuiCol.WindowBg => new Vector4(0.06f, 0.06f, 0.06f, 0.94f),
		ImGuiCol.ChildBg => new Vector4(0.00f, 0.00f, 0.00f, 0.00f),
		ImGuiCol.PopupBg => new Vector4(0.08f, 0.08f, 0.08f, 0.94f),
		ImGuiCol.Border => new Vector4(0.43f, 0.43f, 0.50f, 0.50f),
		ImGuiCol.BorderShadow => new Vector4(0.00f, 0.00f, 0.00f, 0.00f),
		ImGuiCol.FrameBg => new Vector4(0.16f, 0.16f, 0.16f, 0.54f),
		ImGuiCol.FrameBgHovered => new Vector4(0.26f, 0.26f, 0.26f, 0.40f),
		ImGuiCol.FrameBgActive => new Vector4(0.26f, 0.26f, 0.26f, 0.67f),
		ImGuiCol.Button => new Vector4(0.26f, 0.59f, 0.98f, 0.40f),
		ImGuiCol.ButtonHovered => new Vector4(0.26f, 0.59f, 0.98f, 1.00f),
		ImGuiCol.ButtonActive => new Vector4(0.06f, 0.53f, 0.98f, 1.00f),
		_ => new Vector4(0.50f, 0.50f, 0.50f, 1.00f) // Generic fallback
	};

	/// <summary>
	/// Gets metadata about the mapping process.
	/// </summary>
	public ImmutableDictionary<string, object> GetMappingMetadata(ThemeDefinition theme)
	{
		ArgumentNullException.ThrowIfNull(theme);

		return ImmutableDictionary<string, object>.Empty
			.Add("framework", FrameworkName)
			.Add("theme_name", theme.Name)
			.Add("theme_type", theme.IsDarkTheme ? "Dark" : "Light")
			.Add("mapped_colors_count", MapTheme(theme).Count)
			.Add("generation_time", DateTime.UtcNow);
	}

	private static void AddColorIfAvailable(Dictionary<ImGuiCol, Vector4> colors, ThemeDefinition theme,
		ImGuiCol imguiColor, SemanticColorSpec spec, float brightnessFactor = 1.0f, float alpha = 1.0f)
	{
		if (theme.TryGetColor(spec, out ColorProperties color))
		{
			RgbColor rgbColor = brightnessFactor != 1.0f ?
				AdjustBrightness(color.RgbValue, brightnessFactor) :
				color.RgbValue;

			colors[imguiColor] = new Vector4(rgbColor.R, rgbColor.G, rgbColor.B, alpha);
		}
	}

	private void FillMissingColors(Dictionary<ImGuiCol, Vector4> colors)
	{
		// Fill in any ImGui colors that weren't mapped with defaults
		// Use a reasonable upper bound for ImGui color indices (typically < 100)
		const int MaxImGuiColorIndex = 100;

		foreach (ImGuiCol colorKey in Enum.GetValues<ImGuiCol>())
		{
			// Only add colors that have valid indices and aren't already present
			int colorIndex = (int)colorKey;
			if (colorIndex >= 0 && colorIndex < MaxImGuiColorIndex && !colors.ContainsKey(colorKey))
			{
				colors[colorKey] = GetDefaultColor(colorKey);
			}
		}
	}

	private static RgbColor AdjustBrightness(RgbColor color, float factor) =>
		new(
			Math.Clamp(color.R * factor, 0f, 1f),
			Math.Clamp(color.G * factor, 0f, 1f),
			Math.Clamp(color.B * factor, 0f, 1f)
		);
}

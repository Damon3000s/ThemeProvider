// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.ImGui;
using System.Collections.Immutable;
using System.Numerics;
using Hexa.NET.ImGui;
using ktsu.ThemeProvider;

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
	public ImmutableDictionary<ImGuiCol, Vector4> MapTheme(ISemanticTheme theme)
	{
		ArgumentNullException.ThrowIfNull(theme);

		// Get the complete palette for the theme (more efficient than individual requests)
		ImmutableDictionary<SemanticColorRequest, PerceptualColor> completePalette = SemanticColorMapper.GetCompletePalette(theme);

		// Define the mapping from ImGui colors to semantic color requests
		Dictionary<ImGuiCol, SemanticColorRequest> colorMapping = new()
		{
			{ ImGuiCol.WindowBg, new(SemanticMeaning.Neutral, Priority.VeryLow)},
			{ ImGuiCol.ChildBg, new(SemanticMeaning.Neutral, Priority.Low)},
			{ ImGuiCol.PopupBg, new(SemanticMeaning.Neutral, Priority.Low)},
			{ ImGuiCol.MenuBarBg, new(SemanticMeaning.Neutral, Priority.Low)},
			{ ImGuiCol.FrameBg, new(SemanticMeaning.Neutral, Priority.MediumLow)},
			{ ImGuiCol.FrameBgHovered, new(SemanticMeaning.Neutral, Priority.Medium)},
			{ ImGuiCol.FrameBgActive, new(SemanticMeaning.Neutral, Priority.MediumHigh)},
			{ ImGuiCol.TextDisabled, new(SemanticMeaning.Neutral, Priority.High)},
			{ ImGuiCol.Text, new(SemanticMeaning.Neutral, Priority.VeryHigh)},

			{ ImGuiCol.ScrollbarBg, new(SemanticMeaning.Neutral, Priority.Low)},
			{ ImGuiCol.ScrollbarGrab, new(SemanticMeaning.Neutral, Priority.Medium)},
			{ ImGuiCol.ScrollbarGrabHovered, new(SemanticMeaning.Neutral, Priority.MediumHigh)},
			{ ImGuiCol.ScrollbarGrabActive, new(SemanticMeaning.Neutral, Priority.High)},

			// Primary elements now use more spread out priorities to maximize contrast within the 50-90% range
			{ ImGuiCol.Button, new(SemanticMeaning.Primary, Priority.Medium)},
			{ ImGuiCol.ButtonHovered, new(SemanticMeaning.Primary, Priority.High)},
			{ ImGuiCol.ButtonActive, new(SemanticMeaning.Primary, Priority.VeryHigh)},

			{ ImGuiCol.Header, new(SemanticMeaning.Primary, Priority.Medium)},
			{ ImGuiCol.HeaderHovered, new(SemanticMeaning.Primary, Priority.High)},
			{ ImGuiCol.HeaderActive, new(SemanticMeaning.Primary, Priority.VeryHigh)},

			{ ImGuiCol.CheckMark, new(SemanticMeaning.Primary, Priority.High)},

			{ ImGuiCol.ResizeGrip, new(SemanticMeaning.Neutral, Priority.MediumHigh)},
			{ ImGuiCol.ResizeGripHovered, new(SemanticMeaning.Neutral, Priority.High)},
			{ ImGuiCol.ResizeGripActive, new(SemanticMeaning.Neutral, Priority.VeryHigh)},

			{ ImGuiCol.NavWindowingHighlight, new(SemanticMeaning.Primary, Priority.VeryHigh)},

			{ ImGuiCol.SliderGrab, new(SemanticMeaning.Primary, Priority.MediumLow)},
			{ ImGuiCol.SliderGrabActive, new(SemanticMeaning.Primary, Priority.High)},

			{ ImGuiCol.Separator, new(SemanticMeaning.Neutral, Priority.MediumHigh)},
			{ ImGuiCol.SeparatorHovered, new(SemanticMeaning.Neutral, Priority.High)},
			{ ImGuiCol.SeparatorActive, new(SemanticMeaning.Neutral, Priority.VeryHigh)},

			{ ImGuiCol.Tab, new(SemanticMeaning.Neutral, Priority.Medium)},
			{ ImGuiCol.TabSelected, new(SemanticMeaning.Primary, Priority.Medium)},
			{ ImGuiCol.TabHovered, new(SemanticMeaning.Primary, Priority.High)},

			// Alternate elements spread across the full 50-90% range for better contrast
			{ ImGuiCol.PlotLines, new(SemanticMeaning.Alternate, Priority.Medium)},
			{ ImGuiCol.PlotLinesHovered, new(SemanticMeaning.Alternate, Priority.High)},

			{ ImGuiCol.PlotHistogram, new(SemanticMeaning.Alternate, Priority.Medium)},
			{ ImGuiCol.PlotHistogramHovered, new(SemanticMeaning.Alternate, Priority.High)},

			{ ImGuiCol.TableHeaderBg, new(SemanticMeaning.Neutral, Priority.Medium)},
			{ ImGuiCol.TableBorderStrong, new(SemanticMeaning.Neutral, Priority.Medium)},
			{ ImGuiCol.TableBorderLight, new(SemanticMeaning.Neutral, Priority.Medium)},
			{ ImGuiCol.TableRowBg, new(SemanticMeaning.Neutral, Priority.VeryLow) },
			{ ImGuiCol.TableRowBgAlt, new(SemanticMeaning.Neutral, Priority.Low) },

			{ ImGuiCol.TextSelectedBg, new(SemanticMeaning.Alternate, Priority.High) },

			{ ImGuiCol.TitleBg, new(SemanticMeaning.Neutral, Priority.Medium)},
			{ ImGuiCol.TitleBgActive, new(SemanticMeaning.Primary, Priority.Medium)},
			{ ImGuiCol.TitleBgCollapsed, new(SemanticMeaning.Neutral, Priority.MediumLow)},

			{ ImGuiCol.Border, new(SemanticMeaning.Neutral, Priority.Medium) },
			{ ImGuiCol.BorderShadow, new(SemanticMeaning.Neutral, Priority.Low) },
		};

		// Convert the semantic colors to ImGui Vector4 format
		Dictionary<ImGuiCol, Vector4> result = [];
		foreach ((ImGuiCol imguiCol, SemanticColorRequest request) in colorMapping)
		{
			if (completePalette.TryGetValue(request, out PerceptualColor color))
			{
				RgbColor rgb = color.RgbValue;
				result[imguiCol] = new Vector4(rgb.R, rgb.G, rgb.B, 1.0f);
			}
		}

		return result.ToImmutableDictionary();
	}
}

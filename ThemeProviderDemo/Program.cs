// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProviderDemo;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Numerics;
using Hexa.NET.ImGui;
using ktsu.ImGuiApp;
using ktsu.ThemeProvider;
using ktsu.ThemeProvider.ImGui;
using ktsu.ThemeProvider.Themes.Catppuccin;

internal static class Program
{
	private static ISemanticTheme theme = null!;
	private static ImGuiPaletteMapper imguiMapper = null!;

	// Theme selection
	private static readonly ISemanticTheme[] availableThemes = [new Latte(), new Frappe(), new Macchiato(), new Mocha()];
	private static readonly string[] themeNames = ["Latte (Light)", "Frappe (Dark)", "Macchiato (Dark)", "Mocha (Darkest)"];
	private static int selectedThemeIndex;

	// UI State
	private static int selectedSemanticMeaning = (int)SemanticMeaning.Primary;
	private static int selectedPriority = (int)Priority.Medium;
	private static Vector3 selectedColorVec = Vector3.Zero;
	private static Vector3 backgroundColorVec = new(0.1f, 0.1f, 0.1f);
	private static bool isLargeText;

	// Form state
	private static float sliderValue = 0.5f;
	private static bool checkboxValue;
	private static string textValue = "Text Input";

	// Cache for complete semantic mappings
	private static SemanticMeaning? cachedMeaning;
	private static ImmutableDictionary<SemanticColorRequest, PerceptualColor>? cachedMapping;

	private static void Main()
	{
		ImGuiApp.Start(new()
		{
			Title = "ThemeProvider Demo - Semantic Color System",
			OnRender = OnRender,
			OnStart = OnStart,
			SaveIniSettings = false,
		});
	}

	private static void OnStart()
	{
		selectedThemeIndex = 3; // Start with Mocha (darkest)
		theme = availableThemes[selectedThemeIndex];
		imguiMapper = new ImGuiPaletteMapper();

		// Initialize with theme's primary color
		SemanticColorRequest primaryRequest = new(SemanticMeaning.Primary, Priority.MediumHigh);
		PerceptualColor primaryColor = GetColorFromTheme(primaryRequest);
		if (primaryColor != default)
		{
			selectedColorVec = new Vector3(primaryColor.RgbValue.R, primaryColor.RgbValue.G, primaryColor.RgbValue.B);
		}
	}

	private static void OnRender(float deltaTime)
	{
		ApplyTheme();

		if (ImGui.BeginTabBar("DemoTabs"))
		{
			if (ImGui.BeginTabItem("Theme Overview"))
			{
				RenderThemeOverview();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Semantic Colors"))
			{
				RenderSemanticColors();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("ImGui Mapping"))
			{
				RenderImGuiMapping();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Accessibility"))
			{
				RenderAccessibilityDemo();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("UI Preview"))
			{
				RenderUIPreview();
				ImGui.EndTabItem();
			}

			ImGui.EndTabBar();
		}
	}

	private static void ApplyTheme()
	{
		// Use the ImGui palette mapper system
		ImmutableDictionary<ImGuiCol, Vector4> imguiColors = imguiMapper.MapTheme(theme);

		ImGuiStylePtr style = ImGui.GetStyle();
		Span<Vector4> colors = style.Colors;

		// Apply all mapped colors with bounds checking
		foreach ((ImGuiCol colorKey, Vector4 colorValue) in imguiColors)
		{
			int colorIndex = (int)colorKey;
			if (colorIndex >= 0 && colorIndex < colors.Length)
			{
				colors[colorIndex] = colorValue;
			}
		}
	}

	private static void RenderThemeOverview()
	{
		// Theme selection
		ImGui.TextUnformatted("Select Catppuccin Variant:");
		if (ImGui.Combo("##ThemeVariant", ref selectedThemeIndex, themeNames, themeNames.Length))
		{
			theme = availableThemes[selectedThemeIndex];
			// Clear cache when theme changes
			cachedMeaning = null;
			cachedMapping = null;
		}

		ImGui.TextUnformatted($"Semantic Color Grid - Current Theme: {themeNames[selectedThemeIndex]}");
		ImGui.Separator();

		// Get all semantic meanings and priorities
		SemanticMeaning[] semanticMeanings = Enum.GetValues<SemanticMeaning>();
		Priority[] sortedPriorities = [Priority.VeryLow, Priority.Low, Priority.MediumLow, Priority.Medium, Priority.MediumHigh, Priority.High, Priority.VeryHigh];

		// Create a single table showing all semantics and priorities in a grid
		if (ImGui.BeginTable("ColorGrid", sortedPriorities.Length + 1, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
		{
			// Setup columns - first column for semantic names, rest for priorities
			ImGui.TableSetupColumn("Semantic", ImGuiTableColumnFlags.WidthFixed, 100);
			foreach (Priority priority in sortedPriorities)
			{
				ImGui.TableSetupColumn(priority.ToString(), ImGuiTableColumnFlags.WidthFixed, 80);
			}
			ImGui.TableHeadersRow();

			// Create a row for each semantic meaning
			foreach (SemanticMeaning meaning in semanticMeanings)
			{
				ImGui.TableNextRow();

				// First column: semantic meaning name
				ImGui.TableSetColumnIndex(0);
				ImGui.TextUnformatted(meaning.ToString());

				// Rest of columns: color swatches for each priority
				for (int i = 0; i < sortedPriorities.Length; i++)
				{
					ImGui.TableSetColumnIndex(i + 1);
					Priority priority = sortedPriorities[i];
					SemanticColorRequest request = new(meaning, priority);
					PerceptualColor color = GetColorFromTheme(request);

					if (color != default)
					{
						Vector4 colorVec = new(color.RgbValue.R, color.RgbValue.G, color.RgbValue.B, 1.0f);
						Vector2 swatchSize = new(70, 30);

						if (ImGui.ColorButton($"##{meaning}_{priority}", colorVec, ImGuiColorEditFlags.NoTooltip, swatchSize))
						{
							// Optional: could copy color to clipboard or do something on click
						}

						if (ImGui.IsItemHovered())
						{
							ImGui.SetTooltip($"{meaning} - {priority}\nLightness: {color.Lightness:F2}\nRGB: {color.RgbValue.R:F3}, {color.RgbValue.G:F3}, {color.RgbValue.B:F3}\nHex: {color.RgbValue.ToHex()}");
						}
					}
				}
			}

			ImGui.EndTable();
		}

		ImGui.Spacing();
		ImGui.TextUnformatted("Note: Neutral uses full range, non-neutral uses 50-90% of neutral range");
		ImGui.TextUnformatted($"Theme Type: {(theme.IsDarkTheme ? "Dark" : "Light")} - Priority ordering {(theme.IsDarkTheme ? "low to high" : "high to low")} lightness");
	}

	private static void RenderSemanticColors()
	{
		ImGui.TextUnformatted("Explore the semantic color system:");
		ImGui.Separator();

		// Semantic color request builder
		ImGui.TextUnformatted("Build a Semantic Color Request:");

		string[] meaningNames = Enum.GetNames<SemanticMeaning>();
		ImGui.Combo("Semantic Meaning", ref selectedSemanticMeaning, meaningNames, meaningNames.Length);

		string[] priorityNames = Enum.GetNames<Priority>();
		ImGui.Combo("Priority Level", ref selectedPriority, priorityNames, priorityNames.Length);

		// Build the current request
		SemanticColorRequest currentRequest = new(
			(SemanticMeaning)selectedSemanticMeaning,
			(Priority)selectedPriority
		);

		ImGui.Separator();
		ImGui.TextUnformatted($"Current Request: {currentRequest}");

		// Get the color using the semantic color mapper
		PerceptualColor mappedColor = GetColorFromTheme(currentRequest);

		if (mappedColor != default)
		{
			// Color preview
			float previewSize = 150f;
			ImDrawListPtr drawList = ImGui.GetWindowDrawList();
			Vector2 pos = ImGui.GetCursorScreenPos();
			Vector4 rgbColor = ToImVec4(mappedColor.RgbValue);
			drawList.AddRectFilled(pos, new Vector2(pos.X + previewSize, pos.Y + previewSize),
								   ImGui.ColorConvertFloat4ToU32(rgbColor));
			drawList.AddRect(pos, new Vector2(pos.X + previewSize, pos.Y + previewSize),
							ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.5f)));
			ImGui.Dummy(new Vector2(previewSize, previewSize));

			ImGui.SameLine();
			ImGui.BeginGroup();

			ImGui.TextUnformatted("Mapped Color Properties:");
			ImGui.TextUnformatted($"Hex: {mappedColor.RgbValue.ToHex()}");
			ImGui.TextUnformatted($"RGB: ({mappedColor.RgbValue.R:F3}, {mappedColor.RgbValue.G:F3}, {mappedColor.RgbValue.B:F3})");
			ImGui.TextUnformatted($"Lightness: {mappedColor.Lightness:F2}");
			ImGui.TextUnformatted($"Chroma: {mappedColor.Chroma:F2}");
			ImGui.TextUnformatted($"Hue: {mappedColor.Hue:F2}");

			ImGui.EndGroup();
		}
		else
		{
			ImGui.TextUnformatted("No color available for this semantic meaning.");
		}

		// Show interpolation/extrapolation demonstration
		ImGui.Separator();
		ImGui.TextUnformatted("Priority-Based Lightness Mapping Demonstration:");

		if (ImGui.CollapsingHeader("Lightness-Based Priority System", ImGuiTreeNodeFlags.DefaultOpen))
		{
			// Show how all priorities map to lightness values
			Priority[] allPriorities = Enum.GetValues<Priority>();
			SemanticMeaning currentMeaning = (SemanticMeaning)selectedSemanticMeaning;

			// Get original colors available for this semantic
			if (theme.SemanticMapping.TryGetValue(currentMeaning, out Collection<PerceptualColor>? availableColors))
			{
				ImGui.TextUnformatted($"Original colors for {currentMeaning}: {availableColors.Count}");

				// Show original color lightness values
				ImGui.Indent();
				foreach (PerceptualColor originalColor in availableColors)
				{
					ImGui.TextColored(ToImVec4(originalColor.RgbValue), $"• {originalColor.RgbValue.ToHex()} (L: {originalColor.Lightness:F2})");
				}
				ImGui.Unindent();

				ImGui.Separator();

				// Show what each priority maps to
				ImGui.TextUnformatted("Priority → Mapped Lightness (Interpolation/Extrapolation):");

				// Get complete mapping for this semantic meaning (more efficient than individual requests)
				ImmutableDictionary<SemanticColorRequest, PerceptualColor> completeMapping = GetCompleteMappingForSemantic(currentMeaning);

				if (ImGui.BeginTable("PriorityMappingTable", 5, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
				{
					ImGui.TableSetupColumn("Priority");
					ImGui.TableSetupColumn("Target L");
					ImGui.TableSetupColumn("Actual L");
					ImGui.TableSetupColumn("Color");
					ImGui.TableSetupColumn("Method");
					ImGui.TableHeadersRow();

					foreach (Priority priority in allPriorities)
					{
						SemanticColorRequest request = new(currentMeaning, priority);
						if (completeMapping.TryGetValue(request, out PerceptualColor color))
						{
							ImGui.TableNextRow();

							ImGui.TableSetColumnIndex(0);
							ImGui.TextUnformatted(priority.ToString());

							ImGui.TableSetColumnIndex(1);
							// Calculate what the target lightness would be for this priority
							float targetLightness = CalculateTargetLightnessForPriority(priority);
							ImGui.TextUnformatted($"{targetLightness:F2}");

							ImGui.TableSetColumnIndex(2);
							ImGui.TextUnformatted($"{color.Lightness:F2}");

							ImGui.TableSetColumnIndex(3);
							// Small color swatch
							ImDrawListPtr tableDrawList = ImGui.GetWindowDrawList();
							Vector2 tablePos = ImGui.GetCursorScreenPos();
							float swatchSize = 15f;
							tableDrawList.AddRectFilled(tablePos, new Vector2(tablePos.X + swatchSize, tablePos.Y + swatchSize),
														ImGui.ColorConvertFloat4ToU32(ToImVec4(color.RgbValue)));
							ImGui.Dummy(new Vector2(swatchSize, swatchSize));

							ImGui.TableSetColumnIndex(4);
							// Determine if this was interpolated or extrapolated
							string method = DetermineMethod(availableColors, color);
							ImGui.TextUnformatted(method);
						}
					}

					ImGui.EndTable();
				}
			}
		}
	}

	private static float CalculateTargetLightnessForPriority(Priority priority)
	{
		// Calculate the global lightness range
		float minLightness = float.MaxValue;
		float maxLightness = float.MinValue;

		foreach (Collection<PerceptualColor> colors in theme.SemanticMapping.Values)
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

		// Get all priorities and find the position of the current priority
		Priority[] allPriorities = Enum.GetValues<Priority>();
		int priorityIndex = Array.IndexOf(allPriorities, priority);

		if (allPriorities.Length == 1)
		{
			return (minLightness + maxLightness) / 2.0f;
		}

		float position = priorityIndex / (float)(allPriorities.Length - 1);
		float lightnessRange = maxLightness - minLightness;

		if (theme.IsDarkTheme)
		{
			return minLightness + (position * lightnessRange);
		}
		else
		{
			return maxLightness - (position * lightnessRange);
		}
	}

	private static string DetermineMethod(Collection<PerceptualColor> availableColors, PerceptualColor resultColor)
	{
		if (availableColors.Count == 1)
		{
			return availableColors.First().Lightness == resultColor.Lightness ? "Original" : "Extrapolated";
		}

		List<PerceptualColor> sortedColors = [.. availableColors.OrderBy(c => c.Lightness)];
		float minL = sortedColors.First().Lightness;
		float maxL = sortedColors.Last().Lightness;

		if (resultColor.Lightness < minL - 0.01f || resultColor.Lightness > maxL + 0.01f)
		{
			return "Extrapolated";
		}
		else if (availableColors.Any(c => Math.Abs(c.Lightness - resultColor.Lightness) < 0.01f))
		{
			return "Original";
		}
		else
		{
			return "Interpolated";
		}
	}

	private static void RenderImGuiMapping()
	{
		ImGui.TextUnformatted("Systematic ImGui palette mapping from semantic theme:");
		ImGui.Separator();

		// Show mapper info
		ImGui.TextUnformatted($"Framework: {imguiMapper.FrameworkName}");

		// Get the mapped colors
		ImmutableDictionary<ImGuiCol, Vector4> mappedColors = imguiMapper.MapTheme(theme);

		ImGui.TextUnformatted($"Total Mapped Colors: {mappedColors.Count}");

		ImGui.Separator();
		ImGui.TextUnformatted("Complete ImGui Color Mapping:");

		// Show all mapped colors in a table
		if (ImGui.BeginTable("ImGuiColorTable", 3, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
		{
			ImGui.TableSetupColumn("ImGui Color");
			ImGui.TableSetupColumn("Color Preview");
			ImGui.TableSetupColumn("RGB Values");
			ImGui.TableHeadersRow();

			foreach ((ImGuiCol colorKey, Vector4 colorValue) in mappedColors.OrderBy(kvp => kvp.Key.ToString()))
			{
				ImGui.TableNextRow();

				ImGui.TableSetColumnIndex(0);
				ImGui.TextUnformatted(colorKey.ToString());

				ImGui.TableSetColumnIndex(1);
				// Color swatch
				ImDrawListPtr drawList = ImGui.GetWindowDrawList();
				Vector2 pos = ImGui.GetCursorScreenPos();
				float swatchSize = 20f;
				drawList.AddRectFilled(pos, new Vector2(pos.X + swatchSize, pos.Y + swatchSize),
									   ImGui.ColorConvertFloat4ToU32(colorValue));
				drawList.AddRect(pos, new Vector2(pos.X + swatchSize, pos.Y + swatchSize),
								ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.3f)));
				ImGui.Dummy(new Vector2(swatchSize, swatchSize));

				ImGui.TableSetColumnIndex(2);
				ImGui.TextUnformatted($"({colorValue.X:F3}, {colorValue.Y:F3}, {colorValue.Z:F3}, {colorValue.W:F3})");
			}

			ImGui.EndTable();
		}

		ImGui.Separator();
		ImGui.TextUnformatted("Benefits of Systematic Palette Mapping:");
		ImGui.BulletText("Consistent semantic color usage across all ImGui elements");
		ImGui.BulletText("Automatic mapping from semantic meanings to ImGui colors");
		ImGui.BulletText("Easy to adapt to other UI frameworks using the same pattern");
		ImGui.BulletText("Priority-based color interpolation within semantic ranges");
		ImGui.BulletText("Theme-aware color ordering (light vs dark themes)");

		ImGui.Separator();
		ImGui.TextUnformatted("Framework Integration Example:");
		ImGui.Text("// Apply complete theme with one line:\nvar colors = mapper.MapTheme(theme);\n\n// Instead of manually mapping dozens of colors:\n// colors[ImGuiCol.Text] = GetColor(...);\n// colors[ImGuiCol.Button] = GetColor(...);\n// ... (50+ more lines)");
	}

	private static void RenderAccessibilityDemo()
	{
		ImGui.TextUnformatted("Test color combinations for WCAG accessibility compliance:");
		ImGui.Separator();

		// Color pickers
		ImGui.TextUnformatted("Foreground Color:");
		ImGui.ColorEdit3("##Foreground", ref selectedColorVec);

		ImGui.TextUnformatted("Background Color:");
		ImGui.ColorEdit3("##Background", ref backgroundColorVec);

		ImGui.Checkbox("Large Text (18pt+ or 14pt+ bold)", ref isLargeText);

		// Convert to RgbColor
		RgbColor foregroundColor = new(selectedColorVec.X, selectedColorVec.Y, selectedColorVec.Z);
		RgbColor backgroundColor = new(backgroundColorVec.X, backgroundColorVec.Y, backgroundColorVec.Z);

		// Calculate accessibility metrics
		float contrastRatio = ColorMath.GetContrastRatio(foregroundColor, backgroundColor);
		AccessibilityLevel accessibilityLevel = ColorMath.GetAccessibilityLevel(foregroundColor, backgroundColor, isLargeText);

		ImGui.Separator();

		// Results
		ImGui.TextUnformatted($"Contrast Ratio: {contrastRatio:F2}:1");

		// Color-coded accessibility level
		Vector4 levelColor = accessibilityLevel switch
		{
			AccessibilityLevel.AAA => new Vector4(0, 1, 0, 1), // Green
			AccessibilityLevel.AA => new Vector4(1, 1, 0, 1),  // Yellow
			_ => new Vector4(1, 0, 0, 1) // Red
		};

		ImGui.TextColored(levelColor, $"Accessibility Level: {accessibilityLevel}");

		// Requirements
		float aaRequired = isLargeText ? 3.0f : 4.5f;
		float aaaRequired = isLargeText ? 4.5f : 7.0f;

		ImGui.TextUnformatted($"Required for AA: {aaRequired:F1}:1 {(contrastRatio >= aaRequired ? "✓" : "✗")}");
		ImGui.TextUnformatted($"Required for AAA: {aaaRequired:F1}:1 {(contrastRatio >= aaaRequired ? "✓" : "✗")}");

		// Preview text
		ImGui.Separator();
		ImGui.TextUnformatted("Preview:");

		ImDrawListPtr drawList = ImGui.GetWindowDrawList();
		Vector2 pos = ImGui.GetCursorScreenPos();
		Vector2 previewSize = new(400, 100);

		// Background
		drawList.AddRectFilled(pos, new Vector2(pos.X + previewSize.X, pos.Y + previewSize.Y),
							   ImGui.ColorConvertFloat4ToU32(new Vector4(backgroundColorVec, 1)));

		// Text
		ImGui.SetCursorScreenPos(new Vector2(pos.X + 10, pos.Y + 10));
		ImGui.TextColored(new Vector4(selectedColorVec, 1), isLargeText ? "Large Text Sample (18pt+)" : "Normal Text Sample");

		ImGui.Dummy(previewSize);
	}

	private static void RenderUIPreview()
	{
		ImGui.TextUnformatted("Live preview of semantic theme applied to various UI elements:");
		ImGui.Separator();

		// Semantic buttons
		ImGui.TextUnformatted("Semantic Buttons:");

		if (ImGui.Button("Primary"))
		{
			// Action
		}
		ImGui.SameLine();

		PerceptualColor alternateColor = GetColorFromTheme(new(SemanticMeaning.Alternate, Priority.MediumHigh));
		if (alternateColor != default)
		{
			RgbColor buttonTextColor = GetContrastingTextColor(alternateColor.RgbValue);
			ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(alternateColor.RgbValue));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(AdjustBrightness(alternateColor.RgbValue, 1.1f)));
			if (ImGui.Button("Alternate"))
			{
				// Action
			}
			ImGui.PopStyleColor(2);
			ImGui.SameLine();
		}

		PerceptualColor ctaColor = GetColorFromTheme(new(SemanticMeaning.CallToAction, Priority.MediumHigh));
		if (ctaColor != default)
		{
			RgbColor buttonTextColor = GetContrastingTextColor(ctaColor.RgbValue);
			ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(ctaColor.RgbValue));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(AdjustBrightness(ctaColor.RgbValue, 1.1f)));
			if (ImGui.Button("Call-to-Action"))
			{
				// Action
			}
			ImGui.PopStyleColor(2);
			ImGui.SameLine();
		}

		PerceptualColor cautionColor = GetColorFromTheme(new(SemanticMeaning.Caution, Priority.MediumHigh));
		if (cautionColor != default)
		{
			RgbColor buttonTextColor = GetContrastingTextColor(cautionColor.RgbValue);
			ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(cautionColor.RgbValue));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(AdjustBrightness(cautionColor.RgbValue, 1.1f)));
			if (ImGui.Button("Caution"))
			{
				// Action
			}
			ImGui.PopStyleColor(2);
		}

		ImGui.Separator();

		// Form elements
		ImGui.TextUnformatted("Form Elements:");
		ImGui.SliderFloat("Slider", ref sliderValue, 0f, 1f);
		ImGui.Checkbox("Checkbox", ref checkboxValue);
		ImGui.InputText("Input", ref textValue, 100);

		ImGui.Separator();

		// Progress bars with semantic colors
		ImGui.TextUnformatted("Progress Indicators:");

		PerceptualColor successWidget = GetColorFromTheme(new(SemanticMeaning.Success, Priority.High));
		if (successWidget != default)
		{
			ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(successWidget.RgbValue));
			ImGui.ProgressBar(0.7f, new Vector2(0, 0), "70% Complete");
			ImGui.PopStyleColor();
		}

		PerceptualColor warningWidget = GetColorFromTheme(new(SemanticMeaning.Warning, Priority.High));
		if (warningWidget != default)
		{
			ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(warningWidget.RgbValue));
			ImGui.ProgressBar(0.4f, new Vector2(0, 0), "40% Warning");
			ImGui.PopStyleColor();
		}

		PerceptualColor errorWidget = GetColorFromTheme(new(SemanticMeaning.Error, Priority.High));
		if (errorWidget != default)
		{
			ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(errorWidget.RgbValue));
			ImGui.ProgressBar(0.2f, new Vector2(0, 0), "20% Critical");
			ImGui.PopStyleColor();
		}

		ImGui.Separator();

		// Semantic text
		ImGui.TextUnformatted("Semantic Text:");

		PerceptualColor successText = GetColorFromTheme(new(SemanticMeaning.Success, Priority.VeryHigh));
		if (successText != default)
		{
			ImGui.TextColored(ToImVec4(successText.RgbValue), "Success: Operation completed successfully");
		}

		PerceptualColor warningText = GetColorFromTheme(new(SemanticMeaning.Warning, Priority.VeryHigh));
		if (warningText != default)
		{
			ImGui.TextColored(ToImVec4(warningText.RgbValue), "Warning: Please review your input");
		}

		PerceptualColor errorText = GetColorFromTheme(new(SemanticMeaning.Error, Priority.VeryHigh));
		if (errorText != default)
		{
			ImGui.TextColored(ToImVec4(errorText.RgbValue), "Error: Operation failed");
		}

		PerceptualColor infoText = GetColorFromTheme(new(SemanticMeaning.Information, Priority.VeryHigh));
		if (infoText != default)
		{
			ImGui.TextColored(ToImVec4(infoText.RgbValue), "Information: Additional details available");
		}
	}

	private static PerceptualColor GetColorFromTheme(SemanticColorRequest request)
	{
		// Check if we have cached mapping for this semantic meaning
		if (cachedMeaning != request.Meaning || cachedMapping == null)
		{
			// Generate complete mapping for all priorities of this semantic meaning
			List<SemanticColorRequest> requests = [request];
			cachedMapping = SemanticColorMapper.MapColors(requests, theme);
			cachedMeaning = request.Meaning;
		}

		return cachedMapping.TryGetValue(request, out PerceptualColor color) ? color : default;
	}

	private static ImmutableDictionary<SemanticColorRequest, PerceptualColor> GetCompleteMappingForSemantic(SemanticMeaning meaning)
	{
		// Check if we have cached mapping for this semantic meaning
		if (cachedMeaning != meaning || cachedMapping == null)
		{
			// Generate complete mapping for all priorities of this semantic meaning
			List<SemanticColorRequest> requests = [new(meaning, Priority.Medium)]; // Just need one request to trigger full mapping
			cachedMapping = SemanticColorMapper.MapColors(requests, theme);
			cachedMeaning = meaning;
		}

		return cachedMapping;
	}

	private static Vector4 ToImVec4(RgbColor color, float alpha = 1.0f) => new(color.R, color.G, color.B, alpha);

	private static RgbColor AdjustBrightness(RgbColor color, float factor)
	{
		return new RgbColor(
			Math.Clamp(color.R * factor, 0f, 1f),
			Math.Clamp(color.G * factor, 0f, 1f),
			Math.Clamp(color.B * factor, 0f, 1f)
		);
	}

	private static RgbColor GetContrastingTextColor(RgbColor backgroundColor)
	{
		RgbColor white = new(1f, 1f, 1f);
		RgbColor black = new(0f, 0f, 0f);

		float whiteContrast = ColorMath.GetContrastRatio(white, backgroundColor);
		float blackContrast = ColorMath.GetContrastRatio(black, backgroundColor);

		return whiteContrast > blackContrast ? white : black;
	}
}

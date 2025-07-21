// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProviderDemo;
using System.Collections.Immutable;
using System.Numerics;
using Hexa.NET.ImGui;
using ktsu.ImGuiApp;
using ktsu.ThemeProvider.Core;
using ktsu.ThemeProvider.Engines;
using ktsu.ThemeProvider.ImGui;
using ktsu.ThemeProvider.Semantic;
using ktsu.ThemeProvider.Themes;
using ktsu.ThemeProvider.Themes.Catppuccin;

internal static class Program
{
	private static ThemeDefinition theme = null!;
	private static SemanticPaletteEngine paletteEngine = null!;
	private static ImGuiPaletteMapper imguiMapper = null!;

	// UI State
	private static int selectedSemanticMeaning = (int)SemanticMeaning.Primary;
	private static int selectedVisualRole = (int)VisualRole.Surface;
	private static int selectedImportance = (int)ImportanceLevel.Medium;
	private static bool isPrimary = true;
	private static Vector3 selectedColorVec = Vector3.Zero;
	private static Vector3 backgroundColorVec = new(0.1f, 0.1f, 0.1f);
	private static bool isLargeText;

	// Semantic request parameters
	private static float desiredTemperature;
	private static float desiredEnergy = 0.5f;
	private static float desiredFormality = 0.5f;
	private static int accessibilityRequirement = (int)AccessibilityLevel.AA;

	// Form state
	private static float sliderValue = 0.5f;
	private static bool checkboxValue;
	private static string textValue = "Text Input";

	// Generated palette cache
	private static SemanticPaletteResult? lastGeneratedPalette;

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
		theme = CatppuccinMocha.CreateTheme();
		paletteEngine = new SemanticPaletteEngine(theme);
		imguiMapper = new ImGuiPaletteMapper();

		// Initialize with theme's primary text color
		SemanticColorSpec textSpec = new(SemanticMeaning.Primary, VisualRole.Text, ImportanceLevel.Critical, isPrimary: true);
		if (theme.TryGetColor(textSpec, out ColorProperties textColor))
		{
			selectedColorVec = new Vector3(textColor.RgbValue.R, textColor.RgbValue.G, textColor.RgbValue.B);
		}
	}

	private static void OnRender(float deltaTime)
	{
		ApplyCatppuccinTheme();

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

			if (ImGui.BeginTabItem("Color Generator"))
			{
				RenderColorGenerator();
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

	private static void ApplyCatppuccinTheme()
	{
		// Use the new ImGui palette mapper system
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
		ImGui.TextUnformatted($"Theme: {theme.Name}");
		ImGui.TextUnformatted($"Author: {theme.Author}");
		ImGui.TextUnformatted($"Description: {theme.Description}");
		ImGui.TextUnformatted($"Type: {(theme.IsDarkTheme ? "Dark" : "Light")} Theme");
		ImGui.Separator();

		ImGui.TextUnformatted("Semantic Color System:");
		ImGui.TextUnformatted("Colors are defined by semantic meaning, visual role, and importance level rather than specific names.");
		ImGui.Separator();

		ImGui.TextUnformatted("Color Specifications:");

		// Group colors by visual role for better organization
		Dictionary<VisualRole, List<(SemanticColorSpec spec, ColorProperties color)>> roleGroups = [];
		foreach ((SemanticColorSpec spec, ColorProperties color) in theme.Colors)
		{
			if (!roleGroups.TryGetValue(spec.Role, out List<(SemanticColorSpec spec, ColorProperties color)>? group))
			{
				group = [];
				roleGroups[spec.Role] = group;
			}
			group.Add((spec, color));
		}

		float colorSize = 40f;
		foreach ((VisualRole role, List<(SemanticColorSpec spec, ColorProperties color)> colors) in roleGroups.OrderBy(kvp => (int)kvp.Key))
		{
			if (ImGui.CollapsingHeader($"{role} Colors ({colors.Count})"))
			{
				int columns = Math.Max(1, (int)(ImGui.GetContentRegionAvail().X / 200f));
				if (ImGui.BeginTable($"{role}Table", columns * 2, ImGuiTableFlags.None)) // 2 columns per color (swatch + info)
				{
					int itemCount = 0;
					foreach ((SemanticColorSpec spec, ColorProperties color) in colors.OrderBy(x => (int)x.spec.Meaning).ThenBy(x => (int)x.spec.Importance))
					{
						if (itemCount % columns == 0)
						{
							ImGui.TableNextRow();
						}

						// Swatch column
						int baseColumnIndex = itemCount % columns * 2;
						ImGui.TableSetColumnIndex(baseColumnIndex);

						// Color swatch
						ImDrawListPtr drawList = ImGui.GetWindowDrawList();
						Vector2 pos = ImGui.GetCursorScreenPos();
						Vector4 rgbColor = ToImVec4(color.RgbValue);
						drawList.AddRectFilled(pos, new Vector2(pos.X + colorSize, pos.Y + colorSize),
											   ImGui.ColorConvertFloat4ToU32(rgbColor));
						drawList.AddRect(pos, new Vector2(pos.X + colorSize, pos.Y + colorSize),
										ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.3f)));

						ImGui.Dummy(new Vector2(colorSize, colorSize));

						if (ImGui.IsItemHovered())
						{
							ImGui.BeginTooltip();
							ImGui.TextUnformatted($"{spec}");
							ImGui.TextUnformatted($"Hex: {color.RgbValue.ToHex()}");
							ImGui.TextUnformatted($"Temperature: {color.Temperature:F2}");
							ImGui.TextUnformatted($"Energy: {color.Energy:F2}");
							ImGui.TextUnformatted($"Weight: {color.Weight:F2}");
							ImGui.EndTooltip();
						}

						// Info column
						ImGui.TableSetColumnIndex(baseColumnIndex + 1);
						ImGui.TextUnformatted($"{spec.Meaning}");
						ImGui.TextUnformatted($"{spec.Importance}");
						ImGui.TextUnformatted($"{(spec.IsPrimary ? "Primary" : "Secondary")}");

						itemCount++;
					}

					ImGui.EndTable();
				}
			}
		}
	}

	private static void RenderSemanticColors()
	{
		ImGui.TextUnformatted("Explore the semantic color system:");
		ImGui.Separator();

		// Semantic color specification builder
		ImGui.TextUnformatted("Build a Semantic Color Specification:");

		string[] meaningNames = Enum.GetNames<SemanticMeaning>();
		ImGui.Combo("Semantic Meaning", ref selectedSemanticMeaning, meaningNames, meaningNames.Length);

		string[] roleNames = Enum.GetNames<VisualRole>();
		ImGui.Combo("Visual Role", ref selectedVisualRole, roleNames, roleNames.Length);

		string[] importanceNames = Enum.GetNames<ImportanceLevel>();
		ImGui.Combo("Importance Level", ref selectedImportance, importanceNames, importanceNames.Length);

		ImGui.Checkbox("Primary (vs Secondary)", ref isPrimary);

		// Build the current spec
		SemanticColorSpec currentSpec = new(
			(SemanticMeaning)selectedSemanticMeaning,
			(VisualRole)selectedVisualRole,
			(ImportanceLevel)selectedImportance,
			isPrimary
		);

		ImGui.Separator();
		ImGui.TextUnformatted($"Current Specification: {currentSpec}");

		// Display the color if it exists in the theme
		if (theme.TryGetColor(currentSpec, out ColorProperties color))
		{
			// Color preview
			float previewSize = 150f;
			ImDrawListPtr drawList = ImGui.GetWindowDrawList();
			Vector2 pos = ImGui.GetCursorScreenPos();
			Vector4 rgbColor = ToImVec4(color.RgbValue);
			drawList.AddRectFilled(pos, new Vector2(pos.X + previewSize, pos.Y + previewSize),
								   ImGui.ColorConvertFloat4ToU32(rgbColor));
			drawList.AddRect(pos, new Vector2(pos.X + previewSize, pos.Y + previewSize),
							ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.5f)));
			ImGui.Dummy(new Vector2(previewSize, previewSize));

			ImGui.SameLine();
			ImGui.BeginGroup();

			ImGui.TextUnformatted("Color Properties:");
			ImGui.TextUnformatted($"Hex: {color.RgbValue.ToHex()}");
			ImGui.TextUnformatted($"RGB: ({color.RgbValue.R:F3}, {color.RgbValue.G:F3}, {color.RgbValue.B:F3})");

			OklabColor oklab = color.OklabValue;
			ImGui.TextUnformatted($"Oklab: L={oklab.L:F3}, a={oklab.A:F3}, b={oklab.B:F3}");

			ImGui.Separator();
			ImGui.TextUnformatted("Semantic Properties:");
			ImGui.TextUnformatted($"Weight: {color.Weight:F2}");
			ImGui.TextUnformatted($"Temperature: {color.Temperature:F2} ({(color.Temperature > 0 ? "Warm" : "Cool")})");
			ImGui.TextUnformatted($"Energy: {color.Energy:F2}");
			ImGui.TextUnformatted($"Formality: {color.Formality:F2}");
			ImGui.TextUnformatted($"Accessibility Priority: {color.AccessibilityPriority:F2}");

			ImGui.EndGroup();
		}
		else
		{
			ImGui.TextUnformatted("This specification is not defined in the current theme.");

			// Show closest match
			(SemanticColorSpec closestSpec, ColorProperties closestColor) = theme.FindClosestColor(
				ColorProperties.FromRgb(new RgbColor(0.5f, 0.5f, 0.5f), currentSpec)
			);

			ImGui.Separator();
			ImGui.TextUnformatted($"Closest match: {closestSpec}");

			// Show closest color
			float previewSize = 100f;
			ImDrawListPtr drawList = ImGui.GetWindowDrawList();
			Vector2 pos = ImGui.GetCursorScreenPos();
			Vector4 rgbColor = ToImVec4(closestColor.RgbValue);
			drawList.AddRectFilled(pos, new Vector2(pos.X + previewSize, pos.Y + previewSize),
								   ImGui.ColorConvertFloat4ToU32(rgbColor));
			ImGui.Dummy(new Vector2(previewSize, previewSize));
		}
	}

	private static void RenderColorGenerator()
	{
		ImGui.TextUnformatted("Generate semantic color palettes using the engine:");
		ImGui.Separator();

		// Request parameters
		ImGui.TextUnformatted("Semantic Request Parameters:");
		ImGui.SliderFloat("Desired Temperature", ref desiredTemperature, -1.0f, 1.0f, "%.2f");
		ImGui.SliderFloat("Desired Energy", ref desiredEnergy, 0.0f, 1.0f, "%.2f");
		ImGui.SliderFloat("Desired Formality", ref desiredFormality, 0.0f, 1.0f, "%.2f");

		string[] accessibilityNames = Enum.GetNames<AccessibilityLevel>();
		ImGui.Combo("Accessibility Requirement", ref accessibilityRequirement, accessibilityNames, accessibilityNames.Length);

		if (ImGui.Button("Generate Semantic Palette"))
		{
			GenerateSemanticPalette();
		}

		ImGui.Separator();

		if (lastGeneratedPalette != null)
		{
			RenderGeneratedPalette(lastGeneratedPalette);
		}
	}

	private static void GenerateSemanticPalette()
	{
		// Get base background color for contrast calculations
		SemanticColorSpec baseSpec = new(SemanticMeaning.Primary, VisualRole.Background, ImportanceLevel.Low, isPrimary: true);
		ColorProperties baseColor = theme.GetColor(baseSpec);

		// Create a semantic graph with various color requests
		SemanticColorGraph graph = SemanticColorGraph.CreateBuilder()
			.AddRequest(new SemanticColorRequest
			{
				PrimarySpec = new(SemanticMeaning.CallToAction, VisualRole.Widget, ImportanceLevel.Critical, isPrimary: true),
				DesiredTemperature = desiredTemperature,
				DesiredEnergy = desiredEnergy,
				DesiredFormality = desiredFormality,
				AccessibilityRequirement = (AccessibilityLevel)accessibilityRequirement,
				BackgroundColor = baseColor.RgbValue,
				ImportanceWeight = 1.0f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimarySpec = new(SemanticMeaning.Success, VisualRole.Text, ImportanceLevel.High, isPrimary: true),
				DesiredTemperature = desiredTemperature * 0.5f,
				DesiredEnergy = desiredEnergy,
				DesiredFormality = desiredFormality,
				AccessibilityRequirement = (AccessibilityLevel)accessibilityRequirement,
				BackgroundColor = baseColor.RgbValue,
				ImportanceWeight = 0.8f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimarySpec = new(SemanticMeaning.Warning, VisualRole.Widget, ImportanceLevel.High, isPrimary: true),
				DesiredTemperature = Math.Max(0.0f, desiredTemperature),
				DesiredEnergy = Math.Max(0.6f, desiredEnergy),
				DesiredFormality = desiredFormality,
				AccessibilityRequirement = (AccessibilityLevel)accessibilityRequirement,
				BackgroundColor = baseColor.RgbValue,
				ImportanceWeight = 0.7f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimarySpec = new(SemanticMeaning.Error, VisualRole.Text, ImportanceLevel.Critical, isPrimary: true),
				DesiredTemperature = Math.Max(0.2f, desiredTemperature),
				DesiredEnergy = Math.Max(0.7f, desiredEnergy),
				DesiredFormality = desiredFormality,
				AccessibilityRequirement = (AccessibilityLevel)accessibilityRequirement,
				BackgroundColor = baseColor.RgbValue,
				ImportanceWeight = 0.9f
			})
			.AddRequest(new SemanticColorRequest
			{
				PrimarySpec = new(SemanticMeaning.Information, VisualRole.Surface, ImportanceLevel.Medium, isPrimary: true),
				DesiredTemperature = Math.Min(0.0f, desiredTemperature),
				DesiredEnergy = desiredEnergy * 0.8f,
				DesiredFormality = desiredFormality,
				AccessibilityRequirement = (AccessibilityLevel)accessibilityRequirement,
				BackgroundColor = baseColor.RgbValue,
				ImportanceWeight = 0.6f
			})
			.AddHarmony(fromIndex: 0, toIndex: 1, weight: 0.8f) // CTA and Success should harmonize
			.AddHarmony(fromIndex: 2, toIndex: 3, weight: 0.6f) // Warning and Error should relate
			.Build();

		lastGeneratedPalette = paletteEngine.GeneratePalette(graph);
	}

	private static void RenderGeneratedPalette(SemanticPaletteResult result)
	{
		ImGui.TextUnformatted("Generated Semantic Palette:");
		ImGui.TextUnformatted($"Generated {result.GeneratedColors.Length} colors");
		ImGui.TextUnformatted($"Overall Harmony Score: {result.OverallHarmonyScore:F2}");
		ImGui.TextUnformatted($"Meets Accessibility Requirements: {(result.MeetsAccessibilityRequirements ? "Yes" : "No")}");

		if (result.Warnings.Length > 0)
		{
			ImGui.Separator();
			ImGui.TextUnformatted("Warnings:");
			foreach (string warning in result.Warnings)
			{
				ImGui.TextColored(new Vector4(1, 1, 0, 1), $"• {warning}");
			}
		}

		ImGui.Separator();

		string[] requestNames = ["Call-to-Action", "Success", "Warning", "Error", "Information"];
		float swatchSize = 60f;

		for (int i = 0; i < Math.Min(result.GeneratedColors.Length, requestNames.Length); i++)
		{
			RgbColor color = result.GeneratedColors[i];
			float accessibilityScore = result.AccessibilityScores[i];
			float semanticScore = result.SemanticMatchScores[i];

			// Color swatch
			ImDrawListPtr drawList = ImGui.GetWindowDrawList();
			Vector2 pos = ImGui.GetCursorScreenPos();
			Vector4 rgbVec = ToImVec4(color);
			drawList.AddRectFilled(pos, new Vector2(pos.X + swatchSize, pos.Y + swatchSize),
								   ImGui.ColorConvertFloat4ToU32(rgbVec));
			drawList.AddRect(pos, new Vector2(pos.X + swatchSize, pos.Y + swatchSize),
							ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.3f)));
			ImGui.Dummy(new Vector2(swatchSize, swatchSize));

			ImGui.SameLine();
			ImGui.BeginGroup();
			ImGui.TextUnformatted(requestNames[i]);
			ImGui.TextUnformatted($"Hex: {color.ToHex()}");
			ImGui.TextUnformatted($"Accessibility: {accessibilityScore:F2}");
			ImGui.TextUnformatted($"Semantic Match: {semanticScore:F2}");
			ImGui.EndGroup();

			if (i < result.GeneratedColors.Length - 1)
			{
				ImGui.Separator();
			}
		}
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

		// Call-to-action button
		if (theme.TryGetColor(new(SemanticMeaning.CallToAction, VisualRole.Widget, ImportanceLevel.Critical), out ColorProperties ctaColor))
		{
			RgbColor buttonTextColor = GetContrastingTextColor(ctaColor.RgbValue);
			ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(ctaColor.RgbValue));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(AdjustBrightness(ctaColor.RgbValue, 1.1f)));
			ImGui.PushStyleColor(ImGuiCol.Text, ToImVec4(buttonTextColor));
			if (ImGui.Button("Call-to-Action"))
			{
				// Action
			}
			ImGui.PopStyleColor(3);
			ImGui.SameLine();
		}

		// Success button
		if (theme.TryGetColor(new(SemanticMeaning.Success, VisualRole.Widget, ImportanceLevel.High), out ColorProperties successColor))
		{
			RgbColor buttonTextColor = GetContrastingTextColor(successColor.RgbValue);
			ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(successColor.RgbValue));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(AdjustBrightness(successColor.RgbValue, 1.1f)));
			ImGui.PushStyleColor(ImGuiCol.Text, ToImVec4(buttonTextColor));
			if (ImGui.Button("Success"))
			{
				// Action
			}
			ImGui.PopStyleColor(3);
			ImGui.SameLine();
		}

		// Error button
		if (theme.TryGetColor(new(SemanticMeaning.Error, VisualRole.Widget, ImportanceLevel.Critical), out ColorProperties errorColor))
		{
			RgbColor buttonTextColor = GetContrastingTextColor(errorColor.RgbValue);
			ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(errorColor.RgbValue));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(AdjustBrightness(errorColor.RgbValue, 1.1f)));
			ImGui.PushStyleColor(ImGuiCol.Text, ToImVec4(buttonTextColor));
			if (ImGui.Button("Error"))
			{
				// Action
			}
			ImGui.PopStyleColor(3);
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

		if (theme.TryGetColor(new(SemanticMeaning.Success, VisualRole.Widget, ImportanceLevel.High), out ColorProperties successWidget))
		{
			ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(successWidget.RgbValue));
			ImGui.ProgressBar(0.7f, new Vector2(0, 0), "70% Complete");
			ImGui.PopStyleColor();
		}

		if (theme.TryGetColor(new(SemanticMeaning.Warning, VisualRole.Widget, ImportanceLevel.High), out ColorProperties warningWidget))
		{
			ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(warningWidget.RgbValue));
			ImGui.ProgressBar(0.4f, new Vector2(0, 0), "40% Warning");
			ImGui.PopStyleColor();
		}

		if (theme.TryGetColor(new(SemanticMeaning.Error, VisualRole.Widget, ImportanceLevel.Critical), out ColorProperties errorWidget))
		{
			ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(errorWidget.RgbValue));
			ImGui.ProgressBar(0.2f, new Vector2(0, 0), "20% Critical");
			ImGui.PopStyleColor();
		}

		ImGui.Separator();

		// Semantic text
		ImGui.TextUnformatted("Semantic Text:");
		if (theme.TryGetColor(new(SemanticMeaning.Success, VisualRole.Text, ImportanceLevel.High), out ColorProperties successText))
		{
			ImGui.TextColored(ToImVec4(successText.RgbValue), "Success: Operation completed successfully");
		}
		if (theme.TryGetColor(new(SemanticMeaning.Warning, VisualRole.Text, ImportanceLevel.High), out ColorProperties warningText))
		{
			ImGui.TextColored(ToImVec4(warningText.RgbValue), "Warning: Please review your input");
		}
		if (theme.TryGetColor(new(SemanticMeaning.Error, VisualRole.Text, ImportanceLevel.Critical), out ColorProperties errorText))
		{
			ImGui.TextColored(ToImVec4(errorText.RgbValue), "Error: Operation failed");
		}
		if (theme.TryGetColor(new(SemanticMeaning.Information, VisualRole.Text, ImportanceLevel.Medium), out ColorProperties infoText))
		{
			ImGui.TextColored(ToImVec4(infoText.RgbValue), "Information: Additional details available");
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
		ImmutableDictionary<string, object> metadata = imguiMapper.GetMappingMetadata(theme);

		ImGui.TextUnformatted($"Total Mapped Colors: {mappedColors.Count}");
		ImGui.TextUnformatted($"Generation Time: {metadata["generation_time"]}");

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
		ImGui.BulletText("Automatic fallback to sensible defaults for unmapped colors");
		ImGui.BulletText("Easy to adapt to other UI frameworks using the same pattern");
		ImGui.BulletText("Maintains theme authenticity while ensuring proper contrast");
		ImGui.BulletText("Brightness adjustments for interactive states (hover/active)");

		ImGui.Separator();
		ImGui.TextUnformatted("Framework Integration Example:");
		ImGui.Text("// Apply complete theme with one line:\nImGuiColors = mapper.MapTheme(theme);\n\n// Instead of manually mapping dozens of colors:\n// colors[ImGuiCol.Text] = theme.GetColor(...);\n// colors[ImGuiCol.Button] = theme.GetColor(...);\n// ... (50+ more lines)");
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

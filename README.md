# Not-Zune

<div align="center">

# 🎵 Not-Zune
**The authentic cross-platform spiritual successor to Microsoft Zune Desktop & Player**

[![Build & Release](https://github.com/Heretek-AI/not-zune/actions/workflows/build.yml/badge.svg)](https://github.com/Heretek-AI/not-zune/actions/workflows/build.yml)
[![Avalonia UI](https://img.shields.io/badge/Avalonia_UI-11.2-8C15E9?logo=avalonia&logoColor=white)](https://avaloniaui.net/)
[![Platforms](https://img.shields.io/badge/Platforms-Windows%20%7C%20Linux%20(x64%20%26%20arm64)-0078D7)]()
[![Design](https://img.shields.io/badge/Aesthetic-Zune%20Metro%20%2F%20Iris-FA2A55)]()

</div>

---

## ✨ Design Principles: The Zune "Metro" Experience

Not-Zune is built on the purest tenets of the original Microsoft Zune Desktop software:

- **Content Before Chrome:** Zero rounded corners (`CornerRadius = 0`), no drop shadows, no skeuomorphic gradients or faux-leather textures.
- **Typography as Art:** Sized and kerned with Segoe UI / Selawik metrics across Display, Pivot, Sub-pivot, and Caption hierarchies. Opacity communicates state (Active: 100%, Hover: 85%, Inactive: 40%).
- **Iconic Pivot Navigation:** Fluid deceleration panning across `QUICKPLAY`, `COLLECTION`, `DEVICE`, and `SETTINGS`.
- **Quickplay Hub:** Split layout featuring an interactive Smart DJ seed generator on the left, and an interactive sliding ribbon of `Pins`, `History`, and `New` on the right.
- **Dynamic Now Playing Canvas:**
  - *Dynamic Artist Canvas:* High-resolution artist photography with ambient drift and bold typographic overlays.
  - *Album Art Mosaic Wall:* Continuous 2D/3D tapestry of album art tiles from your collection.
- **Tri-State Heart Rating:** Favorite (❤️ / Heart), Disliked/Skip (💔 / Broken Heart), and Neutral.
- **Signature Accent Colors:** Select between Zune Pink (`#FA2A55`), Zune Orange (`#F09609`), Electric Cyan (`#1BA1E2`), Vivid Lime (`#339933`), and Deep Purple (`#A200FF`).

---

## 🚀 Architecture & Features

Built with Clean Architecture in .NET 8 / C# 12:

```
src/
├── NotZune.Domain/                 # Entities (Track, Album, Artist, Playlist, Device)
├── NotZune.Application/            # Player coordinator, Smart DJ engine, sync orchestrator
├── NotZune.Infrastructure.Persistence/ # SQLite database & EF Core
├── NotZune.Infrastructure.Audio/   # Cross-platform audio pipeline & gapless transitions
├── NotZune.Infrastructure.Devices/ # Zune USB sync (MTP/MTPZ, fast ZMDB parser, USB-PPP)
├── NotZune.Infrastructure.External/# Metadata aggregators (MusicBrainz, Fanart.tv, Last.fm)
├── NotZune.Plugins.Protocol/       # Shared JSON-RPC message contracts
├── NotZune.Plugins.Sdk/            # Sandboxed out-of-process plugin SDK
├── NotZune.UI/                     # Shared Avalonia XAML views, ViewModels, and styles
└── NotZune.Desktop/                # Desktop executable for Linux and Windows
```

---

## 🛠️ Building & Running

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Running Desktop
```bash
dotnet run --project src/NotZune.Desktop/NotZune.Desktop.csproj
```

### Running Tests
```bash
dotnet test NotZune.sln
```

### Multi-Platform Publishing
```bash
# Linux x64
dotnet publish src/NotZune.Desktop -r linux-x64 -c Release

# Linux arm64
dotnet publish src/NotZune.Desktop -r linux-arm64 -c Release

# Windows x64
dotnet publish src/NotZune.Desktop -r win-x64 -c Release

# Windows arm64
dotnet publish src/NotZune.Desktop -r win-arm64 -c Release
```

---

## 🧰 Self-Contained Repository Skills & MCP Tools

This repository contains built-in agent customizations and tools:
- **`GEMINI.md`**: Master repository rules enforcing Zune Metro design invariants and multi-platform boundaries.
- **`.agents/skills/zune-design-system`**: Comprehensive design tokens, layout specifications, and XAML templates.
- **`.agents/skills/zune-hardware-sync`**: Guide to USB MTP/MTPZ, ZMDB binary parsing, and USB-PPP reverse interception.
- **`.agents/skills/zune-plugins-protocol`**: Out-of-process plugin wire contracts and packaging specifications.
- **`.agents/mcp_config.json`**: Local MCP development tools (`probe_zune_devices`, `audit_zune_design_invariants`).

---

## 📄 License

Licensed under the MIT License.

# Deferred Registry (documented, not scheduled)

Items consciously deferred after the true-parity program (Phases 5–9) with rationale.
Each is a possible future work item; none blocks the Zune 4.8 experience Not-Zune delivers.

| Item | Origin | Rationale for deferral |
|---|---|---|
| **i18n — all 26 Zune locales** | Zune shipped localized UIs (ZuneShellResources `.UIX` per locale) | Text is baked into AXAML; a resource-dictionary localization pass is mechanical but touches every view. English-only is acceptable for the current audience. |
| **UPnP media sharing (ZuneNSS / `ZuneShareEXE` parity)** | Native component map: network sharing services | Zune's social sharing servers are dead; a local UPnP/DLNA renderer/server has no Zune-visible counterpart to validate against. |
| **Explorer / taskbar shell integration (`ZuneShellExt_Dll`, `ZuneTaskbar_Dll`, `ZuneLauncherEXE`)** | Native component map | Windows-only, shell-level (context menus, taskbar previews). Cross-platform app; value is cosmetic. |
| **MTPZ firmware update / restore / rollback (`ZuneWmduDLL` parity)** | Device lifecycle | Hardware N-A: no physical Zune device is available to develop/test against; the device-sync seam (`IDeviceTransport`) is the correct place to land this later. |
| **Windows jump lists** | Shell integration | Windows-only convenience; documented alongside shell integration deferral. |
| **Mini-player video surface** | Phase 8 polish backlog | The mini-player video mode is text-only; real video needs a `VideoView` surface inside the compact overlay. Video remains fully playable in the main surface and full playback view. |
| **Mixview external related-artist satellites** | Phase 6 backlog | Mixview satellites currently use the local library (genre/mood/related); external MusicBrainz related-artist fetch would add network latency and rate-limit pressure to a purely visual surface. |
| **Notification-area tray icon** | Polish backlog | Avalonia tray support is platform-quirky; Zune itself only had a taskbar presence. |
| **CD Land real pipeline (Phase 10)** | Capability-gated phase | No optical drive is available on the development machine; the DISC view stays in its manual/simulated mode. Implementation should be done blind against platform tooling (`cdparanoia`/`cdrdao`/IMAPI2) only if explicitly requested. |

Last updated: 2026-09-09 (post Phase 9).

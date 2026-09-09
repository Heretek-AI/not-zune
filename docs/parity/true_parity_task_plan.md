# Not-Zune True-Parity Task Plan

Derived from the audit in [`zune48_parity_audit.md`](zune48_parity_audit.md). Scoping decisions: **ManagedBass** audio engine, **video/photos included** (Phase 8), **i18n deferred**.

Cross-cutting convention per phase: unit tests for every new service · design-invariants audit (0 violations) · Release build 0 warnings/0 errors · walkthrough section · milestone commit.

---

## Phase 5 — Real Audio Engine (P0, unblocks everything audible)

Audit target: Domain F (currently SIMULATED/0%).

| # | Task |
|---|---|
| 5.1 | Add ManagedBass + native BASS binaries (win-x64/linux-x64/arm64, non-commercial license); `Bass.Init` with NoSound fallback so CI/headless never crashes |
| 5.2 | `IAudioOutputEngine` contract: Load/Play/Pause/Stop/Seek, real Position/Duration, Volume/Mute, EndOfTrack, TrackTransitioned, FFT |
| 5.3 | `BassAudioOutputEngine`: decode files/URLs, real stream duration, channel position |
| 5.4 | Rewire `PlaybackQueueCoordinator` → engine; real auto-advance honoring shuffle/repeat; retire simulated position timer |
| 5.5 | Gapless via BassMix mixer slots (crossfade = 0 path) |
| 5.6 | Crossfade: equal-power envelopes over mixer, 0–10s from persisted settings; real `IsCrossfading` |
| 5.7 | ReplayGain: RG track/peak tags via TagLibSharp, applied as preamp behind `VolumeLevelingEnabled` |
| 5.8 | Real FFT visualizer: `ChannelGetData` FFT → 24-bar aggregation into `VisualizerBars` |
| 5.9 | Podcast HTTP-stream playback |
| 5.10 | Tests: `FakeAudioOutputEngine`, coordinator wiring tests, NoSound smoke tests |

## Phase 6 — Collection Parity (P1)

| # | Task |
|---|---|
| 6.1 | Smart/auto playlists (`AUTOPLAYLISTDIALOG` parity): rule model (genre/artist/rating/playcount/recency), rule-builder dialog, live evaluation, ZPL export |
| 6.2 | Find Album Info track matching (`FINDALBUMINFOSONGMATCH` parity): per-track MusicBrainz recordings lookup + review dialog honoring `WriteTagsToFile` |
| 6.3 | Search autocomplete dropdown (`AUTOCOMPLETEBOX` parity) |
| 6.4 | Global back-stack navigation service (per-view history; today only MixStack) |
| 6.5 | Mixview hover tiles fully wired: like/hate/info/add + bio hover card |
| 6.6 | Mixview similarity upgrade (local genre-vector weighting + optional external related artists) |

## Phase 7 — Onboarding & Shell Parity (P1)

| # | Task |
|---|---|
| 7.1 | First-launch wizard (`FIRSTLAUNCH` parity): welcome → folder pick → scan → done |
| 7.2 | What's New dialog on version change (`WHATSNEW` parity) |
| 7.3 | Sync animation + instruction toast (`SYNCANIMATION`/`SYNCINSTRUCTIONTOAST` parity) |
| 7.4 | (Optional) Notification-area tray icon |

## Phase 8 — Video & Photos (P2, approved)

| # | Task |
|---|---|
| 8.1 | `Video`/`Photo`/`PhotoFolder` models + EF persistence |
| 8.2 | LibVLCSharp video engine + Avalonia video surface |
| 8.3 | Video library view (`VIDEOLIBRARY` parity) |
| 8.4 | Now Playing video clips + video mini-player (`NOWPLAYINGCLIPS`/`MINIMODEVIDEO` parity) |
| 8.5 | Photo library: folder tree + gallery grid (`PHOTOLIBRARY`/`GALLERYVIEW` parity) |
| 8.6 | Photo slideshow land (`PHOTOSLIDESHOW` parity) |
| 8.7 | Device picture/video sync categories (`DEVICEPICTUREVIDEO` parity) |

## Phase 9 — Device Sync Architecture (P2, hardware N-A)

| # | Task |
|---|---|
| 9.1 | Sync-group engine (`SyncGroup`/`SyncCategory`/`SyncMode` parity), dry-run mode |
| 9.2 | `IDeviceTransport` abstraction (simulator today; MTPZ slot-in later) |
| 9.3 | Live gas gauge + sync animation from progress |
| 9.4 | Guest sync mode (`GuestSchemaSyncGroup` parity) |

## Phase 10 — CD Land Real Pipeline (P3, capability-gated)

| # | Task |
|---|---|
| 10.1 | `IOpticalDriveService` platform detection; graceful no-drive state |
| 10.2 | Real rip: CD-DA extraction → encode (Bass FLAC/MP3) → ingest |
| 10.3 | Real burn via platform tooling |

## Phase 11 — Final Parity Sweep (P3)

| # | Task |
|---|---|
| 11.1 | Re-run parity audit; update status columns; measure delta from ~55–60% |
| 11.2 | Performance pass (startup, scan, decode caching) |
| 11.3 | Self-contained publish + release CI refresh |
| 11.4 | Deferred registry: i18n (26 locales), UPnP sharing, shell extensions/jump lists, firmware update/restore |

## Execution Order

5 → 6 → 7 → 8 → 9 → (10 optional, untestable without drive) → 11

**N-A permanent (no hardware):** MTPZ internals, firmware update/restore/rollback, wireless pairing.
**Deferred:** i18n, UPnP sharing, shell extensions, jump lists.

PREMIERE TIMELINE CLIP EXPORTER (EXPERIMENTAL)

1. Duplicate your Premiere project before testing.
2. Put the clips to export on video track V1.
3. Keep adjustment layers/effects on higher tracks.
4. Create or select an Adobe Media Encoder .epr export preset.
5. Run Export-V1-Clips.jsx using a compatible Premiere scripting host.
6. Select an output folder and the .epr preset.

The helper marks each V1 clip's timeline range and queues that range in Adobe
Media Encoder. Transitions, overlaps, duplicate clip names, nested sequences,
and Premiere API differences may require manual review. Duplicate names can
overwrite one another; rename such clips before exporting.

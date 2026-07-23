namespace LightflowStudio;

internal static class EncodingPresetCatalog
{
    public static EncodingOptions Recommended { get; } = new();

    public static EncodingOptions Get(EncodingPreset preset) => preset switch
    {
        EncodingPreset.Recommended => Recommended,
        EncodingPreset.MaximumQuality => Recommended with
        {
            Codec = VideoCodec.Hevc,
            Quality = 16,
            PixelFormat = VideoPixelFormat.P010,
            AudioMode = AudioEncodingMode.Aac,
            AudioBitrateKbps = 256
        },
        EncodingPreset.FastPreview => Recommended with
        {
            EncoderPreset = 4,
            Quality = 25,
            Multipass = MultipassMode.QuarterResolution,
            TemporalAq = false,
            AqStrength = 5,
            AudioMode = AudioEncodingMode.Aac,
            AudioBitrateKbps = 128
        },
        EncodingPreset.EfficientHevc => Recommended with
        {
            Codec = VideoCodec.Hevc,
            EncoderPreset = 6,
            Quality = 21,
            PixelFormat = VideoPixelFormat.Yuv420p,
            AudioMode = AudioEncodingMode.Aac
        },
        EncodingPreset.Custom => throw new ArgumentException("Custom settings do not map to a named preset.", nameof(preset)),
        _ => throw new ArgumentOutOfRangeException(nameof(preset))
    };
}
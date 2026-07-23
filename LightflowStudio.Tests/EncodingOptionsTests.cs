using LightflowStudio;
using Xunit;

namespace LightflowStudio.Tests;

public sealed class EncodingOptionsTests
{
    [Theory]
    [InlineData(EncodingPreset.Recommended)]
    [InlineData(EncodingPreset.MaximumQuality)]
    [InlineData(EncodingPreset.FastPreview)]
    [InlineData(EncodingPreset.EfficientHevc)]
    internal void NamedPresets_AreValidAndNvidiaBacked(EncodingPreset preset)
    {
        var options = EncodingPresetCatalog.Get(preset);

        Assert.Empty(EncodingOptionValidator.Validate(options));
        Assert.Equal(EncoderBackend.NvidiaNvenc, options.Backend);
    }

    [Fact]
    public void BackendCatalog_ReservesFutureBackendsWithoutEnablingThem()
    {
        Assert.True(EncoderBackendCatalog.IsImplemented(EncoderBackend.NvidiaNvenc));
        Assert.False(EncoderBackendCatalog.IsImplemented(EncoderBackend.Cpu));
        Assert.False(EncoderBackendCatalog.IsImplemented(EncoderBackend.AmdAmf));
        Assert.False(EncoderBackendCatalog.IsImplemented(EncoderBackend.IntelQuickSync));
    }

    [Fact]
    public void Validator_RejectsUnavailableBackendAndIncompatibleTenBitH264()
    {
        var options = EncodingPresetCatalog.Recommended with
        {
            Backend = EncoderBackend.AmdAmf,
            Codec = VideoCodec.H264,
            PixelFormat = VideoPixelFormat.P010
        };

        var errors = EncodingOptionValidator.Validate(options);

        Assert.Contains(errors, error => error.Contains("NVIDIA NVENC"));
        Assert.Contains(errors, error => error.Contains("requires HEVC"));
    }

    [Fact]
    public void Normalize_RepairsOutOfRangeValues()
    {
        var normalized = EncodingOptions.Normalize(new EncodingOptions
        {
            EncoderPreset = 99,
            Quality = -3,
            AqStrength = 40,
            AudioBitrateKbps = 900,
            FrameRate = 900
        });

        Assert.Equal(7, normalized.EncoderPreset);
        Assert.Equal(0, normalized.Quality);
        Assert.Equal(15, normalized.AqStrength);
        Assert.Equal(512, normalized.AudioBitrateKbps);
        Assert.Equal(0, normalized.FrameRate);
    }

    [Fact]
    public void CustomPreset_HasNoCatalogDefinition()
    {
        Assert.Throws<ArgumentException>(() => EncodingPresetCatalog.Get(EncodingPreset.Custom));
    }
}
// ReSharper disable InconsistentNaming

using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Whoop.Sdk.Model;

// ReSharper disable NotAccessedPositionalProperty.Global

namespace Whoop.Core;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public record CycleDto(
    string Id,
    string UserId,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime Start,
    DateTime? End,
    string TimeZoneOffset,
    Cycle.ScoreStateEnum CycleScoreState,
    CycleScoreDto? CycleScore,
    string? SleepId = null,
    RecoveryScore? RecoveryScore = null)
{
    public Type Type { get; init; } = Type.Cycle;
}

public record CycleScoreDto(float Strain, float Kilojoule, float AverageHeartRate, float MaxHeartRate);

public record RecoveryScoreDto(
    bool UserCalibrating,
    float RecoveryScore,
    float RestingHeartRate,
    float HrvRmssdMilli,
    float Spo2Percentage,
    float SkinTempCelsius);

public record ProfileDto(
    string Id, 
    string Email,
    string FirstName,
    string LastName,
    string AccessToken,
    string RefreshToken
    )
{
    public Type Type { get; init; } = Type.Profile;
}

[JsonConverter(typeof(StringEnumConverter))]
public enum Type
{
    Cycle,
    Profile
}
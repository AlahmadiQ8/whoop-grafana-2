// ReSharper disable InconsistentNaming

using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Whoop.Sdk.Model;
// ReSharper disable NotAccessedPositionalProperty.Global

namespace Whoop.Console;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public record CycleDto(
    string Id,
    string UserId,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime Start,
    DateTime? End,
    string TimeZoneOffset,
    Cycle.ScoreStateEnum ScoreState,
    CycleScoreDto? Score,
    string email,
    string firstName,
    string lastName)
{
    public Type type { get; init; } = Type.Cycle;
}

public record CycleScoreDto(float Strain, float Kilojoule, float AverageHeartRate, float MaxHeartRate);

[JsonConverter(typeof(StringEnumConverter))]
public enum Type
{
    Cycle
}

static class CycleExtensions
{
    public static CycleDto ToCycleDto(this Cycle cycle, UserBasicProfile userBasicProfile)
    {
        return new CycleDto(
            Id: cycle.Id.ToString(),
            UserId: cycle.UserId.ToString(),
            CreatedAt: cycle.CreatedAt,
            UpdatedAt: cycle.UpdatedAt,
            Start: cycle.Start,
            End: cycle.End,
            TimeZoneOffset: cycle.TimezoneOffset,
            ScoreState: cycle.ScoreState,
            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            Score: cycle.Score?.ToCycleScoreDto(),
            email: userBasicProfile.Email,
            firstName: userBasicProfile.FirstName,
            lastName: userBasicProfile.LastName
        );
    }

    private static CycleScoreDto ToCycleScoreDto(this CycleScore cycleScore)
    {
        return new CycleScoreDto(
            Strain: cycleScore.Strain,
            Kilojoule: cycleScore.Kilojoule,
            AverageHeartRate: cycleScore.AverageHeartRate,
            MaxHeartRate: cycleScore.MaxHeartRate);
    }
}
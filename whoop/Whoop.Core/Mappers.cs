using Whoop.Sdk.Model;

namespace Whoop.Core;

public static class Mappers
{
    public static CycleDto ToCycleDto(this Cycle cycle, ProfileDto userProfile)
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
            Email: userProfile.Email,
            FirstName: userProfile.FirstName,
            LastName: userProfile.LastName
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
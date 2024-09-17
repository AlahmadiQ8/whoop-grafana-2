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
            CycleScoreState: cycle.ScoreState,
            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            CycleScore: cycle.Score?.ToCycleScoreDto()
        );
    }
    
    public static RecoveryScoreDto ToRecoveryScoreDto(this RecoveryScore recoveryScore)
    {
        return new RecoveryScoreDto(
            UserCalibrating: recoveryScore.UserCalibrating,
            RecoveryScore: recoveryScore.VarRecoveryScore,
            RestingHeartRate: recoveryScore.RestingHeartRate,
            HrvRmssdMilli: recoveryScore.HrvRmssdMilli,
            Spo2Percentage: recoveryScore.Spo2Percentage,
            SkinTempCelsius: recoveryScore.SkinTempCelsius
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
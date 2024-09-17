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

    public static SleepDto ToSleepDto(this Sleep sleep)
    {
        return new SleepDto(
            Nap: sleep.Nap,
            SleepStageSummary: sleep.Score.StageSummary.ToSleepStageSummaryDto(),
            SleepNeeded: sleep.Score.SleepNeeded.ToSleepNeededDto(),
            RespiratoryRate: sleep.Score.RespiratoryRate,
            SleepPerformancePercentage: sleep.Score.SleepPerformancePercentage,
            SleepConsistencyPercentage: sleep.Score.SleepConsistencyPercentage,
            SleepEfficiencyPercentage: sleep.Score.SleepEfficiencyPercentage
        );
    }

    private static SleepStageSummaryDto ToSleepStageSummaryDto(this SleepStageSummary sleepStageSummary)
    {
        return new SleepStageSummaryDto(
            TotalInBedTimeMilli: sleepStageSummary.TotalInBedTimeMilli,
            TotalAwakeTimeMilli: sleepStageSummary.TotalAwakeTimeMilli,
            TotalNoDataTimeMilli: sleepStageSummary.TotalNoDataTimeMilli,
            TotalLightSleepTimeMilli: sleepStageSummary.TotalLightSleepTimeMilli,
            TotalSlowWaveSleepTimeMilli: sleepStageSummary.TotalSlowWaveSleepTimeMilli,
            TotalRemSleepTimeMilli: sleepStageSummary.TotalRemSleepTimeMilli,
            SleepCycleCount: sleepStageSummary.SleepCycleCount,
            DisturbanceCount: sleepStageSummary.DisturbanceCount
        );
    }

    private static SleepNeededDto ToSleepNeededDto(this SleepNeeded sleepNeeded)
    {
        return new SleepNeededDto(
            BaselineMilli: sleepNeeded.BaselineMilli,
            NeedFromSleepDebtMilli: sleepNeeded.NeedFromSleepDebtMilli,
            NeedFromRecentStrainMilli: sleepNeeded.NeedFromRecentStrainMilli,
            NeedFromRecentNapMilli: sleepNeeded.NeedFromRecentNapMilli
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
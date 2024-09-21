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
    RecoveryScoreDto? RecoveryScore = null,
    SleepDto? SleepScore = null) : WithId
{
    public Type Type { get; init; } = Type.Cycle;
}

public record SleepDto(
    bool Nap,
    SleepStageSummaryDto SleepStageSummary,
    SleepNeededDto SleepNeeded,
    float? RespiratoryRate,
    float? SleepPerformancePercentage,
    float? SleepConsistencyPercentage,
    float? SleepEfficiencyPercentage
);

public record SleepStageSummaryDto(
    int TotalInBedTimeMilli,
    int TotalAwakeTimeMilli,
    int TotalNoDataTimeMilli,
    int TotalLightSleepTimeMilli,
    int TotalSlowWaveSleepTimeMilli,
    int TotalRemSleepTimeMilli,
    int SleepCycleCount,
    int DisturbanceCount
);

public record SleepNeededDto(
    long BaselineMilli,
    long NeedFromSleepDebtMilli,
    long NeedFromRecentStrainMilli,
    long NeedFromRecentNapMilli
);

public record CycleScoreDto(float Strain, float Kilojoule, float AverageHeartRate, float MaxHeartRate);

public record RecoveryScoreDto(
    bool UserCalibrating,
    float RecoveryScore,
    float RestingHeartRate,
    float HrvRmssdMilli,
    float? Spo2Percentage,
    float? SkinTempCelsius);

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

public record WorkoutDto(
    string Id,
    string UserId,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime Start,
    DateTime? End,
    string TimeZoneOffset,
    string Sport,
    WorkoutScoreDto Score
) : WithId
{
    public Type Type { get; init; } = Type.Workout;
};

public record WorkoutScoreDto(
    float Strain,
    int AverageHeartRate,
    int MaxHeartRate,
    float Kilojoule,
    float PercentRecorded,
    int? ZoneZeroMilli,
    int? ZoneOneMilli,
    int? ZoneTwoMilli,
    int? ZoneThreeMilli,
    int? ZoneFourMilli,
    int? ZoneFiveMilli
);

[JsonConverter(typeof(StringEnumConverter))]
public enum Type
{
    Cycle,
    Workout,
    Profile
}

public static class SportsLookup
{
    public static string ToSport(this int sportId)
    {
        try
        {
            return _sportsLookup[sportId];
        }
        catch (KeyNotFoundException e)
        {
            return _sportsLookup[-2];
        }
    }
    
    private static Dictionary<int, string> _sportsLookup = new()
    {
        { -2, "FAILED_TO_PARSE" },
        { -1, "Activity" },
        { 0, "Running" },
        { 1, "Cycling" },
        { 16, "Baseball" },
        { 17, "Basketball" },
        { 18, "Rowing" },
        { 19, "Fencing" },
        { 20, "Field Hockey" },
        { 21, "Football" },
        { 22, "Golf" },
        { 24, "Ice Hockey" },
        { 25, "Lacrosse" },
        { 27, "Rugby" },
        { 28, "Sailing" },
        { 29, "Skiing" },
        { 30, "Soccer" },
        { 31, "Softball" },
        { 32, "Squash" },
        { 33, "Swimming" },
        { 34, "Tennis" },
        { 35, "Track & Field" },
        { 36, "Volleyball" },
        { 37, "Water Polo" },
        { 38, "Wrestling" },
        { 39, "Boxing" },
        { 42, "Dance" },
        { 43, "Pilates" },
        { 44, "Yoga" },
        { 45, "Weightlifting" },
        { 47, "Cross Country Skiing" },
        { 48, "Functional Fitness" },
        { 49, "Duathlon" },
        { 51, "Gymnastics" },
        { 52, "Hiking/Rucking" },
        { 53, "Horseback Riding" },
        { 55, "Kayaking" },
        { 56, "Martial Arts" },
        { 57, "Mountain Biking" },
        { 59, "Powerlifting" },
        { 60, "Rock Climbing" },
        { 61, "Paddleboarding" },
        { 62, "Triathlon" },
        { 63, "Walking" },
        { 64, "Surfing" },
        { 65, "Elliptical" },
        { 66, "Stairmaster" },
        { 70, "Meditation" },
        { 71, "Other" },
        { 73, "Diving" },
        { 74, "Operations - Tactical" },
        { 75, "Operations - Medical" },
        { 76, "Operations - Flying" },
        { 77, "Operations - Water" },
        { 82, "Ultimate" },
        { 83, "Climber" },
        { 84, "Jumping Rope" },
        { 85, "Australian Football" },
        { 86, "Skateboarding" },
        { 87, "Coaching" },
        { 88, "Ice Bath" },
        { 89, "Commuting" },
        { 90, "Gaming" },
        { 91, "Snowboarding" },
        { 92, "Motocross" },
        { 93, "Caddying" },
        { 94, "Obstacle Course Racing" },
        { 95, "Motor Racing" },
        { 96, "HIIT" },
        { 97, "Spin" },
        { 98, "Jiu Jitsu" },
        { 99, "Manual Labor" },
        { 100, "Cricket" },
        { 101, "Pickleball" },
        { 102, "Inline Skating" },
        { 103, "Box Fitness" },
        { 104, "Spikeball" },
        { 105, "Wheelchair Pushing" },
        { 106, "Paddle Tennis" },
        { 107, "Barre" },
        { 108, "Stage Performance" },
        { 109, "High Stress Work" },
        { 110, "Parkour" },
        { 111, "Gaelic Football" },
        { 112, "Hurling/Camogie" },
        { 113, "Circus Arts" },
        { 121, "Massage Therapy" },
        { 123, "Strength Trainer" },
        { 125, "Watching Sports" },
        { 126, "Assault Bike" },
        { 127, "Kickboxing" },
        { 128, "Stretching" },
        { 230, "Table Tennis" },
        { 231, "Badminton" },
        { 232, "Netball" },
        { 233, "Sauna" },
        { 234, "Disc Golf" },
        { 235, "Yard Work" },
        { 236, "Air Compression" },
        { 237, "Percussive Massage" },
        { 238, "Paintball" },
        { 239, "Ice Skating" },
        { 240, "Handball" }
    };
}
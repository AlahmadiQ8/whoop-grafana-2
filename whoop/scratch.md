# Notes

## OpenApi Generator

```bash
openapi-generator-cli config-help -g csharp

openapi-generator-cli generate -g csharp -i ./whoop-openapi.yaml -c ./config.json -o ./Testing --global-property supportingFiles=false,apiTests=false,modelTests=false --dry-run
```

```
Whoop.Sdk.sln
appveyor.yml
api/openapi.yaml
.gitignore
git_push.sh
README.md
```

## Adding cosmos necessary permissions

https://techcommunity.microsoft.com/t5/azure-architecture-blog/configure-rbac-for-cosmos-db-with-managed-identity-instead-of/ba-p/3056638

```powershell
az cosmosdb sql role definition create -a "whoop" -g "whoop-grafana-v2" -b @role-definition-ro.json
az cosmosdb sql role definition create -a "whoop" -g "whoop-grafana-v2" -b @role-definition-rw.json

az cosmosdb sql role definition list --account-name "whoop" -g "whoop-grafana-v2"
az cosmosdb sql role assignment create -a "whoop" -g "whoop-grafana-v2" -s "/" -p "68b5c738-954a-479d-b660-244740a04886" -d "4fecc03e-8f97-4b2b-b239-6008aa6c82f1"


az cosmosdb sql role assignment create -a "whoop" -g "whoop-grafana-v2" -s "/" -p "f739f28b-b823-4b3f-b60b-49b56b1eea9e" -d "4fecc03e-8f97-4b2b-b239-6008aa6c82f1"
```

## Kusto Queries

### Table Creation Commands

https://learn.microsoft.com/en-us/kusto/management/update-policy-tutorial?view=azure-data-explorer

```kusto
.create table Raw_Table (RawData: dynamic)

.execute database script <|
    .create-or-alter function Get_Cycles() {
     Raw_Table
        | where RawData.type == 'Cycle'
        | project
            Id = tostring(RawData.id),
            Type = tostring(RawData.type),
            UserId = tostring(RawData.userId),
            CreatedAt = todatetime(RawData.createdAt),
            UpdatedAt = todatetime(RawData.updatedAt),
            Start = todatetime(RawData.start),
            End = todatetime(RawData.end),
            TimeZoneOffset = tostring(RawData.timeZoneOffset),
            SleepId = tostring(RawData.sleepId),
            _ts = tolong(RawData._ts),
            _timestamp = todatetime(RawData.start),
            CycleScore_Strain = toreal(RawData.cycleScore.strain),
            CycleScore_Kilojoule = toreal(RawData.cycleScore.kilojoule),
            CycleScore_AverageHeartRate = toint(RawData.cycleScore.averageHeartRate),
            CycleScore_MaxHeartRate = toint(RawData.cycleScore.maxHeartRate),
            RecoveryScore_UserCalibrating = tobool(RawData.recoveryScore.userCalibrating),
            RecoveryScore_RecoveryScore = toreal(RawData.recoveryScore.recoveryScore),
            RecoveryScore_RestingHeartRate = toreal(RawData.recoveryScore.restingHeartRate),
            RecoveryScore_HrvRmssdMilli = toreal(RawData.recoveryScore.hrvRmssdMilli),
            RecoveryScore_Spo2Percentage = toreal(RawData.recoveryScore.spo2Percentage),
            RecoveryScore_SkinTempCelsius = toreal(RawData.recoveryScore.skinTempCelsius),
            SleepScore_Nap = tobool(RawData.sleepScore.nap),
            SleepScore_Summary_TotalInBedTimeMilli = toint(RawData.sleepScore.sleepStageSummary.totalInBedTimeMilli),
            SleepScore_Summary_TotalAwakeTimeMilli = toint(RawData.sleepScore.sleepStageSummary.totalAwakeTimeMilli),
            SleepScore_Summary_TotalNoDataTimeMilli = toint(RawData.sleepScore.sleepStageSummary.totalNoDataTimeMilli),
            SleepScore_Summary_TotalLightSleepTimeMilli = toint(RawData.sleepScore.sleepStageSummary.totalLightSleepTimeMilli),
            SleepScore_Summary_TotalSlowWaveSleepTimeMilli = toint(RawData.sleepScore.sleepStageSummary.totalSlowWaveSleepTimeMilli),
            SleepScore_Summary_TotalRemSleepTimeMilli = toint(RawData.sleepScore.sleepStageSummary.totalRemSleepTimeMilli),
            SleepScore_Summary_SleepCycleCount = toint(RawData.sleepScore.sleepStageSummary.sleepCycleCount),
            SleepScore_Summary_DisturbanceCount = toint(RawData.sleepScore.sleepStageSummary.disturbanceCount),
            SleepScore_SleepNeeded_BaselineMilli = toreal(RawData.sleepScore.sleepNeeded.baselineMilli),
            SleepScore_SleepNeeded_NeedFromSleepDebtMilli = toreal(RawData.sleepScore.sleepNeeded.needFromSleepDebtMilli),
            SleepScore_SleepNeeded_NeedFromRecentStrainMilli = toreal(RawData.sleepScore.sleepNeeded.needFromRecentStrainMilli),
            SleepScore_SleepNeeded_NeedFromRecentNapMilli = toreal(RawData.sleepScore.sleepNeeded.needFromRecentNapMilli),
            SleepScore_RespiratoryRate = toreal(RawData.sleepScore.respiratoryRate),
            SleepScore_SleepPerformancePercentage = toreal(RawData.sleepScore.sleepPerformancePercentage),
            SleepScore_SleepConsistencyPercentage = toreal(RawData.sleepScore.sleepConsistencyPercentage),
            SleepScore_SleepEfficiencyPercentage = toreal(RawData.sleepScore.sleepEfficiencyPercentage)
 }
    .create-or-alter function Get_Workouts() {
     Raw_Table
        | where RawData.type == 'Workout'
        | project
            Id = tostring(RawData.id),
            Type = tostring(RawData.type),
            UserId = tostring(RawData.userId),
            CreatedAt = todatetime(RawData.createdAt),
            UpdatedAt = todatetime(RawData.updatedAt),
            Start = todatetime(RawData.start),
            End = todatetime(RawData.end),
            TimeZoneOffset = tostring(RawData.timeZoneOffset),
            Sport = tostring(RawData.sport),
            _ts = tolong(RawData._ts),
            _timestamp = todatetime(RawData.start),
            Strain = toreal(RawData.score.strain),
            AverageHeartRate = toint(RawData.score.averageHeartRate),
            MaxHeartRate = toint(RawData.score.maxHeartRate),
            Kilojoule = toreal(RawData.score.kilojoule),
            PercentRecorded = toreal(RawData.score.percentRecorded),
            ZoneZeroMilli = toint(RawData.score.zoneZeroMilli),
            ZoneOneMilli = toint(RawData.score.zoneOneMilli),
            ZoneTwoMilli = toint(RawData.score.zoneTwoMilli),
            ZoneThreeMilli = toint(RawData.score.zoneThreeMilli),
            ZoneFourMilli = toint(RawData.score.zoneFourMilli),
            ZoneFiveMilli = toint(RawData.score.zoneFiveMilli)
 }

.execute database script <|
 .set-or-append Cycles <| Get_Cycles | take 0
 .set-or-append Workouts <| Get_Workouts | take 0

.execute database script <|
  .alter table Cycles policy update "[{\"IsEnabled\":true,\"Source\":\"Raw_Table\",\"Query\":\"Get_Cycles\",\"IsTransactional\":false,\"PropagateIngestionProperties\":true,\"ManagedIdentity\":null}]"
  .alter table Workouts policy update "[{\"IsEnabled\":true,\"Source\":\"Raw_Table\",\"Query\":\"Get_Workouts\",\"IsTransactional\":false,\"PropagateIngestionProperties\":true,\"ManagedIdentity\":null}]"

.create table Raw_Table ingestion json mapping "RawMapping"
\```
[
    {"column":"RawData","path":"$"}
]
\```

.create async materialized-view with(
        backfill=true,
        effectiveDateTime=datetime(2024-05-01),
        MaxSourceRecordsForSingleIngest=3000000,
        Concurrency=2
) CyclesDebup on table Cycles
{
    table('Cycles')
    | summarize arg_max(_ts, *) by Id
}

.create async materialized-view with(
        backfill=true,
        effectiveDateTime=datetime(2024-05-01),
        MaxSourceRecordsForSingleIngest=3000000,
        Concurrency=2
) WorkoutsDebup on table Workouts
{
    table('Workouts')
    | summarize arg_max(_ts, *) by Id
}

```

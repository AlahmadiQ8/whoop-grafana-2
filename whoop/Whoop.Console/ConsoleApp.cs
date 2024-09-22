using Whoop.Core;
using Whoop.Core.Services;

namespace Whoop.Console;

public class ConsoleApp(
    ProfileService profileService,
    CyclesService cyclesService,
    RecoveryService recoveryService,
    SleepService sleepService,
    WorkoutService workoutService)
{
    private const string UserId = "18435265";

    public async Task Run(string[] args)
    {
        // await profileService.UpdateTokenAsync(UserId);
        // await cyclesService.UpdateCyclesAsync(UserId);
        // await recoveryService.UpdateRecoveriesAsync(UserId);
        // await sleepService.UpdateSleepAsync(UserId);
        await workoutService.UpdateWorkoutsAsync(new OrchestratorInput
        {
            UserId = UserId,
            Start = DateTime.Parse("2024-09-17T00:00:00Z"),
            End = DateTime.Parse("2024-09-19T00:00:00Z"),
        });
    }
}
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
        await profileService.UpdateTokenAsync(UserId);
        await cyclesService.UpdateCyclesAsync(UserId);
        await recoveryService.UpdateRecoveriesAsync(UserId);
        await sleepService.UpdateSleepAsync(UserId);
        await workoutService.UpdateWorkoutsAsync(UserId);
    }
}
using Microsoft.Extensions.Logging;
using Whoop.Core.Services;

namespace Whoop.Console;

public class ConsoleApp(
    ProfileService profileService,
    CyclesService cyclesService,
    RecoveryService recoveryService)
{
    private const string UserId = "18435265";

    public async Task Run(string[] args)
    {
        // await profileService.UpdateTokenAsync(UserId);
        var res = await cyclesService.UpdateCyclesAsync(UserId);
        System.Console.WriteLine($"cycleIds: {string.Join(',', res)}");
        await recoveryService.UpdateRecoveriesAsync(UserId, res);
    }
}

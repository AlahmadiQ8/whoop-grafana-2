using Whoop.Core.Services;

namespace Whoop.Console;

public class ConsoleApp(
    ProfileService profileService,
    CyclesService cyclesService)
{
    private const string UserId = "18435265";

    public async Task Run(string[] args)
    {
        await cyclesService.UpdateCyclesAsync(UserId);
    }
}

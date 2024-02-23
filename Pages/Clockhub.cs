using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ClockHub : Hub
{
    public async Task SendTime(int time)
    {
        await Clients.All.SendAsync("UpdateTime", time);
    }
}
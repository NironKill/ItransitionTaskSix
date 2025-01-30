using Microsoft.AspNetCore.SignalR;

namespace JointPresentation.Application.Services.Hubs
{
    public class PresentationHub : Hub
    {
        public async Task Draw(string presentationId, string drawData)
        {
            await Clients.OthersInGroup(presentationId).SendAsync("UpdateDrawing", drawData);
        }

        public async Task<string> JoinBoard(string presentationId, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, presentationId);
            await Clients.OthersInGroup(presentationId).SendAsync("ReceiveUserJoinInfo", userName);
            return $"Added to group with connection id {Context.ConnectionId} in group {presentationId}";
        }
    }
}

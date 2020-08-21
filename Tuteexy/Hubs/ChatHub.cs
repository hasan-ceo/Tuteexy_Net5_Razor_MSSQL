using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tuteexy.Hubs
{
    public class ChatHub : Hub
    {

        public async Task AddToGroup(string groupName, string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("ReceiveMessage",
                $"{username} has" +
                $" joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName, string username)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("ReceiveMessage",
                $"{username} has" +
                $" left the group {groupName}.");
        }

        public async Task SendMessageGroup(string groupName, string username, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", username, message);
        }

        public async Task TypingGroup(string groupName, string username)
        {
            await Clients.Group(groupName).SendAsync("TypingMessage", username);
        }


        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }

        public async Task Typing(string user)
        {
            await Clients.All.SendAsync("TypingMessage", user);
        }
        //public async Task SendMessage(string user, string message)
        //{

        //        await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        //public Task SendMessageToAll(string message)
        //{
        //    return Clients.All.SendAsync("ReceiveMessage", message);
        //}

        //public Task SendMessageToCaller(string message)
        //{
        //    return Clients.Caller.SendAsync("ReceiveMessage", message);
        //}

        //public Task SendMessageToUser(string connectionId, string message)
        //{
        //    return Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        //}

        //public Task JoinGroup(string group)
        //{
        //    return Groups.AddToGroupAsync(Context.ConnectionId, group);
        //}

        //public Task SendMessageToGroup(string group, string message)
        //{
        //    return Clients.Group(group).SendAsync("ReceiveMessage", message);
        //}

        //public override async Task OnConnectedAsync()
        //{
        //    await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
        //    await base.OnConnectedAsync();
        //}

        //public override async Task OnDisconnectedAsync(Exception ex)
        //{
        //    await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
        //    await base.OnDisconnectedAsync(ex);
        //}



        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}



        //public void BroadcastMessage(string name, string message)
        //{
        //    Clients.All.SendAsync("broadcastMessage", name, message);
        //}

        //public void Echo(string name, string message)
        //{
        //    Clients.Client(Context.ConnectionId).SendAsync("echo", name, message + " (echo from server)");
        //}

        //public async Task JoinGroup(string name, string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        //    await Clients.Group(groupName).SendAsync("echo","",$"{name} joined {groupName}");
        //}

        //public async Task LeaveGroup(string name, string groupName)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //    await Clients.Client(Context.ConnectionId).SendAsync("echo", "", $"{name} leaved {groupName}");
        //    await Clients.Group(groupName).SendAsync("echo",  $"{name} leaved {groupName}");
        //}

        //public void SendGroup(string name, string groupName, string message)
        //{
        //    Clients.Group(groupName).SendAsync("echo", name, message);
        //}

        //public void SendGroups(string name, IReadOnlyList<string> groups, string message)
        //{
        //    Clients.Groups(groups).SendAsync("echo", name, message);
        //}

        //public void SendGroupExcept(string name, string groupName, IReadOnlyList<string> connectionIdExcept, string message)
        //{
        //    Clients.GroupExcept(groupName, connectionIdExcept).SendAsync("echo", name, message);
        //}

        //public void SendUser(string name, string userId, string message)
        //{
        //    Clients.User(userId).SendAsync("echo", name, message);
        //}

        //public void SendUsers(string name, IReadOnlyList<string> userIds, string message)
        //{
        //    Clients.Users(userIds).SendAsync("echo", name, message);
        //}
    }
}
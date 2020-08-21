"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("echo", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " : " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

// Group join/leave operations
document.getElementById('joinButton').addEventListener('click', function (event) {
    var uname = document.getElementById("username").value;
    var gname = document.getElementById("groupname").value;
    if (groupname) {
        connection.send('joingroup', uname, gname);
    }
    event.preventDefault();
});

document.getElementById('leaveButton').addEventListener('click', function (event) {
    var uname = document.getElementById("username").value;
    var gname = document.getElementById("groupname").value;
    if (gname) {
        connection.send('leavegroup', uname, gname);
    }
    event.preventDefault();
});

document.getElementById('sendButton').addEventListener('click', function (event) {
    var uname = document.getElementById("username").value;
    var gname = document.getElementById("groupname").value;
    var msg = document.getElementById("message").value;
    if (gname && msg) {
        connection.send('sendgroup', uname, gname, msg);
    }
    message.value = '';
    message.focus();
    event.preventDefault();
});

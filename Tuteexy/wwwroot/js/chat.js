"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
let scroller = document.querySelector('#scroller');
let anchor = document.querySelector('#anchor');

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msgtext = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " : " + msgtext;

    let msg = document.createElement('div');
    msg.className = 'messagebk';
    msg.innerText = encodedMsg;
    scroller.insertBefore(msg, anchor);

    //var li = document.createElement("div");
    //li.className = "messagebk";
    //li.innerText = encodedMsg;
    //document.getElementById("messagesList").insertBefore(msg, anchor);
    //document.getElementById("messagesList").appendChild(li);
    //document.getElementById("messagesList").scrollTop = document.getElementById("messagesList").scrollHeight;
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

var callback = function () {
    // Handler when the DOM is fully loaded
    
};

if (
    document.readyState === "complete" ||
    (document.readyState !== "loading" && !document.documentElement.doScroll)
) {
    callback();
} else {
    document.addEventListener("DOMContentLoaded", callback);
}




document.getElementById('joinButton').addEventListener('click', function (event) {
    var uname = document.getElementById("username").value;
    var gname = document.getElementById("groupname").value;
    if (groupname) {
        connection.send('AddToGroup', gname, uname);
    }
    event.preventDefault();
});

document.getElementById('leaveButton').addEventListener('click', function (event) {
    var uname = document.getElementById("username").value;
    var gname = document.getElementById("groupname").value;
    if (gname) {
        connection.send('RemoveFromGroup', gname, uname);
    }
    event.preventDefault();
});

document.getElementById('sendButton').addEventListener('click', function (event) {
    var uname = document.getElementById("username").value;
    var gname = document.getElementById("groupname").value;
    var msg = document.getElementById("message").value;
    if (gname && msg) {
        connection.send('SendMessageGroup', gname, uname, msg);
    }
    message.value = '';
    message.focus();
    event.preventDefault();
});

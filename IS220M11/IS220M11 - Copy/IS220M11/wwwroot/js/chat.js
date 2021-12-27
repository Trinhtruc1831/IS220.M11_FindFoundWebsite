var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

/*Disable the send button until connection is established.*/


connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    var a = document.createElement("a");
    var ul = document.getElementById("messagesList");
    var today = new Date();
    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var dateTime = '['+date + ' ' + time+'] ';
    a.textContent = `${user}:`;
    li.className = "li-chat";
    a.href = ""
    li.innerHTML = dateTime;
    li.appendChild(a);
    li.innerHTML += ` ${message}`;
    ul.insertBefore(li, ul.childNodes[0]);
    
   

});



connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});
$("#chatForm").first().submit(function ChatRun(event) {
    event.preventDefault();
    var user = document.getElementById("userInput").innerHTML;
    var message = document.getElementById("messageInput").value;

    // Add to hiden form to add into database
    var hidMess = document.getElementById("mess");
    hidMess.value = message;
    var hidUser = document.getElementById("user");
    hidUser.value = user;
    var hidDay = document.getElementById("day");
    var today = new Date();
    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var dateTime = '['+date + ' ' + time+'] ';
    hidDay.value = dateTime;
    event.preventDefault();

    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

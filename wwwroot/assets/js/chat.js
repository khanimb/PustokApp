using Microsoft.A// Bu JavaScript kodudur, brauzer üçün.
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (user, message) {
    // Mesaj gələndə ekrana çıxarır
    alert(user + " deyir ki: " + message);
});

connection.start().then(function () {
    console.log("SignalR bağlantısı quruldu!");
}).catch(function (err) {
    return console.error(err.toString());
});

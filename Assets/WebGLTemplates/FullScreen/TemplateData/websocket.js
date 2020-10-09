setInterval(() => {
    gameInstance.SendMessage('JSCSharpSocket', 'MessageReceivedFromServer', 'My test message sent from JS to C#');
}, 5000);
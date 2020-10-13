let endpoint = 'localhost:5000'; //35.158.139.97:5000

// setInterval(() => {
//     gameInstance.SendMessage('JSCSharpSocket', 'MessageReceivedFromServer', 'My test message sent from JS to C#');
// }, 5000);

let websocket;
window.onload = function() {
    websocket = new WebSocket(`ws://${endpoint}`); //ws://35.158.139.97:5000 localhost
    websocket.onopen = function(evt) { onOpen(evt) };
    websocket.onclose = function(evt) { onClose(evt) };
    websocket.onmessage = function(evt) { onMessage(evt) };
    websocket.onerror = function(evt) { onError(evt) };
}

function onOpen(evt) {
    console.log("CONNECTED");
    websocket.send("hello");
    setInterval(() => {
        websocket.send("ping");
    }, 1000);
}


function onClose(evt) {
    console.log("DISCONNECTED");
}

function onMessage(evt) {
    gameInstance.SendMessage('JSCSharpSocket', 'MessageReceivedFromServer', JSON.stringify(evt.data));
    console.log('< ' + evt.data);
}

function onError(evt) {
    console.log('<span style="color: red;">ERROR:</span> ' + evt.data);
}

function SendMessageToServerFromUnity(msg) {
    console.log(`SendMessageToServerFromUnity(${msg})`);
    websocket.send(msg);
}
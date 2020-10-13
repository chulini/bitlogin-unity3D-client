mergeInto(LibraryManager.library, {

  SendMessageToServer: function (str) {
    console.log(Pointer_stringify(str));
    SendMessageToServerFromUnity(Pointer_stringify(str));
  }

  
})
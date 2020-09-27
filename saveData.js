function saveData() {

    console.log("Hello World");
    var elements = document.getElementById("userInfo").elements;
    for(i = 0; i < elements.length; i++) {
        var key = elements[i].name;
        var value = elements[i].value;
        chrome.storage.sync.set({key: value}, function() {
            console.log('Value is set to ' + value);
          });
     }  

}
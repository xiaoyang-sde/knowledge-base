console.log("content script run!");


//get the information from user input
const allInput = document.getElementsByTagName('input');
console.log(allInput[3].value);
for (i = 0; i < allInput.length; i++) {
    //First Name
    if (allInput[i].autocomplete == "given-name") {
        chrome.storage.sync.get('firstName', function(data) {
            allInput[i].value = data.value;
        });
    }
    //Last Name
    if (allInput[i].autocomplete == "family-name") {
        chrome.storage.sync.get('lastName', function(data) {
            allInput[i].value = data.value;
        });
    }
    //Email
    if (allInput[i].autocomplete == "email") {
        chrome.storage.sync.get('email', function(data) {
            allInput[i].value = data.value;
        });
    }
    //address line
    if (allInput[i].autocomplete == "street-address") {
        chrome.storage.sync.get('address', function(data) {
            allInput[i].value = data.value;
        });
    }
    //zip-code
    if (allInput[i].autocomplete == "postal-code") {
        chrome.storage.sync.get('zip', function(data) {
            allInput[i].value = data.value;
        });
    }
}


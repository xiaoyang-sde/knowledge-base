console.log("content script run!");


//get the information from user input
const allInput = document.getElementsByTagName('input');
console.log(allInput[3].value);
for (const element of allInput) {
    //First Name
    if (element.autocomplete == "given-name") {
        chrome.storage.sync.get('firstName', function(data) {
            element.value = data.firstName;
        });
    }
    //Last Name
    if (element.autocomplete == "family-name") {
        chrome.storage.sync.get('lastName', function(data) {
            element.value = data.lastName;
        });
    }
    //Email
    if (element.autocomplete == "email") {
        chrome.storage.sync.get('email', function(data) {
            element.value = data.email;
        });
    }
    //address line
    if (element.autocomplete == "street-address") {
        chrome.storage.sync.get('address', function(data) {
            element.value = data.address;
        });
    }
    //zip-code
    if (element.autocomplete == "postal-code") {
        chrome.storage.sync.get('zip', function(data) {
            element.value = data.zip;
        });
    }
}


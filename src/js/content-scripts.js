console.log("content script run!");


//get the information from user input
const allInput = document.querySelectorAll('input[type=text]');

//asscociated the labels with the input in each input section
const labels = document.querySelectorAll('label');
console.log(labels);
for (i = 0; i < labels.length; i++) {
    if (labels[i].htmlFor != '') {
         var elem = document.getElementById(labels[i].htmlFor);
         if (elem)
            elem.label = labels[i];         
    }
}
console.log(allInput);
//The most simple fields that can be completed by auto-fill
for (const element of allInput) {
    if (element.label == undefined) {
        console.log(element.labels[0].textContent);
        //First Name
        if (element.autocomplete == "given-name" || element.labels[0].textContent.toLowerCase().includes("first")) {
            chrome.storage.sync.get('firstName', function(data) {
                element.value = data.firstName;
            });
        }
        //Last Name
        if (element.autocomplete == "family-name" || element.labels[0].textContent.toLowerCase().includes("last")) {
            chrome.storage.sync.get('lastName', function(data) {
                element.value = data.lastName;
            });
        }
        //Email
        if (element.autocomplete == "email" || element.labels[0].textContent.toLowerCase().includes("email")) {
            chrome.storage.sync.get('email', function(data) {
                element.value = data.email;
            });
        }
        //address line
        if (element.autocomplete == "street-address" || element.labels[0].textContent.toLowerCase().includes("address")) {
            chrome.storage.sync.get('address', function(data) {
                element.value = data.address;
            });
        }
        //zip-code
        if (element.autocomplete == "postal-code" || element.labels[0].textContent.toLowerCase().includes("code")) {
            chrome.storage.sync.get('zip', function(data) {
                element.value = data.zip;
            });
        }

        //phone
        if (element.autocomplete == "tel" || element.labels[0].textContent.toLowerCase().includes("phone")) {
            chrome.storage.sync.get('phone', function(data) {
                element.value = data.phone;
            });
        }

        //the following parts that cannot be simply completed by html attributes autocomplete
        //linkedin
        if (element.labels[0].textContent.toLowerCase().includes("linkedin")) {
            chrome.storage.sync.get('linkedin', function(data) {
                element.value = data.phone;
            });
        }

        //work authorization
        if (element.labels[0].textContent.toLowerCase().includes("authori")) {
            chrome.storage.sync.get('authorization', function(data) {
                element.value = data.authorization;
            });
        }

        //website
        if (element.labels[0].textContent.toLowerCase().includes("website")) {
            chrome.storage.sync.get('portfolio', function(data) {
                element.value = data.portfolio;
            });
        }
        
        //sponsorship
        if (element.labels[0].textContent.toLowerCase().includes("sponsorship")) {
            chrome.storage.sync.get('sponsorship', function(data) {
                element.value = data.sponsorship;
            });
        }
    } else {
        //First Name
        if (element.autocomplete == "given-name" || element.label.textContent.toLowerCase().includes("first")) {
            chrome.storage.sync.get('firstName', function(data) {
                element.value = data.firstName;
            });
        }
        //Last Name
        if (element.autocomplete == "family-name" || element.label.textContent.toLowerCase().includes("last")) {
            chrome.storage.sync.get('lastName', function(data) {
                element.value = data.lastName;
            });
        }
        //Email
        if (element.autocomplete == "email" || element.label.textContent.toLowerCase().includes("email")) {
            chrome.storage.sync.get('email', function(data) {
                element.value = data.email;
            });
        }
        //address line
        if (element.autocomplete == "street-address" || element.label.textContent.toLowerCase().includes("address")) {
            chrome.storage.sync.get('address', function(data) {
                element.value = data.address;
            });
        }
        //zip-code
        if (element.autocomplete == "postal-code" || element.label.textContent.toLowerCase().includes("code")) {
            chrome.storage.sync.get('zip', function(data) {
                element.value = data.zip;
            });
        }

        //phone
        if (element.autocomplete == "tel" || element.label.textContent.toLowerCase().includes("phone")) {
            chrome.storage.sync.get('phone', function(data) {
                element.value = data.phone;
            });
        }

        //the following parts that cannot be simply completed by html attributes autocomplete

        //linkedin
        if (element.label.textContent.toLowerCase().includes("linkedin")) {
            chrome.storage.sync.get('linkedin', function(data) {
                element.value = data.linkedin;
            });
        }

        //work authorization
        if (element.label.textContent.toLowerCase().includes("authori")) {
            chrome.storage.sync.get('authorization', function(data) {
                element.value = data.authorization;
            });
        }

        //website
        if (element.label.textContent.toLowerCase().includes("website")) {
            chrome.storage.sync.get('portfolio', function(data) {
                element.value = data.portfolio;
            });
        }
        
        //sponsorship
        if (element.label.textContent.toLowerCase().includes("sponsorship")) {
            chrome.storage.sync.get('sponsorship', function(data) {
                element.value = data.sponsorship;
            });
        }
    }
    
}


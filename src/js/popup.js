window.onload = function() {
  //get the information from user input
  var testing = document.getElementById("infoSubmit");
  testing.addEventListener("click", saveData, false);

  
}

function saveData(e) {
  const { elements } = document.getElementById('userInfo');
  for (i = 0; i < elements.length; i++) {
    var key = elements[i].id;
    var { value } = elements[i];
    chrome.storage.sync.set({[key]: value });
  }
  alert("Saved Successfully.")
}

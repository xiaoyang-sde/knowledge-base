window.onload = function() {
  var testing = document.getElementById("infoSubmit");
  testing.addEventListener("click", saveData, false);
}

function saveData(e) {
  const { elements } = document.getElementById('userInfo');
  for (i = 0; i < elements.length; i++) {
    const key = elements[i].name;
    var { value } = elements[i];
    chrome.storage.sync.set({ key: value }, () => {
      alert("Successful");
    });
  }
}

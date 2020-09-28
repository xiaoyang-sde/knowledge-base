function saveData() {
  console.log('Hello World');
  const { elements } = document.getElementById('userInfo');
  for (i = 0; i < elements.length; i++) {
    const key = elements[i].name;
    var { value } = elements[i];
    chrome.storage.sync.set({ key: value }, () => {
      console.log(`Value is set to ${value}`);
    });
  }
}

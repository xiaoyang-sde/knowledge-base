const profileForm = document.forms.profile;
let saved = true;

profileForm.addEventListener('change', () => {
  saved = false;
});

window.addEventListener('beforeunload', (event) => {
  if (!saved) {
    event.preventDefault();
    event.returnValue = '';
  }
});

profileForm.addEventListener('submit', (event) => {
  event.preventDefault();
  for (const element of event.target.elements) {
    let value;
    if (element.type === 'text') {
      value = element.value;
    } else if (element.tagName === 'select') {
      value = element.selectedIndex;
    }
    chrome.storage.sync.set({
      [element.id]: value,
    });
  }
  saved = true;
});

for (const element of profileForm.elements) {
  if (element.type === 'text') {
    chrome.storage.sync.get([element.id],(result) => {
      if (result[element.id]) {
        element.value = result[element.id];
      }
    });
  } else if (element.tagName === 'select') {
    chrome.storage.sync.get([element.id],(result) => {
      if (result[element.id]) {
        element.selectedIndex = result[element.id];
      }
    });
  }
}

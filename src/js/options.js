let profileForm = document.forms.profile;
let saved = true;

profileForm.addEventListener('change', event => {
  saved = false;
});

window.addEventListener('beforeunload', event => {
  if (!saved) {
    event.preventDefault();
    event.returnValue = '';
  }
});

profileForm.addEventListener('submit', event => {
  event.preventDefault();
  for (const element of event.target.elements) {
    let value;
    if (element.type === 'text' || element.type === 'file') {
      value = element.value;
    } else if (element.tagName === 'select') {
      value = element.selectedIndex;
    }
    chrome.storage.sync.set({
      [element.id]: value
    });
  }
  saved = true;
});

for (const element of profileForm.elements) {
  let value;
  if (element.type === 'text' || element.type === 'file') {
    value = element.value;
  } else if (element.tagName === 'select') {
    value = element.selectedIndex;
  }

  chrome.storage.sync.get(
    [element.id],
    (savedData) => {
      if (element.id) {
        value = savedData;
      }
    }
  )
}

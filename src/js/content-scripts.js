function fill() {
  const labelMap = {
    first: 'firstName',
    last: 'lastName',
    email: 'email',
    address: 'address',
    zip: 'zip',
    postal: 'zip',
    phone: 'phone',
    linkedin: 'linkedin',
    authori: 'authorization',
    sponsor: 'sponsorship',
    website: 'portfolio',
    portfolio: 'portfolio',
  };

  const inputs = document.querySelectorAll('input');
  for (const input of inputs) {
    if (!input.labels) {
      continue;
    }
  
    const label = input.labels[0]?.textContent?.toLowerCase();
    for (const key in labelMap) {
      if (label && label.includes(key)) {
        const storageKey = labelMap[key];
        chrome.storage.sync.get(storageKey, (result) => {
          if (result[storageKey] === undefined) {
            return;
          }
          input.value = result[storageKey];
        });
      }
    }
  }
}

chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
  if (request.action !== 'fill') {
    return;
  }

  chrome.storage.sync.get('status', (result) => {
    let status = result['status']
    if (status === undefined) {
      status = true;
    }
    if (!status) {
      return;
    }
    fill();
    chrome.storage.sync.get('count', (result) => {
      const count = result['count'];
      chrome.storage.sync.set({
        count: count + 1,
      });
    });
  });
});

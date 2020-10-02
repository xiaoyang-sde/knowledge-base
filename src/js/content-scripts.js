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
    const label = input.labels[0]?.textContent?.toLowerCase();
    for (const key in labelMap) {
      if (label.includes(key)) {
        chrome.storage.sync.get(labelMap[key], (result) => {
          if (result[key] === undefined) {
            return;
          }
          input.value = result[key];
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
    sendResponse({
      action: 'filled',
    });
  });
});

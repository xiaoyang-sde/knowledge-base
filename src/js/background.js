function enableContextItem() {
  chrome.contextMenus.update('jobAutoFill', {
    visible: true,
  });
}

function disableContextItem() {
  chrome.contextMenus.update('jobAutoFill', {
    visible: false,
  });
}

chrome.contextMenus.removeAll(() => {
  chrome.contextMenus.create({
    id: 'jobAutoFill',
    title: 'Fill Job Application',
    contexts: ['page'],
  });
});

chrome.contextMenus.onClicked.addListener((data) => {
  if (data.menuItemId === 'jobAutoFill') {
    chrome.tabs.query({ active: true, currentWindow: true }, (tabs) => {
      chrome.tabs.sendMessage(tabs[0].id, { action: 'fill' });
    });
  }
});

chrome.storage.sync.get('status', (result) => {
  const status = result['status'];
  if (!status) {
    disableContextItem();
  }
});

function sendJobMessage() {
  chrome.tabs.query({ active: true, currentWindow: true }, (tabs) => {
    chrome.tabs.sendMessage(tabs[0].id, { action: 'fill' });
  });
}

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

chrome.contextMenus.create({
  id: 'jobAutoFill',
  title: 'Fill Job Application',
  contexts: ['page'],
  onclick: sendJobMessage,
});

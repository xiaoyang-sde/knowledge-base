const countBadge = document.getElementById('count');
const toggleButton = document.getElementById('toggle');
const optionsButton = document.getElementById('options');

optionsButton.addEventListener('click', () => {
  chrome.runtime.openOptionsPage();
});

toggleButton.addEventListener('click', () => {
  chrome.store.sync.get('status', (result) => {
    previousStatus = result['status'];
    if (previousStatus === undefined) {
      previousStatus = true;
    }

    const backgroundPage = chrome.extension.getBackgroundPage();
    if (previousStatus) {
      toggleButton.textContent = 'Enable Auto Fill';
      backgroundPage.disableContextItem();
    } else {
      toggleButton.textContent = 'Disable Auto Fill';
      backgroundPage.enableContextItem();
    }

    chrome.store.sync.set({
      status: !previousStatus,
    });
  });
});

chrome.runtime.onMessage.addListener((request) => {
  if (request.action === 'filled') {
    chrome.store.sync.get('count', (result) => {
      previousCount = result['previousCount'];
      countBadge.textContent = previousCount + 1;
      chrome.store.sync.set({
        count: previousCount + 1,
      });
    });
  }
});

chrome.store.sync.get('count', (result) => {
  let count = result['count'];
  if (count === undefined) {
    chrome.store.sync.set({
      count: 0,
    });
  } else {
    countBadge.textContent = count;
  }
});

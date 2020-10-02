const countBadge = document.getElementById('count');
const toggleButton = document.getElementById('toggle');
const optionsButton = document.getElementById('options');

optionsButton.addEventListener('click', () => {
  chrome.runtime.openOptionsPage();
});

toggleButton.addEventListener('click', () => {
  chrome.storage.sync.get('status', (result) => {
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

    chrome.storage.sync.set({
      status: !previousStatus,
    });
  });
});

chrome.storage.sync.get('count', (result) => {
  const count = result['count'];
  console.log(count);
  if (count === undefined) {
    chrome.storage.sync.set({
      count: 0,
    });
  } else {
    countBadge.textContent = count;
  }
});

chrome.storage.sync.get('status', (result) => {
  const status = result['status'];
  if (status) {
    toggleButton.textContent = 'Disable Auto Fill';
  } else {
    toggleButton.textContent = 'Enable Auto Fill';
  }
});

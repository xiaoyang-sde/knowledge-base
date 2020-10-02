let countBadge = document.getElementById('count');
let toggleButton = document.getElementById('toggle');

toggleButton.addEventListener('click', event => {
  chrome.store.sync.get('status', previousStatus => {
    if (previousStatus === undefined) {
      previousStatus = true;
    }

    if (previousStatus) {
      toggle.textContent = "Enable Auto Fill";
    } else {
      toggle.textContent = "Disable Auto Fill";
    }

    chrome.store.sync.set({
      status: !previousStatus
    });
  })
});

chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
    if (request.action == "filled") {
      chrome.store.sync.get('count', (previousCount) => {
        countBadge.textContent = previousCount + 1;
        chrome.store.sync.set({
          count: previousCount + 1
        });
      });
    }
});

chrome.store.sync.get('count', count => {
  if (count === undefined) {
    chrome.store.sync.set({
      count: 0
    });
  } else {
    countBadge.textContent = count;
  }
});

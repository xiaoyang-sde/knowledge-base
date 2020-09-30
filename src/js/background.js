
//Right click to trigger auto-fill in context menu

//put the option into the context menu
var contextMenuItem = {
  "id": "jobAutoFill",
  "title": "Fill Job Application",
  "contexts": ["all"]
};
chrome.contextMenus.create(contextMenuItem);

//implement the onclick event on the context menu
chrome.contextMenus.onClicked.addListener(function(clickData) {
  if (clickData.menuItemId == "jobAutoFill") {
    alert("Testing the Onclicked Event");
  }
    
});
var refreshing = false;

function hideAddress() {
	setTimeout(function() { window.scrollTo(0, 1) }, 100);
}

function gotoPage(pageType, guid, totalPages) {
  var pageNumber = prompt("Enter a page between 1 and " + totalPages, "");
  if (pageNumber !== null) {
  	if (pageType == "forum") {
	  	var baseurl = "forumdisplay.aspx?f=" + guid + "&page=";
  	} else if (pageType == "thread") {
	  	var baseurl = "showthread.aspx?t=" + guid + "&page=";
  	}
    location.href = baseurl + pageNumber;
  } else {
    return false;
  }
}

function searchPrompt(q) {
  var query = prompt("Enter a search term", q);
  if (query !== null) {
    location.href = "search.php?q=" + query;
  } else {
    return false;
  }
}

function viewImage(img) {
	window.open(img.src, "", "location=1,status=1,scrollbars=1,width=320,height=480");
}

function viewCode(code) {
	var form = document.createElement("form");
	form.setAttribute("method", "post");
	form.setAttribute("action", "view.php");
	form.setAttribute("target", "formresult");
	var hiddenField = document.createElement("textarea");
	hiddenField.setAttribute("name", "code");
	hiddenField.setAttribute("style", "display: none;");
	form.appendChild(hiddenField);
	document.body.appendChild(form);
	hiddenField.value = code.innerText;
	form.submit();
}

function showImage(anchorTag) {
	imgHref = anchorTag.href.split("=");
	anchorTag.className = '';
	anchorTag.innerHTML = '<img src="' + imgHref[1] + '" class="messagePhoto">';
	anchorTag.onclick = '';
	anchorTag.href = 'view.php?img=' + imgHref[1];
	return false;
}

function submitSetting(type) {
  if (type == "showAvatars") {
    if (document.getElementById("showAvatarsInput").value == "true") {
      document.getElementById("showAvatarsInput").value = "false";
    } else {
      document.getElementById("showAvatarsInput").value = "true";
    }
  } else if (type == "showImages") {
    if (document.getElementById("showImagesInput").value == "true") {
      document.getElementById("showImagesInput").value = "false";
    } else {
      document.getElementById("showImagesInput").value = "true";
    }
  } else if (type == "showStickies") {
    if (document.getElementById("showStickiesInput").value == "true") {
      document.getElementById("showStickiesInput").value = "false";
    } else {
      document.getElementById("showStickiesInput").value = "true";
    }
  } else if (type == "skipToLastPost") {
    if (document.getElementById("skipToLastPostInput").value == "true") {
      document.getElementById("skipToLastPostInput").value = "false";
    } else {
      document.getElementById("skipToLastPostInput").value = "true";
    }
  } else if (type == "showSmallText") {
    if (document.getElementById("showSmallTextInput").value == "true") {
      document.getElementById("showSmallTextInput").value = "false";
    } else {
      document.getElementById("showSmallTextInput").value = "true";
    }
  } else if (type == "showThreadDates") {
    if (document.getElementById("showThreadDatesInput").value == "true") {
      document.getElementById("showThreadDatesInput").value = "false";
    } else {
      document.getElementById("showThreadDatesInput").value = "true";
    }
  } else if (type == "showInterfaceGfx") {
    if (document.getElementById("showInterfaceGfxInput").value == "true") {
      document.getElementById("showInterfaceGfxInput").value = "false";
    } else {
      document.getElementById("showInterfaceGfxInput").value = "true";
    }
 	} else if (type == "invertColors") {
    if (document.getElementById("invertColorsInput").value == "true") {
      document.getElementById("invertColorsInput").value = "false";
    } else {
      document.getElementById("invertColorsInput").value = "true";
    }
  }
  document.getElementById("settingsForm").submit();
  return false;
}

function timezoneSet(zone) {
	document.getElementById("timezoneInput").value = zone;
	document.getElementById("timezoneForm").submit();
	return false;
}

function fontsizeSet(size) {
	document.getElementById("fontsizeInput").value = size;
	document.getElementById("fontsizeForm").submit();
	return false;
}

function insertBBCode(codeType) {
	var startTag;
	var endTag;
	switch (codeType) { 
		case "bold":
		startTag = "[b]";
		endTag = "[/b]";
		break;

		case "spoiler":
		startTag = "[spoiler]";
		endTag = "[/spoiler]";
		break;

		case "quote":
		startTag = "[quote=]";
		endTag = "[/quote]";
		break;

		case "image":
		startTag = "[img]";
		endTag = "[/img]";
		break;

		case "code":
		startTag = "[code]";
		endTag = "[/code]";
		break;
	}
var m = document.getElementById('preXsltContent_message');
  var selectedText = false;
  // IE version
  if (document.selection != undefined) {
    m.focus();
    var sel = document.selection.createRange();
    selectedText = startTag + sel.text + endTag;
    sel.text = selectedText;
  }

  // Mozilla version
  else if (m.selectionStart != undefined)
  {
    var startPos = m.selectionStart;
    var endPos = m.selectionEnd;
    selectedText = startTag + m.value.substring(startPos, endPos) + endTag;
    m.value = m.value.substring(0, startPos) + selectedText + m.value.substring(endPos, m.value.length);
  }

  if (selectedText == false) {

  	m.value += startTag + endTag;

  }

  return false;
}

function insertAtCursor(myValue) {
    m = document.getElementById("preXsltContent_message");
	if (document.selection) {
		m.focus();
		sel = document.selection.createRange();
		sel.text = myValue;
	} else if (m.selectionStart || m.selectionStart == '0') {
		var startPos = m.selectionStart;
		var endPos = m.selectionEnd; 
		m.value = m.value.substring(0, startPos)+ myValue+ m.value.substring(endPos, m.value.length); 
	} else {
		m.value += myValue; 
	}
	return false;
}

function insertBold() {
	insertBBCode('bold');
}

function insertSpoiler() {
	insertBBCode('spoiler');
}

function insertImage() {
	insertBBCode('image');
}

function insertQuote() {
	insertBBCode('quote');
}
function insertCode() {
	insertBBCode('code');
}

function insertURL() {
	var url = prompt('Enter URL of link', '');
	if (url != null) {
		if (url.substr(0, 7) != 'http://') {
			url = 'http://' + url;
		}
		var desc = prompt('Enter description or cancel to use url', '');
		if (desc != null) {
			insertAtCursor('[url=' + url + ']' + desc + '[/url]');
		} else {
			insertAtCursor('[url=' + url + ']' + url + '[/url]');
		}
	}
}

function insertList() {
	var firstItem = prompt('Enter first item', '');
	if (firstItem != null) {
		var allItems = '\n[list]\n[*]' + firstItem;
		var newItem = prompt('Enter next item', '');
		while (newItem != null) {
			allItems = allItems + '\n[*]' + newItem;
			newItem = prompt('Enter next item', '');
		}
		allItems = allItems + '\n[/list]\n';
		insertAtCursor(allItems);
	}
}

function insertOrderList() {
	var firstItem = prompt('Enter first item', '');
	if (firstItem != null) {
		var allItems = '\n[list=1]\n[*]' + firstItem;
		var newItem = prompt('Enter next item', '');
		while (newItem != null) {
			allItems = allItems + '\n[*]' + newItem;
			newItem = prompt('Enter next item', '');
		}
		allItems = allItems + '\n[/list]\n';
		insertAtCursor(allItems);
	}
}

function removeQuote() {
	document.getElementById("quoteExample").style.display = 'none';
	document.getElementById("quote").removeAttribute('name');
}

function editQuote() {
	document.getElementById("postform").action = 'editquote.php';
	document.getElementById("postform").submit();
}

function cancelQuoteEdit() {
	document.getElementById("cancelQuoteEdit").value = 'true';
	document.getElementById("postform").submit();
}

function submitForm() {
	document.getElementById("postform").submit();
}

function showHideMore() {
	var moreBtns = document.getElementById("postMoreButtons");
	var moreOptions = document.getElementById("postMoreOptions");
	var btn = document.getElementById("btnMore");
	var loggedInName = document.getElementById("loggedInName");
	if (moreOptions.style.display == "none") {
		moreBtns.style.display = "block";
		moreOptions.style.display = "block";
		loggedInName.style.display = "block";
		btn.src = "images/btnMoreOn.gif";
	} else {
		moreBtns.style.display = "none";
		moreOptions.style.display = "none";
		loggedInName.style.display = "none";
		btn.src = "images/btnMore.gif";
	}
}

function deletePM(pmid, folderid) {
	var answer = confirm("Delete this Private Message?");
	if (answer) {
		location.href = "private.php?do=delete&pmid=" + pmid + "&folderid=" + folderid;
	}
}

var req;
function showthreadUpdate(url) {
	if (refreshing) {
		alert("Still loading. Please wait.");
	} else {
		refreshing = true;
		document.getElementById('refresh').innerText = "Loading...";
		document.getElementById('refresh').className = "roundButtonOff";
		req = false;
		if (window.XMLHttpRequest && !(window.ActiveXObject)) {
			try {
				req = new XMLHttpRequest();
			} catch(e) {
				req = false;
			}
		} else if(window.ActiveXObject) {
			try {
				req = new ActiveXObject("Msxml2.XMLHTTP");
			} catch(e) {
				try {
					req = new ActiveXObject("Microsoft.XMLHTTP");
				} catch(e) {
					req = false;
				}
			}
		}
		if (req) {
			req.onreadystatechange = processReqChange;
			req.open("GET", url, true);
			req.send("");
		}
	}
}

function processReqChange() {
	if (req.readyState == 4) {
		refreshing = false;
		if (req.status == 200) {
			if (req.responseText.length > 0) {
				var element = document.getElementById("controls");
  			element.parentNode.removeChild(element);
				document.getElementById('newposts').innerHTML += req.responseText;
			} else {
				document.getElementById('refresh').innerText = "Refresh";
				document.getElementById('refresh').className = "roundButton";
				alert("No new posts");
			}
		} else {
			document.getElementById('refresh').innerText = "Refresh";
			document.getElementById('refresh').className = "roundButton";
			alert("No new posts");
		}
	}
}

function jumpFromForum(forumId) {
    if (forumId == 11) {
        var answer = confirm("Jump to The Spot?");
        if (answer) {
            location.href = "forumdisplay.aspx?f=1";
        }
    } else {
        var answer = confirm("Jump to The Rec Room?");
        if (answer) {
            location.href = "forumdisplay.aspx?f=11";
        }
    }
}
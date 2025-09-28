








////////////////////////////////////////////////////

function MoveItem(ctrlSource, ctrlTarget) {
    var Source = document.getElementById(ctrlSource);
    var Target = document.getElementById(ctrlTarget);
    if ((Source != null) && (Target != null)) {
        while (Source.options.selectedIndex >= 0) {
            var newOption = new Option(); // Create a new instance of ListItem
            newOption.text = Source.options[Source.options.selectedIndex].text;
            newOption.value = Source.options[Source.options.selectedIndex].value;
            Target.options[Target.length] = newOption; //Append the item in Target
            Source.remove(Source.options.selectedIndex);  //Remove the item from Source
        }
    }
}

function GatherKeys(ctrlSource, ctrlTarget) {
    var Source = document.getElementById(ctrlSource);
    var IDlist = document.getElementById(ctrlTarget);
    if ((Source != null) && (IDlist != null)) {
        var x = -1;
        var xc = Source.getElementsByTagName('option').length;
        IDlist.value = "";
        while (x < xc - 1) {
            x = x + 1;
            if (IDlist.value.length > 0) {
                IDlist.value += "," + Source.options[x].value;
            }
            else {
                IDlist.value = Source.options[x].value;
            }
        }
    }
}

///////////////////////////////////////////////////


function AddItem(ctrlTarget, Text) {
    var Target = document.getElementById(ctrlTarget);
    if (Target != null) {
        var newOption = new Option(); // Create a new instance of ListItem
        newOption.text = Text;
        newOption.value = Target.length;
        Target.options.add(newOption, 0); //Append the item in Target
        //Target.options[Target.length] = newOption; //Append the item in Target
    }
}


//Object.prototype.nextObject = function () {
//    var n = this;
//    do n = n.nextSibling;
//    while (n && n.nodeType != 1);
//    return n;
//}

//Object.prototype.previousObject = function () {
//    var p = this;
//    do p = p.previousSibling;
//    while (p && p.nodeType != 1);
//    return p;
//}

///////////////////////////////////////////////////
function nextnode(elem) {
    do { elem = elem.nextSibling; } while (elem && elem.nodeType != 1);
    return elem;
}

function priornode(elem) {
    do { elem = elem.previousSibling; } while (elem && elem.nodeType != 1);
    return elem;
}
////////////////////////////////////////////////////


function IsNumeric(strString)
//  check for valid numeric strings	
{
    var strValidChars = "0123456789.-";
    var strChar;
    var blnResult = true;

    if (strString.length == 0) return false;

    //  test strString consists of valid characters listed above
    for (i = 0; i < strString.length && blnResult == true; i++) {
        strChar = strString.charAt(i);
        if (strValidChars.indexOf(strChar) == -1) {
            blnResult = false;
        }
    }
    return blnResult;
}



function GetParameterStream(ParmameterList) {
    var count = 0;
    var sb = new Sys.StringBuilder();
    for (var property in ParmameterList) {
        if (count > 0) { sb.append("&"); }
        sb.append(property + "=" + ParmameterList[property]);
        count += 1;
    }
    return sb.toString();
}


// Date Time Functions
function getCalendarDate() {
    var months = new Array(13);
    months[0] = "January";
    months[1] = "February";
    months[2] = "March";
    months[3] = "April";
    months[4] = "May";
    months[5] = "June";
    months[6] = "July";
    months[7] = "August";
    months[8] = "September";
    months[9] = "October";
    months[10] = "November";
    months[11] = "December";
    var now = new Date();
    var monthnumber = now.getMonth();
    var monthname = months[monthnumber];
    var monthday = now.getDate();
    var year = now.getYear();
    if (year < 2000) { year = year + 1900; }
    var dateString = monthname +
                    ' ' +
                    monthday +
                    ', ' +
                    year;
    return dateString;
} // function getCalendarDate()

function getClockTime() {
    var now = new Date();
    var hour = now.getHours();
    var minute = now.getMinutes();
    var second = now.getSeconds();
    var ap = "AM";
    if (hour > 11) { ap = "PM"; }
    if (hour > 12) { hour = hour - 12; }
    if (hour == 0) { hour = 12; }
    if (hour < 10) { hour = "0" + hour; }
    if (minute < 10) { minute = "0" + minute; }
    if (second < 10) { second = "0" + second; }
    var timeString = hour +
                    ':' +
                    minute +
                    ':' +
                    second +
                    " " +
                    ap;
    return timeString;
} // function getClockTime()


////////////////////////////////////////////////////////////////////////////////////////////


//function enabledDisabled(chkBox, form) { if (chkBox.checked) { form.enableTxtBox.disabled = false } else { form.enableTxtBox.disabled = true } }
//function visibleInvisible(chkBox, form) { if (chkBox.checked) { form.visibleTxtBox.style.visibility = "visible" } else { form.visibleTxtBox.style.visibility = "hidden" } }
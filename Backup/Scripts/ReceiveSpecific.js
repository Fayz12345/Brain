/* 
* To change this template, choose Tools | Templates
* and open the template in the editor.
*/



//function Test()
//{
//    var s1 = new StopWatch();
//    s1.Start();        
//    // Do something.
//    s1.Stop();
//    alert( s1,.ElapsedMilliseconds );
//}

var AllowProcess = true;
var POPickLineData = '';

// Create a stopwatch "class."
StopWatch = function () { this.StartMilliseconds = 0; this.ElapsedMilliseconds = 0; };
StopWatch.prototype.Start = function () { this.StartMilliseconds = new Date().getTime(); };
StopWatch.prototype.Stop = function () { this.ElapsedMilliseconds = new Date().getTime() - this.StartMilliseconds; };

function SetLabDestination(SelectedItem) {
    if (MCL('hdnBinID').value.length == 0) {
        return;
    }
    SetDefaultLabDestinationBinNumber(SelectedItem.value);
}


function SetGather() {
    //if (MCL('ESN').value.length > 0 || MCL('ESN').value == 'ESN/IMEI Number') {

        alert('Starting SetGather!.');


    if (MCL('ESN').value.length > 0) {
        alert('ESN Set. Unable to process in Bulk when esn set!.');
        ScanFocus();
        return false;
    }
    if (MCL('ISCLIENTSCREEN').value.length == 0) {
            alert('ESN Set. Unable to process in Bulk.!');
            return;
    }
    if (OKToSaveEdits() == false) {
        alert('Not all attributes set. Unable to move into Bulk Process!');
        ScanFocus();
        return false;
    }
    
    alert('Getting the stream!.');
    var ds = GetDataStream(true);
    alert(ds);
    MCL('TXTBULKPROCESS').value = ds;
}

function SetDefaultLabDestinationBinNumber(SelectLabDestination) {
        var service = new WebServer_01();
        service.GetDefaultLabDestinationBinNumber(SelectLabDestination, onSuccessGetDefaultLabDestinationBinNumber, null, null);
}
function onSuccessGetDefaultLabDestinationBinNumber(result) {
    if (result.length > 0) {
        uppdateStatusPanel('Bin Number Set to ' + result);
        var BinField = $get(MCL('hdnBinID').value);
        BinField.value = result;
    }
    return;
}

//// DoSave --------------------------------------------------------------<
function DoSave(CalledFrom) {
    //       LoadMCL();
    //           var CurrProcess = MCL('CurrentProcess').value;
    if (MCL('CurrentProcess').value.substr(0, 6).toUpperCase() == 'SEARCH') {
        //alert('HERE IN DO SAVE SEARCH');
        OKToNextStep('SEARCH', '-1', CalledFrom);
        return;
    }
    MCL('btnSave').click();
    return;
}

function OKToNextStep(btnName, IDField, CalledFrom) {
    //    var newText = MCL('ScanKey').value;
    //    if (newText != null && newText.length > 0) { return false; }    // if there is something in the scanfield, then we do not want to proceed.
    // If there is no IMEI in the field, then we want to exit
    if (MCL('ESN').value.length == 0 || MCL('ESN').value == 'ESN/IMEI Number') {
        ScanFocus();
        return false;
    }
    //    alert('ESN Value:' + MCL('ESN').value + ':');


    // JIM REMOVE
    //alert('ESN Value:' + MCL('ESN').value + ': running OKToSaveEdits');
    //   /////////////////////////////////////////////////////////////////////

    if (OKToSaveEdits() == false) {
        // JIM REMOVE
        //alert('OKToSaveEdits = true');
        //   /////////////////////////////////////////////////////////////////////

        alert('unable to save.');
        ScanFocus();
        return false;
    }
    // JIM REMOVE
    //alert('OKToSaveEdits = true, doing a bit of  house keeping');
    //   /////////////////////////////////////////////////////////////////////
    //alert('tallying on....');
    ResetFields(btnName, IDField);
    // JIM REMOVE
    //alert('small bit of house keeping just finished.');
    //   /////////////////////////////////////////////////////////////////////
    MCL('CalledFrom').value = CalledFrom;

    if (btnName.toUpperCase() == "TSAVE") {
        switch (MCL('CurrentProcess').value.toUpperCase()) {
            case 'BULKRECEIVE': AddDataBulk(); break;
            case 'BULKMOVE': MoveDataBulk(); break;
            case 'RECEIVEFROMBULK': AddDataFromBulk(); break;
            case 'RECEIVE': AddTData(); break;

            case 'RECEIVEDOAB': AddTData(); break;
            case 'RECEIVEWARRANTYB': AddTData(); break;

            case 'RECEIVEDEFECTIVE': AddTData(); break;
            case 'RECEIVEREPAIRED': AddTData(); break;
            case 'RECEIVEDOA': AddTData(); break;
            case 'RECEIVEGENERAL': AddTData(); break;
            case 'RECEIVEINWARRANTY': AddTData(); break;
            case 'RECEIVEEXWARRANTY': AddTData(); break;
            case 'RECEIVEOOWARRANTY': AddTData(); break;
            default: AddTData(); break;
        }
    }
    else {
        switch (MCL('CurrentProcess').value.toUpperCase()) {
            case 'BULKRECEIVE': AddDataBulk(); break;
            case 'BULKMOVE': MoveDataBulk(); break;
            case 'RECEIVEFROMBULK': AddDataFromBulk(); break;
            case 'RECEIVE': AddData(); break;

            case 'RECEIVEDOAB': AddData(); break;
            case 'RECEIVEWARRANTYB': AddData(); break;

            case 'RECEIVEDEFECTIVE': AddData(); break;
            case 'RECEIVEREPAIRED': AddData(); break;
            case 'RECEIVEDOA': AddData(); break;
            case 'RECEIVEGENERAL': AddData(); break;
            case 'RECEIVEINWARRANTY': AddData(); break;
            case 'RECEIVEEXWARRANTY': AddData(); break;
            case 'RECEIVEOOWARRANTY': AddData(); break;
            default: AddData(); break;
        }
    }
    return false;
}

function OKToSaveEdits() {

    //    alert('ESN Valueb:' + MCL('ESN').value + ':');
    if (MCL('ESNVersion').value.length > 0 && MCL('ESNVersion').value != '000') {
        alert('Verion not 000, Save Canceled.');
        uppdateStatusPanelError('Unable to save, invalid version!');
        return false;
    }
    if (IsNumeric(MCL('ClientLocationID').value) == false || MCL('ClientLocationID').value == '-1') {
        alert('You must enter a Client first!');
        uppdateStatusPanelError('You must enter a Client first!');
        return false;
    }
    if (MCL('ESN').value.length == 0 && MCL('CurrentProcess').value.substr(0, 7).toUpperCase() == 'RECEIVE'
        ) {
        alert('You must enter an ESN Number first.');
        uppdateStatusPanelError('You must enter an ESN Number first!');
        return false;
    }
    // JIM REMOVE
    //alert('OKToSaveEdits -- GetReplacementIMEIValue().');
    //   /////////////////////////////////////////////////////////////////////
    var ReplacementIMEI = GetReplacementIMEIValue();
    // JIM REMOVE
    //alert('OKToSaveEdits return value -- GetReplacementIMEIValue()=' + ReplacementIMEI);
    //   /////////////////////////////////////////////////////////////////////
    if ((MCL('CurrentProcess').value.substr(0, 10).toUpperCase() == 'GMP REPAIR')) {
        // Look to see if there is anything in the Replace IMEI field.

        if (ReplacementIMEI == null || ReplacementIMEI.length == 0) {
            if (confirm("A Replacement IMEI has not been provided. Continue save process?") == false) { return false; }
        }
    }
    if (MCL('ESN').value == ReplacementIMEI) {
        alert('Replacement IMEI must differ from original IMEI.  Save cancelled.');
        uppdateStatusPanelError('Replacement IMEI must differ from original IMEI.  Save cancelled!');
        return false;
    }
    return ValidateEntryError();
}

function ValidateEntryError() {
    if (AnyDropdownsSetToDefault() == true) {
        alert('Not all dropdown fields have been set!');
        uppdateStatusPanelError('Not all dropdown fields have been set!');
        return false;
    }
    var PartsUsage = ""
    if (PartsUsage.length > 0) {
        alert(PartsUsage);
        uppdateStatusPanelError(PartsUsage);
        return false;
    }
    var eMessage = '';
    var isManditory = MCL('hdnManditoryFields').value;
    if (isManditory.length == 0) {
        return true;
    }

    var ds = GetDataStream(false);
    var DataList = ds.split(',');
    var ClearDataList = new Array();

    var isManditoryList = isManditory.split(',');
    // reset the background back to normal. (as if there are zero errors)
    for (y in isManditoryList) {
        var dta = isManditoryList[y].split(':');
        if (dta[0].length > 0) {
            var eID = $get(dta[0]);
            if (eID != null) {
                eID.style.color = '';
            }
        }
    }
    // Delete any isManditory records from the list if found inside the data stream.
    for (x in DataList) {
        var dta = DataList[x].split(':');
        var k = dta[0].replace(/'/g, '');
        if (k.indexOf('TC_') > -1 || k.indexOf('TX_') > -1 || k.indexOf('DD_') > -1 || k.indexOf('RD_') > -1 || k.indexOf('CB_') > -1) {
            var d = dta[1].replace(/'/g, '');
            if ((k.indexOf('TX_') > -1 && d.length == 0)
            || (k.indexOf('DD_') > -1 && d == '0')
            || (k.indexOf('RD_') > -1 && d == '0')
            || (k.indexOf('CB_') > -1 && d == '0')) {
                //alert('Missing text in dropdown:dta(' + DataList[x] + ') k(' + k + ') d(' + d + ')');
               // alert('data stream(' + ds + ')');
                // skip it, no text in field.
            }
            else {

                for (y in isManditoryList) {
                    var y1 = isManditoryList[y];
                    if (y1.indexOf(k) > -1) { isManditoryList.splice(y, 1); }
                    else {
                        // Look to see if it is hidden. If so, remove it as well
                        var dta = isManditoryList[y].split(':');
                        if (dta[0].length > 0) {
                            var eID = $get(dta[0]);
                            if (eID != null) {
                                var tr = getParentByTagName(eID, 'tr');
                                if (IsControlHiden(tr) == true) { isManditoryList.splice(y, 1); }
                            }
                        }
                    }
                }
            }
        }
    }
    if (isManditoryList.length > 0) {
        // flag any isManditory records that are left (they have not been filled in)
        //        var d = "";

        for (y in isManditoryList) {
            var dta = isManditoryList[y].split(':');
            //            d += isManditoryList[y] + "__x__";
            if (dta[0].length > 0) {
                var eID = $get(dta[0]);
                if (eID != null) { eID.style.color = 'red'; }
            }
        }
        alert('There are mandatory elements not entered.');
        uppdateStatusPanelError('There are mandatory elements not entered');
        alert('There are mandatory elements not entered. -- returning');
        return false;
    }
    return true;
}

function AnyDropdownsSetToDefault() {
    var rvalue = false;
    // we need to pull all the dropdowns and check to see if the value is set to Jody's default. (* SELECT *)
    //    If we find something, then we need to return false.
    var inputArea = MCL('InputArea');
    var Selects = inputArea.getElementsByTagName('select'); //This should pull all the dropdowns.;
    if (Selects != null) {
        for (var i = 0; i < Selects.length; i++) {
            if (Selects[i].length > 0) {
                //                var xxx = Selects[i]
                //                alert(xxx.style.display);
                var eID = $get(Selects[i].id);
                if (eID != null) {
                    var tr = getParentByTagName(eID, 'tr');
                    // if the control is not hidden, then we need to deal with it.
                    if (IsControlHiden(tr) == false) {
                        var IndexValue = Selects[i].selectedIndex;
                        if (Selects[i].options[IndexValue].text.toUpperCase() == '* SELECT *') {
                            rvalue = true;
                        }
                    }
                }
            }

        }
    }
    return rvalue;
}



//// SCANKEY PROCESSING GOES HERE ----------------------------------------<
function RecordScanKey(pText) {
    var newText = MCL('ScanKey').value;
    var xz = 1;
    if (pText != null) { newText = pText; }

    //alert("Here is the one:" + newText);
    if (newText == null) { return; }
    //alert("Here is the two:" + newText);
    newText = trim(newText);
    MCL('ScanKey').value = '';
    if (newText.length > 0) {
        //        DOALLYOUCAN();
        // Scan key load of tet field.
        // Format - /SN99999999  
        // The slash starts the process, macro codes are searched for SN, the field that was found will be filled with 99999999
        //     any string starting with a forward slash will be interpreted as a text scan code.
        // Macro chain
        // Format - XX.XX.XX.XX.XX

        if (newText.substr(0, 1).toUpperCase() == '/' && newText.length == 3) { MCL('ScanKey').value = newText; AllowProcess = true; return; }
        if (newText.toUpperCase() == 'DOSAVE') { DoSave('RecordScanKey'); AllowProcess = true; return; }  // shortcut to the Save Button.
        if (newText.toUpperCase() == '**') { DoSave('RecordScanKey'); AllowProcess = true; return; }  // shortcut to the Save Button.
        if (newText.toUpperCase() == 'DOCLEAR') { ClearData(); AllowProcess = true; return; }   // shortcut to the Save Button.
        if (newText == '--') { ClearData(); AllowProcess = true; return; }  // shortcut to the Save Button.
        if (newText == '++') { GenerateBagTag(); AllowProcess = true; return; }  // shortcut to the Save Button.
        if (newText.toUpperCase() == 'BAGTAG') { GenerateBagTag(); AllowProcess = true; return; }
        if (newText.toUpperCase() == '//') { ToggleTarget(); AllowProcess = true; return; }


        if (newText.substr(0, 4).toUpperCase() == 'XPTX') {
            if (isInRole("XPTX") == true) { ProjectTagUpdate(newText.substr(4)); AllowProcess = true; return; }
            else { alert("Authorization 'XPTX' required"); AllowProcess = true; return; }
        }
        if (newText.substr(0, 5).toUpperCase() == 'XRMAX') {
            if (isInRole("XRMAX") == true) { RMANumberUpdate(newText.substr(5)); AllowProcess = true; return; }
            else { alert("Authorization 'XRMAX' required"); AllowProcess = true; return; }
        }
        if (newText.substr(0, 5).toUpperCase() == 'XAUTHX') {
            if (isInRole("XAUTHX") == true) { SetupToAuthorize(newText.substr(6)); AllowProcess = true; return; }
            else { alert("Authorization 'XAUTHX' required"); AllowProcess = true; return; }
        }


        if (newText.substr(0, 5).toUpperCase() == 'XBINX') {
            if (isInRole("XBIBX") == true || isInRole("XBINX") == true) { BinBulkProcess(newText.substr(5)); AllowProcess = true; return; }
            else { alert("Authorization 'XBINX' required"); AllowProcess = true; return; }
        }



        var x = newText.substr(0, 6).toUpperCase();


        if (newText.substr(0, 4).toUpperCase() == 'XCLX') {
            if (isInRole("XCLX") == true) { ChangeclientProcess(newText.substr(4)); AllowProcess = true; return; }
            else { alert("Authorization 'XCLX' required"); AllowProcess = true; return; }
        }
        if (newText.substr(0, 6).toUpperCase() == 'XDONEX') {
            if (isInRole("XDONEX") == true) { DONEXBulkProcess(newText.substr(6)); AllowProcess = true; return; }
            else { alert("Authorization 'XDONEX' required"); AllowProcess = true; return; }
        }
        if (newText.substr(0, 5).toUpperCase() == 'XLOCX') {
            if (isInRole("XLOCX") == true) { LocBulkProcess(newText.substr(5)); AllowProcess = true; return; }
            else { alert("Authorization 'XLOCX' required"); AllowProcess = true; return; }
        }
        if (newText.toUpperCase() == 'IMEIBULK') {
            if (isInRole("IMEIBULK") == true) { OpenIMEIBulkWindowCtrl(); AllowProcess = true; return; }
            else { alert("Authorization 'IMEIBULK' required"); AllowProcess = true; return; }
        }

        if (newText.indexOf(':') > -1) { LoadThisESNVersion(newText); AllowProcess = true; return; }

        MCL('ScanKeyHistory').value = newText;

        uppdateStatusPanelYellow('Processing...');

        //        return;


        $('#loading').show();
        var cProcess = MCL('CurrentProcess').value.toUpperCase();
        uppdateStatusPanelYellow('Server Create Start');


        // JIM REMOVE
        //alert('Creating the Web Service to look for:' + newText);
        // ///////////////////////////////////////////////////////
        var service = new WebServer_01();

        uppdateStatusPanelYellow('Server Created');
        AllowProcess = true;
        //alert('ready to look');
        //        service.set_defaultFailedCallback(onWebServerError_01);

        // JIM REMOVE
        //alert('Calling the webservice.ScanCodeParse()');
        // ///////////////////////////////////////////////////////
        service.ScanCodeParse(MCL('ClientLocationID').value, 'XXX', cProcess, newText, MCL('UserName').value, MCL('StepUp').value, MCL('hdnManufacturerIDx').value, MCL('hdnModelIDx').value, onSuccess, onWebServerError_01, null);
        //service.ScanCodeParse(MCL('ClientLocationID').value, 'XXX', cProcess, newText, MCL('UserName').value, MCL('StepUp').value, MCL('hdnManufacturerIDx').value, MCL('hdnModelIDx').value, MCL('hdnMemoryIDx').value, onSuccess, null, null);
    }
    else {
        AllowProcess = true;
    }
}



function isInRole(Role) {
    var dta = MCL('hdnRoleList').value.toUpperCase();
    if (dta.indexOf("," + Role.toUpperCase() + ",") > -1) { return true; }
    return false;
}
function onWebServerError_01(Result) {
    alert('Error:' + Result.get_message());
    $('#loading').hide();
    uppdateStatusPanelError('Error Calling service.ScanCodeParse');
    alert("Stack Trace: " + Result.get_stackTrace() + "/r/n" +
          "Error: " + Result.get_message() + "/r/n" +
          "Status Code: " + Result.get_statusCode() + "/r/n" +
          "Exception Type: " + Result.get_exceptionType() + "/r/n" +
"ServiceBehaviorAttribute: " + Result.ServiceBehaviorAttribute() + "/r/n" +
          "Timed Out: " + Result.get_timedOut());
          

}


function onSuccess(result) {
    // example result = "Option:226:Serial Number iPhone:Serial Number:SN:TX" + ":" + TransferData + ":" + MessageQueueStop + ":" + MessageQueueMessage.Replace(':', ' ');
    var d = new Date();
//    MCL('hdnOpenTime').value = d.toString();
    //    alert(MCL('hdnOpenTime').value);
    //alert('Scanned onSuccess');
    //alert(result);
    uppdateStatusPanelYellow('Done Calling service.ScanCodeParse');

    $('#loading').hide();
    uppdateStatusPanelYellow('Processing...1');
    var B = result.split(':');
    uppdateStatusPanelYellow('Processing...:' + B[0]);
    if (B[0].toUpperCase() == 'MACROCHAIN') { LoadMacroChain(result.substr(11)); return; }
    if (B[0].toUpperCase() == 'CLIENTLOCATION') { onSuccess_LoadClientLocation(B); return; }
    if (B[0].toUpperCase() == 'RECEIVEDETAIL') { onSuccess_LoadReceiveDetail(B); return; }
    if (B[2].toUpperCase() == 'UNKNOWN SCANCODE') {
        //alert('inside unknown scancode');
        if (B[7].length > 0) {
            if (B[7] == 'true' && MCL('CurrentProcess').value.substr(0, 7).toUpperCase() == 'RECEIVE') {
                alert(B[8] + "\n\nUnit can be received, but no further processing can be done until message stop has been released");
            } else { alert(B[8]); }
        }

        if (B[5] == '5') {
            LoadScanNumber(B[3], true);
        }

        else {
            //alert('LoadScanNumber(B[3], false)');
            LoadScanNumber(B[3], false);
        }
        return;
    }
    if (B[2].toUpperCase() == 'UNKNOWN MACROKEY') {
        //alert('inside unknown MACROKEY');
        LoadScanNumber(B[3], false);
        return;
    }
    //alert('UpdateFormScanData');
    UpdateFormScanData(B);
    return;
}

function onSuccess_LoadReceiveDetail(B) {
    var ContinueLoad = true;
    var BumpVersionTo900 = false;

    //alert('onSuccess_LoadReceiveDetail');
    if (B[7].length > 0) {
        if (B[7] == 'true' && MCL('CurrentProcess').value.substr(0, 7).toUpperCase() != 'RECEIVE') {
            alert(B[8] + "\n\nUnit can not be loaded until message stop is released");
            uppdateStatusPanelError(B[8]);
            return;
        }
        alert(B[8]);
    }

    if (MCL('CurrentProcess').value.substr(0, 7).toUpperCase() == 'RECEIVE') {
        var IndexValue = MCL('drpProjectList').selectedIndex;
        if (MCL('drpProjectList').options[IndexValue].text.toUpperCase() == 'MSC') {             // Need to do this only if Approval required.
            uppdateStatusPanelError('Transfer Into MSC');
            B[5] = "2";
            OpenWindowCtrl(B);
            return;
        }


        if (MCL('SProjectOverride').value != 'Y') {
            // open the window to tell them they can not open the IMEI, or if it needs to be transfered.
            uppdateStatusPanelError('IMEI found');
            OpenWindowCtrl(B);
            return;
        }


        // If isSecondaryProjectOverride then we want to bump the 000 record and move forward as normal
        BumpVersionTo900 = true;
    }

    // Invalid Device.
    // ------------- We have a device that has some invalid data. Stop any loading and have them correct the device first.
    if (B[5] == '8') {
        alert('This device has invalid SKU related datadata. Correct SKU before you can open it!');
        uppdateStatusPanelError('IMEI Invalid Data. Correct before proceeding.');
        //OpenWindowCtrl(B);
        return;
    }



    ////////////////////////////////////////////////////////////////////////////////////////////
    // Stop the user from entering these two processes if the unit has not already been
    // in these these processes or already gone through "Tech Receive",'_REPAIR','LAB BILLING','LAB BILLING','HOLD STATUS'
    // B[5] is deduced in SQL.SP.ProcessScanCode

//    // Removed May 9, 2016
//    if ((MCL('CurrentProcess').value.substr(0, 7).toUpperCase() == '_REPAIR' ||
//         MCL('CurrentProcess').value.substr(0, 13).toUpperCase() == 'REQUEST PARTS' ||
//         MCL('CurrentProcess').value.substr(0, 11).toUpperCase() == 'LAB BILLING' ||
//         MCL('CurrentProcess').value.substr(0, 11).toUpperCase() == 'HOLD STATUS') && B[5] != "3") {
//        OpenWindowCtrl(B);
//        return;
//    }
    // JIM RMA Approval Edit Check.  QC Assessment or Not a valid RMA device
    if (MCL('CurrentProcess').value.substr(0, 11).toUpperCase() == 'RMA RECEIVE' && B[5] != "3") {
        OpenWindowCtrl(B);
        return;
    }


 
    // If the unit is opened in "Tech Receive", it must have first gone through "Lab Receive"
    // B[5] tells me if it went through "Lab Receive"  "3" = yes it has.
    // B[5] is deduced in SQL.SP.ProcessScanCode
    // onSuccess_LoadReceiveDetail(B)


//    // Chris asked for this to be removed May 9, 2016   ///////////////////////////////////////////////////
//    if (MCL('CurrentProcess').value.substr(0, 12).toUpperCase() == 'TECH RECEIVE' && B[5] != "3") {
//        OpenWindowCtrl(B);
//        return;
//    }
//    //////////////////////////////////////////////////////////////////


    // If the unit is opened in "Tech Receive", it must have first gone through "Lab Receive"
    // B[5] tells me if it went through "Lab Receive"  "3" = yes it has.
    // B[5] is deduced in SQL.SP.ProcessScanCode
    // onSuccess_LoadReceiveDetail(B)
    if (MCL('CurrentProcess').value.substr(0, 13).toUpperCase() == 'TECH FINISHED' && B[5] != "3") {
        OpenWindowCtrl(B);
        return;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////
    // Adjusted to deal with a new process Jody implemented Oct 29, 2013 -- "Shipping GMP Sales"

    if ((MCL('CurrentProcess').value.substr(0, 7).toUpperCase() == 'KITTING' || MCL('CurrentProcess').value.substr(0, 18).toUpperCase() == 'SHIPPING BW SALES') && B[5] == "4") {
        OpenWindowCtrl(B);
        return;
    }


    // alert('Calling LoadSheetDataDetail');
    LoadSheetDataDetail(B[1], false, BumpVersionTo900);
    ScanFocus();
    return;
}

function SearchClient() {
    var SearchClientName = MCL('txtsClientName').value;
    var SearchLocationName = MCL('txtsLocationName').value;

    var SearchStreet = MCL('txtsStreet').value;
    var SearchPostalCode = MCL('txtsPostalCode').value;
    var service = new WebServer_01();
    var rValue = service.GetSearchClientLocationData(MCL('UserName').value, SearchClientName, SearchLocationName, SearchStreet, SearchPostalCode, onSearchClientSuccess, onWebServerError);
}

function onWebServerError(Result) {
    alert('Error:' + Result.get_message());
}

function onSearchClientSuccess(Result) {
    var OutputHTML = "";
    var HeaderText = "<tr><td>Select</td><td>ID</td><td>Client</td><td>Location Name</td><td>Location</td></tr>";
    var BodyText = "";

//    ClientData = eval('({' + Result + '})');
    ClientData = eval('[' + Result + ']'); // Square brackets to denote an array of elements.

    for (var i = 0; i < ClientData.length; i++) {
        BodyText = BodyText + "<tr><td>"
                               + "<button id='btn' name='btn' onClick='selx("
                               + ClientData[i].ClientLocationID
                               + "); return false;'>Select</button>"
                               + "</td> <td>"
                               + ClientData[i].ClientLocationID
                               + "</td> <td>"
                               + ClientData[i].txtClientName
                               + "</td> <td>"
                               + ClientData[i].txtLocationName
                               + "</td> <td>"
                               + ClientData[i].txtStoreNumber + ' ' + ClientData[i].txtStoreSuffix + ' ' + ClientData[i].txtClientAddress
                               + "</td></tr>";
    }
    OutputHTML = "<table id='XX' class='table'>" + HeaderText + BodyText + "</table>";
    var SearchResults = MCL('pnlSearchResult');
    SearchResults.innerHTML = OutputHTML;
    ScanFocus();
}



function LoadThisESNVersion(versionToLoad) {
    if (MCL('CurrentProcess').value.substr(0, 7).toUpperCase() == 'RECEIVE') {
        alert('You can not open a record this way in Receive!');
        return;
    }
    var service = new WebServer_01();
    service.GetThisESNVersionRecordID(versionToLoad, onGetThisESNVersionRecordID, null, null);
}

function onGetThisESNVersionRecordID(result) {
//    alert(result);
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.ReceiveDetailID != '-1') {
        LoadSheetDataDetail(resultList.ReceiveDetailID);
    }
    else {
        uppdateStatusPanelError(resultList.VersionToLoad + ' Not Found');
    }
}
/////////////////////////////////////////////////////////////////////////////////

function OpenUnitNote() {
    var service = new WebServer_01();

    var ReceiveDetailID = MCL('RECEIVEDETAILID').value;
    var CurrentProcess = MCL('CurrentProcess').value;
    var UserName = MCL('UserName').value;
    var rValue = service.GetUnitNote(UserName, CurrentProcess, ReceiveDetailID, onOpenUnitNoteSuccess, onOpenUnitNoteError);
}


function onOpenUnitNoteError(Result) {
    alert('Error:' + Result); return;
    //alert("No parts found - " + Result); return;
    //alert('Error;' + Result);
}

function onOpenUnitNoteSuccess(Result) {
    if (Result.length == 0) { alert("Note Not Found"); return; }
    //    var temp = new Array();
    //    temp = Result.split('|')
    var SearchResults = MCL('pnlUnitNote');
    SearchResults.innerHTML = Result;
    //    SearchResults = MCL('pnlPickedList');
    //    SearchResults.innerHTML = temp[1];

    // MCL('wndUnitNote').Title = "Unit Note";
    // MCL('wndUnitNote').Open(null, null);

    $('#wndUnitNote').modal('show');
}



//function IsValidReturnType() {
//    return false;
//}


function OpenReturnPartList() {
    //    if (IsValidReturnType() == false) {
    //        return; 
    //    }
    if (MCL('ESNVERSION').value != '000') { return; }
    var service = new WebServer_01();
    var ReceiveDetailID = MCL('RECEIVEDETAILID').value;
    var ClientID = MCL('hdnClientIDx').value;
    var ClientLocationID = MCL('ClientLocationID').value;
    var CarrierID = MCL('hdnCarrierIDx').value;
    var ManufacturerID = MCL('hdnManufacturerIDx').value;
    var ModelID = MCL('hdnModelIDx').value;
    var MemoryID = MCL('hdnMemoryIDx').value;
    
    var UserName = MCL('UserName').value;
//    alert('Not yet implemented');
//    return;
//    onPartNumberReturnListSuccess('XXXXXXXX|YYYYYYYYY');
    var rValue = service.GetPartNumberReturnListData_02(UserName, ClientID, ClientLocationID, CarrierID, ManufacturerID, ModelID, ReceiveDetailID, onPartNumberReturnListSuccess, onWebServerError);
    //var rValue = service.GetPartNumberReturnListData_02(UserName, ClientID, ClientLocationID, CarrierID, ManufacturerID, ModelID, MemoryID, ReceiveDetailID, onPartNumberReturnListSuccess, onWebServerError);
}
function onPartNumberReturnListSuccess(Result) {
//    alert("onPartNumberReturnListSuccess:" + Result + ":");
//    return;
    if (Result.length == 0) { alert("No Parts Found"); return; }
    if (Result.substr(0, 7).toUpperCase() == 'INVALID') { alert(Result); return; }

    var temp = new Array();
    temp = Result.split('|');

    var SearchResults = MCL('pnlPartReturnList');
    SearchResults.innerHTML = temp[0];
    SearchResults = MCL('pnlPickedReturnList');
    SearchResults.innerHTML = temp[1];

    //MCL('wndPartReturnList').Title = "Part Return List";
    //MCL('wndPartReturnList').Open(null, null);

    $('#wndPartReturnList .modal-title').text('Part Return List');
    $('#wndPartReturnList').modal('show');
}

function PickReturnSave() {
    var service = new WebServer_01();
    var table = document.getElementById("PickedReturnList");
//    alert('Inside Pick return save');
    var Data = table.innerHTML;
//    alert(Data);
    var UserName = MCL('UserName').value;
    var rValue = service.ProcessReturnedParts(UserName, Data, onProcessReturnedPartsSuccess, onProcessReturnedPartsError);
//    for (var i = 1, row; row = table.rows[i]; i++) {
//        //iterate through rows
//        //rows would be accessed using the "row" variable assigned in the for loop
////        PlacePartNumber(row.cells[1].innerHTML);
//        //        for (var j = 0, col; col = row.cells[j]; j++) {
//        //            if (col.innerHTML == Data) {
//        //                RowToDelete = row;
//        //                //iterate through columns
//        //                //columns would be accessed using the "col" variable assigned in the for loop
//        //            }
//        //        }
//    }

}
function onProcessReturnedPartsSuccess(Result) {
    if (Result.length == 0) { alert("No Items returned!"); return; }
//    if (Result.substr(0, 7).toUpperCase() == 'INVALID') { alert(Result); return; }
    //MCL('wndPartReturnList').Close();
    $('#wndPartReturnList').modal('hide');

    // loop the returned ids and print the bag tag.
    var Bs = Result.split(',')
    for (var i = 0; i < Bs.length; i++) {
        if (Bs[i].length > 0) {
            OpenReturnPartbagTag(Bs[i])
        }
    }
    //
}

function PickReturnCancel() {
    //MCL('wndPartReturnList').Close();
    $('#wndPartReturnList').modal('hide');
}

function onProcessReturnedPartsError(Result) {
    alert("No parts found - "); return;
    //alert("No parts found - " + Result); return;
    //alert('Error;' + Result);
}


//-------------------------------------------

function OpenIFSPONumberList() {
    //    if (IsValidReturnType() == false) {
    //        return; 
    //    }

//    if (MCL('ESNVERSION').value != '000') { return; }
    var service = new WebServer_01();
    var ReceiveDetailID = MCL('RECEIVEDETAILID').value;
    var ClientID = MCL('hdnClientIDx').value;
    var ClientLocationID = MCL('ClientLocationID').value;
    var CarrierID = MCL('hdnCarrierIDx').value;
    var ManufacturerID = MCL('hdnManufacturerIDx').value;
    var ModelID = MCL('hdnModelIDx').value;
    var MemoryID = MCL('hdnMemoryIDx').value;
    var UserName = MCL('UserName').value;
    var rValue = service.GetIFSPONumberListData_03(ClientLocationID, UserName, onPONumberListSuccess, onWebServerErrorPONumber);
}
function OpenIFSPONumberListForLines() {
    //    if (IsValidReturnType() == false) {
    //        return; 
    //    }

    //    if (MCL('ESNVERSION').value != '000') { return; }
    var service = new WebServer_01();
    var ReceiveDetailID = MCL('RECEIVEDETAILID').value;
    var ClientID = MCL('hdnClientIDx').value;
    var ClientLocationID = MCL('ClientLocationID').value;
    var CarrierID = MCL('hdnCarrierIDx').value;
    var ManufacturerID = MCL('hdnManufacturerIDx').value;
    var ModelID = MCL('hdnModelIDx').value;
    var MemoryID = MCL('hdnMemoryIDx').value;
    var UserName = MCL('UserName').value;
    var rValue = service.GetIFSPONumberListData_ForLinePick(ClientLocationID, UserName, onPONumberListSuccess, onWebServerErrorPONumber);
}
function onWebServerErrorPONumber(Result) {
    alert("Error: No PO Numbers found - " + Result); return;
    //alert("No parts found - " + Result); return;
    //alert('Error;' + Result);
}

function onPONumberListSuccess(Result) {
    if (Result.length == 0) { alert("No PO Numbers Found"); return; }
    if (Result.substr(0, 7).toUpperCase() == 'INVALID') { alert(Result); return; }

    var SearchResults = MCL('pnlPOList');
    SearchResults.innerHTML = Result;             // temp[0];
//    SearchResults = MCL('pnlPOPickedList');
    //    SearchResults.innerHTML = temp[1];

    //MCL('wndPurchaseOrderList').Title = "Master Part Number List";
    //MCL('wndPurchaseOrderList').Open(null, null);

    $('#wndPurchaseOrderList .modal-title').text('Master Part Number List');
    $('#wndPurchaseOrderList').modal('show');
}


function OpenPickPOLineNumber(Result) {
    var SearchResults = MCL('pnlPOList');
    SearchResults.innerHTML = Result;

    //MCL('wndPurchaseOrderList').Title = "Purchase Order Number Line List";
    //MCL('wndPurchaseOrderList').Open(null, null);

    $('#wndPurchaseOrderList .modal-title').text('Purchase Order Number Line List');
    $('#wndPurchaseOrderList').modal('show');
}


function PickPOLine(POLine, ElementID) {
    //alert('You picked Line POLine');
    var SaveMessage = "'Saving data!'";
    var result = POPickLineData;
    result = '({' + result + '})';
    var resultList = eval(result);
    resultList[ElementID] = POLine;
    POPickLineData = DataStream(POPickLineData);
    var service = new WebServer_01();
    Timer1.Start();
    var rValue = service.AddDataDetailThreaded(POPickLineData, 'N', onAddSaveSuccessThreaded, onAddSaveSuccessError);
    uppdateStatusPanelYellow(SaveMessage);
}


function PickPOSave(POData) {
//    alert('PickPOSave:' + POData);
    var temp = new Array();
    temp = POData.split('|');

    PlacePOVendor(temp[1]);
    PlacePONumber(temp[0]);
//    var table = document.getElementById("pnlPOPickedList");
//    for (var i = 1, row; row = table.rows[i]; i++) {
//        //iterate through rows
//        //rows would be accessed using the "row" variable assigned in the for loop
//        PlacePartNumber(row.cells[1].innerHTML);
//        //        for (var j = 0, col; col = row.cells[j]; j++) {
//        //            if (col.innerHTML == Data) {
//        //                RowToDelete = row;
//        //                //iterate through columns
//        //                //columns would be accessed using the "col" variable assigned in the for loop
//        //            }
//        //        }
    //    }
//    uppdateStatusPanel('PO NUmber Added:' + PartNumber);
    //MCL('wndPurchaseOrderList').Close();
    $('#wndPurchaseOrderList').modal('hide');
}


function PickPOLineSave(POData) {
    //    alert('PickPOSave:' + POData);
    var temp = new Array();
    temp = POData.split('|');

    PlacePOVendor(temp[1]);
    PlacePONumber(temp[0]);
    PlacePOLine(temp[2]);
    PlacePOUnitCost(temp[4]);
    //    var table = document.getElementById("pnlPOPickedList");
    //    for (var i = 1, row; row = table.rows[i]; i++) {
    //        //iterate through rows
    //        //rows would be accessed using the "row" variable assigned in the for loop
    //        PlacePartNumber(row.cells[1].innerHTML);
    //        //        for (var j = 0, col; col = row.cells[j]; j++) {
    //        //            if (col.innerHTML == Data) {
    //        //                RowToDelete = row;
    //        //                //iterate through columns
    //        //                //columns would be accessed using the "col" variable assigned in the for loop
    //        //            }
    //        //        }
    //    }
    //    uppdateStatusPanel('PO NUmber Added:' + PartNumber);

    //MCL('wndPurchaseOrderList').Close();
    $('#wndPurchaseOrderList').modal('hide');
}


function PickPOOpenLines(PONumberSupplier) {
    //alert('You picked Line POLine');
    var SaveMessage = "'Opening PO Lines!'";


    var temp = new Array();
    temp = PONumberSupplier.split('|');

//    var result = POPickLineData;
//    result = '({' + result + '})';
//    var resultList = eval(result);
//    resultList[ElementID] = POLine;
    //    POPickLineData = DataStream(POPickLineData);
    var UserName = MCL('UserName').value;
    var service = new WebServer_01();
    Timer1.Start();
    var rValue = service.getPODetailPickLines_HTML(temp[0], temp[1], UserName, OpenPickPOLineNumber, onAddSaveSuccessError);
    uppdateStatusPanelYellow(SaveMessage);
}

function PickPOCancel() {
    //MCL('wndPurchaseOrderList').Close();
    $('#wndPurchaseOrderList').modal('hide');
}

function PlacePOLine(POLineNumber) {
    var PNumbers = MCL('hdnPOLineNumberIDs').value;
    var inputArea = GetInputArea();
    var saved = false;
    // We need to deal with other types.
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
    var Dummy_CB_Sent = 0;
    for (var i = 0; i < inputs.length; i++) {
        var Key = '';
        var Value = '';
        var cBox = inputs[i];
        if (cBox.type == 'text') {
            Dummy_CB_Sent = 0;
            var xName = cBox.name;
            if (xName.indexOf('ScanKey') == -1) {
                var p = cBox.parentNode;
                var currentValue = cBox.getAttribute('someValue') + ',';
                Value = cBox.value;

                if (PNumbers.indexOf(currentValue) > -1) {
//                    if (cBox.value.length < 1) {
                        cBox.value = POLineNumber;
                        uppdateStatusPanel('PO Line Number Added:' + POLineNumber);
                        saved = true;
                        break;
//                    }
                }
            }
        }
    }
    //    if (saved == false) {
    //        alert("PO Vendor not saved. No Attribute available. " + POVendor);
    //    }
}

function PlacePOVendor(POVendor) {
    var PNumbers = MCL('hdnPOVendorIDs').value;
    var inputArea = GetInputArea();
    var saved = false;
    // We need to deal with other types.
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
    var Dummy_CB_Sent = 0;
    for (var i = 0; i < inputs.length; i++) {
        var Key = '';
        var Value = '';
        var cBox = inputs[i];
        if (cBox.type == 'text') {
            Dummy_CB_Sent = 0;
            var xName = cBox.name;
            if (xName.indexOf('ScanKey') == -1) {
                var p = cBox.parentNode;
                var currentValue = cBox.getAttribute('someValue') + ',';
                Value = cBox.value;
                if (PNumbers.indexOf(currentValue) > -1) {
//                    if (cBox.value.length < 1) {
                        cBox.value = POVendor;
                        uppdateStatusPanel('PO Vendor Added:' + POVendor);
                        saved = true;
                        break;
//                    }
                }
            }
        }
    }
//    if (saved == false) {
//        alert("PO Vendor not saved. No Attribute available. " + POVendor);
//    }
    }

function PlacePONumber(PONumber) {
    var PNumbers = MCL('HDNPONUMBERIDS').value;
    var inputArea = GetInputArea();
    var saved = false;
    // We need to deal with other types.
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
    var Dummy_CB_Sent = 0;
    for (var i = 0; i < inputs.length; i++) {
        var Key = '';
        var Value = '';
        var cBox = inputs[i];
        if (cBox.type == 'text') {
            Dummy_CB_Sent = 0;
            var xName = cBox.name;
            if (xName.indexOf('ScanKey') == -1) {
                var p = cBox.parentNode;
                var currentValue = cBox.getAttribute('someValue') + ',';
                Value = cBox.value;

                if (PNumbers.indexOf(currentValue) > -1) {
                    //                    if (cBox.value.length < 1) {
                    cBox.value = PONumber;
                    uppdateStatusPanel('PO Number Added:' + PONumber);
                    saved = true;
                    break;
                    //                    }
                }
            }
        }
    }
    //    if (saved == false) {
    //        alert("PO Number not saved. No Attribute available. " + PONumber);
    //    }
}

function PlacePOUnitCost(POUnitCost) {
//    alert('PlacePOUnitCost:' + POUnitCost);
    var PNumbers = MCL('hdnPOUnnitCostIDs').value;
    var inputArea = GetInputArea();
    var saved = false;
    // We need to deal with other types.
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
    var Dummy_CB_Sent = 0;
    for (var i = 0; i < inputs.length; i++) {
        var Key = '';
        var Value = '';
        var cBox = inputs[i];
        if (cBox.type == 'text') {
            Dummy_CB_Sent = 0;
            var xName = cBox.name;
            if (xName.indexOf('ScanKey') == -1) {
                var p = cBox.parentNode;
                var currentValue = cBox.getAttribute('someValue') + ',';
                Value = cBox.value;

                if (PNumbers.indexOf(currentValue) > -1) {
                    //                    if (cBox.value.length < 1) {
                    cBox.value = POUnitCost;
                    uppdateStatusPanel('PO Unit Cost added:' + POUnitCost);
                    saved = true;
                    break;
                    //                    }
                }
            }
        }
    }
//    if (saved == false) {
//        alert("PO Number not saved. No Attribute available. " + PONumber);
//    }
}


// -------------------------------------------------




function OpenPartList() {
//    if (IsValidReturnType() == false) {
//        return; 
    //    }
    if (MCL('ESNVERSION').value != '000') { return; }
    var service = new WebServer_01();
    var ReceiveDetailID = MCL('RECEIVEDETAILID').value;
    var ClientID = MCL('hdnClientIDx').value;
    var ClientLocationID = MCL('ClientLocationID').value;
    var CarrierID = MCL('hdnCarrierIDx').value;
    var ManufacturerID = MCL('hdnManufacturerIDx').value;
    var ModelID = MCL('hdnModelIDx').value;
    var MemoryID = MCL('hdnMemoryIDx').value;
    var UserName = MCL('UserName').value;
    //alert('ago we go');
    var rValue = service.GetPartNumberListData_03(UserName, ClientID, ClientLocationID, CarrierID, ManufacturerID, ModelID, ReceiveDetailID, onPartNumberListSuccess, onWebServerError);
    //var rValue = service.GetPartNumberListData_03(UserName, ClientID, ClientLocationID, CarrierID, ManufacturerID, ModelID, MemoryID, ReceiveDetailID, onPartNumberListSuccess, onWebServerError);
}

function onWebServerError(Result) {
    alert("No parts found - "); return;
    //alert("No parts found - " + Result); return;
    //alert('Error;' + Result);
}

function onPartNumberListSuccess(Result) {

    //alert('I am back:');

    if (Result.length == 0) { alert("No Parts Found"); return; }
    if (Result.substr(0, 7).toUpperCase() == 'INVALID') { alert(Result); return; }


    var temp = new Array();
    temp = Result.split('|');


    var SearchResults = MCL('pnlPartList');
    SearchResults.innerHTML = temp[0];
    SearchResults = MCL('pnlPickedList');
    SearchResults.innerHTML = temp[1];

    //MCL('wndPartList').Title = "Master Part List";
    //MCL('wndPartList').Open(null, null);

    $('#wndPartList .modal-title').text('Master Part List');
    $('#wndPartList').modal('show');
}


function PickSave() {
    var table = document.getElementById("PickedList");
    for (var i = 1, row; row = table.rows[i]; i++) {
        //iterate through rows
        //rows would be accessed using the "row" variable assigned in the for loop
        PlacePartNumber(row.cells[1].innerHTML);
        //        for (var j = 0, col; col = row.cells[j]; j++) {
        //            if (col.innerHTML == Data) {
        //                RowToDelete = row;
        //                //iterate through columns
        //                //columns would be accessed using the "col" variable assigned in the for loop
        //            }
        //        }
    }

    //MCL('wndPartList').Close();
    $('#wndPartList').modal('hide');

}

function PickCancel() {
    //MCL('wndPartList').Close();
    $('#wndPartList').modal('hide');
}
















function PlacePartNumber(PartNumber) {
    var PNumbers = MCL('PARTNUMBERIDS').value;
    var inputArea = GetInputArea();
    var saved = false;
    // We need to deal with other types.
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
    var Dummy_CB_Sent = 0;
    for (var i = 0; i < inputs.length; i++) {
        var Key = '';
        var Value = '';
        var cBox = inputs[i];
        if (cBox.type == 'text') {
            Dummy_CB_Sent = 0;
            var xName = cBox.name;
            if (xName.indexOf('ScanKey') == -1) {
                var p = cBox.parentNode;
                var currentValue = cBox.getAttribute('someValue') + ',';
                Value = cBox.value;

                if (PNumbers.indexOf(currentValue) > -1) {
                    if (cBox.value.length < 1) {
                        cBox.value = PartNumber;
                        uppdateStatusPanel('Partnumber Added:' + PartNumber);
                        saved = true;
                        break;
                    }
                }
            }
        }
    }
    if (saved == false) {
        alert("Part not saved. No Attribute available. " + PartNumber);
    }
}

function displayResult(Element, PartNumber) {
    // var table = document.getElementById("PickedList");
//    alert("adding Part");
    var table = document.getElementById(Element);
    var row = table.insertRow(-1);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    // cell1.innerHTML = "<input id='Button1' type='button' onclick=\"DeleteResult('" + row.rowIndex + "');\" value='<'/>";
    cell1.innerHTML = "<input id='Button1' type='button' onclick=\"DeleteResult('" + Element + "','" + PartNumber + "');\" value='<'/>";
    cell2.innerHTML = PartNumber;
}


function DeleteResult(Element, Data) {
    //var table = document.getElementById("PickedList");
    var table = document.getElementById(Element);

    var RowToDelete = null;
    for (var i = 0, row; row = table.rows[i]; i++) {
        //iterate through rows
        //rows would be accessed using the "row" variable assigned in the for loop
        for (var j = 0, col; col = row.cells[j]; j++) {
            if (col.innerHTML == Data) {
                RowToDelete = row;
                //iterate through columns
                //columns would be accessed using the "col" variable assigned in the for loop
            }
            if (RowToDelete != null) { break; }
        }
        if (RowToDelete != null) { break; }
    }
    if (RowToDelete != null) { table.deleteRow(RowToDelete.rowIndex); }
    //    document.getElementById("PickedList").deleteRow(row);
}

function OpenEmailWindow() {
    var service = new WebServer_01();
    //    alert("OpenEmailWindow");
    service.GetESNEmail01_Message(MCL('RECEIVEDETAILID').value, MCL('CurrentProcess').value, MCL('Username').value, onSuccessOpenEmailWindow, null, null);
}


function GetEmailHTML(ID, message) {
    var OutputHTML = '';
    var emailTo = '';
    var emailCCTo = '';
    emailTo = MCL('CLIENTLOCATIONEMAIL').value;
    emailCCTo = MCL('CLIENTLOCATIONEMAIL2').value;
    if (emailCCTo.length > 0) {
        emailTo += ';' + emailCCTo;
    }
    var Subject = 'GMP Repair:' + MCL('LastESN').value;
    var Body = 'IMEI ' + MCL('LastESN').value + ', ' + message;

    if (MCL('CurrentProcess').value.substr(0, 10).toUpperCase() == 'GMP REPAIR') {
        //Subject = 'Authorization Required';
        Body = message;
    }

    var MiscText = '"' + emailTo + ": " + Subject + '"';
    var Note = '';                    // '"' + Body + '"';

    //    var MiscText = '"XXXXX"';
    //    var Note = '"YYYYYYY"';


    OutputHTML = "<a href='mailto:" + emailTo;
    OutputHTML += '?subject=' + Subject;
    OutputHTML += '&body=' + Body;
    OutputHTML += "' onClick='RecordContact(" + MiscText + "," + Note + ");'>Create email to send</a>";




    //    RecordContact(ReceiveDetailID, Message, Note)
    //    <a href="#" onClick="myFunc();"><img src="image.jpg"></a>




    //<a href="mailto:name@domain.com,john@doe.com?cc=sales@here.com& bcc=admin@there.com&subject=Complaint& body=Dear sir.%0AI have a complaint to make">Mail us!</a>


    //alert("Leaving:" + OutputHTML);
    //alert(OutputHTML.length);
    return OutputHTML;
}

function LoadOrderEntryIMEIList_OK() {
    var service = new WebServer_01();
    service.GetOrderEntryESNList(MCL('txtOrderNumber').value, MCL('UserName').value, onSuccessGetOrderEntryESNList, null, null);
}

function onSuccessGetOrderEntryESNList(result) {
    MCL('txtIMEIList').value = result;
}

function IMEIBulk_OK() {
    var IMEIListGood = '';
    var IMEIListBad = '';
    var IMEIList = MCL('txtIMEIList');
    IMEIList.value = IMEIList.value.replace(/ /g, '\n');   // space
    IMEIList.value = IMEIList.value.replace(/\t/g, '\n');  // Tab
    IMEIList.value = IMEIList.value.replace(/\r/g, '\n');  // CR
    MCL('ESN').value = 'ESNGOESHERE';  // Set the ESN SPACE SO THE TRUE ESN VALUES CAN BE INSERTED IN IMEIBULK
    if (OKToSaveEdits() == true) {
        MCL('LBLIMEISTATUS').innerHTML = 'Processing... One Moment Please\n(Window will close when finished)';
        ResetFields('Save', 35);           // todo: 35 needs to be mapped to the Process Database for the name record called 'SAVE'.
        MCL('ESN').value = 'ESNGOESHERE';  // Set the ESN SPACE SO THE TRUE ESN VALUES CAN BE INSERTED IN IMEIBULK
        var ds = GetDataStream(true);
        MCL('ESN').value = '';
        var service = new WebServer_01();
        if (isReceiveScreen() == true) {     // We need to advance the ESN if there.
            service.IMEIBulkAdd(IMEIList.value.replace(/\n/g, ','), ds, MCL('UserName').value, 'basic', true, onIMEISuccess, onIMEIError, null);
        }
        else {
            service.IMEIBulkAdd(IMEIList.value.replace(/\n/g, ','), ds, MCL('UserName').value, 'basic', false, onIMEISuccess, onIMEIError, null);
        }
        MCL('txtIMEIList').value = 'Processing...';
    }
    MCL('ESN').value = '';
    return;
}

function onSuccess_LoadClientLocation(B) {
    ProcessToSetUp = MCL('CurrentProcess').value;
    ClientLocationID = MCL('ClientLocationID').value;
    // we do not want to change the client if it is one of our Receive screens from external
    if ((ProcessToSetUp.toUpperCase() != 'RECEIVEDOA'
            && ProcessToSetUp.toUpperCase() != 'RECEIVEDOAB'
            && ProcessToSetUp.toUpperCase() != 'RECEIVEWARRANTYB'
            && ProcessToSetUp.toUpperCase() != 'RECEIVEDEFECTIVE'
            && ProcessToSetUp.toUpperCase() != 'RECEIVEREPAIRED'
            && ProcessToSetUp.toUpperCase() != 'RECEIVEGENERAL'
            && ProcessToSetUp.toUpperCase() != 'RECEIVEINWARRANTY'
            && ProcessToSetUp.toUpperCase() != 'RECEIVEEXWARRANTY'
            && ProcessToSetUp.toUpperCase() != 'RECEIVEOOWARRANTY')
        || (ClientLocationID.length == 0)) {
        LoadClientLocation(B[1]);
        ScanFocus();
        return;
    }
    else {
        uppdateStatusPanelError('Can not load Client' + LoadClientLocation(B[1]));
        return;
    }
}
// ************************************************************************

function OKToProceed(btnName, IDField) {
    MCL('NextStep').value = '';
    MCL('NextStepID').value = '';
    MCL('NextProcess').value = btnName;
    MCL('NextProcessID').value = IDField;
    if (dirty == true) {
        var answer = confirm('Data not Saved!\nContinue without saving?');
        if (answer == false) {
            uppdateStatusPanelError('Data not Saved! - Save Data first');
            //                    alert('Exiting to allow Save');
            return;
        }
        uppdateStatusPanelError('Data not Saved!\nContinued without saving!');
        alert('Data not saved.');
    }
    dirty = false;
    uppdateStatusPanelYellow('Changing Process to ' + btnName + '!');
    MCL('btnNextProcess').click();
    return false;
}

///////////////////////////////
function ProjectTagUpdate(NewProjectTag) {
    if (isReceiveScreen() == true) {
        alert('Unable to run this command from this process screen');
    }
    var service = new WebServer_01();
    var rValue = service.ProjectTagUpdate(MCL('RECEIVEDETAILID').value, NewProjectTag, MCL('UserName').value, onProjectTagUpdateSuccess);
    uppdateStatusPanelYellow('Updating Project Tag Change!');
}

function onProjectTagUpdateSuccess(result) {
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.Result == 'Saved') {
        uppdateStatusPanel('Project Tag Updated');
        MCL('pTag').value = resultList.NewProjectTag;
        // update project tag field
    }
    ScanFocus();
}

//////////////////////
function RMANumberUpdate(NewRMANumber) {
    if (isReceiveScreen() == true) {
        alert('Unable to run this command from this process screen');
    }
    var service = new WebServer_01();
    var rValue = service.RMANumberUpdate(MCL('RECEIVEDETAILID').value, NewRMANumber, MCL('UserName').value, onRMANumberUpdateSuccess);
    uppdateStatusPanelYellow('Updating RMA Number Change!');
}



function SetupToAuthorize(AuthorizationNumber) {
    if (isReceiveScreen() == true) {
        alert('Unable to run this command from this process screen');
        uppdateStatusPanelError('Unable to run this command from this process screen');
        return;
    }
    AuthorizationNumber = AuthorizationNumber.substr(5);
    var keys = AuthorizationNumber.split(":");
    var service = new WebServer_01();
    // service.AuthorizeRepair(keys[0], keys[1], UserName(), OnAuthorizeSuccess, null, null);
    service.AuthorizeAuthorization(keys[0], keys[1], UserName(), OnAuthorizeSuccess, null, null);
}

function OnAuthorizeSuccess(result) {
    var LineText = result;
    uppdateStatusPanel(LineText);
}


function onRMANumberUpdateSuccess(result) {
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.Result == 'Saved') {
        uppdateStatusPanel('RMA Number Updated');
        MCL('RMA').value = resultList.NewRMANumber;
        // update project tag field
    }
    ScanFocus();
}


///////////////////////////////////////////////////////

function ChangeclientProcess(ClientLocationScanCode) {
    if (isReceiveScreen() == true) {
        alert('Unable to run this command from this process screen');
        return;
    }
    var answer = confirm('Are you sure you want to run the XCLX command?\nDoing so will Change the client on this Unit.');
    if (!answer) { alert('XCLX Canceled!'); return; }
    uppdateStatusPanelYellow('Processing XCLX ...');
    var ds = GetDataStream(true);
    uppdateStatusPanelYellow('Changing Unit Client Location !');
    var service = new WebServer_01();
    var rValue = service.XCLXProcess(ClientLocationScanCode, MCL('ReceiveDetailID').value, MCL('UserName').value, onXCLXProcessSuccess, onXCLXProcessError);
}

function onXCLXProcessError(result) {
    uppdateStatusPanelError('XCLX Error:' + result);
    ScanFocus();
}

function onXCLXProcessSuccess(result) {
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.Result == 'Saved') {
        //        alert('Inside Saved.');
        LoadSheetDataDetail(resultList.ReceiveDetailID);
        uppdateStatusPanel('(' + resultList.UnitCount + ') Units - Client Location Updated!');
    }
    else {
        //        alert('Inside Not Saved Saved.' + resultList.Result);
        uppdateStatusPanelError(resultList.Result);
    }
    ScanFocus();
}




////////////////////////
function DONEXBulkProcess(BinNumber) {
    if (isReceiveScreen() == true) {
        alert('Unable to run this command from this process screen');
        return;
    }
    var answer = confirm('Are you sure you want to run the XDONEX command?\nDoing so will show all ESN numbers in this bin as Processed.');
    if (!answer) { alert('XDONEX Canceled!'); return; }
    uppdateStatusPanelYellow('Processing XDONEX ...');
    var ds = GetDataStream(true);
    uppdateStatusPanelYellow('Saving XDONE Bin data!');
    var service = new WebServer_01();
    var rValue = service.DONEXBulkProcess(BinNumber, ds, onDONEXBulkProcessSuccess, onDONEXBulkProcessError);
}

function onDONEXBulkProcessError(result) {
    uppdateStatusPanelError('XDONEX Error:' + result);
    ScanFocus();
}

function onDONEXBulkProcessSuccess(result) {
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.Result == 'Saved') {
        uppdateStatusPanel('(' + resultList.UnitCount + ') Units - Process Saved!');
    }
    else {
        uppdateStatusPanelError(resultList.Result);
    }
    ScanFocus();
}












function BinBulkProcess(BinNumber) {
    if (isReceiveScreen() == true) {
        alert('Unable to run this command from this process screen');
        return;
    }

    var answer = confirm('Are you sure you want to run the XBINX command?\nDoing so will update all ESN numbers in this bin.');
    if (!answer) { alert('XBINX Canceled!'); return; }

    uppdateStatusPanelYellow('Processing XBIBX ...');
    var ds = GetDataStream(true);
    uppdateStatusPanelYellow('Saving Bin data!');
    var service = new WebServer_01();
    var rValue = service.BinBulkProcess(BinNumber, ds, onBinBulkProcessSuccess, onBinBulkProcessError);
}

function onBinBulkProcessError(result) {

    var stackTrace = result.get_stackTrace();
    var message = result.get_message();
    var statusCode = result.get_statusCode();
    var exceptionType = result.get_exceptionType();
    var timedout = result.get_timedOut();
    var eString =
            "Stack Trace: " + stackTrace + "<br/>" +
            "Service Error: " + message + "<br/>" +
            "Status Code: " + statusCode + "<br/>" +
            "Exception Type: " + exceptionType + "<br/>" +
            "Timedout: " + timedout;
    alert("Error: " + eString);
    uppdateStatusPanelError('Error:' + eString);
    ScanFocus();
}

function onBinBulkProcessSuccess(result) {
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.Result == 'Saved') {
        uppdateStatusPanel('(' + resultList.UnitCount + ') Units - Data Saved!');
    }
    else {
        uppdateStatusPanelError(resultList.Result);
    }
    ScanFocus();
}

function LocBulkProcess(LocNumber) {
    if (isReceiveScreen() == true) {
        alert('Unable to run this command from this process screen');
        return;
    }
    var answer = confirm('Are you sure you want to run the XLOCX command?\nDoing so will update all ESN numbers in this bin.');
    if (!answer) { alert('XLOCX Canceled!'); return; }
    var ds = GetDataStream(true);
    var service = new WebServer_01();
    var rValue = service.LocBulkProcess(LocNumber, ds, onLocBulkProcessSuccess);
    uppdateStatusPanelYellow('Saving Loc data!');
}

function onLocBulkProcessSuccess(result) {
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.Result == 'Saved') {
        uppdateStatusPanel('(' + resultList.UnitCount + ') Units - Data Saved!');
    }
    ScanFocus();
}





function PartUsageStatus() {
    // Look to see if there are any parts on this screen.
    // if no parts, return ""
    // If we have parts, 
    // look to see if all requested parts are assigned
    // Those that are picked and those that are on the unit.



    return "";  // Return blank if all is ok.
}


function GetReplacementIMEIValue() {
    var ReplaceIMEI = MCL('OPTIONKEYREPLACEIMEI').value;
    var ds = GetDataStream(false);
    var DataList = ds.split(',');
    for (y in DataList) {
        var dta = DataList[y].split(':');
//        alert("XXX" + dta[0].replace(/'/g, '') + ":" + dta[1].replace(/'/g, ''));
        if (dta[0].replace(/'/g, '') == ReplaceIMEI) { return dta[1].replace(/'/g, ''); }
//        if (dta[0].length > 0) {
//            var eID = $get(dta[0]);
//            if (eID != null) {
//                eID.style.color = '';
//            }
//        }
    }


//    alert(ds);

//    alert('Leaving Empty Handed');
    return "";
}

function AddDataFromBulk() {
    var ds = GetDataStream();
    var service = new WebServer_01();
    var rValue = service.ReceiveDataFromBulk(ds, onReceiveFromBulkAddSaveSuccess);
    uppdateStatusPanelYellow('Searching for Bulk Data!');
}

function onReceiveFromBulkAddSaveSuccess(result) {
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.Result == 'NotFound') {
        uppdateStatusPanelError('Item not found in Bulk, Data not saved!');
        alert('Item not found in Bulk, Data not saved');
        return;
    }
    if (resultList.Result == 'NotSaved') {
        uppdateStatusPanelError('Error, Data not saved!');
        alert('Error, Data not saved');
        return;
    }

    if (resultList.Result == 'Saved') {
        MCL('RECEIVEHEADERID').value = resultList.ReceiveHeaderID;
        MCL('RECEIVEDETAILID').value = resultList.ReceiveDetailID;
        dirty = false;
    }
    uppdateStatusPanel('Item Saved from bulk!');
    dirty = false;
    MCL('LastESN').value = MCL('ESN').value;
    MCL('LastESNVersion').value = MCL('ESNVersion').value;
    MCL('ESN').value = '';
    MCL('ESNVersion').value = '';
    ScanFocus();
    if (MCL('AutoPrint').checked == true) {
        GenerateBagTag();
    }
}

function AddDataBulk() {
    var ds = GetDataStream();
    var service = new WebServer_01();
    var rValue = service.AddDataBulk(ds, onBulkAddSaveSuccess);
    uppdateStatusPanelYellow('Saving Bulk data!');
}

function onBulkAddSaveSuccess(result) {
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.Result == 'Saved') {
        MCL('ReceiveHeaderID').value = resultList.ReceiveHeaderID;
        MCL('ReceiveDetailBulkID').value = resultList.ReceiveDetailBulkID;
        MCL('ReceiveDetailID').value = -1;
        MCL('SearchReturnMode').value = '';
        dirty = false;
        uppdateStatusPanel('Added to Bulk data!');
    }
    else {
        uppdateStatusPanelError('Error, Data not added!');
    }
    // remove the qty to allow it to be filled again.
    MCL('QTY').value = '';
    ScanFocus();
}

function MoveDataBulk() {
    var ds = GetDataStream();
    GatherData('Target');
    var ts = GetDataStream();
    var service = new WebServer_01();
    var rValue = service.MoveDataBulk(ds, ts, onBulkMoveSuccess);
    uppdateStatusPanelYellow('Moving Bulk data!');
}

function onBulkMoveSuccess(result) {
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.Result == 'Saved') {
        MCL('ReceiveHeaderID').value = resultList.ReceiveHeaderID;
        MCL('ReceiveDetailBulkID').value = resultList.ReceiveDetailBulkID;
        MCL('ReceiveDetailID').value = -1;
        MCL('SearchReturnMode').value = '';
        dirty = false;
        uppdateStatusPanel('Bulk data Moved!');
    }
    else {
        uppdateStatusPanelError('Error: ' + resultList.Error);
    }
    // remove the qty to allow it to be filled again.
    MCL('QTY').value = '';
    ScanFocus();
}

/////////////////////////////////

function AddTData() {
    SaveSticky();
    var ds = GetDataStream(true);
    var service = new WebServer_01();
    var rValue = service.AddDataDetailTSave(ds, onAddTSaveSuccess, onAddTSaveSuccessError);
    uppdateStatusPanelYellow('TSaving data!');
}

function onAddTSaveSuccessError(exception) {
    //    alert('Error:' + exception.get_message());
    uppdateStatusPanelError('TError:' + exception.get_message());
}


function onAddTSaveSuccess(result) {
    MCL('DOAUTHORIZE').value = '-1';
    MCL('hdnAllowDupAdd').value = 'N';
    result = '({' + result + '})';
    var resultList = eval(result);
    ProcessToSetUp = MCL('CurrentProcess').value;

    if (resultList.Result != 'Saved') {
        MCL('ESN').value = '';
        uppdateStatusPanelError(resultList.Error);
        ScanFocus();
        return;
    }
    if (resultList.Result == 'Saved') {


        MCL('ReceiveHeaderID').value = resultList.ReceiveHeaderID;
        MCL('ReceiveDetailBulkID').value = resultList.ReceiveDetailBulkID;
        MCL('ReceiveDetailID').value = resultList.ReceiveDetailID;
        MCL('SearchReturnMode').value = '';
        MCL('lblMakeModelTitle').innerHTML = resultList.MMS;
        MCL('lblProjectClientLocationBinTitle').innerHTML = resultList.PCLB;

        MCL('AR').value = resultList.AR;

        MCL('LastESN').value = MCL('ESN').value;
        MCL('LastESNVersion').value = MCL('ESNVersion').value;
        MCL('ESN').value = '';
        MCL('ESNVersion').value = '';


        dirty = false;
        //        UpdateProcessCheckList(resultList.CompProcList);
        uppdateStatusPanel('Data Saved!');
        RecordHistory(MCL('LastESN').value);

        //        if (ProcessToSetUp.toUpperCase() == 'COMMUNICATION') {
        //            OpenEmailWindow();
        //        }
        //        if (ProcessToSetUp.toUpperCase() == 'GMP REPAIR' && resultList.AR == 'Y') {             // Need to do this only if Approval required.
        //            OpenEmailWindow();
        //        }


        //        if (ProcessToSetUp.toUpperCase() == 'RECEIVEDOA'
        //        || ProcessToSetUp.toUpperCase() == 'RECEIVEDOAB'
        //        || ProcessToSetUp.toUpperCase() == 'RECEIVEWARRANTYB'
        //        || ProcessToSetUp.toUpperCase() == 'RECEIVEDEFECTIVE'
        //        || ProcessToSetUp.toUpperCase() == 'RECEIVEREPAIRED'
        //        || ProcessToSetUp.toUpperCase() == 'RECEIVEGENERAL'
        //        || ProcessToSetUp.toUpperCase() == 'RECEIVEINWARRANTY'
        //        || ProcessToSetUp.toUpperCase() == 'RECEIVEEXWARRANTY'
        //        || ProcessToSetUp.toUpperCase() == 'RECEIVEOOWARRANTY') {
        //            MCL('LastESN').value = MCL('ESN').value;
        //            MCL('LastESNVersion').value = MCL('ESNVersion').value;
        //            DoSetESN = false;
        //            // This is required because the printing of the bagtag
        //            //      had errors and problems with the Load Data.
        //            if (MCL('AutoPrint').checked == true || IsNumeric(MCL('hdnAllowProjectPassThrough').value) == true) {
        //                LoadSheetDataDetail(resultList.ReceiveDetailID, true)
        //            }
        //            else {
        //                LoadSheetDataDetail(resultList.ReceiveDetailID)
        //            }
        //            return;
        //        }
        //        if (MCL('AutoPrint').checked == true || IsNumeric(MCL('hdnAllowProjectPassThrough').value) == true) {
        //            GenerateBagTag();
        //        }
    }

    ScanFocus();
}


/////////////////////////////////

function AddData() {
    if (MCL('ISCLIENTSCREEN').value.length > 0) {
        var answer = confirm('Save Unit Data?');
        if (!answer) {
            alert('Save Canceled!');
            return;
        }
    }
    //    CurrentESN = MCL('ESN').value;

    //alert("Saving the ESN Number");

    //alert('here 01:');
    if (AllowProcess == false) { return; }
    //alert('here 02:');

    AllowProcess = false;
    SaveSticky();
    var SaveMessage = "'Saving data!'";
    var RunThreaded = 'N';
    var ds = GetDataStream(true);

    // JIM REMOVE
    //alert('AddData -- Creating the WebService_01');
    //   /////////////////////////////////////////////////////////////////////

    var service = new WebServer_01();

    // JIM REMOVE
   // alert('AddData -- WebService_01 Created, starting the timer');
    //   /////////////////////////////////////////////////////////////////////
    Timer1.Start();


    // JIM REMOVE
    //alert('AddData -- Calling the WebService_01.AddDataDetailThreaded');
    //   /////////////////////////////////////////////////////////////////////

    var rValue = service.AddDataDetailThreaded(ds, 'N', onAddSaveSuccessThreaded, onAddSaveSuccessError);
    ///////////////////////////////////////////////////////////////////////////////////////////////
    uppdateStatusPanelYellow(SaveMessage);
}

function AddDataAlertOK(Key) {
    if (MCL('ISCLIENTSCREEN').value.length > 0) {
        var answer = confirm('Save Unit Data?');
        if (!answer) {
            alert('Save Canceled!');
            return;
        }
    }
    SaveSticky();
    var SaveMessage = "'Saving data!'";
    var RunThreaded = 'N';
    var ds = GetDataStream(true);
    var service = new WebServer_01();
    Timer1.Start();
    var rValue = service.AddDataDetailThreadedAlert(ds, 'N', Key, onAddSaveSuccessThreaded, onAddSaveSuccessError);
    ///////////////////////////////////////////////////////////////////////////////////////////////
    uppdateStatusPanelYellow(SaveMessage);
}


function onAddSaveSuccessError(exception) {
    //    alert('Error:' + exception.get_message());
    // JIM REMOVE
    //alert('onAddSaveSuccessError -- Calling the WebService_01.AddDataDetailThreaded resulted in an error');
    //alert('onAddSaveSuccessError -- Timer Stopped');
    //   /////////////////////////////////////////////////////////////////////
    Timer1.Stop();
    AllowProcess = true;
    uppdateStatusPanelError('Error:' + exception.get_message());
    alert('Error:' + exception.get_message());
    alert("Stack Trace: " + exception.get_stackTrace());
}


function ProcessAlert(Message) {
    var txt;
    var r = confirm(Message);
    if (r == true) {
        return true;
    }
    return false;
}

function onAddSaveSuccessThreaded(result) {    
    // JIM REMOVE
    //alert('onAddSaveSuccessThreaded -- Calling the WebService_01.AddDataDetailThreaded resulted in success');
    //alert('onAddSaveSuccessThreaded -- Timer Stopped');
    //   /////////////////////////////////////////////////////////////////////
    Timer1.Stop();
    //alert('heresss:' + resultList.Result);
    AllowProcess = true;
    MCL('DOAUTHORIZE').value = '-1';
    MCL('hdnAllowDupAdd').value = 'N';
    result = '({' + result + '})';
    var resultList = eval(result);
    ProcessToSetUp = MCL('CurrentProcess').value;

    if (resultList.Result == 'Alert') {
        //jmbeep();
        if (processAlert(resultList.Error + "\n\nContinue with save?") == true) {
            AddDataAlertOK("OK");
        }
        return;
    }


    if (resultList.Result != 'Saved') {
        if (resultList.Error == 'Need PO Line') {
            uppdateStatusPanelError('Pick PO by Line');
            POPickLineData = resultList.DataBack
            OpenPickPOLineNumber(resultList.LineHTML);
            return;
        }

//        MCL('ESN').value = '';
        uppdateStatusPanelError(resultList.Error);
        ScanFocus();

        if (resultList.Error == "Unused assigned parts. Data Not Saved") {
            //jmbeep();
            alert('Unused Parts Assigned To This Unit!\n\nThis unit has a part assigned to it, that was not used.\nPlease return the unused part to the parts department to enable the save.');
        }
//        else
//        {
//            jmbeep();
//            alert(resultList.Error);
//        }
        return;
    }
    if (resultList.Result == 'Saved') {
        // 
        //  -- If there was a Cellbie Receive Error, a message should pop up here.
        //
        if (resultList.Cellbie.length > 0) {
            //jmbeep();
            alert(resultList.Cellbie);
        }
        //
        //
        //
        //


        MCL('ReceiveHeaderID').value = resultList.ReceiveHeaderID;
        MCL('ReceiveDetailBulkID').value = resultList.ReceiveDetailBulkID;
        MCL('ReceiveDetailID').value = resultList.ReceiveDetailID;
        MCL('AR').value = resultList.AR;
        MCL('lblMakeModelTitle').innerHTML = resultList.MMS;
        MCL('lblProjectClientLocationBinTitle').innerHTML = resultList.PCLB;
        MCL('ISTHREADEDSAVE').value = resultList.THREADED;

        MCL('SearchReturnMode').value = '';
        MCL('LastESN').value = MCL('ESN').value;
        MCL('LastESNVersion').value = MCL('ESNVersion').value;
        MCL('ESN').value = '';
        MCL('ESNVersion').value = '';


        dirty = false;
        UpdateProcessCheckList(resultList.CompProcList);
        uppdateStatusPanel('Data Saved! ' + Timer1.ElapsedMilliseconds + 'ms');
//        alert('AAAAAAAAAA' + resultList.ReceiveDetailID);
        RecordHistory(MCL('LastESN').value);
//        alert('BBBBBBBB' + resultList.ReceiveDetailID);
        //        RecordHistory('Time:' + Timer1.ElapsedMilliseconds + 'ms');
        if (ProcessToSetUp.toUpperCase() == 'COMMUNICATION') {
            OpenEmailWindow();
        }
//        alert('CCCCCCCC' + resultList.ReceiveDetailID);
        //        alert("ProcessToSetUp:" + ProcessToSetUp.toUpperCase() + ":" + resultList.AR);
        if ((ProcessToSetUp.toUpperCase() == 'GMP REPAIR' || ProcessToSetUp.toUpperCase() == 'LAB BILLING') && resultList.AR == 'Y') {             // Need to do this only if Approval required.
            OpenEmailWindow();
        }
//        alert('DDDDDDDDD' + resultList.ReceiveDetailID);
        if (ProcessToSetUp.toUpperCase() == 'RECEIVEDOA'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEDOAB'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEWARRANTYB'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEDEFECTIVE'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEREPAIRED'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEGENERAL'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEINWARRANTY'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEEXWARRANTY'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEOOWARRANTY') {
//            alert('EEEEEEEE' + ProcessToSetUp.toUpperCase());
            MCL('LastESN').value = MCL('ESN').value;
            MCL('LastESNVersion').value = MCL('ESNVersion').value;
            DoSetESN = false;

            if (resultList.ReceiveDetailID != '-1') {
                //            // This is required because the printing of the bagtag
                //            //      had errors and problems with the Load Data.
                if (MCL('AutoPrint').checked == true || IsNumeric(MCL('hdnAllowProjectPassThrough').value) == true || MCL('HDNFORCEPRINTONSAVE').value == "Y") {
                    LoadSheetDataDetail(resultList.ReceiveDetailID, true);
                }
                else {
                    LoadSheetDataDetail(resultList.ReceiveDetailID);
                }
            }
            else {
                //uppdateStatusPanel('Data Saved!. ' + Timer1.ElapsedMilliseconds + 'ms');
            }


            return;
        }


        // JIM ERROR
//        alert('Got data back' + resultList.ReceiveDetailID);
        if (resultList.ReceiveDetailID != '-1') {
//            alert('xxGot data back' + resultList.ReceiveDetailID);
            if (MCL('AutoPrint').checked == true || IsNumeric(MCL('hdnAllowProjectPassThrough').value) == true || MCL('HDNFORCEPRINTONSAVE').value == "Y") {
//                alert('Calling Generate Bagtag');
                GenerateBagTag();
                //uppdateStatusPanel('Data Saved!.. ' + Timer1.ElapsedMilliseconds + 'ms');
            }
        }

        var IndexValue = MCL('drpProjectList').selectedIndex;
        if (MCL('drpProjectList').options[IndexValue].text.toUpperCase() == 'CLIENT PORTAL') {
            ClearDataKeepClient();
        }


        //        alert("Leaving onAddSaveSuccess");

//        return;
    }
    ScanFocus();
}


function onAddSaveSuccess(result) {
    Timer1.Stop();
    MCL('DOAUTHORIZE').value = '-1';
    MCL('hdnAllowDupAdd').value = 'N';
    result = '({' + result + '})';
    var resultList = eval(result);
    ProcessToSetUp = MCL('CurrentProcess').value;

    if (resultList.Result != 'Saved') {
        MCL('ESN').value = '';
        uppdateStatusPanelError(resultList.Error);
        ScanFocus();
        return;
    }
    if (resultList.Result == 'Saved') {
        MCL('ReceiveHeaderID').value = resultList.ReceiveHeaderID;
        MCL('ReceiveDetailBulkID').value = resultList.ReceiveDetailBulkID;
        MCL('ReceiveDetailID').value = resultList.ReceiveDetailID;
        MCL('AR').value = resultList.AR;
        MCL('lblMakeModelTitle').innerHTML = resultList.MMS;
        MCL('lblProjectClientLocationBinTitle').innerHTML = resultList.PCLB;


        MCL('SearchReturnMode').value = '';
        MCL('LastESN').value = MCL('ESN').value;
        MCL('LastESNVersion').value = MCL('ESNVersion').value;
        MCL('ESN').value = '';
        MCL('ESNVersion').value = '';


        dirty = false;
        UpdateProcessCheckList(resultList.CompProcList);
        //uppdateStatusPanel('Data Saved!');
        uppdateStatusPanel('Data Saved! ' + Timer1.ElapsedMilliseconds + 'ms');
        RecordHistory(MCL('LastESN').value);
        //        RecordHistory('Time:' + Timer1.ElapsedMilliseconds + 'ms');

        if (ProcessToSetUp.toUpperCase() == 'COMMUNICATION') {
            OpenEmailWindow();
        }
        //        alert("ProcessToSetUp:" + ProcessToSetUp.toUpperCase() + ":" + resultList.AR);
        if ((ProcessToSetUp.toUpperCase() == 'GMP REPAIR' || ProcessToSetUp.toUpperCase() == 'LAB BILLING') && resultList.AR == 'Y') {             // Need to do this only if Approval required.
            OpenEmailWindow();
        }

        if (ProcessToSetUp.toUpperCase() == 'RECEIVEDOA'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEDOAB'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEWARRANTYB'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEDEFECTIVE'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEREPAIRED'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEGENERAL'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEINWARRANTY'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEEXWARRANTY'
        || ProcessToSetUp.toUpperCase() == 'RECEIVEOOWARRANTY') {
            MCL('LastESN').value = MCL('ESN').value;
            MCL('LastESNVersion').value = MCL('ESNVersion').value;
            DoSetESN = false;
            // This is required because the printing of the bagtag
            //      had errors and problems with the Load Data.
            if (MCL('AutoPrint').checked == true || IsNumeric(MCL('hdnAllowProjectPassThrough').value) == true) {
                LoadSheetDataDetail(resultList.ReceiveDetailID, true)
            }
            else {
                LoadSheetDataDetail(resultList.ReceiveDetailID)
            }
            return;
        }
        // JIM ERROR
        //alert( MCL('HDNFORCEPRINTONSAVE').value);
        return;

        if (MCL('AutoPrint').checked == true || IsNumeric(MCL('hdnAllowProjectPassThrough').value) == true || MCL('HDNFORCEPRINTONSAVE').value == "Y") {
            GenerateBagTag();
        }
        var IndexValue = MCL('drpProjectList').selectedIndex;
        if (MCL('drpProjectList').options[IndexValue].text.toUpperCase() == 'CLIENT PORTAL') {
            ClearDataKeepClient();
        }
    }

    ScanFocus();
}






function GenerateBagTag() {

//    if (MCL('ESN').value.length == 0 || MCL('ESN').value == 'ESN/IMEI Number') {
//        ScanFocus();
//        return false;
//    }
    //    alert('ESN Valuec:' + MCL('ESN').value + ':');

//    alert("Inside GenerateBagTag");

    if (MCL('CurrentProcess').value.toUpperCase() == 'BULKRECEIVE' || MCL('CurrentProcess').value.toUpperCase() == 'BULKMOVE') { return; }
    if (MCL('ESN').value.length == 0 && MCL('LastESN').value.length == 0) { alert('You need to set a ESN Number in advance first'); ScanFocus(); return; }
    if (IsNumeric(MCL('ClientLocationID').value) == false) { alert('You must enter a Client first!'); ScanFocus(); return; }

    if (IsNumeric(MCL('hdnAllowProjectPassThrough').value) == true) { OpenClientbagTag(); return; }


    if (MCL('CurrentProcess').value.substr(0, 7).toUpperCase() == 'KITTING') { OpenFinishProductLabel(); return; }
    if (MCL('CurrentProcess').value.substr(0, 18).toUpperCase() == 'SHIPPING GMP SALES') { OpenFinishProductLabel(); return; }
    if (MCL('CurrentProcess').value.substr(0, 13).toUpperCase() == 'BRIDGE REPAIR' ||
        MCL('CurrentProcess').value.substr(0, 11).toUpperCase() == 'LAB BILLING' ||
        MCL('CurrentProcess').value.substr(0, 9).toUpperCase() == 'LAB ADMIN') { OpenSelectRepairReport(); return; }
    OpenbagTag();
}

function OpenRepairForm(RPT) {

    CloseSelectRepairReport();
    var xDataList = {};
    xDataList['A'] = MCL('RECEIVEDETAILID').value;
    xDataList['B'] = '';
    xDataList['C'] = RPT;
    var pstring = GetParameterStream(xDataList);
    var sax = MCL('HDNFORCEPRINTONSAVE').value;
    if (sax == "Y") { pstring = pstring + "&SAX=Y"; }

    var WindowToOpen = 'RPT_RepairForm.aspx';
    if (pstring.length > 0) {
        WindowToOpen = WindowToOpen + '?' + pstring
    }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
    // win.focus();
}

function OpenFinishProductLabel() {
    // some how we need to look to see if we are on a "Force Print on Save" process. If so, we need to send this parameter as "Y".
    // SAX = "Y".  This will stop the automatic print/exit from the page.
    // If this hidden field carries "Y":    FORCEPRINTONSAVE

    var pstring = GetParameterStream(GetReportParameterList('PRODUCTLABEL'));
    var sax = MCL('HDNFORCEPRINTONSAVE').value;
    if (sax == "Y") {pstring = pstring + "&SAX=Y"; }
    var WindowToOpen = 'FinishProductLabel.aspx';
    if (pstring.length > 0) {
        WindowToOpen = WindowToOpen + '?' + pstring
    }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
    // win.focus();
}

function OpenClientbagTag() {
    var pstring = GetParameterStream(GetReportParameterList('CLIENTSUBMIT'));
    //           var WindowToOpen = 'RPT_EXCEL_Out.aspx';
    var WindowToOpen = 'RPT_Submission.aspx';
    var PortalName = MCL('hdnDealerPortal').value;
    if (PortalName == 'DP_01') {
        WindowToOpen = 'RPT_Submission_01.aspx';
    }
    if (PortalName == 'DP_02') {
        WindowToOpen = 'RPT_Submission_02.aspx';
    }

    if (pstring.length > 0) {
        WindowToOpen = WindowToOpen + '?' + pstring
    }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
    ScanFocus();
    return;
}


function OpenbagTag() {
    var report = 'Bagtag';
    var pstring = GetParameterStream(GetReportParameterList(report));
    var sax = MCL('HDNFORCEPRINTONSAVE').value;
    if (sax == "Y") { pstring = pstring + "&SAX=Y"; }
    var WindowToOpen = 'BagTag.aspx';
    if (pstring.length > 0) {
        WindowToOpen = WindowToOpen + '?' + pstring
    }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
}


function OpenReturnPartbagTag(ID) {
    var IsGood = "";
    var report = 'RETURNPART';
    var pstring = GetParameterStream(GetReportParameterList(report));
    var sax = MCL('HDNFORCEPRINTONSAVE').value;
    if (sax == "Y") { pstring = pstring + "&SAX=Y"; }

    var r = confirm("Hit OK Is this a good Part?");
    if (r == true) {
        IsGood = "Good";
    } else {
        IsGood = "Bad";
    } 
    pstring = pstring + "&ID=" + ID + "&ISGOOD=" + IsGood;
    var WindowToOpen = 'BagTag.aspx';
    if (pstring.length > 0) {
        WindowToOpen = WindowToOpen + '?' + pstring
    }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
}

function OpenDefectiveReturnPartbagTag(ID) {
    var IsGood = "";
    var report = 'DEFECTIVERETURNPART';
    var pstring = GetParameterStream(GetReportParameterList(report));
    var sax = MCL('HDNFORCEPRINTONSAVE').value;
    if (sax == "Y") { pstring = pstring + "&SAX=Y"; }

//    var r = confirm("Hit OK Is this a good Part?");
//    if (r == true) {
//        IsGood = "Good";
//    } else {
//        IsGood = "Bad";
//    }
    pstring = pstring + "&ID=" + ID;
    var WindowToOpen = 'BagTag.aspx';
    if (pstring.length > 0) {
        WindowToOpen = WindowToOpen + '?' + pstring
    }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
}


function LoadMacroChain(result) {
    var Bs = result.split(';')
    for (var i = 0; i < Bs.length; i++) {
        if (Bs[i].length > 0) {
            var B = Bs[i].split(':');
            UpdateFormScanData(B);
        }
    }
    //uppdateStatusPanel('Macro Chain Loaded')
    ScanFocus();
}

function LoadPreReceiveDetail(ScanNumber) {
    var service = new WebServer_01();
    service.LoadPreReceiveDetail(ScanNumber, MCL('UserName').value, OnLoadPreReceiveDetailSuccess, null, null);

}

function OnLoadPreReceiveDetailSuccess(result) {
    var Data = eval('[' + result + ']');

    if (Data[0].Status == 'No Action') { return; }

    if (Data[0].ProjectID > 0 && Data[0].ProjectID != CurrentProjectID()) {
        alert('This unit can only be received into this project:' + Data[0].ProjectName);
        // We need to remove the esn from the esn field
        MCL('ESN').value = '';
        return;
    }

    if (Data[0].RMA.length > 0) { MCL('RMA').value = Data[0].RMA; }
    if (Data[0].ProjectTag.length > 0) { MCL('Ptag').value = Data[0].ProjectTag; }
    if (Data[0].Detail.length > 0) {
        var option = Data[0].Detail.split(';');
        for (n in option) {
            if (option[n].length > 0) {
                var B = option[n].split('|');
                // Jim Off
                //                alert(option[n] + ' B[0]=' + B[0] 
                //                                + ' B[1]=' + B[1]
                //                                + ' B[2]=' + B[2]
                //                                + ' B[3]=' + B[3]
                //                                + ' B[4]=' + B[4]
                //                                + ' B[5]=' + B[5] 
                //                                + ' B[6]=' + B[6]);
                UpdateFormScanData(B);
            }
        }

        // Update the detail data
    }
    ScanFocus();
}

function UpdateFormScanData(B) {
    if (B.length == 0) { return; }
    if (B[1] == '') { uppdateStatusPanel(''); return; }
    // Option:777:Part #:Part 1:P1:TX

    // example result = "Option:226:Serial Number iPhone:Serial Number:SN:TX" + ":" + TransferData + ":" + MessageQueueStop + ":" + MessageQueueMessage.Replace(':', ' ');
    //    alert('UpdateFormScanData' + ' B[0]=' + B[0] + ' B[1]=' + B[1]
    //                                + ' B[2]=' + B[2]
    //                                + ' B[3]=' + B[3]
    //                                + ' B[4]=' + B[4]
    //                                + ' B[5]=' + B[5]
    //                                + ' B[6]=' + B[6]);


    // At this point we should look to see if we are dealing with a part number.
    // If we are, we need to look for the first empty partnumber field and put the data there.
    var PNumbers = MCL('PARTNUMBERIDS').value;
    if (PNumbers.indexOf(B[1]) > -1) {
        // We have a part number;
        PlacePartNumber(B[6]);
        return;
    }
    // If not, then we want to move forward.

    var AreaToInput = MCL('hdnSourceOrTarget').value;
    var cProcess = MCL('CurrentProcess').value.toUpperCase();
    if (cProcess == 'BULKMOVE' && AreaToInput == 'Target') {
        var inputArea = MCL('InputTargetArea');
    }
    else {
        var inputArea = MCL('InPutArea');
    }
    //            // We need to deal with any drop down lists.
    //alert('xxxxxxxxx:');
    var Selects = inputArea.getElementsByTagName('select'); //or document.forms[0].elements;
    for (var i = 0; i < Selects.length; i++) {
        var cOptions = Selects[i].options;
        for (var j = 0; j < cOptions.length; j++) {
            var Key = '';
            var Value = '';
            var cBox = cOptions[j];
            if (cBox.value == B[1]) {
                cBox.selected = true;
                if (cOptions.id == MCL('hdnCarrierID').value) { SetupDropDown('Carrier'); }
                if (cOptions.id == MCL('hdnManufacturerID').value) { SetupDropDown('Manufacturer'); }
                if (cOptions.id == MCL('hdnModelID').value) {
                    SetupDropDown('Model'); 
                }
                //if (cOptions.id == MCL('hdnModelID').value) { SetupDropDown('Model'); }
                uppdateStatusPanel('Field/s updated');
                return;
            }
        }
    }
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == 'checkbox') {
            var cBox = inputs[i];
            var p = cBox.parentNode;
            var currentValue = p.getAttribute('someValue');
            if (parseInt(currentValue) == B[1]) {
                dirty = true;
                if (inputs[i].checked) {
                    inputs[i].checked = false;
                }
                else {
                    inputs[i].checked = true;
                }
                uppdateStatusPanel(B[2] + '/' + B[3]);
                return;
            }
        }

        if (inputs[i].type == 'radio') {
            if (inputs[i].value == B[1]) {
                dirty = true;
                inputs[i].checked = true;
                uppdateStatusPanel(B[2] + '/' + B[3]);
                return;
            }
        }

        if (inputs[i].type == 'text') {
            var currentValue = inputs[i].getAttribute('someValue');
            if (currentValue == B[1]) {
                dirty = true;
                inputs[i].value = B[6];
                uppdateStatusPanel(B[2] + '/' + B[6]);
                return;
            }
        }
    }
    uppdateStatusPanel('Field/s updated');
}

function LoadClientLocation(ID) {
    var service = new WebServer_01();
    var rValue = service.GetClientLocationData(ID, MCL('UserName').value, onClientLoadSuccess, onWebServerError);
}

function onClientLoadSuccess(Result) {
    //       ClientLocation:34:ClientLocation:34:goldie::
    var ClientData = eval('({' + Result + '})');

    if (ClientData != null) {

        if (ClientData.txtClientName == 'Access Denied!') {
            uppdateStatusPanelError('Client Access Denied!');
            return;
        }

        if (ClientData.txtClientName == 'Missing Supplier Number!') {
            uppdateStatusPanelError('Missing Supplier Number!');
            return;
        }

        var IndexValue = MCL('drpProjectList').selectedIndex;
        var projectid = -1;
        if (IndexValue > -1) {
            projectid = MCL('drpProjectList').options[IndexValue].value + ' ';
        }
        var processid = ' ' + MCL('CurrentProcessID').value + ' ';

        // if this string ClientData.ProjectDependencies is blank, move forward.
        // Look to see if the current projectID is one of the keys inside ClientData.Project Dependencies example ' 345 657 45678 '
        // if the Project is not there, then give message
        if (ClientData.ProjectDependencies.length == 0 || ClientData.ProjectDependencies.indexOf(projectid) >= 0) {
            if (ClientData.ProcessDependencies.length == 0 || ClientData.ProcessDependencies.indexOf(processid) >= 0) {
                MCL('ProjectDependencies').value = ClientData.ProjectDependencies;
                MCL('ProcessDependencies').value = ClientData.ProcessDependencies;
                MCL('ClientName').value = ClientData.txtClientName
                MCL('ClientLocationID').value = ClientData.ClientLocationID;
                MCL('CLIENTLOCATIONEMAIL').value = ClientData.Email;
                MCL('CLIENTLOCATIONEMAIL2').value = ClientData.Email2;
                MCL('StoreNumber').value = ClientData.txtStoreNumber;
                MCL('StoreSuffix').value = ClientData.txtStoreSuffix;
                MCL('ClientAddress').value = ClientData.txtClientAddress;
                uppdateStatusPanel('Client Loaded');
                RestrictClientQuestions();
            }
            else {
                alert('This client ' + ClientData.txtClientName + ' can not be set up under this process');
                uppdateStatusPanelError('Client NOT Loaded')
            }
        }
        else {
            alert('This client ' + ClientData.txtClientName + ' can not be set up under this project');
            uppdateStatusPanelError('Client NOT Loaded')
        }
        ScanFocus();
    }
}

function LoadSheetDataDetail(ID, WithBagTag, BumpVersionTo900) {
    if (WithBagTag == null) { WithBagTag = false; }
    if (BumpVersionTo900 == null) { BumpVersionTo900 = false; }
    var service = new WebServer_01();


    //alert('LoadSheetDataDetail');
    if (typeof Popup == 'undefined') { }
    else if (Popup == null) { }
    else { $('#loading').show(); }

    //    if (Popup == 'undefined') { } else { $('#loading').show(); }


    if (WithBagTag == true) {

        //alert('GetDetailSheetData WithBagTag = true');
        service.GetDetailSheetData(ID, MCL('UserName').value, BumpVersionTo900, onDetailLoadSuccess_BagTag, onDetailLoadFail);
        //               service.GetDetailSheetData(ID, onDetailLoadSuccess_BagTag, MCL('UserName').value, onDetailLoadFail);
    }
    else {

        //alert('GetDetailSheetData WithBagTag = false');
        service.GetDetailSheetData(ID, MCL('UserName').value, BumpVersionTo900, onDetailLoadSuccess, onDetailLoadFail);
    }
}

function onDetailLoadFail(Result) {

    if (typeof Popup == 'undefined') { } else { $('#loading').hide(); }
    uppdateStatusPanelError('Get Data Sheet Error...:' + Result);
    alert('Data error:' + Result);
}

function onDetailLoadSuccess(Result) {
    if (typeof Popup == 'undefined') { } else { $('#loading').hide(); }
    uppdateStatusPanelYellow('Processing...2');
    //alert('onDetailLoadSuccess');

    RestoreSheetData(Result);
}

function onDetailLoadSuccess_BagTag(Result) {
    $('#loading').hide();
    RestoreSheetData(Result);
    GenerateBagTag();
}

function CurrentProjectID() {
    var IndexValue = MCL('drpProjectList').selectedIndex;
    var ProjectName = MCL('drpProjectList').options[IndexValue].value;
    return ProjectName;
}
function CurrentProjectName() {
    var IndexValue = MCL('drpProjectList').selectedIndex;
    var ProjectName = MCL('drpProjectList').options[IndexValue].text;
    return ProjectName;
}

function LoadScanNumber(ScanNumber, ShowWarrantyMessage) {
    var cProcess = MCL('CurrentProcess').value.toUpperCase();
    // We do not accept header data from any process other than those below.
    if (isReceiveScreen() == false) {
        //alert('isReceiveScreen==false');
        //jmbeep();
        alert('Unable to update from this process:' + cProcess);
        uppdateStatusPanelError('Unable to update from this process:' + cProcess);
        //jmbeep();
        return;
    }

    //alert('isReceiveScreen==true');
    var ProjectSetup = MCL('ProjSetup').value;
    var tcontainer = MCL('t1x').control;
    var activeTab = tcontainer.get_activeTabIndex();
    var IndexValue = MCL('drpProjectList').selectedIndex;
    var ProjectName = MCL('drpProjectList').options[IndexValue].text;
    var QTY = MCL('QTY').value;
    if (QTY == 'Quantity') { QTY = ''; }
    var PROJTAG = MCL('Ptag').value;
    if (PROJTAG == 'Project Tag') { PROJTAG = ''; }
    var RMA = MCL('RMA').value;
    if (RMA == 'RMA Number') { RMA = ''; }
    if (RMA == 'Work Order Number') { RMA = ''; }

    var ESN = MCL('ESN').value;
    if (ESN == 'ESN/IMEI Number') { ESN = ''; }
    if (cProcess == 'BULKRECEIVE' || cProcess == 'BULKMOVE') {
        if (QTY.length == 0 && IsNumeric(ScanNumber) == true) {
            MCL('QTY').value = ScanNumber;
            uppdateStatusPanel('QTY Set');
            ScanFocus();
            return;
        }
    }
    if (cProcess != 'BULKRECEIVE' && cProcess != 'BULKMOVE') {
        if (ESN.length == 0) {
            MCL('ESN').value = ScanNumber;
            LoadPreReceiveDetail(ScanNumber);
            if (MCL('AUTOSAVE').checked == true) { MCL('btnSave').click(); }
            uppdateStatusPanel('ESN Set');
            if (ShowWarrantyMessage == true) {
                alert('This unit may be within GMPI 90 day warranty period.');
            }
            ScanFocus();
            return;
        }
    }
    if (ProjectSetup.indexOf('ZRMAZZEDITZ') > -1) {
        if (RMA.length == 0) {
            MCL('RMA').value = ScanNumber;
            if (MCL('RMA').value == MCL('ESN').value) {
                MCL('ESN').value = '';
                alert('Please enter your ESN Number again');
                uppdateStatusPanelError('RMA Number equal to ESN');
                return;
            }
            uppdateStatusPanel('RMA Set');
            ScanFocus();
            return;
        }
    }
    if (ProjectSetup.indexOf('ZPTAGZZEDITZ') > -1) {
        if (PROJTAG.length == 0) {
            MCL('Ptag').value = ScanNumber;
            if (MCL('Ptag').value == MCL('ESN').value) {
                MCL('ESN').value = '';
                alert('Please enter your ESN Number again');
                uppdateStatusPanelError('Project Tag equal to ESN');
                return;
            }
            uppdateStatusPanel('Project Tag Set');
            ScanFocus();
            return;
        }
    }
    uppdateStatusPanel('No action done:');
    return;
}

function GetDataStream(isCompressed) {
    if (isCompressed == null) { isCompressed = false; }
    var pValue = '';
    var count = 0;
    var sb = new Sys.StringBuilder();
    GatherData();
    for (var property in DataList) {
        if (count > 0) { sb.append(','); }
        pValue = property;
        if (isCompressed == true) { pValue = CompressKey(property); }
        sb.append("'" + pValue + "':'" + DataList[property] + "'");
        count += 1;
    }
    return sb.toString();
}

function DataStream(DataToStream) {
    var pValue = '';
    var count = 0;
    var sb = new Sys.StringBuilder();
    for (var property in DataToStream) {
        if (count > 0) { sb.append(','); }
        pValue = property;
        sb.append("'" + pValue + "':'" + DataToStream[property] + "'");
        count += 1;
    }
    return sb.toString();
}

//////////////////////////////

//       function GetDataStreamCompressed() {
//           var count = 0;
//           var sb = new Sys.StringBuilder();
//           var plist = new Sys.StringBuilder();
//           GatherData();
//           for (var property in DataList) {
//               if (count > 0) { sb.append(','); plist.append(','); }
//               plist.append(property);
//               sb.append(''' + CompressKey(property) + '':'' + DataList[property] + ''');
//               count += 1;
//           }
//           return sb.toString();
//       }


function CleanText(strValue) {
    strValue = strValue.replace(' ', '');
    strValue = strValue.replace(' ', '');
    strValue = strValue.replace(' ', '');
    strValue = strValue.replace('/', '');
    strValue = strValue.replace('*', '');
    strValue = strValue.replace('#', '');
    strValue = strValue.replace('.', '');
    return strValue;
}

function GatherData(Area) {
    DataList = GetParameterList();
    DataList = GetDropDownList(Area, DataList);
    DataList = GetCheckBoxList(Area, DataList);
    DataList = GetRadioButtonList(Area, DataList);
    DataList = GetTextList(Area, DataList);
    //DataList = GetTextList(Area, DataList);  //<<------   This appeared to be a duplicate by mistake. Not sure why it is there, remmed it out Dec 5, 2017 
    DataList['OPENTIME'] = MCL('hdnOpenTime').value;
    return;
}

function SaveSticky() {
    MCL('StickyData').value = '';
    if (MCL('Sticky') != null) {
        if (MCL('Sticky').checked == true) {
            var Area = null;
            var StickyDataList = {};
            StickyDataList = GetDropDownList(Area, StickyDataList);
            StickyDataList = GetCheckBoxList(Area, StickyDataList);
            StickyDataList = GetRadioButtonList(Area, StickyDataList);
            StickyDataList = GetTextList(Area, StickyDataList);
            var data = DataStream(StickyDataList);
            MCL('StickyData').value = data;
        }
    }
}


function GetInputArea(Area) {
    if (Area == null) { return MCL('InPutArea'); }
    else { return MCL('InputTargetArea'); }
}
function GetDropDownList(Area, xDataList) {
    var inputArea = GetInputArea(Area);
    // We need to deal with any drop down lists.
    var Selects = inputArea.getElementsByTagName('select'); //or document.forms[0].elements;
    for (var i = 0; i < Selects.length; i++) {
        var cOptions = Selects[i].options;
        for (var j = 0; j < cOptions.length; j++) {
            var Key = '';
            var Value = '';
            var cBox = cOptions[j];
            if (cBox.selected == true) {
                Key = 'DD_' + cBox.value;
                Value = '1';
                xDataList[Key] = Value;
                break;
            }
        }
    }
    return xDataList;
}
function GetCheckBoxList(Area, xDataList) {
    var inputArea = GetInputArea(Area);
    // We need to deal with other types.
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
    var Dummy_CB_Sent = 0;
    for (var i = 0; i < inputs.length; i++) {
        var Key = '';
        var Value = '';
        var cBox = inputs[i];
        if (cBox.type == 'checkbox') {
            var p = cBox.parentNode;
            var currentValue = p.getAttribute('someValue');
            Key = 'CB_' + currentValue;
            Value = '0';
            if (cBox.checked == true) { Value = '1'; }
            if (Value == '1' || Dummy_CB_Sent == 0) { Dummy_CB_Sent = 1; xDataList[Key] = Value; }
        }
    }
    return xDataList;
}
function GetRadioButtonList(Area, xDataList) {
    var inputArea = GetInputArea(Area);
    // We need to deal with other types.
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
    var Dummy_CB_Sent = 0;
    for (var i = 0; i < inputs.length; i++) {
        var Key = '';
        var Value = '';
        var cBox = inputs[i];
        if (cBox.type == 'radio') {
            Dummy_CB_Sent = 0;
            Key = 'RD_' + cBox.value;
            if (cBox.checked) { Value = '1'; } else { Value = '0'; }
            if (Value == '1') { xDataList[Key] = Value; }
        }
    }
    return xDataList;
}

function GetTextList(Area, xDataList) {
    var inputArea = GetInputArea(Area);
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
    var Dummy_CB_Sent = 0;
    for (var i = 0; i < inputs.length; i++) {
        var Key = '';
        var Value = '';
        var cBox = inputs[i];
        if (cBox.type == 'text') {
            Dummy_CB_Sent = 0;
            var xName = cBox.name
            if (xName.indexOf('ScanKey') == -1) {
                var p = cBox.parentNode;
                var currentValue = cBox.getAttribute('someValue');
                //<<-----------------  here is where it takes on the Null value in the TX_null that is showing up. 
                //                     something is causing it to "not get set"

                Key = 'TX_' + currentValue;
                Value = cBox.value;
                Value = EncodeData(Value);
                xDataList[Key] = Value;
            }
        }
    }
    return xDataList;
}

function GetParameterList() {
    var xDataList = {};
    xDataList['ISTHREAD'] = MCL('ISTHREADEDSAVE').value;
    xDataList['hdnCalledFrom'] = MCL('hdnCalledFrom').value;
    xDataList['DoAuthorize'] = MCL('DOAUTHORIZE').value;
    xDataList['ClientLocationID'] = MCL('ClientLocationID').value;
    xDataList['CurProcessID'] = MCL('CurrentProcessID').value;
    xDataList['NextProcessID'] = MCL('NextProcessID').value;
    xDataList['NextStepID'] = MCL('NextStepID').value;
    xDataList['ReceiveHeaderID'] = MCL('ReceiveHeaderID').value;
    xDataList['ReceiveDetailBulkID'] = MCL('ReceiveDetailBulkID').value;
    xDataList['ReceiveDetailID'] = MCL('ReceiveDetailID').value;

    xDataList['hdnAllowDupAdd'] = MCL('hdnAllowDupAdd').value;

    xDataList['hdnSearchReturnMode'] = MCL('SearchReturnMode').value;
    xDataList['CurProcess'] = MCL('CurrentProcess').value;
    xDataList['NextProcess'] = MCL('NextProcess').value;
    xDataList['NextStep'] = MCL('NextStep').value;
    xDataList['CurUserName'] = MCL('UserName').value;
    xDataList['CurStepUp'] = MCL('StepUp').value;
    xDataList['PROJSet'] = MCL('ProjSetup').value;
    var IndexValue = MCL('drpProjectList').selectedIndex;
    xDataList['Project'] = '';
    xDataList['ProjectID'] = -1;
    if (IndexValue > -1) {
        xDataList['Project'] = MCL('drpProjectList').options[IndexValue].text;
        xDataList['ProjectID'] = MCL('drpProjectList').options[IndexValue].value;
    }
    xDataList['QTY'] = MCL('QTY').value;
    xDataList['PROJTAG'] = MCL('Ptag').value;
    xDataList['RMA'] = MCL('RMA').value;
    xDataList['ESN'] = MCL('ESN').value;
    xDataList['ReceiveDate'] = MCL('DateReceived').value;

    // We need to remove the watermark
    if (xDataList['PROJTAG'] == 'Project Tag') { xDataList['PROJTAG'] = ''; }
    if (MCL('QTY').value == 'Quantity') { xDataList['QTY'] = 0; }
    if (MCL('Ptag').value == 'Project Tag') { xDataList['PROJTAG'] = ''; }
    if (MCL('RMA').value == 'RMA Number') { xDataList['RMA'] = ''; }
    if (MCL('RMA').value == 'Work Order Number') { xDataList['RMA'] = ''; }
    if (MCL('ESN').value == 'ESN/IMEI Number') { xDataList['ESN'] = ''; }
    if (MCL('DateReceived').value == 'Date Received') { xDataList['ReceiveDate'] = ''; }
    return xDataList;
}

function GetReportParameterList(Report) {
    var xDataList = {};
    xDataList['RPT'] = Report;
    xDataList['ISTHREAD'] = MCL('ISTHREADEDSAVE').value;
    xDataList['CP'] = MCL('CurrentProcess').value;
    xDataList['ESN'] = MCL('ESN').value;
    xDataList['LESN'] = MCL('LastESN').value;
    xDataList['CLID'] = MCL('ClientLocationID').value;
    xDataList['RDID'] = MCL('ReceiveDetailID').value;
    xDataList['ReceiveDate'] = MCL('DateReceived').value;
    xDataList['CurProcessID'] = MCL('CurrentProcessID').value;

    if (MCL('ESN').value == 'ESN/IMEI Number') { xDataList['ESN'] = ''; }
    if (MCL('DateReceived').value == 'Date Received') { xDataList['ReceiveDate'] = ''; }

    if (Report.toUpperCase() == 'BAGTAG') { return xDataList; }
    if (Report.toUpperCase() == 'PRODUCTLABEL') { return xDataList; }

    xDataList['RHID'] = MCL('ReceiveHeaderID').value;
    xDataList['RDBID'] = MCL('ReceiveDetailBulkID').value;
    xDataList['UserName'] = MCL('UserName').value;
    xDataList['CurStepUp'] = MCL('StepUp').value;

    var IndexValue = MCL('drpProjectList').selectedIndex;
    xDataList['Project'] = '';
    xDataList['ProjectID'] = -1;
    if (IndexValue > -1) {
        xDataList['Project'] = MCL('drpProjectList').options[IndexValue].text;
        xDataList['ProjectID'] = MCL('drpProjectList').options[IndexValue].value;
    }

    xDataList['PROJTAG'] = MCL('Ptag').value;
    xDataList['RMA'] = MCL('RMA').value;
    // We need to remove the watermark
    if (MCL('RMA').value == 'RMA Number') { xDataList['RMA'] = ''; }
    if (MCL('RMA').value == 'Work Order Number') { xDataList['RMA'] = ''; }
    if (MCL('Ptag').value == 'Project Tag') { xDataList['PROJTAG'] = ''; }
    return xDataList;
}


function GetReceiveIDKeys() {
    var xDataList = {};
    xDataList['ReceiveHeaderID'] = MCL('ReceiveHeaderID').value;
    xDataList['ReceiveDetailBulkID'] = MCL('ReceiveDetailBulkID').value;
    xDataList['ReceiveDetailID'] = MCL('ReceiveDetailID').value;
    xDataList['SearchReturnMode'] = MCL('SearchReturnMode').value;
    var IndexValue = MCL('drpProjectList').selectedIndex;
    xDataList['Project'] = '';
    xDataList['ProjectID'] = -1;
    if (IndexValue > -1) {
        xDataList['Project'] = MCL('drpProjectList').options[IndexValue].text;
        xDataList['ProjectID'] = MCL('drpProjectList').options[IndexValue].value;
    }

    return xDataList;
}

function GetHeaderData() {
    var xDataList = {};
    //           xDataList['TESTCOMMA'] = 'McComb, Jim -- xx,!@#$%^&*()';
    if (MCL('lblVersionTab') != null) { xDataList['VTC'] = MCL('lblVersionTab').style.color; }

    if (MCL('lblAuthorizationTab') != null) { xDataList['ATC'] = MCL('lblAuthorizationTab').style.color; }
    if (MCL('lblHistoryTab') != null) { xDataList['HTC'] = MCL('lblHistoryTab').style.color; }

    xDataList['DTC'] = MCL('lblDataTab').style.color;

    xDataList['CID'] = MCL('ClientLocationID').value;
    var IndexValue = MCL('drpProjectList').selectedIndex;
    xDataList['Project'] = '';
    xDataList['ProjectID'] = -1;
    if (IndexValue > -1) {
        xDataList['Project'] = MCL('drpProjectList').options[IndexValue].text;
        xDataList['ProjectID'] = MCL('drpProjectList').options[IndexValue].value;
    }

    xDataList['Process'] = MCL('CurrentProcess').value;
    xDataList['PROJSet'] = MCL('ProjSetup').value;

    xDataList['PROJTAG'] = MCL('Ptag').value;
    xDataList['RMA'] = MCL('RMA').value;
    xDataList['ESN'] = MCL('ESN').value;
    xDataList['ReceiveDate'] = MCL('DateReceived').value;
    xDataList['QTY'] = MCL('QTY').value;


    xDataList['CNAME'] = MCL('ClientName').value;
    xDataList['CNUM'] = MCL('StoreNumber').value;
    xDataList['CSUF'] = MCL('StoreSuffix').value;
    xDataList['CADD'] = MCL('ClientAddress').value;

    //           // We need to remove the watermark
    if (xDataList['CNAME'] == 'Client Name') { xDataList['CNAME'] = ''; }
    if (xDataList['CNUM'] == 'Store Number') { xDataList['CNUM'] = ''; }
    if (xDataList['CSUF'] == 'Store Suffix') { xDataList['CSUF'] = ''; }
    if (xDataList['CADD'] == 'Location Address') { xDataList['CADD'] = ''; }

    if (xDataList['QTY'] == 'Quantity') { xDataList['QTY'] = ''; }
    if (xDataList['PROJTAG'] == 'Project Tag') { xDataList['PROJTAG'] = ''; }
    if (xDataList['RMA'] == 'RMA Number') { xDataList['RMA'] = ''; }
    if (xDataList['RMA'] == 'Work Order Number') { xDataList['RMA'] = ''; }

    if (xDataList['ESN'] == 'ESN/IMEI Number') { xDataList['ESN'] = ''; }
    if (xDataList['ReceiveDate'] == 'Date Received') { xDataList['ReceiveDate'] = ''; }
    return xDataList;

}

function StoreHeaderData() {
    MCL('HeaderData').value = Sys.Serialization.JavaScriptSerializer.serialize(GetHeaderData());
    //alert("Store Data:" + MCL('HeaderData').value);
}

function RestoreHeaderData(SetESN, SetRMA, SetDateReceived, SetProjectTag) {
    if (SetESN == null) { SetESN = true; }
    if (SetRMA == null) { SetRMA = true; }
    if (SetDateReceived == null) { SetDateReceived = true; }
    if (SetProjectTag == null) { SetProjectTag = true; }
    var dta = MCL('HeaderData').value;          //              MCL('HdnHeaderData').value;

    //alert("Restore Data:" + dta);
    xDataList = Sys.Serialization.JavaScriptSerializer.deserialize(dta, true);
    MCL('ESN').value = '';
    MCL('RMA').value = '';
    var now = new Date();
    MCL('DateReceived').value = now.format('MM/dd/yyyy hh:mm tt');
    MCL('pTag').value = '';
    if (MCL('lblVersionTab') != null) { MCL('lblVersionTab').style.color = xDataList['VTC']; }



    if (MCL('lblAuthorizationTab') != null) { MCL('lblAuthorizationTab').style.color = xDataList['ATC']; }
    if (MCL('lblHistoryTab') != null) { MCL('lblHistoryTab').style.color = xDataList['HTC']; }
    MCL('lblDataTab').style.color = xDataList['DTC'];

    if (SetESN == true) { MCL('ESN').value = xDataList['ESN']; }
    if (SetRMA == true) { MCL('RMA').value = xDataList['RMA']; }
    if (SetDateReceived == true) { MCL('DateReceived').value = xDataList['ReceiveDate']; }
    if (SetProjectTag == true) { MCL('pTag').value = xDataList['PROJTAG']; }

    MCL('QTY').value = xDataList['QTY'];
    MCL('ClientName').value = xDataList['CNAME'];
    MCL('StoreNumber').value = xDataList['CNUM'];
    MCL('StoreSuffix').value = xDataList['CSUF'];
    MCL('ClientAddress').value = xDataList['CADD'];
}


function ClearData() {
    if (MCL('lblVersionTab') != null) {
        MCL('lblVersionTab').style.color = '';
    }
    MCL('lblProcessHeader').style.color = '';
    MCL('SearchReturnMode').value = '';

    MCL('ClientLocationID').value = '-1';
    MCL('CLIENTLOCATIONEMAIL').value = '';
    MCL('CLIENTLOCATIONEMAIL2').value = '';
    MCL('ClientName').value = '';
    MCL('StoreNumber').value = '';
    MCL('StoreSuffix').value = '';
    MCL('ClientAddress').value = '';



    MCL('ReceiveHeaderID').value = '-1';
    MCL('ReceiveDetailBulkID').value = '-1';
    MCL('ReceiveDetailID').value = '-1';
    MCL('RMA').value = '';
    MCL('ESN').value = '';
    MCL('QTY').value = '';
    MCL('pTag').value = '';




    var now = new Date();
    MCL('DateReceived').value = now.format('MM/dd/yyyy hh:mm tt');
    ClearData_Section(MCL('HeaderArea'));
    ClearData_Section(MCL('InputArea'));
    ShowAllQuestions();
    uppdateStatusPanel('Data cleared!');
    ScanFocus();
}

function ClearDataKeepClient() {
    if (MCL('lblVersionTab') != null) {
        MCL('lblVersionTab').style.color = '';
    }
    MCL('lblProcessHeader').style.color = '';
    MCL('SearchReturnMode').value = '';
    //    MCL('ClientLocationID').value = '-1';
    //    MCL('CLIENTLOCATIONEMAIL').value = '';

    MCL('ReceiveHeaderID').value = '-1';
    MCL('ReceiveDetailBulkID').value = '-1';
    MCL('ReceiveDetailID').value = '-1';
    MCL('RMA').value = '';
    MCL('ESN').value = '';
    MCL('QTY').value = '';
    MCL('pTag').value = '';
    //    MCL('ClientName').value = '';
    //    MCL('StoreNumber').value = '';
    //    MCL('StoreSuffix').value = '';
    //    MCL('ClientAddress').value = '';
    var now = new Date();
    MCL('DateReceived').value = now.format('MM/dd/yyyy hh:mm tt');
    ClearData_Section(MCL('HeaderArea'));
    ClearData_Section(MCL('InputArea'));
    ShowAllQuestions();
    uppdateStatusPanel('Data cleared!');
    ScanFocus();
}

function ClearData_Section(inputArea) {
    //            // We need to deal with any drop down lists.
    var Selects = inputArea.getElementsByTagName('select'); //or document.forms[0].elements;
    for (var i = 0; i < Selects.length; i++) {
        var cOptions = Selects[i].options;
        for (var j = 0; j < cOptions.length; j++) {
            var cBox = cOptions[j];
            cBox.selected = true;
            break;
        }
    }
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
    for (var i = 0; i < inputs.length; i++) {
        var Key = '';
        if (inputs[i].type == 'checkbox') { inputs[i].checked = false }
        if (inputs[i].type == 'radio') { inputs[i].checked = false; }

        if (inputs[i].type == 'text') {
            var xName = inputs[i].name
            if (xName.indexOf('ScanKey') == -1) {
                var currentValue = inputs[i].getAttribute('someValue');
                var currentData = '';
                Key = 'TX_' + currentValue;
                if (Key.length > 3 && Key.length < 10) { inputs[i].value = ''; }
            }
        }
    }
}


function RestoreSheetData(Data) {
    //return;

    //alert('RestoreSheetData:' + Data);
    ClearData();
    uppdateStatusPanelYellow('Processing...3');
    xDataList = eval('({' + Data + '})');
    uppdateStatusPanelYellow('Processing...4');
    if (xDataList['ESN'] == 'Access Denied!') { uppdateStatusPanelError('ESN Access Denied!'); return; }
    if (xDataList['ESN'].substr(0, 6).toUpperCase() == 'Froze:') { uppdateStatusPanelError(xDataList['ESN']); return; }

    var cProcess = MCL('CurrentProcess').value;
    var cProcessID = MCL('CurrentProcessID').value;
    var cProcessIDx = ' ' + MCL('CurrentProcessID').value + ' ';
    var cProcess_1 = xDataList['CurP'];
    cProcess = trim(cProcess).toUpperCase();
    cProcess_1 = trim(cProcess_1).toUpperCase();

    xDataList['Project'] = trim(xDataList['Project']);
    MCL('hdnOpenTime').value = xDataList['hdnOpenTime'];

//    alert('xxxx' + xDataList['hdnOpenTime']);
//    alert('yyyy' + MCL('hdnOpenTime').value);
//    alert('jjjj' + Data);

    // Do we have more than one version of this ESN, show it by setting the tab colour red.
//    if (MCL('lblVersionTab') != null) {
//        if (xDataList['O_VERSION'] == '0') { MCL('lblVersionTab').style.color = ''; }
//        else if (xDataList['O_VERSION'] == '1') { MCL('lblVersionTab').style.color = ''; }
//        else if (xDataList['O_VERSION'] == '2') { MCL('lblVersionTab').style.color = '#FF9900'; }
//        else { MCL('lblVersionTab').style.color = '#CC0000'; }


//        if (xDataList['O_VERSION'] == '0') { MCL('lblProcessHeader').style.color = ''; }
//        else if (xDataList['O_VERSION'] == '1') { MCL('lblProcessHeader').style.color = ''; }
//        else if (xDataList['O_VERSION'] == '2') { MCL('lblProcessHeader').style.color = '#FF9900'; }
//        else { MCL('lblProcessHeader').style.color = '#CC0000'; }
//    }
    uppdateStatusPanelYellow('Processing...4a');
    if (xDataList['ProcessDependencies'].length > 0 && xDataList['ProcessDependencies'].indexOf(cProcessIDx) < 0) {
        MCL('DOAUTHORIZE').value = '-1';
        alert('ESN found, but incorrect Process (' + cProcess + ') to Load');
        uppdateStatusPanelError('This process not required for this Client ');
        ScanFocus();
        return;
    }
    uppdateStatusPanelYellow('Processing...4b');
    if (cProcess != cProcess_1 && cProcess != 'SEARCH' && cProcess_1 != 'SAVE') {
        MCL('DOAUTHORIZE').value = '-1';
        alert('ESN found, but incorrect Process (' + cProcess + '/' + cProcess_1 + ') to Load');
        uppdateStatusPanelError('ESN found, but incorrect Process (' + cProcess + '/' + cProcess_1 + ') to Load');
        ScanFocus();
        return;
    }

    var IndexValue = MCL('drpProjectList').selectedIndex;
    var pProject = MCL('drpProjectList').options[IndexValue].text;

    pProject = trim(pProject)
    uppdateStatusPanelYellow('Processing...4c');
    if (xDataList['Project'] != pProject) {
        var service = new WebServer_01();
        var rValue = service.CanUnitJumpProjects(xDataList['ProjectID'], cProcess, onCanUnitJumpProjectSuccess);
        return;
    }
    uppdateStatusPanelYellow('Processing...5');
    // if ((cProcess.toUpperCase() == 'SHIPPING' || cProcess.toUpperCase() == 'QC ASSESSMENT') && xDataList['NEEDHCA'] == 'T' && MCL('DOAUTHORIZE').value == '-1') {
    // as per email 05/18/2012
    if ((cProcess.toUpperCase() == 'LAB BILLING' || cProcess.toUpperCase() == 'QC ASSESSMENT') && xDataList['NEEDHCA'] == 'T' && MCL('DOAUTHORIZE').value == '-1') {
        MCL('DOAUTHORIZE').value = '-1';
        alert('This unit has not yet Received the HardCopy Authorization\nThis unit can not be shipped yet');
        uppdateStatusPanelError('HardCopy Authorization Required');
        ScanFocus();
        return;
    }
    if (cProcess == cProcess_1 || cProcess == 'SEARCH' || cProcess_1 == 'SAVE') {
        //
        //alert('cProcess=' + cProcess_1);
        MCL('ProjectDependencies').value = xDataList['ProjectDependencies'];
        MCL('ProcessDependencies').value = xDataList['ProcessDependencies'];

        MCL('lblMakeModelTitle').innerHTML = xDataList['MMS'];
        MCL('lblProjectClientLocationBinTitle').innerHTML = xDataList['PCLB'];


        MCL('hdnCarrierIDx').value = xDataList['CarrierID'];
        MCL('hdnManufacturerIDx').value = xDataList['ManufactuerID'];
        MCL('hdnModelIDx').value = xDataList['ModelID'];
        MCL('hdnColourIDx').value = xDataList['ColourID'];
        //MCL('hdnColourIDx').value = xDataList['ColourID'];
        //alert('Carrier:' + xDataList['CarrierID'] + ':' + MCL('hdnCarrierIDx').value + ':' + MCL('hdnCarrierID').value);
        //alert('Manufacturer:' + xDataList['ManufactuerID'] + ':' + MCL('hdnManufacturerIDx').value + ':' + MCL('hdnManufacturerID').value);
        //alert('Model:' + xDataList['ModelID'] + ':' + MCL('hdnModelIDx').value + ':' + MCL('hdnModelID').value);
        //alert('Colour:' + xDataList['ColourID'] + ':' + MCL('hdnColourIDx').value);
        //alert('Colour:' + xDataList['ColourID'] + ':' + MCL('hdnColourIDx').value + ':' + MCL('hdnColourID').value);




        MCL('hdnMemoryIDx').value = xDataList['MemoryID'];
        MCL('hdnClientIDx').value = xDataList['CLIENTID'];

        MCL('SearchReturnMode').value = 'Edit';
        MCL('ReceiveHeaderID').value = xDataList['RHID'];
        MCL('ReceiveDetailBulkID').value = xDataList['RDBID'];
        MCL('ReceiveDetailID').value = xDataList['RDID'];
        MCL('Ptag').value = xDataList['PROJTAG'];
        MCL('RMA').value = xDataList['RMA'];
        MCL('LastESN').value = xDataList['ESN'];
        MCL('LastESNVersion').value = MCL('ESNVersion').value;
        if (DoSetESN == true) {
            MCL('ESN').value = xDataList['ESN'];
            MCL('ESNVersion').value = xDataList['ESNVERSION'];
        }
        MCL('QTY').value = xDataList['QTY'];
        MCL('ClientLocationID').value = xDataList['CLID'];
        MCL('ClientName').value = xDataList['CNAME'];
        MCL('StoreNumber').value = xDataList['CNUM'];
        MCL('StoreSuffix').value = xDataList['CSUF'];
        MCL('ClientAddress').value = xDataList['CADD'];
        MCL('CLIENTLOCATIONEMAIL').value = xDataList['Email'];
        MCL('CLIENTLOCATIONEMAIL2').value = xDataList['Email2'];


        //        alert(MCL('CLIENTLOCATIONEMAIL').value);
        if (xDataList['ReceiveDate'].length > 0) { MCL('DateReceived').value = xDataList['ReceiveDate']; }
        if (cProcess == 'SEARCH') { MCL('lblActiveProcess').innerHTML = xDataList['CurP']; }
        uppdateStatusPanelYellow('Processing...6');
        UpdateProcessCheckList(xDataList['CompProcList']);
        uppdateStatusPanelYellow('Processing...7');
        ScatterData(xDataList);
        uppdateStatusPanelYellow('Processing...8');
        if (xDataList['ESNVERSION'] == '000') {
            //if ((cProcess.toUpperCase() == 'SHIPPING' || cProcess.toUpperCase() == "COMMUNICATION") && xDataList['SetShippingDefaults'] == 'Y')
            if ((cProcess.toUpperCase() == 'SHIPPING') && xDataList['SetShippingDefaults'] == 'Y') {
                // We need to set these attributes.
                //Ship To” to be populated with the “Dealer ID” and the question “PO #” populated with the “Service Request #”.
                //Ship TO = cl.ScanKey
                //PO # =
                var yDataList = {};
                yDataList[xDataList['O_ShipToID']] = xDataList['DealerID'];
                yDataList[xDataList['O_PO']] = xDataList['ServiceRequestNum'];
                ScatterData_Section(yDataList, MCL('InPutArea'));
            }

            if (MCL('Sticky') != null) {
                if (MCL('Sticky').checked == true && MCL('StickyData').value.length > 0) {
                    var StickyDataList = eval('({' + MCL('StickyData').value + '})');
                    ScatterData(StickyDataList);
                    uppdateStatusPanelYellow('Processing...9');
                }
            }
        }


        FillDropDown('Carrier');
        uppdateStatusPanelYellow('Processing...10');

        RestrictClientQuestions();
        uppdateStatusPanelYellow('Processing...11');
        if (DoSetESN == true) { uppdateStatusPanel('ESN Loaded'); }
        if (DoSetESN == false) { uppdateStatusPanel('ESN Saved'); }
        if (MCL('AutoSave').checked == true) { RecordScanKey('DOSAVE'); }

    }
    else
    { uppdateStatusPanelError('Unknown display Error'); }

    DoSetESN = true;  // Reset if it is set to false, so next time around it will display
    ScanFocus();
}


function onCanUnitJumpProjectSuccess(result) {
    uppdateStatusPanelYellow('Processing...4e');
    if (result == "true") {
        uppdateStatusPanelYellow('Processing...4f');
        var cProcess = MCL('CurrentProcess').value;
        cProcess = trim(cProcess).toUpperCase();
        OpenUnit(xDataList['RDID'], xDataList['ProjectID'], cProcess)
    }
    else {
        var IndexValue = MCL('drpProjectList').selectedIndex;
        var pProject = MCL('drpProjectList').options[IndexValue].text;
        MCL('DOAUTHORIZE').value = '-1';
        alert('ESN found, but incorrect Project (' + pProject + ')\nOpen Project (' + xDataList['Project'] + ') to Load');
        uppdateStatusPanelError('ESN found, but incorrect Project, Open Project (' + xDataList['Project'] + ') to Load');
        ScanFocus();
        return;
    }
}




function UpdateProcessCheckList(IDList) {
    if (IDList.length > 0) {
        var ProjectIDList = IDList.split(',');
        if (ProjectIDList.length > 0) {
            var inputArea = MCL('chkProcessCheckList');
            if (inputArea != null) {
                var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;
                if (inputs != null) {
                    for (var i = 0; i < inputs.length; i++) {
                        if (inputs[i].type == 'checkbox') {
                            var cBox = inputs[i];
                            var p = cBox.parentNode;
                            var currentValue = p.getAttribute('someValue');
                            var x = 0;
                            //                                if (parseInt(currentValue) == B[1]) {
                            inputs[i].checked = false;
                            for (x = 0; x < ProjectIDList.length; x++) {
                                if (currentValue == ProjectIDList[x]) { inputs[i].checked = true; break; }
                            }
                        }
                    }
                }
            }
        }
    }
}


function ScatterData(xDataList) {
    ScatterData_Section(xDataList, MCL('HeaderArea'));
    ScatterData_Section(xDataList, MCL('InPutArea'));
}

function ScatterData_Section(xDataList, inputArea) {
    var inputs = inputArea.getElementsByTagName('input'); //or document.forms[0].elements;

    // We need to deal with any drop down lists.
    var Selects = inputArea.getElementsByTagName('select'); //or document.forms[0].elements;
    for (var i = 0; i < Selects.length; i++) {
        var cOptions = Selects[i].options;
        for (var j = 0; j < cOptions.length; j++) {
            var Key = '';
            var Value = '';
            var cBox = cOptions[j];
            Key = 'DD_' + cBox.value;
            if (xDataList[Key] != null) {
                //alert('Scatter Data Key Found:' + Key);
                if (xDataList[Key] == '1') { cBox.selected = true; }
                break;
            }
        }
    }
    // Checkboxes, radio buttons, text boxes
    for (var i = 0; i < inputs.length; i++) {
        var Key = '';
        if (inputs[i].type == 'checkbox') {
            var cBox = inputs[i];
            var p = cBox.parentNode;
            var currentValue = p.getAttribute('someValue');
            Key = 'CB_' + currentValue;
            if (xDataList[Key] != null) {
                if (xDataList[Key] == '1') { inputs[i].checked = true; }
                if (xDataList[Key] != '1') { inputs[i].checked = false; }
            }
        }
        if (inputs[i].type == 'radio') {
            Key = 'RD_' + inputs[i].value;
            if (xDataList[Key] != null) {
                if (xDataList[Key] == '1') { inputs[i].checked = true; }
                if (xDataList[Key] != '1') { inputs[i].checked = false; }
            }
        }
        if (inputs[i].type == 'text') {
            var xName = inputs[i].name
            if (xName.indexOf('ScanKey') == -1) {
                var currentValue = inputs[i].getAttribute('someValue');
                var currentData = '';
                Key = 'TX_' + currentValue;
                if (xDataList[Key] != null) { currentData = xDataList[Key]; }
                currentData = DecodeData(currentData);
                inputs[i].value = currentData;
            }
        }
    }
    return;
}

function ResetFields(btnName, IDField) {
    MCL('NextProcess').value = '';
    MCL('NextProcessID').value = '';
    MCL('NextStep').value = (btnName.toUpperCase() == "TSAVE" ? "Save" : btnName); // "TSave" is an switch used by "SAVE" in order to trigger a save as a transaction, not Live)
    MCL('NextStepID').value = IDField;
}

function AddDelimiter(mText, dText) {
    if (mText.length > 0) { mText = mText + dText; }
    return mText;
}

function EncodeData(data) {
    data = data.replace(/,/g, ' ');
    return data;
}

function DecodeData(data) {
    //data = data.replace(/zbbz/g, ',');
    return data;
}




// ************************************************************************
function CompressKey(key) {
    switch (key.toUpperCase()) {
        case 'CLIENTLOCATIONID': return 'a'; break;
        case 'CURPROCESSID': return 'b'; break;
        case 'NEXTPROCESSID': return 'c'; break;
        case 'NEXTSTEPID': return 'd'; break;
        case 'RECEIVEHEADERID': return 'e'; break;
        case 'RECEIVEDETAILBULKID': return 'f'; break;
        case 'RECEIVEDETAILID': return 'g'; break;
        case 'HDNSEARCHRETURNMODE': return 'h'; break;
        case 'CURPROCESS': return 'i'; break;
        case 'NEXTPROCESS': return 'j'; break;
        case 'NEXTSTEP': return 'k'; break;
        case 'CURUSERNAME': return 'l'; break;
        case 'CURSTEPUP': return 'm'; break;
        case 'PROJSET': return 'n'; break;
        case 'PROJECT': return 'o'; break;
        case 'PROJECTID': return 'p'; break;
        case 'QTY': return 'q'; break;
        case 'PROJTAG': return 'r'; break;
        case 'RMA': return 's'; break;
        case 'ESN': return 't'; break;
        case 'RECEIVEDATE': return 'u'; break;
        case 'HDNALLOWDUPADD': return 'v'; break;
        default: return key;
    }
    return key;
}


function ResetHistory() {
    if (MCL('lstHistory') == null) { return; }

    var Source = MCL('lstHistory');
    var Count = MCL('txtHistoryCount');
    Count.value = '0';
    if (Source != null) {
        var xc = Source.getElementsByTagName('option').length;
        for (var i = 0; i < xc; i++) {
            Source.remove(0);
            var count = Count.value;
            Count.value = count.toString();
            count--
        }
    }
}

function DeleteHistory() {
    if (MCL('lstHistory') == null) { return; }
    var Source = MCL('lstHistory');
    var Count = MCL('txtHistoryCount');
    if (Source != null) {
        if (Source.options.selectedIndex >= 0) {
            Source.remove(Source.options.selectedIndex);
            var count = Count.value;
            count--
            Count.value = count.toString();
        }
    }
}

function RecordHistory(Value) {
    if (MCL('lstHistory') == null) { return; }
    var Source = MCL('lstHistory');
    // Check to see if the item is already there.
    var xc = Source.getElementsByTagName('option').length;
    for (var i = 0; i < xc; i++) {
        if (Source.options[i].value == Value) { return; }
    }
    var Count = MCL('txtHistoryCount');
    var count = Count.value;
    count++
    Count.value = count.toString();
    if (Source != null) {
        var newOption = new Option();
        newOption.text = Value;
        Source.options[Source.length] = newOption;
    }
}

function SetupDropDown(DropDownName) {
//    alert("Here i am");
    if (DropDownName == 'Lab Destination') {

    }
    else {
        FillDropDown(DropDownName);
    }
    return;
}


function MoveToGraveYard(ReceiveDetailID) {
    var IndexValue = MCL('drpProjectList').selectedIndex;
    var service = new WebServer_01();
    service.MoveToGraveYard(ReceiveDetailID, MCL('UserName').value, alert('Moved to GraveYard'), null, null);
}

function MoveBackFromGraveYard(ReceiveDetailID) {
    var IndexValue = MCL('drpProjectList').selectedIndex;
    var service = new WebServer_01();
    service.MoveBackFromGraveYard(ReceiveDetailID, MCL('UserName').value, alert('Moved to GraveYard'), null, null);
}


function RestrictClientQuestions() {
    ShowAllQuestions();
    var IndexValue = MCL('drpProjectList').selectedIndex;
    var ProjectID = -1;
    if (IndexValue > -1) { ProjectID = MCL('drpProjectList').options[IndexValue].value; }
    var service = new WebServer_01();
    service.GetClientRestrictedQuestions(MCL('ClientLocationID').value, ProjectID, onSuccessRestrictClientQuestions, null, null);
}


function onSuccessRestrictClientQuestions(result) {
    if (result == "") { return; }
    var ClientIDList = MCL('hdnQuestionClientIDList').value
    var IDList = MCL('hdnQuestionIDList').value
    var ciL = ClientIDList.split(',');
    var idL = IDList.split(',');
    var rL = result.split(',');
    for (n in rL) {
        var id = rL[n];
        for (m in idL) {
            if (idL[m] == id) {
                var dName = ciL[m];
                if (dName.length > 0) {
                    var Cntrol = $get(dName);
                    var tr = getParentByTagName(Cntrol, 'tr');
                    ControlHide(tr);
                }
            }
        }
    }
    return;
}


function ShowAllQuestions() {
    var ClientIDList = MCL('hdnQuestionClientIDList').value
    var IDList = MCL('hdnQuestionIDList').value
    var ciL = ClientIDList.split(',');
    for (n in ciL) {
        var dName = ciL[n];
        if (dName.length > 0) {
            var Cntrol = $get(dName);
            var tr = getParentByTagName(Cntrol, 'tr');
            ControlShow(tr);
        }
    }
}

function HideAllQuestions() {
    var ClientIDList = MCL('hdnQuestionClientIDList').value
    var IDList = MCL('hdnQuestionIDList').value
    var ciL = ClientIDList.split(',');
    for (n in ciL) {
        var dName = ciL[n];
        if (dName.length > 0) {
            var Cntrol = $get(dName);
            var tr = getParentByTagName(Cntrol, 'tr');
            ControlHide(tr);
        }
    }
}

function getParentByTagName(obj, tag) {
    if (obj == null) return null;
    var obj_parent = obj.parentNode;
    if (!obj_parent) return null;
    if (obj_parent.tagName.toLowerCase() == tag) return obj_parent;
    else return getParentByTagName(obj_parent, tag);
}


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function FillDropDown(DropDownName) {
    DisplayIsCarrierLocked(DropDownName);
    var service = new WebServer_01();
    if (DropDownName == 'Carrier') {
        var x = MCL('hdnCarrierID').value;
        if (x == null || x.length == 0) { return; }
        var ctr = $get(MCL('hdnCarrierID').value);
        if (ctr == null) { return; }
        var rValue = service.GetManufacturerDropDownData(GetDropDownValue(MCL('hdnCarrierID').value), MCL('UserName').value, onFillManufacturerList, onFillManufacturerListError, null);
        return;
    }
    if (DropDownName == 'Manufacturer') {
        var x = MCL('hdnCarrierID').value;
        if (x == null || x.length == 0) { return; }
        var ctr = $get(MCL('hdnCarrierID').value);
        if (ctr == null) { return; }
        ctr = $get(MCL('hdnManufacturerID').value);
        if (ctr == null) { return; }
        var rValue = service.GetModelDropDownData(GetDropDownValue(MCL('hdnCarrierID').value), GetDropDownValue(MCL('hdnManufacturerID').value), MCL('UserName').value, onFillModelList, null, null);
        return;
    }
    if (DropDownName == 'Model') {
        var x = MCL('hdnCarrierID').value;
        if (x == null || x.length == 0) { return; }
        var ctr = $get(MCL('hdnCarrierID').value);
        if (ctr == null) { return; }
        ctr = $get(MCL('hdnManufacturerID').value);
        if (ctr == null) { return; }
        ctr = $get(MCL('hdnModelID').value);    
        if (ctr == null) { return; }
        //var rValue = service.GetColourDropDownData(GetDropDownValue(MCL('hdnCarrierID').value), GetDropDownValue(MCL('hdnManufacturerID').value), GetDropDownValue(MCL('hdnModelID').value), MCL('UserName').value, onFillColourList, null, null);
        var rValue = service.GetMemoryDropDownData(GetDropDownValue(MCL('hdnModelID').value), MCL('UserName').value, onFillMMemoryList, onFillManufacturerListError, null);
        return;
    }
    if (DropDownName == 'Memory') {
        var x = MCL('hdnCarrierID').value;
        if (x == null || x.length == 0) { return; }
        var ctr = $get(MCL('hdnCarrierID').value);
        if (ctr == null) { return; }
        ctr = $get(MCL('hdnManufacturerID').value);
        if (ctr == null) { return; }
        ctr = $get(MCL('hdnModelID').value);
        if (ctr == null) { return; }
        var rValue = service.GetColourDropDownData(GetDropDownValue(MCL('hdnCarrierID').value), GetDropDownValue(MCL('hdnManufacturerID').value), GetDropDownValue(MCL('hdnModelID').value), MCL('UserName').value, onFillColourList, null, null);
        return;
    }
}


function onFillManufacturerListError(Result) {
    alert('Error - onFillManufacturerListError:' + Result);
}


function DisplayIsCarrierLocked(msg) {
//    MCL('lblCarrierLock').innerText = 'False:' + msg;
//    if (MCL('hdnisMasterLinked').value == 'True') {
//        MCL('lblCarrierLock').innerText = 'True:' + msg;
//        return; 
//    }
}


function onFillManufacturerList(Result) {
    //DisplayIsCarrierLocked('Manufacturer');
    if (MCL('hdnisMasterLinked').value != 'True') { return; }
    //alert("Fill Manufacturer:" + Result);
    var DropDown = $get(MCL('hdnManufacturerID').value);
    var DTA = "";
    if (DropDown != null) {
        var CurrentValue = MCL('hdnManufacturerIDx').value;
        //var CurrentValue = GetDropDownValue(MCL('hdnManufacturerID').value);
        while (DropDown.options.length > 0) DropDown.remove(0);
        if (Result.length > 0) {

            // This will put it in the order it is sent... Currently, Alpha order.
            ClientData2 = Result.split(",");
            for (var i = 0, l = ClientData2.length; i < l; i++) {
                DTA = ClientData2[i].split(":");
                addOption(DropDown, DTA[0].replace(/['"]+/g, ''), DTA[1].replace(/['"]+/g, ''), CurrentValue);
            }
        }
    }
    FillDropDown('Manufacturer');
    return;
}

function onFillModelList(Result) {
    //DisplayIsCarrierLocked('Model');
    if (MCL('hdnisMasterLinked').value != 'True') { return; }
    //alert("Fill Model:" + Result);
    var DropDown = $get(MCL('hdnModelID').value);
    if (DropDown != null) {
        var CurrentValue = MCL('hdnModelIDx').value;
        //var CurrentValue = GetDropDownValue(MCL('hdnModelID').value);
        while (DropDown.options.length > 0) DropDown.remove(0);
        if (Result.length > 0) {
            // This will put it in the order it is sent... Currently, Alpha order.
            ClientData2 = Result.split(",");
            for (var i = 0, l = ClientData2.length; i < l; i++) {
                DTA = ClientData2[i].split(":");
                addOption(DropDown, DTA[0].replace(/['"]+/g, ''), DTA[1].replace(/['"]+/g, ''), CurrentValue);
            }
        }
    }
    FillDropDown('Model');
    return;
}

function onFillMMemoryList(Result) {
    if (MCL('hdnisMasterLinked').value != 'True') { return; }
    //alert("Fill Memory2:" + Result);
    var DropDown = $get(MCL('hdnMemoryID').value)
    //alert("Fill Memory3:" + Result);
    if (DropDown != null) {
        //alert('aaaaaa');
        var CurrentValue = GetDropDownValue(MCL('hdnMemoryID').value);
        while (DropDown.options.length > 0) DropDown.remove(0);
        if (Result.length > 0) {
            // This will put it in the order it is sent... Currently, Alpha order.
            ClientData2 = Result.split(",");
            for (var i = 0, l = ClientData2.length; i < l; i++) {
                DTA = ClientData2[i].split(":");
                addOption(DropDown, DTA[0].replace(/['"]+/g, ''), DTA[1].replace(/['"]+/g, ''), CurrentValue);
            }
        }
    }
    //alert('bbbbbbb');
    FillDropDown('Memory');
    return;
}

function onFillColourList(Result) {
    //DisplayIsCarrierLocked('Colour');
    if (MCL('hdnisMasterLinked').value != 'True') { return; }
    //alert("Fill Colour:" + Result);
    var DropDown = $get(MCL('hdnColourID').value)
    if (DropDown != null) {
        var CurrentValue = MCL('hdnColourIDx').value;
        //var CurrentValue = GetDropDownValue(MCL('hdnColourID').value);
        while (DropDown.options.length > 0) DropDown.remove(0);
        if (Result.length > 0) {
            // This will put it in the order it is sent... Currently, Alpha order.
            ClientData2 = Result.split(",");
            for (var i = 0, l = ClientData2.length; i < l; i++) {
                DTA = ClientData2[i].split(":");
                addOption(DropDown, DTA[0].replace(/['"]+/g, ''), DTA[1].replace(/['"]+/g, ''), CurrentValue)
            }
        }
    }
    return;
}


function sortDropdownList(ddl, CurrentValue) {

    var options = [].slice.apply(ddl.options, [0]);
    ddl.innerHTML = "";
    var sorted = options.sort(function (a, b) {
        var x = a.innerText.toLowerCase();
        var y = b.innerText.toLowerCase();
        if (x < y) { return -1; }
        if (x > y) { return 1; }
        return 0;
        //        alert("A:" + a.innerText + " B:" + b.innerText);
        //        return +(a.innerText) - +(b.innerText);
    });

    for (var i = 0; i < sorted.length; i++) {
        ddl.options.add(sorted[i]);
    }

}


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


//////function onFillManufacturerListError(Result) {
//////    alert('Error - onFillManufacturerListError:' + Result);
//////}

//////function onFillManufacturerList(Result) {
//////    if (MCL('hdnisMasterLinked').value != 'True') { return; }
//////    var DropDown = $get(MCL('hdnManufacturerID').value);
//////    if (DropDown != null) {
//////        var CurrentValue = GetDropDownValue(MCL('hdnManufacturerID').value);
//////        while (DropDown.options.length > 0) DropDown.remove(0);
//////        if (Result.length > 0) {
//////            ClientData = eval('({' + Result + '})');
//////            for (var key in ClientData) {
//////                var attrName = key;
//////                var attrValue = ClientData[key];
//////                addOption(DropDown, key, ClientData[key], CurrentValue)
//////            }
//////        }
//////    }
//////    FillDropDown('Manufacturer');
//////    return;
//////}


//////function onFillModelList(Result) {
//////    if (MCL('hdnisMasterLinked').value != 'True') { return; }
//////    var DropDown = $get(MCL('hdnModelID').value)
//////    if (DropDown != null) {
//////        var CurrentValue = GetDropDownValue(MCL('hdnModelID').value);
//////        while (DropDown.options.length > 0) DropDown.remove(0);
//////        if (Result.length > 0) {
//////            ClientData = eval('({' + Result + '})');
//////            for (var key in ClientData) {
//////                var attrName = key;
//////                var attrValue = ClientData[key];
//////                addOption(DropDown, key, ClientData[key], CurrentValue)
//////            }
//////        }
//////    }
//////    FillDropDown('Model');
//////    return;
//////}

//////function onFillColourList(Result) {
//////    if (MCL('hdnisMasterLinked').value != 'True') { return; }
//////    var DropDown = $get(MCL('hdnColourID').value)
//////    if (DropDown != null) {
//////        var CurrentValue = GetDropDownValue(MCL('hdnColourID').value);
//////        while (DropDown.options.length > 0) DropDown.remove(0);
//////        if (Result.length > 0) {
//////            ClientData = eval('({' + Result + '})');
//////            for (var key in ClientData) {
//////                var attrName = key;
//////                var attrValue = ClientData[key];
//////                addOption(DropDown, key, ClientData[key], CurrentValue)
//////            }
//////        }
//////    }
//////    return;
//////}

//function GetradiosValue(Name) {
//    var rvalue = 'xx';
//    var test = $get(Name);
//    var sizes = test.length;
////    alert(Name);
////    alert(sizes);
////    alert(test.type);
//    for (i = 0; i < sizes; i++) {
//        if (test[i].checked == true) {
////            alert(test[i].value + ' you got a value');
//            return test[i].value;
//        }
//    }
////    var radios = $get(Name);                    // document.getElementsByName(Name);
////    for (var i = 0, length = radios.length; i < length; i++) {
////        if (radios[i].checked) {
////            alert('Inside');
////            rvalue = radios[i].value;
////            return rvalue;
////        }
////    }
//    return rvalue;
//}

//function GetradiosText(Name) {
//    var rvalue = 'xx';
//    var radios = document.getElementsByName(Name);
//    for (var i = 0, length = radios.length; i < length; i++) {
//        if (radios[i].checked) {
//            rvalue = radios[i].text;
//            return rvalue;
//        }
//    }
//    return rvalue;
//}



function GetDropDownValue(Name) {
    var IndexValue = $get(Name).selectedIndex;
    var xValue = '';
    if (IndexValue > -1) { xValue = $get(Name).options[IndexValue].value; }
    return xValue;
}

function GetDropDownText(Name) {
    var IndexValue = $get(Name).selectedIndex;
    var xValue = '';
    if (IndexValue > -1) { xText = $get(Name).options[IndexValue].text; }
    return xText;
}
function addOption(selectbox, value, text, SelectedValue) {
    var optn = document.createElement('OPTION');
    optn.text = text;
    optn.value = value;
    if (value == SelectedValue) { optn.setAttribute('selected', 'selected'); }
    selectbox.options.add(optn);
}

//////////////////////////////////////////

function ShowTooTip(myControl, Turn) {
    //          $get(myControl).Attributes['Title'];
    if (Turn == true) { MCL('txtToolTip').innerHTML = $get(myControl).title; }
    else { MCL('txtToolTip').innerHTML = ''; }
}


function ShowHidePanel(pnl, show) {
    pnl.style.visibility = show;
}

function ControlShow(cntrl) {
    //    if (cntrl == null) return;
    //    cntrl.style.visibility = 'visible';
    cntrl.style.display = '';
}


function ControlHide(cntrl) {
    //    if (cntrl == null) return;
    //    cntrl.style.visibility = 'hidden';
    cntrl.style.display = 'none';
    //    var row = document.getElementById("captionRow");
    //    if (row.style.display == '') row.style.display = 'none';
    //    else row.style.display = '';
}


function IsControlHiden(cntrl) {
    if (cntrl == null) return true;
    if (cntrl.style.display == 'none') { return true; }
    //    if (cntrl.style.visibility == 'hidden') { return true; }
    return false;
}


function ShowBinReport() {
    var BinNumber = prompt('Bin Number:', '');
    if (BinNumber == null || BinNumber.length == 0) { return; }

    var xDataList = {};
    xDataList['RPT'] = 'Bin';
    xDataList['Bin'] = BinNumber;
    var pstring = GetParameterStream(xDataList);

    // var WindowToOpen = 'RPT_SpotCountReport.aspx';
    var WindowToOpen = '/Reports/RPT_EXCEL_Out.aspx';

    if (pstring.length > 0) {
        WindowToOpen = WindowToOpen + '?' + pstring
    }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
    ScanFocus();
    return;
}

function ClearLogReport() {
    var service = new WebServer_01();
    service.ClearLogFile();
}

function ShowLogReport() {
    var xDataList = {};
    xDataList['RPT'] = 'Log';
    var pstring = GetParameterStream(xDataList);
    var WindowToOpen = '/Reports/RPT_EXCEL_Out.aspx';
    if (pstring.length > 0) { WindowToOpen = WindowToOpen + '?' + pstring }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
    ScanFocus();
    return;
}


function ShowUnitViewReport() {
    var ReceiveDetailID = MCL('RECEIVEDETAILID').value;
    if (ReceiveDetailID == '' || ReceiveDetailID == -1) {
        alert('Load an IMEI first!');
        ScanFocus();
    }
    else {
        var xDataList = {};
        xDataList['RID'] = ReceiveDetailID;
        xDataList['ESN'] = MCL('ESN').value;
        var pstring = GetParameterStream(xDataList);

        var WindowToOpen = '/Reports/RPT_UnitView.aspx';
        if (pstring.length > 0) {
            WindowToOpen = WindowToOpen + '?' + pstring
        }
        var win = window.open(WindowToOpen, '_blank', 'menubar', true);
        return;
    }
}

function isAuthorizeScreen() {
    ProcessToSetUp = MCL('CurrentProcess').value;
    // we do not want to change the client if it is one of our Receive screens from external
    if (ProcessToSetUp.substr(0, 9).toUpperCase() == 'AUTHORIZE') {
        return true;
    }
    return false;
}


function onUpdateReceiveDetailLogSuccess(result) {
    MCL('btnHistoryRefresh').click();
}




////////////////////////////////////////////////////////////////////////////////////////////////
function IMEIBulk_Count() {
    var lCount = MCL('lblIMEICount');
    var Count = 0;
    var IMEIList = MCL('txtIMEIList');
    IMEIList.value = IMEIList.value.replace(/ /g, '\n');   // space
    IMEIList.value = IMEIList.value.replace(/\t/g, '\n');  // Tab
    IMEIList.value = IMEIList.value.replace(/\r/g, '\n');  // CR
    IMEIList.value = IMEIList.value.replace(/,/g, '\n');  // comma
    var IMEINumbers = IMEIList.value.split('\n');
    var Text = 'Count:' + IMEINumbers.length.toString();

    for (y in IMEINumbers) {
        if (IMEINumbers[y].length > 0) { Count += 1; }
    }
    Text = 'Count:' + (Count).toString();
    lCount.innerHTML = Text;
    return;
}

///////////////////////////////////////////////////////////////////////////
function LTrim(value) {
    var re = /\s*((\S+\s*)*)/;
    return value.replace(re, '$1');
}

// Removes ending whitespaces
function RTrim(value) {
    var re = /((\s*\S+)*)\s*/;
    return value.replace(re, '$1');
}

// Removes leading and ending whitespaces
function trim(value) {
    return LTrim(RTrim(value));
}


function page_Load() {
    var manager = Sys.WebForms.PageRequestManager.getInstance();
    manager.add_endRequest(endRequest);
}

function endRequest(sender, args) {
    moveTop();
}

function moveTop() {
    setTimeout('window.scrollTo(0, 1)', 1);
    //           window.scrollTo(0, 0);
}

////////////////////////////////////////////////
function SetUpScreen(ProcessToSetUp) {
    var ProjectSetup = MCL("ProjSetup").value;
    if (ProcessToSetUp.length == 0) { ProcessToSetUp = MCL("CurrentProcess").value; }
    MCL("AutoSave").style.visibility = "hidden";
    MCL("AutoPrint").style.visibility = "hidden";
    //MCL("RMAROW").style.visibility = "hidden";
    MCL("RMA").style.visibility = "hidden";

    MCL("WaitingForRMAROW").style.visibility = "hidden";

    //MCL("pTagROW").style.visibility = "hidden";
    MCL("pTag").style.visibility = "hidden";


    MCL("WaitingForPTAGROW").style.visibility = "hidden";


    MCL("esn").style.visibility = "hidden";
    MCL("QTY").style.visibility = "hidden";

    MCL("AutoSave").style.visibility = "visible";   // We want Auto Save always on. In case they scan in something with sticky on.


    if (ProcessToSetUp.substr(0, 7).toUpperCase() == 'RECEIVE' ||
               ProcessToSetUp.toUpperCase() == 'RECEIVE' ||
               ProcessToSetUp.toUpperCase() == 'RECEIVEFROMBULK' ||
               ProcessToSetUp.toUpperCase() == 'KITTING' ||
               ProcessToSetUp.toUpperCase() == 'SHIPPING GMP SALES' ||
               ProcessToSetUp.toUpperCase() == 'SHIPPING') {
        MCL("AutoPrint").style.visibility = "visible";
    } else { MCL("AutoPrint").checked = false; }

    if (ProcessToSetUp.toUpperCase() == 'BULKRECEIVE' || ProcessToSetUp.toUpperCase() == 'BULKMOVE') {
        MCL("QTY").style.visibility = "visible";
    } else { MCL("esn").style.visibility = "visible"; }

    if (ProjectSetup.indexOf("ZRMAZ") > -1) { MCL("RMAROW").style.visibility = "visible"; }
    if (ProjectSetup.indexOf("ZRMAZ") > -1) { MCL("RMA").style.visibility = "visible"; }


    if (ProjectSetup.indexOf("ZRMAZ") > -1) { MCL("WaitingForRMAROW").style.visibility = "visible"; }

    //if (ProjectSetup.indexOf("ZPTAGZ") > -1) { MCL("pTagROW").style.visibility = "visible"; }
    if (ProjectSetup.indexOf("ZPTAGZ") > -1) { MCL("pTag").style.visibility = "visible"; }

    if (ProjectSetup.indexOf("ZPTAGZ") > -1) { MCL("WaitingForPTAGROW").style.visibility = "visible"; }
    uppdateStatusPanel("");
    ToggleTarget();
    ScanFocus();
}

function ScanFocus() {
    ShowNextDataToGet();
    MCL('ScanKey').focus();
    // moveTop();
    return;
}

function ShowNextDataToGet() {
    var ClientLocationID = MCL('CLIENTLOCATIONID').value;
    var clientname = MCL("ClientName").value;
    if (clientname == "Client Name") { clientname = ""; }
    var QTY = MCL('QTY').value;
    if (QTY == "Quantity") { QTY = ""; }
    var PROJTAG = MCL('Ptag').value;
    if (PROJTAG == "Project Tag") { PROJTAG = ""; }
    var RMA = MCL('RMA').value;
    if (RMA == "RMA Number") { RMA = ""; }
    if (RMA == "Work Order Number") { RMA = ""; }

    var ESN = MCL('ESN').value;
    if (ESN == "ESN/IMEI Number") { ESN = ""; }

    // Set as there. Neutral
    MCL("WAITINGFORRMACHECK").style.visibility = "hidden";
    MCL("WAITINGFORRMAX").style.visibility = "hidden";

    MCL("WAITINGFORPTAGCHECK").style.visibility = "hidden";
    MCL("WAITINGFORPTAGX").style.visibility = "hidden";

    MCL("WAITINGFORIMEICHECK").style.visibility = "hidden";
    MCL("WAITINGFORIMEIX").style.visibility = "hidden";

    MCL("WAITINGFORCLIENTCHECK").style.visibility = "hidden";
    MCL("WAITINGFORCLIENTX").style.visibility = "hidden";

    if (isReceiveScreen() == false) { return; }
    var ProjectSetup = MCL("ProjSetup").value;

    if (ProjectSetup.indexOf("ZRMAZZEDITZ") > -1) {
        MCL("WAITINGFORRMAX").style.visibility = "visible";
        if (RMA.length > 0) {
            MCL("WAITINGFORRMACHECK").style.visibility = "visible";
            MCL("WAITINGFORRMAX").style.visibility = "hidden";
        }
    }

    if (ProjectSetup.indexOf("ZPTAGZZEDITZ") > -1) {
        MCL("WAITINGFORPTAGX").style.visibility = "visible";
        if (PROJTAG.length > 0) {
            MCL("WAITINGFORPTAGCHECK").style.visibility = "visible";
            MCL("WAITINGFORPTAGX").style.visibility = "hidden";
        }
    }

    MCL("WAITINGFORIMEIX").style.visibility = "visible";
    if (ESN.length > 0) {
        MCL("WAITINGFORIMEICHECK").style.visibility = "visible";
        MCL("WAITINGFORIMEIX").style.visibility = "hidden";
    }

    MCL("WAITINGFORCLIENTX").style.visibility = "visible";
    if (clientname.length > 0) {
        MCL("WAITINGFORCLIENTCHECK").style.visibility = "visible";
        MCL("WAITINGFORCLIENTX").style.visibility = "hidden";
    }
}

function isReceiveScreen() {
    ProcessToSetUp = MCL('CurrentProcess').value;
    // we do not want to change the client if it is one of our Receive screens from external
    if (ProcessToSetUp.substr(0, 7).toUpperCase() == 'RECEIVE') {
        return true;
    }
    return false;
}


// ******************************************************************

function uppdateStatusPanelError(message) {
    uppdateStatusPanelDo(message);
    //jmbeep(1);
    //    MCL('StatusPanel').style.background = '#FFCCFF'
    //    MCL('Display_MSG').style.background = '#FFCCFF'
    //    MCL('StatusPanel_B').style.background = '#FFCCFF'
    //    MCL('Display_MSG_B').style.background = '#FFCCFF'
}

function uppdateStatusPanelYellow(message) {
    uppdateStatusPanelDo(message);
    //jmbeep(1);
    //    MCL('StatusPanel').style.background = '#FFFFCC'
    //    MCL('Display_MSG').style.background = '#FFFFCC'
    //    MCL('StatusPanel_B').style.background = '#FFFFCC'
    //    MCL('Display_MSG_B').style.background = '#FFFFCC'
}

function uppdateStatusPanelWarn(message) {
    uppdateStatusPanelDo(message);
    //jmbeep(1);
    //    MCL('StatusPanel').style.background = '#FFFFCC'
    //    MCL('Display_MSG').style.background = '#FFFFCC'
    //    MCL('StatusPanel_B').style.background = '#FFFFCC'
    //    MCL('Display_MSG_B').style.background = '#FFFFCC'
}
function uppdateStatusPanel(message) {
    uppdateStatusPanelDo(message);
    //jmbeep(1);
    //    MCL('StatusPanel').style.background = '#FFFFCC'
    //    MCL('Display_MSG').style.background = '#FFFFCC'
    //    MCL('StatusPanel_B').style.background = '#FFFFCC'
    //    MCL('Display_MSG_B').style.background = '#FFFFCC'
}


function uppdateStatusPanelDo(message) {
    //alert('updatestatus1');
    //alert('updatestatus2');


    var ESN = MCL('ESN').value;
    var ESNVersion = MCL('ESNVERSION').value;
    var LastESN = MCL("LastESN").value;
    var LastESNVersion = MCL("LASTESNVERSION").value;

    if (ESN == "ESN/IMEI Number") { ESN = ""; ESNVersion = ""; }

    ProjectName = "";
    var IndexValue = MCL('drpProjectList').selectedIndex;
    if (IndexValue > -1) {
        var ProjectName = MCL('drpProjectList').options[IndexValue].text;
    }
    var Process = MCL('CurrentProcess').value;
    var mText = "";

    // Update the Misc panel
    mText = AddDelimiter(ProjectName, ":");
    mText += AddDelimiter(Process, ":");
    //MCL('StatusPanel').style.background = "#CCFFFF";
    MCL('StatusPanel').value = mText;
    //MCL('StatusPanel_B').style.background = "#CCFFFF";
    MCL('StatusPanel_B').value = mText;
    // Update the Message panel
    mText = message;
    //MCL('Display_MSG').style.background = "#CCFFFF";
    MCL('Display_MSG').value = mText;
    //MCL('Display_MSG_B').style.background = "#CCFFFF";
    MCL('Display_MSG_B').value = mText;
    // update the ESN panel
    if (ESN.length != 0) { mText = ESN + ":" + ESNVersion; /*MCL('Display_ESN').style.background = "#99FFCC"; MCL('Display_ESN_B').style.background = "#99FFCC";*/ }
    if (ESN.length == 0) { mText = LastESN + ":" + LastESNVersion; /*MCL('Display_ESN').style.background = "#CCFFFF"; MCL('Display_ESN_B').style.background = "#CCFFFF";*/ }
    MCL('Display_ESN').value = MCL("KEEPUNITACTIVE").value + " " + mText;
    MCL('Display_ESN_B').value = MCL("KEEPUNITACTIVE").value + " " + mText;
}

function AddDelimiter(mText, dText) {
    if (mText.length > 0) { mText = mText + dText; }
    return mText;
}

function ToggleTarget() {
    if (MCL('CurrentProcess').value.toUpperCase() != 'BULKMOVE') {
        SetHomeTarget();
        return;
    }
    var AreaToInput = MCL('hdnSourceOrTarget');
    var Targetlbl = MCL('lblTargetEDT');
    if (AreaToInput.value == 'Target') {
        AreaToInput.value = 'Source';
        MCL('lblActiveProcessEDT').innerHTML = '*';
        if (Targetlbl != null) { Targetlbl.innerHTML = ''; }
    }
    else {
        AreaToInput.value = 'Target';
        MCL('lblActiveProcessEDT').innerHTML = '';
        if (Targetlbl != null) { Targetlbl.innerHTML = '*'; }
    }
}

function SetHomeTarget() {
    var Targetlbl = MCL('lblTargetEDT');
    var SourceOrTarget = MCL('hdnSourceOrTarget');
    var ActiveProcessEDT = MCL('lblActiveProcessEDT');

    if (SourceOrTarget != null) { SourceOrTarget.value = 'Source'; }
    if (ActiveProcessEDT != null) { ActiveProcessEDT.innerHTML = '*'; }
    if (Targetlbl != null) { Targetlbl.innerHTML = ''; }
}

function SwitchIMEI() {
    if (MCL("RECEIVEDETAILID").value.length == 0) {
        alert("You must load the ESN to be swapped out first.");
        return;
    }

    //MCL('wndSwitchIMEI').Open();
    $('#wndSwitchIMEI').modal('show');
}
function SwitchIMEIOK() {
    var NewIMEI = MCL('txtNewIMEI').value;
    if (NewIMEI.length == 0) {
        alert("You must first enter an ESN to swap with the current unit.");
        return;
    }

    var answer = confirm('Are you sure you want to switch these IMEIs?')
    if (!answer) {
        alert('Switch Canceled!');

        //MCL('wndSwitchIMEI').Close();
        $('#wndSwitchIMEI').modal('hide');

        return;
    }

    var ReceiveDetailID = MCL("RECEIVEDETAILID").value;
    var service = new WebServer_01();
    service.SwitchIMEI(ReceiveDetailID, NewIMEI, MCL("UserName").value, onSwitchIMEISuccess, onSwitchIMEIError, null);

    //MCL('wndSwitchIMEI').Close();
    $('#wndSwitchIMEI').modal('hide');
}
function SwitchIMEICancel() {
    //MCL('wndSwitchIMEI').Close();
    $('#wndSwitchIMEI').modal('hide');
}

function onSwitchIMEIError(Result) {
    alert("IMEI Swap Error:" + Result.get_message());
    //MCL('wndSwitchIMEI').Close();
    $('#wndSwitchIMEI').modal('hide');
}

function onSwitchIMEISuccess(result) {
    var B = result.split(":")
    if (B[0] == "Error") {
        alert(B[2]);
        uppdateStatusPanelError("Swap Error:" + B[2]);
        return;
    }
    alert("IMEI has been Swapped");
    LoadSheetDataDetail(B[1]);
    uppdateStatusPanel("IMEI Swapped:" + B[2]);
    return;
}


//       //******************************************************************
function selx(ID) {
//    MCL('wndSelectClientLocation').Close();
    $('#wndSelectClientLocation').modal('hide');
    LoadClientLocation(ID);
}
/////////////////////////////////////////////////////////

function OpenAuthorization() {
    if (MCL("RECEIVEDETAILID").value < 1) { alert("No IMEI loaded"); return; }
    //MCL('wndAuthorize').Open(null, null);
    $('#wndAuthorize').modal('show');
}

function Authorization_Cancel() {
    //MCL('wndAuthorize').Close();
    $('#wndAuthorize').modal('hide');
}
function Authorization_Save() {
    //MCL('wndAuthorize').Close();
    $('#wndAuthorize').modal('hide');
}


function OpenWindowCtrl(B) {
    var IndexValue = MCL('drpProjectList').selectedIndex;
    var project = MCL('drpProjectList').options[IndexValue].text.toUpperCase();

    if (project == 'CLIENT PORTAL') {             // Need to do this only if Approval required.
        uppdateStatusPanelError('Unit Already on file.')
        //MCL('wndESNFoundClient').Open(null, null);
        $('#wndESNFoundClient').modal('show');
        return;
    }
    MCL('LBLESNFOUNDTEXT').innerHTML = 'ESN/IMEI Already on file!';
    MCL('btnAlreadyFound').value = 'Record Exists! Refer to Traking and Inspection?';

    MCL('hdnESNID').value = B[1];
    MCL('hdnESNNumber').value = B[3];
    MCL('ClientTransfer').style.visibility = "hidden";
    MCL('TransferToMSC').style.visibility = "hidden";
    MCL('btnKittingDefective').style.visibility = "hidden";
    //MCL('TranserFromMSC').style.visibility = "hidden";

    //MCL('wndESNFound').Title = B[3] + " already on file. (" + B[5] + ')';
    $('#wndESNFound .modal-title').text(B[3] + ' already on file. (' + B[5] + ')');

    if (B[5] == "1" && project == 'CLIENT REPAIR') {           // we are only interested in allowing this if we are in the proper project
        MCL('ClientTransfer').style.visibility = "visible";
    }
    if (B[5] == "2") {
        MCL('TransferToMSC').style.visibility = "visible";
    }

    if ((MCL('CurrentProcess').value.substr(0, 11).toUpperCase() == 'RMA RECEIVE') && B[5] != "3") {
        //        alert('Found Inside the edit');
        if (B[5] == "4") {
            MCL('LBLESNFOUNDTEXT').innerHTML = 'Record Exists!\nThis is  not an RMA device.\n';
            MCL('btnAlreadyFound').value = 'ESN/IMEI Not an RMA device!';
        }
        else {
            MCL('LBLESNFOUNDTEXT').innerHTML = 'Record Exists!\n Unit has not been through QC Assessment.\n';
            MCL('btnAlreadyFound').value = 'ESN/IMEI must be opened in QC Assessment First!';
        }
    }

//    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//    // Jody wants to stop the user from entering these two processes if the unit has not already been
//    // in these these processes or already gone through "Tech Receive",'GMP REPAIR','LAB BILLING','LAB BILLING','HOLD STATUS'
//    // B[5] is deduced in SQL.SP.ProcessScanCode
//    // TURNED OFF FOR TESTING......... TURN ON BEFORE DEPLOYING
//    // JIM
//    if ((MCL('CurrentProcess').value.substr(0, 10).toUpperCase() == 'GMP REPAIR' ||
//         MCL('CurrentProcess').value.substr(0, 11).toUpperCase() == 'LAB BILLING' ||
//         MCL('CurrentProcess').value.substr(0, 11).toUpperCase() == 'HOLD STATUS') && B[5] != "3") {
////        alert('Found Inside the edit');
//        MCL('LBLESNFOUNDTEXT').innerHTML = 'Record Exists!\n Unit has not been received into Lab.\nPlease receive using Tech Receive Module.';
//        MCL('btnAlreadyFound').value = 'ESN/IMEI must be opened in Tech Receive First!';
//    }
//    if ((MCL('CurrentProcess').value.substr(0, 13).toUpperCase() == 'REQUEST PARTS') && B[5] != "3") {
//        //        alert('Found Inside the edit');
//        MCL('LBLESNFOUNDTEXT').innerHTML = 'Record Exists!\n Parts cannot be requested on a unit that hasn’t been Tech Received.\nPlease receive using Tech Receive Module.';
//        MCL('btnAlreadyFound').value = 'ESN/IMEI must be opened in Tech Receive First!';
//    }
//    ///////////////////////////////////////////////////////////////////////////////////////////////
//    // If the unit is opened in "Tech Receive", it must have first gone through "Lab Receive"
//    // B[5] tells me if it went through "Lab Receive"  "3" = yes it has.
//    // B[5] is deduced in SQL.SP.ProcessScanCode
//    // onSuccess_LoadReceiveDetail(B)
//    if (MCL('CurrentProcess').value.substr(0, 12).toUpperCase() == 'TECH RECEIVE' && B[5] != "3") {
//        MCL('LBLESNFOUNDTEXT').innerHTML = 'Record Exists!\n Unit has not been received into Lab Receive.\nPlease receive using Lab Receive Module.';
//        MCL('btnAlreadyFound').value = 'ESN/IMEI must be opened in Lab Receive First!';
//    }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // If the Current process = Kitting, we want to stop any "Defective" units from being kitted.
    // B[5] is deduced in SQL.SP.ProcessScanCode
    if ((MCL('CurrentProcess').value.substr(0, 7).toUpperCase() == 'KITTING' || MCL('CurrentProcess').value.substr(0, 18).toUpperCase() == 'SHIPPING GMP SALES') && B[5] == "4") {
        MCL('btnAlreadyFound').style.visibility = "hidden";
        MCL('btnKittingDefective').style.visibility = "visible";
        MCL('LBLESNFOUNDTEXT').innerHTML = 'Unit set as Defective.\nDefective units can’t be kitted.';
        MCL('btnKittingDefective').value = 'OK';
    }

    if (MCL('CurrentProcess').value.substr(0, 18).toUpperCase() == 'SHIPPING GMP SALES') { OpenFinishProductLabel(); return; }



    //MCL('wndESNFound').Open(null, null);
    $('#wndESNFound').modal('show');
}

function OpenClientSearch() {
    //MCL('wndSelectClientLocation').Title = "Client Search";
    //MCL('wndSelectClientLocation').Open(null, null);

    $('#wndSelectClientLocation .modal-title').text('Client Search');
    $('#wndSelectClientLocation').modal('show');
}




function onSuccessOpenEmailWindow(result) {
    //alert("OpenEmailWindow:" + result);
    //MCL('wndSendEmailWindow').Title = "Email";

    $('#wndSendEmailWindow .modal-title').text('Email');

    var ID = MCL('EmailSend');
    //alert("Starting GetEmailHTML:" );

    ID.innerHTML = GetEmailHTML(ID, result);
    //alert("BackFromGetEmail:" + result);

    //MCL('wndSendEmailWindow').Open(null, null);
    $('#wndSendEmailWindow').modal('show');
}

function AlreadyFound_AddNew_OK() {
    var ESNNumber = MCL('hdnESNNumber').value;
    var ID = MCL('hdnESNID').value;

    //MCL('wndESNFound').Close();
    $('#wndESNFound').modal('hide');

    var service = new WebServer_01();
    service.AdvanceESNVersion(ESNNumber, MCL("UserName").value, onESNAdvanceSuccess, null, null);
    LoadScanNumber(ESNNumber, false);
    alert("All existing data will be archived");
    MCL("hdnAllowDupAdd").value = "Y";
}

function AlreadyFound_TransferIN_OK() {
    var ESNNumber = MCL('hdnESNNumber').value;
    var ID = MCL('hdnESNID').value;

    var IndexValue = MCL('drpProjectList').selectedIndex;

    var ProjectID = -1;
    if (IndexValue > -1) { ProjectID = MCL('drpProjectList').options[IndexValue].value; }
    var ProcessID = MCL("CurrentProcessID").value;
    var ClientLocationID = MCL("CLIENTLOCATIONID").value;
    if (MCL("CLIENTLOCATIONID").value.length == 0) { ClientLocationID = -1; }
    uppdateStatusPanelYellow("ESN Transfer In");

    //MCL('wndESNFound').Close();
    $('#wndESNFound').modal('hide');

    var service = new WebServer_01();
    service.DealerSubmissionESN(ESNNumber, ID, ProjectID, ProcessID, ClientLocationID, MCL("UserName").value, onESNTransferSuccess, onESNTransferError, null);
}

function onESNTransferError(Result) {
    alert("Error:" + Result);
}

function onESNTransferSuccess(ID) {
    LoadSheetDataDetail(ID);
}

function onESNAdvanceSuccess(result) {
    //alert("onESNAdvanceSuccess");
}

function AlreadyFound_AddNew_Cancel() {
    //MCL('wndESNFound').Close();
    $('#wndESNFound').modal('hide');
    uppdateStatusPanelError("Refer to Tracking and Inspection")
}


function KittingDefectiveCancel() {
    //MCL('wndESNFound').Close();
    $('#wndESNFound').modal('hide');
    uppdateStatusPanelError("Unit set as Defective. Defective units can not be kitted.")
}





///////////////////////////////////////
function AlreadyFound_TransferToMSC_OK() {
    var ESNNumber = MCL('hdnESNNumber').value;
    var ID = MCL('hdnESNID').value;

    var IndexValue = MCL('drpProjectList').selectedIndex;

    var ProjectID = -1;
    if (IndexValue > -1) { ProjectID = MCL('drpProjectList').options[IndexValue].value; }
    var ProcessID = MCL("CurrentProcessID").value;
    var ClientLocationID = MCL("CLIENTLOCATIONID").value;
    if (MCL("CLIENTLOCATIONID").value.length == 0) { ClientLocationID = -1; }
    uppdateStatusPanelYellow("ESN MSN Transfer In");

    //MCL('wndESNFound').Close();
    $('#wndESNFound').modal('hide');

    var service = new WebServer_01();
    service.TransferInToMSC(ESNNumber, ID, ProjectID, ProcessID, ClientLocationID, MCL("UserName").value, onTransferToMSCSuccess, onESNTransferError, null);
}


function onTransferToMSCSuccess(ID) {
    LoadSheetDataDetail(ID);
}

/////////////////////////////////////////
//function AlreadyFound_TransferFromMSC_OK() {
//    var ESNNumber = MCL('hdnESNNumber').value;
//    var ID = MCL('hdnESNID').value;

//    var IndexValue = MCL('drpProjectList').selectedIndex;

//    var ProjectID = -1;
//    if (IndexValue > -1) { ProjectID = MCL('drpProjectList').options[IndexValue].value; }
//    var ProcessID = MCL("CurrentProcessID").value;
//    var ClientLocationID = MCL("CLIENTLOCATIONID").value;
//    if (MCL("CLIENTLOCATIONID").value.length == 0) { ClientLocationID = -1; }
//    uppdateStatusPanelYellow("ESN MSN Transfer In");
//    //MCL('wndESNFound').Close();
//    $('#wndESNFound').modal('hide');
//    var service = new WebServer_01();
//    service.TransferInFromMSC(ESNNumber, ID, ProjectID, ProcessID, ClientLocationID, MCL("UserName").value, onTransferFromMSCSuccess, onESNTransferError, null);
//}


function onTransferFromMSCSuccess(ID) {
    LoadSheetDataDetail(ID);
}


/////////////////////////////////////////




///////////////////////
function OpenSelectRepairReport() {
    //    MCL('hdnSelectedLogID').value = B;

    //MCL('WNDSELECTREPAIRREPORT').Title = "Select repair Report";
    //MCL('WNDSELECTREPAIRREPORT').Open(null, null);

    $('#wndSelectRepairReport .modal-title').text('Select Repair Report');
    $('#wndSelectRepairReport').modal('show');
}

function CloseSelectRepairReport() {
    //MCL('WNDSELECTREPAIRREPORT').Close();

    $('#wndSelectRepairReport').modal('hide');
}

///////////////////////
function OpenSelectProcessWindowCtrl(B) {
    MCL('hdnSelectedLogID').value = B;

    //MCL('wndSelectProcess').Title = "Change Process.";
    //MCL('wndSelectProcess').Open(null, null);

    $('#wndSelectProcess .modal-title').text('Change Process');
    $('#wndSelectProces').modal('show');
}

function SelectProcess_Cancel() {
    var LogID = MCL('hdnSelectedLogID').value;
    //MCL('wndSelectProcess').Close();
    $('#wndSelectProces').modal('hide');
}

function SelectProcess_OK() {

    var UserName = MCL("UserName").value;
    var LogID = MCL('hdnSelectedLogID').value;
    //MCL('wndSelectProcess').Close();
    $('#wndSelectProces').modal('hide');


    var IndexValue = MCL('drpProcessList').selectedIndex;
    var xText = MCL('drpProcessList').options[IndexValue].text;
    var xValue = MCL('drpProcessList').options[IndexValue].value;
    var service = new WebServer_01();
    service.UpdateReceiveDetailLog_Process(LogID, xValue, xText, UserName, onUpdateReceiveDetailLogSuccess, null, null);
    return;
}

/////////////////////// JIM Start ServiceCleanup Here.
function OpenIMEIBulkWindowCtrl() {
    //MCL('wndIMEIBulk').Title = "Bulk IMEI Process.";
    //MCL('wndIMEIBulk').Open(null, null);

    $('#wndIMEIBulk .modal-title').text('Bulk IMEI Process');
    $('#wndIMEIBulk').modal('show');
}

function IMEIBulk_Cancel() {
    //MCL('wndIMEIBulk').Close();
    $('#wndIMEIBulk').modal('hide');
}

function onIMEIError(exception) {
    if (exception.get_timedOut()) {
        //MCL('wndIMEIBulk').Close();
        $('#wndIMEIBulk').modal('hide');
        uppdateStatusPanel("IMEIBulk Loaded - onIMEIERROR_TO");
        //               alert("onIMEIERROR_TO");
        //Timeout
    }
    else {
        // alert("onIMEIERROR_OT");
        //MCL('wndIMEIBulk').Close();
        $('#wndIMEIBulk').modal('hide');
        uppdateStatusPanel("IMEIBulk Loaded - onIMEIERROR_OT");
        //Exception occurred
    }
}


function onIMEISuccess(result) {
    MCL("txtIMEIList").value = result;
    MCL("LBLIMEISTATUS").innerHTML = "";

    //MCL('wndIMEIBulk').Close();
    $('#wndIMEIBulk').modal('hide');

    uppdateStatusPanel("IMEIBulk Loaded");
}





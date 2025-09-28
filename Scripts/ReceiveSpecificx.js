
// SCANKEY PROCESSING GOES HERE ----------------------------------------<
function RecordScanKey(pText) {
    var newText = MCL('ScanKey').value;
    if (pText != null) { newText = pText; }
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

        if (newText.toUpperCase() == 'DOSAVE') { DoSave('RecordScanKey'); return; }  // shortcut to the Save Button.
        if (newText.toUpperCase() == '**') { DoSave('RecordScanKey'); return; }  // shortcut to the Save Button.
        if (newText.toUpperCase() == 'DOCLEAR') { ClearData(); return; }  // shortcut to the Save Button.
        if (newText == '--') { ClearData(); return; }  // shortcut to the Save Button.
        if (newText == '++') { GenerateBagTag(); return; }  // shortcut to the Save Button.
        if (newText.toUpperCase() == 'BAGTAG') { GenerateBagTag(); return; }
        if (newText.toUpperCase() == '//') { ToggleTarget(); return; }

        if (newText.substr(0, 4).toUpperCase() == 'XPTX') { ProjectTagUpdate(newText.substr(4)); return; }
        if (newText.substr(0, 5).toUpperCase() == 'XRMAX') { RMANumberUpdate(newText.substr(5)); return; }
        if (newText.substr(0, 5).toUpperCase() == 'XAUTHX') { SetupToAuthorize(newText.substr(5)); return; }
        if (newText.substr(0, 5).toUpperCase() == 'XBINX') { BinBulkProcess(newText.substr(5)); return; }
        if (newText.substr(0, 5).toUpperCase() == 'XLOCX') { LocBulkProcess(newText.substr(5)); return; }
        if (newText.toUpperCase() == 'IMEIBULK') { OpenIMEIBulkWindowCtrl(); return; }
        if (newText.indexOf(':') > -1) { LoadThisESNVersion(newText); return; }

        MCL('ScanKeyHistory').value = newText;
        uppdateStatusPanelYellow('Processing...');

        var service = new WebServer_01();
        service.ScanCodeParse(MCL('ClientLocationID').value, 'XXX', 'Receive', newText, MCL('UserName').value, MCL('StepUp').value, onSuccess, null, null);
    }
}


function onSuccess(result) {
    uppdateStatusPanelYellow('Processing...1');
    var B = result.split(':')
    uppdateStatusPanelYellow('Processing...:' + B[0]);
    if (B[0].toUpperCase() == 'MACROCHAIN') { LoadMacroChain(result.substr(11)); return; }
    if (B[0].toUpperCase() == 'CLIENTLOCATION') { onSuccess_LoadClientLocation(B); return; }
    if (B[0].toUpperCase() == 'RECEIVEDETAIL') { onSuccess_LoadReceiveDetail(B); return; }
    if (B[2].toUpperCase() == 'UNKNOWN SCANCODE') { LoadScanNumber(B[3]); return; }
    if (B[2].toUpperCase() == 'UNKNOWN MACROKEY') { LoadScanNumber(B[3]); return; }
    UpdateFormScanData(B);
    return;
}

function onSuccess_LoadReceiveDetail(B) {
    var ContinueLoad = true;
    var BumpVersionTo900 = false;
    if (MCL('CurrentProcess').value.substr(0, 7).toUpperCase() == 'RECEIVE') {
        if (MCL('SProjectOverride').value != 'Y') {
            // open the window to tell them they can not open the IMEI, or if it needs to be transfered.
            uppdateStatusPanelError('IMEI found')
            OpenWindowCtrl(B);
            return;
        }
        // If isSecondaryProjectOverride then we want to bump the 000 record and move forward as normal
        BumpVersionTo900 = true;
    }
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
    var OutputHTML = '';
    var HeaderText = '<tr><td>Select</td> <td>ID</td><td>Client</td><td>Location Name</td><td>Location</td></tr>';
    var BodyText = '';

    //           ClientData = eval('({' + Result + '})');
    ClientData = eval('[' + Result + ']');               // Square brackets to denote an array of elements.

    for (var i = 0; i < ClientData.length; i++) {
        BodyText = BodyText + '<tr><td>'
                               + "<button id='btn' name='btn' onClick='selx("
                               + ClientData[i].ClientLocationID
                               + '); return false;>Select</button>'
                               + '</td> <td>'
                               + ClientData[i].ClientLocationID
                               + '</td> <td>'
                               + ClientData[i].txtClientName
                               + '</td>   <td>'
                               + ClientData[i].txtLocationName
                               + '</td>   <td>'
                               + ClientData[i].txtStoreNumber + ' ' + ClientData[i].txtStoreSuffix + ' ' + ClientData[i].txtClientAddress
                               + '</td></tr>';
    }
    OutputHTML = "<table id='XX'>" + HeaderText + BodyText + '</table>'
    var SearchResults = MCL('pnlSearchResult');
    SearchResults.innerHTML = OutputHTML;
    ScanFocus();
}



function LoadThisESNVersion(versionToLoad) {
    var service = new WebServer_01();
    service.GetThisESNVersionRecordID(versionToLoad, onGetThisESNVersionRecordID, null, null);
}

function onGetThisESNVersionRecordID(result) {
    result = '({' + result + '})';
    var resultList = eval(result);
    if (resultList.ReceiveDetailID != '-1') {
        LoadSheetDataDetail(resultList.ReceiveDetailID);
    }
    else {
        uppdateStatusPanel(resultList.VersionToLoad + ' Not Found')
    }
}
/////////////////////////////////////////////////////////////////////////////////

function OpenPartList() {
    var service = new WebServer_01();

    var ClientID = MCL('hdnClientIDx').value;
    var CarrierID = MCL('hdnCarrierIDx').value;
    var ManufacturerID = MCL('hdnManufacturerIDx').value;
    var ModelID = MCL('hdnModelIDx').value;
    var UserName = MCL('UserName').value;
    var rValue = service.GetPartNumberListData(UserName, ClientID, CarrierID, ManufacturerID, ModelID, onPartNumberListSuccess, onWebServerError);
}

function onWebServerError(Result) {
    alert('Error:' + Result);
}


function PlacePartNumber(PartNumber) {
    var PNumbers = MCL('PARTNUMBERIDS').value;
    var inputArea = GetInputArea();
    // We need to deal with other types.
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
                var currentValue = cBox.getAttribute('someValue') + ',';
                Value = cBox.value;

                if (PNumbers.indexOf(currentValue) > -1) {
                    if (cBox.value.length < 1) {
                        cBox.value = PartNumber;
                        break;
                    }
                }
            }
        }
    }

}


function OpenEmailWindow() {
    var service = new WebServer_01();
    service.GetESNEmail01_Message(MCL('RECEIVEDETAILID').value, MCL('CurrentProcess').value, onSuccessOpenEmailWindow, null, null);
}


function GetEmailHTML(ID, message) {
    var OutputHTML = '';
    var emailTo = MCL('CLIENTLocationEMAIL').value;
    var Subject = 'GMP Repair Notification';
    var Body = 'IMEI ' + MCL('LastESN').value + ', ' + message;

    if (MCL('CurrentProcess').value.toUpperCase() == 'GMP REPAIR') {
        Subject = 'Authorization Required';
        Body = MCL('LastESN').value + '.%0A' + message;
    }

    OutputHTML = "<a href='mailto:'" + emailTo;
    OutputHTML += '?subject=' + Subject;
    OutputHTML += '&body=' + Body;
    OutputHTML += '>Create email to send</a>';
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
        uppdateStatusPanel('Can not load Client' + LoadClientLocation(B[1]));
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
        var answer = confirm('Data not Saved!\nContinue without saving?')
        if (answer == false) {
            uppdateStatusPanelError('Data not Saved! - Save Data first');
            //                    alert('Exiting to allow Save');
            return;
        }
        uppdateStatusPanelError('Data not Saved!\nContinued without saving!');
        alert('Data not saved.')
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
    }
    var Keys = AuthorizationNumber.split(':');
    MCL('DOAUTHORIZE').value = keys[0];
    LoadSheetDataDetail(keys[1])
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
////////////////////////

function BinBulkProcess(BinNumber) {
    if (isReceiveScreen() == true) {
        alert('Unable to run this command from this process screen');
        return;
    }

    var answer = confirm('Are you sure you want to run the XBINX command?\nDoing so will update all ESN numbers in this bin.')
    if (!answer) { alert('XBINX Canceled!'); return; }

    uppdateStatusPanelYellow('Processing XBIBX ...');
    var ds = GetDataStream(true);
    var service = new WebServer_01();
    var rValue = service.BinBulkProcess(BinNumber, ds, onBinBulkProcessSuccess, onBinBulkProcessError);
    uppdateStatusPanelYellow('Saving Bin data!');
}

function onBinBulkProcessError(result) {
    uppdateStatusPanelError('Error:' + result);
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
    var answer = confirm('Are you sure you want to run the XLOCX command?\nDoing so will update all ESN numbers in this bin.')
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



function DoSave(CalledFrom) {
    //       LoadMCL();
    //           var CurrProcess = MCL('CurrentProcess').value;
    if (MCL('CurrentProcess').value.toUpperCase() == 'SEARCH') {
        //alert('HERE IN DO SAVE SEARCH');
        OKToNextStep('SEARCH', '-1', CalledFrom);
        return;
    }
    MCL('btnSave').click();
    return;
}

function OKToNextStep(btnName, IDField, CalledFrom) {
    if (OKToSaveEdits() == false) {
        ScanFocus();
        return false;
    }
    ResetFields(btnName, IDField);
    MCL('CalledFrom').value = CalledFrom;
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
    return false;
}

function OKToSaveEdits() {
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
    return ValidateEntryError();
}

function ValidateEntryError() {
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
        for (y in isManditoryList) {
            var dta = isManditoryList[y].split(':');
            if (dta[0].length > 0) {
                var eID = $get(dta[0]);
                if (eID != null) { eID.style.color = 'red'; }
            }
        }
        alert('There are manditory elements not entered');
        uppdateStatusPanelError('There are manditory elements not entered');
        return false;
    }
    return true;
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

function AddData() {
    SaveSticky();
    var ds = GetDataStream(true);
    var service = new WebServer_01();
    var rValue = service.AddDataDetail(ds, onAddSaveSuccess);
    uppdateStatusPanelYellow('Saving data!');
}

function onAddSaveSuccess(result) {
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
        UpdateProcessCheckList(resultList.CompProcList);
        uppdateStatusPanel('Data Saved!');
        RecordHistory(MCL('LastESN').value);

        if (ProcessToSetUp.toUpperCase() == 'COMMUNICATION') {
            OpenEmailWindow();
        }
        if (ProcessToSetUp.toUpperCase() == 'GMP REPAIR' && resultList.AR == 'Y') {             // Need to do this only if Approval required.
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
        if (MCL('AutoPrint').checked == true || IsNumeric(MCL('hdnAllowProjectPassThrough').value) == true) {
            GenerateBagTag();
        }
    }

    ScanFocus();
}


function GenerateBagTag() {
    if (MCL('CurrentProcess').value.toUpperCase() == 'BULKRECEIVE' || MCL('CurrentProcess').value.toUpperCase() == 'BULKMOVE') {
        return;
    }
    if (MCL('ESN').value.length == 0 && MCL('LastESN').value.length == 0) {
        alert('You need to set a ESN Number and advance first');
        ScanFocus();
        return;
    }
    if (IsNumeric(MCL('ClientLocationID').value) == false) {
        alert('You must enter a Client first!');
        ScanFocus();
        return;
    }

    if (IsNumeric(MCL('hdnAllowProjectPassThrough').value) == true) {
        OpenClientbagTag()
        return;
    }

    if (MCL('CurrentProcess').value.toUpperCase() == 'KITTING') {
        OpenFinishProductLabel();
        return;
    }
    if (MCL('CurrentProcess').value.toUpperCase() == 'GMP REPAIR') {
        OpenRepairForm();
        return;
    }

    OpenbagTag();
}

function OpenRepairForm() {

    var xDataList = {};
    xDataList['A'] = MCL('RECEIVEDETAILID').value;
    xDataList['B'] = '';
    var pstring = GetParameterStream(xDataList);
    var WindowToOpen = 'RPT_RepairForm.aspx';
    if (pstring.length > 0) {
        WindowToOpen = WindowToOpen + '?' + pstring
    }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
    // win.focus();
}

function OpenFinishProductLabel() {
    var pstring = GetParameterStream(GetReportParameterList('PRODUCTLABEL'));
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

    if (pstring.length > 0) {
        WindowToOpen = WindowToOpen + '?' + pstring
    }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
    ScanFocus();
    return;
}


function OpenbagTag() {
    var report = 'Bagtag';
    //           if (IsNumeric(MCL('hdnAllowProjectPassThrough').value) == false) {
    //               report = 'Bagtag';
    //           }
    var pstring = GetParameterStream(GetReportParameterList(report));
    var WindowToOpen = 'BagTag.aspx';
    if (pstring.length > 0) {
        WindowToOpen = WindowToOpen + '?' + pstring
    }
    var win = window.open(WindowToOpen, '_blank', 'menubar', true);
    // win.focus();
}




function LoadMacroChain(result) {
    var Bs = result.split(';')
    for (var i = 0; i < Bs.length; i++) {
        var B = Bs[i].split(':')
        UpdateFormScanData(B);
    }
    uppdateStatusPanel('Macro Chain Loaded')
    ScanFocus();
}

function LoadPreReceiveDetail(ScanNumber) {
    var service = new WebServer_01();
    service.LoadPreReceiveDetail(ScanNumber, MCL('UserName').value, OnLoadPreReceiveDetailSuccess, null, null);

}

function OnLoadPreReceiveDetailSuccess(result) {
    var Data = eval('[' + result + ']');

    if (Data[0].Status == 'No Action') { return; }

    if (Data[0].RMA.length > 0) { MCL('RMA').value = Data[0].RMA; }
    if (Data[0].ProjectTag.length > 0) { MCL('Ptag').value = Data[0].ProjectTag; }
    if (Data[0].Detail.length > 0) {
        var option = Data[0].Detail.split(';');
        for (n in option) {
            if (option[n].length > 0) {
                var B = option[n].split('|');
                //                       alert(option[n] + '  B[1]=' + B[1] + ' B[6]=' + B[6]);
                UpdateFormScanData(B);
            }
        }

        // Update the detail data
    }
    ScanFocus();
}

function UpdateFormScanData(B) {
    var AreaToInput = MCL('hdnSourceOrTarget').value;
    var cProcess = MCL('CurrentProcess').value.toUpperCase();
    if (cProcess == 'BULKMOVE' && AreaToInput == 'Target') {
        var inputArea = MCL('InputTargetArea');
    }
    else {
        var inputArea = MCL('InPutArea');
    }
    //            // We need to deal with any drop down lists.
    var Selects = inputArea.getElementsByTagName('select'); //or document.forms[0].elements;
    for (var i = 0; i < Selects.length; i++) {
        var cOptions = Selects[i].options;
        for (var j = 0; j < cOptions.length; j++) {
            var Key = '';
            var Value = '';
            var cBox = cOptions[j];
            if (cBox.value == B[1]) {
                cBox.selected = true;
                console.log('you are here !!!');
                if (cOptions.id == MCL('hdnCarrierID').value) { SetupDropDown('Carrier'); }
                if (cOptions.id == MCL('hdnManufacturerID').value) { SetupDropDown('Manufacturer'); }
                if (cOptions.id == MCL('hdnModelID').value) { SetupDropDown('Model'); }
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
                uppdateStatusPanel('Field/s updated');
                return;
            }
        }

        if (inputs[i].type == 'radio') {
            if (inputs[i].value == B[1]) {
                dirty = true;
                inputs[i].checked = true;
                uppdateStatusPanel('Field/s updated');
                return;
            }
        }

        if (inputs[i].type == 'text') {
            var currentValue = inputs[i].getAttribute('someValue');
            if (currentValue == B[1]) {
                dirty = true;
                inputs[i].value = B[6];
                uppdateStatusPanel('Field/s updated');
                return;
            }
        }
    }
    uppdateStatusPanel('Field/s updated')
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
    if (WithBagTag == true) {
        service.GetDetailSheetData(ID, MCL('UserName').value, BumpVersionTo900, onDetailLoadSuccess_BagTag, onDetailLoadFail);
        //               service.GetDetailSheetData(ID, onDetailLoadSuccess_BagTag, MCL('UserName').value, onDetailLoadFail);
    }
    else {
        service.GetDetailSheetData(ID, MCL('UserName').value, BumpVersionTo900, onDetailLoadSuccess, onDetailLoadFail);
    }
}

function onDetailLoadFail(Result) {
    uppdateStatusPanelError('Get Data Sheet Error...:' + Result);
    alert('Data error:' + Result);
}

function onDetailLoadSuccess(Result) {
    uppdateStatusPanelYellow('Processing...2');
    RestoreSheetData(Result);
}

function onDetailLoadSuccess_BagTag(Result) {
    RestoreSheetData(Result);
    GenerateBagTag();
}


function LoadScanNumber(ScanNumber) {
    var cProcess = MCL('CurrentProcess').value.toUpperCase();
    // We do not accept header data from any process other than those below.
    if (isReceiveScreen() == false) {
        uppdateStatusPanel('Unable to update header from this process:' + cProcess);
        return;
    }


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
    return;
}

function SaveSticky() {
    MCL('StickyData').value = '';
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
    xDataList['VTC'] = MCL('lblVersionTab').style.color;
    xDataList['ATC'] = MCL('lblAuthorizationTab').style.color;
    xDataList['HTC'] = MCL('lblHistoryTab').style.color;
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
}

function RestoreHeaderData(SetESN, SetRMA, SetDateReceived, SetProjectTag) {
    if (SetESN == null) { SetESN = true; }
    if (SetRMA == null) { SetRMA = true; }
    if (SetDateReceived == null) { SetDateReceived = true; }
    if (SetProjectTag == null) { SetProjectTag = true; }
    var dta = MCL('HeaderData').value;          //              MCL('HdnHeaderData').value;
    xDataList = Sys.Serialization.JavaScriptSerializer.deserialize(dta, true);
    MCL('ESN').value = '';
    MCL('RMA').value = '';
    var now = new Date();
    MCL('DateReceived').value = now.format('mm/dd/yyyy hh:MM tt');
    MCL('pTag').value = '';

    MCL('lblVersionTab').style.color = xDataList['VTC'];
    MCL('lblAuthorizationTab').style.color = xDataList['ATC'];
    MCL('lblHistoryTab').style.color = xDataList['HTC'];
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
    MCL('lblVersionTab').style.color = '';
    MCL('SearchReturnMode').value = '';
    MCL('ClientLocationID').value = '-1';
    MCL('CLIENTLOCATIONEMAIL').value = '';

    MCL('ReceiveHeaderID').value = '-1';
    MCL('ReceiveDetailBulkID').value = '-1';
    MCL('ReceiveDetailID').value = '-1';
    MCL('RMA').value = '';
    MCL('ESN').value = '';
    MCL('QTY').value = '';
    MCL('pTag').value = '';
    MCL('ClientName').value = '';
    MCL('StoreNumber').value = '';
    MCL('StoreSuffix').value = '';
    MCL('ClientAddress').value = '';
    var now = new Date();
    MCL('DateReceived').value = now.format('mm/dd/yyyy hh:MM tt');
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
    ClearData();
    uppdateStatusPanelYellow('Processing...3');
    xDataList = eval('({' + Data + '})');
    uppdateStatusPanelYellow('Processing...4');
    if (xDataList['ESN'] == 'Access Denied!') { uppdateStatusPanelError('ESN Access Denied!'); return; }

    var cProcess = MCL('CurrentProcess').value;
    var cProcessID = MCL('CurrentProcessID').value;
    var cProcessIDx = ' ' + MCL('CurrentProcessID').value + ' ';
    var cProcess_1 = xDataList['CurP'];
    cProcess = trim(cProcess).toUpperCase();
    cProcess_1 = trim(cProcess_1).toUpperCase();

    xDataList['Project'] = trim(xDataList['Project']);

    // Do we have more than one version of this ESN, show it by setting the tab colour red.
    if (xDataList['O_VERSION'] == '0') { MCL('lblVersionTab').style.color = ''; }
    else if (xDataList['O_VERSION'] == '1') { MCL('lblVersionTab').style.color = ''; }
    else if (xDataList['O_VERSION'] == '2') { MCL('lblVersionTab').style.color = '#FF9900'; }
    else { MCL('lblVersionTab').style.color = '#CC0000'; }

    if (xDataList['ProcessDependencies'].length > 0 && xDataList['ProcessDependencies'].indexOf(cProcessIDx) < 0) {
        MCL('DOAUTHORIZE').value = '-1';
        alert('ESN found, but incorrect Process (' + cProcess + ') to Load');
        uppdateStatusPanelError('This process not required for this Client ');
        ScanFocus();
        return;
    }

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
    if (xDataList['Project'] != pProject) {
        MCL('DOAUTHORIZE').value = '-1';
        alert('ESN found, but incorrect Project (' + pProject + ')\nOpen Project (' + xDataList['Project'] + ') to Load');
        uppdateStatusPanelError('ESN found, but incorrect Project, Open Project (' + xDataList['Project'] + ') to Load');
        ScanFocus();
        return;
    }
    uppdateStatusPanelYellow('Processing...5');
    if (cProcess == 'SHIPPING' && xDataList['NEEDHCA'] == 'T' && MCL('DOAUTHORIZE').value == '-1') {
        MCL('DOAUTHORIZE').value = '-1';
        alert('This unit has not yet Received the HardCopy Authorization\nThis unit can not be shipped yet');
        uppdateStatusPanelError('HardCopy Authorization Required');
        ScanFocus();
        return;
    }

    if (cProcess == cProcess_1 || cProcess == 'SEARCH' || cProcess_1 == 'SAVE') {

        MCL('ProjectDependencies').value = xDataList['ProjectDependencies'];
        MCL('ProcessDependencies').value = xDataList['ProcessDependencies'];

        MCL('lblMakeModelTitle').innerHTML = xDataList['MMS'];
        MCL('lblProjectClientLocationBinTitle').innerHTML = xDataList['PCLB'];


        MCL('hdnCarrierIDx').value = xDataList['CarrierID'];
        MCL('hdnManufacturerIDx').value = xDataList['ManufactuerID'];
        MCL('hdnModelIDx').value = xDataList['ModelID'];
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

        if (xDataList['ReceiveDate'].length > 0) { MCL('DateReceived').value = xDataList['ReceiveDate']; }
        if (cProcess == 'SEARCH') { MCL('lblActiveProcess').innerHTML = xDataList['CurP']; }
        uppdateStatusPanelYellow('Processing...6');
        UpdateProcessCheckList(xDataList['CompProcList']);
        uppdateStatusPanelYellow('Processing...7');
        ScatterData(xDataList);
        uppdateStatusPanelYellow('Processing...8');
        if (MCL('Sticky').checked == true && MCL('StickyData').value.length > 0) {
            var StickyDataList = eval('({' + MCL('StickyData').value + '})');
            ScatterData(StickyDataList);
            uppdateStatusPanelYellow('Processing...9');
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
    MCL('NextStep').value = btnName;
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

// ******************************************************************

function uppdateStatusPanelError(message) {
    uppdateStatusPanel(message);
    MCL('StatusPanel').style.background = '#FFCCFF'
    MCL('Display_MSG').style.background = '#FFCCFF'
}

function uppdateStatusPanelYellow(message) {
    uppdateStatusPanel(message);
    MCL('StatusPanel').style.background = '#FFFFCC'
    MCL('Display_MSG').style.background = '#FFFFCC'
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
    FillDropDown(DropDownName);
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



function FillDropDown(DropDownName) {
    var service = new WebServer_01();
    if (DropDownName == 'Carrier') {
        //               var xid = MCL('hdnCarrierID').value;
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
        var rValue = service.GetColourDropDownData(GetDropDownValue(MCL('hdnCarrierID').value), GetDropDownValue(MCL('hdnManufacturerID').value), GetDropDownValue(MCL('hdnModelID').value), MCL('UserName').value, onFillColourList, null, null);
        return;
    }
}

function onFillManufacturerListError(Result) {
    alert('Error - onFillManufacturerListError:' + Result);
}

function onFillManufacturerList(Result) {
    if (MCL('hdnisMasterLinked').value != 'True') { return; }
    var DropDown = $get(MCL('hdnManufacturerID').value);
    if (DropDown != null) {
        var CurrentValue = GetDropDownValue(MCL('hdnManufacturerID').value);
        while (DropDown.options.length > 0) DropDown.remove(0);
        if (Result.length > 0) {
            ClientData = eval('({' + Result + '})');
            for (var key in ClientData) {
                var attrName = key;
                var attrValue = ClientData[key];
                addOption(DropDown, key, ClientData[key], CurrentValue)
            }
        }
    }
    FillDropDown('Manufacturer');
    return;
}


function onFillModelList(Result) {
    if (MCL('hdnisMasterLinked').value != 'True') { return; }
    var DropDown = $get(MCL('hdnModelID').value)
    if (DropDown != null) {
        var CurrentValue = GetDropDownValue(MCL('hdnModelID').value);
        while (DropDown.options.length > 0) DropDown.remove(0);
        if (Result.length > 0) {
            ClientData = eval('({' + Result + '})');
            for (var key in ClientData) {
                var attrName = key;
                var attrValue = ClientData[key];
                addOption(DropDown, key, ClientData[key], CurrentValue)
            }
        }
    }
    FillDropDown('Model');
    return;
}

function onFillColourList(Result) {
    if (MCL('hdnisMasterLinked').value != 'True') { return; }
    var DropDown = $get(MCL('hdnColourID').value)
    if (DropDown != null) {
        var CurrentValue = GetDropDownValue(MCL('hdnColourID').value);
        while (DropDown.options.length > 0) DropDown.remove(0);
        if (Result.length > 0) {
            ClientData = eval('({' + Result + '})');
            for (var key in ClientData) {
                var attrName = key;
                var attrValue = ClientData[key];
                addOption(DropDown, key, ClientData[key], CurrentValue)
            }
        }
    }
    return;
}



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
    if (cntrl == null) return;
    cntrl.style.visibility = 'visible';
}
function ControlHide(cntrl) {
    if (cntrl == null) return;
    cntrl.style.visibility = 'hidden';
}
function IsControlHiden(cntrl) {
    if (cntrl == null) return true;
    if (cntrl.style.visibility == 'hidden') { return true; }
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
    var WindowToOpen = 'RPT_EXCEL_Out.aspx';

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
    var WindowToOpen = 'RPT_EXCEL_Out.aspx';
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

        var WindowToOpen = 'RPT_UnitView.aspx';
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




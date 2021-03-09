var DEOAUTHID = 'ELOS-SGT-DEO';
var L10AUTHID = 'ELOS-SGT-L10';
var L20AUTHID = 'ELOS-SGT-L20';
var PWRAUTHID = 'ELOS-SGT-PWR';

var LOVAdaptors = {};
var ledger; //This is observable array which contains all the data
var gridColumns = [];



///requirement 2 changes//
var data;

var getEditorDataAdapter = function (datafield) {

    var source =
                 {
                     localdata: data,
                     datatype: "array",
                     datafields:
                     [
                         { name: 'reasons', type: 'string' }
                         //{ name: 'lastname', type: 'string' },
                         //{ name: 'productname', type: 'string' },
                         //{ name: 'available', type: 'bool' },
                         //{ name: 'quantity', type: 'number' },
                         //{ name: 'price', type: 'number' },
                         //{ name: 'date', type: 'date' }
                     ]
                 };
    var dataAdapter = new $.jqx.dataAdapter(source, { uniqueDataFields: [datafield] });
    return dataAdapter;


}
///requirement 2 changes//






function preFetchLedger_Click() {
    if (activeUnitId == 'SGT-ST1') {
        urlFetchLedger = '/SGT/FetchLossLedger1';
        urlPostLedger = '/SGT/PostChanges1';
    }
    else {
        urlFetchLedger = '/SGT/FetchLossLedger2';
        urlPostLedger = '/SGT/PostChanges2';
    }
}

function pastevaluestorow(refrecord, rowidx) {
    var key = $('#divgrid').jqxGrid('getcellvalue', rowidx, 'RdgDateTimeStr');
    console.log('Pasting on Date: ' + key);

    //$('#divgrid').jqxGrid('setcellvalue', rowidx, "E1ActualDispatchMW", refrecord.E1ActualDispatchMW);
    //$('#divgrid').jqxGrid('setcellvaluebyid', key, "E1ActualDispatchMW", refrecord.E1ActualDispatchMW);

    if (activeUnitId == 'SGT-ST1') {
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'SiteId', refrecord.SiteId);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'UnitId', refrecord.UnitId);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'GDC', refrecord.GDC);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ReferenceCapacity', refrecord.ReferenceCapacity);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ActualDispatchMW', refrecord.ActualDispatchMW);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'AmbTemp', refrecord.AmbTemp);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'IsOutageDeration', refrecord.IsOutageDeration);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'IsForced', refrecord.IsForced);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'IsPlanned', refrecord.IsPlanned);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'IsGas', refrecord.IsGas);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'StartupRampId', refrecord.StartupRampId);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'AmbDeration', refrecord.AmbDeration);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ForcedDeration', refrecord.ForcedDeration);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'PlannedDeration', refrecord.PlannedDeration);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'Reason', refrecord.Reason);
    }
    else {
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'SiteId', refrecord.SiteId);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'UnitId', refrecord.UnitId);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'E1ActualDispatchMW', refrecord.E1ActualDispatchMW);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'E2ActualDispatchMW', refrecord.E2ActualDispatchMW);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'E3ActualDispatchMW', refrecord.E3ActualDispatchMW);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'E4ActualDispatchMW', refrecord.E4ActualDispatchMW);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'E5ActualDispatchMW', refrecord.E5ActualDispatchMW);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'E6ActualDispatchMW', refrecord.E6ActualDispatchMW);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'E7ActualDispatchMW', refrecord.E7ActualDispatchMW);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'E8ActualDispatchMW', refrecord.E8ActualDispatchMW);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'Reason', refrecord.Reason);

        //'E1ActualDispatchMW', 'E1Outage', Reason

        $('#divgrid').jqxGrid('setcellvalue', rowidx, "E1Outage", refrecord.E1Outage);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, "E2Outage", refrecord.E2Outage);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, "E3Outage", refrecord.E3Outage);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, "E4Outage", refrecord.E4Outage);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, "E5Outage", refrecord.E5Outage);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, "E6Outage", refrecord.E6Outage);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, "E7Outage", refrecord.E7Outage);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, "E8Outage", refrecord.E8Outage);
    }

    $('#divgrid').jqxGrid('updatebounddata');
    $('#divgrid').jqxGrid('selectrow', rowidx);
    var id = $('#divgrid').jqxGrid('getrowid', rowidx);
    console.log('Row ID: ' + id );
       
    setTimeout(function () { $('#divgrid').jqxGrid('sortby', 'RdgDateTimeStr', 'asc'); $('#divgrid').jqxGrid('ensurerowvisible', refrecord.focusidx); console.log('Passed Param: ' + refrecord.focusidx); }, 10, refrecord)
}

function InitializeForm() {
    urlFetchLedger = '/SGT/FetchLossLedger2';
    urlPostLedger = '/SGT/PostChanges2';
    urlFetchReport = '/ReportCore/getELRHourlyForUnit';

   
    createLOVAdaptors();
    gridColumns = getLedger2Columns();
    dataFields = getLedger2dataFields();
    InitializeELossTmplate();


    data = generatedata();

}

function getLedger1Columns() {
    return(
    [
            {
                text: 'L1',
                datafield: 'L1Approvalbool',
                columntype: 'checkbox',
                width: 20,
                pinned: true,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'L2',
                datafield: 'L2Approvalbool',
                columntype: 'checkbox',
                width: 20,
                pinned: true,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'DateTime',
                datafield: 'RdgDateTimeStr',
                width: 100,
                pinned: true,
                cellsformat: 'dd-MMM-yy HH:mm',
                editable: false,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'SiteId',
                datafield: 'SiteId',
                editable: false,
                width: 50,
                pinned: true,
                hidden: true,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'UnitId',
                datafield: 'UnitId',
                editable: false,
                width: 50,
                pinned: true,
                hidden: true,
                cellbeginedit: fncellbeginedit
            },
             {
                 text: 'GDC',
                 datafield: 'GDC',
                 editable: false,
                 width: 50,
                 pinned: true,
                 cellbeginedit: fncellbeginedit
             },
             {
                 text: 'ReferenceCapacity',
                 datafield: 'ReferenceCapacity',
                 editable: false,
                 width: 40,
                 pinned: true,
                 renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                     return '<div style="text-align:center;margin: 3px 0 0 3px;">Refer.<br>Cap.</div>';
                 },
                 cellbeginedit: fncellbeginedit
             },
            {
                text: 'ActualDispatchMW',
                datafield: 'ActualDispatchMW',
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">Actual<br>Dispatch<br>(MW)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f2',
                validation: dataValidator,
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'AmbTemp',
                datafield: 'AmbTemp',
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">Ambient<br>Temp<br>(Deg. C)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f2',
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'Outage Deration',
                datafield: 'IsOutageDeration',
                displayfield: 'TXTOutageDeration',
                columntype: 'dropdownlist',
                width: 60,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.OutageDeration, displayMember: "Value", valueMember: "Key" }).on('close', function (event) { exiteditmode(event, row, value, editor, 'IsOutageDeration'); }); },
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">Outage<br>Deration</div>';
                },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'Forced Forced Grid',
                datafield: 'IsForced',
                displayfield: 'TXTForcedGrid',
                columntype: 'dropdownlist',
                width: 70,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.ForcedGrid, displayMember: "Value", valueMember: "Key" }).on('close', function (event) { exiteditmode(event, row, value, editor, 'IsForced'); }); },
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">Forced<br>Forced Grid</div>';
                },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,

            },
            {
                text: 'Planned',
                datafield: 'IsPlanned',
                displayfield: 'TXTPlanned',
                columntype: 'dropdownlist',
                width: 60,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.IsPlanned, displayMember: "Value", valueMember: "Key" }).on('close', function (event) { exiteditmode(event, row, value, editor, 'IsPlanned'); }); },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'Gas',
                datafield: 'IsGas',
                displayfield: 'TXTGas',
                columntype: 'dropdownlist',
                width: 50,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.IsGas, displayMember: "Value", valueMember: "Key" }).on('close', function (event) { exiteditmode(event, row, value, editor, 'IsGas'); }); },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'StartupRamp',
                datafield: 'StartupRampId',
                displayfield: 'TXTStartupRamp',
                columntype: 'dropdownlist',
                width: 60,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.StartupRamp, displayMember: "Value", valueMember: "Key" }) },
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">Startup<br>Ramp</div>';
                },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
             {
                 text: 'Deration due to Ambient',
                 datafield: 'AmbDeration',
                 width: 60,
                 hidden: true,
                 renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                     return '<div style="text-align:center;margin: 3px 0 0 3px;">Ambient<br>Deration<br>(MW)</div>';
                 },
                 columntype: 'numberinput',
                 cellsformat: 'f2',
                 cellclassname: fncellclass,
                 cellbeginedit: fncellbeginedit
             },
              {
                  text: 'Forced Deration',
                  datafield: 'ForcedDeration',
                  width: 60,
                  renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                      return '<div style="text-align:center;margin: 3px 0 0 3px;">Forced<br>Deration<br>(MW)</div>';
                  },
                  columntype: 'numberinput',
                  cellsformat: 'f2',
                  validation: dataValidator,
                  cellclassname: fncellclass,
                  cellbeginedit: fncellbeginedit
              },
               {
                   text: 'Planned',
                   datafield: 'PlannedDeration',
                   width: 60,
                   renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                       return '<div style="text-align:center;margin: 3px 0 0 3px;">Planned<br>Deration<br>(MW)</div>';
                   },
                   columntype: 'numberinput',
                   cellsformat: 'f2',
                   cellclassname: fncellclass,
                   cellbeginedit: fncellbeginedit
               },
            //{
            //    text: 'Reason',
            //    datafield: 'Reason',
            //    width: 300,
            //    cellclassname: fncellclass,
            //    cellbeginedit: fncellbeginedit,
            //    initeditor: function (row, column, editor) {
            //        editor.attr('maxlength', 300);
            //    }
            //}



            //implementing requirement 2 changes
                    {
                        text: 'Reason',
                        datafield: 'Reason',
                        columntype: 'template',
                        width: 200,
                        createeditor: function (row, cellvalue, editor, cellText, width, height) {
                            //editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Reason, displayMember: "Value", valueMember: "Key" })
                            // construct the editor.
                            var inputElement = $("<input/>").prependTo(editor);
                            inputElement.jqxInput({ source: getEditorDataAdapter('reasons'), displayMember: "reasons", width: width, height: height });
                        },
                        initeditor: function (row, cellvalue, editor, celltext, pressedkey) {
                            // set the editor's current value. The callback is called each time the editor is displayed.
                            var inputField = editor.find('input');
                            if (pressedkey) {
                                inputField.val(pressedkey);
                                inputField.jqxInput('selectLast');
                            }
                            else {
                                inputField.val(cellvalue);
                                inputField.jqxInput('selectAll');
                            }
                        },
                        geteditorvalue: function (row, cellvalue, editor) {
                            // return the editor's value.
                            return editor.find('input').val();
                        },
                        renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                            return '<div style="text-align:center;margin: 3px 0 0 3px;">Reason</div>';
                        },
                        cellclassname: fncellclass,
                        cellbeginedit: fncellbeginedit
                    }

    ]);
}

function getLedger2Columns() {
    return(
    [
            {
                text: 'L1',
                datafield: 'L1Approvalbool',
                columntype: 'checkbox',
                width: 20,
                pinned: true,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'L2',
                datafield: 'L2Approvalbool',
                columntype: 'checkbox',
                width: 20,
                pinned: true,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'DateTime',
                datafield: 'RdgDateTimeStr',
                width: 100,
                pinned: true,
                cellsformat: 'dd-MMM-yy HH:mm',
                editable: false,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'SiteId',
                datafield: 'SiteId',
                editable: false,
                width: 50,
                pinned: true,
                hidden: true,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'UnitId',
                datafield: 'UnitId',
                editable: false,
                width: 50,
                pinned: true,
                hidden: true,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'E1ActualDispatchMW',
                datafield: 'E1ActualDispatchMW',
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">E1Actual<br>Dispatch<br>(MW)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f3',
                validation: dataValidator,
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ inputMode: 'simple', spinButtons: true, decimalDigits: 3});
                }
            },
            {
                text: 'E1Outage',
                datafield: 'E1Outage',
                displayfield: 'TXTE1Outage',
                columntype: 'dropdownlist',
                width: 75,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Outage, displayMember: 'Value', valueMember: 'Key' }).on('close', function (event) { exiteditmode(event, row, value, editor, 'E1Outage'); }); },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'E2ActualDispatchMW',
                datafield: 'E2ActualDispatchMW',
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">E2Actual<br>Dispatch<br>(MW)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f3',
                validation: dataValidator,
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ inputMode: 'simple', spinButtons: true, decimalDigits: 3 });
                }
            },
            {
                text: 'E2Outage',
                datafield: 'E2Outage',
                displayfield: 'TXTE2Outage',
                columntype: 'dropdownlist',
                width: 75,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Outage, displayMember: 'Value', valueMember: 'Key' }).on('close', function (event) { exiteditmode(event, row, value, editor, 'E2Outage'); }); },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'E3ActualDispatchMW',
                datafield: 'E3ActualDispatchMW',
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">E3Actual<br>Dispatch<br>(MW)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f3',
                validation: dataValidator,
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ inputMode: 'simple', spinButtons: true, decimalDigits: 3 });
                }
            },
            {
                text: 'E3Outage',
                datafield: 'E3Outage',
                displayfield: 'TXTE3Outage',
                columntype: 'dropdownlist',
                width: 75,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Outage, displayMember: 'Value', valueMember: 'Key' }).on('close', function (event) { exiteditmode(event, row, value, editor, 'E3Outage'); }); },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'E4ActualDispatchMW',
                datafield: 'E4ActualDispatchMW',
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">E4Actual<br>Dispatch<br>(MW)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f3',
                validation: dataValidator,
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ inputMode: 'simple', spinButtons: true, decimalDigits: 3 });
                }
            },
            {
                text: 'E4Outage',
                datafield: 'E4Outage',
                displayfield: 'TXTE4Outage',
                columntype: 'dropdownlist',
                width: 75,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Outage, displayMember: 'Value', valueMember: 'Key' }).on('close', function (event) { exiteditmode(event, row, value, editor, 'E4Outage'); }); },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'E5ActualDispatchMW',
                datafield: 'E5ActualDispatchMW',
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">E5Actual<br>Dispatch<br>(MW)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f3',
                validation: dataValidator,
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ inputMode: 'simple', spinButtons: true, decimalDigits: 3 });
                }
            },
            {
                text: 'E5Outage',
                datafield: 'E5Outage',
                displayfield: 'TXTE5Outage',
                columntype: 'dropdownlist',
                width: 75,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Outage, displayMember: 'Value', valueMember: 'Key' }).on('close', function (event) { exiteditmode(event, row, value, editor, 'E5Outage'); }); },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'E6ActualDispatchMW',
                datafield: 'E6ActualDispatchMW',
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">E6Actual<br>Dispatch<br>(MW)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f3',
                validation: dataValidator,
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ inputMode: 'simple', spinButtons: true, decimalDigits: 3 });
                }
            },
            {
                text: 'E6Outage',
                datafield: 'E6Outage',
                displayfield: 'TXTE6Outage',
                columntype: 'dropdownlist',
                width: 75,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Outage, displayMember: 'Value', valueMember: 'Key' }).on('close', function (event) { exiteditmode(event, row, value, editor, 'E6Outage'); }); },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'E7ActualDispatchMW',
                datafield: 'E7ActualDispatchMW',
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">E7Actual<br>Dispatch<br>(MW)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f3',
                validation: dataValidator,
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ inputMode: 'simple', spinButtons: true, decimalDigits: 3 });
                }
            },
            {
                text: 'E7Outage',
                datafield: 'E7Outage',
                displayfield: 'TXTE7Outage',
                columntype: 'dropdownlist',
                width: 75,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Outage, displayMember: 'Value', valueMember: 'Key' }).on('close', function (event) { exiteditmode(event, row, value, editor, 'E7Outage'); }); },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'E8ActualDispatchMW',
                datafield: 'E8ActualDispatchMW',
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">E8Actual<br>Dispatch<br>(MW)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f3',
                validation: dataValidator,
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ inputMode: 'simple', spinButtons: true, decimalDigits: 3 });
                }
            },
            {
                text: 'E8Outage',
                datafield: 'E8Outage',
                displayfield: 'TXTE8Outage',
                columntype: 'dropdownlist',
                width: 75,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Outage, displayMember: 'Value', valueMember: 'Key' }).on('close', function (event) { exiteditmode(event, row, value, editor, 'E8Outage'); }); },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            //{
            //    text: 'Reason',
            //    datafield: 'Reason',
            //    width: 200,
            //    cellclassname: fncellclass,
            //    cellbeginedit: fncellbeginedit,
            //    initeditor: function (row, column, editor) {
            //        editor.attr('maxlength', 300);
            //    }
            //}

            //implementing requirement 2 changes
                    {
                        text: 'Reason',
                        datafield: 'Reason',
                        columntype: 'template',
                        width: 200,
                        createeditor: function (row, cellvalue, editor, cellText, width, height) {
                            //editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Reason, displayMember: "Value", valueMember: "Key" })
                            // construct the editor.
                            var inputElement = $("<input/>").prependTo(editor);
                            inputElement.jqxInput({ source: getEditorDataAdapter('reasons'), displayMember: "reasons", width: width, height: height });
                        },
                        initeditor: function (row, cellvalue, editor, celltext, pressedkey) {
                            // set the editor's current value. The callback is called each time the editor is displayed.
                            var inputField = editor.find('input');
                            if (pressedkey) {
                                inputField.val(pressedkey);
                                inputField.jqxInput('selectLast');
                            }
                            else {
                                inputField.val(cellvalue);
                                inputField.jqxInput('selectAll');
                            }
                        },
                        geteditorvalue: function (row, cellvalue, editor) {
                            // return the editor's value.
                            return editor.find('input').val();
                        },
                        renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                            return '<div style="text-align:center;margin: 3px 0 0 3px;">Reason</div>';
                        },
                        cellclassname: fncellclass,
                        cellbeginedit: fncellbeginedit
                    }

    ]);
}

function getLedger1dataFields() {
    return(
        [
            { name: 'SiteId', type: 'string' },
            { name: 'UnitId', type: 'string' },
            { name: 'RdgDateTimeStr', type: 'date' },
            { name: 'ActualDispatchMW', type: 'number' },
            { name: 'AmbTemp', type: 'float' },
            { name: 'IsOutageDeration', type: 'string' },
            { name: 'TXTOutageDeration', value: 'IsOutageDeration', values: { source: LOVAdaptors.OutageDeration.records, value: 'Key', name: 'Value' } },
            { name: 'IsForced', type: 'string' },
            { name: 'TXTForcedGrid', value: 'IsForced', values: { source: LOVAdaptors.ForcedGrid.records, value: 'Key', name: 'Value' } },
            { name: 'IsPlanned', type: 'string' },
            { name: 'TXTPlanned', value: 'IsPlanned', values: { source: LOVAdaptors.IsPlanned.records, value: 'Key', name: 'Value' } },
            { name: 'IsGas', type: 'string' },
            { name: 'TXTGas', value: 'IsGas', values: { source: LOVAdaptors.IsGas.records, value: 'Key', name: 'Value' } },
            { name: 'StartupRampId', type: 'string' },
            { name: 'TXTStartupRamp', value: 'StartupRampId', values: { source: LOVAdaptors.StartupRamp.records, value: 'Key', name: 'Value' } },
            { name: 'Reason', type: 'string' },
            { name: 'AmbDeration', type: 'float' },
            { name: 'ForcedDeration', type: 'float' },
            { name: 'PlannedDeration', type: 'float' },
            { name: 'L1Approvalbool', type: 'bool' },
            { name: 'L1ApprovalCopy', type: 'bool' },
            { name: 'L2Approvalbool', type: 'bool' },
            { name: 'L2ApprovalCopy', type: 'bool' },
            { name: 'ReferenceCapacity', type: 'string' },
            { name: 'GDC', type: 'string' },
            { name: 'CreateDateStr', type: 'string' }]
        );
}

function getLedger2dataFields() {
    return(
        [{ name: 'SiteId', type: 'string' },
                     { name: 'UnitId', type: 'string' },
                     { name: 'RdgDateTimeStr', type: 'date' },
                     { name: 'E1ActualDispatchMW', type: 'number' },
                     { name: 'E1Outage', type: 'string' },
                     { name: 'TXTE1Outage', value: 'E1Outage', values: { source: LOVAdaptors.Outage.records, value: 'Key', name: 'Value' } },
                     { name: 'E2ActualDispatchMW', type: 'number' },
                     { name: 'E2Outage', type: 'string' },
                     { name: 'TXTE2Outage', value: 'E2Outage', values: { source: LOVAdaptors.Outage.records, value: 'Key', name: 'Value' } },
                     { name: 'E3ActualDispatchMW', type: 'number' },
                     { name: 'E3Outage', type: 'string' },
                     { name: 'TXTE3Outage', value: 'E3Outage', values: { source: LOVAdaptors.Outage.records, value: 'Key', name: 'Value' } },
                     { name: 'E4ActualDispatchMW', type: 'number' },
                     { name: 'E4Outage', type: 'string' },
                     { name: 'TXTE4Outage', value: 'E4Outage', values: { source: LOVAdaptors.Outage.records, value: 'Key', name: 'Value' } },
                     { name: 'E5ActualDispatchMW', type: 'number' },
                     { name: 'E5Outage', type: 'string' },
                     { name: 'TXTE5Outage', value: 'E5Outage', values: { source: LOVAdaptors.Outage.records, value: 'Key', name: 'Value' } },
                     { name: 'E6ActualDispatchMW', type: 'number' },
                     { name: 'E6Outage', type: 'string' },
                     { name: 'TXTE6Outage', value: 'E6Outage', values: { source: LOVAdaptors.Outage.records, value: 'Key', name: 'Value' } },
                     { name: 'E7ActualDispatchMW', type: 'number' },
                     { name: 'E7Outage', type: 'string' },
                     { name: 'TXTE7Outage', value: 'E7Outage', values: { source: LOVAdaptors.Outage.records, value: 'Key', name: 'Value' } },
                     { name: 'E8ActualDispatchMW', type: 'number' },
                     { name: 'E8Outage', type: 'string' },
                     { name: 'TXTE8Outage', value: 'E8Outage', values: { source: LOVAdaptors.Outage.records, value: 'Key', name: 'Value' } },
                     { name: 'Reason', type: 'string' },
                     { name: 'L1Approvalbool', type: 'bool' },
                     { name: 'L1ApprovalCopy', type: 'bool' },
                     { name: 'L2Approvalbool', type: 'bool' },
                     { name: 'L2ApprovalCopy', type: 'bool' },
                     { name: 'CreateDateStr', type: 'string' }]
        );
}
function createLOVAdaptors() {
    LOVAdaptors.Outage = new $.jqx.dataAdapter({
        localdata: LOVOutage,
        datatype: "array",
        datafields: [{ name: 'Key', type: 'string' },
                     { name: 'Value', type: 'string' }]
    },
    { autoBind: true });

    LOVAdaptors.ForcedGrid = new $.jqx.dataAdapter({
        localdata: LOVForcedGrid,
        datatype: "array",
        datafields: [{ name: 'Key', type: 'string' },
                     { name: 'Value', type: 'string' }]
    },
       { autoBind: true });

    LOVAdaptors.IsGas = new $.jqx.dataAdapter({
        localdata: LOVIsGas,
        datatype: "array",
        datafields: [{ name: 'Key', type: 'string' },
                     { name: 'Value', type: 'string' }]
    },
        { autoBind: true });

    LOVAdaptors.IsPlanned = new $.jqx.dataAdapter({
        localdata: LOVIsPlanned,
        datatype: "array",
        datafields: [{ name: 'Key', type: 'string' },
                     { name: 'Value', type: 'string' }]
    },
        { autoBind: true });

    LOVAdaptors.StartupRamp = new $.jqx.dataAdapter({
        localdata: LOVStartupRamp,
        datatype: "array",
        datafields: [{ name: 'Key', type: 'string' },
                     { name: 'Value', type: 'string' }]
    },
        { autoBind: true });

    LOVAdaptors.OutageDeration = new $.jqx.dataAdapter({
        localdata: LOVOutageDeration,
        datatype: "array",
        datafields: [{ name: 'Key', type: 'string' },
                     { name: 'Value', type: 'string' }]
    },
    { autoBind: true });
}

function bindDataGrid(data) {
    var dataFields = [];

    if (activeUnitId == 'SGT-ST1') {
        dataFields = getLedger1dataFields();
        gridColumns = getLedger1Columns();
    }
        
    else{
        dataFields = getLedger2dataFields();
        gridColumns = getLedger2Columns();
    }
        

    ledger = new $.jqx.observableArray(data, function (changed) {
        //logic to keep track which record has been updated/added by the user
        ledger[changed.index].ctrack = "Y";
    });

    var dsledger = {
        localdata: ledger,
        datatype: "obserableArray",
        datafields: dataFields
    };

    var adapledger = new $.jqx.dataAdapter(dsledger);

    $("#divgrid").jqxGrid(
    {
        source: adapledger, width: '100%', height: '420px', sortable: false, columnsresize: true, editable: true, filterable: false, pageable: false,
        altrows: false, showeverpresentrow: false, editmode: 'click', selectionmode: "singlerow", theme: "office", columnsheight: '40',
        columnsheight: 50, autorowheight: false, autoheight: false, enablebrowserselection: true, enableanimations: true, filtermode: 'excel', keyboardnavigation: false,
        columns: gridColumns
    });


    for (var i = 0; i < hiddenCols.length; i++) {
        $('#divgrid').jqxGrid('hidecolumn', hiddenCols[i]);
    }

    $("#divgrid").on('cellvaluechanged', fncellvaluechanged);
    //$('#divgrid').on('rowdoubleclick', DataGrid_DoubleClick);
}

function IsRowEditable(rownum) {
    var data = $('#divgrid').jqxGrid('getrowdata', rownum);

    switch (AuthId) {
        case DEOAUTHID:
            return !(data.L1ApprovalCopy);
            break;
        case L10AUTHID:
            return !(data.L1ApprovalCopy);
            break;
        case L20AUTHID:
            return !(data.L2ApprovalCopy);
            break;
        case PWRAUTHID:
            return true;
            break;
        default:
            return false;
    }
}

var dataValidator = function (cell, value) {
    if (cell.datafield.substring(2) == 'ActualDispatchMW') {
        //if (value < 0 || value > 2.739)
        if (value < 0)
            //return { result: false, message: "Please enter correct value. A valid value ranges from 0 to 2.739" };
            return { result: false, message: "Please enter correct value. A valid value ranges from 0 to maximum" };


        var rowdata = $('#divgrid').jqxGrid('getrowdata', cell.row);
        var OutageSelection = rowdata[cell.datafield.substring(0, 2) + 'Outage'];
        //console.log(OutageSelection);

        if(OutageSelection != 'N' && value != 0)
            return { result: false, message: "Engine, when under Outage, cannot have this dispatch value" };

        if(OutageSelection == 'N' && value == 0)
            return { result: false, message: "Press ESC key and then select Outage reason from " + cell.datafield.substring(0, 2) + "Outage Dropdown" };
    }
    else if (cell.datafield == 'ActualDispatchMW') {
        var rowdata = $('#divgrid').jqxGrid('getrowdata', cell.row);
        var OutageSelection = rowdata['IsOutageDeration'];
        //console.log(OutageSelection);


        if (value < 0)
            //return { result: false, message: "Please enter correct value. A valid value ranges from 0 to 2.739" };
            return { result: false, message: "Please enter correct value. A valid value ranges from 0 to maximum" };

        


        if (OutageSelection == 'O' && value != 0)
            return { result: false, message: "Engine, when under Outage, cannot have this dispatch value" };

        if (OutageSelection != 'O' && value == 0)
            return { result: false, message: "Press ESC key and then select Outage from Outage/Deration dropdown" };





        //set previous | current MW segregation..need to update after sucessful UAT..
        var row = $('#divgrid').jqxGrid('getrowdata', cell.row);
        var b = isPreviousCurrentMWSegregationSameDataValidator(cell.row, value);
        if (b == true) {
            // alert('unable to change the Startup Ramp value');
            SetPreviousCurrentMWSugregationRequiremenDataValidatort(cell.row, value);
            //return false;
        }


    }







    //console.log(cell);
    //console.log(value);
    return true;
}

var setVisibility = function (datafield) {
    return false;
}

var fncellbeginedit = function (row, datafield, columntype, value) {
    var data = $('#divgrid').jqxGrid('getrowdata', row);
    switch (AuthId) {
        case L10AUTHID:
            if (datafield == 'L1Approvalbool' && data.L1ApprovalCopy == true) {
                alert('This record has been authorized earlier and no longer available for editing.If you need to edit this record, please contact to Performance team for unlocking the record');
                return false;
            }
            break;
        case L20AUTHID:
            if (datafield == 'L1Approvalbool' && data.L2Approvalbool == true) {
                alert('Please First revoke Level 2 Approval before revoking Level 1 approval');
                return false;
            }
            if (datafield == 'L2Approvalbool' && data.L2ApprovalCopy == true) {
                alert('This record has been authorized earlier and no longer available for editing. If you need to edit this record, please contact to Power User for unlocking the record');
                return false;
            }
            /*----------------------New Addition From Here--------------------------------------------------*/
            if (datafield == 'L2Approvalbool' && (data.L1ApprovalCopy == false || data.L1Approvalbool == false)) {
                alert('Please First grant Level 1 Approval before granting Level 2 Approval');
                return false;
            }
            if (datafield == 'L1Approvalbool' && data.L1ApprovalCopy == false) {
                alert('You do not have rights to give L1(Operation Team) approval. Please contact Operation team for approving the record');
                return false;
            }
            /*----------------------New Addition To Here--------------------------------------------------*/
            break;
        case PWRAUTHID:
            if (datafield == 'L1Approvalbool' && data.L2Approvalbool == true) {
                alert('Please First revoke Level 2 Approval before revoking Level 1 approval');
                return false;
            }
            if (datafield == 'L2Approvalbool' && data.L1Approvalbool == false) {
                alert('Please First grant Level 1 Approval before granting Level 2 Approval');
                return false;
            }
            break;
        default:
            break;
    }

    if (datafield == 'StartupRampId') {
        var b = isPreviousCurrentMWSegregationSame(row);
        if (b == true) {
            // alert('unable to change the Startup Ramp value');
            SetPreviousCurrentMWSugregationRequirement(row);
            return false;
        }
    }




    return IsRowEditable(row);
}

var fncellclass = function (row, columnfield, value) {
    var data = $('#divgrid').jqxGrid('getrowdata', row);
    if (IsRowEditable(row)) {
        if (AuthId == DEOAUTHID && data.CreateDateStr != '')
            return 'submitted';
        return 'opened';
    }
    return 'disabledrow';
}

var fnexiteditmode = function (event) {
    console.log(event.args);
    //setTimeout(function () {
    //    console.log(row);
    //    $("#divgrid").jqxGrid('endcelledit', row, "E1Outage", false);
    //});
}

function exiteditmode(event, row, value, editor, colName) {
    event.stopPropagation();
    console.log(event, row);

    setTimeout(function () {
        var rowindex = $('#divgrid').jqxGrid('getselectedrowindex');
        var cell = $('#divgrid').jqxGrid('getselectedcells');
        console.log(rowindex, cell);
        $("#divgrid").jqxGrid('endcelledit', rowindex, null, false);
        //$('#divgrid').jqxGrid('sortby', 'RdgDateTimeStr', 'asc');
        //$('#divgrid').jqxGrid('updatebounddata', 'cells');
        //$('#divgrid').jqxGrid('refreshdata');
    }, 1);
}

var fncellvaluechanged = function (event) {
    var outagefieldlist = ['E1Outage', 'E2Outage', 'E3Outage', 'E4Outage', 'E5Outage', 'E6Outage', 'E7Outage', 'E8Outage'];
    var MWfieldlist = ['E1ActualDispatchMW', 'E2ActualDispatchMW', 'E3ActualDispatchMW', 'E4ActualDispatchMW', 'E5ActualDispatchMW', 'E6ActualDispatchMW', 'E7ActualDispatchMW', 'E8ActualDispatchMW'];
    var ledger1fields = ['IsOutageDeration', 'IsGas', 'IsPlanned', 'IsForced'];
    // event arguments.
    //var args = event.args;
    // column data field.
    var datafield = event.args.datafield;
    var rowBoundIndex = args.rowindex;
    var value = args.newvalue;
    var oldvalue = args.oldvalue;
    var newvalue;

    if (value == null || typeof value == 'undefined' || typeof value.value == 'undefined') {
        return;
    }
    else {
        newvalue = value.value;
    }

    var idxoutage = jQuery.inArray(datafield, outagefieldlist);
    var idxledger1fields = jQuery.inArray(datafield, ledger1fields);

    if (idxoutage != -1) {
        var targetfname = MWfieldlist[idxoutage];
        ledger2CellvaluechangeHandler(newvalue, rowBoundIndex, targetfname); 
    }
    else if (idxledger1fields != -1) {
        ledger1CellvaluechangeHandler(datafield, newvalue, rowBoundIndex);
    }
}

function ledger2CellvaluechangeHandler(newvalue, rowBoundIndex, targetfname){
    if (newvalue != 'N') {
        $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, targetfname, 0);
    }
    else {
        var rows = $("#divgrid").jqxGrid('getrows');
        var dt = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'RdgDateTimeStr');
        var dt1;
        var refrow;
        console.log(rows);

        for (var i = 0; i < rows.length -1; i++) {
            dt1 = new Date(rows[i + 1].RdgDateTimeStr);
            if (dt1.valueOf() == dt.valueOf()) {
                refrow = rows[i];
                break;
            }
        }
            
        //console.log(refrow[targetfname]);

        if (typeof refrow == 'undefined') {
            $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, targetfname, 2.739);
        }
        else {
            var currval = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, targetfname);
            if(currval == 0){
                var val = refrow[targetfname];
                if (val == 0)
                    val = 2.739;
                
                $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, targetfname, val);
            }
        }   
    }
}

function ledger1CellvaluechangeHandler(datafield, newvalue, rowBoundIndex) {
    if (datafield == 'IsOutageDeration') {
        if (newvalue == 'O') {
            $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'ActualDispatchMW', 0);
            var F = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsForced');
            var P = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsPlanned');
            var G= $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsGas');
            
            if (F != 'N' && P == 'N' && G == 'N') {
                return;
            }
            if (P != 'N' && F == 'N' && G == 'N') {
                return;
            }
            if (G != 'N' && F == 'N' && P == 'N') {
                return;
            }
            $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsForced', 'F');
            $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsPlanned', 'N');
            $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsGas', 'N');
                //var regex = new RegExp("N+");

            //setTimeout(function () { $('#divgrid').jqxGrid('updatebounddata') }, 10);
        }
    }
    else if (datafield == 'IsForced') {
        var out = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsOutageDeration');
        var F = newvalue; //$("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsForced');
        var P = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsPlanned');
        var G= $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsGas');

        if (out == 'O') {
            if (F == 'N' && P == 'N' && G == 'N') {
                $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsPlanned', 'Y');
            }
            else if(F != 'N'){
                $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsPlanned', 'N');
                $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsGas', 'N');
            }
        //setTimeout(function () { $('#divgrid').jqxGrid('updatebounddata') }, 10);
        }
    }
    else if (datafield == 'IsPlanned') {
        var out = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsOutageDeration');
        var F = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsForced');
        var P = newvalue; //$("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsPlanned');
        var G = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsGas');

        if (out == 'O') {
            if (F == 'N' && P == 'N' && G == 'N') {
                $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsForced', 'F');
            }
            else if (P != 'N') {
                $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsForced', 'N');
                $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsGas', 'N');
            }
            //setTimeout(function () { $('#divgrid').jqxGrid('updatebounddata') }, 10);
        }
    }
    else if (datafield == 'IsGas') {
        var out = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsOutageDeration');
        var F = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsForced');
        var P = $("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsPlanned');
        var G = newvalue; //$("#divgrid").jqxGrid('getcellvalue', rowBoundIndex, 'IsGas');

        if (out == 'O') {
            if (F == 'N' && P == 'N' && G == 'N') {
                $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsForced', 'F');
            }
            else if (G != 'N') {
                $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsForced', 'N');
                $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, 'IsPlanned', 'N');
            }
            //setTimeout(function () { $('#divgrid').jqxGrid('updatebounddata') }, 10);
        }
    }
    setTimeout(function () { $('#divgrid').jqxGrid('updatebounddata', 'cells'); }, 10);
}




//implementing requirement 2 changes..
//implementing requirement 2 cganges..
function interlockoutagefieldlist(newvalue, rowBoundIndex, key) {

    //implementing interlock condition.. ////interlock is required 
    if (newvalue != 'N') {
        var ODfields = ['IsOutageDeration', 'IsForced', 'IsPlanned', 'IsLDC', 'IsGas'];
        var ODfieldsKeys = [newvalue, LOVForcedGrid[1].Key, LOVIsPlanned[1].Key, LOVIsLDC[1].Key, LOVIsGas[1].Key];
        for (var i = 0; i < ODfields.length; i++) {
            $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, ODfields[i], ODfieldsKeys[i]);
        }
    }
    else if (newvalue == 'N') {

        var ODfields = ['IsOutageDeration', 'IsForced', 'IsPlanned', 'IsLDC', 'IsGas'];
        var ODfieldsKeys = [newvalue, LOVForcedGrid[0].Key, LOVIsPlanned[0].Key, LOVIsLDC[0].Key, LOVIsGas[0].Key];
        for (var i = 0; i < ODfields.length; i++) {
            $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, ODfields[i], ODfieldsKeys[i]);
        }

    }


    SetPreviousCurrentMWSugregationRequirement(rowBoundIndex);
    setTimeout(function () { $('#divgrid').jqxGrid('updatebounddata', 'cells'); }, 10);

}


//implementing requirement 2 cganges.
//impementing When the previous and current MWs are same, user should not be able to select the ramp option 
function SetPreviousCurrentMWSugregationRequirement(row) {

    //When the previous and current MWs are same, user should not be able to select the ramp option 
    if (row != 0) {
        var rowdataPrevious = $('#divgrid').jqxGrid('getrowdata', (row - 1));
        var rowdataCurrent = $('#divgrid').jqxGrid('getrowdata', (row));

        var PMWV = rowdataPrevious['ActualDispatchMW'];
        var CMWV = rowdataCurrent['ActualDispatchMW'];
        var StartupRampId = rowdataPrevious['StartupRampId'];
        if (PMWV == CMWV) {
            try {
                // $("#divgrid").jqxGrid('setcellvalue', (row - 1), "StartupRampId", StartupRampId);
                $("#divgrid").jqxGrid('setcellvalue', (row), "StartupRampId", 'N');
            } catch (e) {

            }

        }


    }

    setTimeout(function () { $('#divgrid').jqxGrid('updatebounddata', 'cells'); }, 10);

}

function isPreviousCurrentMWSegregationSame(row) {

    if ((AuthId == DEOAUTHID) && (activeUnitId == 'SGT-ST1')) {

         if (row != 0) {
             var rowdataPrevious = $('#divgrid').jqxGrid('getrowdata', (row - 1));
             var rowdataCurrent = $('#divgrid').jqxGrid('getrowdata', (row));

             var PMWV = rowdataPrevious['ActualDispatchMW'];
             var CMWV = rowdataCurrent['ActualDispatchMW'];
             var StartupRampId = rowdataPrevious['StartupRampId'];
             if (PMWV == CMWV) {
                 return true;
             }
             else {
                 return false;
             }
         }


    }
    return false;
}




function SetPreviousCurrentMWSugregationRequiremenDataValidatort(row, newValue) {

    //When the previous and current MWs are same, user should not be able to select the ramp option 
    if (row != 0) {
        var rowdataPrevious = $('#divgrid').jqxGrid('getrowdata', (row - 1));
        var rowdataCurrent = $('#divgrid').jqxGrid('getrowdata', (row));

        var PMWV = rowdataPrevious['ActualDispatchMW'];
        var CMWV = rowdataCurrent['ActualDispatchMW'];
        var CMWV_ = newValue;


        var StartupRampId = rowdataPrevious['StartupRampId'];
        if (PMWV == CMWV_) {
            try {
                // $("#divgrid").jqxGrid('setcellvalue', (row - 1), "StartupRampId", StartupRampId);
                $("#divgrid").jqxGrid('setcellvalue', (row), "StartupRampId", 'N');
            } catch (e) {

            }

        }


    }

    setTimeout(function () { $('#divgrid').jqxGrid('updatebounddata', 'cells'); }, 10);

}

function isPreviousCurrentMWSegregationSameDataValidator(row, newValue) {
    if (row != 0) {
        var rowdataPrevious = $('#divgrid').jqxGrid('getrowdata', (row - 1));
        var rowdataCurrent = $('#divgrid').jqxGrid('getrowdata', (row));

        var PMWV = rowdataPrevious['ActualDispatchMW'];
        var CMWV = rowdataCurrent['ActualDispatchMW'];
        var CMWV_ = newValue;

        var StartupRampId = rowdataPrevious['StartupRampId'];
        if (PMWV == CMWV_) {
            return true;
        }
        else {
            return false;
        }


    }

    return false;
}



//implementing requirement 2 changes
function generatedata() {
    var data = new Array();
    var reasons =
    [
        "Andrew", "Nancy", "Shelley", "Regina", "Yoshi", "Antoni", "Mayumi", "Ian", "Peter", "Lars", "Petra", "Martin", "Sven", "Elio", "Beate", "Cheryl", "Michael", "Guylene"
    ];

    for (var i = 0; i < LOVReason.length; i++) {
        var row = {};
        row["reasons"] = LOVReason[i].toString();
        data[i] = row;
    }

    return data;

}

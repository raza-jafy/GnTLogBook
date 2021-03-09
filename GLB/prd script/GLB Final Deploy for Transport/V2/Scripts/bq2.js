var DEOAUTHID = 'ELOS-BQ2-DEO';
var L10AUTHID = 'ELOS-BQ2-L10';
var L20AUTHID = 'ELOS-BQ2-L20';
var PWRAUTHID = 'ELOS-BQ2-PWR';

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
    if (activeUnitId == 'BQ2-ST1') {
        urlFetchLedger = '/BQII/FetchLossLedger1';
        urlPostLedger = '/BQII/PostChanges1';
    }
    else {
        urlFetchLedger = '/BQII/FetchLossLedger3';
        urlPostLedger = '/BQII/PostChanges3';
    }
}

/*function createnewledgerentry(datetime, ledgerparams) {
    return { RdgDateTimeStr: datetime, SiteId: ledgerparams.SiteId, UnitId: ledgerparams.UnitId, E1ActualDispatchMW: 0, E2ActualDispatchMW: 0, E3ActualDispatchMW: 0, E4ActualDispatchMW: 0, E5ActualDispatchMW: 0, E6ActualDispatchMW: 0, E7ActualDispatchMW: 0, E8ActualDispatchMW: 0, E1Outage: 'N', E2Outage: 'N', E3Outage: 'N', E4Outage: 'N', E5Outage: 'N', E6Outage: 'N', E7Outage: 'N', E8Outage: 'N', L1Approvalbool: false, L1ApprovalCopy: false, L2Approvalbool: false, L2ApprovalCopy: false, CreateDateStr: '' };
}*/

function pastevaluestorow(refrecord, rowidx) {
    var key = $('#divgrid').jqxGrid('getcellvalue', rowidx, 'RdgDateTimeStr');
    console.log('Pasting on Date: ' + key);

    //$('#divgrid').jqxGrid('setcellvalue', rowidx, "E1ActualDispatchMW", refrecord.E1ActualDispatchMW);
    //$('#divgrid').jqxGrid('setcellvaluebyid', key, "E1ActualDispatchMW", refrecord.E1ActualDispatchMW);

    if (activeUnitId == 'BQ2-ST1') {
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'SiteId', refrecord.SiteId);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'UnitId', refrecord.UnitId);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'GDC', refrecord.GDC);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ReferenceCapacity', refrecord.ReferenceCapacity);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ActualDispatchMW', refrecord.ActualDispatchMW);
        //$('#divgrid').jqxGrid('setcellvalue', rowidx, 'AmbTemp', refrecord.AmbTemp);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'IsOutageDeration', refrecord.IsOutageDeration);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'IsForced', refrecord.IsForced);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'IsPlanned', refrecord.IsPlanned);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'IsLDC', refrecord.IsLDC);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'IsGas', refrecord.IsGas);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'StartupRampId', refrecord.StartupRampId);
        //$('#divgrid').jqxGrid('setcellvalue', rowidx, 'AmbDeration', refrecord.AmbDeration);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ForcedDeration', refrecord.ForcedDeration);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'PlannedDeration', refrecord.PlannedDeration);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'Reason', refrecord.Reason);
    }
    else {
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'SiteId', refrecord.SiteId);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'UnitId', refrecord.UnitId);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'GDC', refrecord.GDC);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ReferenceCapacity', refrecord.ReferenceCapacity);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'AvailableCapacity', refrecord.AvailableCapacity);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ActualDispatchMW', refrecord.ActualDispatchMW);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'OutageNature', refrecord.OutageNature);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'CalculateDeration', refrecord.CalculateDeration);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'StartShut', refrecord.StartShut);
        $('#divgrid').jqxGrid('setcellvalue', rowidx, 'Reason', refrecord.Reason);
    }

    ////$('#divgrid').jqxGrid('updatebounddata');
    //////$('#divgrid').jqxGrid('selectrow', rowidx);
    ////$('#divgrid').jqxGrid('sortby', 'RdgDateTimeStr', 'asc');

    $('#divgrid').jqxGrid('updatebounddata');
    $('#divgrid').jqxGrid('selectrow', rowidx);
    var id = $('#divgrid').jqxGrid('getrowid', rowidx);
    console.log('Row ID: ' + id);

    setTimeout(function () { $('#divgrid').jqxGrid('sortby', 'RdgDateTimeStr', 'asc'); $('#divgrid').jqxGrid('ensurerowvisible', refrecord.focusidx); console.log('Passed Param: ' + refrecord.focusidx); }, 10, refrecord);
}

function InitializeForm() {
    urlFetchLedger = '/BQII/FetchLossLedger3';
    urlPostLedger = '/BQII/PostChanges3';
    urlFetchReport = '/ReportCore/getELRHourlyForUnit';


    createLOVAdaptors();
    gridColumns = getLedger3Columns();
    dataFields = getLedger3dataFields();


    data = generatedata();



    InitializeELossTmplate();
}

function getLedger1Columns() {
    return (
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
                text: 'Outage Deration',
                datafield: 'IsOutageDeration',
                displayfield: 'TXTOutageDeration',
                columntype: 'dropdownlist',
                width: 60,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.OutageDeration, displayMember: "Value", valueMember: "Key" }) },
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
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.ForcedGrid, displayMember: "Value", valueMember: "Key" }) },
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">Forced<br>Forced Grid</div>';
                },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'Planned',
                datafield: 'IsPlanned',
                displayfield: 'TXTPlanned',
                columntype: 'dropdownlist',
                width: 60,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.IsPlanned, displayMember: "Value", valueMember: "Key" }) },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'LDC',
                datafield: 'IsLDC',
                displayfield: 'TXTLDC',
                columntype: 'dropdownlist',
                width: 60,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.IsLDC, displayMember: "Value", valueMember: "Key" }) },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'Gas',
                datafield: 'IsGas',
                displayfield: 'TXTGas',
                columntype: 'dropdownlist',
                width: 50,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.IsGas, displayMember: "Value", valueMember: "Key" }) },
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


            //implementing requirment 2 changes..
            {
                text: 'Reason',
                datafield: 'Reason',
                columntype: 'template',
                width: 500,
                createeditor: function (row, cellvalue, editor, cellText, width, height) {
                    //editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Reason, displayMember: "Value", valueMember: "Key" })
                    // construct the editor.
                    var inputElement = $("<input/>").prependTo(editor);
                    inputElement.jqxInput({ source: getEditorDataAdapter('reasons'), displayMember: "reasons", width: width, height: height });
                },
                initeditor: function (row, cellvalue, editor, celltext, pressedkey) {
                    editor.attr('maxlength', 500);
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

function getLedger3Columns() {
    return (
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
                text: 'GDC',
                datafield: 'GDC',
                editable: false,
                width: 50,
                pinned: true
            },
             {
                 text: 'ReferenceCapacity',
                 datafield: 'ReferenceCapacity',
                 editable: false,
                 width: 60,
                 pinned: true,
                 renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                     return '<div style="text-align:center;margin: 3px 0 0 3px;">Reference<br>Capacity<br>(MW)</div>';
                 }
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
                text: 'AvailableCapacity',
                datafield: 'AvailableCapacity',
                width: 90,
                pinned: true,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">Available<br>Capacity<br>(MW)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f2',
                validation: dataValidator,
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'ActualDispatchMW',
                datafield: 'ActualDispatchMW',
                width: 90,
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
                text: 'Outage Nature',
                datafield: 'OutageNature',
                displayfield: 'TXTOutageNature',
                columntype: 'dropdownlist',
                width: 120,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.OutageNature, displayMember: 'Value', valueMember: 'Key' }) },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'Calculate Deration',
                datafield: 'CalculateDeration',
                displayfield: 'TXTCalculateDeration',
                columntype: 'dropdownlist',
                width: 120,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.CalculateDeration, displayMember: "Value", valueMember: "Key" }) },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'Startup/Shutdown',
                datafield: 'StartShut',
                displayfield: 'TXTStartShut',
                columntype: 'dropdownlist',
                width: 120,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.StartShut, displayMember: "Value", valueMember: "Key" }) },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },


            //{
            //    text: 'Reason',
            //    datafield: 'Reason',
            //    width: 320,
            //    cellclassname: fncellclass,
            //    cellbeginedit: fncellbeginedit,
            //    initeditor: function (row, column, editor) {
            //        editor.attr('maxlength', 300);
            //    }
            //}




            //implementing requirment 2 changes..
                {
                    text: 'Reason',
                    datafield: 'Reason',
                    columntype: 'template',
                    width: 500,
                    createeditor: function (row, cellvalue, editor, cellText, width, height) {
                        //editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Reason, displayMember: "Value", valueMember: "Key" })
                        // construct the editor.
                        var inputElement = $("<input/>").prependTo(editor);
                        inputElement.jqxInput({ source: getEditorDataAdapter('reasons'), displayMember: "reasons", width: width, height: height });
                    },
                    initeditor: function (row, cellvalue, editor, celltext, pressedkey) {
                        editor.attr('maxlength', 500);
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
    return (
        [
            { name: 'SiteId', type: 'string' },
            { name: 'UnitId', type: 'string' },
            { name: 'RdgDateTimeStr', type: 'date' },
            { name: 'ActualDispatchMW', type: 'number' },
            { name: 'IsOutageDeration', type: 'string' },
            { name: 'TXTOutageDeration', value: 'IsOutageDeration', values: { source: LOVAdaptors.OutageDeration.records, value: 'Key', name: 'Value' } },
            { name: 'IsForced', type: 'string' },
            { name: 'TXTForcedGrid', value: 'IsForced', values: { source: LOVAdaptors.ForcedGrid.records, value: 'Key', name: 'Value' } },
            { name: 'IsPlanned', type: 'string' },
            { name: 'TXTPlanned', value: 'IsPlanned', values: { source: LOVAdaptors.IsPlanned.records, value: 'Key', name: 'Value' } },
            { name: 'IsLDC', type: 'string' },
            { name: 'TXTLDC', value: 'IsLDC', values: { source: LOVAdaptors.IsLDC.records, value: 'Key', name: 'Value' } },
            { name: 'IsGas', type: 'string' },
            { name: 'TXTGas', value: 'IsGas', values: { source: LOVAdaptors.IsGas.records, value: 'Key', name: 'Value' } },
            { name: 'StartupRampId', type: 'string' },
            { name: 'TXTStartupRamp', value: 'StartupRampId', values: { source: LOVAdaptors.StartupRamp.records, value: 'Key', name: 'Value' } },
            { name: 'Reason', type: 'string' },
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

function getLedger3dataFields() {
    return (
        [{ name: 'SiteId', type: 'string' },
                     { name: 'UnitId', type: 'string' },
                     { name: 'RdgDateTimeStr', type: 'date' },
                     { name: 'ActualDispatchMW', type: 'number' },
                     { name: 'OutageNature', type: 'string' },
                     { name: 'TXTOutageNature', value: 'OutageNature', values: { source: LOVAdaptors.OutageNature.records, value: 'Key', name: 'Value' } },
                     { name: 'CalculateDeration', type: 'string' },
                     { name: 'TXTCalculateDeration', value: 'CalculateDeration', values: { source: LOVAdaptors.CalculateDeration.records, value: 'Key', name: 'Value' } },
                     { name: 'StartShut', type: 'string' },
                     { name: 'TXTStartShut', value: 'StartShut', values: { source: LOVAdaptors.StartShut.records, value: 'Key', name: 'Value' } },
                     { name: 'Reason', type: 'string' },
                     { name: 'L1Approvalbool', type: 'bool' },
                     { name: 'L1ApprovalCopy', type: 'bool' },
                     { name: 'L2Approvalbool', type: 'bool' },
                     { name: 'L2ApprovalCopy', type: 'bool' },
                     { name: 'CreateDateStr', type: 'string' },
                     { name: 'ReferenceCapacity', type: 'string' },
                     { name: 'GDC', type: 'string' },
                     { name: 'AvailableCapacity', type: 'number' }]
        );
}
function createLOVAdaptors() {
    //LOVAdaptors.Outage = new $.jqx.dataAdapter({
    //    localdata: LOVOutage,
    //    datatype: "array",
    //    datafields: [{ name: 'Key', type: 'string' },
    //                 { name: 'Value', type: 'string' }]
    //},
    //{ autoBind: true });

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

    LOVAdaptors.IsLDC = new $.jqx.dataAdapter({
        localdata: LOVIsLDC,
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

    ////////////////////////
    LOVAdaptors.OutageNature = new $.jqx.dataAdapter({
        localdata: LOVOutageNature,
        datatype: "array",
        datafields: [{ name: 'Key', type: 'string' },
                     { name: 'Value', type: 'string' }]
    },
    { autoBind: true });

    LOVAdaptors.CalculateDeration = new $.jqx.dataAdapter({
        localdata: LOVCalculateDeration,
        datatype: "array",
        datafields: [{ name: 'Key', type: 'string' },
                     { name: 'Value', type: 'string' }]
    },
    { autoBind: true });

    LOVAdaptors.StartShut = new $.jqx.dataAdapter({
        localdata: LOVStartShut,
        datatype: "array",
        datafields: [{ name: 'Key', type: 'string' },
                     { name: 'Value', type: 'string' }]
    },
    { autoBind: true });
}

function bindDataGrid(data) {
    var dataFields = [];

    if (activeUnitId == 'BQ2-ST1') {
        dataFields = getLedger1dataFields();
        gridColumns = getLedger1Columns();
    }

    else {
        dataFields = getLedger3dataFields();
        gridColumns = getLedger3Columns();
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


    //implementing requirement 2 changes..
    $("#divgrid").on('cellvaluechanged', fncellvaluechanged);
    //implementing requirement 2 changes..



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

    //original logic posted by rehan bhai..
    //return true;

    //IMPLEMENTING REQUIREMENT 2 CHANGES
    //if ((AuthId == DEOAUTHID) && (activeUnitId == 'BQ2-ST1')) {
    if ((AuthId == DEOAUTHID)) {
        if (cell.datafield == 'ActualDispatchMW') {

            //if (value < 0 || value > 2.739)
            if (value < 0)
                //return { result: false, message: "Please enter correct value. A valid value ranges from 0 to 2.739" };
                return { result: false, message: "Please enter correct value. A valid value ranges from 0 to maximum" };



            if (activeUnitId == 'BQ2-ST1') {

                var rowdata = $('#divgrid').jqxGrid('getrowdata', cell.row);
                var OutageSelection = rowdata['IsOutageDeration'];
                if (OutageSelection == 'O' && value != 0)
                    return { result: false, message: "Engine, when under Outage, cannot have this dispatch value" };
                if (OutageSelection != 'O' && value == 0)
                    return { result: false, message: "Press ESC key and then select Outage from Outage/Deration dropdown" };

            }

  





            //set previous | current MW segregation..need to update after sucessful UAT..
            var row = $('#divgrid').jqxGrid('getrowdata', cell.row);
            var b = isPreviousCurrentMWSegregationSameDataValidator(cell.row,
                        ((activeUnitId == "BQ2-ST1") ? "StartupRampId" : (activeUnitId.substring(0, activeUnitId.indexOf("-")) == "BQ2") ? "StartShut" : ""), value);

            if (b == true) {
                //SetPreviousCurrentMWSugregationRequirementDataValidator(cell.row, "StartupRampId", value);
                SetPreviousCurrentMWSugregationRequirementDataValidator(cell.row,
                        ((activeUnitId == "BQ2-ST1") ? "StartupRampId" : (activeUnitId.substring(0, activeUnitId.indexOf("-")) == "BQ2") ? "StartShut" : ""), value);
            }




        }
    }


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


    if ((datafield == 'StartupRampId') || (datafield == 'StartShut')) {
        var b = isPreviousCurrentMWSegregationSame(row, datafield);
        if (b == true) {
            // alert('unable to change the Startup Ramp value');
            SetPreviousCurrentMWSugregationRequirement(row, datafield);
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










//implementing requirement 2 changes
var fncellvaluechanged = function (event) {
    if ((AuthId == DEOAUTHID) && (activeUnitId == 'BQ2-ST1')) {

        //var ODfields = ['IsOutageDeration', 'IsForced', 'IsPlanned', 'IsLDC', 'IsGas'];
        var ODfields = ['IsOutageDeration'];

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

        var idODfields = jQuery.inArray(datafield, ODfields);
        if (idODfields != -1) {
            var targetfname = "ActualDispatchMW";
            ODCellvaluechangeHandler(newvalue, rowBoundIndex, targetfname);
        }

                //set previous | current MW segregation..need to update after sucessful UAT..
                var row = $('#divgrid').jqxGrid('getrowdata', args.rowindex);
                var b = isPreviousCurrentMWSegregationSame(args.rowindex, "StartupRampId");
                if (b == true) {
                    // alert('unable to change the Startup Ramp value');
                    SetPreviousCurrentMWSugregationRequirement(args.rowindex, "StartupRampId");
                    //return false;
                }



    }

}

function ODCellvaluechangeHandler(newvalue, rowBoundIndex, targetfname) {
    if ((newvalue != 'N') && (newvalue != 'D')) {
        $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, targetfname, 0);
        //interlockoutagefieldlist(newvalue, rowBoundIndex, newvalue);
    }
    else {
        //interlockoutagefieldlist(newvalue, rowBoundIndex, newvalue);
    }
}


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






//Implementing Requirement 2 Changes.. 
function SetPreviousCurrentMWSugregationRequirement(row,fldName) {

    //When the previous and current MWs are same, user should not be able to select the ramp option 
    //   if ((AuthId == DEOAUTHID) && (activeUnitId == 'BQ2-ST1')) {
    if ((AuthId == DEOAUTHID)) {

        if (row != 0) {
            var rowdataPrevious = $('#divgrid').jqxGrid('getrowdata', (row - 1));
            var rowdataCurrent = $('#divgrid').jqxGrid('getrowdata', (row));

            var PMWV = rowdataPrevious['ActualDispatchMW'];
            var CMWV = rowdataCurrent['ActualDispatchMW'];
            var StartupRampId = rowdataPrevious[fldName];
            if (PMWV == CMWV) {
                try {
                    // $("#divgrid").jqxGrid('setcellvalue', (row - 1), "StartupRampId", StartupRampId);
                    //$("#divgrid").jqxGrid('setcellvalue', (row), "StartupRampId", 'N');

                    $("#divgrid").jqxGrid('setcellvalue', (row), fldName, 'N');

                } catch (e) {

                }

            }


        }

        setTimeout(function () { $('#divgrid').jqxGrid('updatebounddata', 'cells'); }, 10);


    }

   

}

function isPreviousCurrentMWSegregationSame(row,fldName) {
    //if ((AuthId == DEOAUTHID) && (activeUnitId == 'BQ2-ST1')) {
    if ((AuthId == DEOAUTHID)) {


        if (row != 0) {
            var rowdataPrevious = $('#divgrid').jqxGrid('getrowdata', (row - 1));
            var rowdataCurrent = $('#divgrid').jqxGrid('getrowdata', (row));

            var PMWV = rowdataPrevious['ActualDispatchMW'];
            var CMWV = rowdataCurrent['ActualDispatchMW'];
            var StartupRampId = rowdataPrevious[fldName];
            if (PMWV == CMWV) {
                return true;
            }
            else {
                return false;
            }


        }

        return false;


    }
    
    return false;
}




function isPreviousCurrentMWSegregationSameDataValidator(row, fldName,newValue) {
    if ((AuthId == DEOAUTHID)) {
        if (row != 0) {
            var rowdataPrevious = $('#divgrid').jqxGrid('getrowdata', (row - 1));
            var rowdataCurrent = $('#divgrid').jqxGrid('getrowdata', (row));

            var PMWV = rowdataPrevious['ActualDispatchMW'];
            var CMWV = rowdataCurrent['ActualDispatchMW'];
            var CMWV_ = newValue;

            var StartupRampId = rowdataPrevious[fldName];
            if (PMWV == CMWV_) {
                return true;
            }
            else {
                return false;
            }


        }

        return false;


    }

    return false;
}


function SetPreviousCurrentMWSugregationRequirementDataValidator(row, fldName, newValue) {

    //When the previous and current MWs are same, user should not be able to select the ramp option 
    //   if ((AuthId == DEOAUTHID) && (activeUnitId == 'BQ2-ST1')) {
    if ((AuthId == DEOAUTHID)) {

        if (row != 0) {
            var rowdataPrevious = $('#divgrid').jqxGrid('getrowdata', (row - 1));
            var rowdataCurrent = $('#divgrid').jqxGrid('getrowdata', (row));

            var PMWV = rowdataPrevious['ActualDispatchMW'];
            var CMWV = rowdataCurrent['ActualDispatchMW'];
            var CMWV_ = newValue;
            var StartupRampId = rowdataPrevious[fldName];
            if (PMWV == CMWV_) {
                try {
                    // $("#divgrid").jqxGrid('setcellvalue', (row - 1), "StartupRampId", StartupRampId);
                    //$("#divgrid").jqxGrid('setcellvalue', (row), "StartupRampId", 'N');

                    $("#divgrid").jqxGrid('setcellvalue', (row), fldName, 'N');

                } catch (e) {

                }

            }


        }

        setTimeout(function () { $('#divgrid').jqxGrid('updatebounddata', 'cells'); }, 10);


    }



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

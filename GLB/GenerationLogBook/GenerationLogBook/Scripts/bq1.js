var DEOAUTHID = 'ELOS-BQ1-DEO';
var L10AUTHID = 'ELOS-BQ1-L10';
var L20AUTHID = 'ELOS-BQ1-L20';
var PWRAUTHID = 'ELOS-BQ1-PWR';

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

}
/*
function createnewledgerentry(datetime, ledgerparams) {
    return { RdgDateTimeStr: datetime, SiteId: ledgerparams.SiteId, UnitId: ledgerparams.UnitId, ReferenceCapacity: ledgerparams.ReferenceCapacity, GDC: ledgerparams.GDC, ActualDispatchMW: 0, AmbTemp: 0, SeaWaterTemp: 0, AmbPressure: 0, IsOutageDeration: 'N', IsForced: 'N', IsPlanned: 'N', IsLDC: 'N', IsGas: 'N', StartupRampId: 'N', Reason: '', AmbDeration: 0, ForcedDeration: 0, PlannedDeration: 0, L1Approvalbool: false, L1ApprovalCopy: false, L2Approvalbool: false, L2ApprovalCopy: false, CreateDateStr: '' };
}
*/
function pastevaluestorow(refrecord, rowidx) {
    $('#divgrid').jqxGrid('setcellvalue', rowidx, 'SiteId', refrecord.SiteId);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, 'UnitId', refrecord.UnitId);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ReferenceCapacity', refrecord.ReferenceCapacity);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, 'GDC', refrecord.GDC);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ActualDispatchMW', refrecord.ActualDispatchMW);
    /*$('#divgrid').jqxGrid('setcellvalue', rowidx, 'AmbTemp', refrecord.AmbTemp);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, 'SeaWaterTemp', refrecord.SeaWaterTemp);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, 'AmbPressure', refrecord.AmbPressure);*/
    $('#divgrid').jqxGrid('setcellvalue', rowidx, 'Reason', refrecord.Reason);
    //$('#divgrid').jqxGrid('setcellvalue', rowidx, 'AmbDeration', refrecord.AmbDeration);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, 'ForcedDeration', refrecord.ForcedDeration);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, 'PlannedDeration', refrecord.PlannedDeration);

    $('#divgrid').jqxGrid('setcellvalue', rowidx, "IsOutageDeration", refrecord.IsOutageDeration);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, "IsForced", refrecord.IsForced);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, "IsPlanned", refrecord.IsPlanned);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, "IsLDC", refrecord.IsLDC);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, "IsGas", refrecord.IsGas);
    $('#divgrid').jqxGrid('setcellvalue', rowidx, "StartupRampId", refrecord.StartupRampId);

    ////$('#divgrid').jqxGrid('updatebounddata');
    //////$('#divgrid').jqxGrid('selectrow', rowidx);
    ////$('#divgrid').jqxGrid('sortby', 'RdgDateTimeStr', 'asc');
    //////$("#divheader").click();

    $('#divgrid').jqxGrid('updatebounddata');
    $('#divgrid').jqxGrid('selectrow', rowidx);
    var id = $('#divgrid').jqxGrid('getrowid', rowidx);
    console.log('Row ID: ' + id);

    setTimeout(function () { $('#divgrid').jqxGrid('sortby', 'RdgDateTimeStr', 'asc'); $('#divgrid').jqxGrid('ensurerowvisible', refrecord.focusidx); console.log('Passed Param: ' + refrecord.focusidx); }, 10, refrecord);
}

function InitializeForm() {
    urlFetchLedger = '/BQI/FetchLossLedger';
    urlPostLedger = '/BQI/PostChanges';
    urlFetchReport = '/ReportCore/getELRHourlyForUnit';

    gridColumns =
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
                 editable: true,
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
                 hidden: true,
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
            /*{
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
                text: 'SeaWaterTemp',
                datafield: 'SeaWaterTemp',
                width: 60,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">Sea Water<br>Temp<br>(Deg. C)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f2',
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },
            {
                text: 'AmbPressure',
                datafield: 'AmbPressure',
                hidden: true,
                width: 55,
                renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                    return '<div style="text-align:center;margin: 3px 0 0 3px;">Ambient<br>Pressure<br>(mBar)</div>';
                },
                columntype: 'numberinput',
                cellsformat: 'f2',
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit
            },*/
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
                cellbeginedit: fncellbeginedit,
                //editable: false

            },
            {
                text: 'Planned',
                datafield: 'IsPlanned',
                displayfield: 'TXTPlanned',
                columntype: 'dropdownlist',
                width: 60,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.IsPlanned, displayMember: "Value", valueMember: "Key" }) },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                //editable: false
            },
            {
                text: 'LDC',
                datafield: 'IsLDC',
                displayfield: 'TXTLDC',
                columntype: 'dropdownlist',
                width: 50,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.IsLDC, displayMember: "Value", valueMember: "Key" }) },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                //editable: false

            },
            {
                text: 'Gas',
                datafield: 'IsGas',
                displayfield: 'TXTGas',
                columntype: 'dropdownlist',
                width: 50,
                createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.IsGas, displayMember: "Value", valueMember: "Key" }) },
                cellclassname: fncellclass,
                cellbeginedit: fncellbeginedit,
                //editable: false
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
             /*{
                 text: 'Deration due to Ambient',
                 datafield: 'AmbDeration',
                 width: 60,
                 renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
                     return '<div style="text-align:center;margin: 3px 0 0 3px;">Ambient<br>Deration<br>(MW)</div>';
                 },
                 columntype: 'numberinput',
                 cellsformat: 'f2',
                 cellclassname: fncellclass,
                 cellbeginedit: fncellbeginedit
             },*/
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
            //    width: 200,
            //    cellclassname: fncellclass,
            //    cellbeginedit: fncellbeginedit,
            //    initeditor: function (row, column, editor) {
            //        editor.attr('maxlength', 300);
            //    }
            //},



            //   //implementing requirement 2 changes
            //{
            //    text: 'Reason',
            //    datafield: 'Reason',
            //    displayfield: 'TXReason',
            //    columntype: 'dropdownlist',
            //    width: 200,
            //    createeditor: function (row, value, editor) { editor.jqxDropDownList({ autoDropDownHeight: true, source: LOVAdaptors.Reason, displayMember: "Value", valueMember: "Key" }) },
            //    renderer: function (defaultText, alignment, height) { //Required to displat Multiline Header
            //        return '<div style="text-align:center;margin: 3px 0 0 3px;">Reason</div>';
            //    },
            //    cellclassname: fncellclass,
            //    cellbeginedit: fncellbeginedit
            //}
               //implementing requirement 2 changes



                    {
                                    text: 'Reason',
                                    datafield: 'Reason',
                                    columntype: 'template',
                                    width: 600,
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



        ];
    createLOVAdaptors();
    InitializeELossTmplate();


     data = generatedata();


}

function createLOVAdaptors() {

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



    //implementing requirement 2 changes
//    LOVAdaptors.Reason = new $.jqx.dataAdapter({
//        localdata: LOVReason,
//        datatype: "array",
//        datafields: [{ name: 'Key', type: 'string' },
//                     { name: 'Value', type: 'string' }]
//    },
//{ autoBind: true });

    //implementing requirement 2 changes


}

function bindDataGrid(data) {
    ledger = new $.jqx.observableArray(data, function (changed) {
        //logic to keep track which record has been updated/added by the user
        ledger[changed.index].ctrack = "Y";
    });

    var dsledger = {
        localdata: ledger,
        datatype: "obserableArray",
        datafields: [{ name: 'SiteId', type: 'string' },
                     { name: 'UnitId', type: 'string' },
                     //{ name: 'RdgDateTime', type: 'date' },
                     { name: 'RdgDateTimeStr', type: 'date' },
                     { name: 'ActualDispatchMW', type: 'number' },
                     { name: 'AmbTemp', type: 'float' },
                     { name: 'SeaWaterTemp', type: 'float' },
                     { name: 'AmbPressure', type: 'float' },
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

                     //implementing requirement 2 changes
                     //    { name: 'Reason', type: 'string' },
                     //{ name: 'TXReason', value: 'Reason', values: { source: LOVAdaptors.Reason.records, value: 'Key', name: 'Value' } },
                       //implementing requirement 2 changes





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



var setVisibility = function (datafield) {
    return false;
}

var fncellbeginedit = function (row, datafield, columntype, value) {
    var data = $('#divgrid').jqxGrid('getrowdata', row);
    switch (AuthId) {
        case L10AUTHID:
            if (datafield == 'L1Approvalbool' && data.L1ApprovalCopy == true) {
                alert('This record has been authorized earlier and no longer available for editing. If you need to edit this record, please contact to Performance team for unlocking the record');
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
        if (b==true) {
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



//IMPLEMENTING REQUIREMENT 2 CHANGES..

var dataValidator = function (cell, value) {
    //return true;


    //implementing r2 changes..
    if (cell.datafield.substring(2) == 'ActualDispatchMW') {

        // if (value < 0 || value > 2.739)
        if (value < 0)
            //return { result: false, message: "Please enter correct value. A valid value ranges from 0 to 2.739" };

            return { result: false, message: "Please enter correct value. A valid value ranges from 0 to maximum" };



        var rowdata = $('#divgrid').jqxGrid('getrowdata', cell.row);
        var OutageSelection = rowdata[cell.datafield.substring(0, 2) + 'Outage'];
        //console.log(OutageSelection);

        if (OutageSelection != 'N' && value != 0)
            return { result: false, message: "Engine, when under Outage, cannot have this dispatch value" };

        if (OutageSelection == 'N' && value == 0)
            return { result: false, message: "Press ESC key and then select Outage reason from " + cell.datafield.substring(0, 2) + "Outage Dropdown" };
    }
    else if (cell.datafield == 'ActualDispatchMW') {

        //if (value < 0 || value > 2.739)
            if (value < 0)
                //return { result: false, message: "Please enter correct value. A valid value ranges from 0 to 2.739" };
                return { result: false, message: "Please enter correct value. A valid value ranges from 0 to maximum" };


         var rowdata = $('#divgrid').jqxGrid('getrowdata', cell.row);
        var OutageSelection = rowdata['IsOutageDeration'];
        if (OutageSelection == 'O' && value != 0)
            return { result: false, message: "Engine, when under Outage, cannot have this dispatch value" };
        if (OutageSelection != 'O' && value == 0)
            return { result: false, message: "Press ESC key and then select Outage from Outage/Deration dropdown" };

                            
        
        //set previous | current MW segregation..need to update after sucessful UAT..
            var row = $('#divgrid').jqxGrid('getrowdata', cell.row);
            var b = isPreviousCurrentMWSegregationSameDataValidator(cell.row,value);
            if (b == true) {
                // alert('unable to change the Startup Ramp value');
                SetPreviousCurrentMWSugregationRequiremenDataValidatort(cell.row,value);
                //return false;
            }
        



    }

    return true;




}





var fncellvaluechanged = function (event) {
   // var ODfields = ['IsOutageDeration', 'IsForced', 'IsPlanned', 'IsLDC', 'IsGas'];

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

function ODCellvaluechangeHandler(newvalue, rowBoundIndex, targetfname) {
    if ((newvalue != 'N') && (newvalue != 'D')) {
        $("#divgrid").jqxGrid('setcellvalue', rowBoundIndex, targetfname, 0);
        //interlockoutagefieldlist(newvalue, rowBoundIndex, newvalue);
    }
    else{
        //interlockoutagefieldlist(newvalue, rowBoundIndex, newvalue);
    }

   

}

//implementing requirement 2 changes..
//implementing requirement 2 cganges..
function interlockoutagefieldlist(newvalue, rowBoundIndex , key) {

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


//Implementing Requiremnt 2 changes..
function SetPreviousCurrentMWSugregationRequirement(row)
{

    //When the previous and current MWs are same, user should not be able to select the ramp option 
    if (row != 0) {
            var rowdataPrevious = $('#divgrid').jqxGrid('getrowdata', (row-1));
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

function isPreviousCurrentMWSegregationSame(row)
{
    if (row != 0) {
        var rowdataPrevious = $('#divgrid').jqxGrid('getrowdata', (row - 1));
        var rowdataCurrent = $('#divgrid').jqxGrid('getrowdata', (row));

        var PMWV = rowdataPrevious['ActualDispatchMW'];
        var CMWV = rowdataCurrent['ActualDispatchMW'];
        var StartupRampId = rowdataPrevious['StartupRampId'];
        if (PMWV == CMWV) {
            return true;
        }
        else{
            return false;
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
function generatedata()
{
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
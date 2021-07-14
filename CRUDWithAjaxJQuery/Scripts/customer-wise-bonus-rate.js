$(document).ready(function () {
    loadCustomerWiseBonusRateData();
    $("#tblCustomerSearchID").autocomplete({
        source: function (req, res) {
            $.ajax({
                url: "/CustomerWiseBonusRate/AutoCompleteSerach/" + req.term,
                type: "GET",
                dataType: "JSON",
                success: function (data) {
                    res($.map(data, function (item) {
                        return { level: item.CustomerID, value: item.CustomerID };
                    }))
                }
            })
        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    })
});
//Load Customer Data with bonus info  
function loadCustomerWiseBonusRateData() {
    $.ajax({
        url: "/CustomerWiseBonusRate/GetCustomeWiseBonusRateList",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, data) {
                html += '<tr>';
                html += '<td hidden>' + data.ID + '</td>';
                html += '<td>' + data.CustomerID + '</td>';
                html += '<td>' + data.Name + '</td>';
                html += '<td>' + data.BonusRate + '</td>';
                html += '<td>' + data.BonusDate + '</td>';
                html += '<td><a href="#" data-toggle="modal" data-target="#EditCustomerwiseBonusRateModal" onclick="return getbyID(' + data.ID + ')"><i class="fa fa-pencil"></i></a></td>';
                html += '</tr>';
                //console.log(data);
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Load Customer Data with bonus info  
function addCustomerWiseBonusRate() {
    var customerId = $('#tblCustomerSearchID').val().trim();
    $.ajax({
        url: "/CustomerWiseBonusRate/GetCustomerWiseForBonusRateByCustomerID/" + customerId,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            var html = '';          
                //console.log(data);
            html += '<tr>';
            html += '<td id="tblColumnIdForBonusAdd" hidden>' + data.ID + '</td>';
            html += '<td>' + data.CustomerID + '</td>';
            html += '<td>' + data.Name + '</td>';
            html += '<td><input type="number" class="form-control" id="tblBonusRate" placeholder="Bonus Rate"/> </td>';
            html += '<td><input type="date" class="form-control" id="tblBonusDate" placeholder="Bonus Date"/> </td>';
            html += '<td><button btn btn-primary href="#" onclick="return saveCustomerWiseBonusRate(' + data.ID + ')">Save</button></td>';
            html += '</tr>';
            $("table tbody").append(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for updating customer data
function saveCustomerWiseBonusRate(id) {
    var res = validate();
    if (res == false) {
        return false;
    }
    var customer = {
        //ID: $('#tblColumnIdForBonusAdd').val(),
        ID: id,
        BonusRate: $('#tblBonusRate').val(),
        BonusDate: $('#tblBonusDate').val()
    };
    $.ajax({
        url: "/CustomerWiseBonusRate/AddCustomerWiseBonusRate",
        data: JSON.stringify(customer),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //loadCustomerWiseBonusRateData();
            //$('#tblCustomerSearchID').val("");

            if (result.IsUpdate == true) {
                loadCustomerWiseBonusRateData();
                $('#tblCustomerSearchID').val("");
            }
            alert(result.Message);
            console.log(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for updating customer data
function updateCustomerWiseBonusRate() {
    var customer = {
        ID: $('#tblColumnId').val(),
        BonusRate: $('#tblBonusRate').val(),
        BonusDate: $('#tblBonusDate').val()
    };
    $.ajax({
        url: "/CustomerWiseBonusRate/AddCustomerWiseBonusRate",
        data: JSON.stringify(customer),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadCustomerWiseBonusRateData();
            alert(result.Message);
            console.log(result);
            $('#tblCustomerSearchID').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//function for updating customer wise bonus rate data
function getbyID(ID) {
    $.ajax({
        url: "/CustomerWiseBonusRate/GetCustomerWiseBonusRateByCustomerIDforUpdate/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (res) {
            $('#tblColumnId').val(res.ID);
            $('#tblBonusRate').val(res.BonusRate);
            $('#tblBonusDate').val(res.BonusDate);
            console.log(res);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
} 
//Input Valdidation Method
function validate() {
    var isValid = true;
    if ($('#tblBonusRate').val().trim() == "" || $('#tblBonusRate').val().trim() == 0 || $('#tblBonusRate').val().trim() == null || $('#tblBonusRate').val().trim() == undefined) {
        $('#tblBonusRate').css('border-color', 'Red');
        //$('#tblCustomerID').before("Please Enter Customer ID");
        isValid = false;
    }
    else {
        $('#tblBonusRate').css('border-color', 'lightgrey');
    }
    if ($('#tblBonusDate').val().trim() == "" || $('#tblBonusDate').val().trim() == null || $('#tblBonusDate').val().trim() == undefined) {
        $('#tblBonusDate').css('border-color', 'Red');
        //$('#tblCustomerName').before("Please Enter Customer Name");
        isValid = false;
    }
    else {
        $('#tblBonusDate').css('border-color', 'lightgrey');
    }
    return isValid;
}  
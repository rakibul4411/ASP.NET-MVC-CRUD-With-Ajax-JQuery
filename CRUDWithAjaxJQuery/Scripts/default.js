//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Customer Data function  
function loadData() {
    $.ajax({
        url: "/CustomerInfo/GetCustomeList",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, data) {
                html += '<tr>';
                html += '<td>' + data.ID + '</td>';
                html += '<td>' + data.CustomerID + '</td>';
                html += '<td>' + data.Name + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + data.ID + ')"><i class="fa fa-pencil"></i></a> | <a href="#" onclick="Delele(' + data.ID + ')"><i class="fa fa-trash-o"></i></a></td>';
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

//Add Customer data   
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var customer = {
        ID: $('#tblColumnId').val(),
        CustomerID: $('#tblCustomerID').val(),
        Name: $('#tblCustomerName').val()
    };
    $.ajax({
        url: "/CustomerInfo/AddCustomer",
        data: JSON.stringify(customer),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            clearInputField();
            alert(result.Message);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
} 

//Get customer data by ID  
function getbyID(ID) {
    $.ajax({
        url: "/CustomerInfo/GetCustomerByID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (res) {
            $('#tblColumnId').val(res.ID);
            $('#tblCustomerID').val(res.CustomerID);
            $('#tblCustomerName').val(res.Name);
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            console.log(res);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
} 

//function for updating customer data
function Update() {
    //var res = validate();
    //if (res == false) {
    //    return false;
    //}
    var customer = {
        ID: $('#tblColumnId').val(),
        CustomerID: $('#tblCustomerID').val(),
        Name: $('#tblCustomerName').val()
    };
    $.ajax({
        url: "/CustomerInfo/EditCustomer",
        data: JSON.stringify(customer),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            clearInputField();
            $('#btnUpdate').hide();
            $('#btnAdd').show();
            //$('#tblColumnId').val("");
            //$('#tblCustomerID').val("");
            //$('#tblCustomerName').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


//function for deleting customer data
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    var customer = {
        ID: ID
    };

    if (ans) {
        $.ajax({
            url: "/CustomerInfo/DeleteCustomer",
            data: JSON.stringify(customer),
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the input field
function clearInputField() {
    $('#tblColumnId').val("");
    $('#tblCustomerID').val("");
    $('#tblCustomerName').val("");
}

//Function for customer ID checking
//function CheckCustomerID(){
//    var id = $('#tblCustomerID').val();
//    $.ajax({
//        url: "/CustomerInfo/GetCheckCustomerIDUniqueness/" + id,
//        typr: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (res) {
//            console.log(res.Message);
//            $('#tblCustomerID').val("");
//            if (res != null || res != undefined || res != " ")
//            alert(res.Message);
//        },
//        error: function (errormessage) {
            
//        }
//    });
//    return false;
//}

/////////////////////Check Customer ID Uniqueness Using ADO.NET
////Function for customer ID checking
function CheckCustomerID() {
    var id = $('#tblCustomerID').val();
    $.ajax({
        url: "/CustomerInfo/GetCheckCustomerIDUniqueness/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (res) {
            console.log(res.Message);
            if (res.Message != undefined) {
                alert(res.Message);
                $('#tblCustomerID').val("");
            }
        },
        error: function (errormessage) {

        }
    });
    return false;
}
////////////////////

//Input Valdidation Method
function validate() {
    var isValid = true;
    if ($('#tblCustomerID').val().trim() == "" || $('#tblCustomerID').val().trim() == 0) {
        $('#tblCustomerID').css('border-color', 'Red');
        //$('#tblCustomerID').before("Please Enter Customer ID");
        isValid = false;
    }
    else {
        $('#tblCustomerID').css('border-color', 'lightgrey');
    }
    if ($('#tblCustomerName').val().trim() == "") {
        $('#tblCustomerName').css('border-color', 'Red');
        //$('#tblCustomerName').before("Please Enter Customer Name");
        isValid = false;
    }
    else {
        $('#tblCustomerName').css('border-color', 'lightgrey');
    }
    return isValid;
}  

//////////// Transfer Data One Table To Another Table
function transferCustomerData() {
    $.ajax({
        url: "/CustomerInfo/TransferCustomerData",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            alert(result.Message);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


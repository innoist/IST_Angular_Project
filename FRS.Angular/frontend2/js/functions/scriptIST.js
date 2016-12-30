function SendMessage() {
    if (validateContactUsForm()) {
        var serviceUrl = '/Clinic/ContactUs';
        var email = $("#email").val();
        var phone = $("#phone").val();
        var name = $("#name").val();
        var message = $("#message").val();
        var data = { name: name, email: email, phone: phone, message: message };
        $.ajax({
            type: "POST",
            url: serviceUrl,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successSendMessage,
            error: errorSendMessage
        });
    }
}



$(function () {
    $(window).scroll(function () {
        //alert($(this).scrollTop());
        if ($(this).scrollTop() >= 50) {

        }
        //if ($(this).scrollTop() >= aTop) {
        //    alert('header just passed.');
        //    // instead of alert you can use to show your ad
        //    // something like $('#footAd').slideup();
        //}
    });
});

function successSendMessage(data, status) {
    //alert("You have successfully sent Email to Admin!");
    toastr.warning("Thanks for contacting us. We will get back to you shortly");

    resetContactUsForm();
}

function errorSendMessage(e, status) {
}

function validateContactUsForm()
{
    var listSimple = $('.contactus');
    var counter = 0;
    for (var i = 0; i < listSimple.length; i++) {
        var control = listSimple[i];
        var fieldvalue = $(control).val();
        var placeholder = $(control).attr('data-value');
        if (fieldvalue == null || fieldvalue == "" || fieldvalue == placeholder) {
            //$(control).prop('placeholder', $(control).prop('placeholder'));
            counter++;
            $(control).addClass("Error");
            //implementing scroll back to error
        }
        else {
            $(control).removeClass("Error");
        }
    }

    if (counter > 0) {
        //implementing focus back to error
        var divID = $(".Error")[0].id;
        if ($("#" + divID).length > 0)
            $("#" + divID).focus();
        return false;
    }
    return true;
}

function SaveAppointment() {
    if (validateAppointmentForm()) {
        var serviceUrl = '/Clinic/SaveAppointment';
        var email = $("#EmailAdress").val();
        var phone = $("#MobileNumber").val();
        var age = $("#Age").val();
        var nationality = $("#Nationality").val();
        var name = $("#FullName").val();
        var date = $("#datepicker").val();
        var data = { NameA: name, NameE: name, Email: email, Mobile: phone, Age: age, Nationality: nationality, AppointmentDate: date };
        $.ajax({
            type: "POST",
            url: serviceUrl,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successSaveAppointment,
            error: errorSaveAppointment
        });
    }
}

function successSaveAppointment(data, status) {
    //alert("You have successfully booked an appointment!");
    toastr.success("You have successfully booked an appointment");
    resetAppointmentForm();
}

function errorSaveAppointment(e, status) {
}

function validateEmail(email) {
    var oEmail = $(email);
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (!emailReg.test(oEmail.val())) {
        oEmail.val("");
        oEmail.css('border-color', 'red');
        oEmail.attr("placeholder", "Enter valid email.");
        
        return false;
    } else {
        return true;
    }
}

function validateAppointmentForm() {
    var listSimple = $('.appointment');
    var counter = 0;
    for (var i = 0; i < listSimple.length; i++) {
        var control = listSimple[i];
        var fieldvalue = $(control).val();
        var placeholder = $(control).attr('data-value');
        if (fieldvalue == null || fieldvalue == "" || fieldvalue == placeholder) {
            //$(control).prop('placeholder', $(control).prop('placeholder'));
            counter++;
            $(control).addClass("Error");
            //implementing scroll back to error
        }
        else {
            $(control).removeClass("Error");
        }
    }

    if (counter > 0) {
        //implementing focus back to error
        var divID = $(".Error")[0].id;
        if ($("#" + divID).length > 0)
            $("#" + divID).focus();
        return false;
    }
    //return validateDate();
    return true;
} 

function validateDate() {
    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var start = d.getFullYear() + '-' +
        (('' + month).length < 2 ? '0' : '') + month + '-' +
        (('' + day).length < 2 ? '0' : '') + day;
    
    var end = $('#datepicker').val();
    if (new Date(start) < new Date(end)) {
        $('#datepicker').removeClass("Error");
        return true;
    } else {
        $('#datepicker').addClass("Error");
        return false;
    }
}

function resetAppointmentForm()
{
    $(':input', '#contact')
        .not(':button, :submit, :reset, :hidden')
        .val('');

    $("#FullName").attr("placeholder", "الاسم");
    $("#EmailAdress").attr("placeholder", "البريد الالكتروني");
    $("#MobileNumber").attr("placeholder", "رقم الجوال");
    $("#Age").attr("placeholder", "العمر");
    $("#Nationality").attr("placeholder", "الجنسية");
    $("#datepicker").attr("placeholder", "التاريخ");
}

function resetContactUsForm() {
    $(':input', '#contactus')
        .not(':button, :submit, :reset, :hidden')
        .val('');

    $("#name").attr("placeholder", "الاسم");
    $("#email").attr("placeholder", "البريد الالكتروني");
    $("#phone").attr("placeholder", "رقم التواصل");
    $("#message").attr("placeholder", "الرسالة");
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    var str = this.valueOf();
    return true;
}

function ConvertDates(dateTobeChanged, fromCalender, toCalender) {
    var calender = $.calendars.instance(fromCalender);
    var dateToBeChanged = calender.parseDate("dd/mm/yyyy", dateTobeChanged);
    calender = $.calendars.instance(toCalender);
    var changedDate = calender.fromJD(dateToBeChanged.toJD());
    return calender.formatDate("dd/mm/yyyy", changedDate);
}
function HijriToGregorian(arabicCalendar, englishCalendar) {
    if ($(arabicCalendar).val() == "") {
        $(englishCalendar).val("");
    }
    else {
        var splittedDate = $(arabicCalendar).val().split('/');
        $(arabicCalendar).val(splittedDate[2] + '/' + splittedDate[1] + '/' + splittedDate[0]);
        var dateToBeChanged = $(arabicCalendar).val();
        var newDate = ConvertDates(dateToBeChanged, 'islamic', 'gregorian');
        $(englishCalendar).val(newDate);
    }
}
function GregorianToHijri(englishCalendar,arabicCalendar) {
    if ($(englishCalendar).val() == "") {
        $(arabicCalendar).val("");
    }
    else {
        var dateToBeChanged = $(englishCalendar).val();
        var newDate = ConvertDates(dateToBeChanged, 'gregorian', 'islamic');
        $(arabicCalendar).val(newDate);
    }
}

function isDecimalValue(event) {
    //8=backspace, 9=tab, 110 & 190 = .
    if (event.shiftKey == true) {
        event.preventDefault();
    }

    if ((event.keyCode >= 48 && event.keyCode <= 57) ||
        (event.keyCode >= 96 && event.keyCode <= 105) ||
        event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
        event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 110 || event.keyCode == 190) {

    } else {
        event.preventDefault();
    }

    if ($(this).val().indexOf('.') !== -1 && (event.keyCode == 110 || event.keyCode == 190))
        event.preventDefault();
    //if a decimal has been added, disable the "."-button
    if (event.keyCode == 9)
        return true;
}

function isNumberValue(event) {
    //8=backspace, 9=tab, 110 & 190 = .
    if (event.shiftKey == true) {
        event.preventDefault();
    }

    if ((event.keyCode >= 48 && event.keyCode <= 57) ||
        (event.keyCode >= 96 && event.keyCode <= 105) ||
        event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
        event.keyCode == 39 || event.keyCode == 46) {

    } else {
        event.preventDefault();
    }
    if (event.keyCode == 9)
        return true;
}

function RestrictNumberOnlyFields() {
    $(function () {
        return $(".DecimalValue").keydown(isDecimalValue);
    });
    $(function () {
        return $(".NumberValue").keydown(isNumberValue);
    });
}

$(document).ready(function () {
    $(".datepicker").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-M-yy",
    });
    $(".datepickerMinDate").datepicker({
        changeMonth: true,
        changeYear: true,
        minDate: new Date(),
        dateFormat: "dd-M-yy",
        beforeShowDay: $.datepicker.noWeekends,
        firstDay: 1,
        numberOfMonths: 1
    });
    $(".datepickerDefaultDate").datepicker({
        changeMonth: true,
        changeYear: true,
        maxDate: new Date()
    });
    $(".datepickerDefaultDate").datepicker().datepicker("setDate", new Date());
    $(".datepickerGregorian").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        yearRange: "1950:2050"
    });

    $(".datepickerFilterMonth").datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        dateFormat: 'MM yy',

        onClose: function(dateText, inst) {
            var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            $(this).datepicker('setDate', new Date(year, month, 1));
        },
    });//.datepicker("setDate", new Date());


    toastr.options = {
        positionClass: 'toast-bottom-left'
    }

    RestrictNumberOnlyFields();
});





$(document).ready(function () {
   

    $('body').on('click', '#btnSearch', function () {
       // alert($('#txtAppointment').val());
       // var model = { 'bloodgroup': $('#txtSearchBlood').val(), 'appointmentdate': $('#txtAppointment').val() };
        $.ajax({
            url: '/DiabeticDiagnostic/AllPatientDetails',
            type: 'POST',
           // datatype: "json",
           // contenttype: 'application/json; charset=utf-8',
            data: { 'bloodgroup': $('#txtSearchBlood').val(), 'appointmentdate': $('#txtAppointment').val() },
            async: true,
            success: function (data) {
                $("#tblAllPatients").html(data);
            },
            error: function (xhr, errorText, status) {
                console.log(xhr.responseText);
            }
        });

    });

  

    
});
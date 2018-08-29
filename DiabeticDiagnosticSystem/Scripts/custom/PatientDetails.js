$(document).ready(function () {

    $('#txtAppointment').datepicker();

    $('#tblAllPatients').DataTable({ searching: true, paging: false, info: false });

   

    $('body').on('click', '#btnSearch', function () {
      
        $.ajax({
            url: '/DiabeticDiagnostic/AllPatientDetails',
            type: 'GET',
            datatype: "json",
           // contenttype: 'application/json; charset=utf-8',
            data: { 'bloodgroup': $('#BloodGroup :selected').text(), 'appointmentdate': $('#txtAppointment').val() },
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
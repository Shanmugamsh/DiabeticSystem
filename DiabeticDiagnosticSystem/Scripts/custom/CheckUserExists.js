$(document).ready(function () {

    $('body').on('click', '#CheckUser', function () {

        var name = $('#Name').val();

        if (name == '' || name == "") {
            $('#msgUserExists').text('Please enter name');
            $('#msgUserExists').css('color', 'red');
            $('#msgUserExists').css('display', 'block');
        }
        else {
            $.ajax({
                url: '/DiabeticDiagnostic/CheckUserNameExists',
                type: 'GET',
                data: { 'username': $('#Name').val() },
                success: function (data) {
                    //alert(data);
                    if (data == '' || data == "") {
                        $('#btnRegister').attr('disabled', false);
                        $('#msgUserExists').css('display', 'none');
                        $('#Name').attr('disabled', true);
                        //$('#msgUserExists').addClass('glyphicon glyphicon-ok');
                        //$('#msgUserExists').removeClass('glyphicon glyphicon-remove');

                    }
                    else {
                        $('#btnRegister').attr('disabled', true);
                        $('#msgUserExists').text('Try different user name');
                        $('#msgUserExists').css('color', 'red');
                        $('#msgUserExists').css('display', 'block');
                        $('#Name').attr('disabled', false);

                        //$('#msgUserExists').addClass('glyphicon glyphicon-remove');
                        //$('#msgUserExists').removeClass('glyphicon glyphicon-ok');
                    }
                },
                error: function (xhr, errorText, status) { console.log(xhr.responseText); }
            });
        }

       
    });
});
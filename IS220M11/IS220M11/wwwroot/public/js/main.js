$(document).ready(function(){  
    $("#username").keyup(function(){
        var User = $(this).val();
        $.post("Ajax/CheckNewUser",{user:User}, function(data){
            $("#messageUser").html(data);
        });
    });
})

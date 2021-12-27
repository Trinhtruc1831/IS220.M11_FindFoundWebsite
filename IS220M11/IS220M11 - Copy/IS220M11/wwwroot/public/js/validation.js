$().ready(function() {
	$("#demoLogin").validate({
		onfocusout: false,
		onkeyup: false,
		onclick: onclick="location.href='userindex.html';",
		rules: {
			"user": {
				required: true,
				maxlength: 15
			},
			"password": {
				required: true,
				minlength: 8
			}
		}
		
	});
});
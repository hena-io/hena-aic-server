
(function ($) {
	"use strict";

	$('form input[name=file]').change(() => {
		var fff = $('form input[name=file]')[0].files[0];
		console.log(fff, fff == null);
	});

	$("form .start").click(() => {
		console.log('aaa');
		var data = new FormData();
		var fff = $('form input[name=file]')[0].files[0];
		console.log(fff);
		console.log($('form').serializeArray());
		data.append('file', $('form input[name=file]')[0].files[0]);
		//data.append('adResourceId', '1111');
		console.log(data);
		HenaApi.adResource.upload(data, (response) => {
			console.log(response);
		});
	});


})(jQuery);
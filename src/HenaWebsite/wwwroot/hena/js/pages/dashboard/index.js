
(function ($) {
	"use strict";

	$("#btn-refresh").click(() => {
		
		var data = {
			adUnitId: '786546874098379820'
			 , clientType: 'Web'
			 , adSystemType: 'Banner'
			 , isLandscape: false
			 , screenWidth: 1280
			 , screenHeight: 720
		};
		HenaApi.pageAd.ready(data, (response) => {
			console.log(response);
			if (response.result == 'Success') {
				var queryString = HenaUtility.toQueryString({ adUnitId: response.data.adUnitId, ai: response.data.ai });
				var linkUrl = response.data.adClickUrl + "?" + queryString;
				var resourceUrl = response.data.resourceUrl + "?" + queryString;
				$("#ad-link").attr("href", linkUrl);
				$("#img-ad").attr('src', resourceUrl);
			}
		});
	});


})(jQuery);
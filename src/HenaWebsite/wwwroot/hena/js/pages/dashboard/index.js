
(function ($) {
	"use strict";


	var lastCampaignId = '-1';

	// 캠페인 생성
	$('#btn-campaign-create').click(() => {
		console.log('create campaign');
		$.ajax({
			url: "/api/campaigns/create",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify({
				name: '테스트',
				type: 'CPC',
				cost: '1000',
				targetValue: '10000',
				beginDate: '2018-10-05 00:00:00',
				endDate: '2018-10-05 00:00:00'
			}),
			success: function (response) {
				console.log('response : ', response);
				if (response.result == "Success") {
					lastCampaignId = response.data.id;
				}
			},
			error: function (error) {
				console.log(error);
			}
		});
	});

	// 캠페인 수정
	$('#btn-campaign-modify').click(() => {
		console.log('modify campaign');
		$.ajax({
			url: "/api/campaigns/modify",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify({
				id: lastCampaignId,
				name: 'Modified Name',
				type: 'CPM',
				cost: '10001',
				targetValue: '100001',
				beginDate: moment.utc().format('YYYY-MM-DD HH:mm:ss'),
				endDate: moment.utc().add(7, 'd').format('YYYY-MM-DD HH:mm:ss')
			}),
			success: function (response) {
				console.log('response : ', response);
			},
			error: function (error) {
				console.log(error);
			}
		});
	});

	// 캠페인 삭제
	$('#btn-campaign-delete').click(() => {
		console.log('delete campaign');
		$.ajax({
			url: "/api/campaigns/delete",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify({
				id: lastCampaignId,
			}),
			success: function (response) {
				console.log('response : ', response);
				if (response.result == "Success") {
					lastCampaignId = '-1';
				}
			},
			error: function (error) {
				console.log(error);
			}
		});
	});

})(jQuery);
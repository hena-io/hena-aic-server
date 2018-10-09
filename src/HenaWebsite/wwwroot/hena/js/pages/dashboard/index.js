(function ($) {
	"use strict";

	var lastCampaignId = '-1';

	var campaignContainer = {

		items: [],
		add: function (newItem) {
			this.items.push(newItem);
		},
		remove: function (campaignId) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.campaignId == campaignId) {
					this.items.splice(idx, 1);
				}
			}
		},
		replace: function (campaignId, newItem) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.campaignId == campaignId) {
					this.items[idx] = newItem;
					break;
				}
			}
		},
		find: function (campaignId) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.campaignId == campaignId)
					return item;
			}
		}
	};

	console.log(campaignContainer.find);

	$("form-campaign").submit((e) => {
		e.stopPropagation();
		e.preventDefault();
	});


	function bindRowClickEvent() {
		$('#table-campaigns tbody tr').click((e) => {

			var target = $(e.currentTarget);
			target.find("input[name=campaigns]");
			var radio = $(e.currentTarget).find('input[name=campaigns]');
			radio.prop('checked', true);
			var campaignId = radio.data("campaign-id");

			setCampaignFormValues(campaignContainer.find(campaignId));
		})
	}

	$('input[type=radio][name=campaigns]').change(function () {
		console.log($(this).data("campaign-id"));

		var campaignId = $(this).data("campaign-id");
		var campaign = campaignContainer.find(campaignId);

		setCampaignFormValues(campaign);
	});

	function setCampaignFormValues(campaign) {
		if (campaign == null) {
			$("#form-campaign")[0].reset();
			return;
		}
		var form = $("#form-campaign");
		for (var it in campaign) {

			var elem = form.find("input[name=" + it + "]");
			if (it == 'beginTime' || it == 'endTime') {
				var localTime = moment.utc(campaign[it]).local().format("YYYY-MM-DDTHH:mm:ss");
				console.log(localTime, campaign[it]);
				elem.val(localTime);
			} else {
				elem.val(campaign[it]);
			}
		}

	}

	// 캠페인 폼 초기화
	$("#btn-campaign-form-reset").click(() => {
		$("#form-campaign")[0].reset();
	});

	// 캠페인 테이블 아이템 모두 삭제
	function clearCampaignTableItems() {
		$("#table-campaigns tbody").empty();
	}

	// 캠페인 테이블 아이템 갱신
	function refreshCampaignTableItems(campaigns) {

		clearCampaignTableItems();

		var tbodyValue = "";
		for (var it in campaigns) {
			var item = campaigns[it];
			tbodyValue += "<tr data-campaign-id='" + item.campaignId + "'>";
			tbodyValue += "	<td><input type='radio' name='campaigns' data-campaign-id='" + item.campaignId + "' /></td>";
			tbodyValue += "	<td>" + item.campaignId + "</td>";
			tbodyValue += "	<td>" + item.name + "</td>";
			tbodyValue += "	<td>" + item.campaignType + "</td>";
			tbodyValue += "	<td>" + item.targetValue + "</td>";
			tbodyValue += "	<td>" + item.cost + "</td>";
			tbodyValue += "	<td>" + moment.utc(item.beginTime).local().format('YYYY-MM-DD HH:mm:ss') + "</td>";
			tbodyValue += "	<td>" + moment.utc(item.endTime).local().format('YYYY-MM-DD HH:mm:ss') + "</td>";
			tbodyValue += "	<td>" + item.isPause + "</td>";
			tbodyValue += "</tr>";
		}

		$("#table-campaigns tbody").append(tbodyValue);

		bindRowClickEvent();
	}

	// 캠페인 목록 조회
	HenaApi.campaigns.list((response) => {
		console.log(response);
		if (response.result === "Success") {
			campaignContainer.items = response.data.campaigns;
			refreshCampaignTableItems(campaignContainer.items);
		}
	});

	// 캠페인 생성
	$('#btn-campaign-create').click(() => {
		var data = $('#form-campaign').serializeObject();
		data.beginTime = moment(data.beginTime).utc().format("YYYY-MM-DDTHH:mm:ss");
		data.endTime = moment(data.endTime).utc().format("YYYY-MM-DDTHH:mm:ss");

		HenaApi.campaigns.create(data, (response) => {
			if (response.result === "Success") {
				campaignContainer.add(response.data);
				refreshCampaignTableItems(campaignContainer.items);
			}
		});
	});

	// 캠페인 수정
	$('#btn-campaign-modify').click(() => {
		var data = $('#form-campaign').serializeObject();
		data.beginTime = moment(data.beginTime).utc().format("YYYY-MM-DDTHH:mm:ss");
		data.endTime = moment(data.endTime).utc().format("YYYY-MM-DDTHH:mm:ss");

		HenaApi.campaigns.modify(data, (response) => {
			if (response.result === "Success") {
				campaignContainer.replace(data.campaignId, response.data);
				refreshCampaignTableItems(campaignContainer.items);
			}
		});
	});

	// 캠페인 삭제
	$('#btn-campaign-delete').click(() => {
		var campaignId = $("#form-campaign input[name=campaignId]").val();
		if (campaignId == "")
			return;

		HenaApi.campaigns.delete({ campaignId: campaignId }, (response) => {
			if (response.result === "Success") {
				campaignContainer.remove(campaignId);
				refreshCampaignTableItems(campaignContainer.items);
			}
		});

	});

	function execute_campaign_delete() {
		$.ajax({
			url: "/api/campaigns/delete",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify({
				campaignId: lastCampaignId
			}),
			success: function (response) {
				console.log('response : ', response);
				if (response.result === "Success") {
					lastCampaignId = '-1';
				}
			},
			error: function (error) {
				console.log(error);
			}
		});
	}

	$('#btn-campaign-list').click(() => {
		execute_campaign_list();
	});

	// 캠페인 목록 조회
	function execute_campaign_list() {
		$.ajax({
			url: "/api/campaigns/list",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: "",
			success: function (response) {
				console.log('response : ', response);
			},
			error: function (error) {
				console.log(error);
			}
		});
	}

})(jQuery);

(function ($) {
	"use strict";

	// -----------------------------------------------------
	// 캠페인 컨테이너
	// -----------------------------------------------------
	var campaignContainer = {

		items: [],
		add: function (newItem) {
			this.items.push(newItem);
		},
		remove: function (campaignId) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.campaignId === campaignId) {
					this.items.splice(idx, 1);
				}
			}
		},
		replace: function (campaignId, newItem) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.campaignId === campaignId) {
					this.items[idx] = newItem;
					break;
				}
			}
		},
		find: function (campaignId) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.campaignId === campaignId)
					return item;
			}
		},
		clear: function () {
			this.items = [];
		}
	};

	// -----------------------------------------------------
	// 광고 디자인 컨테이너
	// -----------------------------------------------------
	var adDesignContainer = {

		items: [],
		add: function (newItem) {
			this.items.push(newItem);
		},
		remove: function (adDesignId) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.adDesignId === adDesignId) {
					this.items.splice(idx, 1);
				}
			}
		},
		replace: function (adDesignId, newItem) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.adDesignId === adDesignId) {
					this.items[idx] = newItem;
					break;
				}
			}
		},
		find: function (adDesignId) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.adDesignId === adDesignId)
					return item;
			}
		},
		clear: function () {
			this.items = [];
		}
	};

	// -----------------------------------------------------
	// 캠페인 관련 함수
	// -----------------------------------------------------

	// 캠페인 목록 갱신
	function refreshCampaigns() {
		HenaApi.campaigns.list((response) => {
			if (response.result === "Success") {
				campaignContainer.items = response.data.campaigns;
				drawCampaignTable(campaignContainer.items);
			}
		});
	}

	// 캠페인 테이블 Row 클릭 이벤트 할당
	function bindCampaignTableRowClickEvent() {
		$('#table-campaigns tbody tr').click((e) => {
			selectCampaignRow(e.currentTarget);
		});
	}

	// 캠페인 Row 선택
	function selectCampaignRow(row) {
		var target = $(row);
		var radio = target.find('input[name=campaigns]');
		radio.prop('checked', true);
		var campaignId = radio.data("campaign-id");
		setCampaignFormValues(campaignContainer.find(campaignId));

		refreshAdDesigns(campaignId);
	}

	// 캠페인 폼에 값 세팅
	function setCampaignFormValues(campaign) {
		if (campaign === null) {
			$("#form-campaign")[0].reset();
			return;
		}
		var form = $("#form-campaign");
		for (var it in campaign) {

			var elem = form.find("input[name=" + it + "]");
			if (it === 'beginTime' || it === 'endTime') {
				var localTime = moment.utc(campaign[it]).local().format("YYYY-MM-DD");
				elem.val(localTime);
			} else {
				elem.val(campaign[it]);
			}
		}
	}

	// 캠페인 테이블 초기화
	function clearCampaignTable() {
		$("#table-campaigns tbody").empty();
	}

	// 캠페인 테이블 그리기
	function drawCampaignTable(campaigns) {

		clearCampaignTable();

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

		bindCampaignTableRowClickEvent();
	}

	// 캠페인 에러 메시지 설정
	function setCampaignsErrorMessage(message) {
		var elem = $("#section-campaigns .alert");
		if (message == null || message == "") {
			elem.prop('hidden', true);
		} else {
			elem.text(message);
			elem.prop('hidden', false);
		}
	}
	// 캠페인 에러 메시지 설정 - Response 기준
	function setCampaignsErrorMessageByResponse(response) {
		if (response == null || response.result == 'Success') {
			setCampaignsErrorMessage(null);
		} else {
			setCampaignsErrorMessage(response.result);
		}
	}


	// -----------------------------------------------------
	// 캠페인 이벤트 처리
	// -----------------------------------------------------
	// 캠페인 폼 submit 이벤트 무시
	$("#form-campaign").submit((e) => {
		e.stopPropagation();
		e.preventDefault();
	});

	// 캠페인 폼 초기화
	$("#btn-campaign-form-reset").click(() => {
		$("#form-campaign")[0].reset();
		setCampaignsErrorMessage(null);
	});

	// 캠페인 생성
	$('#btn-campaign-create').click(() => {
		var data = $('#form-campaign').serializeObject();
		data.beginTime = moment(data.beginTime).utc()
			.hour(0).minute(0).second(0)
			.format("YYYY-MM-DDTHH:mm:ss");

		data.endTime = moment(data.endTime).utc()
			.hour(23).minute(59).second(59)
			.format("YYYY-MM-DDTHH:mm:ss");

		HenaApi.campaigns.create(data, (response) => {
			if (response.result === "Success") {
				campaignContainer.add(response.data);
				drawCampaignTable(campaignContainer.items);
			}
			setCampaignsErrorMessageByResponse(response);
		});
	});

	// 캠페인 수정
	$('#btn-campaign-modify').click(() => {
		var data = $('#form-campaign').serializeObject();
		data.beginTime = moment(data.beginTime).utc()
			.hour(0).minute(0).second(0)
			.format("YYYY-MM-DDTHH:mm:ss");

		data.endTime = moment(data.endTime).utc()
			.hour(23).minute(59).second(59)
			.format("YYYY-MM-DDTHH:mm:ss");

		HenaApi.campaigns.modify(data, (response) => {
			if (response.result === "Success") {
				campaignContainer.replace(data.campaignId, response.data);
				drawCampaignTable(campaignContainer.items);
			}
			setCampaignsErrorMessageByResponse(response);
		});
	});

	// 캠페인 삭제
	$('#btn-campaign-delete').click(() => {
		var campaignId = $("#form-campaign input[name=campaignId]").val();
		if (campaignId === "")
			return;

		HenaApi.campaigns.delete({ campaignId: campaignId }, (response) => {
			if (response.result === "Success") {
				campaignContainer.remove(campaignId);
				drawCampaignTable(campaignContainer.items);
			}
			setCampaignsErrorMessageByResponse(response);
		});
	});

	// -----------------------------------------------------
	// 광고 디자인 관련 함수
	// -----------------------------------------------------
	// 광고디자인 목록 갱신
	function refreshAdDesigns(campaignId) {
		HenaApi.adDesigns.list({ campaignId: campaignId }, (response) => {
			if (response.result === "Success") {
				adDesignContainer.items = response.data.adDesigns;
				drawAdDesignTable(adDesignContainer.items);

				adDesignFormReset();
			}
		});
	}
	// 광고 디자인 테이블 Row 클릭 이벤트 할당
	function bindAdDesignTableRowClickEvent() {
		$('#table-ad-designs tbody tr').click((e) => {
			selectAdDesignRow(e.currentTarget);
		})
	}

	// 광고 디자인 Row 선택
	function selectAdDesignRow(row) {
		var target = $(row);
		var radio = target.find('input[name=ad-designs]');
		radio.prop('checked', true);
		var campaignId = radio.data("ad-design-id");
		var adDesign = adDesignContainer.find(campaignId);
		setAdDesignFormValues(adDesign);
		console.log(adDesign);
		$("#ad-resource-preview").attr('src', adDesign.adResourceUrl);
	}

	// 광고 디자인 폼에 값 세팅
	function setAdDesignFormValues(adDesign) {
		if (adDesign === null) {
			$("#form-ad-design")[0].reset();
			return;
		}
		var form = $("#form-ad-design");
		for (var it in adDesign) {

			var elem = form.find("input[name=" + it + "]");
			if (it === 'createTime' ) {
				var localTime = moment.utc(adDesign[it]).local().format("YYYY-MM-DDTHH:mm:ss");
				elem.val(localTime);
			} else {
				elem.val(adDesign[it]);
			}
		}
	}

	// 광고 디자인 테이블 초기화
	function clearAdDesignTable() {
		$("#table-ad-designs tbody").empty();
	}

	// 광고 디자인 테이블 그리기
	function drawAdDesignTable(adDesigns) {

		clearAdDesignTable();

		var tbodyValue = "";
		for (var it in adDesigns) {
			var item = adDesigns[it];
			tbodyValue += "<tr data-campaign-id='" + item.adDesignId + "'>";
			tbodyValue += "	<td><input type='radio' name='ad-designs' data-ad-design-id='" + item.adDesignId + "' /></td>";
			tbodyValue += "	<td>" + item.adDesignId + "</td>";
			tbodyValue += "	<td>" + item.name + "</td>";
			tbodyValue += "	<td>" + item.adDesignType + "</td>";
			tbodyValue += "	<td>" + item.adResourceId + "</td>";
			tbodyValue += "	<td>" + item.destinationUrl + "</td>";
			tbodyValue += "	<td>" + item.isPause + "</td>";
			tbodyValue += "	<td>" + moment.utc(item.createTime).local().format('YYYY-MM-DD HH:mm:ss') + "</td>";
			tbodyValue += "</tr>";
		}

		$("#table-ad-designs tbody").append(tbodyValue);

		bindAdDesignTableRowClickEvent();
	}

	// 광고 디자인 폼 초기화
	function adDesignFormReset() {
		$("#form-ad-design")[0].reset();
		var campaignId = $("#form-campaign input[name=campaignId]").val();
		$("#form-ad-design input[name=campaignId]").val(campaignId);
		$("#ad-resource-preview").attr('src', '');
		setAdDesignsErrorMessage(null);
	}

	// 광고 디자인 에러 메시지 설정
	function setAdDesignsErrorMessage(message) {
		var elem = $("#section-ad-designs .alert");
		if (message == null || message == "") {
			elem.prop('hidden', true);
		} else {
			elem.text(message);
			elem.prop('hidden', false);
		}
	}
	// 광고 디자인 에러 메시지 설정 - Response 기준
	function setAdDesignsErrorMessageByResponse(response) {
		if (response == null || response.result == 'Success') {
			setAdDesignsErrorMessage(null);
		} else {
			setAdDesignsErrorMessage(response.result);
		}
	}

	// -----------------------------------------------------
	// 광고 디자인 이벤트 처리
	// -----------------------------------------------------
	// 광고 디자인 폼 submit 이벤트 무시
	$("#form-ad-design").submit((e) => {
		e.stopPropagation();
		e.preventDefault();
	});

	// 광고 디자인 폼 초기화
	$("#btn-ad-design-form-reset").click(() => {
		adDesignFormReset();
	});

	// 광고 디자인 생성
	$('#btn-ad-design-create').click(() => {
		var campaignId = $("#form-campaign input[name=campaignId]").val();
		if (campaignId === "")
			return;

		var data = $('#form-ad-design').serializeObject();

		HenaApi.adDesigns.create(data, (response) => {
			if (response.result === "Success") {
				adDesignContainer.add(response.data);
				drawAdDesignTable(adDesignContainer.items);
			} 
			setAdDesignsErrorMessageByResponse(response);
		});
	});

	// 광고 디자인 수정
	$('#btn-ad-design-modify').click(() => {
		var adDesignId = $("#form-adDesign input[name=adDesignId]").val();
		if (adDesignId === "")
			return;

		var data = $('#form-ad-design').serializeObject();

		HenaApi.adDesigns.modify(data, (response) => {
			if (response.result === "Success") {
				adDesignContainer.replace(data.adDesignId, response.data);
				drawAdDesignTable(adDesignContainer.items);
			}
			setAdDesignsErrorMessageByResponse(response);
		});
	});

	// 광고 디자인 삭제
	$('#btn-ad-design-delete').click(() => {
		var adDesignId = $("#form-ad-design input[name=adDesignId]").val();
		if (adDesignId === "")
			return;

		HenaApi.adDesigns.delete({ adDesignId: adDesignId }, (response) => {
			if (response.result === "Success") {
				adDesignContainer.remove(adDesignId);
				drawAdDesignTable(adDesignContainer.items);
			}
			setAdDesignsErrorMessageByResponse(response);
		});
	});

	// 파일 업로드
	$("#form-ad-design input[name=file]").change(() => {
		var fileData = $('#form-ad-design input[name=file]')[0].files[0];
		if (fileData == null)
			return;

		var formData = new FormData();
		formData.append('file', fileData);
		HenaApi.adResource.upload(formData, (response) => {
			if (response.result === "Success") {
				$('#form-ad-design input[name=adResourceId]').val(response.data.adResourceId);
				$('#form-ad-design input[name=adDesignType]').val(response.data.adDesignType);
				$('#ad-resource-preview').attr('src', response.data.url);
			} else {
				$('#form-ad-design input[name=adResourceId]').val('');
				$('#form-ad-design input[name=adDesignType]').val('');
				$('#ad-resource-preview').attr('src', '');
			}
			setAdDesignsErrorMessageByResponse(response);
		});
	});


	// -----------------------------------------------------
	// 시작 코드
	// -----------------------------------------------------
	// 캠페인 목록 갱신
	refreshCampaigns();


})(jQuery);
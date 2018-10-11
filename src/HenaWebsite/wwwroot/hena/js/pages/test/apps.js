
(function ($) {
	"use strict";

	// -----------------------------------------------------
	// 앱 컨테이너
	// -----------------------------------------------------
	var appContainer = {

		items: [],
		add: function (newItem) {
			this.items.push(newItem);
		},
		remove: function (appId) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.appId === appId) {
					this.items.splice(idx, 1);
				}
			}
		},
		replace: function (appId, newItem) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.appId === appId) {
					this.items[idx] = newItem;
					break;
				}
			}
		},
		find: function (appId) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.appId === appId)
					return item;
			}
		},
		clear: function () {
			this.items = [];
		}
	};

	// -----------------------------------------------------
	// 광고 유닛 컨테이너
	// -----------------------------------------------------
	var adUnitContainer = {

		items: [],
		add: function (newItem) {
			this.items.push(newItem);
		},
		remove: function (adUnitId) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.adUnitId === adUnitId) {
					this.items.splice(idx, 1);
				}
			}
		},
		replace: function (adUnitId, newItem) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.adUnitId === adUnitId) {
					this.items[idx] = newItem;
					break;
				}
			}
		},
		find: function (adUnitId) {
			for (var idx in this.items) {
				var item = this.items[idx];
				if (item.adUnitId === adUnitId)
					return item;
			}
		},
		clear: function () {
			this.items = [];
		}
	};

	// -----------------------------------------------------
	// 앱 관련 함수
	// -----------------------------------------------------

	// 앱 목록 갱신
	function refreshApps() {
		HenaApi.apps.list((response) => {
			if (response.result === "Success") {
				appContainer.items = response.data.apps;
				drawAppTable(appContainer.items);
			}
		});
	}

	// 앱 테이블 Row 클릭 이벤트 할당
	function bindAppTableRowClickEvent() {
		$('#table-apps tbody tr').click((e) => {
			selectAppRow(e.currentTarget);
		});
	}

	// 앱 Row 선택
	function selectAppRow(row) {
		var target = $(row);
		var radio = target.find('input[name=apps]');
		radio.prop('checked', true);
		var appId = radio.data("app-id");
		setAppFormValues(appContainer.find(appId));

		refreshAdUnits(appId);
	}

	// 앱 폼에 값 세팅
	function setAppFormValues(app) {
		if (app === null) {
			$("#form-app")[0].reset();
			return;
		}
		var form = $("#form-app");
		for (var it in app) {

			var elem = form.find("input[name=" + it + "]");
			if (it === 'beginTime' || it === 'endTime') {
				var localTime = moment.utc(app[it]).local().format("YYYY-MM-DDTHH:mm:ss");
				elem.val(localTime);
			} else {
				elem.val(app[it]);
			}
		}
	}

	// 앱 테이블 초기화
	function clearAppTable() {
		$("#table-apps tbody").empty();
	}

	// 앱 테이블 그리기
	function drawAppTable(apps) {

		clearAppTable();

		var tbodyValue = "";
		for (var it in apps) {
			var item = apps[it];
			tbodyValue += "<tr data-app-id='" + item.appId + "'>";
			tbodyValue += "	<td><input type='radio' name='apps' data-app-id='" + item.appId + "' /></td>";
			tbodyValue += "	<td>" + item.appId + "</td>";
			tbodyValue += "	<td>" + item.name + "</td>";
			tbodyValue += "	<td>" + item.marketType + "</td>";
			tbodyValue += "	<td>" + moment.utc(item.createTime).local().format('YYYY-MM-DD HH:mm:ss') + "</td>";
			tbodyValue += "	<td>" + moment.utc(item.lastUpdate).local().format('YYYY-MM-DD HH:mm:ss') + "</td>";
			tbodyValue += "</tr>";
		}

		$("#table-apps tbody").append(tbodyValue);

		bindAppTableRowClickEvent();
	}


	// -----------------------------------------------------
	// 앱 이벤트 처리
	// -----------------------------------------------------
	// 앱 폼 submit 이벤트 무시
	$("#form-app").submit((e) => {
		e.stopPropagation();
		e.preventDefault();
	});

	// 앱 폼 초기화
	$("#btn-app-form-reset").click(() => {
		$("#form-app")[0].reset();
	});

	// 앱 생성
	$('#btn-app-create').click(() => {
		var data = $('#form-app').serializeObject();
		data.beginTime = moment(data.beginTime).utc().format("YYYY-MM-DDTHH:mm:ss");
		data.endTime = moment(data.endTime).utc().format("YYYY-MM-DDTHH:mm:ss");

		HenaApi.apps.create(data, (response) => {
			if (response.result === "Success") {
				appContainer.add(response.data);
				drawAppTable(appContainer.items);
			}
		});
	});

	// 앱 수정
	$('#btn-app-modify').click(() => {
		var data = $('#form-app').serializeObject();
		data.beginTime = moment(data.beginTime).utc().format("YYYY-MM-DDTHH:mm:ss");
		data.endTime = moment(data.endTime).utc().format("YYYY-MM-DDTHH:mm:ss");

		HenaApi.apps.modify(data, (response) => {
			if (response.result === "Success") {
				appContainer.replace(data.appId, response.data);
				drawAppTable(appContainer.items);
			}
		});
	});

	// 앱 삭제
	$('#btn-app-delete').click(() => {
		var appId = $("#form-app input[name=appId]").val();
		if (appId === "")
			return;

		HenaApi.apps.delete({ appId: appId }, (response) => {
			if (response.result === "Success") {
				appContainer.remove(appId);
				drawAppTable(appContainer.items);
			}
		});
	});

	// -----------------------------------------------------
	// 광고 유닛 관련 함수
	// -----------------------------------------------------
	// 광고유닛 목록 갱신
	function refreshAdUnits(appId) {
		HenaApi.adUnits.list({ appId: appId }, (response) => {
			if (response.result === "Success") {
				adUnitContainer.items = response.data.adUnits;
				drawAdUnitTable(adUnitContainer.items);

				adUnitFormReset();
			}
		});
	}
	// 광고 유닛 테이블 Row 클릭 이벤트 할당
	function bindAdUnitTableRowClickEvent() {
		$('#table-ad-units tbody tr').click((e) => {
			selectAdUnitRow(e.currentTarget);
		})
	}

	// 광고 유닛 Row 선택
	function selectAdUnitRow(row) {
		var target = $(row);
		var radio = target.find('input[name=ad-units]');
		radio.prop('checked', true);
		var appId = radio.data("ad-unit-id");
		setAdUnitFormValues(adUnitContainer.find(appId));
	}

	// 광고 유닛 폼에 값 세팅
	function setAdUnitFormValues(adUnit) {
		if (adUnit === null) {
			$("#form-ad-unit")[0].reset();
			return;
		}
		var form = $("#form-ad-unit");
		for (var it in adUnit) {

			var elem = form.find("input[name=" + it + "]");
			if (it === 'createTime' ) {
				var localTime = moment.utc(adUnit[it]).local().format("YYYY-MM-DDTHH:mm:ss");
				elem.val(localTime);
			} else {
				elem.val(adUnit[it]);
			}
		}
	}

	// 광고 유닛 테이블 초기화
	function clearAdUnitTable() {
		$("#table-ad-units tbody").empty();
	}

	// 광고 유닛 테이블 그리기
	function drawAdUnitTable(adUnits) {

		clearAdUnitTable();

		var tbodyValue = "";
		for (var it in adUnits) {
			var item = adUnits[it];
			tbodyValue += "<tr data-app-id='" + item.adUnitId + "'>";
			tbodyValue += "	<td><input type='radio' name='ad-units' data-ad-unit-id='" + item.adUnitId + "' /></td>";
			tbodyValue += "	<td>" + item.adUnitId + "</td>";
			tbodyValue += "	<td>" + item.name + "</td>";
			tbodyValue += "	<td>" + item.adDesignType + "</td>";
			tbodyValue += "	<td>" + moment.utc(item.createTime).local().format('YYYY-MM-DD HH:mm:ss') + "</td>";
			tbodyValue += "	<td>" + moment.utc(item.lastUpdate).local().format('YYYY-MM-DD HH:mm:ss') + "</td>";
			tbodyValue += "</tr>";
		}

		$("#table-ad-units tbody").append(tbodyValue);

		bindAdUnitTableRowClickEvent();
	}

	// 광고 유닛 폼 초기화
	function adUnitFormReset() {
		$("#form-ad-unit")[0].reset();
		var appId = $("#form-app input[name=appId]").val();
		console.log(appId);
		$("#form-ad-unit input[name=appId]").val(appId);
	}

	// -----------------------------------------------------
	// 광고 유닛 이벤트 처리
	// -----------------------------------------------------
	// 광고 유닛 폼 submit 이벤트 무시
	$("#form-ad-unit").submit((e) => {
		e.stopPropagation();
		e.preventDefault();
	});

	// 광고 유닛 폼 초기화
	$("#btn-ad-unit-form-reset").click(() => {
		adUnitFormReset();
	});

	// 광고 유닛 생성
	$('#btn-ad-unit-create').click(() => {
		var appId = $("#form-app input[name=appId]").val();
		if (appId === "")
			return;

		var data = $('#form-ad-unit').serializeObject();
		data.beginTime = moment(data.beginTime).utc().format("YYYY-MM-DDTHH:mm:ss");
		data.endTime = moment(data.endTime).utc().format("YYYY-MM-DDTHH:mm:ss");

		HenaApi.adUnits.create(data, (response) => {
			if (response.result === "Success") {
				adUnitContainer.add(response.data);
				drawAdUnitTable(adUnitContainer.items);
			}
		});
	});

	// 광고 유닛 수정
	$('#btn-ad-unit-modify').click(() => {
		var adUnitId = $("#form-adUnit input[name=adUnitId]").val();
		if (adUnitId === "")
			return;

		var data = $('#form-ad-unit').serializeObject();
		data.beginTime = moment(data.beginTime).utc().format("YYYY-MM-DDTHH:mm:ss");
		data.endTime = moment(data.endTime).utc().format("YYYY-MM-DDTHH:mm:ss");

		HenaApi.adUnits.modify(data, (response) => {
			if (response.result === "Success") {
				adUnitContainer.replace(data.adUnitId, response.data);
				drawAdUnitTable(adUnitContainer.items);
			}
		});
	});

	// 광고 유닛 삭제
	$('#btn-ad-unit-delete').click(() => {
		var adUnitId = $("#form-ad-unit input[name=adUnitId]").val();
		if (adUnitId === "")
			return;

		HenaApi.adUnits.delete({ adUnitId: adUnitId }, (response) => {
			if (response.result === "Success") {
				adUnitContainer.remove(adUnitId);
				drawAdUnitTable(adUnitContainer.items);
			}
		});
	});


	// -----------------------------------------------------
	// 시작 코드
	// -----------------------------------------------------
	// 앱 목록 갱신
	refreshApps();


})(jQuery);
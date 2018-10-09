
var HenaApi = {};
(function ($) {
	"use strict";



	// -----------------------------------------------------------
	// Campaigns
	// -----------------------------------------------------------
	HenaApi.campaigns = {

		// name: 'Campaign Name'
		// campaignType: 'CPC'				Ex ) {CPC, CPM}
		// cost: '1000'
		// targetValue: '10000'
		// beginTime: '2018-10-01 00:00:00'
		// endTime: '2018-10-01 00:00:00'
		create: function (data, callback) {
			post("/api/campaigns/create", data, callback);
		},

		// campaignId:1234567890
		// name: 'Campaign Name'
		// campaignType: 'CPC'				Ex ) {CPC, CPM}
		// cost: '1000'
		// targetValue: '10000'
		// beginTime: '2018-10-01 00:00:00'
		// endTime: '2018-10-01 00:00:00'
		modify: function (data, callback) {
			post("/api/campaigns/modify", data, callback);
		},

		// campaignId:1234567890
		delete: function (data, callback) {
			post("/api/campaigns/delete", data, callback);
		},

		// no parameters
		list: function (callback) {
			post("/api/campaigns/list", null, callback);
		}
	};


	// -----------------------------------------------------------
	// Ad Designs
	// -----------------------------------------------------------
	HenaApi.adDesigns = {

		// campaignId:1234567890
		// name: 'Ad Design Name'
		// adDesignType: 'Banner'				Ex ) {Banner, Interstitial, Video}
		// resourceName: 'image00'
		// destinationUrl: 'http://www.hena.io'
		create: function (data, callback) {
			post("/api/addesigns/create", data, callback);
		},

		// adDesignId:1234567890
		// name: 'Ad Design Name'
		// adDesignType: 'Banner'				Ex ) {Banner, Interstitial, Video}
		// resourceName: 'image00'
		// destinationUrl: 'http://www.hena.io'
		modify: function (data, callback) {
			post("/api/addesigns/modify", data, callback);
		},

		// adDesignId:1234567890
		delete: function (data, callback) {
			post("/api/addesigns/delete", data, callback);
		},

		// campaignId:1234567890
		list: function (data, callback) {
			post("/api/addesigns/list", null, callback);
		}
	};



	// -----------------------------------------------------------
	// utilities
	// -----------------------------------------------------------
	// common api functions

	function post(url, data, callback) {
		$.ajax({
			url: url,
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: data === null ? null : JSON.stringify(data),
			success: function (response) {
				if (callback != null) {
					callback(response);
				}
			},
			error: function (error) {
				if (callback != null) {
					callback({
						result: 'Error'
						, message: error
					});
				}
			}
		});
	};
	// -----------------------------------------------------------


})(jQuery);

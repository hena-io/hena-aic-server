
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

		// campaignId:'1234567890'
		// name: 'Campaign Name'
		// campaignType: 'CPC'				Ex ) {CPC, CPM}
		// cost: '1000'
		// targetValue: '10000'
		// beginTime: '2018-10-01 00:00:00'
		// endTime: '2018-10-01 00:00:00'
		modify: function (data, callback) {
			post("/api/campaigns/modify", data, callback);
		},

		// campaignId:'1234567890'
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

		// campaignId:'1234567890'
		// name: 'Ad Design Name'
		// adDesignType: 'Banner'				Ex ) {None, MobileLeaderboard, MobileBannerLandscape, LargeMobileBanner, Banner, Leaderboard, InlineRectangle, SmartphoneInterstitialPortrait, SmartphoneInterstitialLandscape, TabletInterstitialPortrait, TabletInterstitialLandscape}
		// adResourceId: 'image00'
		// destinationUrl: 'http://www.hena.io'
		create: function (data, callback) {
			post("/api/addesigns/create", data, callback);
		},

		// adDesignId:'1234567890'
		// name: 'Ad Design Name'
		// adDesignType: 'Banner'				Ex ) {None, MobileLeaderboard, MobileBannerLandscape, LargeMobileBanner, Banner, Leaderboard, InlineRectangle, SmartphoneInterstitialPortrait, SmartphoneInterstitialLandscape, TabletInterstitialPortrait, TabletInterstitialLandscape}
		// adResourceId: '1234567890'
		// destinationUrl: 'http://www.hena.io'
		modify: function (data, callback) {
			post("/api/addesigns/modify", data, callback);
		},

		// adDesignId:'1234567890'
		delete: function (data, callback) {
			post("/api/addesigns/delete", data, callback);
		},

		// campaignId:'1234567890'
		list: function (data, callback) {
			post("/api/addesigns/list", data, callback);
		}
	};

	// -----------------------------------------------------------
	// Apps
	// -----------------------------------------------------------
	HenaApi.apps = {
		// name: 'App Name'
		// marketType: 'GooglePlay'				Ex ) {GooglePlay, AppleAppStore, WebSite}
		create: function (data, callback) {
			post("/api/apps/create", data, callback);
		},

		// appId:'1234567890'
		// name: 'App Name'
		// marketType: 'GooglePlay'				Ex ) {GooglePlay, AppleAppStore, WebSite}
		modify: function (data, callback) {
			post("/api/apps/modify", data, callback);
		},

		// appId:'1234567890'
		delete: function (data, callback) {
			post("/api/apps/delete", data, callback);
		},

		// no parameters
		list: function (callback) {
			post("/api/apps/list", null, callback);
		}
	};

	// -----------------------------------------------------------
	// Ad Units
	// -----------------------------------------------------------
	HenaApi.adUnits = {

		// appId:'1234567890'
		// name: 'Ad Unit Name'
		// adSystemType: 'Banner'				Ex ) {Banner, Interstitial, Video}
		create: function (data, callback) {
			post("/api/adunits/create", data, callback);
		},

		// adUnitId:'1234567890'
		// name: 'Ad Unit Name'
		// adSystemType: 'Banner'				Ex ) {Banner, Interstitial, Video}
		modify: function (data, callback) {
			post("/api/adunits/modify", data, callback);
		},

		// adUnitId:'1234567890'
		delete: function (data, callback) {
			post("/api/adunits/delete", data, callback);
		},

		// appId:'1234567890'
		list: function (data, callback) {
			post("/api/adunits/list", data, callback);
		}
	};

	// -----------------------------------------------------------
	// Ad Resource
	// -----------------------------------------------------------
	HenaApi.adResource = {

		// need FormData
		// adResourceId:'1234567890'
		// file: FormFile
		upload: function (data, callback) {
			postFormData("/api/adresources/upload", data, callback);
		},

		// adResourceId:'1234567890'
		delete: function (data, callback) {
			post("/api/adresources/delete", data, callback);
		},

		// adResourceId:'1234567890'
		info: function (data, callback) {
			post("/api/adresources/info", data, callback);
		},

		// no parameters
		list: function (callback) {
			post("/api/adresources/list", null, callback);
		}
	};

	// -----------------------------------------------------------
	// Ad Units
	// -----------------------------------------------------------
	HenaApi.pageAd = {

		// adUnitId:'1234567890'
		// clientType: 'Web'		Ex ) {Android,IOS,Web}
		// adSystemType: 'Banner'				Ex ) {Banner, Interstitial, Video}
		// isLandscape: false
		// screenWidth: 1280
		// screenHeight: 720
		ready: function (data, callback) {
			post("/service/pagead/adready", data, callback);
		},

		// adUnitId:'1234567890'
		// ai: base64 encoded string
		display: function (data, callback) {
			post("/service/pagead/addisplay", data, callback);
		}
	};

	// -----------------------------------------------------------
	// Users
	// -----------------------------------------------------------
	HenaApi.users = {

		// eMail:'abc@def.com'
		resetPassword: function (data, callback) {
			post("/api/users/resetpassword", data, callback);
		},
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
				if (callback !== null) {
					callback(response);
				}
			},
			error: function (error) {
				if (callback !== null) {
					callback({
						result: 'Error'
						, message: error
					});
				}
			}
		});
	};

	HenaApi.post = post;

	function postFormData(url, formData, callback) {
		$.ajax({
			url: url,
			type: "POST",
			contentType: false, // Not to set any content header  
			processData: false, // Not to process data  
			data: formData,
			success: function (response) {
				if (callback !== null) {
					callback(response);
				}
			},
			error: function (response) {
				if (callback !== null) {
					callback({
						result: 'Error'
						, message: error
					});
				}
			}
		});  
	}

	// -----------------------------------------------------------


}) (jQuery);


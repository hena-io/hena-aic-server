
(function ($) {
	"use strict";

	$('form').submit((e) => {
		e.preventDefault();
		e.stopPropagation();
		return false;
	});

	var isValidEMail = false;
	var isValidPassword = false;
	var isValidConfirmPassword = false;
	var isValidVerifyCode = false;
	var inputEMail = $('.signup-form input[name=email]');
	var inputPassword = $('.signup-form input[name=password]');
	var inputConfirmPassword = $('.signup-form input[name=confirm_password]');
	var inputVerifyCode = $('.signup-form input[name=verify_code]');

	/*==================================================================
	 * Common Methods
	==================================================================*/
	function updateCheckIconState(target, isSuccess) {

		var tp = target.parent();

		target.addClass("fa-check");
		target.removeClass("fa-spin");
		target.removeClass("fa-spinner");
		if (isSuccess) {
			tp.removeClass('btn-danger');
			tp.addClass('btn-success');
		} else {
			tp.removeClass('btn-success');
			tp.addClass('btn-danger');
		}
	}

	/*==================================================================
	 * Validation E-Mail
	==================================================================*/
	var validationTimer_EMail;

	$(inputEMail).keyup(() => {
		checkValidation_EMail();
	});

	function setValidState_EMail(isValid) {
		isValidEMail = isValid == true;
		if (isValidEMail) {
			$('#btn_request_verify').removeClass('disabled');
		} else {
			if ($('#btn_request_verify').hasClass('disabled') == false) {
				$('#btn_request_verify').addClass('disabled');
			}
		}
	}

	function checkValidation_EMail() {
		if (validationTimer_EMail != null) {
			clearTimeout(validationTimer_EMail);
			validationTimer_EMail = null;
		}
		validationTimer_EMail = setTimeout(() => {
			var target = inputEMail.parent().find("i");

			$.ajax({
				url: "/api/user/JoinVerifyEMail",
				type: "POST",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				data: JSON.stringify({
					EMail: inputEMail.val(),
				}),
				success: function (response) {

					if (response.result == 'Success') {
						updateCheckIconState(target, true);
						setValidState_EMail(true);
					} else {
						updateCheckIconState(target, false);
						setValidState_EMail(false);
					}
				},
				error: function (error) {
					updateCheckIconState(target, false);
					isValidEMail = false;
					setValidState_EMail(false);
				}
			});
		}, 100);
	}

	// Request Send Verify E-Mail
	$('#btn_request_verify').click(() => {
		if (isValidEMail == false)
			return;

		var email = inputEMail.val();

		inputEMail.prop('readonly', true);

		$.ajax({
			url: "/api/user/SendVerifyEMail",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify({
				EMail: email,
			}),
			success: function (response) {
				if (response.result == 'Success') {
					$('#group-verify-email').prop('hidden', false);
				} else {
					inputEMail.removeClass('readonly');
				}
			},
			error: function (error) {
				inputEMail.removeClass('readonly');
			}
		});

	});

	// Check Verify Code
	$('#btn_request_verify').click(() => {
		if (isValidEMail == false)
			return;

		var email = inputEMail.val();

		inputEMail.prop('readonly', true);
		isValidVerifyCode = false;

		$.ajax({
			url: "/api/user/SendVerifyEMail",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify({
				EMail: email,
			}),
			success: function (response) {
				if (response.result == 'Success') {
					$('#group-verify-email').prop('hidden', false);
				} else {
					inputEMail.removeClass('readonly');
				}
			},
			error: function (error) {
				inputEMail.removeClass('readonly');
			}
		});
	});

	// Check Verify Code
	$('#btn_check_verify_code').click(() => {
		if (isValidEMail == false)
			return;

		var email = inputEMail.val();
		var verifyCode = inputVerifyCode.val();
		isValidVerifyCode = false;
		$.ajax({
			url: "/api/user/IsValidEMailVerifyCode",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify({
				EMail: email,
				VerifyCode: verifyCode,
			}),
			success: function (response) {
				if (response.result == 'Success') {
					isValidVerifyCode = true;
					$('#group-verify-email').prop('hidden', true);
				} else {
					$("#group-verify-email .alert").prop('hidden', false);
				}
				updateSignUpButtonState();
			},
			error: function (error) {
				$("#group-verify-email .alert").prop('hidden', false);
				updateSignUpButtonState();
			}
			
		});
	});

	/*==================================================================
	 * Validation Password
	==================================================================*/
	var validationTimer_Password;
	
	$(inputPassword).keyup(() => {
		if (validationTimer_Password != null) {
			clearTimeout(validationTimer_Password);
			validationTimer_Password = null;
		}
		validationTimer_Password = setTimeout(() => {
			var target = inputPassword.parent().find("i");

			isValidPassword = HenaUtility.checkValidPassword(inputPassword.val(), 3);
			updateCheckIconState(target, isValidPassword);
			updateConfirmPasswordState();
			updateSignUpButtonState();
		}, 100);
	});


	/*==================================================================
	 * Validation Confirm Password
	==================================================================*/
	var validationTimer_ConfirmPassword;

	$(inputConfirmPassword).keyup(() => {
		if (validationTimer_ConfirmPassword != null) {
			clearTimeout(validationTimer_ConfirmPassword);
			validationTimer_ConfirmPassword = null;
		}
		validationTimer_ConfirmPassword = setTimeout(() => {
			updateConfirmPasswordState();
			updateSignUpButtonState();
		}, 100);
	});

	function updateConfirmPasswordState() {
		var target = inputConfirmPassword.parent().find("i");
		isValidConfirmPassword = inputPassword.val() == inputConfirmPassword.val();
		updateCheckIconState(target, isValidConfirmPassword);
	}

	function updateSignUpButtonState() {
		var isValid = isValidEMail && isValidVerifyCode && isValidPassword && isValidConfirmPassword;
		$('#btn_signup').prop('disabled', !isValid);
	}


	/*==================================================================
	 * Join
	==================================================================*/
	var btnSignup = $('#btn_signup');

	btnSignup.click(() => {

		var email = inputEMail.val();
		var password = inputPassword.val();
		var verifyCode = inputVerifyCode.val();

		btnSignup.addClass('disabled');

		$.ajax({
			url: "/api/user/Join",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify({
				EMail: email,
				Password: password,
				VerifyCode: verifyCode,
			}),
			success: function (response) {
				if (response.result == 'Success') {
					window.location.href = '/Dashboard';
				} else {
					$('#alert_signup_failed_message').prop('hidden', false);
					btnSignup.removeClass('disabled');
				}
			},
			error: function (error) {
				$('#alert_signup_failed_message').prop('hidden', false);
				btnSignup.removeClass('disabled');
			}
		});
	});


})(jQuery);
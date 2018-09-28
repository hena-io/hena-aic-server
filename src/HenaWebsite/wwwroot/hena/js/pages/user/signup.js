
(function ($) {
	"use strict";


    /*==================================================================
    [ Focus input ]*/
	$('.input100').each(function () {
		$(this).on('blur', function () {
			if ($(this).val().trim() != "") {
				$(this).addClass('has-val');
			}
			else {
				$(this).removeClass('has-val');
			}
		})
	})


    /*==================================================================
    [ Validate ]*/
	var input = $('.validate-input .input100');
	var inputStep1 = $('#signup_step1 .validate-input .input100');

	function checkValidation(targets, showPopup) {
		var check = true;

		for (var i = 0; i < targets.length; i++) {
			if (validate(targets[i]) == false) {
				if (showPopup) {
					showValidate(targets[i]);
				}
				check = false;
			}
		}

		return check;
	}


	$('.validate-form .input100').each(function () {
		$(this).focus(function () {
			hideValidate(this);
		});
	});

	function validate(input) {

		var val = $(input).val().trim();
		var targetName = $(input).attr('name');
		var targetType = $(input).attr('type');

		if (targetType == 'email' || targetName == 'email') {
			if (val.match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
				return false;
			}
		} else if (targetName == 'username') {
			if (val.length < 3 || val.length > 30)
				return false;

			if (val.match(/^[_a-zA-Z0-9]+$/) == null) {
				return false;
			}
			
		} else if (targetName == 'password') {
			
			if (isValidPassword(val) == false)
				return false;
			
		}

		if (val == '') {
			return false;
		}
	}



	function isValidPassword(val, minMatchCount = 3) {
		if (val.length < 8 || val.length >= 32)
			return false;

		var regexNumber = /[0-9]/; 
		var regexUpperChar = /[A-Z]/; 
		var regexLowerChar = /[a-z]/; 
		var regexSymbols = /[~!@\#$%<>^&*\()\-=+_\’]/gi; 


		var hasNumber = regexNumber.test(val);
		var hasUpperChar = regexUpperChar.test(val);
		var hasLowerChar = regexLowerChar.test(val);
		var hasSymbols = regexSymbols.test(val);

		var matchCount = 0;

		if (hasLowerChar)
			++matchCount;

		if (hasUpperChar)
			++matchCount;

		if (hasNumber)
			++matchCount;

		if (hasSymbols)
			++matchCount;

		return matchCount >= minMatchCount;
	}

	function showValidate(input) {
		var thisAlert = $(input).parent();

		$(thisAlert).addClass('alert-validate');
	}

	function hideValidate(input) {
		var thisAlert = $(input).parent();

		$(thisAlert).removeClass('alert-validate');
	}

    /*==================================================================
    [ Show pass ]*/
	var showPass = 0;
	$('.btn-show-pass').on('click', function () {
		if (showPass == 0) {
			$(this).next('input').attr('type', 'text');
			$(this).find('i').removeClass('zmdi-eye');
			$(this).find('i').addClass('zmdi-eye-off');
			showPass = 1;
		}
		else {
			$(this).next('input').attr('type', 'password');
			$(this).find('i').addClass('zmdi-eye');
			$(this).find('i').removeClass('zmdi-eye-off');
			showPass = 0;
		}

	});

	var isValidEMail = false;
	var isValidUsername = false;
	var inputEMail = $('.login100-form input[name=email]');
	var inputUsername = $('.login100-form input[name=username]');
	var inputPassword = $('.login100-form input[name=password]');
	var inputVerifyCode = $('.login100-form input[name=verify_code]');

	// validation email
	var validationTimer_EMail;
	
	$(inputEMail).keyup(() => {
		checkValidation_EMail();
	});

	function checkValidation_EMail() {
		if (validationTimer_EMail != null) {
			clearTimeout(validationTimer_EMail);
			validationTimer_EMail = null;
		}
		validationTimer_EMail = setTimeout(() => {

			$.ajax({
				url: "/api/user/JoinVerifyEMail",
				type: "POST",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				data: JSON.stringify({
					EMail: inputEMail.val(),
				}),
				success: function (response) {
					var target = $('#icon_email_validation');
					target.removeClass('text-danger');
					target.removeClass('text-success');
					target.removeClass('text-error');

					if (response.result == 'Success') {
						target.addClass('text-success');
						isValidEMail = true;
					} else {
						if (inputEMail.length == 0) {
							target.addClass('text-error');
						} else {
							target.addClass('text-danger');
						}
						isValidEMail = false;
					}
					refreshContinueButtonState();
				},
				error: function (error) {
					isValidEMail = false;
					refreshContinueButtonState();
				}
			});
		}, 100);
	}

	// validation username
	var validationTimer_Username;
	$(inputUsername).keyup(() => {
		checkValidation_Username();
	});

	function checkValidation_Username() {
		if (validationTimer_Username != null) {
			clearTimeout(validationTimer_Username);
			validationTimer_Username = null;
		}
		validationTimer_Username = setTimeout(() => {

			$.ajax({
				url: "/api/user/JoinVerifyUsername",
				type: "POST",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				data: JSON.stringify({
					Username: inputUsername.val(),
				}),
				success: function (response) {
					var target = $('#icon_username_validation');
					target.removeClass('text-danger');
					target.removeClass('text-success');
					target.removeClass('text-error');

					if (response.result == 'Success') {
						target.addClass('text-success');
						isValidUsername = true;
					} else {
						if (inputEMail.length == 0) {
							target.addClass('text-error');
						} else {
							target.addClass('text-danger');
						}
						isValidUsername = false;
					}
					refreshContinueButtonState();
				},
				error: function (error) {
					isValidUsername = false;
					refreshContinueButtonState();
				}
			});
		}, 100);
	}

	inputPassword.on('keyup', ()=>{
		refreshContinueButtonState();
	});

	// continue 버튼 상태 업데이트
	function refreshContinueButtonState() {

		var isValid = checkValidation(inputStep1, false);
		
		isValid = isValid && isValidUsername && isValidEMail;
		if (isValid) {
			
			$('#btn_step1').removeClass('disabled');
		}
		else {
			if ($('#btn_step1').hasClass('disabled') == false) {
				$('#btn_step1').addClass('disabled');
			}
		}
	}

	// 인증메일 발송 및 다음 스텝으로 이동
	var btn_step1 = $('#btn_step1');
	btn_step1.click(() => {
		if (btn_step1.hasClass('disabled') || btn_step1.prop('disabled'))
			return;

		if (checkValidation(inputStep1, true) == false)
			return;


		var username = inputUsername.val();
		var email = inputEMail.val();

		btn_step1.addClass('disabled');

		$.ajax({
			url: "/api/user/SendVerifyEMail",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify({
				EMail: email,
				Username: username,
			}),
			success: function (response) {
				if (response.result != 'Success') {
					btn_step1.removeClass('disabled');
					return;
				}

				$('#signup_step1').prop('hidden', true);
				$('#signup_step2').prop('hidden', false);
				
			},
			error: function (error) {
				isValidUsername = false;
			}
		});
	
	});


	// 회원 가입 요청
	var btnSignup = $('#btn_signup');

	btnSignup.click(() => {

		var username = inputUsername.val();
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
				Username: username,
				Password: password,
				VerifyCode: verifyCode,
			}),
			success: function (response) {
				if (response.result == 'Success') {
					window.location.href = '/Dashboard';
				} else {
					$('#alert_incorrect_message').prop('hidden', false);
					btnSignup.removeClass('disabled');
				}
			},
			error: function (error) {
			}
		});
	});


})(jQuery);
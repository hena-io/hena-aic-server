function HenaUtility() {

}

HenaUtility.checkValidEmail = function (val) {
	if (val.match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) === null) {
		return false;
	}
	return true;
};

HenaUtility.checkValidPassword = function (val, minMatchCount = 3) {
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
};
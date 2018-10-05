using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models
{
	public enum ErrorCode
	{
		Success,				// 성공
		Failed,					// 실패
		BadRequest,				// 잘못된 요청
		InvalidParameters,		// 잘못된 파라미터
		InvalidSession,			// 잘못된 세션
		InvalidVerifyCode,		// 잘못된 인증 코드
		InvalidUserName,		// 잘못된 유저명
		InvalidEMail,			// 잘못된 이메일
		InvalidPassword,		// 잘못된 비밀번호
		InvalidFormat,			// 잘못된 포멧
		ExistUserName,			// 이미 존재하는 유저명
		ExistEMail,				// 이미 존재하는 이메일
		AlreadyLoggedin,		// 이미 로그인되어 있음.
		DatabaseError,			// 데이터베이스 에러
		UknownError,			// 알 수 없는 에러
	}
}

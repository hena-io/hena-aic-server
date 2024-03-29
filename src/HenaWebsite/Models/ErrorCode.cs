﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
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
		InvalidResource,		// 잘못된 리소스
		InvalidSize,			// 잘못된 사이즈
		InvalidId,				// 잘못된 Id
		NotFound,               // 찾을 수 없다.
		ExistUserName,          // 이미 존재하는 유저명
		ExistEMail,				// 이미 존재하는 이메일
		NotSupportFormat,		// 지원하지 않는 포멧
		AlreadyLoggedin,        // 이미 로그인되어 있음.
		AlreadyStarted,         // 이미 시작됨.
		NotRunning,				// 실행중이 아님.
		DatabaseError,			// 데이터베이스 에러
		UknownError,			// 알 수 없는 에러
	}
}

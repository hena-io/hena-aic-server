using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.Shared.Data
{
	
	public enum AccountStatusType
	{
		None,
		Pending,
		Active,
		Block,
	}

	public enum AccountPermissionType
	{
		None,
		Administrator,  // 관리자 권한
		EZBot,          // EZBot 사용 권한
        Korbit,         // 코빗 권한
	}

	// 상품 타입
	public enum ProductTypes
	{
		None,
		Normal,
		Event,
		Special,
	}

    // 입금 상태
    public enum DepositState
    {
        None,
        NotFoundAccount,    // 입금 대상 계정을 찾을 수 없음
        Pending,            // 입금 대기
        Complete,           // 완료
    }

	public enum TimeEventType
	{
		None,
		DepositBonusPoint,	// 입금 보너스 포인트 이벤트
	}
}

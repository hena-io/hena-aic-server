# API 매뉴얼
----------------------------------------
##  참고 자료형

    enum AdDesignTypes
    {
        None,				
        MobileLeaderboard,                  // 320 x 50: 모바일 리더보드
        MobileBannerLandscape,              // 480 x 32: 모바일 배너(가로 모드)
        LargeMobileBanner,                  // 320 x 100: 큰 모바일 배너
        Banner,                             // 468 x 60: 배너
        Leaderboard,                        // 728 x 90: 리더보드
        InlineRectangle,                    // 300 x 250: 인라인 직사각형
        SmartphoneInterstitialPortrait,     // 320 x 480: 스마트폰 전면 광고(세로 모드)
        SmartphoneInterstitialLandscape,    // 480 x 320: 스마트폰 전면 광고(가로 모드)
        TabletInterstitialPortrait,         // 768 x 1024: 태블릿 전면 광고(세로 모드)
        TabletInterstitialLandscape,        // 1024 x 768: 태블릿 전면 광고(가로 모드)
    }

    enum ClientTypes
	{
        None,
        Android,
        IOS,
        Web,
	}

    enum CampaignTypes
	{
        None,
        CPC,            // Click Per Click( 클릭 1회당 과금 )
        CPM,            // Click Per Mille( 노출 1000회당 과금 )
	}

    enum AdSystemTypes
	{
        None,
        Banner,         // 배너 광고
        Interstitial,   // 전면 광고
        Video,          // 비디오 광고
	}

## 홈페이지 내부 API
    아래 API들은 홈페이지에서 로그인 되어있다는 전제로 작성되어있습니다.
    [Authorize] 속성이 걸려있어 로그인 하지 않은 경우 호출이 되지 않습니다.

    내부적으로 필요한 UserId는 ASP.NET SignIn Data를 사용해서 얻어내도록 되어있습니다.
    추후 OpenAPI 형식으로 변경이 필요한 경우 UserId를 입력 받도록 구조 변경이 필요합니다.

### 1. 캠페인 
- ##### 생성
        [POST]
            /api/campaigns/create
        
        [Parameters]
            name: 'Campaign Name'
            campaignType: 'CPC'
            cost: '1000'
            targetValue: '10000'
            beginTime: '2018-10-01 00:00:00'
            endTime: '2018-10-01 00:00:00'
   
- ##### 수정
        [POST] 
            /api/campaigns/modify
    
        [Parameters]
            campaignId:'1234567890'
            name: 'Campaign Name'
            campaignType: 'CPC'
            cost: '1000'
            targetValue: '10000'
            beginTime: '2018-10-01 00:00:00'
            endTime: '2018-10-01 00:00:00'

- ##### 삭제
        [POST]
            /api/campaigns/delete
        
        [Parameters]
            campaignId:'1234567890'

- ##### 목록
        [POST]
            /api/campaigns/list
        
        [Parameters]
            없음

----------------------------------------
### 2. 광고 디자인 
- ##### 생성
        [POST]
            /api/addesigns/create
        
        [Parameters]
            campaignId:'1234567890'
            name: 'Ad Design Name'
            adDesignType: 'Banner'
            resourceId: 'image00'
            destinationUrl: 'http://www.hena.io'
            
   
- ##### 수정
        [POST] 
            /api/addesigns/modify
    
        [Parameters]
            adDesignId:'1234567890'
            name: 'Ad Design Name'
            adDesignType: 'Banner'
            adResourceId: '1234567890'
            destinationUrl: 'http://www.hena.io'

- ##### 삭제
        [POST]
            /api/addesigns/delete
        
        [Parameters]
            adDesignId:'1234567890'

- ##### 목록
        [POST]
            /api/addesigns/list
        
        [Parameters]
            campaignId:'1234567890'

----------------------------------------
### 3. 앱
- ##### 생성
        [POST]
            /api/apps/create
        
        [Parameters]
            name: 'App Name'
	    	marketType: 'GooglePlay'
            
   
- ##### 수정
        [POST] 
            /api/apps/modify
    
        [Parameters]
            appId:'1234567890'
            name: 'App Name'
            marketType: 'GooglePlay'

- ##### 삭제
        [POST]
            /api/apps/delete
        
        [Parameters]
            appId:'1234567890'

- ##### 목록
        [POST]
            /api/apps/list
        
        [Parameters]
            없음.


----------------------------------------
### 4. 광고 유닛
- ##### 생성
        [POST]
            /api/adunits/create
        
        [Parameters]
            appId:'1234567890'
            name: 'Ad Unit Name'
            adSystemType: 'Banner'
            
   
- ##### 수정
        [POST] 
            /api/adunits/modify
    
        [Parameters]
            adUnitId:'1234567890'
            name: 'Ad Unit Name'
            adSystemType: 'Banner'

- ##### 삭제
        [POST]
            /api/adunits/delete
        
        [Parameters]
            adUnitId:'1234567890'

- ##### 목록
        [POST]
            /api/adunits/list
        
        [Parameters]
            appId:'1234567890'

----------------------------------------
### 5. 광고 리소스
- ##### 업로드
        [POST]
            /api/adresources/upload
        
        [Parameters] - FormData에 넣어 업로드 필요.
            adResourceId:'1234567890'
            file: FormFile
            
   
- ##### 삭제
        [POST] 
            /api/adresources/delete
    
        [Parameters]
            adResourceId:'1234567890'

- ##### 개별 리소스 정보
        [POST]
            /api/adresources/info
        
        [Parameters]
            adResourceId:'1234567890'

- ##### 목록
        [POST]
            /api/adresources/list
        
        [Parameters]
            없음.



## SDK API 및 페이지
    SDK에서 사용할 API 및 페이지 링크입니다.

### 1. SDK API 
- ##### 광고준비요청
        - 광고를 요청합니다.
        
        [POST]
            /service/pagead/adready
        
        [Parameters]
            adUnitId:'1234567890'
            clientType: 'Web'
            adSystemType: 'Banner'
            isLandscape: false
            screenWidth: 1280
            screenHeight: 720
   
- ##### 광고 노출 알림( 모바일 전용 )
        광고가 노출되었음을 서버에 알립니다.
        모바일에서 리소스 캐싱등 별도의 처리가 필요할 수 있기 때문에, 모바일 전용으로만 사용됩니다.
        
        [POST] 
            /service/pagead/addisplay
    
        [Parameters]
            ai: adready 호출시 받은 ai 정보.

- ##### 광고 리소스 URL ( 웹 전용 )
        웹에서 광고 리소스를 링크로 사용합니다.
        내부적으로 실제 리소스 URL로 Redirect를 발생시키며,
        호출 시점에서 광고가 노출되었다고 판단합니다.

        [GET]
            /service/pagead/adresource
        
        [Parameters]
            ai: adready 호출시 받은 ai 정보.

- ##### [페이지] 광고 클릭 URL
        호출시 실제 광고 URL로 Redirect를 발생시키며,
        호출 시점에서 광고가 클릭되었다고 판단합니다.

        [GET]
            /service/pagead/adclick
        
        [Parameters]
            ai: adready 호출시 받은 ai 정보.

----------------------------------------
## 저장소
   - DB SQL : https://github.com/hena-io/hena-aic-server/tree/master/db/sql/
   - 프로젝트 : https://github.com/hena-io/hena-aic-server/tree/master/src/
        - HenaLibrary : 공유 라이브러리 프로젝트
        - HenaWebsite : AIC 웹사이트 프로젝트
        - HenaSharedModels : 공유 데이터 모델 관리 프로젝트
        - HenaDatabaseLibrary : 데이터베이스 관련 관리 프로젝트
        - HenaSampleImageCreator : AIC 테스트용 이미지 생성 툴

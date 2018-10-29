# API 매뉴얼
----------------------------------------
##  참고 자료형

	enum CurrencyTypes
	{
		None,
		HENA,
		HENA_AIC,
		HENA_MINING,
	}

## API
    

### 1. 마이닝 API 
- ##### 마이닝 상태
        [POST]
            /service/mining/miningstate
        
        [Parameters]
            userId: '791472617497798814'

        [Response]
        {
            result:'Success'
            data:{
                isRunning:false
            }
        }

- ##### 마이닝 시작
        [POST]
            /service/mining/miningstart
        
        [Parameters]
            userId: '791472617497798814'

        [Response]
        {
            result:'Success'
            data:null
        }
   
- ##### 마이닝 정지
        [POST] 
            /service/mining/miningstop
    
        [Parameters]
            userId: '791472617497798814'
        
        [Response]
        {
            result:'Success'
            data:null
        }

- ##### 마이닝 세션 갱신
        [POST] 
            /service/mining/miningupdatesession
    
        [Parameters]
            userId: '791472617497798814'

        [Response]
        {
            result:'Success'
            data:null
        }

- ##### 마이닝 기록 조회
        [POST]
            /service/mining/mininghistory
        
        [Parameters]
            userId: '791472617497798814'
            offset: 0
            count: 20

        [Response]
        {
            result:'Success'
            data:{
                items:[
                    {
                        miningHistoryId: '1234567890',
                        currencyType: 'HENA_MINING',
                        miningAmount: 0.123,
                        miningTime: '2018-10-25T00:00:00', // UTC TIME
                    },
                    { ... }
                ]
            }
        }

- ##### 마이닝 리포트 조회
        [POST]
            /service/mining/miningreport
        
        [Parameters]
            userId: '791472617497798814'
            beginTime: '2018-10-01 00:00:00'    // LOCAL TIME
            endTime: '2018-11-01 00:00:00'      // LOCAL TIME
            timeZoneOffset: '09:00:00'          // LOCAL TIME - UTC TIME(한국의 경우 9시간 차이)

        [Response]
        {
            result:'Success'
            data:{
                items:[
                    {
                        date: '2018-10-25T00:00:00', // LOCAL TIME
                        amount: 123456.123456,
                    },
                    { ... }
                ]
            }
        }   

----------------------------------------
### 2. AIC - 유저 API
- ##### 잔고 조회
        [POST]
            /service/users/balances
        
        [Parameters]
            userId: '791472617497798814'
        
        [Response]
        {
            result:'Success'
            data:{
                balances:[
                    {
                        currencyType:'HENA',
                        amount:123456.123456
                    },
                    {
                        currencyType:'HENA_AIC',
                        amount:123456.123456
                    },
                    {
                        currencyType:'HENA_MINING',
                        amount:123456.123456
                    },
                    { ... }
                ]
            }
        }   
            
   
- ##### AIC 기록 조회
        [POST]
            /service/users/aichistory
        
        [Parameters]
            userId: '791472617497798814'
            // false : 고객 Id로 검색, true : 퍼블리셔 Id로 검색
            isPublisherReport:false 
            offset:0
            count:20
        
        [Response]
        {
            result:'Success'
            data:{
                items:[
                    {
                        adHistoryId:'1234567890',
                        customerId:'1234567890',
                        publisherId:'1234567890',
                        appId:'1234567890',
                        adUnitId:'1234567890',
                        advertiserId:'1234567890',
                        campaignId:'1234567890',
                        adDesignId:'1234567890',
                        campaignType:'CPC',
                        adDesignType:'Banner',
                        cost:1000.0,
                        publisherRevenue:600.0,
                        customerRevenue:400.0,
                        isDisplayed:true,
                        displayTime:'2018-10-25T12:12:12',	// UTC 시간
                        isClicked:true,
                        clickTime:'2018-10-25T12:12:12'	// UTC 시간
                    }
                    { ... }
                ]
            }
        }  

- ##### AIC 수익 리포트
        [POST]
            /service/users/aicrevenuereport
        
        [Parameters]
            userId: '791472617497798814'
            // false : 고객 Id로 검색, true : 퍼블리셔 Id로 검색
            isPublisherReport:false 
            beginTime: '2018-10-01 00:00:00'    // LOCAL TIME
            endTime: '2018-11-01 00:00:00'      // LOCAL TIME
            timeZoneOffset: '09:00:00'          // LOCAL TIME - UTC TIME(한국의 경우 9시간 차이)
        
        [Response]
        {
            result:'Success'
            data:{
                items:[
                    {
                        reportDate: '2018-10-25T00:00:00', // LOCAL TIME
                        revenue:4000.0,
                        displayCount:10,
                        clickCount:10,
                    }
                    { ... }
                ]
            }
        }  

----------------------------------------
## 저장소
   - DB SQL : https://github.com/hena-io/hena-aic-server/tree/master/db/sql/
   - 프로젝트 : https://github.com/hena-io/hena-aic-server/tree/master/src/
        - HenaLibrary : 공유 라이브러리 프로젝트
        - HenaWebsite : AIC 웹사이트 프로젝트
        - HenaSharedModels : 공유 데이터 모델 관리 프로젝트
        - HenaDatabaseLibrary : 데이터베이스 관련 관리 프로젝트
        - HenaSampleImageCreator : AIC 테스트용 이미지 생성 툴

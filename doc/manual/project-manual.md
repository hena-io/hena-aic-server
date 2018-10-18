# 웹사이트 프로젝트 매뉴얼
----------------------------------------
## 1. 설정파일
    설정파일은 HenaWebSite\wwwroot\App_Data 폴더에 넣으며, 프로젝트 빌드시 같이 Export됩니다.

- config.json
    전체적인 서버 옵션정보를 관리합니다.

- database.json
    데이터베이스 접속정보를 관리합니다.
    
## 2. 주요 프로젝트
- HenaLibrary : 공유 라이브러리 프로젝트
- HenaWebsite : AIC 웹사이트 프로젝트
- HenaSharedModels : 공유 데이터 모델 관리 프로젝트
- HenaDatabaseLibrary : 데이터베이스 관련 관리 프로젝트
- HenaSampleImageCreator : AIC 테스트용 이미지 생성 툴
  
## 3. 주요 폴더
- HenaWebsite/API : API 관련 클래스를 관리하는 폴더
- HenaWebsite/API/APIModels : API에서 사용하는 모델 클래스를 관리하는 폴더
- HenaWebsite/Controllers : 일반 페이지 Controller를 관리하는 폴더입니다.
- HenaWebsite/Views : cshtml파일을 관리하는 폴더입니다.
- HenaSharedModels/Data : 솔루션에서 사용하는 공유 데이터 클래스를 관리하는 폴더
- HenaDatabaseLibrary/DB/Query : 쿼리 호출 관련 클래스를 관리하는 폴더
- HenaDatabaseLibrary/DB/Data : DB와 데이터를 연결하는데 필요한 클래스를 관리하는 폴더

## 3. 주요 클래스
- Startup
    - 프로그램이 시작되는 클래스입니다.
    - 각종 초기화 처리를 진행합니다.
- WebConfiguration
    - 프로젝트 설정 정보를 관리합니다.
    - config.json 파일을 통해 데이터를 읽어옵니다.
- DBThread
    - DBThread를 통해 Query를 보내고 받습니다.
- GlobalDefine
    - 전역적으로 미리 정의해 둘 필요가 있는 변수를 지정하는데 사용됩니다.
- DBKey
    - Database 키값으로 사용되는 Wrapping 클래스입니다.
    - 실제 자료형은 int64를 기반으로 합니다.

----------------------------------------
## 2. 저장소
   - DB SQL : https://github.com/hena-io/hena-aic-server/tree/master/db/sql/
   - 프로젝트 : https://github.com/hena-io/hena-aic-server/tree/master/src/
        - HenaLibrary : 공유 라이브러리 프로젝트
        - HenaWebsite : AIC 웹사이트 프로젝트
        - HenaSharedModels : 공유 데이터 모델 관리 프로젝트
        - HenaDatabaseLibrary : 데이터베이스 관련 관리 프로젝트
        - HenaSampleImageCreator : AIC 테스트용 이미지 생성 툴

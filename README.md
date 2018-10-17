# HENA AIC 서버 프로젝트
--------------------------------
## 1. 개발환경
- #### 웹사이트
    - ASP.NET Core 2.1
    - Visual Studio 2017 Community

- #### 데이터베이스
    - Windows Server 2016 Datacenter
    - MySQL Server 8.0

--------------------------------
## 2. 참고문서
- 데이터베이스 매뉴얼 : https://github.com/hena-io/hena-aic-server/blob/master/doc/db/database-manual.md

----------------------------------------
## 3. 저장소
   - DB SQL : https://github.com/hena-io/hena-aic-server/tree/master/db/sql/
   - 프로젝트 : https://github.com/hena-io/hena-aic-server/tree/master/src/
        - HenaLibrary : 공유 라이브러리 프로젝트
        - HenaWebsite : AIC 웹사이트 프로젝트
        - HenaSharedModels : 공유 데이터 모델 관리 프로젝트
        - HenaDatabaseLibrary : 데이터베이스 관련 관리 프로젝트
        - HenaSampleImageCreator : AIC 테스트용 이미지 생성 툴
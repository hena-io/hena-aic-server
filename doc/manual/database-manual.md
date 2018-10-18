# 데이터베이스 매뉴얼
----------------------------------------
## 1. 데이터베이스 설정
- #### 개발 환경
    - Windows Server 2016 Datacenter
    - MySQL Server 8.0
    
- #### MySQL 설치 후 필요한 설정
    
        데이터베이스의 타임존을 UTC 기준으로 변경.
        
        설정 파일 : C:\ProgramData\MySQL\MySQL Server 8.0\my.ini
        ---------------------------------------------------
        [mysqld]
            default-time-zone='+0:00'
        ---------------------------------------------------

----------------------------------------
## 2. 스키마 & 테이블

- #### `hena_aic_config`
    데이터 베이스 설정 또는 장비 ID를 관리하는 스키마    
    - `tbl_machine` : 장비 ID를 관리하는 테이블

- #### `hena_aic_log`
    로그를 관리하는 스키마
    - 현재는 테이블 없음.
        
- #### `hena_aic_report`
    통계관련 데이터를 관리하는 스키마
    - `tbl_ad_history` : 광고 요청, 노출, 클릭 기록을 관리.

- #### `hena_aic_service`
    AIC 홈페이지 및 서비스 관련된 데이터를 관리하는 스키마
    
    - `tbl_campaign` : 광고주가 발행한 캠페인을 관리
    - `tbl_ad_design` : 광고 디자인
    - `tbl_ad_resource` : 광고 리소스
  
    - `tbl_app` : 앱 관리
    - `tbl_ad_unit` : 광고 유닛
    
    - `tbl_balance` : 잔고 관리
    
    - `tbl_user` : 유저정보를 관리
    - `tbl_user_permission` : 유저별 권한을 관리

----------------------------------------
## 3. 저장소
   - DB SQL : https://github.com/hena-io/hena-aic-server/tree/master/db/sql/
   - 프로젝트 : https://github.com/hena-io/hena-aic-server/tree/master/src/
        - HenaLibrary : 공유 라이브러리 프로젝트
        - HenaWebsite : AIC 웹사이트 프로젝트
        - HenaSharedModels : 공유 데이터 모델 관리 프로젝트
        - HenaDatabaseLibrary : 데이터베이스 관련 관리 프로젝트
        - HenaSampleImageCreator : AIC 테스트용 이미지 생성 툴

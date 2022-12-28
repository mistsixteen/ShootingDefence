# ShootingDefence

## Unity3D Shooting-Defence Game Project

### PC용 3D Top-down Shooting game 개발 프로젝트

## 프로젝트 기본 구조
### Scene 구조 
 - AppInstance : Manager, System Class Object들이 포함된 Scene으로, 해당 Scene은 다른 Scene 로드시에도 언로드되지 않고 유지.
 
### Program 구조
 - AppInstance
 
   - ModelSystem : Inventory, Userdata 등 각종 Model들을 관리한다.
   - EventSystem : Event Listener을 등록하고, 해당 Event 발생 시 등록된 Action들을 실행한다.
   - UISystem : 각종 UI들을 등록/해제하여 관리한다.
   - Table Manager : CSV 테이블 데이터를 읽어와, TableRow 형태로 관리한다.
   
 - Common : Enum, Gloval Value 등, 공통으로 쓰이는 데이터들을 관리한다.
 - GameScene : 인게임에서 쓰이는 스크립트들을 관리한다
   - Character
   - Enemy
   - GamePlayState
   - Weapons
 - UI : 각 UI들의 Script들을 관리한다.
 
## 개발 기능 설명

### 적 AI 관리
- 추적, 공격, 원거리공격 등 FSM을 사용한 적의 AI 구현, 애니메이션 적용
- NavMesh를 이용한 플레이어 추적

### MVC 패턴을 사용한 게임 데이터 관리 
- EventListener을 사용하여, 해당 Model이 수정될 시 UI에 정보 변경 이벤트 메시지가 전달되도록 구현

### CSV 파일을 사용한 테이블 생성 및 아이템 정보 로드

### 회복, 아이템획득 등 아이템 오브젝트 구현

### Factory 패턴을 사용한 총알/적 오브젝트 생성

##### - Object Pooling을 사용한 리소스 관리


## 플레이 스크린샷

![Screenshot](https://user-images.githubusercontent.com/30260233/172665111-e5cac1b2-6bb1-4b53-8b71-bb381216fdb3.PNG)


## 기타 주의 사항

##### - 3D 모델의 용량/저작권 문제로 인하여, Resource 폴더는 Commit하고 있지 않음

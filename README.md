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
 - GameScene : Game Play Scene에서 쓰이는 스크립트들을 관리한다
   - Character
   - Enemy
   - GamePlayState
   - Weapons
 - UI : 각 UI들의 Script들을 관리한다.
 
## 개발 기능 설명

### Event 관리
- 성능을 향상시키기 위하여, BroadcastMessage() 대신 직접 구현한 EventSystem을 사용하였습니다. 해당 클래스는 이벤트 타입별 Action을 관리하여, 이벤트를 전달받는 객체가 직접 이벤트 수신 여부를 등록하게 합니다.
```C#
private Dictionary<EventType, UnityAction> eventDict;

public void RegistEventListener(EventType eventName, UnityAction eventAction)
{
	if(!eventDict.ContainsKey(eventName))
	{
		UnityAction newUnityAction = null;
		eventDict.Add(eventName, newUnityAction);
	}
	eventDict[eventName] += eventAction;
}
```

- 이벤트를 발생시키는 객체에서는, InvokeEvent 함수를 통하여 해당 이벤트가 발생하였다는 사실을 EventSystem에 통보하고, EventSystem은 등록된 Action을 호출합니다.
```C#
public void InvokeEvent(EventType eventName)
{
	if (eventDict.ContainsKey(eventName))
	{
		var unityAction = eventDict[eventName];
		unityAction?.Invoke();
	}
}
```

### FinitetateMachine을 사용한 게임 플레이 관리
  - 유한 상태 머신을 사용하여 현재 게임 플레이 상태(게임시작/낮/밤/게임오버 등)을 관리한다.
  - 각각의 상태는 추상 클래스를 사용하여 구현한 후, GameStateManager에서 모두 가지고 있는 형태로 관리한다.
```C#
public abstract class GamePlayState
{
    public StateGamePlay gameState;
    public virtual void StartState();
    public virtual void EndState();
    public virtual void UpdateState(float timeDelta);
}
...
public class GameStateManager : MonoBehaviour
{
	private Dictionary<StateGamePlay, GamePlayState> dictGameStates;
//중략
	dictGameStates = new Dictionary<StateGamePlay, GamePlayState>
	{
		{ StateGamePlay.StateGameReady, new GamePlayStateReady() as GamePlayState },
		{ StateGamePlay.StateGameDay, new GamePlayStateDay() as GamePlayState },
		{ StateGamePlay.StateGameNight, new GamePlayStateNight() as GamePlayState },
		{ StateGamePlay.StateGameOver, new GamePlayStateNight() as GamePlayState }
	};
```
### 적 AI 관리
- 추적, 공격, 원거리공격 등, FSM을 사용한 적의 AI 구현, 애니메이션 적용
- NavMesh를 이용한 플레이어 추적/공격

### MVC 패턴을 사용한 게임 데이터 관리
- Inventory, UserData 등, 각각의 모델이 해당 데이터 수정/저장을 전담. 
- EventListener을 사용하여, 해당 Model이 수정될 시 UI에 정보 변경 이벤트 메시지가 전달되도록 구현

### CSV 파일을 사용한 테이블 생성 및 아이템 정보 로드

### 회복, 아이템획득 등 아이템 오브젝트 구현

### Factory 패턴을 사용한 총알/적 오브젝트 생성

- Object Pooling을 사용한 리소스 관리

### Minimap 구현
 - 미니맵용 카메라를 별도로 정의하여, 카메라의 Culling mask 설정을 통하여 Minimap 표기용 Object만 렌더링되도록 처리한다.
 - 해당 카메라의 Render Texture을 UI에서 사용하여 미니맵을 표기한다.

## 플레이 스크린샷

![Screenshot](https://user-images.githubusercontent.com/30260233/209845479-ef24a58c-b837-476c-9072-c87652de8c13.PNG)


## 기타 주의 사항

##### - 3D 모델의 용량/저작권 문제로 인하여, Resource 관련 폴더는 Commit하고 있지 않음

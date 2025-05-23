# SpartaDungeonAdventure

## 1. 개요


## 2. 유니티 버전
Unity 2022.3.17f1

## 3. 추가 패키지
- Unity Input System
- TextMesh Pro

## 4. 실행 방법
1. **Unity Hub**에서 Unity 2022.3.17f1 버전으로 프로젝트를 엽니다
2. `Assets/Scenes` 폴더에서 메인 씬 `StartScene`을 열어 실행합니다

**플레이어 조작:**
- 이동: WASD
- 점프: Space
- 상호작용: E
- 인벤토리: TAB
- 다시시작: R

## 4. 프로젝트 구조
```
Assets/
  ├─ Animation/         # 애니메이션 컨트롤러 및 클립
  ├─ Data/              # 데이터 에셋
  ├─ Fonts/             # 폰트 리소스
  ├─ Input/             # 입력 시스템 설정
  ├─ Materials/         # 머티리얼
  ├─ Prefabs/           # 프리팹(환경, 상호작용, 아이템 등)
  ├─ Scenes/            # 씬 파일
  ├─ Scripts/           # 주요 C# 스크립트
  │   ├─ Core/          # 싱글턴, 게임 매니저 등
  │   ├─ UI/            # UI 관리 및 인벤토리
  │   ├─ Player/        # 플레이어 이동, 상호작용, 카메라
  │   ├─ Item/          # 아이템 데이터 및 효과
  │   ├─ Interaction/   # 상호작용 오브젝트(상자, 점프패드 등)
  │   └─ Sound/         # 사운드 매니저
  ├─ Sounds/            # 효과음
  ├─ Sprite/            # 2D 스프라이트
  ├─ TextMesh Pro/      # 텍스트 메쉬 프로 리소스
  └─ Textures/          # 텍스처
  ```

## 6. 주요 시스템 및 기능
### 1) 게임 매니저 및 싱글턴 구조
**GameManager:** 게임의 전역 상태(플레이어, 인벤토리 등) 관리. 씬 리스타트 기능 제공
**Singleton<T>:** 모든 매니저 클래스에 적용되는 싱글턴 패턴

### 2) 플레이어 시스템
**PlayerController:**
- Unity Input System 사용하여 이동, 점프 등 조작
- 체력/스태미나 관리, 지면 체크 등

**PlayerRaycaster:**
- 플레이어 시점에서 상호작용/클라이밍 오브젝트 감지
- 레이캐스트로 상호작용 가능 오브젝트 탐지 및 UI 표시

**ThirdPersonCameraController:**
- 3인칭 카메라 이동 및 회전 제어

![게임 시작 및 3인칭 카메라 시점 화면](./Screenshots/game.png)

게임 시작 및 3인칭 카메라 시점 화면

### 3) 상호작용 오브젝트
**Chest (상자):**

![상자](./Screenshots/chest.png)

- 플레이어가 상호작용(E키) 시 열고 닫을 수 있음
- 아이템 보관 및 획득, 애니메이션 연동, 인벤토리 UI 표시

**JumpBlock (점프 블록):**

![점프블록](./Screenshots/jump_pad.png)

플레이어가 닿으면 강한 점프력 부여

**DisappearBlock (사라지는 블록):**

![사라지는 블록](./Screenshots/disappear.png)

- 플레이어가 밟으면 일정 시간 후 사라짐 (사라진 일정 시간 후 다시 생성)

**MovingBlock (이동 블록):**
- 여러개의 Waypoint를 지정해 순차적으로 이동시키는 블록

### 4) 아이템 시스템
**ItemData (ScriptableObject):**
- 아이템 이름, 설명, 아이콘, 타입(장비/소비), 효과 리스트 등

**ItemEffect (ScriptableObject):**
- 체력 회복, 스피드 증가 등 다양한 효과를 `coroutine`으로 적용/해제.

**ItemManager:**
- 아이템 관리

### 5) UI 시스템
**UIManager:**
- 체력/스태미나 바, 상호작용 프롬프트, 인벤토리 UI 등 관리.

**UIPlayerInventory, UIChestInventory:**
- 플레이어/상자 인벤토리 표시.

![플레이어 인벤토리 UI](./Screenshots/player_inventory.png)

**UIInteraction:**
- 상호작용 프롬프트 표시.

![상호작용 프롬프트](./Screenshots/interaction_ui.png)

### 6) 사운드 시스템
**SoundManager:**
- 효과음 재생 및 관리(점프, 발소리 등).




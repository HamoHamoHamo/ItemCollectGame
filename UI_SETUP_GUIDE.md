# UI 설정 가이드

이 가이드는 Unity Editor에서 UI를 설정하는 방법을 안내합니다.

## 1. Canvas 및 EventSystem 추가

1. **Hierarchy**에서 우클릭 → **UI** → **Canvas** 선택
2. **Canvas** 설정:
   - Render Mode: `Screen Space - Overlay`
   - UI Scale Mode: `Scale With Screen Size`
   - Reference Resolution: `1920 x 1080`
3. **EventSystem**이 자동으로 생성됩니다 (없으면 우클릭 → UI → Event System)

---

## 2. 인게임 UI 생성

### 2.1 아이템 카운트 텍스트 (좌상단)

1. **Canvas** 우클릭 → **UI** → **Text - TextMeshPro** 선택
2. 이름을 `ItemCountText`로 변경
3. **TextMeshPro - Text (UI)** 컴포넌트 설정:
   - Text: `0 / 20`
   - Font Size: `36`
   - Color: 흰색 또는 원하는 색상
   - Alignment: 좌측 상단
4. **RectTransform** 설정:
   - Anchor Presets: 좌상단 (Left-Top)
   - Pos X: `20`, Pos Y: `-20`
   - Width: `200`, Height: `50`

### 2.2 타이머 텍스트 (상단 중앙)

1. **Canvas** 우클릭 → **UI** → **Text - TextMeshPro** 선택
2. 이름을 `TimerText`로 변경
3. **TextMeshPro - Text (UI)** 컴포넌트 설정:
   - Text: `00:00`
   - Font Size: `48`
   - Color: 흰색 또는 원하는 색상
   - Alignment: 중앙 정렬
4. **RectTransform** 설정:
   - Anchor Presets: 상단 중앙 (Top-Center)
   - Pos X: `0`, Pos Y: `-20`
   - Width: `150`, Height: `60`

---

## 3. 게임 종료 패널 생성

### 3.1 패널 배경

1. **Canvas** 우클릭 → **UI** → **Panel** 선택
2. 이름을 `GameEndPanel`로 변경
3. **Image** 컴포넌트 설정:
   - Color: 반투명 검정색 (R:0, G:0, B:0, A:200)
4. **RectTransform** 설정:
   - Anchor Presets: Stretch (가운데 사각형 아이콘)
   - Left: `0`, Right: `0`, Top: `0`, Bottom: `0`

### 3.2 게임 클리어 타이틀

1. **GameEndPanel** 우클릭 → **UI** → **Text - TextMeshPro** 선택
2. 이름을 `TitleText`로 변경
3. **TextMeshPro - Text (UI)** 컴포넌트 설정:
   - Text: `게임 클리어!`
   - Font Size: `72`
   - Color: 노란색 또는 원하는 색상
   - Alignment: 중앙 정렬
4. **RectTransform** 설정:
   - Anchor Presets: 상단 중앙
   - Pos X: `0`, Pos Y: `-150`
   - Width: `600`, Height: `100`

### 3.3 클리어 시간 텍스트

1. **GameEndPanel** 우클릭 → **UI** → **Text - TextMeshPro** 선택
2. 이름을 `ClearTimeText`로 변경
3. **TextMeshPro - Text (UI)** 컴포넌트 설정:
   - Text: `클리어 시간: 00:00`
   - Font Size: `42`
   - Color: 흰색
   - Alignment: 중앙 정렬
4. **RectTransform** 설정:
   - Anchor Presets: 중앙
   - Pos X: `0`, Pos Y: `50`
   - Width: `500`, Height: `60`

### 3.4 최단 기록 텍스트

1. **GameEndPanel** 우클릭 → **UI** → **Text - TextMeshPro** 선택
2. 이름을 `BestTimeText`로 변경
3. **TextMeshPro - Text (UI)** 컴포넌트 설정:
   - Text: `최단기록: --:--`
   - Font Size: `36`
   - Color: 금색 또는 원하는 색상
   - Alignment: 중앙 정렬
4. **RectTransform** 설정:
   - Anchor Presets: 중앙
   - Pos X: `0`, Pos Y: `-20`
   - Width: `500`, Height: `50`

### 3.5 다시하기 버튼

1. **GameEndPanel** 우클릭 → **UI** → **Button - TextMeshPro** 선택
2. 이름을 `RestartButton`으로 변경
3. **Button** 컴포넌트는 기본 설정 유지
4. 자식 오브젝트 **Text (TMP)** 설정:
   - Text: `다시하기`
   - Font Size: `32`
   - Color: 흰색
5. **RectTransform** 설정:
   - Anchor Presets: 중앙
   - Pos X: `-120`, Pos Y: `-150`
   - Width: `200`, Height: `60`

### 3.6 게임 종료 버튼

1. **GameEndPanel** 우클릭 → **UI** → **Button - TextMeshPro** 선택
2. 이름을 `QuitButton`으로 변경
3. **Button** 컴포넌트는 기본 설정 유지
4. 자식 오브젝트 **Text (TMP)** 설정:
   - Text: `게임 종료`
   - Font Size: `32`
   - Color: 흰색
5. **RectTransform** 설정:
   - Anchor Presets: 중앙
   - Pos X: `120`, Pos Y: `-150`
   - Width: `200`, Height: `60`

---

## 4. GameManager 오브젝트 추가

1. **Hierarchy**에서 우클릭 → **Create Empty** 선택
2. 이름을 `GameManager`로 변경
3. **Inspector**에서 **Add Component** 클릭
4. `GameManager` 스크립트를 추가
5. **Target Item Count**를 `20`으로 설정

---

## 5. UIManager 연결

1. **Hierarchy**에서 **UIManager** 오브젝트를 찾거나 새로 생성
2. **UIManager** 스크립트의 Inspector에서 다음 필드를 연결:
   - **Item Count Text**: `ItemCountText` 오브젝트를 드래그
   - **Timer Text**: `TimerText` 오브젝트를 드래그
   - **Game End Panel**: `GameEndPanel` 오브젝트를 드래그
   - **Clear Time Text**: `ClearTimeText` 오브젝트를 드래그
   - **Best Time Text**: `BestTimeText` 오브젝트를 드래그
   - **Restart Button**: `RestartButton` 오브젝트를 드래그
   - **Quit Button**: `QuitButton` 오브젝트를 드래그

---

## 6. Item Prefab 설정

1. **Project** 창에서 Item Prefab을 찾아서 선택
2. **Add Component** → **Box Collider** (또는 **Sphere Collider**)
3. **Collider** 설정:
   - **Is Trigger**: ✓ 체크
   - Size/Radius를 적절하게 조정
4. Item에 **Tag**를 설정하지 않았다면 기본값 유지
5. Prefab을 저장

---

## 7. Player 설정

1. **Hierarchy**에서 **Player** 오브젝트 선택
2. **Inspector** 상단에서 **Tag**를 `Player`로 설정
   - 만약 Player 태그가 없다면:
     - Tag 드롭다운 → **Add Tag** 클릭
     - **Tags** 섹션에서 `+` 버튼 클릭
     - `Player` 입력 후 Save
     - 다시 Player 오브젝트로 돌아가서 Tag를 Player로 설정

---

## 8. ItemSpawner Prefab 설정

1. **Project** 창에서 ItemSpawner가 사용하는 Item Prefab 선택
2. **Inspector**에서:
   - Item Prefab 필드에 Item 오브젝트가 연결되어 있는지 확인
   - Item 오브젝트에 **Item.cs** 스크립트가 있는지 확인

---

## 9. 최종 확인

1. **GameEndPanel**을 선택하고 **Inspector**에서 비활성화 (좌측 상단 체크박스 해제)
2. **Play** 버튼을 눌러 게임 테스트:
   - 좌상단에 "0 / 20" 표시 확인
   - 상단 중앙에 "00:00" 타이머 작동 확인
   - 아이템을 수집하면 카운트 증가 확인
   - 20개 수집 시 게임 종료 패널 표시 확인
   - 버튼 클릭 동작 확인

---

## 추가 팁

### TextMeshPro 폰트 임포트 안내
- 처음 TextMeshPro를 사용하면 "TMP Importer" 창이 나타날 수 있습니다
- **Import TMP Essentials** 버튼을 클릭하여 기본 리소스를 임포트하세요

### 한글 폰트 사용
- 한글이 제대로 표시되지 않으면:
  1. 한글 폰트 파일(.ttf, .otf)을 프로젝트에 임포트
  2. Window → TextMeshPro → Font Asset Creator
  3. Source Font File에 한글 폰트 선택
  4. Character Set: Unicode Range (Hex) 또는 Characters from File
  5. Generate Font Atlas 클릭
  6. Save를 눌러 Font Asset 저장
  7. TextMeshPro 컴포넌트의 Font Asset에 새로 생성한 폰트 적용

---

완료되었습니다! 게임을 실행하고 테스트해보세요.

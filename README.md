# EventBus

설명:

UI 제어시 사용하는 이벤트 버스 예제입니다.

Windows 어플리케이션의 메세지 루프나 Flux 패턴 비슷한 느낌으로 작동합니다.


# 전반적인 흐름

1) 데이터 컨테이너에서 값 변경시 이벤트를 발생시킵니다.
2) 발생한 이벤트는 GameEventDispatcher 를 통하여 UIController로 이동합니다.
3) UIController에서 수신한 이벤트를 현재 열려있는 모든 UI로 브로드캐스팅 합니다.
4) 각 UI 클래스에서 수신받은 이벤트에 따라 기능을 구현합니다.

## 클래스 설명 (데이터 컨테이너)

UserDataContainerBase:
 데이터 컨테이너 클래스입니다. 내부적으론 Dictionary 형태로 관리됩니다. 상속받은 클래스에서, 값 변경시 특정한 이벤트를 발생시킵니다.

UserDataContainer:
 데이터 컨테이너들이 모여있는 클래스입니다. 장비나 캐릭터 정보가 있습니다.



## 클래스 설명 (이벤트)

GameEventBase:
 이벤트 클래스입니다.

```
예시:
// 캐릭터 레벨 변경시 이벤트입니다.
// 데이터 컨테이너에서 값 변경시 이 이벤트를 발생시킵니다.
public class GameEventCharacterLevelChange : GameEventBase
{
    private ulong UUID = 0;
    private uint LvChange = 0;
    public GameEventCharacterLevelChange Set(UserCharacterData data, uint prevLv)
    {
        UUID = data.UUID;
        LvChange = data.Lv - prevLv;
        return this;
    }

    public ulong GetUUID() => UUID;
    public uint GetLvChange() => LvChange;
}


## 클래스 설명 (디스패치)

GameEventDispatcher:
 발생된 이벤트는 모두 이 클래스를 거칩니다. UI 컨트롤러의 이벤트 수신 메소드를 호출합니다.
 

## 클래스 설명 (UI)

UIController:
 열려있는 모든 UI로 이벤트를 전파합니다.
 


## License

[MIT](https://choosealicense.com/licenses/mit/)

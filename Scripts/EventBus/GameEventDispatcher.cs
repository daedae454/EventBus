/// <summary>
/// Windows 프로그램의 Dispatcher 역할을 하는 클래스입니다. 뭔가 이벤트 생기면 UIController에 알려줍니다.
/// </summary>
public class GameEventDispatcher
{
    public static GameEventDispatcher I
    {
        get
        {
            if (_inst == null)
                _inst = new GameEventDispatcher();
            return _inst;
        }
    }

    private static GameEventDispatcher _inst;

    public void DispatchEvent(GameEventBase ev)
    {
        UIController.I.GameEventDispatch(ev);
    }
}
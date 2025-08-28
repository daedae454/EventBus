using UnityEngine;
using UnityEngine.UI;

/// <summary>UI 열 때 사용하는 파라미터 베이스 클래스.</summary>
public class UIOpenParamBase
{
}

public class UIBase : MonoBehaviour
{
    [SerializeField] protected Button btnClose;

    protected virtual void Awake()
    {
        if (btnClose != null)
        {
            btnClose.onClick.RemoveAllListeners();
            btnClose.onClick.AddListener(Close);
        }
    }

    /// <summary>UI를 열 때 호출됩니다.</summary>
    /// <param name="p">특정 파라미터가 필요한 경우 사용합니다.</param>
    public virtual void Open(UIOpenParamBase p = null)
    {
        gameObject.SetActive(true);
    }

    /// <summary>UI를 숨길때 호출됩니다. (파괴 X)</summary>
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    #region Game Events

    /// <summary>Flux 액션 발생시 호출됩니다. 각 이벤트에 맞게 UI를 갱신하세요!</summary>
    /// <param name="ev">게임 이벤트.</param>
    public virtual void OnGameEventDispatch(GameEventBase ev)
    {
    }

    #endregion Flux
}
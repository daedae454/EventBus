using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Singleton

    private static UIController _inst;

    public static UIController I
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindFirstObjectByType<UIController>();
                if (_inst == null)
                {
                    GameObject obj = new GameObject("UIController");
                    _inst = obj.AddComponent<UIController>();
                }
            }
            return _inst;
        }
    }

    #endregion Singleton

    private List<UIBase> listUI = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterUI(UIBase ui)
    {
        if (!listUI.Contains(ui))
        {
            listUI.Add(ui);
        }
    }

    #region Game Events

    public void GameEventDispatch(GameEventBase ev)
    {
        for (int i = 0; i < listUI.Count; i++)
        {
            listUI[i]?.OnGameEventDispatch(ev);
        }
    }

    #endregion Game Events
}
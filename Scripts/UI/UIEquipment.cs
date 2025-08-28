using UnityEngine;
using TMPro;

public class UIEquipment : UIBase
{
    [SerializeField] private TMP_Text txtLevel;
    [SerializeField] private TMP_Text txtLevel2;

    #region TEST_DATA

    private ulong uuid1 = 1001;
    private ulong uuid2 = 1002;

    #endregion TEST_DATA

    public override void Open(UIOpenParamBase p = null)
    {
        base.Open(p);
        RefreshAll(uuid1);
        RefreshAll(uuid2);
    }

    private void RefreshAll(ulong uuid)
    {
        var data = GetEquipmentData(uuid);
        if (data != null)
        {
            SetLevel(data.Lv);
        }
    }

    private void SetLevel(uint level)
    {
        txtLevel.text = level.ToString();
    }

    private UserEquipmentData GetEquipmentData(ulong uuid) => UserDataContainer.I.Equipment.GetData(uuid);

    #region Game Events

    public override void OnGameEventDispatch(GameEventBase ev)
    {
        // 장비 레벨 상승.
        if (ev is GameEventEquipmentLevelChange)
        {
            var store = (GameEventEquipmentLevelChange)ev;
            RefreshAll(store.GetUUID());
        }
    }

    #endregion Game Events
}
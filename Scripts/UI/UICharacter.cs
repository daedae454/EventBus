using UnityEngine;
using TMPro;

public class UICharacter : UIBase
{
    [SerializeField] private TMP_Text txtLevel;
    [SerializeField] private TMP_Text txtExp;
    [SerializeField] private TMP_Text txtEquipLevel;

    private ulong uuid = 0;

    public class UIPARAM : UIOpenParamBase
    {
        public ulong UUID = 0;
    }

    public override void Open(UIOpenParamBase p)
    {
        base.Open(p);
        if (p != null)
        {
            UIPARAM param = (UIPARAM)p;
            uuid = param.UUID;
            RefreshAll(param.UUID);
        }
    }

    private void RefreshAll(ulong uuid)
    {
        var data = GetCharacterData(uuid);
        if (data != null)
        {
            SetLevel(data.Lv);
            SetExp(data.Exp);
            SetEquipLevel(data.EquipUUID);
        }
    }

    private void SetLevel(uint level)
    {
        txtLevel.text = level.ToString();
    }

    private void SetExp(ulong exp)
    {
        txtExp.text = exp.ToString();
    }

    private void SetEquipLevel(ulong equipUUID)
    {
        var equipData = UserDataContainer.I.Equipment.GetData(equipUUID);
        if (equipData != null)
        {
            txtEquipLevel.text = equipData.Lv.ToString();
        }
        else
        {
            txtEquipLevel.text = "-";
        }
    }

    private UserCharacterData GetCharacterData(ulong uuid) => UserDataContainer.I.Character.GetData(uuid);

    #region Game Events

    public override void OnGameEventDispatch(GameEventBase ev)
    {
        // 캐릭터 추가.
        if (ev is GameEventCharacterAdd)
        {
            var store = (GameEventCharacterAdd)ev;
            RefreshAll(store.GetUUID());
        }

        // 캐릭터 레벨 추가.
        if (ev is GameEventCharacterLevelChange)
        {
            var store = (GameEventCharacterLevelChange)ev;
            var character = GetCharacterData(store.GetUUID());
            // 현재 레벨.
            //character.Lv
            // 레벨 상승된 수치(여러 개 올라갈 수 있습니다 - 연출에 사용!)
            //store.GetLvChange()
            SetLevel(character.Lv);
        }

        // 캐럭터 경험치 증감.
        if (ev is GameEventCharacterExpChange)
        {
            var store = (GameEventCharacterExpChange)ev;
            var character = GetCharacterData(store.GetUUID());
            // 현재 경함치.
            //character.Exp
            // 경험치 추가된 수치 (연출에 사용!)
            //store.GetExpChange();
            SetExp(character.Exp);
        }
    }

    #endregion Game Events
}
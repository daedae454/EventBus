using System.Collections;
using System.Collections.Generic;

public class UserEquipmentData : UserDataBase
{
    public ushort Lv { set; get; } // 장비 레벨.
}

public class UserEquipmentDataContainer : UserDataContainerBase<UserEquipmentData>
{
    public UserEquipmentData GetData(ulong uuid)
    {
        if (dictData.ContainsKey(uuid))
            return dictData[uuid];
        return null;
    }

    public void ForEach(System.Action<UserEquipmentData> action)
    {
        foreach (var item in dictData.Values)
        {
            action?.Invoke(item);
        }
    }

    public override void Upsert(UserEquipmentData data, bool ignoreEvent = false)
    {
        var prevData = GetData(data.UUID);
        uint prevLV = prevData?.Lv ?? 0;

        base.Upsert(data);

        if (!ignoreEvent)
        {
            if(prevData == null)
            {
                GameEventDispatcher.I.DispatchEvent(new GameEventEquipmentAdd().Set(data));
                return;
            }

            var currentData = GetData(data.UUID);
            if (prevData != null)
            {
                if (prevData.Lv != currentData.Lv)
                    GameEventDispatcher.I.DispatchEvent(new GameEventEquipmentLevelChange().Set(data, prevLV));
            }
        }
    }

    public void LevelAdd(ulong uuid, ushort add)
    {
        var data = GetData(uuid);
        var prevLv = data?.Lv ?? 0;
        if (data != null)
        {
            data.Lv += add;
            GameEventDispatcher.I.DispatchEvent(new GameEventEquipmentLevelChange().Set(data, prevLv));
        }
    }
}

#region Game Events


/// <summary>장비를 새로 얻은 경우.</summary>
public class GameEventEquipmentAdd : GameEventBase
{
    private ulong UUID = 0;

    public GameEventEquipmentAdd Set(UserEquipmentData data)
    {
        UUID = data.UUID;
        return this;
    }

    public ulong GetUUID() => UUID;
}

/// <summary>장비의 레벨이 변경된 경우.</summary>
public class GameEventEquipmentLevelChange : GameEventBase
{
    private ulong UUID = 0;
    private uint LvChange = 0;
    public GameEventEquipmentLevelChange Set(UserEquipmentData data, uint prevLv)
    {
        UUID = data.UUID;
        LvChange = data.Lv - prevLv;
        return this;
    }

    public ulong GetUUID() => UUID;
    public uint GetLvChange() => LvChange;
}


#endregion
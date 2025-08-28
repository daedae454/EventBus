public class UserCharacterData : UserDataBase
{
    public uint Lv;
    public ulong Exp;
    public ulong EquipUUID;
}

public class UserCharacterDataContainer : UserDataContainerBase<UserCharacterData>
{
    public UserCharacterData GetData(ulong uuid)
    {
        if (dictData.ContainsKey(uuid))
            return dictData[uuid];
        return null;
    }

    public override void Upsert(UserCharacterData data, bool ignoreEvent = false)
    {
        var prevData = GetData(data.UUID);
        uint prevLV = prevData?.Lv ?? 0;
        ulong prevEXP = prevData?.Exp ?? 0;

        base.Upsert(data);

        if (!ignoreEvent)
        {
            if(prevData == null)
            {
                GameEventDispatcher.I.DispatchEvent(new GameEventCharacterAdd().Set(data));
                return;
            }

            var currentData = GetData(data.UUID);
            if (prevData != null)
            {
                if(prevData.Lv != currentData.Lv)
                    GameEventDispatcher.I.DispatchEvent(new GameEventCharacterLevelChange().Set(data, prevLV));
                if(prevData.Exp != currentData.Exp)
                    GameEventDispatcher.I.DispatchEvent(new GameEventCharacterExpChange().Set(data, prevEXP));
            }
        }
    }

    public void LevelAdd(ulong uuid, uint add)
    {
        var data = GetData(uuid);
        var prevLv = data?.Lv ?? 0;
        if (data != null)
        {
            data.Lv += add;
            GameEventDispatcher.I.DispatchEvent(new GameEventCharacterLevelChange().Set(data, prevLv));
        }
    }

    public void ExpAdd(ulong uuid, ulong add)
    {
        var data = GetData(uuid);
        var prevExp = data?.Exp ?? 0;
        if (data != null)
        {
            data.Exp += add;
            GameEventDispatcher.I.DispatchEvent(new GameEventCharacterExpChange().Set(data, prevExp));
        }
    }
}

#region Game Events

/// <summary>캐릭터를 새로 얻은 경우.</summary>
public class GameEventCharacterAdd : GameEventBase
{
    private ulong UUID = 0;

    public GameEventCharacterAdd Set(UserCharacterData data)
    {
        UUID = data.UUID;
        return this;
    }

    public ulong GetUUID() => UUID;
}

/// <summary>캐릭터의 레벨이 변경된 경우.</summary>
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

/// <summary>캐릭터의 경험치가 변경된 경우.</summary>
public class GameEventCharacterExpChange : GameEventBase
{
    private ulong UUID = 0;
    private ulong ExpChange = 0;

    public GameEventCharacterExpChange Set(UserCharacterData data, ulong prevExp)
    {
        UUID = data.UUID;
        ExpChange = data.Exp - prevExp;
        return this;
    }

    public ulong GetUUID() => UUID;
    public ulong GetExpChange() => ExpChange;
}

#endregion
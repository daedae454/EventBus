using System.Collections.Generic;

public class UserDataBase
{
    public ulong UUID; // db상 고유 id. 변경하지 마세요.
}

/// <summary>
/// 데이터 컨테이너 클래스입니다.
/// <para>참고- 자식 클래스의 데이터 갱신시(주로 Upsert) Game Event를 발생시키세요!</para>
/// </summary>
public class UserDataContainerBase<T> where T : UserDataBase, new()
{
    protected Dictionary<ulong, T> dictData { private set; get; } = new Dictionary<ulong, T>();

    public virtual void Add(T data)
    {
        if (!dictData.ContainsKey(data.UUID))
            dictData.Add(data.UUID, data);
    }

    public virtual bool Remove(T data)
    {
        bool retval = false;

        if (dictData.ContainsKey(data.UUID))
        {
            dictData.Remove(data.UUID);
            retval = true;
        }

        return retval;
    }

    /// <summary>주로 데이터 갱신시 호출됩니다.</summary>
    /// <param name="data">변경된 데이터.</param>
    /// <param name="ignoreGameEvent">게임 이벤트 발동 무시 여부.</param>
    public virtual void Upsert(T data, bool ignoreGameEvent = false)
    {
        if (dictData.ContainsKey(data.UUID))
        {
            dictData[data.UUID] = data;
        }
        else
        {
            Add(data);
        }
    }
}
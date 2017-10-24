using System;

//鍙互鐩戝惉鍙樺寲锛屾湁鍐呭瓨鍔犲瘑
public class VarInt : VarBase<int>
{
    public VarInt()
    {
        Set(0);
    }

    public VarInt(int Value)
    {
        Set(Value);
    }

    protected override int Var_Get(long Value)
    {
        return (int)(((ulong)((Value ^ 0x12345678L) ^ _tick)) & 0xffffffffL);
    }

    protected override long Var_Set(int Value)
    {
        return (long)((Value ^ 0x12345678) ^ ((int)(((ulong)_tick) & 0xffffffffL)));
    }
}


using System;

//鍙互鐩戝惉鍙樺寲锛屾湁鍐呭瓨鍔犲瘑
public class VarLong : VarBase<long>
{
    public VarLong()
    {
        Set(0);
    }

    public VarLong(long Value)
    {
        Set(Value);
    }

    protected override long Var_Get(long Value)
    {
        return ((Value ^ 0x12345678L) ^ _tick);
    }

    protected override long Var_Set(long Value)
    {
        return ((Value ^ 0x12345678L) ^ _tick);
    }
}


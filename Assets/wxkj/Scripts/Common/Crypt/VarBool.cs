using System;

//鍙互鐩戝惉鍙樺寲锛屾湁鍐呭瓨鍔犲瘑
public class VarBool : VarBase<bool>
{
    public VarBool()
    {
        Set(false);
    }

    public VarBool(bool Value)
    {
        Set(Value);
    }

    protected sealed override bool Var_Get(long Value)
    {
        return (((Value ^ 0x12345678L) ^ _tick) == 1L);
    }

    protected sealed override long Var_Set(bool Value)
    {
        return (((!Value ? 0 : 1) ^ 0x12345678L) ^ _tick);
    }
}


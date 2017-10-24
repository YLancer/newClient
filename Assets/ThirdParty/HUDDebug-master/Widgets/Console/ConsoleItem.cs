
using System;
using Playflock.Log.Widget;

public struct ConsoleItem
{

    #region CONSTRUCTORS

    public ConsoleItem( string message, string stackTrace, EConsoleType type, ConsoleWidget.OutputColor color )
    {
        _message = message;
        _stripMessage = message.Substring(0, Math.Min(message.Length, MESSAGE_LENGTH_LIMIT));
        _stackTrace = stackTrace;
        //_type = type;
        switch ( type )
        {
            case EConsoleType.Log:
                _color = color.Log;
                _type = EConsoleType.Log;
                break;
            case EConsoleType.Assert:
                _color = color.Error;
                _type = EConsoleType.Error;
                break;
            case EConsoleType.Warning:
                _color = color.Warning;
                _type = EConsoleType.Warning;
                break;
            case EConsoleType.Error:
                _color = color.Error;
                _type = EConsoleType.Error;
                break;
            case EConsoleType.Exception:
                _color = color.Error;
                _type = EConsoleType.Error;
                break;
            default:
                _color = color.Log;
                _type = EConsoleType.Log;
                break;
        }
    }

    #endregion

    #region PUBLIC_VARIABLES

    public string Message
    {
        get { return _message; }
    }

    public string StripMessage
    {
        get { return _stripMessage; }
    }

    public string StackTrace
    {
        get { return _stackTrace; }
    }

    public EConsoleType Type
    {
        get { return _type; }
    }

    public UnityEngine.Color Color
    {
        get { return _color; }
    }

    #endregion

    #region PRIVATE_VARIABLES

    private readonly string _message;
    private readonly string _stripMessage;
    private readonly string _stackTrace;
    private readonly EConsoleType _type;
    private readonly UnityEngine.Color _color;
    private const int MESSAGE_LENGTH_LIMIT = 200;

    #endregion


}

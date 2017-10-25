//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Assets/ProtoBuffMsg/MJ.proto
namespace packet.mj
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperStartSyn")]
  public partial class GameOperStartSyn : global::ProtoBuf.IExtensible
  {
    public GameOperStartSyn() {}
    
    private int _bankerPos = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"bankerPos", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int bankerPos
    {
      get { return _bankerPos; }
      set { _bankerPos = value; }
    }
    private int _serviceGold;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"serviceGold", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int serviceGold
    {
      get { return _serviceGold; }
      set { _serviceGold = value; }
    }
    private readonly global::System.Collections.Generic.List<packet.mj.GameOperHandCardSyn> _playerHandCards = new global::System.Collections.Generic.List<packet.mj.GameOperHandCardSyn>();
    [global::ProtoBuf.ProtoMember(3, Name=@"playerHandCards", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<packet.mj.GameOperHandCardSyn> playerHandCards
    {
      get { return _playerHandCards; }
    }
  
    private int _quanNum = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"quanNum", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int quanNum
    {
      get { return _quanNum; }
      set { _quanNum = value; }
    }
    private bool _bankerContinue = default(bool);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"bankerContinue", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool bankerContinue
    {
      get { return _bankerContinue; }
      set { _bankerContinue = value; }
    }
    private int _dice1 = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"dice1", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int dice1
    {
      get { return _dice1; }
      set { _dice1 = value; }
    }
    private int _dice2 = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"dice2", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int dice2
    {
      get { return _dice2; }
      set { _dice2 = value; }
    }
    private int _seq = default(int);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"seq", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int seq
    {
      get { return _seq; }
      set { _seq = value; }
    }
    private bool _reconnect = default(bool);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"reconnect", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool reconnect
    {
      get { return _reconnect; }
      set { _reconnect = value; }
    }
    private int _cardLeft = default(int);
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"cardLeft", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int cardLeft
    {
      get { return _cardLeft; }
      set { _cardLeft = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperHandCardSyn")]
  public partial class GameOperHandCardSyn : global::ProtoBuf.IExtensible
  {
    public GameOperHandCardSyn() {}
    
    private int _position;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int position
    {
      get { return _position; }
      set { _position = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _handCards = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(2, Name=@"handCards", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public global::System.Collections.Generic.List<int> handCards
    {
      get { return _handCards; }
    }
  
    private readonly global::System.Collections.Generic.List<int> _downCards = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(3, Name=@"downCards", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public global::System.Collections.Generic.List<int> downCards
    {
      get { return _downCards; }
    }
  
    private readonly global::System.Collections.Generic.List<int> _cardsBefore = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(4, Name=@"cardsBefore", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public global::System.Collections.Generic.List<int> cardsBefore
    {
      get { return _cardsBefore; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperPublicInfoSyn")]
  public partial class GameOperPublicInfoSyn : global::ProtoBuf.IExtensible
  {
    public GameOperPublicInfoSyn() {}
    
    private int _cardLeft;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"cardLeft", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int cardLeft
    {
      get { return _cardLeft; }
      set { _cardLeft = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperActorSyn")]
  public partial class GameOperActorSyn : global::ProtoBuf.IExtensible
  {
    public GameOperActorSyn() {}
    
    private int _position;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int position
    {
      get { return _position; }
      set { _position = value; }
    }
    private int _timeLeft;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"timeLeft", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int timeLeft
    {
      get { return _timeLeft; }
      set { _timeLeft = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperPlayerActionSyn")]
  public partial class GameOperPlayerActionSyn : global::ProtoBuf.IExtensible
  {
    public GameOperPlayerActionSyn() {}
    
    private int _position;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int position
    {
      get { return _position; }
      set { _position = value; }
    }
    private int _action;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"action", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int action
    {
      get { return _action; }
      set { _action = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _cardValue = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(3, Name=@"cardValue", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public global::System.Collections.Generic.List<int> cardValue
    {
      get { return _cardValue; }
    }
  
    private int _seq = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"seq", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int seq
    {
      get { return _seq; }
      set { _seq = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperPlayerActionNotify")]
  public partial class GameOperPlayerActionNotify : global::ProtoBuf.IExtensible
  {
    public GameOperPlayerActionNotify() {}
    
    private int _position;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int position
    {
      get { return _position; }
      set { _position = value; }
    }
    private int _actions;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"actions", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int actions
    {
      get { return _actions; }
      set { _actions = value; }
    }
    private int _newCard = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"newCard", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int newCard
    {
      get { return _newCard; }
      set { _newCard = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _tingList = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(4, Name=@"tingList", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public global::System.Collections.Generic.List<int> tingList
    {
      get { return _tingList; }
    }
  
    private readonly global::System.Collections.Generic.List<int> _tingDzs = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(5, Name=@"tingDzs", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public global::System.Collections.Generic.List<int> tingDzs
    {
      get { return _tingDzs; }
    }
  
    private readonly global::System.Collections.Generic.List<packet.mj.GameOperChiArg> _chiArg = new global::System.Collections.Generic.List<packet.mj.GameOperChiArg>();
    [global::ProtoBuf.ProtoMember(6, Name=@"chiArg", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<packet.mj.GameOperChiArg> chiArg
    {
      get { return _chiArg; }
    }
  
    private int _pengArg = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"pengArg", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int pengArg
    {
      get { return _pengArg; }
      set { _pengArg = value; }
    }
    private int _lastActionPosition = default(int);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"lastActionPosition", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int lastActionPosition
    {
      get { return _lastActionPosition; }
      set { _lastActionPosition = value; }
    }
    private int _lastActionCard = default(int);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"lastActionCard", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int lastActionCard
    {
      get { return _lastActionCard; }
      set { _lastActionCard = value; }
    }
    private int _seq = default(int);
    [global::ProtoBuf.ProtoMember(100, IsRequired = false, Name=@"seq", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int seq
    {
      get { return _seq; }
      set { _seq = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperAutoChuArg")]
  public partial class GameOperAutoChuArg : global::ProtoBuf.IExtensible
  {
    public GameOperAutoChuArg() {}
    
    private int _card;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"card", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int card
    {
      get { return _card; }
      set { _card = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperChiArg")]
  public partial class GameOperChiArg : global::ProtoBuf.IExtensible
  {
    public GameOperChiArg() {}
    
    private int _myCard1;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"myCard1", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int myCard1
    {
      get { return _myCard1; }
      set { _myCard1 = value; }
    }
    private int _myCard2;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"myCard2", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int myCard2
    {
      get { return _myCard2; }
      set { _myCard2 = value; }
    }
    private int _targetCard;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"targetCard", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int targetCard
    {
      get { return _targetCard; }
      set { _targetCard = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperBaoChangeSyn")]
  public partial class GameOperBaoChangeSyn : global::ProtoBuf.IExtensible
  {
    public GameOperBaoChangeSyn() {}
    
    private int _oldBao;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"oldBao", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int oldBao
    {
      get { return _oldBao; }
      set { _oldBao = value; }
    }
    private int _position = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int position
    {
      get { return _position; }
      set { _position = value; }
    }
    private int _dice = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"dice", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int dice
    {
      get { return _dice; }
      set { _dice = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperPlayerHuSyn")]
  public partial class GameOperPlayerHuSyn : global::ProtoBuf.IExtensible
  {
    public GameOperPlayerHuSyn() {}
    
    private int _position;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public int position
    {
      get { return _position; }
      set { _position = value; }
    }
    private int _card = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"card", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int card
    {
      get { return _card; }
      set { _card = value; }
    }
    private int _bao = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"bao", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int bao
    {
      get { return _bao; }
      set { _bao = value; }
    }
    private readonly global::System.Collections.Generic.List<packet.mj.GameOperPlayerSettle> _detail = new global::System.Collections.Generic.List<packet.mj.GameOperPlayerSettle>();
    [global::ProtoBuf.ProtoMember(4, Name=@"detail", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<packet.mj.GameOperPlayerSettle> detail
    {
      get { return _detail; }
    }
  
    private int _resultType = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"resultType", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int resultType
    {
      get { return _resultType; }
      set { _resultType = value; }
    }
    private int _paoPosition = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"paoPosition", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int paoPosition
    {
      get { return _paoPosition; }
      set { _paoPosition = value; }
    }
    private bool _skipHuSettle = default(bool);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"skipHuSettle", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool skipHuSettle
    {
      get { return _skipHuSettle; }
      set { _skipHuSettle = value; }
    }
    private int _winType = default(int);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"winType", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int winType
    {
      get { return _winType; }
      set { _winType = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperPlayerSettle")]
  public partial class GameOperPlayerSettle : global::ProtoBuf.IExtensible
  {
    public GameOperPlayerSettle() {}
    
    private int _position = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int position
    {
      get { return _position; }
      set { _position = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _handcard = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(2, Name=@"handcard", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    public global::System.Collections.Generic.List<int> handcard
    {
      get { return _handcard; }
    }
  
    private int _fanType = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"fanType", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int fanType
    {
      get { return _fanType; }
      set { _fanType = value; }
    }
    private int _fanNum = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"fanNum", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int fanNum
    {
      get { return _fanNum; }
      set { _fanNum = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _fanDetail = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(6, Name=@"fanDetail", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> fanDetail
    {
      get { return _fanDetail; }
    }
  
    private int _coin = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"coin", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int coin
    {
      get { return _coin; }
      set { _coin = value; }
    }
    private int _score = default(int);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"score", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int score
    {
      get { return _score; }
      set { _score = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameOperFinalSettleSyn")]
  public partial class GameOperFinalSettleSyn : global::ProtoBuf.IExtensible
  {
    public GameOperFinalSettleSyn() {}
    
    private string _settleDate = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"settleDate", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string settleDate
    {
      get { return _settleDate; }
      set { _settleDate = value; }
    }
    private readonly global::System.Collections.Generic.List<packet.mj.PlayerFinalResult> _detail = new global::System.Collections.Generic.List<packet.mj.PlayerFinalResult>();
    [global::ProtoBuf.ProtoMember(2, Name=@"detail", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<packet.mj.PlayerFinalResult> detail
    {
      get { return _detail; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PlayerFinalResult")]
  public partial class PlayerFinalResult : global::ProtoBuf.IExtensible
  {
    public PlayerFinalResult() {}
    
    private long _playerId = default(long);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"playerId", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long playerId
    {
      get { return _playerId; }
      set { _playerId = value; }
    }
    private string _playerName = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"playerName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string playerName
    {
      get { return _playerName; }
      set { _playerName = value; }
    }
    private int _position = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int position
    {
      get { return _position; }
      set { _position = value; }
    }
    private string _headImage = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"headImage", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string headImage
    {
      get { return _headImage; }
      set { _headImage = value; }
    }
    private int _score = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"score", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int score
    {
      get { return _score; }
      set { _score = value; }
    }
    private int _bankerCount = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"bankerCount", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int bankerCount
    {
      get { return _bankerCount; }
      set { _bankerCount = value; }
    }
    private int _huCount = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"huCount", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int huCount
    {
      get { return _huCount; }
      set { _huCount = value; }
    }
    private int _paoCount = default(int);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"paoCount", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int paoCount
    {
      get { return _paoCount; }
      set { _paoCount = value; }
    }
    private int _moBaoCount = default(int);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"moBaoCount", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int moBaoCount
    {
      get { return _moBaoCount; }
      set { _moBaoCount = value; }
    }
    private int _baoZhongBaoCount = default(int);
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"baoZhongBaoCount", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int baoZhongBaoCount
    {
      get { return _baoZhongBaoCount; }
      set { _baoZhongBaoCount = value; }
    }
    private int _kaiPaiZhaCount = default(int);
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"kaiPaiZhaCount", DataFormat = global::ProtoBuf.DataFormat.ZigZag)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int kaiPaiZhaCount
    {
      get { return _kaiPaiZhaCount; }
      set { _kaiPaiZhaCount = value; }
    }
    private bool _roomOwner = default(bool);
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"roomOwner", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool roomOwner
    {
      get { return _roomOwner; }
      set { _roomOwner = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}
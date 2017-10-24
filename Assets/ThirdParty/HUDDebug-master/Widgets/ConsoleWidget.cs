using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Playflock.Extension;

namespace Playflock.Log.Widget
{

    public class ConsoleWidget : HUDWidget
    {

        #region INSPECTOR_FIELDS
        [Space(10f)]
        [SerializeField]
        private OutputColor minOutputColor;
        [Space( 10f )]
        [SerializeField]
        private OutputColor outputColor;
        [Space(10f)]
        [SerializeField]
        private Text minErrorCountLabel;

        [SerializeField]
        private Text logText;

        [SerializeField]
        private GameObject _fullLogPrefab;

        [SerializeField]
        private Button minimizePreBtn;

        [SerializeField]
        private Button previewBtn;

        [SerializeField]
        private Button fullscreenPreBtn;

        [SerializeField]
        private Button minimizeFullBtn;

        [SerializeField]
        private Button previewFullBtn;

        [SerializeField]
        private Button settingsBtn;

        [SerializeField]
        private Button previewClearBtn;
        [Header("Fullscreen Log")]
        [SerializeField]
        private Button fullscreenClearBtn;

        [SerializeField]
        private Text _stackTraceField;

        [SerializeField]
        private RectTransform _fullViewContent;

        [SerializeField]
        private Toggle _fullscreenLogToggle;
        [SerializeField]
        private Toggle _fullscreenWarningToggle;
        [SerializeField]
        private Toggle _fullscreenErrorToggle;

        [SerializeField]
        private Text _fullscreenLogCountLabel;
        [SerializeField]
        private Text _fullscreenWarningCountLabel;
        [SerializeField]
        private Text _fullscreenErrorCountLabel;
        [SerializeField]
        private Scrollbar fullviewScrollbar;
        [SerializeField]
        private ConsoleItemView itemPrefab;
        [SerializeField]
        private Transform itemLayout; //parent of log items

        #endregion

        #region PUBLIC_VARIABLES

        [System.Serializable]
        public class OutputColor
        {
            public Color Log = Color.white;
            public Color Warning = Color.yellow;
            public Color Error = Color.red;
        }
        [Space]
        [SerializeField]
        private GameObject[] views;

        #endregion

        #region PRIVATE_VARIABLES
        private HUDDelegate _hudDelegate;
        private readonly List<ConsoleItemView> _itemsViews = new List<ConsoleItemView>();
        private Vector2 _fullViewContentOffset;
        private struct _fullLogView
        {
            public Text title;
            public Button btn;
            public string description;
        }
        private enum EDisplayMode
        {
            All,
            Logs,
            Warnings,
            Errors
        }
        private uint _errorCount;
        private uint _warningCount;
        private uint _logCount;
        private EDisplayMode _displayMode;
        private const int AUTO_CLEAR_LIMIT = 1000;
        #endregion

        #region UNITY_EVENTS

        private void OnEnable()
        {
            Application.logMessageReceived += HandleUnityLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleUnityLog;
        }

        public override void Initialize()
        {
            minimizePreBtn.onClick.AddListener( MinimizeBtnAction );
            minimizeFullBtn.onClick.AddListener( MinimizeFullBtnAction );
            previewBtn.onClick.AddListener( PreviewBtnAction );
            previewFullBtn.onClick.AddListener( PreviewFullBtnAction );
            fullscreenPreBtn.onClick.AddListener( FullscreenBtnAction );
            previewClearBtn.onClick.AddListener( ClearLog );

            //fullscreen
            fullscreenClearBtn.onClick.AddListener( ClearLog );
            _fullscreenLogToggle.onValueChanged.AddListener( ShowLogs );
            _fullscreenWarningToggle.onValueChanged.AddListener( ShowWarnings );
            _fullscreenErrorToggle.onValueChanged.AddListener( ShowErrors );

            _hudDelegate = FindObjectOfType<HUDDelegate>();

        }

        public override void OnDismiss()
        {
            base.OnDismiss();
            minimizePreBtn.onClick.RemoveListener(MinimizeBtnAction);
            minimizeFullBtn.onClick.RemoveListener(MinimizeFullBtnAction);
            previewBtn.onClick.RemoveListener(PreviewBtnAction);
            previewFullBtn.onClick.RemoveListener(PreviewFullBtnAction);
            fullscreenPreBtn.onClick.RemoveListener(FullscreenBtnAction);
            previewClearBtn.onClick.RemoveListener(ClearLog);
            //fullscreen
            fullscreenClearBtn.onClick.RemoveListener(ClearLog);
            _fullscreenLogToggle.onValueChanged.RemoveListener(ShowLogs);
            _fullscreenWarningToggle.onValueChanged.RemoveListener(ShowWarnings);
            _fullscreenErrorToggle.onValueChanged.RemoveListener(ShowErrors);
        }

        private void ShowLogs(bool isOn)
        {
            if ( isOn )
            {
                _fullscreenWarningToggle.isOn = false;
                _fullscreenErrorToggle.isOn = false;
                ShowLogType( EConsoleType.Log );
            }
            else
            {
                HideLogType( EConsoleType.Log );
                ShowAllLogs();
            }
        }

        private void ShowWarnings(bool isOn)
        {
            if ( isOn )
            {
                _fullscreenLogToggle.isOn = false;
                _fullscreenErrorToggle.isOn = false;
                ShowLogType( EConsoleType.Warning );
            }
            else
            {
                HideLogType( EConsoleType.Warning );
                ShowAllLogs();
            }
        }

        private void ShowErrors(bool isOn)
        {
            if ( isOn )
            {
                _fullscreenLogToggle.isOn = false;
                _fullscreenWarningToggle.isOn = false;
                ShowLogType( EConsoleType.Error );
            }
            else
            {
                HideLogType( EConsoleType.Error );
                ShowAllLogs();
            }
        }

        private void ShowAllLogs()
        {
            for (var i = 0; i < _itemsViews.Count; i++)
            {
                _itemsViews[i].gameObject.SetActive(true);
            }
            _displayMode = EDisplayMode.All;
            StartCoroutine(RefreshFullView());
        }
        private void ShowLogType(EConsoleType type)
        {
            for (var i = 0; i < _itemsViews.Count; i++)
            {
                if (_itemsViews[i].item.Type != type)
                    _itemsViews[i].gameObject.SetActive(false);
            }
            switch ( type )
            {
                case EConsoleType.Log:
                    _displayMode = EDisplayMode.Logs;
                    break;
                case EConsoleType.Warning:
                    _displayMode = EDisplayMode.Warnings;
                    break;
                case EConsoleType.Error:
                    _displayMode = EDisplayMode.Errors;
                    break;
            }
        }

        private void HideLogType(EConsoleType type)
        {
            for (var i = 0; i < _itemsViews.Count; i++)
            {
                if (_itemsViews[i].item.Type == type)
                    _itemsViews[i].gameObject.SetActive(false);
            }
        }

        public override void Draw()
        {
        }
        #endregion

        private void ShowFullLogDescription( string stackTrace )
        {
            _stackTraceField.text = stackTrace;
        }

        private void HandleUnityLog(string logString, string stackTrace, LogType type)
        {
            var cType = EConsoleType.Log;
            switch (type)
            {
                case LogType.Assert:
                    cType = EConsoleType.Assert;
                    _errorCount++;
                    break;
                case LogType.Error:
                    cType = EConsoleType.Error;
                    _errorCount++;
                    break;
                case LogType.Log:
                    cType = EConsoleType.Log;
                    _logCount++;
                    break;
                case LogType.Exception:
                    cType = EConsoleType.Exception;
                    _errorCount++;
                    break;
                case LogType.Warning:
                    cType = EConsoleType.Warning;
                    _warningCount++;
                    break;
            }
            if (_itemsViews.Count > AUTO_CLEAR_LIMIT)
            {
                ClearLog(100);
            }
            UpdateLogsCount(_logCount, _warningCount, _errorCount);
            StartCoroutine(AddItemAsync(new ConsoleItem(logString, stackTrace, cType, outputColor)));
        }


        private IEnumerator AddItemAsync( ConsoleItem item )
        {
            var instance = Instantiate( itemPrefab.gameObject );
            var itemView = instance.GetComponent<ConsoleItemView>();
            itemView.item = item;

            itemView.label.text = item.StripMessage;
            itemView.label.color = item.Color;
            itemView.button.onClick.AddListener( delegate
                                                 { ShowFullLogDescription( itemView.item.StackTrace ); } );

            instance.transform.SetParent( itemLayout );
            instance.transform.localScale = Vector3.one;

            if ( _displayMode == EDisplayMode.Logs && itemView.item.Type != EConsoleType.Log )
            {
                instance.gameObject.SetActive( false );
            }
            else if(_displayMode == EDisplayMode.Warnings && itemView.item.Type != EConsoleType.Warning)
            {
                instance.gameObject.SetActive(false);
            }
            else if (_displayMode == EDisplayMode.Errors && itemView.item.Type != EConsoleType.Error)
            {
                instance.gameObject.SetActive(false);
            }

            _itemsViews.Add( itemView );

            //Waiting 2 frames
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();


            if ( fullviewScrollbar && fullviewScrollbar.enabled )
                fullviewScrollbar.value = 0;
        }

        private IEnumerator RefreshFullView()
        {
            //Waiting 2 frames
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            if (fullviewScrollbar && fullviewScrollbar.enabled)
                fullviewScrollbar.value = 0;
        }

        private void UpdateLogsCount(uint logs, uint warnings, uint errors)
        {
            if ( views[0].gameObject.activeSelf ) // minimize view
            {
                var sbuilder = new System.Text.StringBuilder();
                sbuilder.Append( "<color=" + ColorPF.ToHex( minOutputColor.Log ) + ">" );
                sbuilder.Append( _logCount + "</color>/" );
                sbuilder.Append( "<color=" + ColorPF.ToHex( minOutputColor.Warning ) + ">" );
                sbuilder.Append( _warningCount + "</color>/" );
                sbuilder.Append( "<color=" + ColorPF.ToHex( minOutputColor.Error ) + ">" );
                sbuilder.Append( _errorCount + "</color>" );
                minErrorCountLabel.text = sbuilder.ToString();
            }
            else if(views[2].gameObject.activeSelf) //full view
            {
                _fullscreenLogCountLabel.text = logs.ToString();
                _fullscreenWarningCountLabel.text = warnings.ToString();
                _fullscreenErrorCountLabel.text = errors.ToString();
            }
        }

        private void ResetScreenLogs()
        {
            _logCount = 0;
            _warningCount = 0;
            _errorCount = 0;
            UpdateLogsCount(_logCount, _warningCount, _errorCount);
        }

        private void MinimizeBtnAction()
        {
            views[1].SetActive( false );
            views[0].SetActive( true );
        }

        private void MinimizeFullBtnAction()
        {
            ResetScreenLogs();
            //return to minimize view
            views[2].SetActive( false );
            views[0].SetActive( true );
            UpdateLogsCount(_logCount, _warningCount, _errorCount);
        }

        private void PreviewBtnAction()
        {
            //views[0].SetActive( false );
            //views[1].SetActive( true );
            //enter in fullscreen mode
            views[0].SetActive(false);
            views[2].SetActive(true);
            UpdateLogsCount(_logCount, _warningCount, _errorCount);
            StartCoroutine( RefreshFullView() );
        }

        private void PreviewFullBtnAction()
        {
            views[2].SetActive( false );
            views[1].SetActive( true );
        }

        private void FullscreenBtnAction()
        {
            views[1].SetActive( false );
            views[2].SetActive( true );
        }

        private void ClearLog()
        {
            _stackTraceField.text = string.Empty;
            for ( var i = 0; i < _itemsViews.Count; i++ )
            {
                Destroy( _itemsViews[i].gameObject );
            }
            _itemsViews.Clear();
            ResetScreenLogs();
        }

        private void ClearLog(int lastMsgCount)
        {
            var delta = _itemsViews.Count - lastMsgCount;
            for (var i = 0; i < delta; i++)
            {
                Destroy(_itemsViews[i].gameObject);
            }
            _itemsViews.RemoveRange(0, delta);
        }

        #region UTILS

        //TODO
        //private static List<_logStruct> Collapse( List<_logStruct> fullList )
        //{
        //    var result = new List<_logStruct>();
        //    var r = fullList.Distinct();
        //    result = r.ToList();
        //    return result;
        //}

        private void CopyToClipboard( string message )
        {
#if UNITY_EDITOR
            var T = typeof ( GUIUtility );
            var propertyInfo = T.GetProperty( "systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic );
            propertyInfo.SetValue( null, message, null );
#else
            //TODO
            //Clipboard.Push(message);
#endif
        }


        private Color DetectLogColor( LogType logType )
        {
            Color logColor;
            switch ( logType )
            {
                case LogType.Log:
                    logColor = outputColor.Log;
                    break;
                case LogType.Assert:
                    logColor = Color.magenta;
                    break;
                case LogType.Error:
                    logColor = outputColor.Error;
                    break;
                case LogType.Exception:
                    logColor = outputColor.Error;
                    break;
                case LogType.Warning:
                    logColor = outputColor.Warning;
                    break;
                default:
                    logColor = Color.white;
                    break;
            }
            return logColor;
        }

        #endregion

    }
    
}

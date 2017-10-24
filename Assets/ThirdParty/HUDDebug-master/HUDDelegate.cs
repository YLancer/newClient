using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Playflock.Log
{

    [System.Serializable]
    public struct RootNode
    {
        public HUDNode node;
        public bool isActiveOnStart;
    }

    public class HUDDelegate : MonoBehaviour
    {
        #region CONSTRUCTORS
        private HUDDelegate()
        {
        }
        #endregion

        #region PUBLIC_VARIABLES
        public static HUDDelegate Instance
        {
            get { return instance; }
        }

        public List<RootNode> rootNodes;

        [System.Serializable]
        public struct AdditionalPreferences
        {
            public float nodeRadius;
            public Button openBtn;
            public Sprite[] openBtnImgs;
        }

        [Header( "Base prefabs" )]
        public RectTransform rootNode;

        public RectTransform prefabNode;
        public RectTransform widgetsRoot;

        [Space( 10f )]
        public AdditionalPreferences preferences;

        public struct Node
        {
            public int prevNodeID;
            public string title;
            public List<RootNode> includedNodes;

            public Node( int _prevNodeID, string _title, List<RootNode> _includedNodes)
            {
                prevNodeID = _prevNodeID;
                title = _title;
                includedNodes = _includedNodes;
            }
        }

        [HideInInspector]
        public int previousNodeID = 0;

        [HideInInspector]
        public int currentNodeID = 0;

        public Dictionary<int, Node> currentNodeInfo = new Dictionary<int, Node>();

#endregion

        #region PRIVATE_VARIABLES
        private static HUDDelegate instance;
        private const int nodesLimit = 10; //limit nodes per page
        private int lastNodeIndex = 0; //by default 0 is home nodes
        private List<RectTransform> nodes = new List<RectTransform>();
        private UnityEngine.Events.UnityAction hudRootAction;
        private bool isInited;
        private bool isPresented;
        private int _openCondition = 3;
        private int _openCurrentClicks = 0;
#endregion

        #region UNITY_EVENTS

        void Awake()
        {
            if ( !Debug.isDebugBuild )
                gameObject.SetActive( false );
            instance = this;
            DontDestroyOnLoad( this.gameObject );
            //Hidden Open Btn
            preferences.openBtn.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            previousNodeID = 0;
            currentNodeInfo.Add( 0, new Node( 0, "Root", rootNodes ) );
            hudRootAction = delegate
                            {
                                _openCurrentClicks++;
                                if(_openCurrentClicks < _openCondition)
                                    return;
                                isPresented = !isPresented;
                                if ( isPresented )
                                {
                                    preferences.openBtn.GetComponent<Image>().color = Color.white;
                                    preferences.openBtn.image.sprite = preferences.openBtnImgs[1];
                                    rootNode.gameObject.SetActive( true );
                                    if ( !isInited )
                                        ShowHUDRoot( "Root", rootNodes );
                                }
                                else
                                {
                                    _openCurrentClicks = 0;
                                    preferences.openBtn.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
                                    preferences.openBtn.image.sprite = preferences.openBtnImgs[0];
                                    rootNode.gameObject.SetActive( false );
                                }
                            };
            preferences.openBtn.onClick.AddListener( hudRootAction );
            rootNode.GetComponent<Button>().onClick.AddListener( RootNodeClick );

            //Check nodes on property "isActiveOnStart"
            RecursivePass( ref rootNodes );
        }


        void OnDestroy()
        {
            preferences.openBtn.onClick.RemoveListener( hudRootAction );
            rootNode.GetComponent<Button>().onClick.RemoveListener( RootNodeClick );
        }

#endregion

        private void RootNodeClick()
        {
            if ( currentNodeID == 0 )
                return;
            CleanNodes();
            var node = currentNodeInfo[currentNodeID];
            var prevNode = currentNodeInfo[node.prevNodeID];
            lastNodeIndex = node.prevNodeID;
            currentNodeInfo.Remove( currentNodeID );
            currentNodeID = node.prevNodeID;
            ShowHUDRoot( prevNode.title, prevNode.includedNodes );
        }

        public void ShowHUDRoot( string title, List<RootNode> _rootNodes )
        {
            previousNodeID = lastNodeIndex;
            rootNode.FindChild( "Text" ).GetComponent<Text>().text = title;

            for ( var i = 0; i < _rootNodes.Count; i++ )
            {
#if UNITY_5
            var prefab = Instantiate(prefabNode);
#else
                RectTransform prefab = (RectTransform)Instantiate( prefabNode );
#endif
                prefab.name = _rootNodes[i].node.name;
                if (_rootNodes.Count > nodesLimit )
                {
                    var nodeScale = 1f / ((float)_rootNodes.Count / nodesLimit);
                    prefab.localScale = new Vector3( nodeScale, nodeScale, nodeScale );
                }
                if (_rootNodes[i].node.icon )
                {
                    prefab.FindChild( "Icon" ).GetComponent<Image>().sprite = _rootNodes[i].node.icon;
                }
                else
                {
                    prefab.FindChild( "Icon" ).GetComponent<Image>().enabled = false;
                    prefab.FindChild( "Title" ).GetComponent<Text>().text = _rootNodes[i].node.title;
                }

                var targetPosition = CalculateRadiusPos( i, _rootNodes.Count, rootNode.anchoredPosition,
                                                             new Vector2( rootNode.rect.width / preferences.nodeRadius,
                                                                          rootNode.rect.height / preferences.nodeRadius ) );
                prefab.anchoredPosition = targetPosition;
                prefab.SetParent( rootNode, false );

                lastNodeIndex++;
                _rootNodes[i].node.Id = lastNodeIndex;

                var j = i;

                switch (_rootNodes[j].node.nType )
                {
                    case NodeType.Toggle:
                        prefab.GetComponent<Button>()
                            .onClick.AddListener( delegate
                                                  {
                                                      _rootNodes[j].node.isActive = !_rootNodes[j].node.isActive;
                                                      prefab.GetComponent<Button>().image.color = _rootNodes[j].node.isActive ? Color.gray : Color.white;
                                                      _rootNodes[j].node.OnToggle(_rootNodes[j].node.isActive);
                                                  });
                        break;
                    case NodeType.Button:
                        prefab.GetComponent<Button>()
                              .onClick.AddListener( delegate { _rootNodes[j].node.OnClick(); } );
                        break;
                }
                nodes.Add( prefab );
                isInited = true;
            }



        }

        public void CleanNodes()
        {
            foreach ( var node in nodes )
            {
                Destroy( node.gameObject );
            }
            nodes.Clear();
        }

    #region UTILS

        private Vector3 CalculateRadiusPos( int index, int count, Vector3 center, Vector2 radius )
        {
            Vector3 pos;
            float elemCount = (index * 1.0f) / count;
            float angle = elemCount * Mathf.PI * 2;
            float x = Mathf.Sin( angle ) * radius.x;
            float y = Mathf.Cos( angle ) * radius.y;
            pos = center + new Vector3( x, y );
            return pos;
        }
        
        private void RecursivePass( ref List<RootNode> rNodes )
        {
            foreach ( var n in rNodes )
            {
                n.node.OnToggle( n.isActiveOnStart );
                RecursivePass( ref n.node.includeNodes );
            }
        }

#endregion

    }
}

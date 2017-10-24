using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Playflock.Log
{
    public abstract class HUDNode : MonoBehaviour
    {
        #region PUBLIC_VARIABLES
        [HideInInspector]
        public int Id;

        public NodeType nType;
        public string title;
        public Sprite icon;
        public Transform widgetPrefab;
        public List<RootNode> includeNodes;

        [NonSerialized]
        public bool isActive;
        #endregion

        #region PRIVATE_VARIABLES
        private const string postfix = "_hud";
        #endregion

        #region LISTINERS

        public virtual void OnToggle(bool isOn)
        {
            if(widgetPrefab == null)
                return;
            if ( !isOn )
            {
                DismissWidget();
                return;
            }
            var prefab = (RectTransform)Instantiate( widgetPrefab );
            prefab.name = widgetPrefab.name + postfix;
            prefab.SetParent( HUDDelegate.Instance.widgetsRoot, false );
            prefab.GetComponent<HUDWidget>().ParentNode = this;
            isActive = true;
        }

        public virtual void OnClick()
        {
            if (includeNodes.Count > 0)
            {
                HUDDelegate.Instance.CleanNodes();
                HUDDelegate.Instance.currentNodeID = Id;
                HUDDelegate.Instance.currentNodeInfo.Add(Id,
                                                          new HUDDelegate.Node(HUDDelegate.Instance.previousNodeID,
                                                                                gameObject.name, includeNodes));

                HUDDelegate.Instance.ShowHUDRoot(/*gameObject.name*/title, includeNodes);
            }
            if ( widgetPrefab != null )
            {
                RectTransform prefab = (RectTransform)Instantiate(widgetPrefab);
                prefab.name = widgetPrefab.name + postfix;
                prefab.SetParent(HUDDelegate.Instance.widgetsRoot, false);
                prefab.GetComponent<HUDWidget>().ParentNode = this;
            }
        }

#endregion

        public virtual void DismissWidget()
        {
            if(widgetPrefab == null)
                return;
            Destroy( GameObject.Find( widgetPrefab.name + postfix ) );
        }
    }
}

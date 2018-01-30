/*************************************************************
   Copyright(C) 2017 by #COMPANY#
   All rights reserved.
   
   #SCRIPTFULLNAME#
   #PROJECTNAME#
   
   Created by #AUTHOR# on #DATE#.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdateFPS : MonoBehaviour
{
    /// <summary>
    /// 功能：修改游戏FPS
    /// </summary>
    //游戏的FPS，可在属性窗口中修改
        public int targetFrameRate = 300;

        //当程序唤醒时
        void Awake()
        {
            //修改当前的FPS
            Application.targetFrameRate = targetFrameRate;
        }

}


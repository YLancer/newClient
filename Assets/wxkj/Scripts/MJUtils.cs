using UnityEngine;
using System.Collections;

public class MJUtils
{
    // 拿吃听为例，我推0x40000给你，你推0x40000给我，带上吃的牌，我广播，我推支对给你(如有)， 你推支对给我，我广播支对，我推出给你，你推出给我，我广播出去。
    // 可选操作列表(位与),后面有再补充。抢听这个应该可以忽略，目前都是随吃听和碰听一起下发的
    // (0x40000:吃听) (0x2000000:碰听) 并没有到听这一动作，只是告诉玩家吃碰后就可以听了。 吃碰后后端会再下发0x40
    public const int ACT_CHI = 0x01;                    // 吃
    public const int ACT_PENG = 0x02;                   // 碰
    public const int ACT_AN_GANG = 0x04;                // 暗杠
    public const int ACT_BU_GANG = 0x08;                // 补杠 自己碰了之后补杠
    public const int ACT_ZHI_GANG = 0x10;               // 直杠 别人打出的被我直接杠上
    public const int ACT_DROP_CARD = 0x20;              // 出牌
    public const int ACT_HU = 0x40;                     // 胡牌
    public const int ACT_TING = 0x80;                   // 听牌
    public const int ACT_PASS = 0x100;                   // 取消（过）
    //public const int ACT_DROP_CARD_AUTO_TING = 0x400;   // 听牌后自动出牌
    //public const int ACT_DROPED = 0x8000;               // 已出
    public const int ACT_TING_CHI = 0x40000;            // 吃听
    public const int ACT_SHUAIJIUYAO = 0x1000000;            //甩九幺
    //public const int ACT_DROP_CARD_AUTO = 0x80000;      // 超时自动出牌
    //public const int ACT_TING_QIANG = 0x4000000;        // 抢听
    public const int ACT_DRAG_CARD = 0x40000000;        // 摸牌
    public const int ACT_TING_PENG = 0x2000000;         // 碰听
    public const int ACT_TING_ZHIDUI = 0x20000000;      // 支对听

    //(0x01:开牌炸)  (0x02:支对)  (0x04:37夹) (0x08:单调夹) (0x10:带大风) (0x20:红中满天飞) (0x40:带漏的) (0x80:不夹不胡)
    public const int MODE_ZHA = 0x01;      // 开牌炸
    public const int MODE_ZHIDUI = 0x02;      // 支对
    public const int MODE_37JIA = 0x04;      // 37夹
    public const int MODE_DANDIAOJIA = 0x08;      // 单调夹
    public const int MODE_DAFENG = 0x10;      // 带大风
    public const int MODE_HONGZHONG = 0x20;      // 红中满天飞
    public const int MODE_DAILOU = 0x40;      // 带漏的
    public const int MODE_JIAHU = 0x80;      // 不夹不胡

    //新增玩法 FOR 西凉/金昌麻将（七小对，报听，风牌，带会，自摸胡，清一色一条龙） 
    public const int MODE_SEVENPAIR = 0x110;  //七小对
    public const int MODE_FENGPAI = 0x120;    //风牌
    public const int MODE_DAIHUI = 0x130;     //带会
    public const int MODE_ZIMOHU = 0x140;     //自摸胡
    public const int MODE_ONECOLORTRAIN = 0x150;  // 清一色一条龙

    //0x100000:胡牌      0x200000:输了    0x400000:流局     0x0002:点炮    0x0080:宝中宝    0x0020:摸宝胡      0x0800:开牌炸      0x0400刮大风     0x2000:红中满天飞        0x4000:带漏胡
    public const int HU_Hu = 0x100000;// 胡牌
    public const int HU_Shu = 0x200000;// 输了    
    public const int HU_LiuJu = 0x400000;// 流局     
    public const int HU_ShouPao = 0x800000;// 收炮     
    public const int HU_Pao = 0x0002;// 点炮
    public const int HU_BaoZhongBao = 0x0080;// 宝中宝
    public const int HU_MoBao = 0x0020;// 摸宝胡
    public const int HU_KaiPaiZha = 0x0800;// 开牌炸
    public const int HU_GuaDaFeng = 0x0400;// 刮大风
    public const int HU_HongZhong = 0x2000;// 红中满天飞
    public const int HU_DaiLou = 0x4000;// 带漏胡

    public static bool CanAct(int act, int actions = -1)
    {
        if (-1 == actions)
        {
            if (null != RoomMgr.actionNotify)
            {
                actions = RoomMgr.actionNotify.actions;
            }
            else
            {
                return false;
            }
        }
        return (actions & act) > 0;
    }

    public static bool Chi(int actions = -1)
    {
        return CanAct(ACT_CHI, actions);
    }

    public static bool Peng(int actions = -1)
    {
        return CanAct(ACT_PENG, actions);
    }

    public static bool AnGang(int actions = -1)
    {
        return CanAct(ACT_AN_GANG, actions);
    }

    public static bool BuGang(int actions = -1)
    {
        return CanAct(ACT_BU_GANG, actions);
    }

    public static bool ZhiGang(int actions = -1)
    {
        return CanAct(ACT_ZHI_GANG, actions);
    }

    public static bool DragCard(int actions = -1)
    {
        return CanAct(ACT_DRAG_CARD, actions);
    }

    public static bool DropCard(int actions = -1)
    {
        return CanAct(ACT_DROP_CARD, actions);
    }

    public static bool Ting(int actions = -1)
    {
        return CanAct(ACT_TING, actions);
    }
    //胡我写的
    public static bool Hu(int actions =-1)
    {
        return CanAct(ACT_HU, actions);
    }

    public static bool TingChi(int actions = -1)
    {
        return CanAct(ACT_TING_CHI, actions);
    }

    public static bool TingPeng(int actions = -1)
    {
        return CanAct(ACT_TING_PENG, actions);
    }

    public static bool TingZhidui(int actions = -1)
    {
        return CanAct(ACT_TING_ZHIDUI, actions);
    }
    // 甩九幺 
    public static bool ShuaiJiuYao(int actions=-1)
    {
        return CanAct(ACT_SHUAIJIUYAO, actions) && Game.MJMgr.isShuaiJiuYao == true; //TODO 技术债务 临时处理 
    }

    public static bool HasWanfa(int act)
    {
        if (null == RoomMgr.playerGamingSyn)
        {
            return false;
        }

        int wanfa = RoomMgr.playerGamingSyn.wanfa;
        return (wanfa & act) > 0;
    }

    public static string GetHuType(int resultType)
    {
        switch (resultType)
        {
            case HU_Hu: return "胡牌";
            case HU_Shu: return "输了";
            case HU_LiuJu: return "流局";
            case HU_Pao: return "点炮";
            case HU_BaoZhongBao: return "宝中宝";
            case HU_MoBao: return "摸宝胡";
            case HU_KaiPaiZha: return "开牌炸";
            case HU_GuaDaFeng: return "刮大风";
            case HU_HongZhong: return "红中满天飞";
            case HU_DaiLou: return "带漏胡";
            default: return "本局结算";
        }
    }
}

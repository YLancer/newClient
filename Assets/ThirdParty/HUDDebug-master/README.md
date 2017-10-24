HUDDebug v 2.0
===============

[![https://gitter.im/indieyp/HUDDebug](https://img.shields.io/gitter/room/gitterHQ/gitter.svg)](https://gitter.im/indieyp/HUDDebug)

[![donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=5VW3E89ZUYYXC)

Unity 5 console module for log stack trace on mobile devices.

Installation
-------------
1. Click on release link.<img src="https://git.playflock.com/github-enterprise-assets/0000/0012/0000/0040/4ef56924-2661-11e6-8905-7122ae902c87.png">
2. Download latest unitypackage release.<img src="https://git.playflock.com/github-enterprise-assets/0000/0012/0000/0041/b24dcd2c-2661-11e6-81e0-fde7becbeb89.png">
3. Open **PlayFlock_Utils->HUDDebug** folder and drag HUDRoot prefab into the scene.

## Creating nodes

Open context menu **Playflock/Create/HUDDebug** by clicking on left mouse button.

For creating node enter name and select the implementation type:

<img src="https://git.playflock.com/github-enterprise-assets/0000/0012/0000/0042/84c90038-2666-11e6-8e9f-ed5887b0f627.png">

Finally, click on the **"Create HUD"** button

To display node in game put your created prefab from **Node** folder to **Scene Hierarchy->HUDRoot->HUDDelegate(Component)->RootNodes**

<img src="https://git.playflock.com/github-enterprise-assets/0000/0012/0000/0043/cb0e19c2-267d-11e6-9e90-b93eb32416ec.png">

## Creating widgets

Open context menu **Playflock/Create/HUDDebug** by clicking on left mouse button.

For creating widget enter name(template and members optional fields) and click on the **"Create HUD"** button:

<img src="https://git.playflock.com/github-enterprise-assets/0000/0012/0000/0044/8faea08a-267e-11e6-8286-7976b086c851.png">

To display widget in game your need connect widget to existing node.

<img src="https://git.playflock.com/github-enterprise-assets/0000/0012/0000/0045/7b4e04da-2680-11e6-89cb-41692b1154b6.png">

Triple click on debug zone and see result:

<img src="https://git.playflock.com/github-enterprise-assets/0000/0012/0000/0046/50ff3428-2681-11e6-8e3c-556527af3279.gif">

You can also use ready-made crossplatform console:

<img src="https://cloud.githubusercontent.com/assets/7010398/15651246/0c11dbd0-2687-11e6-9a3c-0fb85bfa8529.PNG">

If you want to share your custom widget, please send me mail on **indieypdev@gmail.com**

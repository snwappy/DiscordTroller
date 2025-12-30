using System;
using System.Collections.Generic;
using System.Text;

static class LogUtil
{
    public static string PlayerName()
        => Player.localPlayer.photonView.Owner.NickName;
}


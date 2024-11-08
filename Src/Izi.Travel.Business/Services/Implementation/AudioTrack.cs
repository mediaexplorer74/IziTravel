// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.AudioService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Agent.Audio;
using System;

namespace Izi.Travel.Business.Services.Implementation
{
    internal class AudioTrack : PlayerTrack
    {
        private Uri uri1;
        private string title;
        private string v1;
        private string v2;
        private Uri uri2;
        private string v3;
        private object pause;

        public AudioTrack(Uri uri1, string title, string v1, string v2, Uri uri2, string v3, object pause)
        {
            this.uri1 = uri1;
            this.title = title;
            this.v1 = v1;
            this.v2 = v2;
            this.uri2 = uri2;
            this.v3 = v3;
            this.pause = pause;
        }
    }
}
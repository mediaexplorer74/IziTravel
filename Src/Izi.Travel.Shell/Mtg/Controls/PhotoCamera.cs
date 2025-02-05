// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Controls.BarcodeScanner
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;

namespace Izi.Travel.Shell.Mtg.Controls
{
    public class PhotoCamera
    {
        public EventHandler<CameraOperationCompletedEventArgs> Initialized;
        public EventHandler<CameraOperationCompletedEventArgs> AutoFocusCompleted;
        public CameraFlashMode FlashMode;
        public CameraResolution PreviewResolution;

        public void Focus()
        {
            throw new NotImplementedException();
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }

        internal void GetPreviewBufferArgb32(object pixels)
        {
            throw new NotImplementedException();
        }
    }
}
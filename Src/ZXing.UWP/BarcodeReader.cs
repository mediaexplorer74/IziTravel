// Decompiled with JetBrains decompiler
// Type: ZXing.BarcodeReader
// Assembly: zxing.wp8.0, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DD293DF0-BBAA-4BF0-BAC7-F5FAF5AC94ED
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\zxing.wp8.0.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\zxing.wp8.0.xml

using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;
using ZXing.Common;
//using System.Windows.Media.Imaging;

#nullable disable
namespace ZXing
{
  /// <summary>
  /// A smart class to decode the barcode inside a bitmap object
  /// </summary>
  public class BarcodeReader : 
    BarcodeReaderGeneric<WriteableBitmap>,
    IBarcodeReader,
    IMultipleBarcodeReader
  {
    private static readonly Func<WriteableBitmap, LuminanceSource> defaultCreateLuminanceSource = (Func<WriteableBitmap, LuminanceSource>) 
            (bitmap => (LuminanceSource) new BitmapLuminanceSource(bitmap));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ZXing.BarcodeReader" /> class.
    /// </summary>
    public BarcodeReader()
      : this((Reader) new MultiFormatReader(), BarcodeReader.defaultCreateLuminanceSource, (Func<LuminanceSource, Binarizer>) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ZXing.BarcodeReader" /> class.
    /// </summary>
    /// <param name="reader">Sets the reader which should be used to find and decode the barcode.
    /// If null then MultiFormatReader is used</param>
    /// <param name="createLuminanceSource">Sets the function to create a luminance source object for a bitmap.
    /// If null, an exception is thrown when Decode is called</param>
    /// <param name="createBinarizer">Sets the function to create a binarizer object for a luminance source.
    /// If null then HybridBinarizer is used</param>
    public BarcodeReader(
      Reader reader,
      Func<WriteableBitmap, LuminanceSource> createLuminanceSource,
      Func<LuminanceSource, Binarizer> createBinarizer)
      : base(reader, createLuminanceSource ?? BarcodeReader.defaultCreateLuminanceSource, createBinarizer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ZXing.BarcodeReader" /> class.
    /// </summary>
    /// <param name="reader">Sets the reader which should be used to find and decode the barcode.
    /// If null then MultiFormatReader is used</param>
    /// <param name="createLuminanceSource">Sets the function to create a luminance source object for a bitmap.
    /// If null, an exception is thrown when Decode is called</param>
    /// <param name="createBinarizer">Sets the function to create a binarizer object for a luminance source.
    /// If null then HybridBinarizer is used</param>
    public BarcodeReader(
      Reader reader,
      Func<WriteableBitmap, LuminanceSource> createLuminanceSource,
      Func<LuminanceSource, Binarizer> createBinarizer,
      Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource> createRGBLuminanceSource)
      : base(reader, createLuminanceSource ?? BarcodeReader.defaultCreateLuminanceSource, createBinarizer, createRGBLuminanceSource)
    {
    }

        bool IBarcodeReader.TryHarder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IMultipleBarcodeReader.TryHarder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IBarcodeReader.PureBarcode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IMultipleBarcodeReader.PureBarcode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IBarcodeReader.CharacterSet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IMultipleBarcodeReader.CharacterSet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IList<BarcodeFormat> IBarcodeReader.PossibleFormats { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IList<BarcodeFormat> IMultipleBarcodeReader.PossibleFormats { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DecodingOptions IBarcodeReader.Options { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DecodingOptions IMultipleBarcodeReader.Options { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        event Action<ResultPoint> IBarcodeReader.ResultPointFound
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event Action<ResultPoint> IMultipleBarcodeReader.ResultPointFound
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event Action<Result> IBarcodeReader.ResultFound
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event Action<Result> IMultipleBarcodeReader.ResultFound
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public Result Decode(WriteableBitmap barcodeBitmap)
        {
            throw new NotImplementedException();
        }

        public Result[] DecodeMultiple(WriteableBitmap barcodeBitmap)
        {
            throw new NotImplementedException();
        }

        Result IBarcodeReader.Decode(byte[] rawRGB, int width, int height, RGBLuminanceSource.BitmapFormat format)
        {
            throw new NotImplementedException();
        }

        Result IBarcodeReader.Decode(LuminanceSource luminanceSource)
        {
            throw new NotImplementedException();
        }

     

        Result[] IMultipleBarcodeReader.DecodeMultiple(byte[] rawRGB, int width, int height, RGBLuminanceSource.BitmapFormat format)
        {
            throw new NotImplementedException();
        }

        Result[] IMultipleBarcodeReader.DecodeMultiple(LuminanceSource luminanceSource)
        {
            throw new NotImplementedException();
        }


    }
}

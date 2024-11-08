// Decompiled with JetBrains decompiler
// Type: RestSharp.Authenticators.OAuth.Extensions.OAuthExtensions
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace RestSharp.Authenticators.OAuth.Extensions
{
  internal static class OAuthExtensions
  {
    public static string ToRequestValue(this OAuthSignatureMethod signatureMethod)
    {
      string upper = signatureMethod.ToString().ToUpper();
      int startIndex = upper.IndexOf("SHA1");
      return startIndex <= -1 ? upper : upper.Insert(startIndex, "-");
    }

    public static OAuthSignatureMethod FromRequestValue(this string signatureMethod)
    {
      switch (signatureMethod)
      {
        case "HMAC-SHA1":
          return OAuthSignatureMethod.HmacSha1;
        case "RSA-SHA1":
          return OAuthSignatureMethod.RsaSha1;
        default:
          return OAuthSignatureMethod.PlainText;
      }
    }

    public static string HashWith(this string input, HashAlgorithm algorithm)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(input);
      return Convert.ToBase64String(algorithm.ComputeHash(bytes));
    }
  }
}

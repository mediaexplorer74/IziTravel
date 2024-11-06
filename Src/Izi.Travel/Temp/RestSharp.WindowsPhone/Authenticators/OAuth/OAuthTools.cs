// Decompiled with JetBrains decompiler
// Type: RestSharp.Authenticators.OAuth.OAuthTools
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Authenticators.OAuth.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace RestSharp.Authenticators.OAuth
{
  internal static class OAuthTools
  {
    private const string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
    private const string Digit = "1234567890";
    private const string Lower = "abcdefghijklmnopqrstuvwxyz";
    private const string Unreserved = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-._~";
    private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly Random _random;
    private static readonly object _randomLock = new object();
    /// <summary>
    /// All text parameters are UTF-8 encoded (per section 5.1).
    /// </summary>
    /// <seealso cref="!:http://www.hueniverse.com/hueniverse/2008/10/beginners-gui-1.html" />
    private static readonly Encoding _encoding = Encoding.UTF8;
    /// <summary>
    /// The set of characters that are unreserved in RFC 2396 but are NOT unreserved in RFC 3986.
    /// </summary>
    /// <seealso cref="!:http://stackoverflow.com/questions/846487/how-to-get-uri-escapedatastring-to-comply-with-rfc-3986" />
    private static readonly string[] UriRfc3986CharsToEscape = new string[5]
    {
      "!",
      "*",
      "'",
      "(",
      ")"
    };
    private static readonly string[] UriRfc3968EscapedHex = new string[5]
    {
      "%21",
      "%2A",
      "%27",
      "%28",
      "%29"
    };

    static OAuthTools() => OAuthTools._random = new Random();

    /// <summary>
    /// Generates a random 16-byte lowercase alphanumeric string.
    /// </summary>
    /// <seealso cref="!:http://oauth.net/core/1.0#nonce" />
    /// <returns></returns>
    public static string GetNonce()
    {
      char[] chArray = new char[16];
      lock (OAuthTools._randomLock)
      {
        for (int index = 0; index < chArray.Length; ++index)
          chArray[index] = "abcdefghijklmnopqrstuvwxyz1234567890"[OAuthTools._random.Next(0, "abcdefghijklmnopqrstuvwxyz1234567890".Length)];
      }
      return new string(chArray);
    }

    /// <summary>
    /// Generates a timestamp based on the current elapsed seconds since '01/01/1970 0000 GMT"
    /// </summary>
    /// <seealso cref="!:http://oauth.net/core/1.0#nonce" />
    /// <returns></returns>
    public static string GetTimestamp() => OAuthTools.GetTimestamp(DateTime.UtcNow);

    /// <summary>
    /// Generates a timestamp based on the elapsed seconds of a given time since '01/01/1970 0000 GMT"
    /// </summary>
    /// <seealso cref="!:http://oauth.net/core/1.0#nonce" />
    /// <param name="dateTime">A specified point in time.</param>
    /// <returns></returns>
    public static string GetTimestamp(DateTime dateTime) => dateTime.ToUnixTime().ToString();

    /// <summary>
    /// URL encodes a string based on section 5.1 of the OAuth spec.
    /// Namely, percent encoding with [RFC3986], avoiding unreserved characters,
    /// upper-casing hexadecimal characters, and UTF-8 encoding for text value pairs.
    /// </summary>
    /// <param name="value">The value to escape.</param>
    /// <returns>The escaped value.</returns>
    /// <remarks>
    /// The <see cref="M:System.Uri.EscapeDataString(System.String)" /> method is <i>supposed</i> to take on
    /// RFC 3986 behavior if certain elements are present in a .config file.  Even if this
    /// actually worked (which in my experiments it <i>doesn't</i>), we can't rely on every
    /// host actually having this configuration element present.
    /// </remarks>
    /// <seealso cref="!:http://oauth.net/core/1.0#encoding_parameters" />
    /// <seealso cref="!:http://stackoverflow.com/questions/846487/how-to-get-uri-escapedatastring-to-comply-with-rfc-3986" />
    public static string UrlEncodeRelaxed(string value)
    {
      StringBuilder stringBuilder = new StringBuilder(Uri.EscapeDataString(value));
      for (int index = 0; index < OAuthTools.UriRfc3986CharsToEscape.Length; ++index)
      {
        string oldValue = OAuthTools.UriRfc3986CharsToEscape[index];
        stringBuilder.Replace(oldValue, OAuthTools.UriRfc3968EscapedHex[index]);
      }
      return stringBuilder.ToString();
    }

    /// <summary>
    /// URL encodes a string based on section 5.1 of the OAuth spec.
    /// Namely, percent encoding with [RFC3986], avoiding unreserved characters,
    /// upper-casing hexadecimal characters, and UTF-8 encoding for text value pairs.
    /// </summary>
    /// <param name="value"></param>
    /// <seealso cref="!:http://oauth.net/core/1.0#encoding_parameters" />
    public static string UrlEncodeStrict(string value)
    {
      string result = "";
      // ISSUE: reference to a compiler-generated field
      value.ForEach<char>((Action<char>) (c => this.result += "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-._~".Contains<char>(c) ? c.ToString() : c.ToString().PercentEncode()));
      return result;
    }

    /// <summary>
    /// Sorts a collection of key-value pairs by name, and then value if equal,
    /// concatenating them into a single string. This string should be encoded
    /// prior to, or after normalization is run.
    /// </summary>
    /// <seealso cref="!:http://oauth.net/core/1.0#rfc.section.9.1.1" />
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static string NormalizeRequestParameters(WebParameterCollection parameters)
    {
      return OAuthTools.SortParametersExcludingSignature(parameters).Concatenate("=", "&");
    }

    /// <summary>
    /// Sorts a <see cref="T:RestSharp.Authenticators.OAuth.WebParameterCollection" /> by name, and then value if equal.
    /// </summary>
    /// <param name="parameters">A collection of parameters to sort</param>
    /// <returns>A sorted parameter collection</returns>
    public static WebParameterCollection SortParametersExcludingSignature(
      WebParameterCollection parameters)
    {
      WebParameterCollection parameterCollection = new WebParameterCollection((IEnumerable<WebPair>) parameters);
      IEnumerable<WebPair> parameters1 = parameterCollection.Where<WebPair>((Func<WebPair, bool>) (n => n.Name.EqualsIgnoreCase("oauth_signature")));
      parameterCollection.RemoveAll(parameters1);
      parameterCollection.ForEach<WebPair>((Action<WebPair>) (p =>
      {
        p.Name = OAuthTools.UrlEncodeStrict(p.Name);
        p.Value = OAuthTools.UrlEncodeStrict(p.Value);
      }));
      parameterCollection.Sort((Comparison<WebPair>) ((x, y) => string.CompareOrdinal(x.Name, y.Name) == 0 ? string.CompareOrdinal(x.Value, y.Value) : string.CompareOrdinal(x.Name, y.Name)));
      return parameterCollection;
    }

    /// <summary>
    /// Creates a request URL suitable for making OAuth requests.
    /// Resulting URLs must exclude port 80 or port 443 when accompanied by HTTP and HTTPS, respectively.
    /// Resulting URLs must be lower case.
    /// </summary>
    /// <seealso cref="!:http://oauth.net/core/1.0#rfc.section.9.1.2" />
    /// <param name="url">The original request URL</param>
    /// <returns></returns>
    public static string ConstructRequestUrl(Uri url)
    {
      if (url == (Uri) null)
        throw new ArgumentNullException(nameof (url));
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = "{0}://{1}".FormatWith((object) url.Scheme, (object) url.Host);
      string str2 = ":{0}".FormatWith((object) url.Port);
      bool flag1 = url.Scheme == "http" && url.Port == 80;
      bool flag2 = url.Scheme == "https" && url.Port == 443;
      stringBuilder.Append(str1);
      stringBuilder.Append(flag1 || flag2 ? "" : str2);
      stringBuilder.Append(url.AbsolutePath);
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Creates a request elements concatentation value to send with a request.
    /// This is also known as the signature base.
    /// </summary>
    /// <seealso cref="!:http://oauth.net/core/1.0#rfc.section.9.1.3" />
    /// <seealso cref="!:http://oauth.net/core/1.0#sig_base_example" />
    /// <param name="method">The request's HTTP method type</param>
    /// <param name="url">The request URL</param>
    /// <param name="parameters">The request's parameters</param>
    /// <returns>A signature base string</returns>
    public static string ConcatenateRequestElements(
      string method,
      string url,
      WebParameterCollection parameters)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = method.ToUpper().Then("&");
      string str2 = OAuthTools.UrlEncodeRelaxed(OAuthTools.ConstructRequestUrl(url.AsUri())).Then("&");
      string str3 = OAuthTools.UrlEncodeRelaxed(OAuthTools.NormalizeRequestParameters(parameters));
      stringBuilder.Append(str1);
      stringBuilder.Append(str2);
      stringBuilder.Append(str3);
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Creates a signature value given a signature base and the consumer secret.
    /// This method is used when the token secret is currently unknown.
    /// </summary>
    /// <seealso cref="!:http://oauth.net/core/1.0#rfc.section.9.2" />
    /// <param name="signatureMethod">The hashing method</param>
    /// <param name="signatureBase">The signature base</param>
    /// <param name="consumerSecret">The consumer key</param>
    /// <returns></returns>
    public static string GetSignature(
      OAuthSignatureMethod signatureMethod,
      string signatureBase,
      string consumerSecret)
    {
      return OAuthTools.GetSignature(signatureMethod, OAuthSignatureTreatment.Escaped, signatureBase, consumerSecret, (string) null);
    }

    /// <summary>
    /// Creates a signature value given a signature base and the consumer secret.
    /// This method is used when the token secret is currently unknown.
    /// </summary>
    /// <seealso cref="!:http://oauth.net/core/1.0#rfc.section.9.2" />
    /// <param name="signatureMethod">The hashing method</param>
    /// <param name="signatureTreatment">The treatment to use on a signature value</param>
    /// <param name="signatureBase">The signature base</param>
    /// <param name="consumerSecret">The consumer key</param>
    /// <returns></returns>
    public static string GetSignature(
      OAuthSignatureMethod signatureMethod,
      OAuthSignatureTreatment signatureTreatment,
      string signatureBase,
      string consumerSecret)
    {
      return OAuthTools.GetSignature(signatureMethod, signatureTreatment, signatureBase, consumerSecret, (string) null);
    }

    /// <summary>
    /// Creates a signature value given a signature base and the consumer secret and a known token secret.
    /// </summary>
    /// <seealso cref="!:http://oauth.net/core/1.0#rfc.section.9.2" />
    /// <param name="signatureMethod">The hashing method</param>
    /// <param name="signatureBase">The signature base</param>
    /// <param name="consumerSecret">The consumer secret</param>
    /// <param name="tokenSecret">The token secret</param>
    /// <returns></returns>
    public static string GetSignature(
      OAuthSignatureMethod signatureMethod,
      string signatureBase,
      string consumerSecret,
      string tokenSecret)
    {
      return OAuthTools.GetSignature(signatureMethod, OAuthSignatureTreatment.Escaped, consumerSecret, tokenSecret);
    }

    /// <summary>
    /// Creates a signature value given a signature base and the consumer secret and a known token secret.
    /// </summary>
    /// <seealso cref="!:http://oauth.net/core/1.0#rfc.section.9.2" />
    /// <param name="signatureMethod">The hashing method</param>
    /// <param name="signatureTreatment">The treatment to use on a signature value</param>
    /// <param name="signatureBase">The signature base</param>
    /// <param name="consumerSecret">The consumer secret</param>
    /// <param name="tokenSecret">The token secret</param>
    /// <returns></returns>
    public static string GetSignature(
      OAuthSignatureMethod signatureMethod,
      OAuthSignatureTreatment signatureTreatment,
      string signatureBase,
      string consumerSecret,
      string tokenSecret)
    {
      if (tokenSecret.IsNullOrBlank())
        tokenSecret = string.Empty;
      consumerSecret = OAuthTools.UrlEncodeRelaxed(consumerSecret);
      tokenSecret = OAuthTools.UrlEncodeRelaxed(tokenSecret);
      string str;
      switch (signatureMethod)
      {
        case OAuthSignatureMethod.HmacSha1:
          HMACSHA1 algorithm = new HMACSHA1();
          string s = "{0}&{1}".FormatWith((object) consumerSecret, (object) tokenSecret);
          algorithm.Key = OAuthTools._encoding.GetBytes(s);
          str = signatureBase.HashWith((HashAlgorithm) algorithm);
          break;
        case OAuthSignatureMethod.PlainText:
          str = "{0}&{1}".FormatWith((object) consumerSecret, (object) tokenSecret);
          break;
        default:
          throw new NotImplementedException("Only HMAC-SHA1 is currently supported.");
      }
      return signatureTreatment == OAuthSignatureTreatment.Escaped ? OAuthTools.UrlEncodeRelaxed(str) : str;
    }
  }
}

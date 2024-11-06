// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.StringNumberComparer
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Utility
{
  public class StringNumberComparer : IComparer<string>
  {
    public int Compare(string s1, string s2)
    {
      if (s1 == null || s2 == null)
        return 0;
      int length1 = s1.Length;
      int length2 = s2.Length;
      int index1 = 0;
      int index2 = 0;
      while (index1 < length1 && index2 < length2)
      {
        char c1 = s1[index1];
        char c2 = s2[index2];
        char[] chArray1 = new char[length1];
        char[] chArray2 = new char[length2];
        int num1 = 0;
        int num2 = 0;
        do
        {
          chArray1[num1++] = c1;
          ++index1;
          if (index1 < length1)
            c1 = s1[index1];
          else
            break;
        }
        while (char.IsDigit(c1) == char.IsDigit(chArray1[0]));
        do
        {
          chArray2[num2++] = c2;
          ++index2;
          if (index2 < length2)
            c2 = s2[index2];
          else
            break;
        }
        while (char.IsDigit(c2) == char.IsDigit(chArray2[0]));
        string s = new string(chArray1);
        string str = new string(chArray2);
        int num3 = !char.IsDigit(chArray1[0]) || !char.IsDigit(chArray2[0]) ? s.CompareTo(str) : int.Parse(s).CompareTo(int.Parse(str));
        if (num3 != 0)
          return num3;
      }
      return length1 - length2;
    }
  }
}

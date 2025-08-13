// ********************************************************************
// Type: Izi.Travel.Core.Context.NavigationParameter
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using System.Collections.Generic;
#nullable disable

namespace Izi.Travel.Core.Context
{
    public class NavigationParameter
    {
        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

        public NavigationParameter()
        {
        }

        public NavigationParameter(string key, object value)
        {
            _parameters[key] = value;
        }

        public void Add(string key, object value)
        {
            _parameters[key] = value;
        }

        public T GetValue<T>(string key)
        {
            if (_parameters.TryGetValue(key, out var value) && value is T typedValue)
            {
                return typedValue;
            }
            return default(T);
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            if (_parameters.TryGetValue(key, out var objValue) && objValue is T typedValue)
            {
                value = typedValue;
                return true;
            }
            value = default(T);
            return false;
        }
    }
}

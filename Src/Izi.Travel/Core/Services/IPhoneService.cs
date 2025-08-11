using System;
using System.Collections.Generic;

namespace Izi.Travel.Shell.Core.Services
{
    /// <summary>
    /// Interface for phone-specific services
    /// </summary>
    public interface IPhoneService
    {
        /// <summary>
        /// Gets the state dictionary for storing key-value pairs
        /// </summary>
        IStateDictionary State { get; }
    }

    /// <summary>
    /// Interface for a state dictionary that can store key-value pairs
    /// </summary>
    public interface IStateDictionary
    {
        /// <summary>
        /// Gets a value from the state dictionary
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TValue">The type of the value</typeparam>
        /// <param name="key">The key to get the value for</param>
        /// <returns>The value if found; otherwise, default(TValue)</returns>
        TValue Get<TKey, TValue>(TKey key) where TValue : class;

        /// <summary>
        /// Sets a value in the state dictionary
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TValue">The type of the value</typeparam>
        /// <param name="key">The key to set</param>
        /// <param name="value">The value to store</param>
        void Set<TKey, TValue>(TKey key, TValue value);
    }

    /// <summary>
    /// UWP implementation of IPhoneService
    /// </summary>
    public class UwpPhoneService : IPhoneService
    {
        private static readonly Lazy<UwpPhoneService> _instance = new Lazy<UwpPhoneService>(() => new UwpPhoneService());
        private readonly Dictionary<object, object> _state = new Dictionary<object, object>();

        /// <summary>
        /// Gets the singleton instance of the UwpPhoneService
        /// </summary>
        public static UwpPhoneService Instance => _instance.Value;

        /// <summary>
        /// Gets the state dictionary
        /// </summary>
        public IStateDictionary State { get; } = new UwpStateDictionary();

        private UwpPhoneService() { }
    }

    /// <summary>
    /// UWP implementation of IStateDictionary
    /// </summary>
    public class UwpStateDictionary : IStateDictionary
    {
        private readonly Dictionary<object, object> _dictionary = new Dictionary<object, object>();

        /// <inheritdoc />
        public TValue Get<TKey, TValue>(TKey key) where TValue : class
        {
            if (_dictionary.TryGetValue(key, out var value))
            {
                return value as TValue;
            }
            return default(TValue);
        }

        /// <inheritdoc />
        public void Set<TKey, TValue>(TKey key, TValue value)
        {
            _dictionary[key] = value;
        }
    }
}

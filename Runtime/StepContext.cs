using System;
using SerializableDictionary;
using TypesRecord;
using TypesRecord.TypeSelect;
using UnityEngine;

namespace Linxium.AsyncFlow {
    [Serializable]
    public class StepContext {
        [SerializeField, TypeSelect(typeof(IStep), false)] string stepType;
        [SerializeField] SerializableDictionary<string, string> stringProperties;
        [SerializeField] SerializableDictionary<string, int> intProperties;
        [SerializeField] SerializableDictionary<string, float> floatProperties;
        [SerializeField] SerializableDictionary<string, bool> boolProperties;

        public Type StepType => TypesRecordUtils.ResolveFromString(stepType);
        public SerializableDictionary<string, string> StringProperties => stringProperties;
        public SerializableDictionary<string, int> IntProperties => intProperties;
        public SerializableDictionary<string, float> FloatProperties => floatProperties;
        public SerializableDictionary<string, bool> BoolProperties => boolProperties;

        public T GetValue<T>(string key, T defaultValue) {
            if (typeof(T) == typeof(string)) {
                return StringProperties.ContainsKey(key) ? (T)(object)StringProperties[key] : defaultValue;
            }
            if (typeof(T) == typeof(int)) {
                return IntProperties.ContainsKey(key) ? (T)(object)IntProperties[key] : defaultValue;
            }
            if (typeof(T) == typeof(float)) {
                return FloatProperties.ContainsKey(key) ? (T)(object)FloatProperties[key] : defaultValue;
            }
            if (typeof(T) == typeof(bool)) {
                return BoolProperties.ContainsKey(key) ? (T)(object)BoolProperties[key] : defaultValue;
            }
            throw new NotSupportedException($"Type {typeof(T).Name} not supported");
        }
    }
}
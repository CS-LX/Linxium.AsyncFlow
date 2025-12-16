using System;
using System.Collections.Generic;
using UnityEngine;

namespace Linxium.AsyncFlow {
    [Serializable]
    public class AsyncFlowContext {
        [SerializeField] string id;
        [SerializeField] List<StepContext> steps;

        public string ID => id;

        public List<StepContext> Steps => steps;

        public static AsyncFlowContext FromJson(string json) => JsonUtility.FromJson<AsyncFlowContext>(json);

        public static string ToJson(AsyncFlowContext context) => JsonUtility.ToJson(context);
    }
}
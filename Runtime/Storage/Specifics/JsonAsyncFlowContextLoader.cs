using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Linxium.AsyncFlow.Storage.Specifics {
    [Preserve]
    public class JsonAsyncFlowContextLoader : IAsyncFlowContextLoader {
        Dictionary<string, AsyncFlowContext> contexts = new();

        public void Initialize() {
            TextAsset[] assets = Resources.LoadAll<TextAsset>("Definitions/AsyncFlow");
            foreach (TextAsset asset in assets) {
                try {
                    AsyncFlowContext flowContext = AsyncFlowContext.FromJson(asset.text);
                    contexts[flowContext.ID] = flowContext;
                }
                catch (Exception e) {
                    Debug.LogError("Failed to initialize AsyncFlowContext. Exception: \n" + e);
                }
            }
        }

        public AsyncFlowContext LoadFlow(string id) {
            return contexts.GetValueOrDefault(id);
        }
    }
}
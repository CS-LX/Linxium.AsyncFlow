using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;

namespace Linxium.AsyncFlow.Storage.Specifics {
    [Preserve]
    public class SOAsyncFlowContextLoader : IAsyncFlowContextLoader {
        Dictionary<string, AsyncFlowContext> contexts;

        public void Initialize() {
            contexts = Resources.LoadAll<AsyncFlowContextSO>("SO/AsyncFlow").ToDictionary(x => x.Context.ID, x => x.Context);
        }

        public AsyncFlowContext LoadFlow(string id) {
            return contexts.GetValueOrDefault(id);
        }
    }
}
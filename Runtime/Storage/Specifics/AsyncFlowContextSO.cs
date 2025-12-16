using UnityEngine;

namespace Linxium.AsyncFlow.Storage.Specifics {
    [CreateAssetMenu(fileName = "AsyncFlowContext", menuName = "GamePlay/AsyncFlowContext", order = 0)]
    public class AsyncFlowContextSO : ScriptableObject {
        [SerializeField] AsyncFlowContext context;

        public AsyncFlowContext Context => context;

#if UNITY_EDITOR
        public void SetContext(AsyncFlowContext ctx) {
            context = ctx;
        }
#endif
    }
}
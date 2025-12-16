using JetBrains.Annotations;

namespace Linxium.AsyncFlow.Storage {
    public interface IAsyncFlowContextLoader {
        void Initialize();
        [CanBeNull] AsyncFlowContext LoadFlow(string id);
    }
}
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Linxium.AsyncFlow {
    public interface IStep {
        void Initialize(StepContext context);
        UniTask ExecuteAsync(CancellationToken cancellationToken);
    }
}
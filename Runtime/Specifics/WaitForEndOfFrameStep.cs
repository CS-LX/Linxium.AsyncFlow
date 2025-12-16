using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;

namespace Linxium.AsyncFlow.Specifics {
    [Preserve]
    public class WaitForEndOfFrameStep : IStep {

        public void Initialize(StepContext context) {
        }

        public UniTask ExecuteAsync(CancellationToken cancellationToken) => UniTask.WaitForEndOfFrame(cancellationToken: cancellationToken);
    }
}
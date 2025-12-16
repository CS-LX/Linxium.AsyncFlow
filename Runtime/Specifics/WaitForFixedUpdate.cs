using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;

namespace Linxium.AsyncFlow.Specifics {
    [Preserve]
    public class WaitForFixedUpdateStep : IStep {

        public void Initialize(StepContext context) {
        }

        public UniTask ExecuteAsync(CancellationToken cancellationToken) => UniTask.WaitForFixedUpdate(cancellationToken: cancellationToken);
    }
}
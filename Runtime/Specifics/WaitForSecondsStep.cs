using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;

namespace Linxium.AsyncFlow.Specifics {
    [Preserve]
    public class WaitForSecondsStep : IStep {
        float waitTime;

        public void Initialize(StepContext context) {
            waitTime = context.GetValue("WaitTime", waitTime);
        }

        public UniTask ExecuteAsync(CancellationToken cancellationToken) => UniTask.WaitForSeconds(waitTime, cancellationToken: cancellationToken);
    }
}
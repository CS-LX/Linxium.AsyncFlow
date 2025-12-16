using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TypesRecord;
using UnityEngine.Events;

namespace Linxium.AsyncFlow {
    public class AsyncFlowRunner {
        List<IStep> steps = new();
        int currentStepIndex = 0;
        CancellationTokenSource cancellationTokenSource;

        public bool IsRunning { get; private set; }

        public void Start() {
            cancellationTokenSource = new CancellationTokenSource();
            StartInner(cancellationTokenSource.Token).Forget();
        }

        async UniTask StartInner(CancellationToken cancellationToken) {
            OnFlowStarted();
            IsRunning = true;
            try {
                while (currentStepIndex < steps.Count) {
                    cancellationToken.ThrowIfCancellationRequested();
                    var currentStep = steps[currentStepIndex];
                    await currentStep.ExecuteAsync(cancellationToken);
                    currentStepIndex++;
                }
            }
            catch (OperationCanceledException) {
            }
            finally {
                IsRunning = false;
                OnFlowEnded();
            }
        }

        public void Stop() {
            currentStepIndex = 0;
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }

        public void SetSteps(List<IStep> steps) {
            this.steps = steps;
            currentStepIndex = 0;
        }

        public void SetSteps(List<StepContext> contexts) {
            List<IStep> newSteps = new();
            foreach (StepContext stepContext in contexts) {
                IStep step = TypesRecordReader.CreateInstance<IStep>(nameof(IStep), stepContext.StepType);
                OnStepCreating(step);
                step.Initialize(stepContext);
                newSteps.Add(step);
            }
            SetSteps(newSteps);
        }

        public void SetSteps(AsyncFlowContext asyncFlowContext) {
            SetSteps(asyncFlowContext.Steps);
        }

        public void ClearSteps() {
            Stop();
            foreach (IStep step in steps) {
                if (step is IDisposable disposable) disposable.Dispose();
            }
            steps.Clear();
        }

        protected virtual void OnStepCreating(IStep step) {

        }

        protected virtual void OnFlowStarted() {

        }

        protected virtual void OnFlowEnded() {

        }
    }
}
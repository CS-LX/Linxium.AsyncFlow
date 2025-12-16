using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TypesRecord;

namespace Linxium.AsyncFlow.Storage {
    public class AsyncFlowContextLoader {
        List<IAsyncFlowContextLoader> loaders = new();

        public AsyncFlowContextLoader() {
            Type[] loadersType = TypesRecordReader.GetRecordedTypes(nameof(IAsyncFlowContextLoader));
            foreach (Type loaderType in loadersType) {
                IAsyncFlowContextLoader loader = TypesRecordReader.CreateInstance<IAsyncFlowContextLoader>(nameof(IAsyncFlowContextLoader), loaderType);
                loader.Initialize();
                loaders.Add(loader);
            }
        }

        [CanBeNull]
        public AsyncFlowContext GetContext(string id) {
            return loaders.Select(loader => loader.LoadFlow(id)).FirstOrDefault(context => context != null);
        }
    }
}
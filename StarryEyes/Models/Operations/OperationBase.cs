using System;
using System.Net;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace StarryEyes.Models.Operations
{
    public abstract class OperationBase<T> : IRunnerQueueable
    {
        public OperationBase() { }

        private readonly Subject<T> resultHandler = new Subject<T>();
        protected Subject<T> ResultHandler
        {
            get { return resultHandler; }
        }

        /// <summary>
        /// Run operation via operation queue.
        /// </summary>
        internal IObservable<T> Run(OperationPriority priority = OperationPriority.Middle)
        {
            return Observable.Defer(() =>
            {
                OperationQueueRunner.Enqueue(this, priority);
                return resultHandler;
            });
        }

        /// <summary>
        /// Run operation without operation queue
        /// </summary>
        public IObservable<T> RunImmediate()
        {
            return Observable.Defer(() => Observable.Start(() => RunCore()))
                .SelectMany(_ => _);
        }

        /// <summary>
        /// Core operation(Synchronously)
        /// </summary>
        protected abstract IObservable<T> RunCore();

        protected IObservable<string> GetExceptionDetail(Exception ex)
        {
            return Observable.Defer(() =>
                {
                    var wex = ex as WebException;
                    if (wex != null && wex.Response != null)
                    {
                        return Observable.Return(wex.Response)
                            .ObserveOn(TaskPoolScheduler.Default)
                            .SelectMany(r => r.DownloadStringAsync())
                            .Select(s => ParseErrorMessage(s));
                    }
                    else
                    {
                        return Observable.Return(ex.Message);
                    }
                });
        }

        private string ParseErrorMessage(string error)
        {
            if (error.StartsWith("{error:") && error.EndsWith("}"))
                return error.Substring(7, error.Length - 8);
            else
                return error;
        }

        IObservable<Unit> IRunnerQueueable.Run()
        {
            return Observable.Defer(() => RunCore())
                .Publish(connectable =>
                {
                    connectable.Subscribe(resultHandler);
                    return connectable;
                })
                .Select(_ => new Unit());
        }
    }
}
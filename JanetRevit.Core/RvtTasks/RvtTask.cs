using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanetRevit.Core.RvtTasks
{
    public class RvtTask
    {
        private EventHandler handler;
        private TaskCompletionSource<object> tcs;
        private ExternalEvent externalEvent;

        public RvtTask()
        {
            handler = new EventHandler();

            handler.EventCompleted += OnEventCompleted;

            externalEvent = ExternalEvent.Create(handler);
        }

        public Task<TResult> Run<TResult>(Func<UIApplication, TResult> func)
        {
            tcs = new TaskCompletionSource<object>();

            var task = Task.Run(async () => (TResult)await tcs.Task);

            handler.Func = (app) => func(app);

            externalEvent.Raise();

            //// var task = Task.FromResult((TResult)_tcs.Task.Result);

            return task;
        }

        public Task Run(Action<UIApplication> act)
        {
            tcs = new TaskCompletionSource<object>();

            handler.Func = (app) => { act(app); return new object(); };

            externalEvent.Raise();

            return tcs.Task;
        }

        private void OnEventCompleted(object sender, object result)
        {
            if (handler.Exception == null)
            {
                tcs.TrySetResult(result);
            }
            else
            {
                tcs.TrySetException(handler.Exception);
            }
        }

        private class EventHandler : IExternalEventHandler
        {
            private Func<UIApplication, object> _func;

            public event EventHandler<object> EventCompleted;

            public Exception Exception { get; private set; }

            public Func<UIApplication, object> Func
            {
                get => _func;
                set => _func = value ??
                               throw new ArgumentNullException();
            }

            public void Execute(UIApplication app)
            {
                object result = null;
                Exception = null;

                try
                {
                    result = Func(app);
                }
                catch (Exception ex)
                {
                    Exception = ex;
                }
                EventCompleted?.Invoke(this, result);
            }

            public string GetName()
            {
                return GetType().Name;
            }
        }
    }
}

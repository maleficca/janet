using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace JanetRevit.Core.Helpers
{
    public class AsyncHelper
    {
        public static T HandleAsync<T>(Func<Task<T>> func)
        {
            var tcs = new TaskCompletionSource<T>();

            var thread = new Thread(delegate ()
            {
                var dispatcher = Dispatcher.CurrentDispatcher;
                dispatcher.InvokeAsync(async delegate
                {
                    var obj = await func();

                    tcs.SetResult(obj);
                    dispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);
                });

                Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return tcs.Task.Result;
        }

    }
}

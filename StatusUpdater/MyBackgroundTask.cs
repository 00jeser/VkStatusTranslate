using System;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;

namespace StatusUpdater
{
    class MyBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral _deferral = taskInstance.GetDeferral();

            // некоторая работа

            _deferral.Complete();
            DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 20, 0) }; // 1 секунда
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {

        }
    }
}

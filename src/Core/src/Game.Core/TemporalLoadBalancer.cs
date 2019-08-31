﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using static Game.Core.Debug;

namespace Game.Core
{
    /// <summary>
    /// Distributes work (the execution of coroutines) over several frames to avoid freezes by soft-limiting execution time.
    /// </summary>
    public class TemporalLoadBalancer
    {
        List<IEnumerator> _tasks = new List<IEnumerator>();
        Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// Adds a task coroutine and returns it.
        /// </summary>
        public IEnumerator AddTask(IEnumerator taskCoroutine)
        {
            _tasks.Add(taskCoroutine);
            return taskCoroutine;
        }

        public void CancelTask(IEnumerator taskCoroutine) => _tasks.Remove(taskCoroutine);

        public void RunTasks(float desiredWorkTime)
        {
            Assert(desiredWorkTime >= 0);
            if (_tasks.Count == 0)
                return;
            _stopwatch.Reset();
            _stopwatch.Start();
            // Run the tasks.
            do
            {
                if (!_tasks[0].MoveNext()) // Try to execute an iteration of a task. Remove the task if it's execution has completed.
                    _tasks.RemoveAt(0);
            } while (_tasks.Count > 0 && _stopwatch.Elapsed.TotalSeconds < desiredWorkTime);
            _stopwatch.Stop();
        }

        public void WaitForTask(IEnumerator taskCoroutine)
        {
            Assert(_tasks.Contains(taskCoroutine));
            while (taskCoroutine.MoveNext()) { }
            _tasks.Remove(taskCoroutine);
        }

        public void WaitForAllTasks()
        {
            foreach (var task in _tasks)
                while (task.MoveNext()) { }
            _tasks.Clear();
        }
    }
}
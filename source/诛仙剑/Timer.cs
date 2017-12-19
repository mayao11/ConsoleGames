using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace ConsoleGame
{
    class Timer
    {
        public static int GetTimeStamp()
        {
            TimeSpan span = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            int ts = (int)span.TotalSeconds;
            return ts;
        }

        public static int begin_time = 0;

        private static SortedDictionary<int, object> time_events = new SortedDictionary<int, object>();

        public static void Init()
        {
            begin_time = GetTimeStamp();
        }

        public static void AddTimer(int seconds, object time_event)
        {
            int t = seconds + GetTimeStamp() - begin_time;
            if (time_events.ContainsKey(t))
            {
                throw new InvalidOperationException("同样时间的Timer已经存在了，不能添加");
            }
            time_events.Add(t, time_event);
        }

        public static object CheckTimer()
        {
            int cur = GetTimeStamp() - begin_time;

            Debug.Print("CheckTimer {0}", cur);

            object e = null;
            if (time_events.ContainsKey(cur))
            {
                e = time_events[cur];
                time_events.Remove(cur);
            }
            return e;
        }
    }
}

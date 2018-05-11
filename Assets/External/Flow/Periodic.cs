// (C) 2012-2018 Christian Schladetsch. See https://github.com/cschladetsch/Flow.

using System;

namespace Flow.Impl
{
    internal class Periodic : Subroutine<bool>, IPeriodic
    {
        public TimeSpan TimeRemaining
        {
            get { throw new NotImplementedException(); }
            set { }
        }
        public event TransientHandler Elapsed;

        public DateTime TimeStarted { get; private set; }
        public TimeSpan Interval { get; set; }

        internal Periodic(IKernel kernel, TimeSpan interval)
        {
            Interval = interval;
            TimeStarted = kernel.Time.Now;
            _expires = TimeStarted + Interval;
            Sub = StepTimer;
        }

        private bool StepTimer(IGenerator self)
        {
            if (Kernel.Time.Now < _expires)
                return true;

            if (Elapsed != null)
                Elapsed(this);

            _expires = Kernel.Time.Now + Interval;

            return true;
        }

        private DateTime _expires;
    }
}
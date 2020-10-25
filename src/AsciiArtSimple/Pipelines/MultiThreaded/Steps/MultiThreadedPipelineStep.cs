using System;
using System.Threading;

namespace AsciiArtSimple.Pipelines.MultiThreaded.Steps
{
    public interface IPipelineStatus
    {
        PipelineStepStatus GetStatusPreviousStep(IPipelineStep step);
    }

    interface IMultiThreadedPipelineStep : IPipelineStep, IDisposable
    {
    }

    public abstract class MultiThreadedPipelineStep : PipelineStep, IMultiThreadedPipelineStep
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private PipelineStepStatus _status = PipelineStepStatus.Running;
        private readonly IPipelineStatus _pipelineStatus;

        protected MultiThreadedPipelineStep(int number, IPipelineStatus pipelineStatus) : base(number)
        {
            _pipelineStatus = pipelineStatus;
        }

        public override PipelineStepStatus Status
        {
            get => _status;

            protected set
            {
                _lock.EnterWriteLock();
                _status = value;
                _lock.ExitWriteLock();
            }
        }

        protected bool GetStatusPreviousStep(bool currentStatus)
        {
            if (currentStatus) return true;
            return _pipelineStatus.GetStatusPreviousStep(this) == PipelineStepStatus.Complited;
        }

        public void Dispose()
        {
            ((IDisposable)_lock)?.Dispose();
        }
    }
}
#include <thread>

#include "Task.hpp"

/// <summary>
/// Initializes a new instance of this class.
/// </summary>
Task::Task()
    : _ptrThread(nullptr)
    , _nativeHandle(nullptr)
    , _ptrThreadTerminator(nullptr)
    , _status(Status::STOPPED)
{

}

/// <summary>
/// Initializes a new instance of this class.
/// </summary>
/// <param name="threadTerminator">Function that will terminate the thread.</param>
Task::Task(ThreadTerminator * threadTerminator)
    : _ptrThread(nullptr)
    , _nativeHandle(nullptr)
    , _ptrThreadTerminator(threadTerminator)
    , _status(Status::STOPPED)
{

}

/// <summary>
/// Destructs the instance of this class.
/// </summary>
Task::~Task()
{
    if (_ptrThread != nullptr && _ptrThreadTerminator != nullptr)
    {
        (*_ptrThreadTerminator)(_nativeHandle);

        delete _ptrThread;

        _ptrThread = nullptr;
    }
}

/// <summary>
/// Indicates whether tkas is running.
/// </summary>
/// <returns>True if task is running, false otherwise.</returns>
bool Task::IsRunning()
    const noexcept
{
    return _status == Status::RUNNING;
}

/// <summary>
/// Starts the task.
/// </summary>
void Task::Start()
{
    LockGuardMutex lock(_mutex);

    if (IsRunning())
    {
        return;
    }

    _status = Status::RUNNING;

    _ptrThread = new std::thread(&Task::ThreadRoutine, this);
    _nativeHandle = _ptrThread->native_handle();
}

/// <summary>
/// Stops the task.
/// </summary>
void Task::Stop()
{
    LockGuardMutex lock(_mutex);

    if (!IsRunning() || _ptrThread == nullptr)
    {
        return;
    }

    _status = Status::STOPPED;

    if (_ptrThreadTerminator != nullptr)
    {
        (*_ptrThreadTerminator)(_nativeHandle);
        // TODO: Resources should be released here, but DeleteThread() causes an exception.
    }
}

/// <summary>
/// Joins the task.
/// </summary>
void Task::Join()
{
    LockGuardMutex lock(_mutex);

    if (!IsRunning()
        || _ptrThread == nullptr
        || !_ptrThread->joinable()
    )
    {
        return;
    }

    _status = Status::STOPPED;

    _ptrThread->join();

    DeleteThread();
}

/// <summary>
/// Sleeps the task.
/// </summary>
/// <param name="msg">Sleep duration in MS.</param>
void Task::Sleep(int miliseconds)
{
    std::this_thread::sleep_for(std::chrono::milliseconds(miliseconds));
}

/// <summary>
/// Releases resources related to thread.
/// </summary>
void Task::DeleteThread()
{
    delete _ptrThread;
    _ptrThread = nullptr;
}
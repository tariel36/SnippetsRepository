#ifndef ITHREAD
#define ITHREAD

#include <thread>
#include <mutex>

#include "../Types/Types.hpp"

/// <summary>
/// A class that represents asynchrounous task. Should be inherited.
/// </summary>
class Task
{
public:
    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    Task();

    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    /// <param name="threadTerminator">Function that will terminate the thread.</param>
    Task(ThreadTerminator * threadTerminator);

    /// <summary>
    /// Destructs the instance of this class.
    /// </summary>
    virtual ~Task();

    /// <summary>
    /// Indicates whether tkas is running.
    /// </summary>
    /// <returns>True if task is running, false otherwise.</returns>
    bool IsRunning() const noexcept;

    /// <summary>
    /// Starts the task.
    /// </summary>
    void Start();

    /// <summary>
    /// Stops the task.
    /// </summary>
    void Stop();

    /// <summary>
    /// Joins the task.
    /// </summary>
    void Join();

    /// <summary>
    /// Function that is executed on another thread.
    /// </summary>
    virtual void ThreadRoutine() = 0;

protected:
    /// <summary>
    /// Sleeps the task.
    /// </summary>
    /// <param name="msg">Sleep duration in MS.</param>
    void Sleep(int = 100);

private:
    /// <summary>
    /// Task status.
    /// </summary>
    enum Status
    {
        /// <summary>
        /// Task is not running.
        /// </summary>
        STOPPED = 0,
        /// <summary>
        /// Task is running.
        /// </summary>
        RUNNING = 1
    };

    /// <summary>
    /// Current task status.
    /// </summary>
    Task::Status _status;

    /// <summary>
    /// Synchronization context.
    /// </summary>
    std::mutex _mutex;

    /// <summary>
    /// Pointer th thread.
    /// </summary>
    std::thread * _ptrThread;

    /// <summary>
    /// Native thread handle.
    /// </summary>
    std::thread::native_handle_type _nativeHandle;

    /// <summary>
    /// Function that terminates the thread.
    /// </summary>
    ThreadTerminator * _ptrThreadTerminator;
    
    /// <summary>
    /// Releases resources related to thread.
    /// </summary>
    void DeleteThread();
};

#endif
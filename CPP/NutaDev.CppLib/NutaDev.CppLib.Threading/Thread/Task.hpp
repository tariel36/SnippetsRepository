// The MIT License (MIT)
// 
// Copyright (c) 2022 tariel36
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#ifndef NUTADEV_CPPLIB_THREADING_THREAD_TASK_HPP
#define NUTADEV_CPPLIB_THREADING_THREAD_TASK_HPP

#include <thread>
#include <mutex>

#include "../Types/Types.hpp"

namespace NutaDev
{
    namespace CppLib
    {
        namespace Threading
        {
            namespace Thread
            {
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
                    Task(NutaDev::CppLib::Threading::Types::ThreadTerminator * threadTerminator);

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
                    NutaDev::CppLib::Threading::Types::ThreadTerminator * _ptrThreadTerminator;

                    /// <summary>
                    /// Releases resources related to thread.
                    /// </summary>
                    void DeleteThread();
                };
            }
        }
    }
}

#endif

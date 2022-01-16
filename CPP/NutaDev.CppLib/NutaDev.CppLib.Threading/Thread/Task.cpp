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

#include "Task.hpp"

namespace NutaDev
{
    namespace CppLib
    {
        namespace Threading
        {
            namespace Thread
            {
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
                Task::Task(NutaDev::CppLib::Threading::Types::ThreadTerminator * threadTerminator)
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
                    NutaDev::CppLib::Threading::Types::LockGuardMutex lock(_mutex);

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
                    NutaDev::CppLib::Threading::Types::LockGuardMutex lock(_mutex);

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
                    NutaDev::CppLib::Threading::Types::LockGuardMutex lock(_mutex);

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
            }
        }
    }
}

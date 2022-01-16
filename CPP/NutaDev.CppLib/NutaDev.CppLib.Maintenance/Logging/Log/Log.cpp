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

#include <fstream>
#include <chrono>
#include <vector>
#include <ctime>

#include "NutaDev.CppLib.Types/Helpers/String/string.hpp"
#include "NutaDev.CppLib.Types/Helpers/Time/time.hpp"

#include "Log.hpp"

namespace NutaDev
{
    namespace CppLib
    {
        namespace Maintenance
        {
            namespace Logging
            {
                namespace Log
                {
                    /// <summary>
                    /// Initializes a new instance of this class.
                    /// </summary>
                    Log::Log()
                        : _logLevel(LogLevel::All)
                        , _ofStream(nullptr)
                        , _lastException(nullptr)
                        , _forceFlush(false)
                        , _forceFileFlush(false)
                        , _throwIfCantWriteToStream(false)
                    {
                        OpenFileStream();
                    };

                    /// <summary>
                    /// Destructs instance of this class.
                    /// </summary>
                    Log::~Log()
                    {
                        CloseFileStream();

                        if (_ofStream != nullptr)
                        {
                            try
                            {
                                _ofStream->flush();
                                _ofStream->close();
                            }
                            catch (...)
                            {

                            }

                            delete _ofStream;
                        }
                    }

                    /// <summary>
                    /// Writes to log.
                    /// </summary>
                    /// <param name="msg">Log message.</param>
                    /// <param name="ex">Exception pointer.</param>
                    /// <param name="messageLevel">Log level.</param>
                    void Log::WriteToStream(const std::string & msg, const std::exception * ex, LogLevel messageLevel)
                    {
                        if (messageLevel > GetLogLevel()) return;
                        if (!CanWriteToStream())
                        {
                            if (GetThrowIfCantWriteToStream())
                            {
                                throw std::exception("LOG: Can't write to stream!");
                            }

                            return;
                        }

                        std::string timestampStr = NutaDev::CppLib::Types::Helpers::Time::getCurrentDateTime("[%Y-%m-%d %H:%M:%S] ");
                        size_t timestampStrLen = timestampStr.length();

                        std::vector<std::string> lines = NutaDev::CppLib::Types::Helpers::String::split(msg, '\n');

                        *_ofStream << timestampStr << lines[0];

                        if (lines.size() > 1)
                        {
                            *_ofStream << '\n';
                        }

                        for (size_t i = 1, size = lines.size(); i < size; ++i)
                        {
                            *_ofStream << NutaDev::CppLib::Types::Helpers::String::moveRight(lines[i], timestampStrLen) << "\n";
                        }

                        if (ex != nullptr)
                        {
                            lines = NutaDev::CppLib::Types::Helpers::String::split(ex->what(), '\n');

                            for (size_t i = 0, size = lines.size(); i < size; ++i)
                            {
                                *_ofStream << NutaDev::CppLib::Types::Helpers::String::moveRight(lines[i], timestampStrLen) << "\n";
                            }
                        }

                        *_ofStream << '\n';

                        if (_forceFlush)
                        {
                            *_ofStream << std::flush;
                        }

                        if (_forceFileFlush)
                        {
                            CloseFileStream();
                            OpenFileStream();
                        }
                    }

                    /// <summary>
                    /// Opens the stream.
                    /// </summary>
                    /// <returns>True if stream has been opened, false otherwise.</returns>
                    bool Log::OpenFileStream()
                    {
                        try
                        {
                            if (_ofStream == nullptr)
                            {
                                _ofStream = new std::ofstream(Log::CreateFilePath());

                                return true;
                            }
                        }
                        catch (std::exception & ofsException)
                        {
                            *_lastException = ofsException;
                        }

                        return false;
                    }

                    /// <summary>
                    /// Closes the stream.
                    /// </summary>
                    /// <returns>True if stream has been closed, false otherwise.</returns>
                    bool Log::CloseFileStream()
                    {
                        try
                        {
                            if (_ofStream != nullptr)
                            {
                                _ofStream->close();
                                _ofStream->flush();

                                delete _ofStream;

                                _ofStream = nullptr;

                                return true;
                            }
                        }
                        catch (std::exception & ofsException)
                        {
                            *_lastException = ofsException;
                        }

                        return false;
                    }

                    /// <summary>
                    /// Writes to log.
                    /// </summary>
                    /// <param name="msg">Log message.</param>
                    /// <param name="ex">Exception pointer.</param>
                    /// <param name="messageLevel">Log level.</param>
                    Log & Log::FlushBuffer(LogLevel messageLevel)
                    {
                        WriteToStream(_logBuffer.str(), nullptr, messageLevel);

                        _logBuffer.str("");
                        _logBuffer.clear();

                        return *this;
                    }

                    /// <summary>
                    /// Indicates whether can write to stream.
                    /// </summary>
                    /// <returns>True if can write to stream, false otherwise.</returns>
                    bool Log::CanWriteToStream() const
                    {
                        return _ofStream != nullptr && _ofStream->is_open() && _ofStream->good();
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator<<(const char * str)
                    {
                        _logBuffer << str;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (const std::string & str)
                    {
                        _logBuffer << str;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (bool val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (short val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (unsigned short val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (int val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (unsigned val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (long val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (unsigned long val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (float val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (double val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (long double val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="val">Value to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (void * val)
                    {
                        _logBuffer << val;
                        return *this;
                    }

                    /// <summary>
                    /// Prints provided value to log.
                    /// </summary>
                    /// <param name="ex">Exception to print.</param>
                    /// <returns>Reference to itself.</returns>
                    Log & Log::operator << (const std::exception & ex)
                    {
                        _logBuffer << ex.what();

                        return *this;
                    }

                    /// <summary>
                    /// Creates file path.
                    /// </summary>
                    /// <returns>File path.</returns>
                    std::string Log::CreateFilePath()
                    {
                        std::chrono::time_point<std::chrono::system_clock> nowChrono = std::chrono::system_clock::now();
                        std::time_t nowTime = std::chrono::system_clock::to_time_t(nowChrono);
                        struct tm nowTm;
                        localtime_s(&nowTm, &nowTime);

                        std::stringstream ss;

                        ss << "logs/" << NutaDev::CppLib::Types::Helpers::Time::getCurrentDateTime("%Y%m%d_%H%M%S") << ".txt";

                        return ss.str();
                    }

                    ///// <summary>
                    ///// Returns a new instance of object.
                    ///// </summary>
                    ///// <param name="messageLevel">Message log level.</param>
                    ///// <returns>The EndLogManipulator object.</returns>
                    //EndLogManipulator::EndLogManipulator EndLog(LogLevel messageLevel)
                    //{
                    //    return EndLogManipulator::EndLogManipulator(messageLevel);
                    //}

                    /// <summary>
                    /// Writes end log to logger.
                    /// </summary>
                    /// <param name="log">Logger instance.</param>
                    /// <param name="endlog">End log manipulator.</param>
                    /// <returns>Reference to itself.</returns>
                    //Log & operator << (Log & log, const EndLogManipulator::EndLogManipulator & endlog)
                    //{
                    //    endlog(log);

                    //    return log;
                    //}
                }
            }
        }
    }
}

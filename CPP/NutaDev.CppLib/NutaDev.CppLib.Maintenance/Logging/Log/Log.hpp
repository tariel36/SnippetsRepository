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

#ifndef NUTADEV_CPPLIB_MAINTENANCE_LOGGING_LOG_LOG_HPP
#define NUTADEV_CPPLIB_MAINTENANCE_LOGGING_LOG_LOG_HPP

#include <string>
#include <sstream>

#include "../ForwardDeclarations.hpp"
#include "../LogLevel.hpp"

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
                    /// The logger,
                    /// </summary>
                    class Log
                    {
                    public:
                        /// <summary>
                        /// Friend class forward reference.
                        /// </summary>
                        friend class ::NutaDev::CppLib::Maintenance::Logging::EndLogManipulator::EndLogManipulator;

                        /// <summary>
                        /// Static instance to logger.
                        /// </summary>
                        /// <returns>Logger instance.</returns>
                        static Log & I()
                        {
                            static Log instance;
                            return instance;
                        }

                        /// <summary>
                        /// Writes to log.
                        /// </summary>
                        /// <param name="msg">Log message.</param>
                        /// <param name="ex">Exception pointer.</param>
                        /// <param name="messageLevel">Log level.</param>
                        static void Write(const std::string & msg, const std::exception * ex = nullptr, LogLevel messageLevel = LogLevel::Info)
                        {
                            I().WriteToStream(msg, ex, messageLevel);
                        }

                        /// <summary>
                        /// Gets the field value.
                        /// </summary>
                        /// <returns>True if throws an exception if can't write to stream, false otherwise.</returns>
                        bool GetThrowIfCantWriteToStream()                    const noexcept { return _throwIfCantWriteToStream; }

                        /// <summary>
                        /// Gets the field value.
                        /// </summary>
                        /// <returns>True if file force flushes, false otherwise.</returns>
                        bool GetForceFileFlish()                                const noexcept { return _forceFileFlush; }

                        /// <summary>
                        /// Gets the field value.
                        /// </summary>
                        /// <returns>True if force flushes, false otherwise.</returns>
                        bool GetForceFlush()                                    const noexcept { return _forceFlush; }

                        /// <summary>
                        /// Gets the field value.
                        /// </summary>
                        /// <returns>Last exception.</returns>
                        const std::exception *    GetLastException()                                const noexcept { return _lastException; }

                        /// <summary>
                        /// Gets the field value.
                        /// </summary>
                        /// <returns>Current log level.</returns>
                        LogLevel GetLogLevel()                                    const noexcept { return _logLevel; }

                        /// <summary>
                        /// Sets the field.
                        /// </summary>
                        /// <param name="logLevel">The value.</param>
                        void SetLogLevel(LogLevel logLevel)                    noexcept { _logLevel = logLevel; }

                        /// <summary>
                        /// Sets the field.
                        /// </summary>
                        /// <param name="forceFlush">The value.</param>
                        void SetForceFlush(bool forceFlush)                    noexcept { _forceFlush = forceFlush; }

                        /// <summary>
                        /// Sets the field.
                        /// </summary>
                        /// <param name="forceFlush">The value.</param>
                        void SetForceFileFlush(bool forceFlush)                noexcept { _forceFileFlush = forceFlush; }

                        /// <summary>
                        /// Sets the field.
                        /// </summary>
                        /// <param name="throwIfCant">The value.</param>
                        void SetThrowIfCantWriteToStream(bool throwIfCant)    noexcept { _throwIfCantWriteToStream = throwIfCant; }

                        /// <summary>
                        /// Writes to log.
                        /// </summary>
                        /// <param name="msg">Log message.</param>
                        /// <param name="ex">Exception pointer.</param>
                        /// <param name="messageLevel">Log level.</param>
                        void WriteToStream(const std::string & msg, const std::exception * ex, LogLevel messageLevel);

                        /// <summary>
                        /// Indicates whether can write to stream.
                        /// </summary>
                        /// <returns>True if can write to stream, false otherwise.</returns>
                        bool CanWriteToStream() const;

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (const char * str);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (const std::string & str);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (bool val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (short val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (unsigned short val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (int val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (unsigned int val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (long val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (unsigned long val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (float val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (double val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (long double val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="val">Value to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (void * val);

                        /// <summary>
                        /// Prints provided value to log.
                        /// </summary>
                        /// <param name="ex">Exception to print.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & operator << (const std::exception & ex);

                        /// <summary>
                        /// Destructs instance of this class.
                        /// </summary>
                        ~Log();

                        /// <summary>
                        /// Removes copy constructor.
                        /// </summary>
                        Log(Log const &) = delete;

                        /// <summary>
                        /// Removes assign operator.
                        /// </summary>
                        void operator=(Log const &) = delete;

                    private:
                        /// <summary>
                        /// Current log level.
                        /// </summary>
                        LogLevel _logLevel;

                        /// <summary>
                        /// Output file stream.
                        /// </summary>
                        std::ofstream *        _ofStream;

                        /// <summary>
                        /// Last exception reference.
                        /// </summary>
                        std::exception *    _lastException;

                        /// <summary>
                        /// Forces buffer to flush.
                        /// </summary>
                        bool _forceFlush;

                        /// <summary>
                        /// Forces file to flush.
                        /// </summary>
                        bool _forceFileFlush;

                        /// <summary>
                        /// Whether should throw an exception if can't write to stream.
                        /// </summary>
                        bool _throwIfCantWriteToStream;

                        /// <summary>
                        /// Internal buffer.
                        /// </summary>
                        std::ostringstream _logBuffer;

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        Log();

                        /// <summary>
                        /// Opens the stream.
                        /// </summary>
                        /// <returns>True if stream has been opened, false otherwise.</returns>
                        bool OpenFileStream();

                        /// <summary>
                        /// Closes the stream.
                        /// </summary>
                        /// <returns>True if stream has been closed, false otherwise.</returns>
                        bool CloseFileStream();

                        /// <summary>
                        /// Flushes the internal buffer.
                        /// </summary>
                        /// <param name="messageLevel">Log level.</param>
                        /// <returns>Reference to itself.</returns>
                        Log & FlushBuffer(LogLevel messageLevel = LogLevel::All);

                        /// <summary>
                        /// Creates file path.
                        /// </summary>
                        /// <returns>File path.</returns>
                        static std::string CreateFilePath();
                    };

                    /// <summary>
                    /// Returns a new instance of object.
                    /// </summary>
                    /// <param name="messageLevel">Message log level.</param>
                    /// <returns>The EndLogManipulator object.</returns>
                    //EndLogManipulator::EndLogManipulator EndLog(LogLevel messageLevel);

                    /// <summary>
                    /// Writes end log to logger.
                    /// </summary>
                    /// <param name="log">Logger instance.</param>
                    /// <param name="endlog">End log manipulator.</param>
                    /// <returns>Reference to itself.</returns>
                    //Log & operator << (Log & log, const EndLogManipulator::EndLogManipulator & endlog);
                }
            }
        }
    }
}

#endif

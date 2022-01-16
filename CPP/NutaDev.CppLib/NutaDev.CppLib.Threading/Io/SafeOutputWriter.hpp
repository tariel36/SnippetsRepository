#include <mutex>
#include <iostream>
#include <sstream>

#include "../Types/Types.hpp"

/// <summary>
/// Thread safe standard output writer.
/// </summary>
class SafeOutputWriter
{
private:
    /// <summary>
    /// Synchronization context.
    /// </summary>
    static std::mutex _mutex;

public:
    /// <summary>
    /// Write to standard output.
    /// </summary>
    /// <param name="value">Value to write.</param>
    /// <param name="args">Arguments.</param>
    template<typename T, typename... Args>
    static void Write(T value, Args... args)
    {
        LockGuardMutex lock(_mutex);

        std::cout << StringConverter::ToString(value, args ...);
    }
};
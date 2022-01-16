#include <thread>
#include <mutex>
#include <functional>

/// <summary>
/// Lock guard for mutex.
/// </summary>
typedef std::lock_guard<std::mutex> LockGuardMutex;

/// <summary>
/// Delegate for function that terminates thread.
/// </summary>
typedef std::function<void(std::thread::native_handle_type)> ThreadTerminator;
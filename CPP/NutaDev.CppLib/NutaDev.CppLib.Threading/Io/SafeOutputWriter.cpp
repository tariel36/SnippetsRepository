#include "SafeOutputWriter.hpp"

/// <summary>
/// Synchronization context.
/// </summary>
std::mutex SafeOutputWriter::_mutex;
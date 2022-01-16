#include <iostream>
#include <sstream>

/// <summary>
/// Converts value to std::string.
/// </summary>
class StringConverter
{
private:
    /// <summary>
    /// Write to standard output.
    /// </summary>
    /// <param name="ss">Target stream.</param>
    /// <param name="value">Value to write.</param>
    template <typename T>
    static void Write(std::stringstream & ss, T value)
    {
        ss << value << std::endl;
    }

    /// <summary>
    /// Write to standard output.
    /// </summary>
    /// <param name="ss">Target stream.</param>
    /// <param name="value">Value to write.</param>
    /// <param name="args">Additional arguments.</param>
    template<typename T, typename... Args>
    static void Write(std::stringstream & ss, T value, Args... args)
    {
        ss << value;

        Write(ss, args...);
    }

public:
    /// <summary>
    /// Write to standard output.
    /// </summary>
    /// <param name="value">Value to write.</param>
    /// <param name="args">Additional arguments.</param>
    template<typename T, typename... Args>
    static std::string ToString(T value, Args... args)
    {
        std::stringstream ss;

        ss << value;

        Write(ss, args...);

        return ss.str();
    }
};
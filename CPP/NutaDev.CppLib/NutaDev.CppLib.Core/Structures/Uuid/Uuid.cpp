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

#include "Uuid.hpp"

#include <string>
#include <sstream>
#include <iomanip>
#include <random>
#include <limits>

namespace NutaDev
{
    namespace CppLib
    {
        namespace Core
        {
            namespace Structures
            {
                namespace Uuid
                {
                    /// <summary>
                    /// Generates a new UUID object.
                    /// </summary>
                    /// <returns>A UUID object.</returns>
                    UUID UUID::Generate()
                    {
                        static std::random_device rd;
                        static std::mt19937 gen(rd());
                        static std::uniform_int_distribution<> dis(INT_MIN, INT_MAX);
                        static std::uniform_int_distribution<> dis8_11(8, 11);

                        int data2 = dis(gen);
                        int data3 = dis(gen);

                        data2 &= 0xFFFF0FFF;
                        data2 |= 0x00004000;

                        data3 &= 0x0FFFFFFF;
                        data3 |= (dis8_11(gen) << 28);

                        UUID uuid = UUID(
                            dis(gen),
                            data2,
                            data3,
                            dis(gen));

                        return uuid;
                    }

                    /// <summary>
                    /// Initializes a new instance of the class.
                    /// </summary>
                    /// <param name="data1">First quarter.</param>
                    /// <param name="data2">Second quarter.</param>
                    /// <param name="data3">Third quarter.</param>
                    /// <param name="data4">Fourth quarter.</param>
                    UUID::UUID(int data1, int data2, int data3, int data4)
                        : _data1(data1)
                        , _data2(data2)
                        , _data3(data3)
                        , _data4(data4)
                    {
                    }

                    /// <summary>
                    /// Initializes a new instance of the class.
                    /// </summary>
                    /// <param name="str">String representation of a UUID.</param>
                    UUID::UUID(const std::string & str)
                    {
                        std::string item;
                        std::stringstream ss(str);
                        std::stringstream intSS;

                        for (int i = 0; i <= 4; ++i)
                        {
                            std::getline(ss, item, '-');
                            intSS << item;
                        }

                        ss = std::stringstream(intSS.str());

                        for (int i = 0; i < 4; ++i)
                        {
                            char buff[9];
                            ss.get(buff, 9);
                            intSS = std::stringstream();
                            intSS << std::hex << std::string(buff);

                            int temp;
                            intSS >> temp;

                            switch (i)
                            {
                            case 0: _data1 = 0 | temp; break;
                            case 1: _data2 = 0 | temp; break;
                            case 2: _data3 = 0 | temp; break;
                            case 3: _data4 = 0 | temp; break;
                            }
                        }
                    }

                    /// <summary>
                    /// Compares UUID to another UUID.
                    /// </summary>
                    /// <param name="uuid">Another UUID.</param>
                    /// <returns>A value indicating comarison result, -1, 0, or 1.</returns>
                    int UUID::Compare(const UUID & uuid) const
                    {
                        return ToString().compare(uuid.ToString());
                    }

                    /// <summary>
                    /// Indicates whether this object is smaller than another one.
                    /// </summary>
                    /// <param name="right">Another UUID.</param>
                    /// <returns>True if it's smaller, false otherwise.</returns>
                    bool UUID::operator <(const UUID & right) const
                    {
                        if (_data1 != right._data1) return _data1 < right._data1;
                        if (_data2 != right._data2) return _data2 < right._data2;
                        if (_data3 != right._data3) return _data3 < right._data3;

                        return _data4 < right._data4;
                    }

                    /// <summary>
                    /// Converts UUID into string.
                    /// </summary>
                    /// <returns>A string representation.</returns>
                    std::string UUID::ToString() const
                    {
                        std::stringstream outStream;
                        std::stringstream strStream;

                        outStream << std::hex << std::setw(8) << std::setfill('0') << _data1 << '-';

                        strStream << std::hex << std::setw(8) << std::setfill('0') << _data2;
                        outStream << strStream.str().substr(0, 4) << '-' << strStream.str().substr(4, 4) << '-';

                        strStream = std::stringstream();
                        strStream << std::hex << std::setw(8) << std::setfill('0') << _data3;
                        outStream << strStream.str().substr(0, 4) << '-' << strStream.str().substr(4, 4);

                        outStream << std::hex << std::setw(8) << std::setfill('0') << _data4;

                        return outStream.str();
                    }
                }
            }
        }
    }
}

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

#ifndef NUTADEV_CPPLIB_MATH_MODELS_POINT_POINT_HPP
#define NUTADEV_CPPLIB_MATH_MODELS_POINT_POINT_HPP

namespace NutaDev
{
    namespace CppLib
    {
        namespace Math
        {
            namespace Models
            {
                namespace Point
                {
                    /// <summary>
                    /// The point structure.
                    /// </summary>
                    template <typename T>
                    class Point
                    {
                    public:
                        /// <summary>
                        /// Destructs this object.
                        /// </summary>
                        virtual ~Point()
                        {

                        }
                    };

                    /// <summary>
                    /// The point structure with 2 coordinates.
                    /// </summary>
                    template <typename T>
                    class Point2d
                        : public Point<T>
                    {
                    public:
                        /// <summary>
                        /// X position.
                        /// </summary>
                        T X;

                        /// <summary>
                        /// Y position.
                        /// </summary>
                        T Y;
                    };

                    /// <summary>
                    /// Specialized type for point with integer coordinates.
                    /// </summary>
                    typedef Point2d<int> Point2dInt;

                    /// <summary>
                    /// Calculates angle between two points.
                    /// </summary>
                    /// <param name="point1">The first point.</param>
                    /// <param name="point2">The second point.</param>
                    /// <returns>Angle between two points.</returns>
                    inline double angleBetweenTwoPoints(const Point2dInt & point1, const Point2dInt & point2);

                    /// <summary>
                    /// Calculates the distance between two points.
                    /// </summary>
                    /// <param name="point1">The first point.</param>
                    /// <param name="point2">The second point.</param>
                    /// <returns>Distance between two points.</returns>
                    inline double distanceBetweenTwoPoints(const Point2dInt & point1, const Point2dInt & point2);
                }
            }
        }
    }
}

#endif

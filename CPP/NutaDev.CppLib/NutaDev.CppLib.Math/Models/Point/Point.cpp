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

#include <cmath>

#include "../../Constants/Constants.hpp"

#include "Point.hpp"

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
                    /// Calculates angle between two points.
                    /// </summary>
                    /// <param name="point1">The first point.</param>
                    /// <param name="point2">The second point.</param>
                    /// <returns>Angle between two points.</returns>
                    inline double angleBetweenTwoPoints(const Point2dInt & point1, const Point2dInt & point2)
                    {
                        double deltaY = point2.Y - point1.Y;
                        double deltaX = point2.X - point1.X;

                        double angle = atan2(deltaY, deltaX) * 180.0 / Constants::PI;

                        return angle;
                    }

                    /// <summary>
                    /// Calculates the distance between two points.
                    /// </summary>
                    /// <param name="point1">The first point.</param>
                    /// <param name="point2">The second point.</param>
                    /// <returns>Distance between two points.</returns>
                    inline double distanceBetweenTwoPoints(const Point2dInt & point1, const Point2dInt & point2)
                    {
                        return sqrt(pow(point1.X - point2.X, 2.0) + pow(point1.Y - point2.Y, 2.0));
                    }
                }
            }
        }
    }
}

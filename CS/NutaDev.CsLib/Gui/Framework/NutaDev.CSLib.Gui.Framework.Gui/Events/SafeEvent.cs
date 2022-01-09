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

using System;
using System.Collections.Generic;

namespace NutaDev.CSLib.Gui.Framework.Gui.Events
{
    /// <summary>
    /// Safe event implementation that allows clearing of all handlers whether one knows about them or not.
    /// We have to handle 2 lists unfortunately. Our inner list of handlers and inner handler list of inner event (it's safer than reflection, tho).
    /// Also C# doesn't allow function call operator override, so best we can do is to call one of the following:
    /// * Indexer operator, like obj[sender, args]();
    /// * Call Invoke(sender, args) method
    /// * Cast to Action[object, EventArgs] type and invoke
    /// * Call GetInvoker and invoke returned object
    /// </summary>
    public class SafeEvent
    {
        /// <summary>
        /// Inner handler list.
        /// </summary>
        private readonly List<EventHandler> _handlers;

        /// <summary>
        /// Initializes a instance of the <see cref="SafeEvent"/> class.
        /// </summary>
        public SafeEvent()
        {
            _handlers = new List<EventHandler>();
        }

        /// <summary>
        /// Public EventHandler that registers to InnerEvent and puts handlers to inner list.
        /// </summary>
        public event EventHandler Event
        {
            add
            {
                InnerEvent += value;
                _handlers.Add(value);
            }
            remove
            {
                InnerEvent -= value;
                _handlers.Remove(value);
            }
        }

        /// <summary>
        /// Inner event.
        /// </summary>
        private event EventHandler InnerEvent;

        /// <summary>
        /// Return invoker object. Use obj[sender, e]() to call.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public Action this[object sender, EventArgs e]
        {
            get
            {
                return () => Invoke(sender, e);
            }
        }

        /// <summary>
        /// Adds handler to provided SafeEvent obj.
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static SafeEvent operator +(SafeEvent ev, EventHandler handler)
        {
            ev.Event += handler;
            return ev;
        }

        /// <summary>
        /// Removes handler from provided SafeEvent obj.
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static SafeEvent operator -(SafeEvent ev, EventHandler handler)
        {
            ev.Event -= handler;
            return ev;
        }

        /// <summary>
        /// Casts SaveEvent to Action[object, EventArgs].
        /// </summary>
        /// <param name="ev"></param>
        public static implicit operator Action<object, EventArgs>(SafeEvent ev)
        {
            return ev.Invoke;
        }

        /// <summary>
        /// Clears all event handlers.
        /// </summary>
        public void Clear()
        {
            while (_handlers.Count > 0)
            {
                Event -= _handlers[0];
            }
        }

        /// <summary>
        /// Invokes event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Invoke(object sender, EventArgs e)
        {
            if (InnerEvent != null)
            {
                InnerEvent(sender, e);
            }
        }

        /// <summary>
        /// Returns Action[object, EventArgs] object which allows event invoking.
        /// </summary>
        /// <returns></returns>
        public Action<object, EventArgs> GetInvoker()
        {
            return Invoke;
        }
    }
}

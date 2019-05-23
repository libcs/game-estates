/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

using System;
using System.Collections.Generic;

namespace Niflib
{
    /*! Stores an animation key and the time in the animation that it takes affect. It is a template class so it can hold any kind of data as different objects key different sorts of information to the animation timeline.*/
    public class Key<T>
    {
        public float time; /*!< The time on the animation timeline that this keyframe takes affect. */
        public T data; /*!< The data being keyed to the timeline. */
        public T forward_tangent; /*!< A piece of data of the same type as is being keyed to the time line used as the forward tangent in quadratic interpolation.  Ignored if key type is set as something else. */
        public T backward_tangent; /*!< A piece of data of the same type as is being keyed to the time line used as the backward tangent in quadratic interpolation.  Ignored if key type is set as something else. */
        public float tension; /*!< The amount of tension to use in tension, bias, continuity interpolation.  Ignored if key type is something else.*/
        public float bias; /*!< The amount of bias to use in tension, bias, continuity interpolation.  Ignored if key type is something else.*/
        public float continuity; /*!< The amount of continuity to use in tension, bias, continuity interpolation.  Ignored if key type is something else.*/

        public string BangBang => $@"Time:  {time}
Data:  {data}
Forward Tangent:  {forward_tangent}
Backward Tangent:  {backward_tangent}
Bias:  {bias}
Continuity:  {continuity}\n";
    }

    public static class Key
    {
        /*!
         * A function to normalize the key times in a vector of keys to be in seconds,
         * effectivly setting phase to zero and frequency to 1.
         * \param[in/out] keys The vector of keys to be normalized.
         * \param[in] phase The phase shift to remove during normalization.
         * \param[in] frequency The original frequency of the keys which will be
         * normalized to 1.
         */
        public static void NormalizeKeyVector<T>(IList<Key<T>> keys, float phase, float frequency)
        {
            for (var i = 0; i < keys.Count; ++i)
                keys[i].time = (keys[i].time - phase) / frequency;
        }

        /*!
         * A function to extract key values for a certain amount of time.  Values will be
         * duplicated if necessary when cycle_type is CYCLE_LOOP or CYCLE_REVERSE.
         */
        public static IList<Key<T>> ExtractKeySlice<T>(IList<Key<T>> keys, float slice_start, float slice_stop, float keys_start, float keys_stop, CycleType cycle = CycleType.CYCLE_CLAMP)
        {
            var o = new List<Key<T>>();
            //Get first set of keys
            for (var i = 0; i < keys.Count; ++i)
                if (keys[i].time >= slice_start && keys[i].time <= slice_stop)
                    o.Add(keys[i]);
            //Get additional keys based on cycle type.
            if (cycle == CycleType.CYCLE_LOOP || cycle == CycleType.CYCLE_REVERSE)
            {
                var c = (float)Math.Floor(slice_start / (keys_stop - keys_start)) + 1.0f;
                var reverse = false;
                var failed = false;
                while (!failed)
                {
                    if (cycle == CycleType.CYCLE_REVERSE)
                        reverse = !reverse;
                    int first, last, vec;
                    if (reverse)
                    {
                        first = keys.Count - 1;
                        last = -1;
                        vec = -1;
                    }
                    else
                    {
                        first = 0;
                        last = keys.Count;
                        vec = 1;
                    }
                    for (var i = first; i != last; i += vec)
                    {
                        var time = keys[i].time;
                        time = keys_start + (keys_stop - time) + c * (keys_stop - keys_start);
                        if (time >= slice_start && time <= slice_stop)
                        {
                            var add_key = true;
                            var prev_key = o.Count - 1;
                            if (o.Count > 0 && o[prev_key].time == keys[i].time)
                                add_key = false;
                            if (add_key)
                                o.Add(keys[i]);
                        }
                        else
                        {
                            failed = true;
                            break;
                        }
                    }
                    c += 1.0f;
                }
            }
            return o;
        }
    }
}
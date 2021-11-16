using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaximumValue;
        private readonly float r_MinmumValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) : base(string.Format(
            @"Wrong Input!
The input you entered is not in the range of {0} - {1},
Please try again.",
            i_MinValue,
            i_MaxValue))
        {
            r_MinmumValue = i_MinValue;
            r_MaximumValue = i_MaxValue;
        }

        public float MaxValue
        {
            get { return r_MaximumValue; }
        }

        public float MinValue
        {
            get { return r_MinmumValue; }
        }
    }
}
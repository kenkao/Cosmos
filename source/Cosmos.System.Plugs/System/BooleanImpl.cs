﻿using System;

namespace Cosmos.System.Plugs.System
{
    //[Plug(Target = typeof(bool))]
    public static class BooleanImpl
    {
        //NOTE; Not used any more??
        //public static string ToString(ref Boolean aThis)
        //{
        //    if (aThis)
        //    {
        //        return "true";
        //    }
        //    else
        //    {
        //        return "false";
        //    }
        //}

        public static bool Parse(string aBoolText)
        {
            if (aBoolText == null)
            {
                throw new ArgumentNullException("aBoolText");
            }

            Boolean xResult = false;
            if (!Boolean.TryParse(aBoolText, out xResult))
            {
                throw new FormatException("String was not recognized as a valid Boolean.");
            }
            return xResult;
        }

        public static bool TryParse(string aBoolText, out Boolean aResult)
        {
            aResult = false;

            //Currently .Equals(string, StringComparison) does not work, so only exactly "True" and "False" work. Not "true", "tRuE" etc.
            if ("True".Equals(aBoolText, StringComparison.OrdinalIgnoreCase))
            {
                aResult = true;
                return true;
            }
            else if ("False".Equals(aBoolText, StringComparison.OrdinalIgnoreCase))
            {
                aResult = false;
                return true;
            }

            return false;
        }

        public static int GetHashCode(ref bool aThis)
        {
            if (aThis == true)
                return 1;

            return 0;
        }
    }
}
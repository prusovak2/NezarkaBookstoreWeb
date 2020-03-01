using System;

namespace JumpingPlatformGame 
{
    public static class ExtensionsInt
    {
        public static Meters Meters(this int what)
        {
            return new Meters(what);
        }
        public static Seconds Seconds(this int what)
        {
            return new Seconds(what);
        }
        public static MeterPerSeconds MeterPerSeconds(this int what)
        {
            return new MeterPerSeconds(what);
        }
    }

    public static class ExtensionDouble
    {
        public static MeterPerSeconds MeterPerSeconds(this double what)
        {
            return new MeterPerSeconds(what);
        }
        public static Seconds Seconds(this double what)
        {
            return new Seconds(what);
        }
        
    }
    public struct Years
    {
        public int Value;
        public Years(int i)
        {
            this.Value = i;
        }
        public static bool operator <(TimeSpan time, Years years)
        {
            if ((time.Days % 365) < years.Value)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(TimeSpan time, Years years)
        {
            if (((time.Days % 365) > years.Value) || (((time.Days % 365) == years.Value)
                    && (time.Hours > 0 || time.Minutes > 0 || time.Seconds > 0 || time.Milliseconds > 0)))
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(TimeSpan time, Years years)
        {
            if (((time.Days % 365) < years.Value)|| (((time.Days % 365) == years.Value)
                && (time.Hours == 0 && time.Minutes == 0 && time.Seconds == 0 && time.Milliseconds == 0)))
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(TimeSpan time, Years years)
        {
            if ((time.Days % 365) >= years.Value) 
            {
                return true;
            }
            return false;
        }
    }

    public struct Meters
    {
        public double Value;
        public Meters(double i)
        {
            this.Value = i;
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
        public static MeterPerSeconds operator /(Meters meters, Seconds seconds)
        {
            return new MeterPerSeconds(meters.Value / seconds.Value);
        }
        public static bool operator <=(Meters m1, Meters m2)
        {
            if (m1.Value <= m2.Value)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Meters m1, Meters m2)
        {
            if (m1.Value > m2.Value)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Meters m1, Meters m2)
        {
            if (m1.Value < m2.Value)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Meters m1, Meters m2)
        {
            if (m1.Value >= m2.Value)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Meters m1, Meters m2)
        {
            if (m1.Value == m2.Value)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Meters m1, Meters m2)
        {
            if (m1.Value != m2.Value)
            {
                return true;
            }
            return false;
        }
        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            if(o is Meters m)
            {
                if (this.Value == m.Value)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public static Meters operator *(Meters m, int i)
        {
            return new Meters(m.Value * i);
        }
        public static Meters operator +(Meters m1, Meters m2)
        {
            return new Meters(m1.Value + m2.Value);
        }


    }
    public struct Seconds
    {
        public double Value;
        public Seconds(double i)
        {
            this.Value = i;
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
    public struct MeterPerSeconds //MetersPerSecond?
    {
        public double Value;
        public MeterPerSeconds(double i)
        {
            this.Value = i;
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
        public static Meters operator *(Seconds seconds, MeterPerSeconds mps)
        {
            return new Meters(seconds.Value * mps.Value);
        }
        public static MeterPerSeconds operator *(MeterPerSeconds mps, int i)
        {
            return new MeterPerSeconds(mps.Value * i);
        }
    }
}
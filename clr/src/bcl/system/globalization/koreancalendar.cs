// ==++==
//
//   
//    Copyright (c) 2006 Microsoft Corporation.  All rights reserved.
//   
//    The use and distribution terms for this software are contained in the file
//    named license.txt, which can be found in the root of this distribution.
//    By using this software in any fashion, you are agreeing to be bound by the
//    terms of this license.
//   
//    You must not remove this notice, or any other, from this software.
//   
//
// ==--==
namespace System.Globalization {

    using System;

    /*=================================KoreanCalendar==========================
    **
    ** Korean calendar is based on the Gregorian calendar.  And the year is an offset to Gregorian calendar.
    ** That is,
    **      Korean year = Gregorian year + 2333.  So 2000/01/01 A.D. is Korean 4333/01/01
    **
    ** 0001/1/1 A.D. is Korean year 2334.
    **
    **  Calendar support range:
    **      Calendar    Minimum     Maximum
    **      ==========  ==========  ==========
    **      Gregorian   0001/01/01   9999/12/31
    **      Korean      2334/01/01  12332/12/31
    ============================================================================*/


[System.Runtime.InteropServices.ComVisible(true)]
    [Serializable] public class KoreanCalendar: Calendar {
        //
        // The era value for the current era.
        //

        public const int KoreanEra = 1;

        // Since
        //    Gregorian Year = Era Year + yearOffset
        // Gregorian Year 1 is Korean year 2334, so that
        //    1 = 2334 + yearOffset
        //  So yearOffset = -2333
        // Gregorian year 2001 is Korean year 4334.

        //m_EraInfo[0] = new EraInfo(1, new DateTime(1, 1, 1).Ticks, -2333, 2334, GregorianCalendar.MaxYear + 2333);

        internal static EraInfo[] m_EraInfo = GregorianCalendarHelper.InitEraInfo(Calendar.CAL_KOREA);

        //internal static Calendar m_defaultInstance;

        internal GregorianCalendarHelper helper;


        [System.Runtime.InteropServices.ComVisible(false)]
        public override DateTime MinSupportedDateTime
        {
            get
            {
                return (DateTime.MinValue);
            }
        }

        [System.Runtime.InteropServices.ComVisible(false)]
        public override DateTime MaxSupportedDateTime
        {
            get
            {
                return (DateTime.MaxValue);
            }
        }

        // Return the type of the Korean calendar.
        //

        [System.Runtime.InteropServices.ComVisible(false)]
        public override CalendarAlgorithmType AlgorithmType
        {
            get
            {
                return CalendarAlgorithmType.SolarCalendar;
            }
        }

        /*=================================GetDefaultInstance==========================
        **Action: Internal method to provide a default intance of KoreanCalendar.  Used by NLS+ implementation
        **       and other calendars.
        **Returns:
        **Arguments:
        **Exceptions:
        ============================================================================*/
        /*
        internal static Calendar GetDefaultInstance() {
            if (m_defaultInstance == null) {
                m_defaultInstance = new KoreanCalendar();
            }
            return (m_defaultInstance);
        }
        */


        public KoreanCalendar() {
            helper = new GregorianCalendarHelper(this, m_EraInfo);
        }

        internal override int ID {
            get {
                return (CAL_KOREA);
            }
        }


        public override DateTime AddMonths(DateTime time, int months) {
            return (helper.AddMonths(time, months));
        }


        public override DateTime AddYears(DateTime time, int years) {
            return (helper.AddYears(time, years));
        }

        /*=================================GetDaysInMonth==========================
        **Action: Returns the number of days in the month given by the year and month arguments.
        **Returns: The number of days in the given month.
        **Arguments:
        **      year The year in Korean calendar.
        **      month The month
        **      era     The Japanese era value.
        **Exceptions
        **  ArgumentException  If month is less than 1 or greater * than 12.
        ============================================================================*/


        public override int GetDaysInMonth(int year, int month, int era) {
            return (helper.GetDaysInMonth(year, month, era));
        }


        public override int GetDaysInYear(int year, int era) {
            return (helper.GetDaysInYear(year, era));
        }


        public override int GetDayOfMonth(DateTime time) {
            return (helper.GetDayOfMonth(time));
        }


        public override DayOfWeek GetDayOfWeek(DateTime time)  {
            return (helper.GetDayOfWeek(time));
        }


        public override int GetDayOfYear(DateTime time)
        {
            return (helper.GetDayOfYear(time));
        }


        public override int GetMonthsInYear(int year, int era) {
            return (helper.GetMonthsInYear(year, era));
        }


        [System.Runtime.InteropServices.ComVisible(false)]
        public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
        {
            return (helper.GetWeekOfYear(time, rule, firstDayOfWeek));
        }


        public override int GetEra(DateTime time) {
            return (helper.GetEra(time));
        }

        public override int GetMonth(DateTime time) {
            return (helper.GetMonth(time));
        }


        public override int GetYear(DateTime time) {
            return (helper.GetYear(time));
        }


        public override bool IsLeapDay(int year, int month, int day, int era)
        {
            return (helper.IsLeapDay(year, month, day, era));
        }


        public override bool IsLeapYear(int year, int era) {
            return (helper.IsLeapYear(year, era));
        }

        // Returns  the leap month in a calendar year of the specified era. This method returns 0
        // if this calendar does not have leap month, or this year is not a leap year.
        //

        [System.Runtime.InteropServices.ComVisible(false)]
        public override int GetLeapMonth(int year, int era)
        {
            return (helper.GetLeapMonth(year, era));
        }


        public override bool IsLeapMonth(int year, int month, int era) {
            return (helper.IsLeapMonth(year, month, era));
        }


        public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era) {
            return (helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era));
        }


        public override int[] Eras {
            get {
                return (helper.Eras);
            }
        }

        private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 4362;


        public override int TwoDigitYearMax {
            get {
                if (twoDigitYearMax == -1) {
                    twoDigitYearMax = GetSystemTwoDigitYearSetting(ID, DEFAULT_TWO_DIGIT_YEAR_MAX);
                }
                return (twoDigitYearMax);
            }

            set {
                VerifyWritable();
                if (value < 99 || value > helper.MaxYear) {
                    throw new ArgumentOutOfRangeException(
                                "year",
                                String.Format(
                                    CultureInfo.CurrentCulture,
                                    Environment.GetResourceString("ArgumentOutOfRange_Range"),
                                    99,
                                    helper.MaxYear));

                }
                twoDigitYearMax = value;
            }
        }


        public override int ToFourDigitYear(int year) {
            return (helper.ToFourDigitYear(year, this.TwoDigitYearMax));
        }
    }
}

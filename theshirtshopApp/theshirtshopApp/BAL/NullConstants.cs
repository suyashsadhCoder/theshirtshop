using System;
using System.Collections.Generic;


/// <summary>
/// Summary description for NullConstants
/// </summary>
public class NullConstants
{
    public static DateTime NullDateTime = DateTime.MinValue;

    public static decimal NullDecimal = 0;

    public static double NullDouble = 0;

    public static Guid NullGuid = Guid.Empty;

    public static int NullInt = 0;

    public static long NullLong = 0;

    public static float NullFloat = 0;

    public static string NullString = string.Empty;

    public static DateTime SqlMaxDate = new DateTime(9999, 1, 3, 23, 59, 59);

    public static DateTime SqlMinDate = new DateTime(1753, 1, 1, 00, 00, 00);

    public static byte[] NullByte = null;

    public static Boolean NullBoolean = false;
    public static int [] NullIntArray = null;

}
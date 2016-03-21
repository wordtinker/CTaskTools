using System;

namespace TaskTools.Shared
{
    public enum Category
    {
        Money,
        Health,
        Business,
        Fun,
        FriendsFamily,
        SelfDevelopment,
        Environment
    }

    public enum Stage
    {
        Incoming,
        Someday,
        Waiting,
        Backlog,
        Today
    }

    public enum RepeatType
    {
        LastDay = -1,
        Sunday = DayOfWeek.Sunday, // 0
        Monday = DayOfWeek.Monday, // 1
        Tuesday = DayOfWeek.Tuesday, // 2
        Wednesday = DayOfWeek.Wednesday, // 3
        Thursday = DayOfWeek.Thursday, // 4
        Friday = DayOfWeek.Friday, //5
        Saturday = DayOfWeek.Saturday, // 6
        Day = 7,
        _1 = 101,
        _2 = 102,
        _3 = 103,
        _4 = 104,
        _5 = 105,
        _6 = 106,
        _7 = 107,
        _8 = 108,
        _9 = 109,
        _10 = 110,
        _11 = 111,
        _12 = 112,
        _13 = 113,
        _14 = 114,
        _15 = 115,
        _16 = 116,
        _17 = 117,
        _18 = 118,
        _19 = 119,
        _20 = 120,
        _21 = 121,
        _22 = 122,
        _23 = 123,
        _24 = 124,
        _25 = 125,
        _26 = 126,
        _27 = 127,
        _28 = 128,
        _29 = 129,
        _30 = 130,
        _31 = 131
    }
}

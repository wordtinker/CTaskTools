using System;
using System.ComponentModel;
using TaskTools.Properties;

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

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
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
        [LocalizedDescription("_1", typeof(Resources))]
        _1 = 101,
        [LocalizedDescription("_2", typeof(Resources))]
        _2 = 102,
        [LocalizedDescription("_3", typeof(Resources))]
        _3 = 103,
        [LocalizedDescription("_4", typeof(Resources))]
        _4 = 104,
        [LocalizedDescription("_5", typeof(Resources))]
        _5 = 105,
        [LocalizedDescription("_6", typeof(Resources))]
        _6 = 106,
        [LocalizedDescription("_7", typeof(Resources))]
        _7 = 107,
        [LocalizedDescription("_8", typeof(Resources))]
        _8 = 108,
        [LocalizedDescription("_9", typeof(Resources))]
        _9 = 109,
        [LocalizedDescription("_10", typeof(Resources))]
        _10 = 110,
        [LocalizedDescription("_11", typeof(Resources))]
        _11 = 111,
        [LocalizedDescription("_12", typeof(Resources))]
        _12 = 112,
        [LocalizedDescription("_13", typeof(Resources))]
        _13 = 113,
        [LocalizedDescription("_14", typeof(Resources))]
        _14 = 114,
        [LocalizedDescription("_15", typeof(Resources))]
        _15 = 115,
        [LocalizedDescription("_16", typeof(Resources))]
        _16 = 116,
        [LocalizedDescription("_17", typeof(Resources))]
        _17 = 117,
        [LocalizedDescription("_18", typeof(Resources))]
        _18 = 118,
        [LocalizedDescription("_19", typeof(Resources))]
        _19 = 119,
        [LocalizedDescription("_20", typeof(Resources))]
        _20 = 120,
        [LocalizedDescription("_21", typeof(Resources))]
        _21 = 121,
        [LocalizedDescription("_22", typeof(Resources))]
        _22 = 122,
        [LocalizedDescription("_23", typeof(Resources))]
        _23 = 123,
        [LocalizedDescription("_24", typeof(Resources))]
        _24 = 124,
        [LocalizedDescription("_25", typeof(Resources))]
        _25 = 125,
        [LocalizedDescription("_26", typeof(Resources))]
        _26 = 126,
        [LocalizedDescription("_27", typeof(Resources))]
        _27 = 127,
        [LocalizedDescription("_28", typeof(Resources))]
        _28 = 128,
        [LocalizedDescription("_29", typeof(Resources))]
        _29 = 129,
        [LocalizedDescription("_30", typeof(Resources))]
        _30 = 130,
        [LocalizedDescription("_31", typeof(Resources))]
        _31 = 131
    }
}

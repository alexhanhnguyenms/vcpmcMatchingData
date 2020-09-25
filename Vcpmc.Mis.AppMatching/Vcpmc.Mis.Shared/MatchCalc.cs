using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Shared
{
    public class MatchCalc
    {
        public bool IsMatchingByTitle { get; set; } = false;
        public bool IsMatchingByWriter { get; set; }  = false;
        public bool IsMatchingByWorkCode { get; set; } = false;
        public bool isMatchingByArtist { get; set; } = false;
        public MatchingTitleType MatchingTitle { get; set; } = MatchingTitleType.Match;
        public MatchingGetCountType MatchingGetCount { get; set; } = MatchingGetCountType.First;
        public MatchingRateWriterType MatchingRateWriter { get; set; } = MatchingRateWriterType._50;
        public MatchingRateAristType MatchingRateArist { get; set; } = MatchingRateAristType.Not;
    }

    public enum MatchingTitleType
    {
        Match,
        Constrains
    }
    public enum MatchingGetCountType
    {
        Unique,
        First
    }
    public enum MatchingRateWriterType
    {
        _100,
        _75,
        _50,
        _25,
        _0
    }
    public enum MatchingRateAristType
    {
        All,
        Exist,
        Not
    }
}

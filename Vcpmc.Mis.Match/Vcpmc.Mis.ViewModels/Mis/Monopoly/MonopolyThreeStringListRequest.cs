using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Monopoly
{
    public class MonopolyThreeStringListRequest
    {
        public MonopolyByStringListRequest Item1 { get; set; } = new MonopolyByStringListRequest();
        public MonopolyByStringListRequest Item2 { get; set; } = new MonopolyByStringListRequest();
        public MonopolyByStringListRequest Item3 { get; set; } = new MonopolyByStringListRequest();
    }
}

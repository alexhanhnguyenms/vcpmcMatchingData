using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Common.detect
{
    public enum LeterErrorType
    {
        NotCharactorVN,
        NotFindVovel,
        VovelNotCorrectformat,
        ConsonantFirstNotCorrectformat,
        ConsonantSecondNotCorrectformat,
        Normal
    }
}

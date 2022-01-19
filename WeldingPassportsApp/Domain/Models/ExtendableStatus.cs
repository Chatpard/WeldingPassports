using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public enum ExtendableStatus
    {
        NotYetExtendable,
        Extendable,
        NoMoreExtendable,
        Revoked,
        Null
    }
}

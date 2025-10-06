using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElementaPrime.Core.Interfaces.Functions;

namespace ElementaPrime.Core.Functions
{
    public class ReturnValue: IValue
    {
        public object Value { get; set; }
        public string Name { get; }
        public Guid Id { get; } = Guid.Empty;
    }
}

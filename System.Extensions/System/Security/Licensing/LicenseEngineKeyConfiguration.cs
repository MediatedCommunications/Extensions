using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Security.Licensing;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Licensing {
    public record LicenseEngineKeyConfiguration<TKey> : DisplayRecord
        where TKey : LicenseEngineKey {

        public ImmutableArray<TKey> ProductionKeys { get; init; } = ImmutableArray<TKey>.Empty;
        public ImmutableArray<TKey> TrialKeys { get; init; } = ImmutableArray<TKey>.Empty;

    }
}

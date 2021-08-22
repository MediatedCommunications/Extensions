using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace System.Net.Http.Message.Senders
{
    public abstract record RetryDelegatingMessageSender : DelegatingMessageSender<IMessageModifier, HttpResponseMessage> {

        public RetryDelegatingMessageSender(IEnumerable<TimeSpan>? Attempts, IMessageSender<IMessageModifier, HttpResponseMessage>? Child = default) : base(Child) {
            if (Attempts is { } V1) {
                Delays = V1.ToImmutableList();
            }

        }

        public ImmutableList<TimeSpan> Delays { get; init; } = new[] {
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(15),
            TimeSpan.FromSeconds(60),
            TimeSpan.FromSeconds(60 * 5),
        }.ToImmutableList();

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Delays.Select(x => (object?)x).ToArray())
                ;
        }

    }

}

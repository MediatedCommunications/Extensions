using System.Diagnostics;
using System.Linq;

namespace System {
    public record EmailAddress : DisplayRecord {

        /// <summary>
        /// MAILBOX+ALIAS@DOMAIN
        /// </summary>
        public string Mailbox { get; init; } = string.Empty;
        public string? Alias { get; init; } = default;
        public string Domain { get; init; } = string.Empty;

        public string Address {
            get {
                var ret = Mailbox;
                if (Alias is { }) {
                    ret += $@"+{Alias}";
                }
                ret += $@"@{Domain}";

                return ret;
            }
        }

        public EmailAddress WithoutAlias() {
            return new EmailAddress() {
                Mailbox = Mailbox,
                Domain = Domain,
            };
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Address)
                ;
        }

    }

}

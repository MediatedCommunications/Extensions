using System.Diagnostics;

namespace System {
    public record EmailAddress : DisplayRecord {

        /// <summary>
        /// MAILBOX+ALIAS@DOMAIN
        /// </summary>
        public string Mailbox { get; init; } = Strings.Empty;
        public string? Alias { get; init; } = default;
        public string Domain { get; init; } = Strings.Empty;

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

        public override string ToString() {
            return Address;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Address)
                ;
        }

    }

    public static class EmailAddresses {
        public static EmailAddress Unknown { get; }
        public static string UnknownAddress { get; }

        static EmailAddresses() {
            Unknown = new EmailAddress()
            {
                Mailbox = "Unknown",
                Domain = "Example.com",
            };

            UnknownAddress = Unknown.Address;

        }
    }

}

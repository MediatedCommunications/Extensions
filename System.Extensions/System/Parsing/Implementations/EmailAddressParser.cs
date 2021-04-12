using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace System {

    public record EmailAddressParser : RegexClassParser<EmailAddress> {
        private static readonly Regex REGEX_EMAIL = new($@"\b(?<{nameof(EmailAddress.Mailbox)}>(   ([A-Z0-9]+) ([._+-][A-Z0-9]+)*)    )@(?<{nameof(EmailAddress.Domain)}>(([A-Z0-9-]+\.)+([A-Z]{{2,}})))\b", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        public EmailAddressParser(string? Value) : base(REGEX_EMAIL, ClassParser, Value) {

        }

        static bool ClassParser(Match Input, [NotNullWhen(true)] out EmailAddress? Result) {
            var ret = false;
            Result = default;

            var FullMailbox = Input.Groups[nameof(EmailAddress.Mailbox)].Value;
            var Domain = Input.Groups[nameof(EmailAddress.Domain)].Value;

            if(FullMailbox.IsNotBlank() && Domain.IsNotBlank()) {
                var Mailbox = FullMailbox;
                var Alias = default(string?);

                if (Mailbox.Contains("+")) {
                    var Parts = Mailbox.Split('+', 2);
                    Mailbox = Parts[0].Coalesce();
                    Alias = Parts[1].Coalesce();
                }

                Result = new EmailAddress() {
                    Mailbox = Mailbox,
                    Alias = Alias,
                    Domain = Domain,
                };
                ret = true;
            }

            return ret;
        }

    }


}

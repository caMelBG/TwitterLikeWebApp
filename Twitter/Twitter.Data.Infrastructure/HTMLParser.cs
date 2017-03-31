namespace Twitter.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class HTMLParser
    {
        public IEnumerable<string> ExtractUserNames(string htmlText)
        {
            var usernames = new List<string>();
            var pattern = @"(>*)(.*)(\s*)<\/a>";
            var matches = Regex.Matches(htmlText, pattern);
            foreach (Match match in matches)
            {
                var parts = match.Value.Split(new[] { '<' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    var username = parts[0].Trim();
                    if (username.Length < 20 && 4 < username.Length)
                    {
                        username = username.Replace(' ', '_');
                        usernames.Add(username);
                    }
                }
            }

            return usernames;
        }
    }
}

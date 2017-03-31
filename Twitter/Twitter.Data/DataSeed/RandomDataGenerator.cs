using System;
using System.Text;

namespace Twitter.Data.DataSeed
{
    public class RandomDataGenerator
    {
        private const int MinStringLength = 5;
        private const int MaxStringLength = 20;
        private const int MinTweetLength = 16;
        private const int MaxTweetLength = 256;
        private const int MaxWordsPerTweet = 50;
        private const int AlphabetLength = 26;
        private CustomRandom random;

        public RandomDataGenerator()
        {
            this.random = new CustomRandom();
        }

        public int GetRandomNumber(int max)
        {
            return this.random.Next(max);
        }

        public DateTime GetRandomDate()
        {
            var date = new DateTime(2005, 1, 1);
            var maxDays = 3000;
            var randDays = this.random.Next(maxDays);
            return date.AddDays(randDays);
        }

        public string GetRandomTweetContent()
        {
            var tweetContent = new StringBuilder();
            var length = this.random.Next(MaxTweetLength);
            for (int index = 0; index <= length; index++)
            {
                tweetContent.Append(this.GetRandomString(MinStringLength, MaxStringLength) + " ");
            }

            if (tweetContent.Length >= MaxTweetLength)
            {
                tweetContent = tweetContent.Remove(MaxTweetLength - 1, tweetContent.Length - MaxTweetLength + 1);
            }
            else if (tweetContent.Length < MinTweetLength)
            {
                for (int i = 0; i < 5; i++)
                {
                    tweetContent = tweetContent.Append(this.GetRandomString(4, 20));    
                }
            }
            return tweetContent.ToString();
        }

        public string GetRandomName()
        {
            var name = this.GetRandomString(MinStringLength, MaxStringLength);
            var firstLetter = name[0].ToString().ToUpper();
            name = name.Remove(0, 1);
            var final = firstLetter + name;
            return final;
        }

        private string GetRandomString(int minLegth, int maxLength)
        {
            var result = new StringBuilder();
            var length = this.random.Next(maxLength - minLegth);
            for (int index = 0; index <= length; index++)
            {
                var randNum = this.random.Next(AlphabetLength);
                result.Append((char)(randNum + 97));
            }

            return result.ToString();
        }
    }
}

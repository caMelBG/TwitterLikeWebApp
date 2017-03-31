namespace Twitter.Data.DataSeed
{
    using System;
    using System.Security.Cryptography;

    public class CustomRandom
    {
        public int Next(int max)
        {
            short value = 0;
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[max];
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                    value = BitConverter.ToInt16(data, 0);
                }
            }

            var result = value % max;
            if (result < 0)
            {
                result *= -1;
            }

            return result;
        }
    }
}

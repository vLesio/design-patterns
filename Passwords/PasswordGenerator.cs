using System.Text;

namespace design_patterns.Passwords; 

public class PasswordGenerator {
    private const string Characters = "abcdefghijklmnopqrstuvwxyz0123456789";
    private const int MaxLength = 5;

    public static string[] GenerateBatch(int batchSize, int batchNumber)
    {
        long start = (long)batchSize * (batchNumber - 1);
        long end = start + batchSize;

        string[] passwords = new string[batchSize];
        for (long i = start; i < end; i++)
        {
            passwords[i - start] = IndexToPassword(i);
        }

        return passwords;
    }

    private static string IndexToPassword(long index)
    {
        StringBuilder password = new StringBuilder();
        do
        {
            password.Insert(0, Characters[(int)(index % Characters.Length)]);
            index = (index == 0) ? -1 : index / Characters.Length - 1;
        } while (index >= 0 && password.Length < MaxLength);

        return password.ToString();
    }
}
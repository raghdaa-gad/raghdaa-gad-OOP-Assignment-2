
using System.Linq;

public class Solution
{
    public int MaxVowels(string s, int k)
    {
        int countVowel = 0;

        for (int i = 0; i < k; i++)
        {
            if ("aeiou".Contains(s[i]))
                countVowel++;
        }

        int maxCount = countVowel;

        for (int i = k; i < s.Length; i++)
        {
            if ("aeiou".Contains(s[i - k]))
                countVowel--;

            if ("aeiou".Contains(s[i]))
                countVowel++;

            maxCount = Math.Max(maxCount, countVowel);
        }

        return maxCount;
    }
}

using System;

public static class CcountExtensionMethod
{
	public static int Count(this string text, char c)
	{
		if (text == null)
		{
			throw new ArgumentNullException(nameof(text));
		}

		int count = 0;
		foreach (char ch in text)
		{
			if (ch == c)
			{
				count++;
			}
		}

		return count;
	}
}

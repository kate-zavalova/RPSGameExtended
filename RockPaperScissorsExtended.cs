using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections;

class RockPaperScissorsExtended
{
    public static string byteToHex(byte[] byteArray)
    {
        StringBuilder result = new StringBuilder();
        foreach (byte b in byteArray) { result.AppendFormat("{0:x2}", b); }
        return result.ToString();
    }

    static void Main()
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[16];
        rng.GetBytes(buff);
        var secret_key = BitConverter.ToString(buff);
        secret_key = secret_key.Replace("-", "");
        byte[] k = Encoding.ASCII.GetBytes(secret_key);
        HMACSHA256 myhmacsha256 = new HMACSHA256(k);

        Console.Write("Enter the odd number: ");
        int n = Convert.ToInt32(Console.ReadLine());
        while (n%2==0 ||n<3) { 
            Console.WriteLine("The number must be odd and grater or equal than 3 (3,..., 2n+1). "); 
            Console.Write("Enter the number: ");
        n = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine($"Name {n} objects");
        string[] strs = new string[n];
        for (int i = 0; i < n; i++)
        { strs[i] = Console.ReadLine(); }
        
        for (var j = 0; j <n; j++){
            for (var i = 0; i < n; i++){
                if (strs[i] == strs[j] & i!=j) 
                {Console.WriteLine("Совпадают!");
                Console.WriteLine($"Name {n} objects");
                for (i = 0; i < n; i++)
        {strs[i] = Console.ReadLine();}
                }
            }
        }

        int comp_move = new Random().Next(0, n);
        byte[] byteArray = Encoding.ASCII.GetBytes("strs[comp_move]");
        MemoryStream stream = new MemoryStream(byteArray);
        Console.WriteLine("Encrypted computer move: " + byteToHex(myhmacsha256.ComputeHash(stream)));

        Console.WriteLine("Make your move");
        for (int i = 0; i < n; i++)
        { Console.WriteLine($"{i + 1} — {strs[i]}"); }
        Console.Write("Your move: ");
        int person_move = Convert.ToInt32(Console.ReadLine()) - 1;
        Console.WriteLine($"You choosed {strs[person_move]}");
        Console.WriteLine($"The choice of the computer was {strs[comp_move]}");
        if (person_move == comp_move) { Console.WriteLine("It's a draw!"); }
        else
        {
            int a = (person_move + 1) % n;
            int b = (person_move + (n - 1) / 2) % n;
            if (a < b) { if (a <= comp_move & comp_move <= b) { Console.WriteLine("You won!"); } else { Console.WriteLine("You lost!"); } }
            else { if ((a <= comp_move & comp_move <= n) | (1 <= comp_move & comp_move <= b)) { Console.WriteLine("You won!"); } else { Console.WriteLine("You lost!"); } }
        }
        Console.WriteLine("Secret key: " + secret_key);
        Console.ReadKey();
    }
}
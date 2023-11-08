internal class Program
{
    public static string password;
    public static string password2;
    public static byte[] salt;
    public static byte[] hashed;
    public static byte[] Loginsalt;
    public static byte[] Loginhashed;
    public static bool correctPassword;
    private static void Main(string[] args)
    {
        string op="1";
        while (op!="3")
        {
            Console.WriteLine("Selecione una opcion");
            Console.WriteLine("1-Generar Password");
            Console.WriteLine("2-Verificar Password");
            Console.WriteLine("3-Salir");
            op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    generatePassword();
                    break;
                case "2":
                    verifyPassword();
                    string a = Console.ReadLine();
                    break;
                case "3":
                    Console.WriteLine("Cerrando App, presione cualquier tecla para continuar");
                    string c = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Opcion invalida, presione cualquier tecla para continuar");
                    string b=Console.ReadLine();
                    break;
            }
        }

    }

    private static void verifyPassword()
    {   //en un service real debemos pasar estos atributos al metodo verify
        //private static void verify(string? password, out byte[] Psalt, out byte[] Phash)

        correctPassword = true;
        Console.WriteLine("Simulando un login Service,Enter a password to verify it against the register Service password");
        Console.WriteLine("Enter a password");
        password2 = Console.ReadLine();
        using (var  hmac = new System.Security.Cryptography.HMACSHA512(salt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password2));
            for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != hashed[i])
                {
                    correctPassword= false;
                }
                
            }

        }
        if (correctPassword==true)
        {
            Console.WriteLine("El password es correcto,presiona cualquier tecla para continuar");
            string op = Console.ReadLine();
        }
        else
        {
            Console.WriteLine("El password es Incorrecto,presiona cualquier tecla para continuar");
            string op = Console.ReadLine();
        }
    }

    private static void generatePassword()
    {
        Console.WriteLine("Simulando Register Service");
        Console.Write("Generate Password,Enter a password: ");
         password = Console.ReadLine();


        generate(password, out byte[] Psalt, out byte[] Phash);
        salt = Psalt;
        hashed = Phash;
        Console.WriteLine("Password ingresado : " + password);
       // Console.WriteLine("PasswordSalt generado: " + salt);
       // Console.WriteLine("PasswordHash generado : " + Phash);

    }

    private static void generate(string? password, out byte[] Psalt, out byte[] Phash)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            Psalt = hmac.Key;
            Phash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
// See https://aka.ms/new-console-template for more information
Console.WriteLine("\nSeja Bem-vindo(a)!\n");
Console.WriteLine("\nO que deseja?\n");
Console.WriteLine("1. Cátalogo");
Console.WriteLine("2. Devolver Livros");
Console.WriteLine("3. Sair");

bool inputValido = int.TryParse(Console.ReadLine(), out int opcao);

            if (!inputValido)
            {
                Console.WriteLine("Erro: Entrada inválida, por favor, digite um número entre 1 e 3.");
                Console.ReadKey();
                continue;
            }

switch (opcao)
{
    case 1:
    Console.Writeline("Biblia sagrada")
    Console.Writeline("Harry Potter e a Pedra Filosofal")
    Console.Writeline("")
}
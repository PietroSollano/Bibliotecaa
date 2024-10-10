
     using System;
using System.Collections.Generic;

class Livro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Genero { get; set; }
    public int Quantidade { get; set; }

    public Livro(string titulo, string autor, string genero, int quantidade)
    {
        Titulo = titulo;
        Autor = autor;
        Genero = genero;
        Quantidade = quantidade;
    }
}

class Program
{
    static List<Livro> catalogo = new List<Livro>
    {
        new Livro("Bíblia Sagrada", "Vários autores", "Religioso", 10),
        new Livro("Harry Potter e a Pedra Filosofal", "J.K. Rowling", "Fantasia", 5),
        new Livro("Dom Quixote", "Miguel de Cervantes", "Clássico", 3),
        new Livro("Moby Dick", "Herman Melville", "Aventura", 2),
        new Livro("A Arte da Guerra", "Sun Tzu", "Estratégia", 4)
    };

    static List<string> livrosEmprestados = new List<string>();

    static void Main()
    {
        Console.WriteLine("\nSeja Bem-vindo(a)!\n");
        int opcao = 0;

        while (opcao != 4)
        {
            Console.WriteLine("\nO que deseja?\n");
            Console.WriteLine("1. Catálogo");
            Console.WriteLine("2. Devolver Livros");
            Console.WriteLine("3. Administrador (Cadastrar novo livro)");
            Console.WriteLine("4. Sair");

            string entrada = Console.ReadLine();
            if (!int.TryParse(entrada, out opcao) || opcao < 1 || opcao > 4)
            {
                Console.WriteLine("Erro: Entrada inválida, por favor, digite um número entre 1 e 4.");
                opcao = 0;
            }

            switch (opcao)
            {
                case 1:
                    ExibirCatalogo();
                    if (livrosEmprestados.Count >= 3)
                    {
                        Console.WriteLine("Você já atingiu o limite de 3 livros emprestados.");
                    }
                    else
                    {
                        EmprestarLivro();
                    }
                    break;

                case 2:
                    DevolverLivros();
                    break;

                case 3:
                    CadastrarLivro();
                    break;
            }
        }

        Console.WriteLine("Saindo...");
    }

    static void ExibirCatalogo()
    {
        Console.WriteLine("\nCatálogo de Livros:");
        foreach (var livro in catalogo)
        {
            Console.WriteLine($"Título: {livro.Titulo}, Autor: {livro.Autor}, Gênero: {livro.Genero}, Quantidade disponível: {livro.Quantidade}");
        }
    }

    static void EmprestarLivro()
    {
        Console.WriteLine("\nEscolha um livro para emprestar (digite o número correspondente):");
        for (int i = 0; i < catalogo.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {catalogo[i].Titulo} (Disponíveis: {catalogo[i].Quantidade})");
        }

        if (int.TryParse(Console.ReadLine(), out int escolha) && escolha > 0 && escolha <= catalogo.Count)
        {
            Livro livroEscolhido = catalogo[escolha - 1];

            if (livroEscolhido.Quantidade > 0)
            {
                Console.WriteLine($"Você escolheu: {livroEscolhido.Titulo}");
                livrosEmprestados.Add(livroEscolhido.Titulo);
                livroEscolhido.Quantidade--;
                Console.WriteLine($"Livro '{livroEscolhido.Titulo}' emprestado com sucesso!");
            }
            else
            {
                Console.WriteLine("Desculpe, este livro não está disponível no momento.");
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }

    static void DevolverLivros()
    {
        if (livrosEmprestados.Count > 0)
        {
            Console.WriteLine("\nLivros emprestados:");
            for (int i = 0; i < livrosEmprestados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {livrosEmprestados[i]}");
            }

            Console.WriteLine("Qual livro deseja devolver? (digite o número correspondente):");
            if (int.TryParse(Console.ReadLine(), out int devolucao) && devolucao > 0 && devolucao <= livrosEmprestados.Count)
            {
                string livroDevolvido = livrosEmprestados[devolucao - 1];
                livrosEmprestados.RemoveAt(devolucao - 1);

                foreach (var livro in catalogo)
                {
                    if (livro.Titulo == livroDevolvido)
                    {
                        livro.Quantidade++;
                        break;
                    }
                }

                Console.WriteLine($"{livroDevolvido} devolvido com sucesso!");
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }
        else
        {
            Console.WriteLine("Você não tem livros para devolver.");
        }
    }

    static void CadastrarLivro()
    {
        Console.WriteLine("\nCadastrar novo livro");

        Console.Write("Título: ");
        string titulo = Console.ReadLine();

        Console.Write("Autor: ");
        string autor = Console.ReadLine();

        Console.Write("Gênero: ");
        string genero = Console.ReadLine();

        Console.Write("Quantidade: ");
        if (int.TryParse(Console.ReadLine(), out int quantidade))
        {
            catalogo.Add(new Livro(titulo, autor, genero, quantidade));
            Console.WriteLine($"Livro '{titulo}' cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Quantidade inválida.");
        }
    }
}

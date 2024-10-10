
     class Livro {
    [string]$Titulo
    [string]$Autor
    [string]$Genero
    [int]$Quantidade

    Livro([string]$titulo, [string]$autor, [string]$genero, [int]$quantidade) {
        $this.Titulo = $titulo
        $this.Autor = $autor
        $this.Genero = $genero
        $this.Quantidade = $quantidade
    }
}

# Catálogo inicial de livros
$catalogo = @(
    [Livro]::new("Bíblia Sagrada", "Vários autores", "Religioso", 10),
    [Livro]::new("Harry Potter e a Pedra Filosofal", "J.K. Rowling", "Fantasia", 5),
    [Livro]::new("Dom Quixote", "Miguel de Cervantes", "Clássico", 3),
    [Livro]::new("Moby Dick", "Herman Melville", "Aventura", 2),
    [Livro]::new("A Arte da Guerra", "Sun Tzu", "Estratégia", 4)
)

$livrosEmprestados = @()

function ExibirCatalogo {
    Write-Host "`nCatálogo de Livros:"
    foreach ($livro in $catalogo) {
        Write-Host "Título: $($livro.Titulo), Autor: $($livro.Autor), Gênero: $($livro.Genero), Quantidade disponível: $($livro.Quantidade)"
    }
}

function EmprestarLivro {
    if ($livrosEmprestados.Count -ge 3) {
        Write-Host "Você já atingiu o limite de 3 livros emprestados."
        return
    }

    Write-Host "`nEscolha um livro para emprestar (digite o número correspondente):"
    for ($i = 0; $i -lt $catalogo.Count; $i++) {
        Write-Host "$($i + 1). $($catalogo[$i].Titulo) (Disponíveis: $($catalogo[$i].Quantidade))"
    }

    $escolha = Read-Host
    if ([int]::TryParse($escolha, [ref]$escolha) -and $escolha -gt 0 -and $escolha -le $catalogo.Count) {
        $livroEscolhido = $catalogo[$escolha - 1]

        if ($livroEscolhido.Quantidade -gt 0) {
            Write-Host "Você escolheu: $($livroEscolhido.Titulo)"
            $livrosEmprestados += $livroEscolhido.Titulo
            $livroEscolhido.Quantidade--
            Write-Host "Livro '$($livroEscolhido.Titulo)' emprestado com sucesso!"
        }
        else {
            Write-Host "Desculpe, este livro não está disponível no momento."
        }
    }
    else {
        Write-Host "Opção inválida."
    }
}

function DevolverLivros {
    if ($livrosEmprestados.Count -gt 0) {
        Write-Host "`nLivros emprestados:"
        for ($i = 0; $i -lt $livrosEmprestados.Count; $i++) {
            Write-Host "$($i + 1). $($livrosEmprestados[$i])"
        }

        $devolucao = Read-Host "Qual livro deseja devolver? (digite o número correspondente)"
        if ([int]::TryParse($devolucao, [ref]$devolucao) -and $devolucao -gt 0 -and $devolucao -le $livrosEmprestados.Count) {
            $livroDevolvido = $livrosEmprestados[$devolucao - 1]
            $livrosEmprestados.RemoveAt($devolucao - 1)

            foreach ($livro in $catalogo) {
                if ($livro.Titulo -eq $livroDevolvido) {
                    $livro.Quantidade++
                    break
                }
            }

            Write-Host "$livroDevolvido devolvido com sucesso!"
        }
        else {
            Write-Host "Opção inválida."
        }
    }
    else {
        Write-Host "Você não tem livros para devolver."
    }
}

function CadastrarLivro {
    Write-Host "`nCadastrar novo livro"

    $titulo = Read-Host "Título"
    $autor = Read-Host "Autor"
    $genero = Read-Host "Gênero"
    $quantidade = Read-Host "Quantidade"
    
    if ([int]::TryParse($quantidade, [ref]$quantidade)) {
        $catalogo += [Livro]::new($titulo, $autor, $genero, $quantidade)
        Write-Host "Livro '$titulo' cadastrado com sucesso!"
    }
    else {
        Write-Host "Quantidade inválida."
    }
}

# Menu principal
function Menu {
    $opcao = 0
    while ($opcao -ne 4) {
        Write-Host "`nO que deseja?"
        Write-Host "1. Catálogo"
        Write-Host "2. Devolver Livros"
        Write-Host "3. Administrador (Cadastrar novo livro)"
        Write-Host "4. Sair"
        $opcao = Read-Host

        if ([int]::TryParse($opcao, [ref]$opcao) -and $opcao -ge 1 -and $opcao -le 4) {
            switch ($opcao) {
                1 { ExibirCatalogo; EmprestarLivro }
                2 { DevolverLivros }
                3 { CadastrarLivro }
            }
        }
        else {
            Write-Host "Erro: Entrada inválida, por favor, digite um número entre 1 e 4."
        }
    }

    Write-Host "Saindo..."
}

# Execução do menu
Menu

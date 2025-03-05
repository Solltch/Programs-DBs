using VideoGemes;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {

            string connectionString = "Server=localhost;Port=3306;Database=videojogos;User=root;Password=;ConvertZeroDateTime=True;";
            CriadoresRepo criadoresRepo = new CriadoresRepo(connectionString);
            JogosRepo jogosRepo = new JogosRepo(connectionString);
            PublicadoraRepo publiRepo = new PublicadoraRepo(connectionString);
            MotorRepo motorRepo = new MotorRepo(connectionString);

            while (true)
            {
                Console.WriteLine("\nPressione Espaço para acessar o banco");

                var tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.Spacebar)
                {
                    break;
                }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("|=-=-=-=-=| Menu Principal |=-=-=-=-=|");
                Console.WriteLine("1. Jogos");
                Console.WriteLine("2. Publicadoras");
                Console.WriteLine("3. Engines");
                Console.WriteLine("4. Criadores");
                Console.WriteLine("5. Sair");

                var tecla = Console.ReadKey(true);

                switch (tecla.Key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        MenuJogos(jogosRepo);
                        break;

                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        MenuPublicadoras(publiRepo);
                        break;

                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        MenuEngines(motorRepo);
                        break;

                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        MenuCriadores(criadoresRepo);
                        break;

                    case ConsoleKey.NumPad5:
                    case ConsoleKey.D5:
                        Console.WriteLine("Saindo...");
                        return;

                    default:
                        Console.WriteLine("Opção inválida! Pressione Enter para continuar...");
                        Console.ReadLine();
                        break;
                }
            }


            // ==============================
            //           MENU JOGOS
            // ==============================
            static void MenuJogos(JogosRepo jogosrepo)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("|=-=-=-=-=| Menu de Jogos |=-=-=-=-=|");
                    Console.WriteLine("1. Listar");
                    Console.WriteLine("2. Inserir");
                    Console.WriteLine("3. Atualizar");
                    Console.WriteLine("4. Remover");
                    Console.WriteLine("5. Listar Jogos Mais Vendidos");
                    Console.WriteLine("6. Listar Jogos por Gênero");
                    Console.WriteLine("7. Voltar");
                    Console.Write("Escolha uma opção: ");

                    var opcao = Console.ReadKey(true);
                    switch (opcao.Key)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            ListarJogos(jogosrepo);
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            InserirJogo(jogosrepo);
                            break;

                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            AtualizarJogos(jogosrepo);
                            break;
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            RemoverJogos(jogosrepo);
                            break;
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            ListarVendidos(jogosrepo);
                            break;
                        case ConsoleKey.NumPad6:
                        case ConsoleKey.D6:
                            ListarPesquisa(jogosrepo);
                            break;
                        case ConsoleKey.NumPad7:
                        case ConsoleKey.D7:
                            return;
                        default:
                            Console.WriteLine("Opção inválida! Pressione Enter para continuar...");
                            Console.ReadLine();
                            break;
                    }
                }
            }

            static void ListarJogos(JogosRepo jogosRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Lista de Jogos =====");
                List<Jogos> jogos = jogosRepo.TodosJogos();
                foreach (var jogo in jogos)
                {
                    Console.WriteLine($"ID: {jogo.ID}, Nome: {jogo.nome}, Lançamento: {jogo.release_date}, Desenvolvedora: {jogo.Developer}, Publicadora {jogo.ID_publi}, MOtor Gráfico: {jogo.ID_motor}, Genero {jogo.genero}, Vendas {jogo.Vendas}");
                }
                Console.WriteLine("\nPressione Enter para voltar...");
                Console.ReadLine();
            }

            static void InserirJogo(JogosRepo jogosRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Inserir Jogo =====");
                Jogos jogos = new Jogos();

                Console.Write("Digite o nome do Jogo: ");
                jogos.nome = Console.ReadLine();
                Console.Write("Digite a data de lançamento do Jogo (YYYY-MM-DD): ");
                DateTime release_date;
                while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out release_date))
                {
                    Console.WriteLine("Formato inválido! Digite no formato correto (YYYY-MM-DD): ");
                }
                jogos.release_date = release_date.ToString("yyyy-MM-dd"); // Formata corretamente a data
                Console.Write("Digite o ID da engine do Jogo: ");
                jogos.ID_motor = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite a Desenvolvedora do Jogo: ");
                jogos.Developer = Console.ReadLine();
                Console.Write("Digite o ID da Publicadora do Jogo: ");
                jogos.ID_publi = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite o genero do Jogo: ");
                jogos.genero = Console.ReadLine();
                Console.Write("Digite o número de vendas do Jogo: ");
                jogos.Vendas = Convert.ToInt32(Console.ReadLine());

                int linhas = jogosRepo.NovaJogo(jogos);

                if (linhas > 0)
                    Console.WriteLine("Jogo inserido com sucesso! Pressione Enter para voltar...");
                else
                    Console.WriteLine("Nenhum registro foi inserido...");

                Console.ReadLine();
                ;
            }

            static void AtualizarJogos(JogosRepo jogosRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Atualizar Jogo =====");
                Console.Write("Digite o ID do Jogo: ");
                int id = Convert.ToInt32(Console.ReadLine());

                // Verifique se o Jogo existe antes de permitir a atualização
                if (!jogosRepo.JogoExiste(id))
                {
                    Console.WriteLine("Jogo não encontrado! Pressione Enter para voltar...");
                    Console.ReadLine();
                    return;
                }

                Jogos jogos = new Jogos();

                Console.Write("Digite o nome do Jogo: ");
                jogos.nome = Console.ReadLine();

                Console.Write("Digite a data de lançamento do Jogo (YYYY-MM-DD): ");
                DateTime release_date;
                while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out release_date))
                {
                    Console.WriteLine("Formato inválido! Digite no formato correto (YYYY-MM-DD): ");
                }
                jogos.release_date = release_date.ToString("yyyy-MM-dd"); // Formata corretamente a data

                Console.Write("Digite o ID da engine do Jogo: ");
                jogos.ID_motor = Convert.ToInt32(Console.ReadLine());

                Console.Write("Digite a Desenvolvedora do Jogo: ");
                jogos.Developer = Console.ReadLine();

                Console.Write("Digite o ID da Publicadora do Jogo: ");
                jogos.ID_publi = Convert.ToInt32(Console.ReadLine());

                Console.Write("Digite o genero do Jogo: ");
                jogos.genero = Console.ReadLine();

                Console.Write("Digite o número de vendas do Jogo: ");
                jogos.Vendas = Convert.ToInt32(Console.ReadLine());

                // Passa o jogo para o método de atualização no repositorio
                int linhas = jogosRepo.AtualizarJogo(jogos);

                if (linhas > 0)
                {
                    Console.WriteLine($"Jogo atualizado com sucesso! Linhas afetadas: {linhas}. Pressione Enter para voltar...");
                }
                else
                {
                    Console.WriteLine($"Não houve atualização. Verifique se os dados fornecidos estão corretos e tente novamente. {linhas}");
                }

                Console.ReadLine();
            }

            static void RemoverJogos(JogosRepo jogosRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Remover Jogo =====");
                Console.Write("Digite o ID do Jogo a ser removido: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (!jogosRepo.JogoExiste(id))
                {
                    Console.WriteLine("Jogo não encontrado! Pressione Enter para voltar...");
                    Console.ReadLine();
                    return;
                }

                int linhas = jogosRepo.RemoverJogo(id);

                if (linhas > 0)
                    Console.WriteLine("Jogo removido com sucesso! Pressione Enter para voltar...");
                else
                    Console.WriteLine("Não houve remoção...");
                Console.ReadLine();
            }

            static void ListarVendidos(JogosRepo jogosRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Lista de Jogos =====");
                List<Jogos> jogos = jogosRepo.JogosPorVenda();
                foreach (var jogo in jogos)
                {
                    Console.WriteLine($"ID: {jogo.ID}, Nome: {jogo.nome}, Lançamento: {jogo.release_date}, Desenvolvedora: {jogo.Developer}, Publicadora {jogo.ID_publi}, MOtor Gráfico: {jogo.ID_motor}, Genero {jogo.genero}, Vendas {jogo.Vendas}");
                }
                Console.WriteLine("\nPressione Enter para voltar...");
                Console.ReadLine();
            }

            static void ListarPesquisa(JogosRepo jogosRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Lista de Jogos =====");
                Console.Write("Digite o Gênero para pesquisa: ");
                string genero = Console.ReadLine();

                List<Jogos> jogos = jogosRepo.JogosPorGenero(genero);

                if (jogos.Count > 0)
                {
                    foreach (var jogo in jogos)
                    {
                        Console.WriteLine($"ID: {jogo.ID}, Nome: {jogo.nome}, Lançamento: {jogo.release_date}, " +
                            $"Desenvolvedora: {jogo.Developer}, Publicadora: {jogo.ID_publi}, " +
                            $"Motor Gráfico: {jogo.ID_motor}, Gênero: {jogo.genero}, Vendas: {jogo.Vendas}");
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum jogo encontrado para o gênero informado.");
                }

                Console.WriteLine("\nPressione Enter para voltar...");
                Console.ReadLine();
            }


            // ==============================
            //        MENU PUBLICADORAS
            // ==============================
            static void MenuPublicadoras(PublicadoraRepo publiRepo)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("===== Menu de Disciplinas =====");
                    Console.WriteLine("1. Listar");
                    Console.WriteLine("2. Inserir");
                    Console.WriteLine("3. Atualizar");
                    Console.WriteLine("4. Remover");
                    Console.WriteLine("5. Voltar");
                    Console.Write("Escolha uma opção: ");

                    var opcao = Console.ReadKey(true);
                    switch (opcao.Key)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            ListarPublis(publiRepo);
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            InserirPubli(publiRepo);
                            break;
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            AtualizarPublicadora(publiRepo);
                            break;
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            RemoverPublicadora(publiRepo);
                            break;
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            return;
                        default:
                            Console.WriteLine("Opção inválida! Pressione Enter para continuar...");
                            Console.ReadLine();
                            break;
                    }
                }
            }

            static void ListarPublis(PublicadoraRepo publisRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Lista de Publicadoras =====");

                List<Publicadora> publis = publisRepo.TodasPublicadoras();

                // Verifique se a lista de publicadoras está vazia
                if (publis.Count == 0)
                {
                    Console.WriteLine("Nenhuma publicadora encontrada.");
                }
                else
                {
                    foreach (var publi in publis)
                    {
                        // Verifique se estamos entrando no loop
                        Console.WriteLine($"ID: {publi.ID}, Nome: {publi.nome}, Fundação: {publi.fundacao}");
                    }
                }

                Console.WriteLine("\nPressione Enter para voltar...");
                Console.ReadLine();
            }

            static void InserirPubli(PublicadoraRepo publisRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Inserir Publicadora =====");
                Console.Write("Digite o nome da Publicadora: ");
                string nome = Console.ReadLine();
                Console.Write("Digite o ano de fundação da Publicadora: ");
                string fundacao = Console.ReadLine();

                int linhas = publisRepo.NovaPublicadora(new Publicadora { nome = nome, fundacao = fundacao });

                if (linhas > 0)
                    Console.WriteLine("Publicadora inserida com sucesso! Pressione Enter para voltar...");
                else
                    Console.WriteLine("Não houve inserção...");
                Console.ReadLine();
            }

            static void AtualizarPublicadora(PublicadoraRepo publisRepo)
            {
                Publicadora publi = new Publicadora();
                Console.Clear();
                Console.WriteLine("===== Atualizar Publicadora =====");
                Console.Write("Digite o ID da Publicadora: ");
                publi.ID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite o novo nome da Publicadora: ");
                publi.nome = Console.ReadLine();
                Console.Write("Digite a nova Fundacao da Publicadora: ");
                publi.fundacao = Console.ReadLine();

                int linhas = publisRepo.AtualizarPublicadora(publi);

                if (linhas > 0)
                    Console.WriteLine("Disciplina atualizada com sucesso! Pressione Enter para voltar...");
                else
                    Console.WriteLine("Não houve atualização...");

                Console.ReadLine();


            }

            static void RemoverPublicadora(PublicadoraRepo publisRepo)
    {
        Console.Clear();
        Console.WriteLine("===== Remover Disciplina =====");
        Console.Write("Digite o ID da disciplina a ser removida: ");
        int id = Convert.ToInt32(Console.ReadLine());

        int linhas = publisRepo.RemoverPublicadora(id);

        if (linhas > 0)
            Console.WriteLine("Disciplina removida com sucesso! Pressione Enter para voltar...");
        else
            Console.WriteLine("Não houve remoção...");

        Console.ReadLine();
    }


            // ==============================
            //        MENU CRIADORES
            // ==============================
            static void MenuCriadores(CriadoresRepo criadoresRepo)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("|=-=-=-=-=| Menu de Criadores |=-=-=-=-=|");
                    Console.WriteLine("1. Listar");
                    Console.WriteLine("2. Inserir");
                    Console.WriteLine("3. Atualizar");
                    Console.WriteLine("4. Listar Criadores Por Jogo");
                    Console.WriteLine("5. Voltar");
                    Console.Write("Escolha uma opção: ");

                    var opcao = Console.ReadKey(true);
                    switch (opcao.Key)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            ListarCriadores(criadoresRepo);
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            InserirCriador(criadoresRepo);
                            break;
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            AtualizarCriador(criadoresRepo);
                            break;
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            CriadorPorJogo(criadoresRepo);
                            break;
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            return;
                        default:
                            Console.WriteLine("Opção inválida! Pressione Enter para continuar...");
                            Console.ReadLine();
                            break;
                    }
                }
            }

            static void ListarCriadores(CriadoresRepo criadorRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Lista de Criadores =====");

                List<Criadores> criadores = criadorRepo.TodosCriadores();

                // Verifique se a lista de criadores está vazia
                if (criadores.Count == 0)
                {
                    Console.WriteLine("Nenhum criador encontrado.");
                }
                else
                {
                    foreach (var criador in criadores)
                    {
                        // Calcular a idade com base na data de nascimento
                        var idade = DateTime.Now.Year - criador.nascimento.Year;
                        if (DateTime.Now.DayOfYear < criador.nascimento.DayOfYear)
                            idade--; // Ajusta se ainda não fez aniversário esse ano

                        Console.WriteLine($"ID: {criador.ID}, Nome: {criador.nome}, Nascimento: {criador.nascimento.ToString("yyyy-MM-dd")}, Idade: {idade}");
                    }
                }

                Console.WriteLine("\nPressione Enter para voltar...");
                Console.ReadLine();
            }

            static void InserirCriador(CriadoresRepo criadorRepo)
            {
                Criadores criador = new Criadores();
                Console.Clear();
                Console.WriteLine("===== Inserir Criador =====");
                Console.Write("Digite o nome do Criador: ");
                criador.nome = Console.ReadLine();
                Console.Write("Digite a data de nascimento do Criador: ");
                criador.nascimento = Convert.ToDateTime(Console.ReadLine());
                criador.idade = DateTime.Now.Year - criador.nascimento.Year;
                if (DateTime.Now.DayOfYear < criador.nascimento.DayOfYear)
                    criador.idade--;

                int linhas = criadorRepo.NovoCriador(criador);

                if (linhas > 0)
                    Console.WriteLine("Criador inserido com sucesso! Pressione Enter para voltar...");
                else
                    Console.WriteLine("Não houve inserção...");
                Console.ReadLine();
            }

            static void AtualizarCriador(CriadoresRepo criadorRepo)
            {
                Criadores criador = new Criadores();
                Console.Clear();
                Console.WriteLine("===== Atualizar Criador =====");
                Console.Write("Digite o ID do Criador: ");
                criador.ID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite o novo nome do criador: ");
                criador.nome = Console.ReadLine();
                Console.Write("Digite a nova Fundacao da Publicadora: ");
                criador.nascimento = Convert.ToDateTime(Console.ReadLine());
                criador.idade = DateTime.Now.Year - criador.nascimento.Year;
                if (DateTime.Now.DayOfYear < criador.nascimento.DayOfYear)
                    criador.idade--;

                int linhas = criadorRepo.AtualizarCriador(criador);

                if (linhas > 0)
                    Console.WriteLine("Disciplina atualizada com sucesso! Pressione Enter para voltar...");
                else
                    Console.WriteLine("Não houve atualização...");

                Console.ReadLine();


            }

            static void CriadorPorJogo(CriadoresRepo criadorRepo)
            {
                var criadoresJogo = criadorRepo.CriadoresPorJogo();

                Console.Clear();
                Console.WriteLine("===== Criadores por Jogo =====");

                if (criadoresJogo.Count == 0)
                {
                    Console.WriteLine("Nenhum criador encontrado para jogos.");
                }
                else
                {
                    foreach (var (criador, jogo) in criadoresJogo)
                    {
                        Console.WriteLine($"Criador: {criador}, Jogo: {jogo}");
                    }
                }

                Console.WriteLine("\nPressione Enter para voltar...");
                Console.ReadLine();
            }
                // ==============================
                //        MENU ENGINES
                // ==============================
                static void MenuEngines(MotorRepo motorRepo)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("|=-=-=-=-=| Menu de Criadores |=-=-=-=-=|");
                    Console.WriteLine("1. Listar");
                    Console.WriteLine("2. Inserir");
                    Console.WriteLine("3. Atualizar");
                    Console.WriteLine("4. Voltar");
                    Console.Write("Escolha uma opção: ");

                    var opcao = Console.ReadKey(true);
                    switch (opcao.Key)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            ListarEngines(motorRepo);
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            InserirEngine(motorRepo);
                            break;
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            AtualizarEngine(motorRepo);
                            break;
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            return;
                        default:
                            Console.WriteLine("Opção inválida! Pressione Enter para continuar...");
                            Console.ReadLine();
                            break;
                    }
                }
            }

            static void ListarEngines(MotorRepo motoresRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Lista de Motores Gráficos =====");

                List<Motor> motores = motoresRepo.TodasEngine();

                // Verifique se a lista de criadores está vazia
                if (motores.Count == 0)
                {
                    Console.WriteLine("Nenhum Motor Gráfico encontrado.");
                }
                else
                {
                    foreach (var motor in motores)
                    {
                        Console.WriteLine($"ID: {motor.ID}, Nome: {motor.nome}");
                    }
                }

                Console.WriteLine("\nPressione Enter para voltar...");
                Console.ReadLine();
            }

            static void InserirEngine(MotorRepo motorRepo)
            {
                Console.Clear();
                Console.WriteLine("===== Inserir Motor Gráfico =====");
                Console.Write("Digite o nome do Motor Gráfico: ");
                Motor motor = new Motor();
                motor.nome = Console.ReadLine();

                int linhas = motorRepo.NovaEngine(motor);

                if (linhas > 0)
                    Console.WriteLine("Motor Gráfico inserido com sucesso! Pressione Enter para voltar...");
                else
                    Console.WriteLine("Não houve inserção...");

                Console.ReadLine();
            }

            static void AtualizarEngine(MotorRepo motorRepo)
            {
                Motor motor = new Motor();
                Console.Clear();
                Console.WriteLine("===== Atualizar Motor Gráfico =====");
                Console.Write("Digite o ID do Motor Gráfico: ");
                motor.ID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite o novo nome do criador: ");
                motor.nome = Console.ReadLine();
                
                int linhas = motorRepo.AtualizarEngine(motor);

                if (linhas > 0)
                    Console.WriteLine("Disciplina atualizada com sucesso! Pressione Enter para voltar...");
                else
                    Console.WriteLine("Não houve atualização...");

                Console.ReadLine();


            }


        }

        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}


using Mao_Na_Massa_blog.Repositories;
using Mao_Na_Massa_blog.Screens.TagScreens;

namespace Mao_Na_Massa_blog.Screens.RoleScreens
{
    public static class DeleteRoleScreen
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Apagar Perfil");
            Console.WriteLine("-------------");

            Console.Write("Id: ");
            var id = short.Parse(Console.ReadLine()!);

            ExcluirPerfil(id);

            Console.WriteLine();
            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if (opcao == 1)
                MenuRoleScreens.CarregartelaPincipalRole();
            else
            {
                Console.WriteLine("Aperte enter 2x para fechar");
                if ("Enter" == Convert.ToString(ConsoleKey.Enter))
                    Console.ReadKey();
                Console.Clear();
            }
        }

        public static void ExcluirPerfil(short id)
        {
            try
            {
                RoleRepository repositorio = new(Database.Conexao);
                bool resultado = repositorio.Deletar(id);

                if (resultado == false)
                    return;

                Console.WriteLine("Perfil foi excluido com sucesso!");
            }
            catch (Exception ex)
            {

                 Console.WriteLine($"Ocorreu um erro ao excluir perfil, Mensagem: {ex.Message}");
            }
        }
    }
}
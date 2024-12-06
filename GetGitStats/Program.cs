using System;
using System.Linq;
using LibGit2Sharp;

namespace GetGitStats
{
  internal static class Program
  {
    static void Main()
    {
      const string repoPath = @"C:\repos\repoName"; // Chemin vers votre dépôt Git local
      if (!Repository.IsValid(repoPath))
      {
        Console.WriteLine("Le chemin spécifié n'est pas un dépôt Git valide.");
        return;
      }

      using (var repo = new Repository(repoPath))
      {
        // Récupérer tous les commits et grouper par auteur
        var authorCommitCounts = repo.Commits
            .Select(commit => commit.Author.Name)
            .GroupBy(author => author)
            .Select(group => new
            {
              Author = group.Key,
              CommitCount = group.Count()
            })
            .OrderByDescending(entry => entry.CommitCount);

        // Afficher les résultats
        Console.WriteLine("Auteur\t\tNombre de commits");
        foreach (var entry in authorCommitCounts)
        {
          Console.WriteLine($"{entry.Author}\t\t{entry.CommitCount}");
        }
      }

      Console.WriteLine("Press any key to exit:");
      Console.ReadKey();
    }
  }
}

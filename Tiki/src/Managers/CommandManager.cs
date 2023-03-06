using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tiki.src.Managers;
internal static class CommandManager
{ 
  private static CommandService CommandService = ServiceManager.GetService<CommandService>();

  public static async Task LoadCommandsAsync()
  { 
    await CommandService.AddModulesAsync(Assembly.GetEntryAssembly(), ServiceManager.ServiceProvider);

    foreach(CommandInfo info in CommandService.Commands)
      Console.WriteLine($"Command: '{info.Name}' has been loaded!");
  }
}

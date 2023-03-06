using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiki;
internal class Config
{
  internal class DiscordConfig
  { 
    /// <summary>Discord Token</summary>
    public string DiscordToken { get; set; }

    /// <summary>Discord Command Prefix</summary>
    public string DiscordCommandPrefix { get; set; }
  }
}

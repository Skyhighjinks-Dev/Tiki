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
    public string DiscordToken { get; set; }
    public string DiscordCommandPrefix { get; set; }
  }
}

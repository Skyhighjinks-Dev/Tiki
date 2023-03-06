using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiki.src.Managers;
public static class ServiceManager
{
  /// <summary>Service Provider</summary>
  public static IServiceProvider ServiceProvider { get; private set; }


  /// <summary>
  /// Builds and sets service provider from Service Collection
  /// </summary>
  /// <param name="nCollection">Collection of services</param>
  public static void SetProvider(ServiceCollection nCollection) => ServiceProvider = nCollection.BuildServiceProvider();


  /// <summary>
  /// Retreives a specific service from DI 
  /// </summary>
  /// <typeparam name="T">Type of service</typeparam>
  /// <returns>Service if found</returns>
  public static T GetService<T>() where T: new() => ServiceProvider.GetRequiredService<T>();
}

# Nano Dependency Injector

Easy to apply dependency injection into .NET Core Projects with Attributes like Spring Boot Applications

## Installation

```bash
 dotnet add package Nano.DependencyInjector
```

## Usage

Define injection lifetime attribute to your injection class (Singleton, Scoped or Transient)

### Singleton Example

```cs
    public interface ISingletonTestService
    {
        string Test();
    }

    [Singleton] // Add attribute to service class
    public class SingletonTestService : ISingletonTestService
    {
        public SingletonTestService()
        {
            Console.WriteLine("Created singleton lifetime instance");
        }

        public string Test()
        {
            return "Singleton";
        }
    }
```

### Scoped Example

```cs
    public interface IScopedTestService
    {
        string Test();
    }

    [Scoped] // Add attribute to service class
    public class ScopedTestService : IScopedTestService
    {
        public ScopedTestService()
        {
            Console.WriteLine("Created scoped lifetime instance");
        }

        public string Test()
        {
            return "Scoped";
        }
    }
```

### Transient Example

```cs
    public interface ITransientTestService
    {
        string Test();
    }

    [Transient] // Add attribute to service class
    public class TransientTestService : ITransientTestService
    {
        public TransientTestService()
        {
            Console.WriteLine("Created transient lifetime instance");
        }

        public string Test()
        {
            return "Transient";
        }
    }
```

### Register Dependencies to IoC

Add code to ```ConfigureServices``` on Startup.cs file and it will register all dependencies

```cs
    services.RegisterDependencies(Assembly.GetExecutingAssembly());
```

## TODO List

* [x] Setup CI for publishing package on nuget
* [ ] Add unit tests
* [ ] Register named typed dependency
* [ ] Inject name typed dependency
* [ ] Add property injection without constructor
* [ ] Register injectable multiple interfaces
* [ ] Generic interfaces

## Contribution

Feel the free for contribution. Open issues, PR and contact to me

## License

This project is licensed under the MIT License

This document will cover various information that will help you getting started
with development using the Entity Framework Core on the FLEA project.

# Installing `dotnet-ef`
`dotnet-ef` is the main CLI used for interacting with the entity framework tooling.
This is used for things such as generating database migrations or generating code
models from a pre-existing database.

If you have the `dotnet` cli tool installed you can easily install it using the following
command:
```
dotnet tool install --global dotnet-ef
```

Note: Beware that for some custom installations of the `dotnet` cli tool
the `dotnet-ef` executable may be installed to a folder that is not in your
path thus causing errors when executing entity framework commands through
the `dotnet` command.

# Adding new models

When adding a new model to the system the entity framework must be made aware of the
model in one of two ways. The first method is to add a new `DbSet<T>` property for it
on the relevant `DbContext` class (`BingoContext`). The second method is to have it
detected recursively through another model that the framework is already aware of.
To give an example: the `Event` class contains a `List<Reservation>` property containing
all reservations for the given event. This automatically cause the `Reservation` class
to be scanned, likewise the `Customer` class will be scanned since the `Reservation`
class refers to the `Customer` instance that owns the `Reservation`.

Another thing to note is that all models should have a primary key of some sort.
Typically this will just be the property:

```cs
public int Id { get; set; }
```


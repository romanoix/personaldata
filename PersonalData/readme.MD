Project compilation instructions:

- compile the project using the following command:

```
{compiler path}msbuild.exe "{project path}PersonalData.csproj" /p:OutputPath=output;Config=Release
```

- run the compiled program located in the directory `output`:

```
PersonalData.exe
```

The program uses:

- mssql database. Three tables: Person, Address, Phone (connect HasMany) for the use of many address and numper phone for person.
- nHibernate and FluentHibernate to mapping
- Ninject IcO container

Functionality of the program:

- add new people
- modify the person's details,
- delete the entire record of this person

The program is missing the MVVM - time out.

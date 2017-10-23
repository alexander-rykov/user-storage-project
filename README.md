# UserStorage Project

The goal of this project is to create an easily configured distributed application that has open WCF API and communicates its state through the network.

![UserStorage project overview](images/UserStorageOverview.png "UserStorage project overview")

UserStorage service is a simple service that stores user records and provides an API for managing user records and searching. It is possible to run several instances of this service and share user records between them. The only instance that allows READ and WRITE operations is called MASTER NODE. The other instances allow only READ operations. Those are known as SLAVE NODES. The only one READ operation in API is SEARCH, and there are two WRITE operations - ADD and REMOVE. That means UserStorage service can operate in two modes - MASTER and SLAVE. Responsibilities of the service in MASTER mode includes spreading the changes to all services that operate in SLAVE mode.

In other words, MASTER NODE accepts READ (SEARCH) and WRITE (ADD/REMOVE) operations, changes its state, and sends an update to all SLAVES NODE that accepts only READ (SEARCH) operations. If a client sends WRITE request to a SLAVE NODE, the node replies with an error.

Described approach when MASTER NODE owns original data and other SLAVE NODES have only the copy is knows [MASTER-SLAVE data replication](https://ruhighload.com/post/%D0%A0%D0%B5%D0%BF%D0%BB%D0%B8%D0%BA%D0%B0%D1%86%D0%B8%D1%8F+%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D1%85). Possible solutions here are:
* MASTER NODE sends updates to all SLAVE NODES by himself.
* SLAVE NODES sends a request to MASTER NODE and MASTER replies with a bunch of updates.
* Other...

We recommend using the first approach, because we think that this solution is simpler that others.

Also, a MASTER NODE has a persistent storage for user record information when the application is not working. SLAVE NODES have only in-memory storage, and they do not save its state when they are not running. A persistent storage uses the file system to save user records when an application is shutting down and load them when it starts. A good question here is how to initialize the internal state of a SLAVE NODE when an application starts. The answer to this question is a part of the architectural design of this project.

A MASTER NODE sends updates to SLAVE NODES using TCP as a [transport channel](https://en.wikipedia.org/wiki/List_of_network_protocols_(OSI_model)#Layer_4_.28Transport_Layer.29) and [internet sockets](https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets) as endpoints.

The one main thing about this project is that the final application should be configurable, and all application settings should be placed in App.config file. SLAVE NODE is the same application as a MASTER NODE except differences in application configuration file.


## Template

In the [UserStorage](UserStorage) folder you can find a solution template that you can use for building your own application. Let's take a look at the C# projects in the folder:

* [UserStorageApp](UserStorage/UserStorageApp) - a console application project with predefined [App.config](UserStorage/UserStorageApp/App.config). This project should not contain any service related code, only initialization and configuration logic. The configuration file has a custom section that is named _serviceConfiguration_. This section is for defining services configuration and settings. Visual Studio also provides IntelliSense support for this section because the section schema is defined in [ServiceConfiguration.xsd](UserStorage/UserStorageApp/ServiceConfiguration.xsd) file. 
* [UserStorageServices](UserStorage/UserStorageServices) - a class library project for all service related code.
* [UserStorageServices.Tests](UserStorage/UserStorageServices.Tests) - a class library project with all unit tests for service related behavior.
* [ServiceConfigurationSection](UserStorage/ServiceConfigurationSection) - a class library project that stores classes for handling _serviceConfiguration_ custom section in App.config.

UserStorage service operates over an entity that describes a user and has relevant name - [User class](UserStorage/UserStorageServices/User.cs) in UserStorageServices project. This class is pretty simple, and has FirstName, LastName and Age fields only.

```cs
class User
{
	public string FirstName { get; set; }

	public string LastName { get; set; }

	public int Age { get; set; }
}
```

UserStorageServices project also has [UserStorageService class](UserStorage/UserStorageServices/UserStorageService.cs) that is a template for UserStorage service you will be working with.

It is worth mentioning that this code is only the initial template - you are allowed not only to add new code, but also the code refactor it in a way you like.

We encourage you to practice TDD and actively use Git during this exersise. Here are some principles that might be useful for you:
* [Test-First](http://www.extremeprogramming.org/rules/testfirst.html)
* [Red-Green-Refactor cycle](http://www.jamesshore.com/Blog/Red-Green-Refactor.html)
* [Do commit early and often](https://sethrobertson.github.io/GitBestPractices/#commit)
* [Branch-per-Feature](http://dymitruk.com/blog/2012/02/05/branch-per-feature/)


## Prepare

- [ ] Create a new repository on github. Move all content of the master branch in this repository to your new repository.

- [ ] Install [StyleCop](https://github.com/StyleCop/StyleCop) or [Visual StyleCop](https://www.youtube.com/watch?v=0OMuzHRrScw). Open UserStorage solution and run StyleCop to check your code and to make sure that there are no code issues.

- [ ] Check unstaged files in your repository.

```sh
$ git status
On branch master

Initial commit

Untracked files:
  (use "git add <file>..." to include in what will be committed)
...
```

- [ ] Add files to the [staging area](https://git-scm.com/book/ru/v1/Введение-Основы-Git#Три-состояния). Check status of staged files.

```sh
$ git add *
$ git status
...
```

- [ ] Review changes using [git diff](https://git-scm.com/book/ru/v1/Основы-Git-Запись-изменений-в-репозиторий#Просмотр-индексированных-и-неиндексированных-изменений). (Notice that **git diff** doesn't return any changes anymore.)

```sh
$ git diff
(no output)
$ git diff --staged
(changes output)
```

- [ ] [Commit](https://git-scm.com/book/ru/v1/Основы-Git-Запись-изменений-в-репозиторий) and publish all changes. Check status.

```sh
$ git commit -m "Add UserStorage template."
...
$ git status
On branch master
nothing to commit, working directory clean
```

- [ ] Edit README.md and mark all checkboxes in this section. Check status and review changes. Commit changes.

```sh
$ git status
$ git diff --staged
(no output)
$ git diff
(changes output)
$ git add *.md
$ git status
$ git diff
(no output)
$ git diff --staged
(changes output)
$ git commit -m "Mark completed items."
[master ...] Mark completed items.
 1 file changed, 1 insertion(+), 1 deletion(-)
$ git status
On branch master
nothing to commit, working directory clean
```

- [ ] Publish changes to github.

```sh
$ git push
```

Now you have the initial version of your repository uploaded to the github.


## Step 1

The class diagram below shows the current [relationship](http://creately.com/blog/diagrams/class-diagram-relationships) between Client and UserStorageService classes.

![Client and UserStorageService](images/ClientAndServiceBeginning.png "Client and UserStorageService")


- [ ] [Create a new branch](https://git-scm.com/book/ru/v1/Ветвление-в-Git-Основы-ветвления-и-слияния) with name "step1", and switch to this branch. Make sure that you are on "step1" branch before continue.

```sh
$ git checkout -b step1
$ git branch
  master
* step1
```

- [ ] Add a new _Id_ field to the _User_ class. Use System.Guid as a field type. The field value should uniquely identify a user in the storage. Review changes. Commit changes.

- [ ] Add an internal storage to _UserStorageService_ class. Consider collections from [System.Collections.Generic](https://msdn.microsoft.com/en-us/library/system.collections.generic(v=vs.110).aspx) namespace. A new identifier should be populated and assigned to each new entity before adding it to a collection. Implement Count property getter to return the amount of users in the storage. Review and commit.

- [ ] _UserStorageService_ class contains Add() method that adds a new user to the storage. The method has one guard clause and one validation statement. Tests for the methods of the class are located in _UserStorageServiceTests_ class. Think what more validation rules you can add here. Add tests for those rules, and then write code to implement them.

Test-First: add use cases in form of tests to _UserStorageServiceTests_ class (**red** tests), and only then add implementation to the Add method (make your tests **green**).

Review and commit.

- [ ] Test-First: add use cases (red) and then add an implementation for Remove method (green). Review. Commit.

- [ ] Test-First: add use cases (red) and then add an implementation for Search method (green). Use cases:
  * Search by FirstName.
  * Search by LastName.
  * Search by Age.

Review and commit.

- [ ] Add a new bool field _IsLoggingEnabled_ to _UserStorageService_ class, and add logging functionality to Add method:

```cs
if (IsLoggingEnabled)
{
    Console.WriteLine("Add() method is called.");
}
```

Add logging to Remove and Search methods too. Review and commit.

- [ ] Run StyleCop to make sure the code you have added fits defined code standards. Fix all code issues StyleCop identified. Review and commit.

```sh
$ git status
$ git diff
$ git commit -m "Fix StyleCop issues."
```

- [ ] Mark all completed items in README.md. Review and commit.

- Publish "step1" branch to [remote branch](https://git-scm.com/book/ru/v2/Ветвление-в-Git-Удалённые-ветки) on github.

```sh
$ git push -u origin step2
```

- Switch to master branch. Merge "step1" branch into master. Publish changes to master branch on github.

```sh
$ git checkout master
$ git branch
* master
  step1
$ git merge --squash step1
$ git status
$ git diff --staged
$ git commit -m "Add implementation for Add, Remove and Search methods. Add logging."
$ git log --oneline
$ git status
On branch master
nothing to commit, working directory clean
```


## Step 2

The class diagram below shows the application state after all refactorings in the current step.

![Client and UserStorageService Step 2](images/ClientAndServiceStep2.png "Client and UserStorageService Step 2")

- [ ] Create a new branch with name "step2", and switch to this branch.

- [ ] [Extract Class refactoring](https://refactoring.guru/extract-class): extract functionality of generating new user identifier into a new class.
  * Create a new interface in _UserStorageServices_ project, give it a meaningful name.
  * Test-First: create a new class in _UserStorageServices_ project that implements the interface, and move your code (generation of a new identifier) from _UserStorageService_ class to your new class.
  * Modify _UserStorageService_ to create a new instance of your new class, and use it to generate an identifier when adding a new user.

Run all tests to make sure that _UserStorageService_ works as expected.

Review and commit.

- [ ] Extract Class refactoring: extract functionality of validating user data when adding a new user to the storage.
  * Create a new interface in _UserStorageServices_ project, give it a meaningful name.
  * Test-First: create a new class in _UserStorageServices_ project that implements the interface, and move your code (validation of the user data) from _UserStorageService_ class to your new class.
  * Modify _UserStorageService_ to create a new instance of your new class, and use it to validate a user data when adding a new user.

Run all tests to make sure that _UserStorageService_ works as expected.

Review and commit.

- [ ] [Extract Interface refactoring](https://refactoring.guru/extract-interface): extract an interface for the UserStorageService class.
  * Create a new interface _IUserStorageService_ in _UserStorageServices_ project, give it a meaningful name.
  * Add all public methods and properties from _UserStorageService_ class to your new interface.
  * Refactor _userStorageService field in _Client_ class: change the field type to your new interface.
  * Refactor constructor in _Client_ class to use [Constructor Injection](http://sergeyteplyakov.blogspot.com.by/2012/12/di-constructor-injection.html) to set _userStorageService_ field.

Run tests, review and commit.

- [ ] Configure logging using App.config.
  * Refactor your _UserStorageService_ class to use [boolean switch](https://msdn.microsoft.com/en-us/library/system.diagnostics.booleanswitch%28v=vs.110%29.aspx) instead of _IsLoggingEnabled_ property.
  * Use _enableLogging_ boolean switch that is already added to your App.config.
  * Remove unnecessary _IsLoggingEnabled_ property.
  * Run application with _enableLogging_ switch enabled and disabled to make sure that logging works properly.

Run tests, review and commit.

- [ ] Run StyleCop. Fix issues. Commit.

- [ ] Mark. Commit.

- Publish "step2" branch to github.

- Switch to master branch. Merge "step2" branch into master. Publish changes to master branch on github.


## Step 3

- [ ] New branch "step3".

- [ ] Composite validator.
  * Refactor your class that validates user data to extract validation logic for each validation rule to a separate class.
  * Use [Composite design pattern](https://refactoring.guru/design-patterns/composite) to create a composite validator.

Run tests, review and commit.

![Composite Validator](images/ClientAndServiceCompositeValidator.png "Composite Validator")

- [ ] Validation exceptions. Create a custom exception for each validation case.Examples: FirstNameIsNullOrEmptyException, LastNameExceedsLimitsException, AgeExceedsLimisException. Each validator rule class should throw its own exception. Modify tests.

Run tests, review and commit.

- [ ] Extended search functionality. Add new functionality to your Search method for supporting these use cases:
  * Search by FirstName and LastName.
  * Search by FirstName and Age.
  * Search by LastName and Age.
  * Search by FirstName, LastName and Age.

Add new tests. Run tests, review and commit.

- [ ] Extract logging functionality.
  * Extract Class: extract logging functionality to a separate class that inherits _IUserStorageService_ class.
  * Use [Decorator design pattern](https://refactoring.guru/design-patterns/decorator) to create a log decorator.
  * Make _UserStorageServiceDecorator_ class abstract.
  * Modify your application code to create a new log decorator and pass it to the _Client_ class instead of _UserStorageService_ class.

![Log Decorator](images/ClientAndServiceLogDecorator.png "Log Decorator")

Run tests, review and commit.

- [ ] Refactor _UserStorageServiceLog_ to use [Trace Listeners](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/trace-listeners) to log all _UserStorageService_ method calls.
  * Configure [TextWriterTraceListener](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.textwritertracelistener) [by using a configuration file](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/how-to-create-and-initialize-trace-listeners).
  * Replace Console.WriteLine method calls with appropriate Debug or Trace methods.
  * Add more listeners to the configuration file - for console, XML and CSV output.

Run tests, review and commit.

- [ ] Run StyleCop, fix issues, commit. Mark, commit. Publish "step3". Merge "step3" into master. Publish.


## Step 4


- [ ] Add a persistent storage for storing the service's internal state.
  * Store all necessary information in XML file. Create an [appSettings section](https://msdn.microsoft.com/en-us/library/system.configuration.configurationmanager.appsettings(v=vs.110).aspx) in App.config file, and add a new key-value pair to store the file name.
  * The service should be able to store all user records that are added to user storage service to a file on disk using XML format.
  * The service should also store information about an unique identifier generation.
  * The service should be able to restore it's state using the provided persistent storage.


## Step 5

- [ ] Create a new class for an update notification to reflect the changes on MASTER NODE:
  * ADD event when a new user is added to the user storage service.
  * REMOVE event when an existed user is removed from the user storage service.

- [ ] Refactor the user storage service class to add an user service mode. The service should have only two modes: MASTER and SLAVE.
  * MASTER NODE should support all operations (add, remove, search), and have ability to use persistent storage.
  * SLAVE NODE should support only search operation, and it should throw an [user-defined exception](https://msdn.microsoft.com/en-us/library/87cdya3t(v=vs.110).aspx). SLAVE NODE should have no persitant storage, and the only way to change the service state should be to send the service a notification, a message with update information.

- [ ] Refactor infrastructure code: each instance of the user storage service class should be activated in a separate AppDomain. Both master and slave instances should be placed in a dedicated application domain.

![UserServiceApplication with AppDomains](images/UserServiceWithAppDomains.png "UserServiceApplication with AppDomains")

- [ ] Refactor communication between instances to send update notifications to all SLAVE NODES about the changes on MASTER NODE.

- [ ] Use App.config to store the application service configuration. Use [custom configuration sections](https://habrahabr.ru/post/128517/) in App.config to bring more structure to your configuration file.


## Step 6

- [ ] Refactor the user storage service class to add new functionality to communicate over the network using TCP protocol:
  * For MASTER NODE - send update notifications to all registered SLAVE NODE endpoints.
  * For SLAVE NODE - listen to endpoint and receive update notifications from MASTER NODE.
  * Note: If you use the other communication approach for MASTER-SLAVE communication, those items wouldn't work for you.
  * Note: Use [NetworkStream](https://msdn.microsoft.com/ru-ru/library/system.net.sockets.networkstream%28v=vs.110%29.aspx), [TcpClient](https://msdn.microsoft.com/ru-ru/library/system.net.sockets.tcpclient(v=vs.110).aspx) and [TcpListener](https://msdn.microsoft.com/ru-ru/library/system.net.sockets.tcplistener(v=vs.110).aspx) or [Socket](https://msdn.microsoft.com/ru-ru/library/system.net.sockets.socket(v=vs.110).aspx) to establish communication channel between nodes.

![Master-Slave communication for one application](images/UserServiceApplicationSimpleCase.png "Master-Slave communication for one application")

- [ ] Use App.config to store the information about endpoints (hosts and ports) for all registered services.

- [ ] Refactor your application to ensure that your application can work in distributed mode:
  * There is at least one application with service configuration that works as MASTER node.
  * There are at least two applications with service configurations that work as SLAVE node.
  * There is no need to write code that synchronize MASTER and SLAVE nodes, just run the applications in the order you need.

![Master-Slave communication for distributed application](images/UserServiceApplicationDistributed.png "Master-Slave communication for distributed application")
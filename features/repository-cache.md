# Feature - Repository Cache

- Apply Decorator design pattern to repository:

![Repository Decorators](../images/RepositoryDecorators.png "Repository Decorators")

- Implement _UserRepositoryDelayer_ - this class should wait for a timeout before calling the next repository in the decorator chain. The goal of this class is to emulate slow storage.

- Implement _UserRepositoryCache_ using [MemoryCache](https://habrahabr.ru/company/infopulse/blog/258247/) class. This class should store a user object in a cache before it will expire (use expirationInterval).

- Create a chain of decorators: UserRepositoryCache->UserRepositoryDelayer->UserRepositoryCache(WithState).
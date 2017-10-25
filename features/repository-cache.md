# Feature - Repository Cache

**Feature branch**
feature-repository-cache

**Feature description** ([md](https://github.com/epam-lab/user-storage-project/blob/version3/features/repository-cache.md))
Create a repository cache to improve the performance of getting user record from a a slow repository.

**Implementation details**

- Apply Decorator design pattern.

![Repository Decorators](https://github.com/epam-lab/user-storage-project/raw/master/images/RepositoryDecorators.png "Repository Decorators")

- Implement _UserRepositoryDelayer_ - this decorator should wait for a timeout before calling the next repository in the decorator chain. The goal of this class is to emulate slow storage.

- Implement _UserRepositoryCache_ using [MemoryCache](https://habrahabr.ru/company/infopulse/blog/258247/) class. This class should have a parameter expirationTimeoutInterval that specifies a live time of a user in the cache.

- Create a chain of decorators: UserRepositoryCache->UserRepositoryDelayer->UserRepositoryCache(WithState).
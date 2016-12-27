using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace CourseSelecting.EntityFramework.Repositories
{
    public abstract class CourseSelectingRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<CourseSelectingDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected CourseSelectingRepositoryBase(IDbContextProvider<CourseSelectingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class CourseSelectingRepositoryBase<TEntity> : CourseSelectingRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected CourseSelectingRepositoryBase(IDbContextProvider<CourseSelectingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}

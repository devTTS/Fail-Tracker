using NHibernate;
using StructureMap;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Infrastructure.DependencyResolution
{
	public static class IoC
	{
		public static IContainer Initialize()
		{
			ObjectFactory.Initialize(x =>
						{
							x.Scan(scan =>
									{
										scan.TheCallingAssembly();
										scan.AssembliesFromApplicationBaseDirectory(assembly => assembly.FullName.Contains("FailTracker"));
										scan.AddAllTypesOf(typeof (QueryBase<,>));
										scan.WithDefaultConventions();
									});

							x.For<ISession>().Use(NHibernateBootstrapper.GetSession);
							x.For<IProvideQueries>().Use<NHibernateQueryProvider>();
						});

			return ObjectFactory.Container;
		}
	}
}
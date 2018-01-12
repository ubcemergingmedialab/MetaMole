using System.Collections.Generic;

namespace Meta
{
    /// <summary>
    /// Contains the components built from the MetaFactory
    /// </summary>
    internal class MetaFactoryPackage
    {
        public MetaFactoryPackage()
        {
            EventReceivers = new List<IEventReceiver>();
            MetaContext = new MetaContext();
        }

        public List<IEventReceiver> EventReceivers;
        public IMetaContextInternal MetaContext;
    }
}

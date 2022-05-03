using ExcelUpload.Core.Services;
using Gamification.Shared.Core.Enums;
using Gamification.Shared.Core.Interfaces.Services.Connector;
using Gamification.Shared.Core;
using Outlook.Core.Services;
using System;
using ExcelUpload.Core.Interfaces;

namespace Gamification.Factory
{
    public class ConnectorFactory : IConnectorFactory
    {
        readonly IServiceProvider _container;

        public ConnectorFactory(IServiceProvider container)
        {
            _container = container;
        }

        public IConnectorClient CreateFor(ConnectorTypeEnum connectorType)
        {
            switch (connectorType)
            {
                case ConnectorTypeEnum.ExcelUpload:
                    return (ExcelUploadConnectorClient)_container.GetService(typeof(IExcelUploadClient));
                case ConnectorTypeEnum.Outlook:
                    return (OutlookConnectorClient)_container.GetService(typeof(IOutlookClient));
            }

            throw new ArgumentException($"Unknown connectorType '{connectorType}'");
        }
    }
}

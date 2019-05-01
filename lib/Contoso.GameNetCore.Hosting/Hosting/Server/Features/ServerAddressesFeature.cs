// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Contoso.GameNetCore.Hosting.Server.Features
{
    public class ServerAddressesFeature : IServerAddressesFeature
    {
        public ICollection<string> Addresses { get; } = new List<string>();

        public bool PreferHostingUrls { get; set; }
    }
}

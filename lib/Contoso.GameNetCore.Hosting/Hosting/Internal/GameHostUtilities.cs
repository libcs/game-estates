// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.Configuration;
using System;

namespace Contoso.GameNetCore.Hosting.Internal
{
    public class GameHostUtilities
    {
        public static bool ParseBool(IConfiguration configuration, string key) =>
            string.Equals("true", configuration[key], StringComparison.OrdinalIgnoreCase)
                || string.Equals("1", configuration[key], StringComparison.OrdinalIgnoreCase);
    }
}

﻿namespace MultiTenancyAzureAD.Core
{
    public class Tenant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}
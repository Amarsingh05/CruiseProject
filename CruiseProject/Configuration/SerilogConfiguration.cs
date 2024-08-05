namespace SKO.CruiseProject.Configuration
{
    public class SerilogConfiguration
    {
        public MinimumLogLevel MinimumLevel { get; set; }
        public List<WriteToSink> WriteTo { get; set; }
        public List<string> Enrich { get; set; }
        public ApplicationProperties Properties { get; set; }

        public class MinimumLogLevel
        {
            public string Default { get; set; }
            public Dictionary<string, string> Override { get; set; }
        }

        public class WriteToSink
        {
            public string Name { get; set; }
            public SinkArgs Args { get; set; }
        }

        public class SinkArgs
        {
            public string Path { get; set; }
            public string RollingInterval { get; set; }
            public string OutputTemplate { get; set; }
            public string Formatter { get; set; }
        }

        public class ApplicationProperties
        {
            public string Application { get; set; }
        }
    }
}

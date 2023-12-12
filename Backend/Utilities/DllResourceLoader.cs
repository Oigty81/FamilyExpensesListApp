﻿namespace Backend.Utilities
{
    using System.Reflection;
    using System.Text;

    public class DllResourceLoader : IResourceLoader
    {
        private readonly string resourceBase;

        public DllResourceLoader(string resourceBase)
        {
            this.resourceBase = resourceBase;
        }

        public byte[] ReadRessource(string url)
        {
            try
            {
                var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;

                if (assemblyLocation == null)
                {
                    return new byte[0];
                }

                var directoryName = System.IO.Path.GetDirectoryName(assemblyLocation);

                if (directoryName == null)
                {
                    return new byte[0];
                }

                Assembly assembly = Assembly.LoadFrom(Path.Combine(directoryName, this.resourceBase + ".dll"));

                string resourcePath = this.resourceBase + "." + url.Replace("/", ".");

                Stream? resourceStream = assembly.GetManifestResourceStream(resourcePath);

                if (resourceStream != null)
                {
                    BinaryReader streamReader = new BinaryReader(resourceStream);
                    return streamReader.ReadBytes((int)resourceStream.Length);
                }
            }
            catch (Exception e)
            {
                ////TODO: implement logservice
                Console.WriteLine(e.Message);
            }

            return Encoding.Default.GetBytes(url);
        }
    }
}

namespace RestSharp.Extensions
{
    using System;
    using System.Reflection;

    public static class UriExtensions
    {
        public static Uri ForceCanonicalPathAndQuery(this Uri uri)
        {
#if !SILVERLIGHT
            var pathAndQuery = uri.PathAndQuery;

            var flagsFieldInfo = typeof(Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
            if (flagsFieldInfo == null)
            {
                return uri;
            }

            var flags = (ulong)flagsFieldInfo.GetValue(uri);
            flags &= ~((ulong)0x30); // Flags.PathNotCanonical|Flags.QueryNotCanonical
            flagsFieldInfo.SetValue(uri, flags);
#endif
            return uri;
        }
    }
}
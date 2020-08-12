using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace leave_management.Services.Extensions
{
    public static class SessionExtensions
    {
        public static void ExtSet<T>(this IHttpContextAccessor session, string key, T value)
        {
            session.HttpContext.Session.Set(key, JsonSerializer.SerializeToUtf8Bytes(value));
        }

        public static T ExtGet<T>(this IHttpContextAccessor session, string key)
        {
            var value = session.HttpContext.Session.Get(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }

        public static void ExtSet<T>(this ISession session, string key, T value)
        {
            session.Set(key, JsonSerializer.SerializeToUtf8Bytes(value));
        }

        public static T ExtGet<T>(this ISession session, string key)
        {
            var value = session.Get(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}

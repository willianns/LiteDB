﻿using LiteDB;
using LiteDB.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiteDB.Demo
{
    public class TestInsertEngine
    {
        static Random RND = new Random();
        static string PATH = @"D:\insert-vf.db";
        static string PATH_LOG = @"D:\insert-vf-log.db";

        public static void Run(Stopwatch sw)
        {
            File.Delete(PATH);
            File.Delete(PATH_LOG);

            var settings = new EngineSettings
            {
                Filename = PATH,
                CheckpointOnShutdown = true
            };

            sw.Start();

            using (var db = new LiteEngine(settings))
            {
                db.Insert("col1", GetDocs(1, 100000), BsonAutoId.Int32);
            }

            sw.Stop();
        }

        static IEnumerable<BsonDocument> GetDocs(int start, int count)
        {
            var end = start + count;

            for (var i = start; i < end; i++)
            {
                yield return new BsonDocument
                {
                    ["_id"] = i, // Guid.NewGuid(),
                    ["rnd"] = Guid.NewGuid().ToString(),
                    ["name"] = "NoSQL Database",
                    ["birthday"] = new DateTime(1977, 10, 30),
                    ["phones"] = new BsonArray { "000000", "12345678" },
                    ["bytes"] = new byte[750],
                    ["active"] = true
                };
            }
        }
    }
}
﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphDB.Core.Persisted
{
  
    public class DataFileManager:PersistManagerBase<int, HashSet<int>>
    {

        public GraphDBEngine DB { get; set; }
        public DataFileManager( GraphDBEngine db) : base()
        {
            DB = db;
            DB.ApplyData += PersistTransaction;
        }


        protected override void SetTransaction(HashSet<int> container, int transaction)
        {
            container.Add(transaction / AppSettings.DATABLOCKSIZE);
        }

        protected override int GetTransaction(HashSet<int> container)
        {
            return container.First();
        }

        protected override void ClearTransaction(HashSet<int> container, int transaction)
        {
            container.Remove(transaction);
        }

        protected override void ProcessTransaction(int transaction)
        {
            Console.WriteLine($"Data Transaction - {transaction}");

            //Serialize(transaction);
        }
        public override void Dispose()
        {
            DB.ApplyData -= PersistTransaction;
            base.Dispose();
        }
    }
}

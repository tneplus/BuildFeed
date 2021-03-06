﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BuildFeed.Models
{
   public partial class Build
   {
      public Task<TypeOfSource[]> SelectAllSources(int limit = -1, int skip = 0)
      {
         return Task.Run(() => Enum.GetValues(typeof(TypeOfSource)) as TypeOfSource[]);
      }

      public Task<long> SelectAllSourcesCount()
      {
         return Task.Run(() => Enum.GetValues(typeof(TypeOfSource))
                                   .LongLength);
      }

      public async Task<List<BuildModel>> SelectSource(TypeOfSource source, int limit = -1, int skip = 0)
      {
         var query = _buildCollection.Find(new BsonDocument(nameof(BuildModel.SourceType), source))
                                     .Sort(sortByOrder)
                                     .Skip(skip);

         if (limit > 0)
         {
            query = query.Limit(limit);
         }

         return await query.ToListAsync();
      }

      public async Task<long> SelectSourceCount(TypeOfSource source) { return await _buildCollection.CountAsync(new BsonDocument(nameof(BuildModel.SourceType), source)); }
   }
}
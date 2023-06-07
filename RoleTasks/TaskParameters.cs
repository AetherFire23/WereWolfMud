using System;
using System.Collections.Generic;
using System.Linq;
using WereWolfUltraCool.Entities;

namespace WereWolfMud.RoleTasks
{
    public class TaskParameters : List<(string ParamType, string Id)>
    {
        public TaskParameters()
        {

        }

        public TaskParameters(List<(string ParamType, string Id)> list)
        {
            this.AddRange(list);
        }

        public TaskParameters(List<Tuple<string, string>> list)
        {
            this.AddRange(list.Select(x =>
            {
                (string ParamType, string Id) kvp = (x.Item1, x.Item2);
                return kvp;
            }));
        }

        public List<Guid> GetPlayerTargetIds()
        {
            var targets = this.Where(x => x.ParamType == nameof(Player)).ToList();
            return targets.Select(x=> new Guid(x.Id)).ToList();
        }
    }
}

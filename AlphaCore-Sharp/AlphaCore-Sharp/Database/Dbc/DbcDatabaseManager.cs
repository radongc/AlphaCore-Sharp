using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Database.Dbc
{
    // TODO: Add comments explaining this class.
    internal class DbcDatabaseManager
    {
        //** Skill **//

        internal class SkillHolder
        {
            public static Dictionary<int, SkillLine> Skills = new Dictionary<int, SkillLine>();

            public static void LoadSkill(SkillLine skill) => Skills[skill.ID] = skill;

            public static SkillLine? SkillGetByID (int skillID) => Skills[skillID];
        }

        public static List<SkillLine> SkillLineGetAll()
        {
            using (DbcModels models = new DbcModels()) 
            {
                return models.SkillLines.ToList();
            }
        }
    }
}

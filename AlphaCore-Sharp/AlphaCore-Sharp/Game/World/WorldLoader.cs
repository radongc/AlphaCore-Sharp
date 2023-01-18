using AlphaCore_Sharp.Database.Dbc;
using AlphaCore_Sharp.Database.Dbc.Models;
using AlphaCore_Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Game.World
{
    internal class WorldLoader
    {
        // TODO: Add comments explaining this class.
        public static void LoadData()
        {
            LoadSkills();
        }

        public static void LoadSkills()
        {
            List<SkillLine> skills = DbcDatabaseManager.SkillLineGetAll();
            int length = skills.Count;
            int count = 0;

            foreach (SkillLine skill in skills)
            {
                DbcDatabaseManager.SkillHolder.LoadSkill(skill);

                count++;
                Logger.Info($"Loading skill {count}/{length}...");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaCore_Sharp.Database.Dbc.Models;
using Dapper;
using MySqlConnector;

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
            MySqlConnection models = DbcModels.GetDbcConnection();
            models.Open();

            List<SkillLine> skills = models.Query<SkillLine>($"SELECT * FROM {SkillLine.TABLENAME}").ToList();
            models.Close();

            return skills;
        }
    }
}
